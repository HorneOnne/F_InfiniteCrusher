﻿using UnityEngine;
using System.Numerics;

namespace InfiniteCrusher
{
    public class TeethUpgrade : MonoBehaviour
    {
        public static event System.Action OnUpgraded;

        [Header("Data")]
        [SerializeField] private UpgradeData _baseTeethUpgradeData;

        public int CurrentLevel { get; private set; }
        public int CurrentTeethCount { get; private set; }
        public BigInteger CurrentUpgradeCost { get; private set; }


        private const int MIN_TEETH_COUNT = 2;
        private const int MAX_TEETH_COUNT = 10;

        private void Awake()
        {
            CurrentLevel = _baseTeethUpgradeData.StartLevel;
            CurrentUpgradeCost = _baseTeethUpgradeData.StartCost;

            CurrentTeethCount = MIN_TEETH_COUNT;
        }

        public void LevelUp()
        {
            CurrentLevel++;
            CurrentUpgradeCost *= 2;

            if (CurrentTeethCount < MAX_TEETH_COUNT)
                CurrentTeethCount += 1;
            else
                CurrentTeethCount = MAX_TEETH_COUNT;

            OnUpgraded?.Invoke();
        }

        public void LoadLevel(int level)
        {
            for (int i = 0; i < level - 1; i++)
            {
                LevelUp();
            }
        }
    }
}
