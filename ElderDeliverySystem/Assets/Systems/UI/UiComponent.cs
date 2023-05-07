using SystemBase.Core.Components;
using TMPro;
using UnityEngine;

namespace Systems.UI
{
    [SingletonComponent]
    public class UiComponent : GameComponent
    {
        public TextMeshProUGUI soulsCounter;
        public TextMeshProUGUI timer;
        public GameObject startScreen;
        public GameObject PauseScreen;
        public GameObject endScreen;
        public TextMeshProUGUI endScreenMessage;

        public Animator soulsCounterAnimator;
        public TextMeshProUGUI soulsCounterText;
        
        public void ShowStartScreen()
        {
            startScreen.SetActive(true);
            endScreen.SetActive(false);
            PauseScreen.SetActive(false);
            soulsCounter.enabled = false; 
            timer.enabled = false;
        } 
    }
}