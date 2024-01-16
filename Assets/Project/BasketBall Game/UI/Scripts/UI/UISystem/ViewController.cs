﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UISystem
{

    public class ViewController : Singleton<ViewController>
    {
        Screen currentView;
        Screen previousView;
        [SerializeField] ScreenName initScreen;

        [Divider]
        [SerializeField] List<ScreenView> screens = new List<ScreenView>();
        [Divider]
        [SerializeField] List<PopupView> popups = new List<PopupView>();
        [Divider]
        [SerializeField] NavBar navBar;
        [SerializeField] Popup toast;

        [System.Serializable]
        public struct ScreenView
        {
            public Screen screen;
            public ScreenName screenName;
            public bool hasNavBar;
        }

        [System.Serializable]
        public struct PopupView
        {
            public Popup popup;
            public PopupName popupName;
        }
        void Start() => Init();

        public void ShowPopup(PopupName popupName)
        {
            Debug.Log(popupName);
            popups[GetPopupIndex(popupName)].popup.Show();
        }

        public void HidePopup(PopupName popupName)
        {
            popups[GetPopupIndex(popupName)].popup.Hide();
        }
        public void ShowToast(string description, float delay = 3)
        {
            toast.Fill(description);
            toast.Show();
            Helper.Execute(this, () => toast.Hide(), delay);
        }
        public void ChangeView(ScreenName screen)
        {
            if (currentView != null)
            {
                previousView = currentView;
                previousView.Hide();
                currentView = screens[GetScreenIndex(screen)].screen;
                currentView.Show();
            }
            else
            {
                currentView = screens[GetScreenIndex(screen)].screen;
                currentView.Show();
            }

            //enable nav bar if allowed in current screen
            // if (screens[GetScreenIndex(screen)].hasNavBar)
            // {
            //     if (!navBar.isActive)
            //         navBar.Show();
            // }
            // else
            // {
            //     if (navBar.isActive)
            //         navBar.Hide();
            // }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                for (int i = 0; i < popups.Count; i++)
                {
                    if (popups[i].popup.isActive)
                    {
                        popups[i].popup.Hide();
                        return;
                    }
                }
            }
        }

        int GetScreenIndex(ScreenName screen)
        {
            return screens.FindIndex(
            delegate (ScreenView screenView)
            {
                return screenView.screenName.Equals(screen);
            });
        }

        int GetPopupIndex(PopupName popup)
        {
            return popups.FindIndex(
            delegate (PopupView popupView)
            {
                return popupView.popupName.Equals(popup);
            });
        }

        public void RedrawView() => currentView.Redraw();

        private void Init()
        {
            for (int indexOfScreen = 0; indexOfScreen < screens.Count; indexOfScreen++)
            {
                screens[indexOfScreen].screen.Disable();
            }

            if (initScreen != ScreenName.None)
            {
                ChangeView(initScreen);
            }
        }

        // public void ShowPopup(string title, string description)
        // {
        //     toast.Show(title, description);
        // }

        // public void HidePopup()
        // {
        //     toast.Hide();
        // }

        // ViewManager.Instance.GetViewComponent<ViewHunting>().ToggleChipsPopup(true);

        public T GetScreen<T>(ScreenName sName) => (T)screens[GetScreenIndex(sName)].screen.GetComponent<T>();
        public T GetPopup<T>(PopupName sName) => (T)popups[GetPopupIndex(sName)].popup.GetComponent<T>();
    }
}