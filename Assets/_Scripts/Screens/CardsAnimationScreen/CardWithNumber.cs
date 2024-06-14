using System;
using TMPro;
using UnityEngine;

namespace _Scripts.Screens.CardsAnimationScreen
{
    public class CardWithNumber : MonoBehaviour
    {
        [SerializeField] private TMP_Text number;

        public void SetNumber(int i)
        {
            number.text = i.ToString();
        }
    }
}