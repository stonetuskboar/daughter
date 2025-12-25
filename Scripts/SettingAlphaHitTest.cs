using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingAlphaHitTest : MonoBehaviour
{
    public Image image;
    public float threshold = 0.05f;

    private void Start()
    {
        if(image == null)
        {
            image = GetComponent<Image>();
        }
        if(image == null)
        {
            Debug.LogError("Image component not found!");
            return;
        }
        image.alphaHitTestMinimumThreshold = threshold;
    }
}
