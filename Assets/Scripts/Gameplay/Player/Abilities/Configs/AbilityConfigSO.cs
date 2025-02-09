using UnityEngine;

namespace Gameplay.Player.Abilities.Configs
{
    public abstract class AbilityConfigSO : ScriptableObject
    {
        public Sprite AbilitySprite;
        public float AbilityDuration;
    }
}