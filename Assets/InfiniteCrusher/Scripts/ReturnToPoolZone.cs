using UnityEngine;


namespace InfiniteCrusher
{
    public class ReturnToPoolZone : MonoBehaviour
    {
        public LayerMask _blockLayer;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_blockLayer == (_blockLayer | (1 << collision.gameObject.layer)))
            {
                collision.gameObject.SetActive(false);

                Currency.Instance.Deposite(10);
            }
        }
    }
}
