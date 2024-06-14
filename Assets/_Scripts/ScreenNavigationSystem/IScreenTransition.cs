using System;
using UnityEngine;

public interface IScreenTransition
{
    void PlayShowAnimation(GameObject screen, Action onComplete);
    void PlayHideAnimation(GameObject screen, Action onComplete);
}
