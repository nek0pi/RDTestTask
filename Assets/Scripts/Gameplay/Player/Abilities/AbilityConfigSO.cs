using UnityEngine;

namespace Gameplay.Player.Abilities
{
    public abstract class AbilityConfigSO : ScriptableObject
    {
        public Sprite AbilitySprite;
        public float AbilityDuration;
    }
}