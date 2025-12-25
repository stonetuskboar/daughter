using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageTextButton : BasicPressableButton
{
    public ImageColorTweenObj image;
    public TextColorTweenObj text;
    public override void ChangeStateToIdle()
    {
        base.ChangeStateToIdle();
        image.NormalTween(normalTime);
        text.NormalTween(normalTime);
    }
    public override void ChangeStateToHover()
    {
        base.ChangeStateToHover();
        image.HoverTween(hoverTime);
        text.HoverTween(hoverTime);
    }

    public override void ChangeStateToSelected()
    {
        base.ChangeStateToSelected();
        image.HoverTween(hoverTime);
        text.HoverTween(hoverTime);
    }
}
