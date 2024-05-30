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
    public PlayerController controller;

    Condition health { get { return uiCondition.health; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public event Action onTakeDamage;
    
    void Update()
    {
        stamina.Add(stamina.regenRate * Time.deltaTime);

        if (controller.curSpeed == controller.runSpeed && stamina.curValue > 0)
        {
            stamina.Subtract(20f * Time.deltaTime);
        }

        if (health.curValue == 0.0f)
        {
            Die();
        }

        if (stamina.curValue <= 0.0f)
        {
            Debug.Log("±×¸¸ ¶Ù¾î");
            controller.curSpeed = controller.moveSpeed;
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Die()
    {
        Debug.Log("Á×´Ù");
    }
    
    public void TakePhysicalDamage(int damageAmount)
    {
        health.Subtract(damageAmount);
        onTakeDamage?.Invoke();
    }

    public bool UseStamina(float amount)
    {
        if (stamina.curValue - amount < 0f)
        {
            return false;
        }

        stamina.Subtract(amount);
        return true;
    }
}
