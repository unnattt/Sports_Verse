using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Yudiz.BaseFramework
{
    [Serializable]
    public class PopUpScreen
    {
        public PopUpType popUpType;
        public UIScreenView screenView;
    }

    public enum PopUpType
    {
        None,
        ConfirmationPopup,
        ToastPopup,
        ShopPopup,
        JoyridePopup,
        NavigationBarPopup
    }
    public class PopUpController : Singleton<PopUpController>
    {
        public List<PopUpScreen> popUps;

        public void ShowNextPopup(PopUpType screenType, float Delay = 0.1f)
        {
            StartCoroutine(ExecuteAfterDelay(Delay, () =>
            {
                ShowPopup(screenType);
            }));
        }

        public void ShowPopup(PopUpType popUpType)
        {
            HideAllPopUps();
            GetPopup(popUpType).Show();
        }

        public void HidePopup(PopUpType popUpType)
        {
            GetPopup(popUpType).Hide();
        }

        public UIScreenView GetPopup(PopUpType popUpType)
        {
            return popUps.Find(popUp => popUp.popUpType == popUpType).screenView;
        }
        public T GetPopup<T>(PopUpType popUpType) where T : UIScreenView
        {
            return (popUps.Find(popUp => popUp.popUpType == popUpType).screenView) as T;
        }

        IEnumerator ExecuteAfterDelay(float Delay, Action CallbackAction)
        {
            yield return new WaitForSeconds(Delay);

            CallbackAction();
        }
        public void HideAllPopUps()
        {
            for (int i = 0; i < popUps.Count; i++)
            {
                PopUpType popUpType = popUps[i].popUpType;
                HidePopup(popUpType);
            }
        }

    }
}