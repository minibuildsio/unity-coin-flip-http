using UnityEngine;

namespace Dto
{
    [System.Serializable]
    public class PlayResponse
    {
        public int balance;
        public string result;
        public int payout;

        public static PlayResponse FromJson(string json)
        {
            return JsonUtility.FromJson<PlayResponse>(json);
        }
    }
}