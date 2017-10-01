using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {

    public Text[] ButtonList;
    //public GameObject containerObject;

    //protected GameObject _spawnedObject;
    protected List<MenuButton> _buttons;

    // Use this for initialization
    void Start()
    {
        //_spawnedObject = Instantiate(containerObject) as GameObject;
    }

    // Update is called once per frame
    void Update () {
		
	}

    void Awake()
    {
        SetControllerReferenceOnButtons();
    }

    protected virtual  void SetControllerReferenceOnButtons()
    {
        _buttons = new List<MenuButton>();
        foreach (var menuButton in ButtonList.Select(t => t.GetComponentInParent<MenuButton>()))
        {
            _buttons.Add(menuButton);
            menuButton.SetControllerReference(this);
        }
    }

    public virtual void ButtonClick(MenuButton button) { }

}
