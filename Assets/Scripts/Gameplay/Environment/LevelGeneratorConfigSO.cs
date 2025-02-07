using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Environment
{
    [CreateAssetMenu(menuName = "Configs", fileName = "LevelGeneratorConfigSO", order = 0)]
    public class LevelGeneratorConfigSO : ScriptableObject
    {
        [Range(3, 11)] public int LevelWidth;
        [Range(3, 11)] public int LevelHeight;
        public BlockController BlockPrefab;
    }
}