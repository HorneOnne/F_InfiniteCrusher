using UnityEngine;


namespace InfiniteCrusher
{
    [CreateAssetMenu(fileName = "BlockData", menuName = "InfiniteCrusher/BlockData")]
    public class BlockData : ScriptableObject
    {       
        public int BlockLevel;
        public int Health;
    }

}
