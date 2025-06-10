using Unity.FPS.Game;
using UnityEngine;

public class CurrencyIncreaseEvent : GameEvent
{
    public ECurrencyType Type { get; }
    public int Value { get; }

    public CurrencyIncreaseEvent(ECurrencyType type, int value)
    {
        Type = type;
        Value = value;
    }
}