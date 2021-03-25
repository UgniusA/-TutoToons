using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour, IDamageable
{

    public Image healthBar;
    public float health;
    public float MaxHealth;


    private void Start()
    {
        UpdateBarHealthUI();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, MaxHealth);
        if (health <= 0)
        {
            Debug.Log("Dead");
        }
        UpdateBarHealthUI();
    }

    void UpdateBarHealthUI()
    {
        healthBar.fillAmount = health / MaxHealth;
    }
}
