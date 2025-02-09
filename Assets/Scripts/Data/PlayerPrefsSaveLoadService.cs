using System.Threading.Tasks;
using Data.Models;
using UnityEngine;

namespace Data
{
    public class PlayerPrefsSaveLoadService : MonoBehaviour, ISaveLoadService
    {
        public void Save(GameProgressModel progress)
        {
            // Convert the GameProgressModel to json
            var json = JsonUtility.ToJson(progress);
            PlayerPrefs.SetString("progress", json);
        }

        public Task<GameProgressModel> AsyncLoad()
        {
            // This is just a placeholder, not really needed for PlayerPrefs ofc
            return Task.FromResult(LoadSync());
        }

        public GameProgressModel LoadSync()
        {
            // Load the json from PlayerPrefs
            var json = PlayerPrefs.GetString("progress");
            // Convert the json to GameProgressModel
            return JsonUtility.FromJson<GameProgressModel>(json);
        }
    }
}