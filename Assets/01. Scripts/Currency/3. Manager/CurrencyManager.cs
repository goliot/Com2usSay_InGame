using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.FPS.Game;

public class CurrencyManager : Singleton<CurrencyManager>
{
    private CurrencyChangedEvent _currencyChangedEvent = new CurrencyChangedEvent();
    private Dictionary<ECurrencyType, Currency> _currencies;
    private CurrencyRepository _repository;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        EventManager.AddListener<CurrencyIncreaseEvent>(AddCurrency);
    }

    private void Init()
    {
        _repository = new CurrencyRepository();
        _currencies = new Dictionary<ECurrencyType, Currency>((int)ECurrencyType.Count);

        List<CurrencyDTO> loadCurrencies = _repository.Load();

        if (loadCurrencies != null)
        {
            foreach (var data in loadCurrencies)
            {
                _currencies[data.Type] = new Currency(data.Type, data.Value);
            }
        }

        foreach (ECurrencyType type in Enum.GetValues(typeof(ECurrencyType)))
        {
            if (type == ECurrencyType.Count || _currencies.ContainsKey(type))
            {
                continue;
            }

            _currencies.Add(type, new Currency(type, 0));
        }

        EventManager.Broadcast(_currencyChangedEvent);
    }

    private List<CurrencyDTO> ToDtoList()
    {
        return _currencies.Values.ToList().ConvertAll(currency => new CurrencyDTO(currency));
    }

    public CurrencyDTO GetValue(ECurrencyType type)
    {
        return new CurrencyDTO(_currencies[type]);
    }

    private void AddCurrency(CurrencyIncreaseEvent evt)
    {
        AddCurrency(evt.Type, evt.Value);
    }

    public void AddCurrency(ECurrencyType type, int value)
    {
        _currencies[type].Add(value);
        Debug.Log($"{type} : {_currencies[type].Value}");

        _repository.Save(ToDtoList());

        EventManager.Broadcast(_currencyChangedEvent);
    }

    public bool TryUseCurrency(ECurrencyType type, int value)
    {
        if (!_currencies[type].TryUse(value))
            return false;

        _repository.Save(ToDtoList());

        EventManager.Broadcast(_currencyChangedEvent);
        return true;
    }
}