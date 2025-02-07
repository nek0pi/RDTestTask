namespace Gameplay.Player.Interfaces
{
    public interface IPowerUp
    {
        void Init(ICollide collide, PlayerController playerController);
    }
}