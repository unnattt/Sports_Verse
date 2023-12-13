using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace Yudiz.BaseFramework
{
    public class MoveElementAnimatable : ElementAnimatable
    {

        public AnimationDirection animationDirection;

        RectTransform _TargetTransform;
        Vector3 _initialPosition;

        float multiplier = 2f;


        public override void Initialize(UIScreenView screenView)
        {

            _TargetTransform = GetComponent<RectTransform>();
            _initialPosition = _TargetTransform.anchoredPosition3D;

            float offset = Screen.width > Screen.height ? Screen.width : Screen.height;
            offset *= multiplier;


            switch (animationDirection)
            {
                case AnimationDirection.Top:
                    _TargetTransform.anchoredPosition = new Vector2(_initialPosition.x, Screen.height + offset);
                    break;

                case AnimationDirection.Bottom:
                    _TargetTransform.anchoredPosition = new Vector2(_initialPosition.x, -Screen.height - offset);
                    break;

                case AnimationDirection.Left:
                    _TargetTransform.anchoredPosition = new Vector2(-Screen.width - offset, _initialPosition.y);
                    break;

                case AnimationDirection.Right:
                    _TargetTransform.anchoredPosition = new Vector2(Screen.width + offset, _initialPosition.y);
                    break;
            }

            gameObject.SetActive(false);

        }

        public override void ResetAnimator()
        {
            float offset = Screen.width > Screen.height ? Screen.width : Screen.height;
            offset *= multiplier;

            switch (animationDirection)
            {
                case AnimationDirection.Top:
                    _TargetTransform.anchoredPosition = new Vector2(_initialPosition.x, Screen.height + offset);
                    break;

                case AnimationDirection.Bottom:
                    _TargetTransform.anchoredPosition = new Vector2(_initialPosition.x, -Screen.height - offset);
                    break;

                case AnimationDirection.Left:
                    _TargetTransform.anchoredPosition = new Vector2(-Screen.width - offset, _initialPosition.y);
                    break;

                case AnimationDirection.Right:
                    _TargetTransform.anchoredPosition = new Vector2(Screen.width + offset, _initialPosition.y);
                    break;
            }

            gameObject.SetActive(true);

        }

        public override void ShowAnimation(float time, Ease ease, float delay, ShowAnimationCompleted callback)
        {

            _TargetTransform.DOAnchorPos(_initialPosition, time).SetEase(ease).SetDelay(delay).OnStart(ResetAnimator).onComplete = () =>
            {

                if (callback != null)
                {
                    callback();
                }

            };
        }

        public override void HideAnimation(float time, Ease ease, float delay, AnimationTransition animationTransition, HideAnimationCompleted callback)
        {

            float offset = Screen.width > Screen.height ? Screen.width : Screen.height;
            offset *= multiplier;

            Vector2 _endPosition = Vector2.zero;

            switch (animationDirection)
            {

                case AnimationDirection.Top:

                    if (animationTransition == AnimationTransition.Forward)
                    {
                        _endPosition = new Vector2(_initialPosition.x, Screen.height + offset);

                    }
                    else
                    {
                        _endPosition = new Vector2(_initialPosition.x, -Screen.height - offset);
                    }

                    break;

                case AnimationDirection.Bottom:
                    if (animationTransition == AnimationTransition.Forward)
                    {
                        _endPosition = new Vector2(_initialPosition.x, -Screen.height - offset);

                    }
                    else
                    {
                        _endPosition = new Vector2(_initialPosition.x, Screen.height + offset);
                    }
                    break;

                case AnimationDirection.Left:

                    if (animationTransition == AnimationTransition.Forward)
                    {
                        _endPosition = new Vector2(-Screen.width - offset, _initialPosition.y);
                    }
                    else
                    {
                        _endPosition = new Vector2(Screen.width + offset, _initialPosition.y);
                    }

                    break;

                case AnimationDirection.Right:

                    if (animationTransition == AnimationTransition.Forward)
                    {
                        _endPosition = new Vector2(Screen.width + offset, _initialPosition.y);
                    }
                    else
                    {
                        _endPosition = new Vector2(-Screen.width - offset, _initialPosition.y);
                    }
                    break;
            }

            _TargetTransform.DOAnchorPos(_endPosition, time).SetEase(ease).SetDelay(delay).onComplete = () =>
            {

                if (callback != null)
                {
                    callback();
                }

                gameObject.SetActive(false);


            };


        }

        public override void IdleAnimation()
        {
        }

    }
}

