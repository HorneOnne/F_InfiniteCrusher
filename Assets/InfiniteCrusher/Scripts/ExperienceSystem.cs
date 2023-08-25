using UnityEngine;
using System.Numerics;

namespace InfiniteCrusher
{
    public class ExperienceSystem : MonoBehaviour
    {
        public static ExperienceSystem Instance { get; private set; }
        public static event System.Action OnLevelUp;
        public static event System.Action OnExperienceGain;

        [field: SerializeField] public int CurrentLevel { get;  set; } = 1;
        [field: SerializeField] public int CurrentExperience { get;  set; } = 0;
        public int ExperienceToLevelUp { get;  set; } = 50; // Initial experience required to level up
        [field: SerializeField] public bool LockExpericenGain { get; set; } = false;
        private float _experienceMultiplier = 1.5f; // Experience multiplier for each level


        private void Awake()
        {
            Instance = this;
        }

        public void GainExperience(int amount)
        {
            if (LockExpericenGain) return;

            CurrentExperience += amount;
            if (CanLevelUp())
            {
                LockExpericenGain = true;
            }
            OnExperienceGain?.Invoke();
        }

        public void LevelUp()
        {
            if(CanLevelUp())
            {
                CurrentExperience -= ExperienceToLevelUp;
                CurrentLevel++;
                ExperienceToLevelUp = Mathf.RoundToInt(ExperienceToLevelUp * _experienceMultiplier);

                OnLevelUp?.Invoke();
            }         
        }

        public bool CanLevelUp()
        {
            return CurrentExperience >= ExperienceToLevelUp;
        }

        public void SetLevelInit(ExperienceData experienceData)
        {
            for(int i = 0; i < experienceData.CurrentLevel - 1; i++)
            {
                OnLevelUp?.Invoke();
            }

            CurrentLevel = experienceData.CurrentLevel;
            CurrentExperience = experienceData.CurrentExperience;
            ExperienceToLevelUp = experienceData.ExperienceToLevelUp;
        }
    }


    [System.Serializable]
    public struct ExperienceData
    {
        public int CurrentLevel;
        public int CurrentExperience;
        public int ExperienceToLevelUp;
    }
}
