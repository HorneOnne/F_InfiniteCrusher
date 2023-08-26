using System.Collections;
using UnityEngine;
using UnityEngine.UI;


namespace InfiniteCrusher
{
    public class Crusher : MonoBehaviour
    {
        private Rigidbody2D _rb;
        [SerializeField] private float _torque = 0;
        private float _maxAngularVelocity = 0;

        public bool IsClockWise = true;

        [Header("UI")]
        [SerializeField] private Image _speedFillImage;


        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            SpeedUpgrade.OnUpgraded += UpdateSpeed;
        }
        private void OnDisable()
        {
            SpeedUpgrade.OnUpgraded += UpdateSpeed;
        }


        private void Start()
        {
            UpdateSpeed();
            _rb.AddTorque(_torque, ForceMode2D.Force);
        }

        private void FixedUpdate()
        {
            _rb.AddTorque(_torque, ForceMode2D.Force);

            if (_rb.angularVelocity > _maxAngularVelocity)
                _rb.angularVelocity = _maxAngularVelocity;

            if (_rb.angularVelocity < -_maxAngularVelocity)
                _rb.angularVelocity = -_maxAngularVelocity;
        }

        private void UpdateSpeed()
        {                       
            _torque = GameLogicHandler.Instance.SpeedUpgrade.CurrentSpeed;
            _maxAngularVelocity = GameLogicHandler.Instance.SpeedUpgrade.MaxAngularVelocity;

            if (IsClockWise)
                _torque = -_torque;

            LoadTorqueUI();
        }


        private void LoadTorqueUI()
        {
            float fillValue = MapValue(Mathf.Abs(_torque), 0, Mathf.Abs(_maxAngularVelocity), 0f, 1f);
            _speedFillImage.fillAmount = fillValue;
        }

        private float MapValue(float value, float minFrom, float maxFrom, float minTo, float maxTo)
        {
            return Mathf.Lerp(minTo, maxTo, Mathf.InverseLerp(minFrom, maxFrom, value));
        }

        private void OnMouseDown()
        {          
            UpdateSpeedTesting();
            StopAllCoroutines();
        }

        private void OnMouseUp()
        {
            StartCoroutine(PerformResetToSystemSpeed());
        }

        private void UpdateSpeedTesting()
        {          
            if (IsClockWise)
            {
                _torque -= 10;
                _maxAngularVelocity += 10;

                if (_maxAngularVelocity > GameLogicHandler.Instance.SpeedUpgrade.MaxAngularVelocity)
                    _maxAngularVelocity = GameLogicHandler.Instance.SpeedUpgrade.MaxAngularVelocity;
            }
            else
            {
                _torque += 10;
                _maxAngularVelocity += 10;

                if (_maxAngularVelocity > GameLogicHandler.Instance.SpeedUpgrade.MaxAngularVelocity)
                    _maxAngularVelocity = GameLogicHandler.Instance.SpeedUpgrade.MaxAngularVelocity;
            }
                

            LoadTorqueUI();
        }

        private IEnumerator PerformResetToSystemSpeed()
        {
            yield return new WaitForSeconds(1.0f);

            _torque = GameLogicHandler.Instance.SpeedUpgrade.CurrentSpeed;
            _maxAngularVelocity = GameLogicHandler.Instance.SpeedUpgrade.MaxAngularVelocity;

            if (IsClockWise)
                _torque = -_torque;

            LoadTorqueUI();

            Debug.Log("AA");
        }
    }

}
