using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : ButtonController
{
    public GameObject containerObject;

    protected GameObject _spawnedObject;

    // Use this for initialization
    void Start()
    {
        _spawnedObject = Instantiate(containerObject) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void EnableButtons(bool enable)
    {
        foreach (var button in _buttons.Where(button => button.ButtonName != "Menu"))
        {
            button.Button.gameObject.SetActive(enable);
        }
    }

    private void UpdateMenuButtonText(string text)
    {
        ButtonList
            .Select(t => t.GetComponentInParent<MenuButton>())
            .First(b => b.ButtonName == "Menu")
            .ButtonText.text = text;
    }

    protected override void SetControllerReferenceOnButtons()
    {
        base.SetControllerReferenceOnButtons();
        EnableButtons(false);
    }

    public override void ButtonClick(MenuButton button)
    {
        if (button.ButtonName == "Menu") EnableButtons(true);
        else
        {
            _spawnedObject.SendMessage("EnableCanvas", button.ButtonText.text, SendMessageOptions.RequireReceiver);
            UpdateMenuButtonText(button.ButtonText.text);
            EnableButtons(false);
        }
    }
}