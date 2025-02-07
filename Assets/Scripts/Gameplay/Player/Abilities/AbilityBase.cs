namespace Gameplay.Player.Abilities
{
    public abstract class AbilityBase
    {
        public AbilityConfigSO AbilityConfig;
        public abstract void PowerUp(PlayerController playerController);
        public abstract void PowerDown(PlayerController playerController);
    }
}