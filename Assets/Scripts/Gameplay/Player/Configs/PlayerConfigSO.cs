using UnityEngine;

namespace Gameplay.Player.Configs
{
    [CreateAssetMenu(menuName = "Configs", fileName = "PlayerConfigSO", order = 0)]
    public sealed class PlayerConfigSO : ScriptableObject
    {
        public bool InitMovable;
        public bool InitInvincible;
        public float MoveYSpeed;
        public float DeathTimeout;
    }
}