using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_AttendanceRewardSlot : MonoBehaviour
{
    private string _attendanceID;
    private int _attendanceRewardIndex;
    private AttendanceRewardDTO _rewardDto;

    public TextMeshProUGUI RewardAmountText;
    public Image RewardTypeIcon;
    public Button RewardClaimButton;

    public void Refresh(string attendanceId, int attendanceRewardIndex, AttendanceRewardDTO attendanceReward)
    {
        _attendanceID = attendanceId;
        _attendanceRewardIndex = attendanceRewardIndex;
        _rewardDto = attendanceReward;

        RewardAmountText.text = $"{_rewardDto.Amount:N0}개";
        RewardClaimButton.enabled = !_rewardDto.CanClaim;
    }

    public void TryRewardClaim()
    {
        AttendanceManager.Instance.TryRewardClaim(_attendanceID, _attendanceRewardIndex);
    }
}