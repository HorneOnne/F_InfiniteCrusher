using UnityEngine;

namespace InfiniteCrusher
{
    public class SaveManager : MonoBehaviour
    {
        public static event System.Action OnLoadDataFinished;
        public static SaveManager Instance { get; private set; }

        [Space(20)]
        private SaveGameData _saveGameData;


        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        private void Start()
        {
            Load();
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
            if(gameData.Equals(default(GameData)))
            {
                return;
            }


            Currency.Instance.CurrentBalance = gameData.Balance.ToBigInteger();
            ExperienceSystem.Instance.SetLevelInit(gameData.ExperienceData);
            GameLogicHandler.Instance.SpeedUpgrade.LoadLevel(gameData.UpgradeSpeedLevel);
            GameLogicHandler.Instance.TeethUpgrade.LoadLevel(gameData.UpgradeTeethLevel);
            GameLogicHandler.Instance.ToothSizeUpgrade.LoadLevel(gameData.UpgradeToothSizeLevel);

            OnLoadDataFinished?.Invoke();
        }



        private void OnApplicationQuit()
        {
            Save();
        }
    }
}
