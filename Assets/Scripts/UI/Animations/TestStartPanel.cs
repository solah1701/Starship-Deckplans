using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestStartPanel : MonoBehaviour {

	public InputField MyInputField;
	public TestGameObject MyTestGameObject;

	// Use this for initialization
	void OnEnable () {
		MyInputField.text = MyTestGameObject.SomeValue;
	}
}
