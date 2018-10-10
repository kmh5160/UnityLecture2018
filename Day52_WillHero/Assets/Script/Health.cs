using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public float currentHealth;
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

    public void DecreaseHP(float amount)
    {
        if (currentHealth <= 0f)
            return;

        currentHealth -= amount;
        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            die();
        }
        RedrawHealthBar();
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
