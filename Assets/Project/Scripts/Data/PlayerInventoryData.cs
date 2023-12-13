using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sportsverse.Data
{
    [CreateAssetMenu(menuName = "Sportsverse/PlayerInventoryData", fileName = "PlayerInventoryData")]
    public class PlayerInventoryData : ScriptableObject
    {
        public int startingCoins;
        public int gameCoins;
        public ComplexGamesData gamesData;
        public Dictionary<GameTypes, int> gameTickets = new Dictionary<GameTypes, int>();

        public void ClearData()
        {
            gameCoins = startingCoins;
            gameTickets.Clear();
        }

        #region COINS_CALCULATION
        public void AddCoins(int amount)
        {
            gameCoins += amount;
        }
        public void RemoveCoins(int amount)
        {
            gameCoins -= amount;
        }
        #endregion

        #region TICKETS_CALCULATION
        public void BuyTicket(GameTypes gameType, int quantity = 1)
        {
            int coinsUsed = gamesData.GetGameTicketPrice(gameType) * quantity;
            
            if (coinsUsed > gameCoins) return;

            RemoveCoins(coinsUsed);
            AddTicket(gameType, quantity);
        }
        public void AddTicket(GameTypes gameType, int quantity = 1)
        {
            if (gameTickets.ContainsKey(gameType))
            {
                gameTickets[gameType] += quantity;
            }
            else
            {
                gameTickets.Add(gameType, quantity);
            }
        }
        public void UseTicket(GameTypes gameType, int quantity = 1)
        {
            if (gameTickets.ContainsKey(gameType) && gameTickets[gameType] > 0)
            {
                gameTickets[gameType] -= quantity;
                if (gameTickets[gameType] == 0)
                {
                    gameTickets.Remove(gameType);
                }
            }
        }

        public bool HasTicket(GameTypes gameType)
        {
            return gameTickets.ContainsKey(gameType);
        }
        public int GetTicketsCount(GameTypes gameType)
        {
            return gameTickets[gameType];
        }
        #endregion
    }
}