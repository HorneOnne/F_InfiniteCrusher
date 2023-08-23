using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InfiniteCrusher
{
    public class TeethGenerator : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _centerPoint;
        [SerializeField] private Transform _teethParent;
        [SerializeField] private GameObject _teethPrefab;

        [Header("Properties")]
        [SerializeField] private float _circleRadius = 1;
        [SerializeField] private float _teethRotationOffset = -25f;
        private float _teethSize = 1.0f;

        private List<GameObject> teethList = new List<GameObject>();

        private void OnEnable()
        {
            TeethUpgrade.OnUpgraded += OnUpgradeTeeth;

            ToothSizeUpgrade.OnUpgraded += OnUpgradeToothSize;
        }

        private void OnDisable()
        {
            TeethUpgrade.OnUpgraded -= OnUpgradeTeeth;

            ToothSizeUpgrade.OnUpgraded -= OnUpgradeToothSize;
        }

        private void Start()
        {
            _teethSize = GameLogicHandler.Instance.ToothSizeUpgrade.CurrentSize;
            var _numOfTeeth = GameLogicHandler.Instance.TeethUpgrade.CurrentTeethCount;       
            GenerateTeeth(_numOfTeeth);
        }



        public void GenerateTeeth(int numOfTeeth)
        {
 
            for (int i = 0; i < numOfTeeth; i++)
            {
                float angle = i * (360f / numOfTeeth);
                float radians = angle * Mathf.Deg2Rad;

                Vector2 spawnPosition = (Vector2)_centerPoint.position + new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * _circleRadius;
                GameObject teethObject = Instantiate(_teethPrefab, spawnPosition, Quaternion.identity, _teethParent.transform);
                teethObject.transform.localScale *= _teethSize;
                Vector2 directionToCenter = (Vector2)_centerPoint.position - spawnPosition;
                float rotationAngle = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg;
                rotationAngle += _teethRotationOffset;
                teethObject.transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);

                teethList.Add(teethObject);
            }
        }


        private void RemoveOldTeeth()
        {
            foreach(var teeth in teethList)
            {
                Destroy(teeth.gameObject);
            }
            teethList.Clear();
        }


        private void OnUpgradeTeeth()
        {
            RemoveOldTeeth();
            GenerateTeeth(GameLogicHandler.Instance.TeethUpgrade.CurrentTeethCount);
        }

        private void OnUpgradeToothSize()
        {          
            _teethSize = GameLogicHandler.Instance.ToothSizeUpgrade.CurrentSize;
            RemoveOldTeeth();
            GenerateTeeth(GameLogicHandler.Instance.TeethUpgrade.CurrentTeethCount);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _circleRadius);
        }
    }

}
