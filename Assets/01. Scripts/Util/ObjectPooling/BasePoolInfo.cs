using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BasePoolInfo<TEnum> where TEnum : Enum
{
    public TEnum Type;
    public int InitCount;
    public GameObject Prefab;
    public Transform Container;

    public Queue<GameObject> PoolQueue = new Queue<GameObject>();
}