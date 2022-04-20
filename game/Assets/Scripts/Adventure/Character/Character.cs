using System.Collections;
using System.Collections.Generic;

public abstract class Character
{
    public virtual string Name => "Character";
    public virtual float MaxHealth => 100f;
    public virtual float AttackDamage => 10f;
    public virtual float DefenseMultiplier => 1f;
    public virtual float PlayerTurnTime => 2.5f;
}


public class GenericCharacter : Character
{
    private string name;
    private float maxHealth, attackDamage, defenseMultiplier, playerTurnTime;

    public GenericCharacter(string name, float maxHealth, float attackDamage, float defenseMultiplier, float playerTurnTime)
    {
        this.name = name;
        this.maxHealth = maxHealth;
        this.attackDamage = attackDamage;
        this.defenseMultiplier = defenseMultiplier;
        this.playerTurnTime = playerTurnTime;
    }

    public override string Name => name;
    public override float MaxHealth => maxHealth;
    public override float AttackDamage => attackDamage;
    public override float DefenseMultiplier => defenseMultiplier;
    public override float PlayerTurnTime => playerTurnTime;
}

