using UnityEngine;


namespace InfiniteCrusher
{
    public class BlockSpawner : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private Block _block01Prefab;
        [SerializeField] private Block _block02Prefab;
        [SerializeField] private Block _block03Prefab;

        [Header("References")]
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private float _spawnOffsetX;



        private void OnEnable()
        {
            Block.OnBlockDestroyed += OnBlockDestroyed;
        }

        private void OnDisable()
        {
            Block.OnBlockDestroyed -= OnBlockDestroyed;
        }


        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                SpawnRandom(_block02Prefab);
            }
        }

        private void SpawnRandom(Block block)
        {
            Vector2 randomPosition = _spawnPosition.position + new Vector3(Random.Range(-_spawnOffsetX, _spawnOffsetX), 0, 0);
            //Instantiate(block, randomPosition, Quaternion.identity);
            GameObject blockLevel02 = ObjectPooler.SharedInstance.GetPooledObject("BlockLevel_02");
            if(blockLevel02 != null)
            {
                blockLevel02.SetActive(true);
                blockLevel02.transform.position = randomPosition;
            }
        }

        private void Spawn(Block block, Vector2 position)
        {
            Instantiate(block, position, Quaternion.identity);
        }

        private void OnBlockDestroyed(int blockLevel, Vector2 position)
        {
            GameObject blockLevel01 = ObjectPooler.SharedInstance.GetPooledObject("BlockLevel_01");
            if (blockLevel01 != null)
            {
                blockLevel01.SetActive(true);
                blockLevel01.transform.position = position;
            }
        }
    }

}
