using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneController : MonoBehaviour
{
    public BasicUIView startSceneUI;
    public BasicUIView nameSceneUI;

    public BasicPressableButton startButton;


    public void Awake()
    {
        startButton.PointerClicked += StartGame;
        nameSceneUI.SetCanvasGroupAlpha(0f);
        nameSceneUI.SetCanvasGroupUnblock();
        startSceneUI.SetCanvasGroupAlpha(1f);
        startSceneUI.SetCanvasGroupBlockable();
    }

    public void StartGame()
    {
        startSceneUI.SetCanvasGroupUnblock();
        startSceneUI.DoAlphaTween(0f, 0.3f);
        nameSceneUI.DoDelay(0.1f, callBack: () => { nameSceneUI.DoAlphaTween(1f, 0.3f, callBack: nameSceneUI.SetCanvasGroupBlockable); });
    }

}
