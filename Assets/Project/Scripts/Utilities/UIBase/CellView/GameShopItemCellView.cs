using Sportsverse.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Sportsverse
{
    public class GameShopItemCellView : MonoBehaviour
    {
        public delegate void OnAddTicketDelegate(GameShopItemCellView cellView, GameTypes gameType);
        public static event OnAddTicketDelegate onAddTicketEvent;
        public delegate void OnRemoveTicketDelegate(GameShopItemCellView cellView, GameTypes gameType);
        public static event OnRemoveTicketDelegate onRemoveTicketEvent;

        public GameObject addButton, removeButton;
        public int ticketCount;
        public TextMeshProUGUI gameNameText;
        public TextMeshProUGUI ticketsCountText;
        private GameData priceData;
        public void SetData(GameData priceData)
        {
            this.priceData = priceData;
            ticketCount = 0;
            gameNameText.text = priceData.gameName;
            ticketsCountText.text = ticketCount.ToString();
        }
        public void SetData(string gameName, int ticketsCount)
        {
            ticketCount = ticketsCount;
            gameNameText.text = gameName;
            ticketsCountText.text = ticketCount.ToString();
            addButton.SetActive(false);
            removeButton.SetActive(false);
        }

        public void OnAddTicketClicked()
        {
            onAddTicketEvent.Invoke(this, priceData.gameType);
        }

        public void OnRemoveTicketClicked()
        {
            if (ticketCount > 0)
            {
                onRemoveTicketEvent.Invoke(this, priceData.gameType);
            }
        }

        public void AddTicket()
        {
            ticketCount++;
            ticketsCountText.text = ticketCount.ToString();
        }
        public void RemoveTicket()
        {
            ticketCount--;
            ticketsCountText.text = ticketCount.ToString();
        }

        public void BuyTickets(PlayerInventoryData inventoryData)
        {
            if (ticketCount > 0)
            {
                inventoryData.BuyTicket(priceData.gameType, ticketCount);
            }
        }
    }
}