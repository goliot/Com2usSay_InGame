using Unity.FPS.Game;
using UnityEngine;

public class CurrencyIncreasedEvent : GameEvent
{
    public ECurrencyType Type { get; }
    public int Value { get; }

    public CurrencyIncreasedEvent(ECurrencyType type, int value)
    {
        Type = type;
        Value = value;
    }
}