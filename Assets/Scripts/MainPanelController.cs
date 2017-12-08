using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanelController : MonoBehaviour {

    public InputField ShipNameText;
    public ShipManager ShipManager;

	// Use this for initialization
	void OnEnable () {
		ShipManager.Init ();
		ShipNameText.text = ShipManager.GetShipName();
	}

	public void UpdateShipname() {
		ShipManager.UpdateShipName (ShipNameText.text);
	}
}
