using System.Collections.Generic;
using System;

[Serializable]
public class CurrencySaveData
{
    public ECurrencyType Type;
    public int Value;
}

[Serializable]
public class CurrencySaveDataList
{
    public List<CurrencySaveData> DataList;
}