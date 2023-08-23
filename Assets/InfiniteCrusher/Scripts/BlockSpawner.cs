using UnityEngine;


namespace InfiniteCrusher
{
    public class BlockSpawner : MonoBehaviour
    {
        public static BlockSpawner Instance { get; private set; }

  
        [Header("References")]
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private float _spawnOffsetX;


        private float _spawnTimer = 0.0f;
        private float _spawnTime = 0.2f;

        private int _minBlockCount = 50;
        private int _maxBlockCount = 100;
        [SerializeField] private int _currentCountSize;

        public int CurrentBlockCount = 0;


        private void Awake()
        {
            Instance = this;
        }


        private void OnEnable()
        {
            ExperienceSystem.OnLevelUp += IncreaseCurrentCountSize;
        }

        private void OnDisable()
        {
            ExperienceSystem.OnLevelUp -= IncreaseCurrentCountSize;
        }

        private void Start()
        {
            _currentCountSize = _minBlockCount;

            for (int i = 0; i < 30; i++)
            {
                Vector2 randomPosition = _spawnPosition.position + new Vector3(Random.Range(-_spawnOffsetX, _spawnOffsetX), 0, 0);
                float rate = Random.Range(0f, 1f);
                if (rate < 0.15f + (0.01f * ExperienceSystem.Instance.CurrentLevel))
                {
                    SpawnBlock(3, randomPosition);
                }
                else
                {
                    SpawnBlock(2, randomPosition);
                }
            }
        }

        private void Update()
        {
            if(Time.time - _spawnTimer > _spawnTime)
            {
                _spawnTimer = Time.time;

                if (CurrentBlockCount < _currentCountSize)
                {
                    Vector2 randomPosition = _spawnPosition.position + new Vector3(Random.Range(-_spawnOffsetX, _spawnOffsetX), 0, 0);
                    float rate = Random.Range(0f, 1f);
                    if (rate < 0.15f + (0.01f * ExperienceSystem.Instance.CurrentLevel))
                    {
                        SpawnBlock(3, randomPosition);
                    }
                    else
                    {
                        SpawnBlock(2, randomPosition);
                    }
                }
            }    
        }

        public void SpawnBlock(int blockLevel, Vector2 position)
        {
            GameObject block;
            

            if (blockLevel == 1)
                block = ObjectPooler.SharedInstance.GetPooledObject("BlockLevel_01");
            else if (blockLevel == 2)
                block = ObjectPooler.SharedInstance.GetPooledObject("BlockLevel_02");
            else if (blockLevel == 3)
                block = ObjectPooler.SharedInstance.GetPooledObject("BlockLevel_03");
            else
                block = ObjectPooler.SharedInstance.GetPooledObject("BlockLevel_01");


            if (block != null)
            {
                block.SetActive(true);
                block.transform.position = position;

                AddBlockCount();
            }         


            
        }

        public void AddBlockCount()
        {
            CurrentBlockCount += 1;
        }

        public void RemoveBlockCount()
        {
            CurrentBlockCount -= 1;
            if (CurrentBlockCount < 0)
                CurrentBlockCount = 0;
        }

        private void IncreaseCurrentCountSize()
        {
            if (_currentCountSize < _maxBlockCount)
            {
                _currentCountSize++;
            }
            else
            {
                _currentCountSize = _maxBlockCount;
            }
        }
    }

}
