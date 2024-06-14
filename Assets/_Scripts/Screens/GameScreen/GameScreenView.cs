using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using _Scripts.Data;

namespace _Scripts.Screens.GameScreen
{
    public class GameScreenView : ScreenBase
    {
        [SerializeField] private LeaderBoardStudent leaderBoardStudentPrefab;
        [SerializeField] private Transform content;
        [SerializeField] private TMP_Text currentStudentText;
        [SerializeField] private Button yesButton;
        [SerializeField] private Button noButton;

        private List<LeaderBoardStudent> leaderBoardStudents = new List<LeaderBoardStudent>();
        private List<string> availableStudents;
        private string currentStudent;
        private int currentPoints = 1;
        private string filePath;

        private void Start()
        {
            // Set file path to a subfolder in the Assets folder
            string directoryPath = Path.Combine(Application.dataPath, "StudentScores");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            filePath = Path.Combine(directoryPath, "StudentScores.txt");

            InitializeLeaderBoard();
            StartNewRound();
        }

        private void InitializeLeaderBoard()
        {
            foreach (var studentName in AnswerManager.PresentStudents)
            {
                var studentInstance = Instantiate(leaderBoardStudentPrefab, content);
                AnswerManager.StudentScores[studentName] = 0; // Initialize scores
                studentInstance.SetView(studentName, AnswerManager.StudentScores[studentName]);
                leaderBoardStudents.Add(studentInstance);
            }
        }

        private void StartNewRound()
        {
            availableStudents = new List<string>(AnswerManager.PresentStudents);
            PickRandomStudent();
            currentPoints = 1;
            yesButton.onClick.AddListener(OnYesButtonClicked);
            noButton.onClick.AddListener(OnNoButtonClicked);
        }

        private void PickRandomStudent()
        {
            if (availableStudents.Count == 0)
            {
                availableStudents = new List<string>(AnswerManager.PresentStudents);
            }

            int randomIndex = Random.Range(0, availableStudents.Count);
            currentStudent = availableStudents[randomIndex];
            availableStudents.RemoveAt(randomIndex);
            currentStudentText.text = $"{currentStudent} + {currentPoints}";
        }

        private void OnYesButtonClicked()
        {
            AnswerManager.StudentScores[currentStudent] += currentPoints;
            currentPoints = 1;
            UpdateLeaderBoard();
            SaveScoresToFile();
            PickRandomStudent();
        }

        private void OnNoButtonClicked()
        {
            currentPoints++;
            PickRandomStudent();
        }

        private void UpdateLeaderBoard()
        {
            foreach (LeaderBoardStudent student in leaderBoardStudents)
            {
                string studentName = student.NameText.text;
                if (AnswerManager.StudentScores.TryGetValue(studentName, out var score))
                {
                    student.SetView(studentName, score);
                }
            }
        }

        private void SaveScoresToFile()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var kvp in AnswerManager.StudentScores)
                {
                    writer.WriteLine($"{kvp.Key}: {kvp.Value}");
                }
            }
        }

        public override void Show(object data = null)
        {
        }

        public override void Hide()
        {
            yesButton.onClick.RemoveListener(OnYesButtonClicked);
            noButton.onClick.RemoveListener(OnNoButtonClicked);
        }
    }
}
