using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeableText : TextColorTweenObj
{
    public string normalText;
    public string hoverText;

    public override void SetToNormal()
    {
        SetColor(normalColor);
        SetText(normalText);
    }
    public virtual void SetToHover()
    {
        SetColor(hoverColor);
        SetText(hoverText);
    }

    public override void HoverTween(float time = 0.2f)
    {
        DoColorTween(hoverColor, time);
        SetText(hoverText);
    }
    public override void NormalTween(float time = 0.3f)
    {
        DoColorTween(normalColor, time);
        SetText(normalText);
    }
}
