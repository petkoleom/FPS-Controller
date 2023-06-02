using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamage
{
    [SerializeField] int maxHealth = 100;
    private float health;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float _damage)
    {
        health -= _damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
