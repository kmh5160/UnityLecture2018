using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Chatting : NetworkBehaviour {
    ChatSystem chatSystem;
    string nickName;

    private void Start()
    {
        chatSystem = GameObject.FindObjectOfType<ChatSystem>();
        var inputField = chatSystem.inputField;
        inputField.onEndEdit.AddListener(message =>
        {
            if (!isLocalPlayer)
                return;

            print(message);
            if (message != string.Empty)
            {
                CmdSendChat(nickName + ": " + message);
            }
        });
    }

    [Command]
    void CmdSendChat(string message)
    {
        RpcReceivedChat(message);
    }

    [ClientRpc]
    private void RpcReceivedChat(string message)
    {
        chatSystem.AddMessage(message, !isLocalPlayer);
    }

    public override void OnStartLocalPlayer()
    {
        nickName = "User " + netId;
    }

}
