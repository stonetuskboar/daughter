using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainSceneController : MonoBehaviour
{
    public BasicUIView sceneUI;

    public BasicUIView leftUI;
    public BasicUIView rightUI;

    public void Awake()
    {

    }


    public void SetSceneUnShow() 
    {
        sceneUI.SetCanvasGroupAlpha(0f);
        sceneUI.SetCanvasGroupUnblock();
    }


    public void Show(Action callBack = null)
    {
        sceneUI.DoDelay(0.6f, sceneUI.SetCanvasGroupBlockable);
        sceneUI.DoAlphaTween(1f, 0.6f, callBack: callBack, type: EaseType.EaseInQuad);
    }

}
