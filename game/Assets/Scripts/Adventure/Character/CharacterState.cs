using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState
{
    private Character character;
    private float health, maxHealth;

    public CharacterState(Character character)
    {
        this.character = character;
        maxHealth = character.MaxHealth;
        health = character.MaxHealth;
    }

    public float TakeDamage(float baseDamage)
    {
        float actualDamage = baseDamage * character.DefenseMultiplier;
        health = Mathf.Max(0f, health - actualDamage);
        return actualDamage;
    }

    public float Heal(float healAmount)
    {
        health = Mathf.Min(maxHealth, health + healAmount);
        return healAmount;
    }

    public (float, float) GetHealth()
    {
        return (health, maxHealth);
    }

    public bool IsDead()
    {
        return health <= 0f;
    }
}
