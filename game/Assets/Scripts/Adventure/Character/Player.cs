using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public override string Name => "Player";
    public override float MaxHealth => 100f;
    public override float DefenseMultiplier => 1f;
}
