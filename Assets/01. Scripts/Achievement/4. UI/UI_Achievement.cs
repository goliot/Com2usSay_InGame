using UnityEngine;
using System.Collections.Generic;
using Unity.FPS.Game;

public class UI_Achievement : MonoBehaviour
{
    [SerializeField] private List<UI_AchievementSlot> _slots;

    private void Start()
    {
        EventManager.AddListener<OnAchievementDataChangedEvent>(Refresh);
        Refresh();
    }

    private void Refresh()
    {
        List<AchievementDTO> achievements = AchievementManager.Instance.Achievements;

        for (int i = 0; i < achievements.Count; ++i)
        {
            _slots[i].Refresh(achievements[i]);
        }
    }

    private void Refresh(OnAchievementDataChangedEvent evt)
    {
        List<AchievementDTO> achievements = AchievementManager.Instance.Achievements;

        for(int i=0; i<achievements.Count; ++i)
        {
            _slots[i].Refresh(achievements[i]);
        }
    }
}
