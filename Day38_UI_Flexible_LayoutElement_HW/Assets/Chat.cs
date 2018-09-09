using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour {
    public GameObject Send;
    public GameObject target;
    public GameObject InputText;

    public void OnClick()
    {
        GameObject g = Instantiate(Send);
        g.GetComponentInChildren<Text>().text = InputText.GetComponent<InputField>().text;
        InputText.GetComponent<InputField>().text = "";
        g.transform.SetParent(target.transform);
        InputText.GetComponent<InputField>().Select();
        InputText.GetComponent<InputField>().ActivateInputField();
    }
}
