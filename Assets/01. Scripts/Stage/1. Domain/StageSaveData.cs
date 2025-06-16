public class StageSaveData
{
    public string UserId;
    public int CurrentLevel;

    public StageSaveData(int currentLevel)
    {
        CurrentLevel = currentLevel;
    }

    public StageSaveData(StageDTO dto)
    {
        CurrentLevel = dto.CurrentLevel;
    }
}