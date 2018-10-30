using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SmoothTransform : NetworkBehaviour {
    public float lerpRate = 15f;

    struct TransformData
    {
        public Vector3 position;
        public Quaternion rotation;
    }

    [SyncVar]
    TransformData syncTransform;

    void FixedUpdate()
    {
        if (isLocalPlayer)
            SendTransform();
        else
            LerpTransform();
    }

    [ClientCallback]
    void SendTransform()
    {
        TransformData tm = new TransformData()
        {
            position = transform.position,
            rotation = transform.rotation
        };
        CmdSendTransformToServer(tm);
    }

    [Command]
    void CmdSendTransformToServer(TransformData tm)
    {
        syncTransform = tm;
    }

    void LerpTransform()
    {
        transform.position = Vector3.Lerp(transform.position, syncTransform.position, Time.fixedDeltaTime * lerpRate);
        transform.rotation = Quaternion.Lerp(transform.rotation, syncTransform.rotation, Time.fixedDeltaTime * lerpRate);
    }
}
