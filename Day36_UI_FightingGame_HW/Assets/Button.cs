using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour {
    public GameObject LeftHP;
    public GameObject RightHP;

	public void OnClick()
    {
        print("Click");
        LeftHP.GetComponent<Image>().fillAmount -= 0.05f;
        RightHP.GetComponent<Image>().fillAmount -= 0.03f;
    }
}
