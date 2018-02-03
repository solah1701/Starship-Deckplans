using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpButtonController : MonoBehaviour
{

    public bool IsActive;
    public Color NormalColor = Color.white;
    public Color SelectedColor = new Color(106, 119, 255, 255);

    // Use this for initialization
    void Start()
    {
        BindEventClicks();
    }

    void BindEventClicks()
    {
        var button = GetComponent<Button>();
        if (button == null) return;
        button.onClick.AddListener(() => ButtonClick(button));
        Debug.Log("ButtonClick bound");
    }

    void ButtonClick(Button button)
    {
        var colorBlock = button.colors;
        var updatedColor = UpdateState(ref IsActive);
        colorBlock.normalColor = updatedColor;
        colorBlock.highlightedColor = updatedColor;
        button.colors = colorBlock;
    }

    Color UpdateState(ref bool value)
    {
        value = !value;
        return value ? SelectedColor : NormalColor;
    }
}
