using System.Collections.Generic;
using System.Linq;
using _Scripts.Data;
using _Scripts.ScreenNavigationSystem;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Screens.PresentScreen
{
    public class PresentScreenView : ScreenBase
    {
        [SerializeField] private Button nextButton;
        [SerializeField] private List<StudentItem> studentItems;

        private readonly List<string> _presentStudents = new();

        private void Awake()
        {
            nextButton.onClick.AddListener(GoToNextScreen);
        }

        private void GoToNextScreen()
        {
            foreach (var student in studentItems.Where(student => student.IsPresent))
            {
                _presentStudents.Add(student.Name.text);
            }

            AnswerManager.PresentStudents = _presentStudents;
            ScreenNavigationSystem.ScreenNavigationSystem.Instance.NavigateTo(ScreenName.Game);
        }

        public void UpdateStudentItems()
        {
            _presentStudents.Clear();
            foreach (var student in studentItems)
            {
                if (AnswerManager.PresentStudents.Contains(student.Name.text))
                {
                    student.IsPresent = true;
                }
                else
                {
                    student.IsPresent = false;
                }
            }
        }

        public override void Show(object data = null)
        {
            UpdateStudentItems();
        }

        public override void Hide()
        {
        }
    }
}