using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyManager
{
    public static string lastEnemy = "Minotaur";
    public static Character GetRandomEnemy()
    {
        Character enemy;
        do
        {
            float rand = Random.value;
            if (rand < 0.45f) enemy = new Werewolf();
            else if (rand < 0.7f) enemy = new Orc();
            else if (rand < 0.95f) enemy = new Goblin();
            else enemy = new Minotaur();
        } while (enemy.Name == lastEnemy);
        lastEnemy = enemy.Name;
        return enemy;
    }
}


public class Werewolf : Character
{
    public override string Name => "Werewolf";
    public override float MaxHealth => 100f;
    public override float AttackDamage => 12f;
    // public override float DefenseMultiplier => 1f;
    // public override float PlayerTurnTime => playerTurnTime;
}

public class Orc : Character
{
    public override string Name => "Orc";
    public override float MaxHealth => 70f;
    public override float AttackDamage => 13f;
    // public override float DefenseMultiplier => 1f;
    // public override float PlayerTurnTime => playerTurnTime;
}

public class Goblin : Character
{
    public override string Name => "Goblin";
    public override float MaxHealth => 120f;
    public override float AttackDamage => 10f;
    // public override float DefenseMultiplier => 1f;
    // public override float PlayerTurnTime => playerTurnTime;
}

public class Minotaur : Character
{
    public override string Name => "Minotaur";
    public override float MaxHealth => 140f;
    public override float AttackDamage => 14f;
    // public override float DefenseMultiplier => 1f;
    // public override float PlayerTurnTime => playerTurnTime;
}

