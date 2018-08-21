using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColliderState
{
    Closed,
    Open,
    Colliding
}

public interface IHitBoxResponder
{
    void CollisionWith(Collider collider);
}

public class HitBox : MonoBehaviour {
    public LayerMask mask;
    public BoxCollider hitBox;
    public Color inActiveColor;
    public Color collisionOpenColor;
    public Color collidingColor;

    public ColliderState state = ColliderState.Closed;
    IHitBoxResponder responder = null;

    private void OnDrawGizmos()
    {
        //UpdateHitBox();
        CheckGizmoColor();
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(hitBox.center, hitBox.size);
    }

    private void CheckGizmoColor()
    {
        switch (state)
        {
            case ColliderState.Closed:
                Gizmos.color = inActiveColor;
                break;
            case ColliderState.Open:
                Gizmos.color = collisionOpenColor;
                break;
            case ColliderState.Colliding:
                Gizmos.color = collidingColor;
                break;
        }
    }

    public void UpdateHitBox()
    {
        if (state == ColliderState.Closed)
            return;
        Collider[] colliders = Physics.OverlapBox(transform.TransformPoint(hitBox.center), hitBox.size * 0.5f, transform.rotation, mask);

        for (int i = 0; i < colliders.Length; i++)
        {
            Collider aCollider = colliders[i];
            if (responder != null)
                responder.CollisionWith(aCollider);
        }

        state = colliders.Length > 0 ? ColliderState.Colliding : ColliderState.Open;
    }

    public void StartCheckingCollision()
    {
        state = ColliderState.Open;
        CheckGizmoColor();
    }
    public void StopCheckingCollision()
    {
        state = ColliderState.Closed;
        CheckGizmoColor();
    }

    public void SetResponder(IHitBoxResponder responder)
    {
        this.responder = responder;
    }
}
