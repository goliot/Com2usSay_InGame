using UnityEngine;
using System.Collections.Generic;
using Unity.FPS.Game;
using System.Linq;

public class UI_Achievement : MonoBehaviour
{
    [SerializeField] private List<UI_AchievementSlot> _slots;

    private void Start()
    {
        GetSlots();
        Refresh();
        EventManager.AddListener<AchievementDataChangedEvent>(Refresh);
    }

    private void GetSlots()
    {
        UI_AchievementSlot[] slots = FindObjectsByType<UI_AchievementSlot>(FindObjectsSortMode.None);
        _slots = slots.ToList<UI_AchievementSlot>();
    }

    private void Refresh()
    {
        List<AchievementDTO> achievements = AchievementManager.Instance.Achievements;

        for (int i = 0; i < achievements.Count; ++i)
        {
            _slots[i].Refresh(achievements[i]);
        }
    }

    private void Refresh(AchievementDataChangedEvent evt)
    {
        List<AchievementDTO> achievements = AchievementManager.Instance.Achievements;

        for(int i=0; i<achievements.Count; ++i)
        {
            _slots[i].Refresh(achievements[i]);
        }
    }
}
