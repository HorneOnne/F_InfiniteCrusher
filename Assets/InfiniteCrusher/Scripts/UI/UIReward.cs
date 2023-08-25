using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace InfiniteCrusher
{
    public class UIReward : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _claimBtn;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _rewardText;


        private void OnEnable()
        {
            ExperienceSystem.OnLevelUp += LoadRewardText;
        }

        private void OnDisable()
        {
            ExperienceSystem.OnLevelUp -= LoadRewardText;
        }

        private void Start()
        {
            LoadRewardText();

            _claimBtn.onClick.AddListener(() =>
            {
                UIGameplayManager.Instance.DisplayRewardMenu(false);
                Currency.Instance.Deposite(RewardSystem.Instance.CurrentReward);
                //RewardSystem.Instance.CreateNewReward();           
                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });
        }

        private void OnDestroy()
        {
            _claimBtn.onClick.RemoveAllListeners();
        }

        private void LoadRewardText()
        {
            string rewardText = Currency.Instance.GetCurrencyString(RewardSystem.Instance.CurrentReward);
            _rewardText.text = $"${rewardText}";
        }
    }
}
