using System.Linq.Expressions;
using System.Threading;
using UnityEngine;

namespace InfiniteCrusher
{
    public class SaveManager : MonoBehaviour
    {
        public static event System.Action OnLoadDataFinished;
        public static SaveManager Instance { get; private set; }

        [Space(20)]
        private SaveGameData _saveGameData;

        private float _timer;
        private bool _isloaded = false;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        private void Start()
        {
            Load();
            _isloaded = true;
        }

        public void Save()
        {
            GameData gameData = new GameData()
            {
                Balance = new SerializableBigInt(Currency.Instance.CurrentBalance),
                ExperienceData = new ExperienceData()
                {
                    CurrentLevel = ExperienceSystem.Instance.CurrentLevel,
                    CurrentExperience = ExperienceSystem.Instance.CurrentExperience,
                    ExperienceToLevelUp = ExperienceSystem.Instance.ExperienceToLevelUp,
                },
                UpgradeSpeedLevel = GameLogicHandler.Instance.SpeedUpgrade.CurrentLevel,
                UpgradeTeethLevel = GameLogicHandler.Instance.TeethUpgrade.CurrentLevel,
                UpgradeToothSizeLevel = GameLogicHandler.Instance.ToothSizeUpgrade.CurrentLevel,
            };

            _saveGameData = new SaveGameData(gameData);
            _saveGameData.SaveAllData();
        }

        public void Load()
        {
            GameData gameData = new GameData();
            _saveGameData = new SaveGameData(gameData);
            _saveGameData.LoadAllData();

         
            gameData = _saveGameData.GetSaveData();
            try
            {
                Currency.Instance.CurrentBalance = gameData.Balance.ToBigInteger();
                ExperienceSystem.Instance.SetLevelInit(gameData.ExperienceData);
                GameLogicHandler.Instance.SpeedUpgrade.LoadLevel(gameData.UpgradeSpeedLevel);
                GameLogicHandler.Instance.TeethUpgrade.LoadLevel(gameData.UpgradeTeethLevel);
                GameLogicHandler.Instance.ToothSizeUpgrade.LoadLevel(gameData.UpgradeToothSizeLevel);

                OnLoadDataFinished?.Invoke();
            }
            catch
            {
                Debug.Log("No data");
            }
        }

        //private void Update()
        //{
        //    if (_isloaded == false) return;
        //    if(Time.time - _timer > 0.5f)
        //    {
        //        _timer = Time.time;
        //        Save();
        //    }
        //}

        private void OnApplicationQuit()
        {
            Save();
        }

        private void OnApplicationPause(bool pause)
        {
            if (_isloaded == false) return;
            Save();
        }
    }
}
