using System;

[Serializable]
public class AttendanceRewardDTO
{
    public readonly ECurrencyType CurrencyType;
    public readonly int Amount;
    public readonly bool IsClaimed;
    public readonly bool CanClaim;

    public AttendanceRewardDTO(ECurrencyType currencyType, int amount, bool isClaimed, bool canClaim)
    {
        if (amount < 0)
        {
            throw new Exception("출석 보상량은 0보다 작을 수 없습니다.");
        }

        CurrencyType = currencyType;
        Amount = amount;
        IsClaimed = isClaimed;
        CanClaim = canClaim;
        CanClaim = canClaim;
    }

    public AttendanceRewardDTO(AttendanceReward reward)
    {
        CurrencyType = reward.CurrencyType;
        Amount = reward.Amount;
        IsClaimed = reward.IsClaimed;
    }

    public AttendanceReward ToDomain()
    {
        return new AttendanceReward(CurrencyType, Amount, IsClaimed);
    }
}
