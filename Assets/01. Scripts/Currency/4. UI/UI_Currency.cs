using TMPro;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;

public class UI_Currency : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _goldCountText;
    [SerializeField] private TextMeshProUGUI _diamondCountText;
    [SerializeField] private TextMeshProUGUI _buyHealthText;

    private void OnEnable()
    {
        EventManager.AddListener<CurrencyChangedEvent>(OnCurrencyChanged);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<CurrencyChangedEvent>(OnCurrencyChanged);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            OnClickBuyHealth();
        }
    }

    private void OnCurrencyChanged(CurrencyChangedEvent evt)
    {
        Refresh();
    }

    private void Refresh()
    {
        var gold = CurrencyManager.Instance.GetValue(ECurrencyType.Gold);
        var diamond = CurrencyManager.Instance.GetValue(ECurrencyType.Diamond);

        _goldCountText.text = $"Gold : {gold.Value}";
        _diamondCountText.text = $"Diamond : {diamond.Value}";

        _buyHealthText.color = gold.HaveEnough(300) ? Color.green : Color.red;
    }

    public void OnClickBuyHealth()
    {
        // 묻지 말고 시켜라!
        if (CurrencyManager.Instance.TryUseCurrency(ECurrencyType.Gold, 300))
        {
            Health playerHealth = FindAnyObjectByType<PlayerCharacterController>().GetComponent<Health>();
            playerHealth.Heal(100);
        }
        else
        {
            // 알림 : 돈이 부족합니다
            Debug.LogWarning("골드가 부족합니다");
        }
    }
}
