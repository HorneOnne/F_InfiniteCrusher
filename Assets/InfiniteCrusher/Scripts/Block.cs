﻿using UnityEngine;


namespace InfiniteCrusher
{

    public class Block : MonoBehaviour
    {
        public LayerMask _teethLayer;
        public bool IsUnbreakable;
        [SerializeField] private BlockData _blockData;
        [SerializeField] private int _currentHealth;


        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        private void Start()
        {
            _currentHealth = _blockData.Health;
        }

        private void OnEnable()
        {
            _currentHealth = _blockData.Health;
        }

        public void TakeDamage(int damage = 1)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                int blockSpawnLevel = _blockData.BlockLevel - 1;
                BlockSpawner.Instance.SpawnBlock(blockSpawnLevel, transform.position);
                BlockSpawner.Instance.SpawnBlock(blockSpawnLevel, transform.position);

                BlockSpawner.Instance.RemoveBlockCount();
                gameObject.SetActive(false);
            }
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_teethLayer == (_teethLayer | (1 << collision.gameObject.layer)))
            {

                if (_rb.position.x < -0.2f)
                {
                    _rb.AddForce(new Vector3(1, 0.3f, 0) * (3f -_rb.position.x), ForceMode2D.Impulse);
                }
                else if (_rb.position.x > 0.2f)
                {
                    _rb.AddForce(new Vector3(-1, 0.3f, 0) * (3f + _rb.position.x), ForceMode2D.Impulse);
                }
                else
                {

                }

                if (IsUnbreakable == false)
                {
                    TakeDamage();
                };
            }
        }
    }

}
