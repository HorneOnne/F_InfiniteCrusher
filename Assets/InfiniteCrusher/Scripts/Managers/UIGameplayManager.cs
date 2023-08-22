using UnityEngine;

namespace InfiniteCrusher
{
    public class UIGameplayManager : MonoBehaviour
    {
        public static UIGameplayManager Instance { get; private set; }

        public UIGameplay UIGameplay;
  


        private void Awake()
        {
            Instance = this;
        }


        private void Start()
        {
            CloseAll();
        }

        public void CloseAll()
        {          
              
        }


        public void DisplayGameplayMenu(bool isActive)
        {
            UIGameplay.DisplayCanvas(isActive);
        }


    
    }
}
