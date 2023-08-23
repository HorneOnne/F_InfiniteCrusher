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

   

        public void CreateNewReward()
        {
            CurrentReward = CurrentReward * _rewardMultiplier;
            CanClaim = false;
        }

    }
}
