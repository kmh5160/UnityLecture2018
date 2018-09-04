﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour {

    public Button startButton;
    public Animator anim;

    private void Start()
    {
        startButton.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        print("Clicked!");
        anim.SetTrigger("Died");
    }
}
