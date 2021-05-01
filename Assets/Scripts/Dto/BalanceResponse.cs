using UnityEngine;

namespace Dto
{
    [System.Serializable]
    public class BalanceResponse
    {
        public int balance;

        public static BalanceResponse FromJson(string json)
        {
            return JsonUtility.FromJson<BalanceResponse>(json);
        }
    }
}