using Utils;

namespace Gameplay
{
    public interface IScoreService
    {
        int GetMaxScore();
        ReactiveInt GetCurrentScore();
    }
}