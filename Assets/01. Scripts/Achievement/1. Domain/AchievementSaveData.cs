using System.Collections.Generic;

[System.Serializable]
public class AchievementSaveData
{
    public string ID;
    public int CurrentValue;
    public bool IsRewardClaimed;
}

[System.Serializable]
public class AchievementSaveDataList
{
    public List<AchievementSaveData> DataList;
}