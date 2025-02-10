using System.Threading.Tasks;
using Data.Models;
using JetBrains.Annotations;

namespace Data
{
    public interface ISaveLoadService
    {
        void Save(GameProgressModel progress);
        [CanBeNull] Task<GameProgressModel> AsyncLoad();
        [CanBeNull] GameProgressModel LoadSync();
    }
}