using UnityEngine;


namespace InfiniteCrusher
{
    [CreateAssetMenu(fileName = "UpgradeData", menuName = "InfiniteCrusher/UpgradeData")]
    public class UpgradeData : ScriptableObject
    {
        public int StartLevel;
        public int StartCost;
    }

}
