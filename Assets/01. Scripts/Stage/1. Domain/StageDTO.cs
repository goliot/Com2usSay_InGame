public class StageDTO
{
    public EStageType StageType { get; private set; }
    public int CurrentLevel { get; private set; }

    public StageDTO(EStageType stageType, int currentLevel)
    {
        StageType = stageType;
        CurrentLevel = currentLevel;
    }

    public StageDTO(Stage stage)
    {
        StageType = stage.StageType;
        CurrentLevel = stage.CurrentLevel;
    }
}