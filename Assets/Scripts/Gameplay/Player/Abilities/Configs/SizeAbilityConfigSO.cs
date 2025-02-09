using UnityEngine;

namespace Gameplay.Player.Abilities.Configs
{
    [CreateAssetMenu(menuName = "Configs", fileName = "SizeAbilityConfigSO", order = 0)]
    public sealed class SizeAbilityConfigSO : AbilityConfigSO
    {
        public float SizeMultiplier;
    }
}