using Gameplay.Player.Abilities.Configs;
using UnityEngine;

namespace Gameplay.Player.Abilities
{
    public abstract class AbilityBase : MonoBehaviour
    {
        public AbilityConfigSO AbilityConfig;
        public virtual void Init(PlayerController playerController){}
        public abstract void PowerUp(PlayerController playerController);
        public abstract void PowerDown(PlayerController playerController);
    }
}