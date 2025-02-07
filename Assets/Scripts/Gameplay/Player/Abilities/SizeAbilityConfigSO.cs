using UnityEngine;

namespace Gameplay.Player.Abilities
{
    [CreateAssetMenu(menuName = "Configs", fileName = "SizeAbilityConfigSO", order = 0)]
    public sealed class SizeAbilityConfigSO : AbilityConfigSO
    {
        public float SizeMultiplier;
    }
}