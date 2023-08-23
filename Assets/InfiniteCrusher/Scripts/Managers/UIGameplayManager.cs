using UnityEngine;

namespace InfiniteCrusher
{
    public class UIGameplayManager : MonoBehaviour
    {
        public static UIGameplayManager Instance { get; private set; }

        public UIGameplay UIGameplay;
        public UIReward UIReward;
  


        private void Awake()
        {
            Instance = this;
        }


        private void Start()
        {
            CloseAll();
            DisplayGameplayMenu(true);
        }

        public void CloseAll()
        {
            DisplayRewardMenu(false);
        }


        public void DisplayGameplayMenu(bool isActive)
        {
            UIGameplay.DisplayCanvas(isActive);
        }

        public void DisplayRewardMenu(bool isActive)
        {
            UIReward.DisplayCanvas(isActive);
        }
    }
}
