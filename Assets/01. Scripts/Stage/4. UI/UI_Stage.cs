using UnityEngine;

public class UI_Stage : MonoBehaviour
{
    private void Start()
    {
        Refresh();

        StageManager.Instance.OnLevelChangeEvent += Refresh;
    }

    private void Refresh()
    {
        StageDTO dto = StageManager.Instance.StageDto;


    }
}
