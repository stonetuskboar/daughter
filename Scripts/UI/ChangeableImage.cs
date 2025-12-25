using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeableImage : ImageColorTweenObj
{
    public Sprite NormalSprite;
    public Sprite HoverSprite;

    public override void SetToNormal()
    {
        SetColor(normalColor);
        image.sprite = NormalSprite;
    }
    public virtual void SetToHover()
    {
        SetColor(hoverColor);
        image.sprite = HoverSprite;
    }

    public override void HoverTween(float time = 0.2f)
    {
        image.sprite = HoverSprite;
        DoColorTween(hoverColor, time);
    }
    public override void NormalTween(float time = 0.3f)
    {
        image.sprite = NormalSprite;
        DoColorTween(normalColor, time);
    }
}
