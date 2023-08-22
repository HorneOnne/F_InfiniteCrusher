using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace InfiniteCrusher
{
    public class UIUpgradeSlot : CustomCanvas
    {
        [Header("Buttons")]
        public Button UpgradeBtn;

        [Header("Texts")]
        public TextMeshProUGUI LevelText;
        public TextMeshProUGUI CostText; 
    }
}
