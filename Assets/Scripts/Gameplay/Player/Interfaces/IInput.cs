using Utils;

namespace Gameplay.Player.Interfaces
{
    public interface IInput
    {
        void Init();
        ReactiveFloat GetX();
    }
}