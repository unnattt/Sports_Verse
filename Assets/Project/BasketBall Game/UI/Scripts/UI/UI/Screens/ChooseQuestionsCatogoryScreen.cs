using UnityEngine;
using UnityEngine.UI;
using UISystem;
using Yudiz.VRBasketBall.Core;
using DG.Tweening;
public class ChooseQuestionsCatogoryScreen : UISystem.Popup
{
    [SerializeField] QuestionDatabase firstCatogorySo;
    [SerializeField] QuestionDatabase secondCatogorySo;
    [SerializeField] QuestionDatabase thirdCatogorySo;
    public void FirstCatogory()
    {
        GameManager.instance.questionDatabase = firstCatogorySo;
        Hide();
    }
    public void SecondCatogory()
    {
        GameManager.instance.questionDatabase = secondCatogorySo;
        Hide();
    }
    public void ThirdCatogory()
    {
        GameManager.instance.questionDatabase = thirdCatogorySo;
        Hide();
    }
    #region BASE_UI_CALLBACKS

    public override void Show()
    {
        base.Show();
    }
    public override void Hide()
    {
        ViewController.Instance.ChangeView(ScreenName.GamePlayScreen);
        Events.onGameplayStart?.Invoke();
        base.Hide();
    }
    public override void Disable()
    {
        base.Disable();
    }
    public override void Redraw()
    {
        base.Redraw();
    }


    #endregion
}
