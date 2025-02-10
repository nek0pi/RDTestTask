using System.Collections.Generic;
using Gameplay.Player;
using Lean.Pool;
using UnityEngine;

namespace Gameplay.Environment
{
    public class LevelGeneratorController : MonoBehaviour
    {
        [SerializeField] private LevelGeneratorConfigSO _config;
        [SerializeField] private PlayerController _player;

        private List<PathSegment> _pathSegments = new();
        private Queue<GameObject> _activeBlocks = new();
        private float _currentTopY = 0f;
        private int _currentPathX;
        private int _levelWidth;
        private Dictionary<Vector2Int, GameObject> _blockGrid = new();
        private int _straightBlockCount = 0;
        private int _blocksUntilNextAbility;

        private struct PathSegment
        {
            public Vector2Int start;
            public Vector2Int end;
            public bool isTurn;
            public int turnDirection;

            public PathSegment(Vector2Int start, Vector2Int end, bool isTurn, int turnDirection)
            {
                this.start = start;
                this.end = end;
                this.isTurn = isTurn;
                this.turnDirection = turnDirection;
            }
        }

        [ContextMenu("Regenerate Path")]
        public void RegeneratePath()
        {
            // Clean up existing blocks
            while (_activeBlocks.Count > 0)
            {
                GameObject block = _activeBlocks.Dequeue();
                if (block != null)
                {
                    LeanPool.Despawn(block);
                }
            }
            
            _blockGrid.Clear();
            _pathSegments.Clear();
            _currentTopY = 0f;
            _straightBlockCount = 0;
            _blocksUntilNextAbility = _config.BlocksBetweenAbilities;

            InitializeLevel();
            GenerateInitialSegments();
        }

        private void Start()
        {
            if (_player == null)
            {
                _player = FindObjectOfType<PlayerController>();
                if (_player == null)
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
            _levelWidth = Random.Range(_config.MinWidth, _config.MaxWidth + 1);
            // Always start from the middle block (for width 7, it would be block 4)
            _currentPathX = (_levelWidth) / 2;
            _straightBlockCount = 0;
            _blocksUntilNextAbility = _config.BlocksBetweenAbilities;
            Vector2Int startPoint = new Vector2Int(_currentPathX, 0);
            _pathSegments.Add(new PathSegment(startPoint, startPoint, false, 0));
        }

        private void GenerateNextSegment()
        {
            int newY = _pathSegments[_pathSegments.Count - 1].end.y + 1;
            Vector2Int startPoint = new Vector2Int(_currentPathX, newY);

            bool shouldTurn = _straightBlockCount >= _config.MaxStraightBlocks ||
                            (_straightBlockCount >= _config.MinStraightBlocks && Random.value < _config.TurnChance);

            if (shouldTurn)
            {
                int maxLeftTurn = _currentPathX - 1;
                int maxRightTurn = _levelWidth - 2 - _currentPathX;

                bool canTurnLeft = maxLeftTurn >= 1;
                bool canTurnRight = maxRightTurn >= 1;

                if (canTurnLeft || canTurnRight)
                {
                    bool turnLeft = canTurnLeft && (!canTurnRight || Random.value < 0.5f);
                    int maxTurn = turnLeft ? maxLeftTurn : maxRightTurn;
                    int turnDistance = Random.Range(1, maxTurn + 1);

                    int newPathX = _currentPathX + (turnLeft ? -turnDistance : turnDistance);

                    for (int h = 0; h < _config.TurnHeight; h++)
                    {
                        Vector2Int turnStart = new Vector2Int(_currentPathX, newY + h);
                        Vector2Int turnEnd = new Vector2Int(newPathX, newY + h);
                        _pathSegments.Add(new PathSegment(turnStart, turnEnd, true, turnLeft ? -1 : 1));
                        GenerateRow(newY + h);
                    }

                    _currentPathX = newPathX;
                    _currentTopY = (newY + _config.TurnHeight - 1) * _config.BlockSize;
                    _straightBlockCount = 0;
                }
                else
                {
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
            Vector2Int endPoint = new Vector2Int(_currentPathX, newY);
            _pathSegments.Add(new PathSegment(startPoint, endPoint, false, 0));
            GenerateRow(newY);
            _currentTopY = newY * _config.BlockSize;
            _straightBlockCount++;
        }

        private void GenerateRow(int y)
        {
            for (int x = 0; x < _levelWidth; x++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                if (_blockGrid.ContainsKey(pos))
                {
                    LeanPool.Despawn(_blockGrid[pos]);
                    _blockGrid.Remove(pos);
                }
            }

            PathSegment currentSegment = _pathSegments[_pathSegments.Count - 1];

            for (int x = 0; x < _levelWidth; x++)
            {
                bool shouldPlaceBlock = true;

                if (currentSegment.isTurn)
                {
                    int minX = Mathf.Min(currentSegment.start.x, currentSegment.end.x);
                    int maxX = Mathf.Max(currentSegment.start.x, currentSegment.end.x);
                    if (x >= minX && x <= maxX)
                    {
                        shouldPlaceBlock = false;
                    }
                }
                else if (x == _currentPathX)
                {
                    shouldPlaceBlock = false;
                }

                if (shouldPlaceBlock)
                {
                    Vector3 position = new Vector3(x * _config.BlockSize, y * _config.BlockSize, 0);
                    GameObject block = LeanPool.Spawn(_config.WallBlockPrefab, position, Quaternion.identity);

                    Vector2Int gridPos = new Vector2Int(x, y);
                    _blockGrid[gridPos] = block;
                    _activeBlocks.Enqueue(block);
                }
            }

            // Place ability if it's time
            _blocksUntilNextAbility--;
            if (_blocksUntilNextAbility <= 0 && _config.AbilityBasePrefab != null)
            {
                Vector3 abilityPosition = new Vector3(_currentPathX * _config.BlockSize, y * _config.BlockSize, 0);
                LeanPool.Spawn(_config.AbilityBasePrefab, abilityPosition, Quaternion.identity);
                _blocksUntilNextAbility = _config.BlocksBetweenAbilities;
            }
        }

        private void CheckAndGenerateNewSegments()
        {
            float playerY = _player.transform.position.y;
            float remainingHeight = _currentTopY - playerY;

            if (remainingHeight < _config.VisibleLevelHeight + _config.GenerationThreshold)
            {
                int segmentsNeeded = Mathf.CeilToInt((_config.VisibleLevelHeight + _config.GenerationThreshold - remainingHeight) / _config.BlockSize);
                for (int i = 0; i < segmentsNeeded; i++)
                {
                    GenerateNextSegment();
                }
            }
        }

        private void CleanupOldSegments()
        {
            float playerY = _player.transform.position.y;
            float cleanupThreshold = playerY - _config.VisibleLevelHeight;

            List<GameObject> blocksToRemove = new List<GameObject>();
            foreach (var block in _activeBlocks)
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
                _activeBlocks.Dequeue();
                Vector2Int gridPos = new Vector2Int(
                    Mathf.RoundToInt(block.transform.position.x / _config.BlockSize),
                    Mathf.RoundToInt(block.transform.position.y / _config.BlockSize)
                );
                _blockGrid.Remove(gridPos);
                LeanPool.Despawn(block);
            }
        }

        private void GenerateInitialSegments()
        {
            int segmentsNeeded = Mathf.CeilToInt(_config.VisibleLevelHeight / _config.BlockSize);
            for (int i = 0; i < segmentsNeeded; i++)
            {
                GenerateNextSegment();
            }
        }
    }
}