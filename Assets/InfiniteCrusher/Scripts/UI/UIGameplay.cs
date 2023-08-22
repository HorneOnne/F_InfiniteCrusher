using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace InfiniteCrusher
{
    public class UIGameplay : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _levelUpBtn;


        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _balanceText;

        [Header("Others")]
        [SerializeField] private UIUpgradeSlot _upgradeSpeed;
        [SerializeField] private UIUpgradeSlot _upgradeTeeth;
        [SerializeField] private UIUpgradeSlot _upgradeToothSize;

        private void OnEnable()
        {
            Currency.OnBalanceChanged += LoadBalance;

            SpeedUpgrade.OnUpgraded += LoadUpgardeSpeedUI;
            TeethUpgrade.OnUpgraded += LoadUpgardTeethUI;
            ToothSizeUpgrade.OnUpgraded += LoadUpgardToothSizeUI;

        }

        private void OnDisable()
        {
            Currency.OnBalanceChanged -= LoadBalance;

            SpeedUpgrade.OnUpgraded -= LoadUpgardeSpeedUI;
            TeethUpgrade.OnUpgraded -= LoadUpgardTeethUI;
            ToothSizeUpgrade.OnUpgraded -= LoadUpgardToothSizeUI;
        }

        private void Start()
        {
            LoadBalance();
            LoadUpgardeSpeedUI();
            LoadUpgardTeethUI();
            LoadUpgardToothSizeUI();

            _levelUpBtn.onClick.AddListener(() =>
            {
              
            });


            _upgradeSpeed.UpgradeBtn.onClick.AddListener(() =>
            {
                GameLogicHandler.Instance.UpgradeSpeed();
            });

            _upgradeTeeth.UpgradeBtn.onClick.AddListener(() =>
            {
                GameLogicHandler.Instance.UpgradeTeeth();
            });

            _upgradeToothSize.UpgradeBtn.onClick.AddListener(() =>
            {
                GameLogicHandler.Instance.UpgradeToothSize();
            });
        }

        private void OnDestroy()
        {
            _levelUpBtn.onClick.RemoveAllListeners();

            _upgradeSpeed.UpgradeBtn.onClick.RemoveAllListeners();
            _upgradeTeeth.UpgradeBtn.onClick.RemoveAllListeners();
            _upgradeToothSize.UpgradeBtn.onClick.RemoveAllListeners();
        }

        private void LoadBalance()
        {
            string balanceString = Currency.Instance.GetCurrencyString(Currency.Instance.CurrentBalance);
            _balanceText.text = $"${balanceString}";
        }


        private void LoadUpgardeSpeedUI()
        {
            _upgradeSpeed.LevelText.text = GameLogicHandler.Instance.SpeedUpgrade.CurrentLevel.ToString();

            string costString = Currency.Instance.GetCurrencyString(GameLogicHandler.Instance.SpeedUpgrade.CurrentUpgradeCost);
            _upgradeSpeed.CostText.text = costString;
        }

        private void LoadUpgardTeethUI()
        {
            _upgradeTeeth.LevelText.text = GameLogicHandler.Instance.TeethUpgrade.CurrentLevel.ToString();

            string costString = Currency.Instance.GetCurrencyString(GameLogicHandler.Instance.TeethUpgrade.CurrentUpgradeCost);
            _upgradeTeeth.CostText.text = costString;
        }

        private void LoadUpgardToothSizeUI()
        {
            _upgradeToothSize.LevelText.text = GameLogicHandler.Instance.ToothSizeUpgrade.CurrenLevel.ToString();

            string costString = Currency.Instance.GetCurrencyString(GameLogicHandler.Instance.ToothSizeUpgrade.CurrentUpgradeCost);
            _upgradeToothSize.CostText.text = costString;
        }
    }
}
