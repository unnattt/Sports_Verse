using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Yudiz.BaseFramework
{
    public class UIScreenView : UIBase
    {
        [HideInInspector]
        public Image Background;
        [HideInInspector]
        public RectTransform Parent;

        UIAnimator _uiAnimator;

        public override void OnAwake()
        {
            base.OnAwake();
            Background = transform.Find(BACKGROUND).GetComponent<Image>();
            Parent = transform.Find(PARENT).GetComponent<RectTransform>();
            _uiAnimator = GetComponent<UIAnimator>();
        }

        public override void OnScreenShowCalled()
        {
            ToggleCanvasElements(true);
            base.OnScreenShowCalled();
        }

        public override void OnScreenShowAnimationCompleted()
        {
            base.OnScreenShowAnimationCompleted();

            //BackKeyRoutine = StartCoroutine(BackKeyUpdateRoutine());
        }

        Coroutine BackKeyRoutine;

        public override void OnScreenHideCalled()
        {
            base.OnScreenHideCalled();

            //StopCoroutine(BackKeyRoutine);


            if (_uiAnimator == null)
            {
                OnScreenHideAnimationCompleted();
            }
        }

        public override void OnScreenHideAnimationCompleted()
        {
            base.OnScreenHideAnimationCompleted();

            ToggleCanvasElements(false);
        }

        //IEnumerator BackKeyUpdateRoutine()
        //{
        //    while (true)
        //    {
        //        if ( Keyboard.current.escapeKey.wasPressedThisFrame)
        //        {
        //            OnBack();
        //        }

        //        yield return null;
        //    }
        //}

    }
}