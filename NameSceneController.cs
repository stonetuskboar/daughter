using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameSceneController : MonoBehaviour
{
    public Canvas StartCanvas;
    public BasicUIView nameSceneUI;
    public MainSceneController mainSceneController;
    public ImageTextButton fatherButton;
    public ImageTextButton motherButton;

    public TMP_InputField parentNameInputField;
    public TMP_InputField daughterNameInputField;

    public ImageTextButton confirmButton;

    private ParentType nowType = ParentType.None;
    public enum ParentType
    {
        None,
        father,
        mother,
    }

    private string ParentName = "";
    private string DaughterName = "";

    public void Awake()
    {
        fatherButton.PointerClicked += OnFatherButtonClicked;
        motherButton.PointerClicked += OnMotherButtonClicked;
        parentNameInputField.onValueChanged.AddListener(OnParentNameChanged);
        daughterNameInputField.onValueChanged.AddListener(OnDaughterNameChanged);
        confirmButton.PointerClicked += OnConfirmClicked;
        confirmButton.SetCanvasGroupAlpha(0f);
        confirmButton.SetCanvasGroupBlockable();
        mainSceneController.SetSceneUnShow();
    }

    public void OnFatherButtonClicked()
    {
        if (nowType != ParentType.father)
        {
            fatherButton.ChangeStateToSelected();
            motherButton.ChangeStateToIdle();
            nowType = ParentType.father;
        }
        else
        {
            fatherButton.ChangeStateToIdle();
            nowType = ParentType.None;
        }
        JudgeIfOkToContinue();
    }
    public void OnMotherButtonClicked()
    {
        if (nowType != ParentType.mother)
        {
            fatherButton.ChangeStateToIdle();
            motherButton.ChangeStateToSelected();
            nowType = ParentType.mother;
        }
        else
        {
            motherButton.ChangeStateToIdle();
            nowType = ParentType.None;
        }
        JudgeIfOkToContinue();
    }


    public void OnParentNameChanged(string str)
    {
        ParentName = str;
        JudgeIfOkToContinue();
    }

    public void OnDaughterNameChanged(string str)
    {
        DaughterName = str;
        JudgeIfOkToContinue();
    }

    public void JudgeIfOkToContinue()
    {
        if (string.IsNullOrWhiteSpace(ParentName) == false
            && string.IsNullOrWhiteSpace(DaughterName) == false
            && nowType != ParentType.None)
        {
            confirmButton.DoAlphaTween(1f, 0.3f);
            confirmButton.SetCanvasGroupBlockable();
        }
        else
        {
            confirmButton.DoAlphaTween(0f, 0.3f);
            confirmButton.SetCanvasGroupUnblock();
        }
    }


    public void OnConfirmClicked()
    {
        mainSceneController.Show(AfterMainUIShow);
        nameSceneUI.DoAlphaTween(0f, 0.3f);
        nameSceneUI.SetCanvasGroupUnblock();
    }

    private void AfterMainUIShow()
    {
        StartCanvas.gameObject.SetActive(false);
    }
}
