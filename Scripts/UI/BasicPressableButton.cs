using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BasicPressableButton : BasicUIView, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public event Action PointerEnterd;
    public event Action PointerDowned;
    public event Action PointerExited;
    protected Vector2 OgSize;
    protected State state = State.idle;
    public enum State
    {
        idle,
        hover,
        disable,
    }
    public float hoverTime = 0.2f;
    public float unHoverTime = 0.3f;
    public Vector2 HoverSize = new Vector2(1.2f, 1f);
    public BasicColorTweenObject buttonGraphic;
    protected override void Awake()
    {
        base.Awake();
        OgSize = rectTransform.sizeDelta;
    }


    protected void OnPointerEntered()
    {
        PointerEnterd?.Invoke();
    }

    protected void OnPointerExited()
    {
        PointerExited?.Invoke();
    }
    protected void OnPointerDowned()
    {
        PointerDowned?.Invoke();
    }

    public virtual void ChangeStateToIdle()
    {
        state = State.idle;
        buttonGraphic.NormalTween(unHoverTime);
        if(HoverSize != Vector2.one)
        {
            DoSizeTween(OgSize, unHoverTime + 0.1f, type: EaseType.M3Spring);
        }
    }
    public virtual void ChangeStateToHover()
    {
        state = State.hover;
        buttonGraphic.HoverTween(hoverTime);
        if (HoverSize != Vector2.one)
        {
            DoSizeTween(HoverSize * OgSize, hoverTime + 0.1f, type: EaseType.M3Spring);
        }
    }
    public virtual void ChangeStateToDisable()
    {
        state = State.disable;
        if(GetCanvasGroup() != null )
        {
            DoAlphaTween(0.5f, unHoverTime);
            buttonGraphic.NormalTween(unHoverTime);
        }
        else
        {
            buttonGraphic.DoAlphaTween(buttonGraphic.color.a / 2, unHoverTime);
        }
        if (HoverSize != Vector2.one)
        {
            DoSizeTween(OgSize, unHoverTime + 0.1f, type: EaseType.M3Spring);
        }
    }
    public virtual void ChangeStateToDisableImmediately()
    {
        state = State.disable;
        if(GetCanvasGroup() != null )
        {
            SetCanvasGroupAlpha(0.5f);
            buttonGraphic.SetToNormal();
        }
        else
        {
            buttonGraphic.SetAlpha(buttonGraphic.color.a /2);
        }
        SetSize(OgSize);
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (state != State.disable)
        {
            OnPointerDowned();
        }
    }
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (state != State.disable)
        {
            ChangeStateToHover();
            OnPointerEntered();
        }
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (state != State.disable)
        {
            ChangeStateToIdle();
            OnPointerExited();
        }
    }


}
