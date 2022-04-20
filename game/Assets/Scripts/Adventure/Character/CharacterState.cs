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

    public (float, float) TakeDamage(float baseDamage)
    {
        float actualDamage = baseDamage * character.DefenseMultiplier;
        health = Mathf.Max(0f, health - actualDamage);
        return GetHealth();
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
