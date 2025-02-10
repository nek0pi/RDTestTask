using UnityEngine;

namespace Gameplay.Player.Abilities.Configs
{
    [CreateAssetMenu(menuName = "Configs/SizeAbilityConfig", fileName = "SizeAbilityConfigSO")]
    public sealed class SizeAbilityConfigSO : AbilityConfigSO
    {
        public float ShrinkSize;
        public float OriginalSize;
    }
}