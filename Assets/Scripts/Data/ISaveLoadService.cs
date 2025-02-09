using System.Threading.Tasks;
using Data.Models;

namespace Data
{
    public interface ISaveLoadService
    {
        void Save(GameProgressModel progress);
        Task<GameProgressModel> AsyncLoad();
        GameProgressModel LoadSync();
    }
}