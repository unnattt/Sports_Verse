using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sportsverse.Data
{
    [CreateAssetMenu(menuName = "Sportsverse/ComplexPricingData", fileName = "PricingData")]
    public class ComplexGamesData : ScriptableObject
    {
        public List<GameData> pricingData;

        public int GetGameTicketPrice(GameTypes gameType)
        {
            return pricingData.Find(x=>x.gameType == gameType).ticketPrice;
        }
    }
    [System.Serializable]
    public class GameData
    {
        public GameTypes gameType;
        public string gameName;
        public int ticketPrice;
    }
}