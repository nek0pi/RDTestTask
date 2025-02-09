using TMPro;
using UnityEngine;
using Utils;

namespace UI.Views
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        public void SubscribeToScoreChanges(ReactiveInt scoreValue)
        {
            scoreValue.OnChanged += SetScore;
        }

        public void SetScore(int score)
        {
            _scoreText.SetText(score.ToString("0000"));
        }
    }
}