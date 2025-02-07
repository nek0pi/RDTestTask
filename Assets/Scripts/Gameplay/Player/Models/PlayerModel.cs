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
        public float MoveYSpeed;
        public float DeathTimeout;

        public PlayerModel(PlayerConfigSO playerConfig)
        {
            IsMovable = playerConfig.InitMovable;
            IsInvincible = playerConfig.InitInvincible;
            MoveYSpeed = playerConfig.MoveYSpeed;
            DeathTimeout = playerConfig.DeathTimeout;
        }
    }
}