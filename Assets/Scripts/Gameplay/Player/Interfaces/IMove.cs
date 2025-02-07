using Gameplay.Player.Models;

namespace Gameplay.Player.Interfaces
{
    public interface IMove
    {
        void Init(PlayerModel playerModel, IInput input);
    }
}