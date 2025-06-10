using System;
using TMPro;
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

    private AchievementDTO _achievementDto;

    public void Refresh(AchievementDTO achievementDto)
    {
        _achievementDto = achievementDto;

        _nameText.text = achievementDto.Name;
        _descriptionText.text = achievementDto.Description;
        _rewardAmountText.text = achievementDto.RewardAmount.ToString();
        _progressSlider.value = achievementDto.CurrentValue / achievementDto.GoalValue;
        _progressText.text = $"{achievementDto.CurrentValue} / {achievementDto.GoalValue}";
        _rewardAmountText.text = string.Empty;
        _getRewardButton.interactable = achievementDto.CanClaimReward();
    }

    public void OnClickClaimRewardButton()
    {
        Debug.Log("업적받기");
        if(AchievementManager.Instance.TryClaimReward(_achievementDto))
        {
            _rewardClaimDateText.text = DateTime.Now.ToString("yyyy.MM.dd");
        }
    }
}