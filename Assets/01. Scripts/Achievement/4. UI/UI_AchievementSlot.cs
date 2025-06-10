using System;
using TMPro;
using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.UI;

public class UI_AchievementSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _rewardAmountText;
    [SerializeField] private Slider _progressSlider;
    [SerializeField] private TextMeshProUGUI _progressText;
    [SerializeField] private TextMeshProUGUI _rewardClaimDateText;
    [SerializeField] private Button _getRewardButton;

    public void Refresh(AchievementDTO achievement)
    {
        _nameText.text = achievement.Name;
        _descriptionText.text = achievement.Description;
        _rewardAmountText.text = achievement.RewardAmount.ToString();
        _progressSlider.value = achievement.CurrentValue / achievement.GoalValue;
        _progressText.text = $"{achievement.CurrentValue} / {achievement.GoalValue}";
        _rewardAmountText.text = string.Empty;
        if (!achievement.IsRewardClaimed && achievement.CurrentValue >= achievement.GoalValue)
        {
            _getRewardButton.interactable = true;
        }
        else
        {
            _getRewardButton.interactable = false;
        }
    }

    public void OnClickGetButton()
    {
        _rewardClaimDateText.text = DateTime.Now.ToString("yyyy.MM.dd");
        _getRewardButton.interactable = false;
        //EventManager.Broadcast(new CurrencyIncreasedEvent(ECurrencyType.Gold, (int)_rewardAmountText));
    }
}
