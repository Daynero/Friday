using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.ScreenNavigationSystem
{
    public class ScreenNavigationSystem : MonoBehaviour
    {
        public static ScreenNavigationSystem Instance { get; private set; }

        public List<ScreenBase> screenList;
        public IScreenTransition screenTransition;

        private Dictionary<ScreenName, ScreenBase> screens = new Dictionary<ScreenName, ScreenBase>();
        private ScreenBase currentScreen;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeScreens();

                // Ініціалізація переходу екранів, якщо не встановлено вручну
                if (screenTransition == null)
                {
                    screenTransition = gameObject.AddComponent<SimpleFadeTransition>();
                }

                // Запуск екрану за замовчуванням
                NavigateTo(ScreenName.Preview); 
            }
            else
            {
                Destroy(gameObject);
            }
        }


        private void InitializeScreens()
        {
            foreach (var screen in screenList)
            {
                if (!screens.ContainsKey(screen.screenName))
                {
                    screens.Add(screen.screenName, screen);
                    screen.gameObject.SetActive(false);
                }
                else
                {
                    Debug.LogError($"Screen name '{screen.screenName}' is already added!");
                }
            }
        }

        public void NavigateTo(ScreenName screenName, object data = null)
        {
            if (screens.TryGetValue(screenName, out ScreenBase newScreen))
            {
                if (currentScreen != null)
                {
                    // Вимкнення інтерактивності під час анімації
                    SetInteractivity(currentScreen.gameObject, false);

                    screenTransition.PlayHideAnimation(currentScreen.gameObject, () =>
                    {
                        currentScreen.Hide();
                        currentScreen.gameObject.SetActive(false);
                        ShowNewScreen(newScreen, data);
                    });
                }
                else
                {
                    ShowNewScreen(newScreen, data);
                }
            }
            else
            {
                Debug.LogError($"Screen {screenName} not found!");
            }
        }

        private void ShowNewScreen(ScreenBase newScreen, object data)
        {
            newScreen.gameObject.SetActive(true);

            // Вимкнення інтерактивності під час анімації
            SetInteractivity(newScreen.gameObject, false);

            screenTransition.PlayShowAnimation(newScreen.gameObject, () =>
            {
                newScreen.Show(data);
                currentScreen = newScreen;

                // Увімкнення інтерактивності після завершення анімації
                SetInteractivity(newScreen.gameObject, true);
            });
        }

        private void SetInteractivity(GameObject screenObject, bool isInteractive)
        {
            CanvasGroup canvasGroup = screenObject.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = screenObject.AddComponent<CanvasGroup>();
            }
            canvasGroup.interactable = isInteractive;
        }
    }
}
