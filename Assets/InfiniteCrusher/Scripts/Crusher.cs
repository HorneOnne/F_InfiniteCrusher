using UnityEngine;


namespace InfiniteCrusher
{
    public class Crusher : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private float _torque = 0;
        private float _maxAngularVelocity = 0;

        public bool IsClockWise = true;

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
        }
    }

}
