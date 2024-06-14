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
        [SerializeField] private GameObject crown;
        [SerializeField] private GameObject crownBottom;

        private Color _orange = new(1, 0.7f, 0.01f);

        public void SetView(string name, string score)
        {
            NameText.text = name;
            scoreText.text = score;
        }

        public void ChangeColor(bool isAnswer)
        {
            bgImage.color = isAnswer ? _orange : Color.white;
        }

        public void SetCrown(bool isActive)
        {
            crown.SetActive(isActive);
            crownBottom.SetActive(isActive);
        }
    }
}