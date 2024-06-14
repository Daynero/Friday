using _Scripts.ScreenNavigationSystem;
using UnityEngine;

public abstract class ScreenBase : MonoBehaviour
{
    public ScreenName screenName;
    public abstract void Show(object data = null);
    public abstract void Hide();
}