using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.UI;

public class MeshButtonController : MonoBehaviour
{
    public bool UseButtonLogic;

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

    private Dictionary<ButtonType, Button> buttons = new Dictionary<ButtonType, Button>();

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
            this.buttons.Add(mapButtonTypes[theButton.name], theButton);
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
        if (UseButtonLogic) ButtonLogic(buttonType);
    }

    void ButtonLogic(ButtonType value)
    {
        if (Cylinder && ButtonType.Cylinder == value) ClearAllBut(ButtonType.Cylinder);
        if (Cuboid && ButtonType.Cuboid == value) ClearAllBut(ButtonType.Cuboid);
        if (Select && ButtonType.Select == value) ClearAllBut(ButtonType.Select);
        if (ButtonType.Edit == value) ClearAllBut(ButtonType.Edit);
    }

    void ClearAllBut(ButtonType value)
    {
        if (Cylinder && ButtonType.Cylinder != value) UpdateButtonState(value, ref Cylinder);
        if (Cuboid && ButtonType.Cuboid != value) UpdateButtonState(value, ref Cuboid);
        if (Select && ButtonType.Select != value) UpdateButtonState(value, ref Select);
        if (Edit && ButtonType.Edit != value) UpdateButtonState(value, ref Edit);
        if (ButtonType.LockX != value) UpdateButtonState(value, ref LockX, Edit);
        if (ButtonType.LockY != value) UpdateButtonState(value, ref LockY, Edit);
    }

    void UpdateButtonState(ButtonType button, ref bool value, bool enabled = true)
    {
        var buttons = GetComponentsInChildren<Button>();
        var theButton = buttons.First(b => b.name == mapButtonTypes.FirstOrDefault(v => v.Value == button).Key);
        //var theButton = buttons[button];
        var cb = theButton.colors;
        cb.normalColor = UpdateState(ref value);
        theButton.colors = cb;
        theButton.enabled = enabled;
        Debug.Log(string.Format("Button Name {0}", theButton.name));
        var bmgr = theButton.GetComponent<ButtonManager>();
        if (bmgr != null) bmgr.EnableButton(enabled);
    }

    Color UpdateState(ref bool value)
    {
        value = !value;
        return value ? SelectedColor : NormalColor;
    }
}
