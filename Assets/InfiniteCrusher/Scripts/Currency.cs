using UnityEngine;
using System.Numerics;

namespace InfiniteCrusher
{
    public class Currency : MonoBehaviour
    {
        public static Currency Instance { get; private set; }
        public static event System.Action OnBalanceChanged;
       
        public BigInteger CurrentBalance { get; set; } = 0;

        private void Awake()
        {
            Instance = this;    
        }

        private void Start ()
        {
       
        }


        public void Deposite(BigInteger value)
        {
            CurrentBalance += value;
            OnBalanceChanged?.Invoke();
        }

        public void Withdraw(BigInteger value)
        {
            CurrentBalance -= value;
            OnBalanceChanged?.Invoke();
        }

        public string GetCurrencyString(BigInteger veryLargeNumber)
        {
            if (veryLargeNumber >= new BigInteger(1000000000000000000))
            {
                return (veryLargeNumber / new BigInteger(1000000000000000000)).ToString("0.#") + "ab";
            }
            if (veryLargeNumber >= new BigInteger(1000000000000000))
            {
                return (veryLargeNumber / new BigInteger(1000000000000000)).ToString("0.#") + "aa";
            }
            if (veryLargeNumber >= new BigInteger(1000000000000))
            {
                return (veryLargeNumber / new BigInteger(1000000000000)).ToString("0.#") + "T";
            }
            if (veryLargeNumber >= 1000000000)
            {
                return (veryLargeNumber / 1000000000).ToString("0.#") + "B";
            }
            if (veryLargeNumber >= 1000000)
            {
                return (veryLargeNumber / 1000000).ToString("0.#") + "M";
            }
            if (veryLargeNumber >= 1000)
            {
                return (veryLargeNumber / 1000).ToString("0.#") + "K";
            }

            return veryLargeNumber.ToString();
        }
    }

}
