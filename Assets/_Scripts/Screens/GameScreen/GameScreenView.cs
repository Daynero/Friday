using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using _Scripts.Data;
using _Scripts.ScreenNavigationSystem;

namespace _Scripts.Screens.GameScreen
{
    public class GameScreenView : ScreenBase
    {
        [SerializeField] private LeaderBoardStudent leaderBoardStudentPrefab;
        [SerializeField] private Transform content;
        [SerializeField] private Button yesButton;
        [SerializeField] private Button noButton;
        [SerializeField] private Button backButton; // Back button

        private List<LeaderBoardStudent> leaderBoardStudents = new List<LeaderBoardStudent>();
        private List<string> availableStudents;
        private string currentStudent;
        private int currentPoints = 1;
        private string filePath;

        private void Start()
        {
            // Set file path to a subfolder in the Assets folder
            backButton.onClick.AddListener(OnBackButtonClicked); // Initialize back button
        }

        private void InitializeLeaderBoard()
        {
            // Clear the current leaderboard display
            foreach (Transform child in content)
            {
                Destroy(child.gameObject);
            }
            leaderBoardStudents.Clear();

            foreach (var studentName in AnswerManager.PresentStudents)
            {
                var studentInstance = Instantiate(leaderBoardStudentPrefab, content);
                if (!AnswerManager.StudentScores.ContainsKey(studentName))
                {
                    AnswerManager.StudentScores[studentName] = 0; // Initialize scores
                }
                studentInstance.SetView(studentName, AnswerManager.StudentScores[studentName].ToString());
                leaderBoardStudents.Add(studentInstance);
            }
        }

        private void StartNewRound()
        {
            UpdateAvailableStudents();
            PickRandomStudent();
            currentPoints = 1;
            yesButton.onClick.AddListener(OnYesButtonClicked);
            noButton.onClick.AddListener(OnNoButtonClicked);
        }

        private void UpdateAvailableStudents()
        {
            availableStudents = new List<string>(AnswerManager.PresentStudents);
        }

        private void PickRandomStudent()
        {
            if (availableStudents.Count == 0)
            {
                UpdateAvailableStudents();
            }

            if (availableStudents.Count > 0)
            {
                int randomIndex = Random.Range(0, availableStudents.Count);
                currentStudent = availableStudents[randomIndex];
                availableStudents.RemoveAt(randomIndex);
                UpdateAnsweringStudent(currentStudent);
            }
        }

        private void OnYesButtonClicked()
        {
            if (AnswerManager.StudentScores.ContainsKey(currentStudent))
            {
                AnswerManager.StudentScores[currentStudent] += currentPoints;
                currentPoints = 1;
                UpdateLeaderBoard();
                SaveScoresToFile();
                PickRandomStudent();
            }
        }

        private void OnNoButtonClicked()
        {
            currentPoints++;
            PickRandomStudent();
        }

        private void UpdateLeaderBoard()
        {
            // Clear the current leaderboard display
            foreach (Transform child in content)
            {
                Destroy(child.gameObject);
            }
            leaderBoardStudents.Clear();

            // Sort students by score in descending order
            var sortedStudents = new List<KeyValuePair<string, int>>(AnswerManager.StudentScores);
            sortedStudents.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

            bool firstStudent = true;
            foreach (var kvp in sortedStudents)
            {
                if (AnswerManager.PresentStudents.Contains(kvp.Key))
                {
                    var studentInstance = Instantiate(leaderBoardStudentPrefab, content);
                    studentInstance.SetView(kvp.Key, kvp.Value.ToString());
                    studentInstance.SetCrown(firstStudent); // Установити корону першому студенту
                    firstStudent = false;
                    leaderBoardStudents.Add(studentInstance);
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

        private void OnBackButtonClicked()
        {
            yesButton.onClick.RemoveListener(OnYesButtonClicked);
            noButton.onClick.RemoveListener(OnNoButtonClicked);
            ScreenNavigationSystem.ScreenNavigationSystem.Instance.NavigateTo(ScreenName.Preview);
        }

        public override void Show(object data = null)
        {
            string directoryPath = Path.Combine(Application.dataPath, "StudentScores");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            filePath = Path.Combine(directoryPath, "StudentScores.txt");

            InitializeLeaderBoard();
            StartNewRound();
        }

        public override void Hide()
        {
            yesButton.onClick.RemoveListener(OnYesButtonClicked);
            noButton.onClick.RemoveListener(OnNoButtonClicked);
        }

        private void UpdateAnsweringStudent(string name)
        {
            foreach (var student in leaderBoardStudents)
            {
                if (student.NameText.text == name)
                {
                    student.ChangeColor(true);
                    student.SetView(name, $"{AnswerManager.StudentScores[name]} + {currentPoints}");
                }
                else
                {
                    student.ChangeColor(false);
                    student.SetView(student.NameText.text, AnswerManager.StudentScores[student.NameText.text].ToString());
                }
            }
        }
    }
}
