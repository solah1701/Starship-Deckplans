using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Extenders;
using Assets.Scripts.Models;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListController : MonoBehaviour
{

    public GameObject PrefabButton;
    public RectTransform ButtonPanel;
    public ButtonListManager ButtonListManager;

    private ObjectItemList ButtonNames;
    private string current;

    // Use this for initialization
	void Start()
	{
		InitButtons ();
	}

    void OnEnable()
    {
        InitButtons();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitButtons()
    {
        ButtonNames = ButtonListManager.InitButtons();
        PopulateButtons();
    }

    public void AddButton()
    {
		ButtonNames = ButtonListManager.AddButton();
        current = ButtonNames[ButtonNames.Count - 1].Cast<ButtonItem>().Key;
        PopulateButtons();
    }

    public void RemoveButton()
    {
        if (string.IsNullOrEmpty(current)) return;
        ButtonNames = ButtonListManager.RemoveButton(current);
        PopulateButtons();
    }

    void PopulateButtons()
    {
        var index = 0;
        ClearButtons();
        if (ButtonNames == null) return;
        foreach (var buttonName in ButtonNames)
        {
            CreateButton(ButtonPanel, buttonName.Cast<ButtonItem>().Key, index++, TheButtonClicked);
        }
    }

    void ClearButtons()
    {
        var tempButtons = ButtonPanel.GetComponentsInChildren<Button>();
        foreach (var tempButton in tempButtons)
        {
            Destroy(tempButton.gameObject);
        }
    }

    private bool CreateButton(RectTransform panel, string value, int index, ButtonClicked buttonClicked)
    {
        var ypos = -index * 30 - 30;
        var goButton = Instantiate(PrefabButton);
        goButton.transform.SetParent(panel, false);
        goButton.transform.localScale = new Vector3(1, 1, 1);
        goButton.transform.position += new Vector3(0, ypos, 0);

        var tempButton = goButton.GetComponent<Button>();
        var buttonText = goButton.GetComponentInChildren<Text>();
        buttonText.text = string.Format(" {0}", value);

        tempButton.onClick.AddListener(() => buttonClicked(value));

        var height = ButtonPanel.rect.height - 45;

        return -ypos > height;
    }

    delegate void ButtonClicked(string value);

    private void TheButtonClicked(string value)
    {
        current = value;
        ButtonListManager.ButtonListClicked(ButtonNames.Find(x => x.Cast<ButtonItem>().Key == value).Cast<ButtonItem>().Value);
    }
}
