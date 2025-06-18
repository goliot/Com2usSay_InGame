using System.Collections.Generic;
using UnityEngine;

public class UI_Ranking_Lecture : MonoBehaviour
{
    public List<UI_RankingSlot_Lecture> RankingSlots;
    public UI_RankingSlot_Lecture MyRankingSlot;

    public void Refresh()
    {
        var rankings = RankingManager_Lecture.Instance.Rankings;

        int index = 0;
        foreach(var slot in RankingSlots)
        {
            slot.Refresh(rankings[index]);
            index++;
        }

        MyRankingSlot.Refresh(RankingManager_Lecture.Instance.MyRanking);
    }
}
