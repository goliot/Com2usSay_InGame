using System;

public class AttendanceReward
{
    public readonly ECurrencyType CurrencyType;
    public readonly int Amount;
    public bool IsClaimed { get; private set; }

    public AttendanceReward(ECurrencyType currencyType, int amount, bool isClaimed)
    {
        if(amount < 0)
        {
            throw new Exception("출석 보상량은 0보다 작을 수 없습니다.");
        }

        CurrencyType = currencyType;
        Amount = amount;
        IsClaimed = isClaimed;
    }

    public bool TryClaim()
    {
        if(IsClaimed)
        {
            return false;
        }

        IsClaimed = true;

        return true;
    }
}