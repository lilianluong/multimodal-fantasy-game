using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollTurnResponse
{
    public float timeRemaining { get; set; }
    public string spellCast { get; set; }
    public float score { get; set; }

    public override string ToString()
    {
        if (timeRemaining > 0) return $"Poll<Seconds Left: {timeRemaining}>";
        if (spellCast.Length == 0) return "Turn<No Spell>";
        return $"Turn<{spellCast}, {score}>";
    }
}
