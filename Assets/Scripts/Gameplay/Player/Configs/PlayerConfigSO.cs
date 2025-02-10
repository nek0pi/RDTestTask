using UnityEngine;

namespace Gameplay.Player.Configs
{
    [CreateAssetMenu(menuName = "Configs/PlayerConfig", fileName = "PlayerConfigSO")]
    public sealed class PlayerConfigSO : ScriptableObject
    {
        public bool InitMovable;
        public bool InitInvincible;
        public float MoveYSpeed;
        public float DeathTimeout;
        public float MinX;
        public float MaxX;
    }
}