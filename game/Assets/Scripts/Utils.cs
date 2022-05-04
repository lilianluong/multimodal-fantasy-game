using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SpellcastInfo
{
    public string Name { get; }
    public float Score { get; }
    public SpellEffect Effect { get; }

    public SpellcastInfo(string name, float score, SpellEffect effect)
    {
        Name = name;
        Score = score;
        Effect = effect;
    }

    public override string ToString() => $"Spellcast<{Name}, {Score}%, {Effect}>";
}

public struct SpellEffect
{
    public SpellEffectType EffectType { get; }
    public int Magnitude { get; }

    public SpellEffect(SpellEffectType effectType, float magnitude)
    {
        EffectType = effectType;
        Magnitude = Mathf.RoundToInt(magnitude);
    }

    public string LogString()
    {
        switch (EffectType)
        {
            case SpellEffectType.Damage:
                return $"Dealt {Magnitude} damage!";
            case SpellEffectType.Heal:
                return $"Healed {Magnitude} HP!";
            case SpellEffectType.Shield:
                return $"Shielded {Magnitude} incoming damage!";
            default:
                return ToString();
        }
    }

    public override string ToString() => $"{EffectType.ToString()}({Magnitude})";
}

public enum SpellEffectType { Damage, Heal, Shield };
