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
        [field: SerializeField] public int ExperienceToLevelUp { get;  set; } = 10; // Initial experience required to level up
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
    }
}
