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
        CurrentStageText.text = dto.CurrentLevel.ToString();
        TimerText.text = StageManager.Instance.Timer.ToString();
    }
}
