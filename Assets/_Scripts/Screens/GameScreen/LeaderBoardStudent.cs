using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Screens.GameScreen
{
    public class LeaderBoardStudent : MonoBehaviour
    {
        [field:SerializeField] public TMP_Text NameText { get; set; }
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private Image bgImage;

        public void SetView(string name, int score)
        {
            NameText.text = name;
            scoreText.text = score.ToString();
        }
    }
}