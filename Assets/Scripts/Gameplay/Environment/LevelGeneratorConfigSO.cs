using UnityEngine;

namespace Gameplay.Environment
{
    [CreateAssetMenu(fileName = "LevelGeneratorConfig", menuName = "Game/Level Generator Config")]
    public class LevelGeneratorConfigSO : ScriptableObject
    {
        [Header("Level Generation Settings")]
        public int MinWidth = 7;
        public int MaxWidth = 7;
        public float BlockSize = 1f;
        public float VisibleLevelHeight = 10f;
        public GameObject WallBlockPrefab;

        [Header("Player Settings")]
        public float GenerationThreshold = 5f;

        [Header("Path Generation Settings")]
        public int MinStraightBlocks = 2;
        public int MaxStraightBlocks = 4;
        public float TurnChance = 0.3f;
        public int TurnHeight = 2;

        [Header("Ability Generation Settings")]
        public GameObject AbilityBasePrefab;
        public int BlocksBetweenAbilities = 15;
    }
}