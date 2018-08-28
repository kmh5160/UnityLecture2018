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
    public Collider[] hitBoxes;
    public Color inactiveColor;
    public Color collisionOpenColor;
    public Color collidingColor;

    public ColliderState state = ColliderState.Closed;
    IHitBoxResponder responder = null;
    List<Collider> colliderList;

    private void Awake()
    {
        colliderList = new List<Collider>();
    }

    private void OnDrawGizmos()
    {
        //UpdateHitBox();
        CheckGizmoColor();
        Gizmos.matrix = transform.localToWorldMatrix;
        foreach (var c in hitBoxes)
        {
            if (c.GetType() == typeof(BoxCollider))
            {
                BoxCollider bc = (BoxCollider)c;
                Gizmos.DrawCube(bc.center, bc.size);
            }
            if (c.GetType() == typeof(SphereCollider))
            {
                SphereCollider sc = (SphereCollider)c;
                Gizmos.DrawSphere(sc.center, sc.radius);
            }
        }
    }

    private void CheckGizmoColor()
    {
        switch (state)
        {
            case ColliderState.Closed:
                Gizmos.color = inactiveColor;
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
        colliderList.Clear();

        if (state == ColliderState.Closed)
            return;

        foreach(var c in hitBoxes)
        {
            if (c.GetType() == typeof(BoxCollider))
            {
                BoxCollider bc = (BoxCollider)c;
                Collider[] colliders = Physics.OverlapBox(transform.TransformPoint(bc.center), bc.size * 0.5f, transform.rotation, mask);
                colliderList.AddRange(colliders);
            }
            if (c.GetType() == typeof(SphereCollider))
            {
                SphereCollider sc = (SphereCollider)c;
                Collider[] colliders = Physics.OverlapSphere(transform.TransformPoint(sc.center), sc.radius, mask);
                colliderList.AddRange(colliders);
            }
        }        

        foreach(var c in colliderList)
        {
            // C# 6.0: responder?.collisionWith(c);
            if (responder != null)
                responder.CollisionWith(c);
        }

        state = colliderList.Count > 0 ? ColliderState.Colliding : ColliderState.Open;
    }

    public void StartCheckingCollision()
    {
        state = ColliderState.Open;
    }
    public void StopCheckingCollision()
    {
        state = ColliderState.Closed;
    }

    public void SetResponder(IHitBoxResponder responder)
    {
        this.responder = responder;
    }
}
