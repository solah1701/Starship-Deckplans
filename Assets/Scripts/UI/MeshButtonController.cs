using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.UI;

public class MeshButtonController : MonoBehaviour
{

    public bool Cylinder;
    public bool Cuboid;
    public bool Select;
    public bool Edit;
    public bool LockX;
    public bool LockY;

    public Color NormalColor = Color.white;
    public Color SelectedColor = new Color(106, 119, 255, 255);

    private enum ButtonType
    {
        Cylinder,
        Cuboid,
        Select,
        Edit,
        LockX,
        LockY
    }

    private Dictionary<string, ButtonType> mapButtonTypes = new Dictionary<string, ButtonType>
    {
        {"Cylinder Button", ButtonType.Cylinder},
        {"Cuboid Button", ButtonType.Cuboid},
        {"Select Button", ButtonType.Select},
        {"Edit Button", ButtonType.Edit},
        {"Lock X Button", ButtonType.LockX},
        {"Lock Y Button", ButtonType.LockY}
    };

    void Start()
    {
        Debug.Log("Start Mesh Button Controller");
        GetButtonsInPanel();
    }

    void GetButtonsInPanel()
    {
        var buttons = GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            var theButton = button;
            button.onClick.AddListener(() => ButtonClicked(theButton));
            Debug.Log(string.Format("Button Name {0}", button.name));
        }
    }

    void ButtonClicked(Button button)
    {
        var buttonType = mapButtonTypes[button.name];
        var cb = button.colors;
        if (buttonType == ButtonType.Cylinder) cb.normalColor = UpdateState(ref Cylinder);
        if (buttonType == ButtonType.Cuboid) cb.normalColor = UpdateState(ref Cuboid);
        if (buttonType == ButtonType.Select) cb.normalColor = UpdateState(ref Select);
        if (buttonType == ButtonType.Edit) cb.normalColor = UpdateState(ref Edit);
        if (buttonType == ButtonType.LockX) cb.normalColor = UpdateState(ref LockX);
        if (buttonType == ButtonType.LockY) cb.normalColor = UpdateState(ref LockY);
        button.colors = cb;
    }

    public Color UpdateState(ref bool value)
    {
        value = !value;
        return value ? SelectedColor : NormalColor;
    }
}
