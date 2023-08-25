using UnityEngine;
using System.Numerics;

namespace InfiniteCrusher
{
    public class RewardSystem : MonoBehaviour
    {
        public static RewardSystem Instance { get; private set; }

        private BigInteger _baseReward = 5000;
        public BigInteger CurrentReward { get; private set; }
        public bool CanClaim { get; set; } = false;
        private int _rewardMultiplier = 2;

        private void Awake()
        {
            Instance = this;
            CurrentReward = _baseReward;
        }


        private void OnEnable()
        {
            ExperienceSystem.OnLevelUp += () =>
            {
                GetRewardByLevel(ExperienceSystem.Instance.CurrentLevel);
            };
        }

        private void OnDisable()
        {
            ExperienceSystem.OnLevelUp -= () =>
            {
                GetRewardByLevel(ExperienceSystem.Instance.CurrentLevel);
            };
        }

        private void Start()
        {
            GetRewardByLevel(ExperienceSystem.Instance.CurrentLevel);
        }

        public void CreateNewReward()
        {
            CurrentReward = CurrentReward * _rewardMultiplier;
            CanClaim = false;
        }

        public void GetRewardByLevel(int level)
        {
            CurrentReward = _baseReward;
            for (int i = 0; i < level - 1; i++)
            {
                CurrentReward = CurrentReward * _rewardMultiplier;
            }
            CanClaim = false;
        }

    }
}
