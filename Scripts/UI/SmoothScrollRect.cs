using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//注意,input system在[1.9.0] - 2024-07-15及以后大改了输入，使得输入不再自动归一化
//https://docs.unity3d.com/Packages/com.unity.inputsystem@1.14/changelog/CHANGELOG.html
//回退到版本[1.7.0] - 2023-08-14可以解决一半的问题，pc速度正常了，但webgl变成了0.05
//只能再添加一个normalized归一化操作，但这会导致eventsystem中的scroll delta per tick失效
//在unity 2022.3.40f1版本+1.7.0版本，一切正常，默认的webgl和pc滚动速度是一样的。
//然而2022.3.40+1.9.0以上版本，所有滚动速度都会变大120倍。 2022.3.62+1.7.0版本，windows滚动6，webgl滚动0.05
//2022.3.62+1.9.0以上版本，windows滚动720，webgl滚动6。推测是input system版本增大了滚动倍率（也就是不再归一化），unity引擎版本不同更改了pc的滚动基础值（也就是120）。
public class SmoothScrollRect : ScrollRect
{
    public bool SmoothScrolling { get; set; } = true;
    public float SmoothScrollTime { get; set; } = 0.2f;

    private int ScrollTweenId = 0;
    private Vector2 DeltaNormalizedPosition = Vector2.zero;
    // Start is called before the first frame update

    public override void OnScroll(PointerEventData data)
    {
        if (!IsActive())
            return;

        if (SmoothScrolling == true)
        {
            Vector2 positionBefore = normalizedPosition;
            data.scrollDelta = data.scrollDelta.normalized;
            base.OnScroll(data);
            Vector2 positionAfter = normalizedPosition;
            DeltaNormalizedPosition += positionAfter - positionBefore;
            SetNormalizedPosition(positionBefore);
            DoNormalizedTween(DeltaNormalizedPosition + positionBefore , SmoothScrollTime);
        }
        else
        {
            base.OnScroll(data);
        }
    }
    public void DoNormalizedTween(Vector2 position, float time, Action callBack = null, EaseType type = EaseType.EaseOutQuad, bool IsCallbackRequiredOnComplete = true)
    {
        StartCoroutine(NormalizedTween(position, time, callBack, type, IsCallbackRequiredOnComplete));
    }
    IEnumerator NormalizedTween(Vector2 targetPosition, float time, Action callback , EaseType type, bool IsCallbackRequiredOnComplete)
    {
        ScrollTweenId++;
        int id = ScrollTweenId;
        Vector2 startPositon = normalizedPosition;
        float nowTime = 0;
        nowTime += Time.unscaledDeltaTime;
        Func<float, float> EaseFunc = EasingFunc.GetEaseFuncByType(type, time);
        while (nowTime < time && id == ScrollTweenId)
        {
            UpdateNormalizedPosition(Vector2.LerpUnclamped(startPositon, targetPosition, EaseFunc(nowTime / time)));
            yield return null;
            nowTime += Time.unscaledDeltaTime;
        }
        if (id == ScrollTweenId)
        {
            UpdateNormalizedPosition(targetPosition);
            callback?.Invoke();
        }
        else
        {
            if (IsCallbackRequiredOnComplete == false)
            {
                callback?.Invoke();
            }
            //被中断
        }
    }
    public void UpdateNormalizedPosition(Vector2 position)
    {
        DeltaNormalizedPosition -= position - normalizedPosition;
        SetNormalizedPosition(position);
    }
    public void SetNormalizedPosition(Vector2 position)
    {
        if(vertical == true)
        {
            SetNormalizedPosition(position.y, 1);
        }
        if(horizontal == true)
        {
            SetNormalizedPosition(position.x, 0);
        }
    }


}
