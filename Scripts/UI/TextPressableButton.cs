using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPressableButton : BasicPressableButton
{
    public ChangeableText Text;
    public override void ChangeStateToIdle()
    {
        base.ChangeStateToIdle();
        Text.NormalTween(normalTime);
    }
    public override void ChangeStateToHover()
    {
        base .ChangeStateToHover();
        Text.HoverTween(hoverTime);
    }

    public override void ChangeStateToDisable()
    {
        state = State.disable;
        if (GetCanvasGroup() != null)
        {
            DoAlphaTween(0.5f, normalTime);
            Text.NormalTween(normalTime);
        }
        else
        {
            Text.DoAlphaTween(Text.color.a / 2, normalTime);
        }
        if (HoverSize != Vector2.one)
        {
            DoSizeTween(OgSize, normalTime + 0.1f, type: EaseType.M3Spring);
        }
    }

    public override void ChangeStateToDisableImmediately()
    {
        state = State.disable;
        if (GetCanvasGroup() != null)
        {
            SetCanvasGroupAlpha(0.5f);
            Text.SetToNormal();
        }
        else
        {
            Text.SetAlpha(Text.color.a / 2);
        }
        SetSize(OgSize);
    }
}
