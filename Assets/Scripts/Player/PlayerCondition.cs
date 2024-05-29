using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakePhysicalDamage(int damageAmount);
}

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public event Action onTakeDamage;
    
    void Update()
    {
        stamina.Add(stamina.regenRate * Time.deltaTime);

        if (health.curValue == 0.0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Die()
    {
        Debug.Log("ав╢ы");
    }
    
    public void TakePhysicalDamage(int damageAmount)
    {
        onTakeDamage?.Invoke();
    }
}
