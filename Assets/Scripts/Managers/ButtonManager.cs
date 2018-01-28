using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    public void EnableButton(bool value)
    {
        var button = GetComponentInParent<Button>();
        button.enabled = value;
        Debug.Log(string.Format("Set button enabled to {0}", enabled));
    }
}
