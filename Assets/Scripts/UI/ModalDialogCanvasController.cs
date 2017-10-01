using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ModalDialogCanvasController : BaseCanvasController
{

    public Text MessageText;
    public Button YesButton;
    public Button NoButton;
    public Button CancelButton;

    public void Choice(string question, UnityAction yesEvent, UnityAction noEvent, UnityAction cancelEvent)
    {
        ShowGameObject(true);
        YesButton.onClick.RemoveAllListeners();
        YesButton.onClick.AddListener(yesEvent);
        YesButton.onClick.AddListener(ClosePanel);

        NoButton.onClick.RemoveAllListeners();
        NoButton.onClick.AddListener(noEvent);
        NoButton.onClick.AddListener(ClosePanel);

        CancelButton.onClick.RemoveAllListeners();
        CancelButton.onClick.AddListener(cancelEvent);
        CancelButton.onClick.AddListener(ClosePanel);

        this.MessageText.text = question;

        //this.iconImage.gameObject.SetActive(false);
        YesButton.gameObject.SetActive(true);
        NoButton.gameObject.SetActive(true);
        CancelButton.gameObject.SetActive(true);
    }

    void ClosePanel()
    {
        ShowGameObject(false);
    }

}
