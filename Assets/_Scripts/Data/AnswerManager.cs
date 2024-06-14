using System.Collections.Generic;
using _Scripts.ScreenNavigationSystem;
using _Scripts.Screens.QuestionScreen.QuestionPanels;
using UnityEngine;

namespace _Scripts.Data
{
    public class AnswerManager : MonoBehaviour
    {
        public static AnswerManager Instance { get; private set; }

        public static List<string> PresentStudents = new();
        public static Dictionary<string, int> StudentScores = new();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}