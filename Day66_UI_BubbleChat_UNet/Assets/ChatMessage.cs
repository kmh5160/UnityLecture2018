﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatMessage : MonoBehaviour {
    public Color alignLeftColor;
    public Color alignRightColor;

    bool isAlignLeft;
    public bool IsAlignLeft
    {
        get
        {
            return isAlignLeft;
        }
        set
        {
            isAlignLeft = value;
            Transform bubble = transform.GetChild(0);
            if (isAlignLeft)
            {
                bubble.localRotation = Quaternion.AngleAxis(0, Vector3.up);
                bubble.GetComponent<Image>().color = alignLeftColor;
                bubble.GetChild(0).localRotation = Quaternion.AngleAxis(0, Vector3.up);
                GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.MiddleLeft;
                GetComponent<HorizontalLayoutGroup>().padding = new RectOffset(0, 200, 0, 0);
            }
            else
            {
                bubble.localRotation = Quaternion.AngleAxis(180, Vector3.up);
                bubble.GetComponent<Image>().color = alignRightColor;
                bubble.GetChild(0).localRotation = Quaternion.AngleAxis(180, Vector3.up);
                GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.MiddleRight;
                GetComponent<HorizontalLayoutGroup>().padding = new RectOffset(200, 0, 0, 0);
            }
        }
    }

    public void SetMessage(string message, bool isAlignLeft)
    {
        IsAlignLeft = isAlignLeft;
        GetComponentInChildren<Text>().text = message;
    }
}
