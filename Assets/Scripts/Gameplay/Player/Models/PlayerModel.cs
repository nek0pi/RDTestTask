using Gameplay.Player.Configs;

namespace Gameplay.Player.Models
{
    /// <summary>
    /// Potential improvement: For better performance: Best to keep it as struct
    /// </summary>
    public sealed class PlayerModel
    {
        public bool IsMovable;
        public bool IsInvincible;
        public bool IsDead;
        public float MoveYSpeed;
        public float DeathTimeout;
        public float MinX;
        public float MaxX;

        public PlayerModel(PlayerConfigSO playerConfig)
        {
            IsMovable = playerConfig.InitMovable;
            IsInvincible = playerConfig.InitInvincible;
            MoveYSpeed = playerConfig.MoveYSpeed;
            DeathTimeout = playerConfig.DeathTimeout;
            MinX = playerConfig.MinX;
            MaxX = playerConfig.MaxX;
            IsDead = false;
        }
    }
}