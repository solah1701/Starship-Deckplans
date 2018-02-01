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

    private const string CYLINDER_BUTTON = "Cylinder Button";
    private const string CUBOID_BUTTON = "Cuboid Button";
    private const string SELECT_BUTTON = "Select Button";
    private const string EDIT_BUTTON = "Edit Button";
    private const string LOCK_X_BUTTON = "Lock X Button";
    private const string LOCK_Y_BUTTON = "Lock Y Button";

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
        {CYLINDER_BUTTON, ButtonType.Cylinder},
        {CUBOID_BUTTON, ButtonType.Cuboid},
        {SELECT_BUTTON, ButtonType.Select},
        {EDIT_BUTTON, ButtonType.Edit},
        {LOCK_X_BUTTON, ButtonType.LockX},
        {LOCK_Y_BUTTON, ButtonType.LockY}
    };

    void Start()
    {
        Debug.Log("Start Mesh Button Controller");
        GetButtonsInPanel();
    }

    private Dictionary<string, string[]> mapButtonsToClear = new Dictionary<string, string[]>
    {
        {CYLINDER_BUTTON, new[] { CUBOID_BUTTON, SELECT_BUTTON, EDIT_BUTTON, LOCK_X_BUTTON, LOCK_Y_BUTTON }},
        {CUBOID_BUTTON, new[] { CYLINDER_BUTTON, SELECT_BUTTON, EDIT_BUTTON, LOCK_X_BUTTON, LOCK_Y_BUTTON }},
        {SELECT_BUTTON, new[] { CYLINDER_BUTTON, CUBOID_BUTTON, EDIT_BUTTON, LOCK_X_BUTTON, LOCK_Y_BUTTON }},
        {EDIT_BUTTON, new[] { CYLINDER_BUTTON, CUBOID_BUTTON, SELECT_BUTTON, LOCK_X_BUTTON, LOCK_Y_BUTTON }},
        {LOCK_X_BUTTON, new[] { LOCK_Y_BUTTON }},
        {LOCK_Y_BUTTON, new[] { LOCK_X_BUTTON}}
    };

    private Dictionary<string, string[]> mapButtonsToDisable = new Dictionary<string, string[]>
    {
        {CYLINDER_BUTTON, new[] { LOCK_X_BUTTON, LOCK_Y_BUTTON }},
        {CUBOID_BUTTON, new[] { LOCK_X_BUTTON, LOCK_Y_BUTTON }},
        {SELECT_BUTTON, new[] { LOCK_X_BUTTON, LOCK_Y_BUTTON }},
        {EDIT_BUTTON, new [] { "" }},
        {LOCK_X_BUTTON, new[] { "" }},
        {LOCK_Y_BUTTON, new[] { "" }}

    };

    private Dictionary<string, string[]> mapButtonsToEnable = new Dictionary<string, string[]>
    {
        {CYLINDER_BUTTON, new [] { "" }},
        {CUBOID_BUTTON, new [] { "" }},
        {SELECT_BUTTON, new [] { "" }},
        {EDIT_BUTTON, new[] { LOCK_X_BUTTON, LOCK_Y_BUTTON }},
        {LOCK_X_BUTTON, new[] { "" }},
        {LOCK_Y_BUTTON, new[] { "" }}

    };

    void GetButtonsInPanel()
    {
        var buttons = GetComponentsInChildren<Button>();

        foreach (Button button in buttons)
        {
            var btn = button;
            button.onClick.AddListener(() => ButtonClicked(btn));
            AddActionListener(button, buttons.ToList().FindAll(b => mapButtonsToClear[btn.name].Contains(b.name)), true, ButtonClear);
            AddActionListener(button, buttons.ToList().FindAll(b => mapButtonsToDisable[btn.name].Contains(b.name)), false, ButtonDisable);
            AddActionListener(button, buttons.ToList().FindAll(b => mapButtonsToEnable[btn.name].Contains(b.name)), true, ButtonDisable);
        }
    }

    void AddActionListener(Button button, List<Button> btns, bool enable, Action<Button, bool> action)
    {
        foreach (Button btn in btns)
        {
            var bn = btn;
            button.onClick.AddListener(() => action(bn, enable));
        }
    }

    void ButtonClicked(Button button)
    {
        var buttonType = mapButtonTypes[button.name];
        if (buttonType == ButtonType.Cylinder) UpdateButtonState(button, ref Cylinder);
        if (buttonType == ButtonType.Cuboid) UpdateButtonState(button, ref Cuboid);
        if (buttonType == ButtonType.Select) UpdateButtonState(button, ref Select);
        if (buttonType == ButtonType.Edit) UpdateButtonState(button, ref Edit);
        if (buttonType == ButtonType.LockX) UpdateButtonState(button, ref LockX);
        if (buttonType == ButtonType.LockY) UpdateButtonState(button, ref LockY);
    }

    void ButtonClear(Button button, bool enable)
    {
        var buttonType = mapButtonTypes[button.name];
        if (buttonType == ButtonType.Cylinder) ClearButtonState(button, ref Cylinder);
        if (buttonType == ButtonType.Cuboid) ClearButtonState(button, ref Cuboid);
        if (buttonType == ButtonType.Select) ClearButtonState(button, ref Select);
        if (buttonType == ButtonType.Edit) ClearButtonState(button, ref Edit);
    }

    void ButtonDisable(Button button, bool enable)
    {
        var buttonType = mapButtonTypes[button.name];
        if (buttonType == ButtonType.LockX) ClearButtonState(button, ref LockX, enable);
        if (buttonType == ButtonType.LockY) ClearButtonState(button, ref LockY, enable);
    }

    void ClearButtonState(Button button, ref bool value, bool enabled = true)
    {
        value = true;
        UpdateButtonState(button, ref value, enabled);
    }

    void UpdateButtonState(Button button, ref bool value, bool enabled = true)
    {
        var cb = button.colors;
        cb.normalColor = UpdateState(ref value);
        button.colors = cb;
        button.enabled = enabled;
        Debug.Log(string.Format("Button Name {0}", button.name));
    }

    Color UpdateState(ref bool value)
    {
        value = !value;
        return value ? SelectedColor : NormalColor;
    }
}
