using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yudiz.BaseFramework;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using Sportsverse.Data;
using Sportsverse;

namespace Yudiz.UI
{
    public class ShopPopup : UIScreenView
    {
        [SerializeField] private PlayerInventoryData playerInventoryData;
        [SerializeField] private TextMeshProUGUI totalCoinsText;
        [SerializeField] private TextMeshProUGUI coinsRequiredText;
        [SerializeField] private GameShopItemCellView shopItemCellViewPrefab;
        [SerializeField] private Transform shopScrollContainer;
        [SerializeField] private Transform ticketsScrollContainer;
        [SerializeField] private GameObject shopView;
        [SerializeField] private GameObject ticketsView;

        private int coinsRequiredCount = 0;
        private List<GameShopItemCellView> shopItemCellViews;
        private List<GameShopItemCellView> ticketsItemCellViews;
        #region PUBLIC_METHODS
        public void OnBuyClicked()
        {
            foreach (var game in shopItemCellViews)
            {
                game.BuyTickets(playerInventoryData);
            }
            SetupShopView();
        }
        public void MoveScreen(Transform location)
        {
            transform.position = location.position;
            transform.rotation = location.rotation;
        }
        public void OnShopClicked()
        {
            SetupShopView();
        }
        public void OnTicketsClicked()
        {
            SetupTicketsView();
        }
        public void OnAddCoinsClicked()
        {
            
        }
        #endregion

        #region PRIVATE_METHODS
        private void ToggleShopView(bool showShop)
        {
            shopView.SetActive(showShop);
            ticketsView.SetActive(!showShop);
        }
        public void SetupShopView()
        {
            ToggleShopView(true);
            coinsRequiredCount = 0;
            totalCoinsText.text = playerInventoryData.gameCoins.ToString();
            coinsRequiredText.text = 0.ToString();

            if (shopItemCellViews == null || shopItemCellViews.Count == 0)
            {
                ClearChildren(shopScrollContainer);
                shopItemCellViews = new List<GameShopItemCellView>();
                foreach (var priceData in playerInventoryData.gamesData.pricingData)
                {
                    GameShopItemCellView cellView = Instantiate(shopItemCellViewPrefab, shopScrollContainer);
                    cellView.SetData(priceData);

                    shopItemCellViews.Add(cellView);
                }
            }
            else
            {
                for (int index = 0; index < playerInventoryData.gamesData.pricingData.Count; index++)
                {
                    shopItemCellViews[index].SetData(playerInventoryData.gamesData.pricingData[index]);
                }
            }
        }
        public void SetupTicketsView()
        {
            ToggleShopView(false);
            ClearChildren(ticketsScrollContainer);
            ticketsItemCellViews = new List<GameShopItemCellView>();
            foreach (var priceData in playerInventoryData.gamesData.pricingData)
            {
                if (playerInventoryData.HasTicket(priceData.gameType))
                {
                    GameShopItemCellView cellView = Instantiate(shopItemCellViewPrefab, ticketsScrollContainer);
                    cellView.SetData(priceData.gameName, playerInventoryData.GetTicketsCount(priceData.gameType));
                    ticketsItemCellViews.Add(cellView);
                }
            }
        }
        private void OnAddTicketRequired(GameShopItemCellView cellView, GameTypes gameType)
        {
            int price = playerInventoryData.gamesData.GetGameTicketPrice(gameType);
            if (price < (playerInventoryData.gameCoins - coinsRequiredCount))
            {
                cellView.AddTicket();
                coinsRequiredCount += price;
                coinsRequiredText.text = coinsRequiredCount.ToString();
            }
        }

        private void OnRemoveTicketRequired(GameShopItemCellView cellView, GameTypes gameType)
        {
            cellView.RemoveTicket();

            int price = playerInventoryData.gamesData.GetGameTicketPrice(gameType);
            coinsRequiredCount -= price;
            coinsRequiredText.text = coinsRequiredCount.ToString();
        }
        private void ClearChildren(Transform parent)
        {
            if (parent.childCount > 0)
            {
                foreach (Transform child in parent)
                {
                    Destroy(child.gameObject);
                }

            }
        }
        #endregion

        #region UI_CALLBACKS
        [ContextMenu("Show")]
        public override void OnScreenShowCalled()
        {
            SetupShopView();
            base.OnScreenShowCalled();
            //Sportsverse.GameManager.Instance.networkLocalPlayer.SetObjectInPlayerCameraForward(transform);
            GameShopItemCellView.onAddTicketEvent += OnAddTicketRequired;
            GameShopItemCellView.onRemoveTicketEvent += OnRemoveTicketRequired;
        }
        public override void OnScreenShowAnimationCompleted()
        {
            base.OnScreenShowAnimationCompleted();
        }

        public override void OnScreenHideCalled()
        {
            base.OnScreenHideCalled();
            GameShopItemCellView.onAddTicketEvent -= OnAddTicketRequired;
            GameShopItemCellView.onRemoveTicketEvent -= OnRemoveTicketRequired;
        }


        #endregion
    }
}


