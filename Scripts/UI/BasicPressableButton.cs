using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BasicPressableButton : BasicUIView, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public event Action PointerEnterd;
    public event Action PointerClicked;
    public event Action PointerExited;
    protected Vector2 OgSize;
    protected State state = State.idle;
    public enum State
    {
        idle,
        hover,
        disable,
        selected,
    }
    public float hoverTime = 0.2f;
    public float normalTime = 0.3f;
    public Vector2 HoverSize = new Vector2(1.2f, 1f);

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
    protected void OnPointerClicked()
    {
        PointerClicked?.Invoke();
    }

    public virtual void ChangeStateToIdle()
    {
        state = State.idle;
        if(HoverSize != Vector2.one)
        {
            DoSizeTween(OgSize, normalTime + 0.1f, type: EaseType.M3Spring);
        }
    }
    public virtual void ChangeStateToHover()
    {
        state = State.hover;
        if (HoverSize != Vector2.one)
        {
            DoSizeTween(HoverSize * OgSize, hoverTime + 0.1f, type: EaseType.M3Spring);
        }
    }
    public virtual void ChangeStateToSelected()
    {
        state = State.selected;
        if (HoverSize != Vector2.one)
        {
            DoSizeTween(OgSize, normalTime + 0.1f, type: EaseType.M3Spring);
        }
    }
    public virtual void ChangeStateToDisable()
    {
        state = State.disable;
        if(GetCanvasGroup() != null )
        {
            DoAlphaTween(0.5f, normalTime);
        }
        if (HoverSize != Vector2.one)
        {
            DoSizeTween(OgSize, normalTime + 0.1f, type: EaseType.M3Spring);
        }
    }
    public virtual void ChangeStateToDisableImmediately()
    {
        state = State.disable;
        if(GetCanvasGroup() != null )
        {
            SetCanvasGroupAlpha(0.5f);
        }
        SetSize(OgSize);
    }
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (state != State.disable)
        {
            OnPointerClicked();
        }
    }
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (state == State.idle)
        {
            ChangeStateToHover();
            OnPointerEntered();
        }
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (state == State.hover)
        {
            ChangeStateToIdle();
            OnPointerExited();
        }
    }


}
