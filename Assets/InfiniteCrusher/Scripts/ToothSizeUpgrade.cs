using UnityEngine;
using System.Numerics;

namespace InfiniteCrusher
{
    public class ToothSizeUpgrade : MonoBehaviour
    {
        public static event System.Action OnUpgraded;

        [Header("Data")]
        [SerializeField] private UpgradeData _baseToothSizeUpgradeData;

        public int CurrentLevel { get; private set; }
        public float CurrentSize { get; private set; }
        public BigInteger CurrentUpgradeCost { get; private set; }


        private const float MIN_SIZE = 0.7f;
        private const float MAX_SIZE = 1.7f;

        private void Awake()
        {
            CurrentLevel = _baseToothSizeUpgradeData.StartLevel;
            CurrentUpgradeCost = _baseToothSizeUpgradeData.StartCost;

            CurrentSize = MIN_SIZE;
        }

        public void LevelUp()
        {
            CurrentLevel++;
            CurrentUpgradeCost *= 2;

            if (CurrentSize < MAX_SIZE)
                CurrentSize += 0.01f;
            else
                CurrentSize = MAX_SIZE;

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
