using TMPro;
using UnityEngine;

public class UI_RankingSlot_Lecture : MonoBehaviour
{
    public TextMeshProUGUI RankTextUI;
    public TextMeshProUGUI NicknameTextUI;
    public TextMeshProUGUI ScoreTextUI;

    public void Refresh(RankingDTO_Lecture ranking)
    {
        RankTextUI.text = ranking.Rank.ToString("N1");
        NicknameTextUI.text = ranking.Nickname;
        ScoreTextUI.text = ranking.Score.ToString("N1");
    }
}