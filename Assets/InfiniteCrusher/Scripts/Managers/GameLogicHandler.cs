using UnityEngine;

namespace InfiniteCrusher
{
    public class GameLogicHandler : MonoBehaviour
    {
        public static GameLogicHandler Instance { get; private set; }

        [SerializeField] private SpeedUpgrade _speedUpgrade;
        [SerializeField] private TeethUpgrade _teethUpgrade;
        [SerializeField] private ToothSizeUpgrade _toothSizeUpgrade;


        private Currency _currency;


        #region Properties
        public SpeedUpgrade SpeedUpgrade { get => _speedUpgrade; }
        public TeethUpgrade TeethUpgrade { get => _teethUpgrade; }
        public ToothSizeUpgrade ToothSizeUpgrade { get => _toothSizeUpgrade; }
        #endregion


        private void Awake()
        {         
            Instance = this;
        }

        private void Start()
        {
            _currency = Currency.Instance;
        }

        public bool UpgradeSpeed()
        {
            if(_currency.CurrentBalance >= _speedUpgrade.CurrentUpgradeCost)
            {              
                _currency.Withdraw(_speedUpgrade.CurrentUpgradeCost);
                _speedUpgrade.LevelUp();
                return true;
            }
            return false;
        }

        public bool UpgradeTeeth()
        {
            if (_currency.CurrentBalance >= _teethUpgrade.CurrentUpgradeCost)
            {             
                _currency.Withdraw(_teethUpgrade.CurrentUpgradeCost);
                _teethUpgrade.LevelUp();
                return true;
            }
            return false;
        }

        public bool UpgradeToothSize()
        {
            if (_currency.CurrentBalance >= _toothSizeUpgrade.CurrentUpgradeCost)
            {             
                _currency.Withdraw(_toothSizeUpgrade.CurrentUpgradeCost);
                _toothSizeUpgrade.LevelUp();
                return true;
            }
            return false;
        }



    }
}
