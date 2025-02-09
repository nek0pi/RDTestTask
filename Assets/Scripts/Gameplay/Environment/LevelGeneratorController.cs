using System.Collections.Generic;
using Gameplay.Player;
using Lean.Pool;
using UnityEngine;

namespace Gameplay.Environment
{
    // TODO Refactor this to have separate config class
    // TODO Refactor this to have proper variable names
    public class LevelGeneratorController : MonoBehaviour
    {
        [Header("Level Generation Settings")]
        [SerializeField] private int minWidth = 3;
        [SerializeField] private int maxWidth = 11;
        [SerializeField] private float blockSize = 1f;
        [SerializeField] private float visibleLevelHeight = 10f;
        [SerializeField] private GameObject wallBlockPrefab;
    
        [Header("Player Settings")]
        [SerializeField] private PlayerController player;
        [SerializeField] private float generationThreshold = 5f;
    
        [Header("Path Generation Settings")]
        [SerializeField] private int minStraightBlocks = 2;
        [SerializeField] private int maxStraightBlocks = 4;
        [SerializeField] private float turnChance = 0.3f;
        [SerializeField] private int turnHeight = 2; // Minimum height needed for turns
    
        private List<PathSegment> pathSegments = new List<PathSegment>();
        private Queue<GameObject> activeBlocks = new Queue<GameObject>();
        private float currentTopY = 0f;
        private int currentPathX;
        private int levelWidth;
        private Dictionary<Vector2Int, GameObject> blockGrid = new Dictionary<Vector2Int, GameObject>();
        private int straightBlockCount = 0; // Track current straight path length

        private struct PathSegment
        {
            public Vector2Int start;
            public Vector2Int end;
            public bool isTurn;
            public int turnDirection; // 0 = straight, -1 = left, 1 = right

            public PathSegment(Vector2Int start, Vector2Int end, bool isTurn, int turnDirection)
            {
                this.start = start;
                this.end = end;
                this.isTurn = isTurn;
                this.turnDirection = turnDirection;
            }
        }

        private void Start()
        {
            if (player == null)
            {
                player = FindObjectOfType<PlayerController>();
                if (player == null)
                {
                    Debug.LogError("PlayerController not found!");
                    enabled = false;
                    return;
                }
            }

            InitializeLevel();
            GenerateInitialSegments();
        }

        private void Update()
        {
            CheckAndGenerateNewSegments();
            CleanupOldSegments();
        }

        private void InitializeLevel()
        {
            levelWidth = Random.Range(minWidth, maxWidth + 1);
            currentPathX = levelWidth / 2;
            straightBlockCount = 0;
            Vector2Int startPoint = new Vector2Int(currentPathX, 0);
            pathSegments.Add(new PathSegment(startPoint, startPoint, false, 0));
        }

        private void GenerateNextSegment()
        {
            int newY = pathSegments[pathSegments.Count - 1].end.y + 1;
            Vector2Int startPoint = new Vector2Int(currentPathX, newY);
        
            // Force a turn if we've exceeded maxStraightBlocks
            bool shouldTurn = straightBlockCount >= maxStraightBlocks || 
                              (straightBlockCount >= minStraightBlocks && Random.value < turnChance);

            if (shouldTurn)
            {
                int maxLeftTurn = currentPathX - 1;
                int maxRightTurn = levelWidth - 2 - currentPathX;
            
                bool canTurnLeft = maxLeftTurn >= 1;
                bool canTurnRight = maxRightTurn >= 1;
            
                if (canTurnLeft || canTurnRight)
                {
                    bool turnLeft = canTurnLeft && (!canTurnRight || Random.value < 0.5f);
                    int maxTurn = turnLeft ? maxLeftTurn : maxRightTurn;
                    int turnDistance = Random.Range(1, maxTurn + 1);
                
                    int newPathX = currentPathX + (turnLeft ? -turnDistance : turnDistance);
                
                    // Generate turn segment with proper height
                    for (int h = 0; h < turnHeight; h++)
                    {
                        Vector2Int turnStart = new Vector2Int(currentPathX, newY + h);
                        Vector2Int turnEnd = new Vector2Int(newPathX, newY + h);
                        pathSegments.Add(new PathSegment(turnStart, turnEnd, true, turnLeft ? -1 : 1));
                        GenerateRow(newY + h);
                    }
                
                    currentPathX = newPathX;
                    currentTopY = (newY + turnHeight - 1) * blockSize;
                    straightBlockCount = 0;
                }
                else
                {
                    // Add straight segment if can't turn
                    AddStraightSegment(startPoint, newY);
                }
            }
            else
            {
                AddStraightSegment(startPoint, newY);
            }
        }

        private void AddStraightSegment(Vector2Int startPoint, int newY)
        {
            Vector2Int endPoint = new Vector2Int(currentPathX, newY);
            pathSegments.Add(new PathSegment(startPoint, endPoint, false, 0));
            GenerateRow(newY);
            currentTopY = newY * blockSize;
            straightBlockCount++;
        }

        private void GenerateRow(int y)
        {
            // Clear any existing blocks in this row
            for (int x = 0; x < levelWidth; x++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                if (blockGrid.ContainsKey(pos))
                {
                    LeanPool.Despawn(blockGrid[pos]);
                    blockGrid.Remove(pos);
                }
            }

            PathSegment currentSegment = pathSegments[pathSegments.Count - 1];

            for (int x = 0; x < levelWidth; x++)
            {
                bool shouldPlaceBlock = true;

                // Check if position is part of current path
                if (currentSegment.isTurn)
                {
                    int minX = Mathf.Min(currentSegment.start.x, currentSegment.end.x);
                    int maxX = Mathf.Max(currentSegment.start.x, currentSegment.end.x);
                    if (x >= minX && x <= maxX)
                    {
                        shouldPlaceBlock = false;
                    }
                }
                else if (x == currentPathX)
                {
                    shouldPlaceBlock = false;
                }

                if (shouldPlaceBlock)
                {
                    Vector3 position = new Vector3(x * blockSize, y * blockSize, 0);
                    GameObject block = LeanPool.Spawn(wallBlockPrefab, position, Quaternion.identity);
                
                    Vector2Int gridPos = new Vector2Int(x, y);
                    blockGrid[gridPos] = block;
                    activeBlocks.Enqueue(block);
                }
            }
        }

        private void CheckAndGenerateNewSegments()
        {
            float playerY = player.transform.position.y;
            float remainingHeight = currentTopY - playerY;

            if (remainingHeight < visibleLevelHeight + generationThreshold)
            {
                int segmentsNeeded = Mathf.CeilToInt((visibleLevelHeight + generationThreshold - remainingHeight) / blockSize);
                for (int i = 0; i < segmentsNeeded; i++)
                {
                    GenerateNextSegment();
                }
            }
        }

        private void CleanupOldSegments()
        {
            float playerY = player.transform.position.y;
            float cleanupThreshold = playerY - visibleLevelHeight;

            List<GameObject> blocksToRemove = new List<GameObject>();
            foreach (var block in activeBlocks)
            {
                if (block != null && block.transform.position.y < cleanupThreshold)
                {
                    blocksToRemove.Add(block);
                }
                else
                {
                    break;
                }
            }

            foreach (var block in blocksToRemove)
            {
                activeBlocks.Dequeue();
                Vector2Int gridPos = new Vector2Int(
                    Mathf.RoundToInt(block.transform.position.x / blockSize),
                    Mathf.RoundToInt(block.transform.position.y / blockSize)
                );
                blockGrid.Remove(gridPos);
                LeanPool.Despawn(block);
            }
        }

        private void GenerateInitialSegments()
        {
            int segmentsNeeded = Mathf.CeilToInt(visibleLevelHeight / blockSize);
            for (int i = 0; i < segmentsNeeded; i++)
            {
                GenerateNextSegment();
            }
        }

        public bool IsPositionValid(Vector2 position)
        {
            int x = Mathf.RoundToInt(position.x / blockSize);
            int y = Mathf.RoundToInt(position.y / blockSize);
            Vector2Int gridPos = new Vector2Int(x, y);
            return !blockGrid.ContainsKey(gridPos);
        }
    }
}