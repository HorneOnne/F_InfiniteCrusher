using UnityEngine;
using System.Numerics;
using TMPro.EditorUtilities;

namespace InfiniteCrusher
{
    public class SpeedUpgrade : MonoBehaviour
    {
        public static event System.Action OnUpgraded;

        [Header("Data")]
        [SerializeField] private UpgradeData _baseUpgradeSpeedData;

        public int CurrentLevel { get; private set; }
        public float CurrentSpeed { get; private set; }
        public float MaxAngularVelocity { get; private set; }
        public BigInteger CurrentUpgradeCost { get; private set; }


        private const int START_SPEED = 20;
        private const int MAX_SPEED = 100;
        private const int START_ANGULAR_VELOCITY = 300;
        private const int MAX_ANGULAR_VELOCITY = 700;

        private void Awake()
        {
            CurrentLevel = _baseUpgradeSpeedData.StartLevel;
            CurrentUpgradeCost = _baseUpgradeSpeedData.StartCost;

            CurrentSpeed = START_SPEED;
            MaxAngularVelocity = START_ANGULAR_VELOCITY;
        }

        public void LevelUp()
        {
            CurrentLevel++;
            CurrentUpgradeCost *= 2;

            if (CurrentSpeed < MAX_SPEED)
                CurrentSpeed += 2.6f;
            else
                CurrentSpeed = MAX_SPEED;

            if (MaxAngularVelocity < MAX_ANGULAR_VELOCITY)
                MaxAngularVelocity += 13f;
            else
                MaxAngularVelocity = MAX_ANGULAR_VELOCITY;
           
            OnUpgraded?.Invoke();
        }

        public void LoadLevel(int level)
        {
            for(int i =0; i < level-1; i++)
            {
                LevelUp();
            }
        }
    }

}
