using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class Health : NetworkBehaviour
{
    public int maxHealth = 100;
    [SyncVar(hook ="OnChangeHealth")]
    public float currentHealth;
    public bool destroyOnDeath;

    public RectTransform healthBar;
    public event System.Action OnDeath;

    private void Start()
    {
        RedrawHealthBar();
    }

    void RedrawHealthBar()
    {
        healthBar.localScale = new Vector3(currentHealth / maxHealth, 1f, 1f);
    }

    void OnChangeHealth(float health)
    {
        currentHealth = health;
        RedrawHealthBar();
    }

    public void DecreaseHP(float amount)
    {
        if (!isServer)
            return;

        //if (currentHealth <= 0f)
        //    return;

        currentHealth -= amount;
        if (currentHealth <= 0f)
        {
            if (destroyOnDeath)
                Destroy(gameObject);
            else
            {
                currentHealth = maxHealth;
                RpcRespawn();
            }
            //die();
        }
        RedrawHealthBar();
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
            transform.position = Vector3.zero;
    }

    public void IncreaseHP(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        RedrawHealthBar();
    }

    [ContextMenu("Self Destruct")]
    public void die() {
        if (OnDeath != null)
            OnDeath();
        //Destroy(gameObject);
    }
}
