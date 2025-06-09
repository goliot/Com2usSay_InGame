using UnityEngine;
using System;

public class Currency
{
    // 화폐 '도메인' (콘텐츠, 지식, 문제, 기획서를 바탕으로 작성 : 기획자랑 말이 통해야 한다)
    private ECurrencyType _type;
    public ECurrencyType Type => _type;

    private int _value = 0;
    public int Value => _value;

    public Currency(ECurrencyType type, int value)
    {
        if (value < 0)
        {
            throw new Exception("value는 0보다 작을 수 없습니다.");
        }

        _type = type;
        _value = value;
    }

    public void Add(int addedValue)
    {
        if (addedValue < 0)
        {
            throw new Exception("추가 값은 0보다 작을 수 없습니다.");
        }

        _value += addedValue;
    }

    public bool TryUse(int value)
    {
        if (value < 0 || _value < value)
        {
            return false;
        }

        _value -= value;

        return true;
    }
}
