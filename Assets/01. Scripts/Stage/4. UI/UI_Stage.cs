using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using System;

public class UI_Stage : MonoBehaviour
{
    [Header("Stage Bar Settings")]
    public RectTransform difficultyBar;     // 전체 난이도 바 (Level.png)
    public float tweenDuration = 0.4f;      // DOTween 이동 시간
    public float haLoopResetThreshold = -400f; // 1 타일만큼 밀리면 초기화

    [Header("# Texts")]
    //public TextMeshProUGUI StageTypeText;
    public TextMeshProUGUI CurrentStageText;
    public TextMeshProUGUI TimerText;

    private void Start()
    {
        tweenDuration = StageManager.Instance.RequiredTimeForNextStage * 3;
        Refresh();

        StageManager.Instance.OnLevelChangeEvent += Refresh;

        float targetX = difficultyBar.anchoredPosition.x + haLoopResetThreshold;
        Sequence loopSequence = DOTween.Sequence();
        loopSequence.Append(
            difficultyBar.DOAnchorPosX(difficultyBar.anchoredPosition.x + haLoopResetThreshold, tweenDuration)
                .SetEase(Ease.Linear)
        );
        loopSequence.SetLoops(-1, LoopType.Restart);
    }

    private void Refresh()
    {
        StageDTO dto = StageManager.Instance.StageDto;
        // 현재 위치에서 -400만큼 이동

        //StageTypeText.text = dto.StageType.ToString();
        CurrentStageText.text = $"{dto.StageType.ToString()}\nLevel {dto.CurrentLevel.ToString()}";
        TimerText.text = GetFormattedFullGameTime();
    }

    public string GetFormattedFullGameTime()
    {
        int minutes = Mathf.FloorToInt(StageManager.Instance.FullGameTimer / 60f);
        int seconds = Mathf.FloorToInt(StageManager.Instance.FullGameTimer % 60f);
        return string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }
}
