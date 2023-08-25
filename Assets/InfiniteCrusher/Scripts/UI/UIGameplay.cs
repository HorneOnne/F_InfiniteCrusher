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

        [Header("Sliders")]
        [SerializeField] private Slider _expSlider;


        [Header("Sprites")]
        [SerializeField] private Sprite _normalLevelBtnSprite;
        [SerializeField] private Sprite _levelUpLevelBtnSprite;



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

            // Exp
            ExperienceSystem.OnLevelUp += UpdateExpSliderValueWhenLevelUp;
            ExperienceSystem.OnExperienceGain += UpdateExpSlider;


            SaveManager.OnLoadDataFinished += () =>
            {
                LoadBalance();
                LoadUpgardeSpeedUI();
                LoadUpgardTeethUI();
                LoadUpgardToothSizeUI();
                LoadUpgardToothSizeUI();

                UpdateExpSliderValueWhenLevelUp();
                UpdateExpSlider();
            };

        }

        private void OnDisable()
        {
            Currency.OnBalanceChanged -= LoadBalance;

            SpeedUpgrade.OnUpgraded -= LoadUpgardeSpeedUI;
            TeethUpgrade.OnUpgraded -= LoadUpgardTeethUI;
            ToothSizeUpgrade.OnUpgraded -= LoadUpgardToothSizeUI;

            // Exp
            ExperienceSystem.OnLevelUp -= UpdateExpSliderValueWhenLevelUp;
            ExperienceSystem.OnExperienceGain -= UpdateExpSlider;

            SaveManager.OnLoadDataFinished -= () =>
            {
                LoadBalance();
                LoadUpgardeSpeedUI();
                LoadUpgardTeethUI();
                LoadUpgardToothSizeUI();
                LoadUpgardToothSizeUI();

                UpdateExpSliderValueWhenLevelUp();
                UpdateExpSlider();
            };
        }

        private void Start()
        {
            LoadBalance();
            LoadUpgardeSpeedUI();
            LoadUpgardTeethUI();
            LoadUpgardToothSizeUI();

            UpdateExpSliderValueWhenLevelUp();
            UpdateExpSlider();

            _levelUpBtn.onClick.AddListener(() =>
            {
                if (ExperienceSystem.Instance.CanLevelUp() && RewardSystem.Instance.CanClaim == false)
                {
                    RewardSystem.Instance.CanClaim = true;

                    _levelUpBtn.GetComponent<Image>().sprite = _normalLevelBtnSprite;
                    ExperienceSystem.Instance.LockExpericenGain = false;
                    ExperienceSystem.Instance.LevelUp();

                    UIGameplayManager.Instance.DisplayRewardMenu(true);
                    SoundManager.Instance.PlaySound(SoundType.ScoreUp, false);
                }
            });


            _upgradeSpeed.UpgradeBtn.onClick.AddListener(() =>
            {
                bool canUpgrade = GameLogicHandler.Instance.UpgradeSpeed();

                if (canUpgrade)
                    SoundManager.Instance.PlaySound(SoundType.Button, false);
            });

            _upgradeTeeth.UpgradeBtn.onClick.AddListener(() =>
            {
                bool canUpgrade = GameLogicHandler.Instance.UpgradeTeeth();
                if(canUpgrade)
                    SoundManager.Instance.PlaySound(SoundType.Button, false);
            });

            _upgradeToothSize.UpgradeBtn.onClick.AddListener(() =>
            {
                bool canUpgrade = GameLogicHandler.Instance.UpgradeToothSize();

                if (canUpgrade)
                    SoundManager.Instance.PlaySound(SoundType.Button, false);
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
            _upgradeToothSize.LevelText.text = GameLogicHandler.Instance.ToothSizeUpgrade.CurrentLevel.ToString();

            string costString = Currency.Instance.GetCurrencyString(GameLogicHandler.Instance.ToothSizeUpgrade.CurrentUpgradeCost);
            _upgradeToothSize.CostText.text = costString;
        }


        private void UpdateExpSliderValueWhenLevelUp()
        {
            _expSlider.minValue = 0;
            _expSlider.maxValue = ExperienceSystem.Instance.ExperienceToLevelUp;
            _expSlider.value = ExperienceSystem.Instance.CurrentExperience;

            _levelText.text = $"{ExperienceSystem.Instance.CurrentLevel} LVL";
        }
        private void UpdateExpSlider()
        {
            if (ExperienceSystem.Instance.CurrentExperience > _expSlider.maxValue)
                _expSlider.value = _expSlider.maxValue;
            else

                _expSlider.value = ExperienceSystem.Instance.CurrentExperience;

            if (ExperienceSystem.Instance.CurrentExperience >= ExperienceSystem.Instance.ExperienceToLevelUp)
            {
                _levelUpBtn.GetComponent<Image>().sprite = _levelUpLevelBtnSprite;
            }
        }
    }
}
