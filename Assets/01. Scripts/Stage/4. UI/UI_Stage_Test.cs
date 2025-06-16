using System;
using TMPro;
using UnityEngine;

public class UI_Stage_Test : MonoBehaviour
{
    public TextMeshProUGUI StageTypeText;
    public TextMeshProUGUI CurrentStageText;
    public TextMeshProUGUI TimerText;

    private void Start()
    {
        Refresh();

        StageManager.Instance.OnLevelChangeEvent += Refresh;
    }

    private void Refresh()
    {
        StageDTO dto = StageManager.Instance.StageDto;

        StageTypeText.text = dto.StageType.ToString();
        CurrentStageText.text = $"Stage {dto.CurrentLevel.ToString()}";
        TimerText.text = GetFormattedFullGameTime(); ;
    }

    private string GetFormattedFullGameTime()
    {
        TimeSpan time = TimeSpan.FromSeconds(StageManager.Instance.FullGameTimer);
        return string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);
    }
}
