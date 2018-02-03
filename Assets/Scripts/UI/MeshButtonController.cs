using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.UI;

public class MeshButtonController : MonoBehaviour
{
    public Color NormalColor = Color.white;
    public Color SelectedColor = new Color(106, 119, 255, 255);

    private bool _cylinder;
    private bool _cuboid;
    private bool _select;
    private bool _edit;
    private bool _lockX;
    private bool _lockY;

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

    #region Mapping Dictionaries
    private Dictionary<string, ButtonType> mapButtonTypes = new Dictionary<string, ButtonType>
    {
        {CYLINDER_BUTTON, ButtonType.Cylinder},
        {CUBOID_BUTTON, ButtonType.Cuboid},
        {SELECT_BUTTON, ButtonType.Select},
        {EDIT_BUTTON, ButtonType.Edit},
        {LOCK_X_BUTTON, ButtonType.LockX},
        {LOCK_Y_BUTTON, ButtonType.LockY}
    };

    private readonly Dictionary<string, string[]> _mapButtonsToClear = new Dictionary<string, string[]>
    {
        {CYLINDER_BUTTON, new[] { CUBOID_BUTTON, SELECT_BUTTON, EDIT_BUTTON, LOCK_X_BUTTON, LOCK_Y_BUTTON }},
        {CUBOID_BUTTON, new[] { CYLINDER_BUTTON, SELECT_BUTTON, EDIT_BUTTON, LOCK_X_BUTTON, LOCK_Y_BUTTON }},
        {SELECT_BUTTON, new[] { CYLINDER_BUTTON, CUBOID_BUTTON, EDIT_BUTTON, LOCK_X_BUTTON, LOCK_Y_BUTTON }},
        {EDIT_BUTTON, new[] { CYLINDER_BUTTON, CUBOID_BUTTON, SELECT_BUTTON, LOCK_X_BUTTON, LOCK_Y_BUTTON }},
        {LOCK_X_BUTTON, new[] { LOCK_Y_BUTTON }},
        {LOCK_Y_BUTTON, new[] { LOCK_X_BUTTON}}
    };

    private readonly Dictionary<string, string[]> _mapButtonsToDisable = new Dictionary<string, string[]>
    {
        {CYLINDER_BUTTON, new[] { LOCK_X_BUTTON, LOCK_Y_BUTTON }},
        {CUBOID_BUTTON, new[] { LOCK_X_BUTTON, LOCK_Y_BUTTON }},
        {SELECT_BUTTON, new[] { LOCK_X_BUTTON, LOCK_Y_BUTTON }},
        {EDIT_BUTTON, new [] { "" }},
        {LOCK_X_BUTTON, new[] { "" }},
        {LOCK_Y_BUTTON, new[] { "" }}
    };

    private readonly Dictionary<string, string[]> _mapButtonsToEnable = new Dictionary<string, string[]>
    {
        {CYLINDER_BUTTON, new [] { "" }},
        {CUBOID_BUTTON, new [] { "" }},
        {SELECT_BUTTON, new [] { "" }},
        {EDIT_BUTTON, new[] { LOCK_X_BUTTON, LOCK_Y_BUTTON }},
        {LOCK_X_BUTTON, new[] { "" }},
        {LOCK_Y_BUTTON, new[] { "" }}
    };
    #endregion

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
            var btn = button;
            button.onClick.AddListener(() => ButtonClicked(btn));
            AddActionListener(button, buttons.ToList().FindAll(b => _mapButtonsToClear[btn.name].Contains(b.name)), true, ButtonClear);
            AddActionListener(button, buttons.ToList().FindAll(b => _mapButtonsToDisable[btn.name].Contains(b.name)), false, ButtonDisable);
            AddActionListener(button, buttons.ToList().FindAll(b => _mapButtonsToEnable[btn.name].Contains(b.name)), true, ButtonDisable);
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
        if (buttonType == ButtonType.Cylinder) UpdateButtonState(button, ref _cylinder);
        if (buttonType == ButtonType.Cuboid) UpdateButtonState(button, ref _cuboid);
        if (buttonType == ButtonType.Select) UpdateButtonState(button, ref _select);
        if (buttonType == ButtonType.Edit) UpdateButtonState(button, ref _edit);
        if (buttonType == ButtonType.LockX) UpdateButtonState(button, ref _lockX);
        if (buttonType == ButtonType.LockY) UpdateButtonState(button, ref _lockY);
    }

    void ButtonClear(Button button, bool enable)
    {
        var buttonType = mapButtonTypes[button.name];
        if (buttonType == ButtonType.Cylinder) ClearButtonState(button, ref _cylinder);
        if (buttonType == ButtonType.Cuboid) ClearButtonState(button, ref _cuboid);
        if (buttonType == ButtonType.Select) ClearButtonState(button, ref _select);
        if (buttonType == ButtonType.Edit) ClearButtonState(button, ref _edit);
    }

    void ButtonDisable(Button button, bool enable)
    {
        var buttonType = mapButtonTypes[button.name];
        if (buttonType == ButtonType.LockX) ClearButtonState(button, ref _lockX, enable && _edit);
        if (buttonType == ButtonType.LockY) ClearButtonState(button, ref _lockY, enable && _edit);
    }

    void ClearButtonState(Button button, ref bool value, bool enabled = true)
    {
        value = true;
        UpdateButtonState(button, ref value, enabled);
    }

    void UpdateButtonState(Button button, ref bool value, bool enabled = true)
    {
        var colorBlock = button.colors;
        var updatedColor = UpdateState(ref value);
        colorBlock.normalColor = updatedColor;
        colorBlock.highlightedColor = updatedColor;
        button.colors = colorBlock;
        button.enabled = enabled;
        Debug.Log(string.Format("Button Name {0}", button.name));
    }

    Color UpdateState(ref bool value)
    {
        value = !value;
        return value ? SelectedColor : NormalColor;
    }
}
