using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UI_Stage : MonoBehaviour
{
    [Header("Stage Bar Settings")]
    public RectTransform difficultyBar;     // 전체 난이도 바 (Level.png)
    public float pixelsPerStage = 133.33f;  // 스테이지 1단계당 이동 거리
    public float tweenDuration = 0.4f;      // DOTween 이동 시간

    [Header("HAHA Loop Settings")]
    public RectTransform haLoop;            // HAHAHA 반복 영역
    public float haLoopSpeed = 100f;        // px/sec
    public float haLoopResetThreshold = -400f; // 1 타일만큼 밀리면 초기화
    public float haLoopWidth = 400f;        // 한 HAHAHA 이미지 폭

    private int lastStageLevel = 0;

    private void Start()
    {
        Refresh();

        StageManager.Instance.OnLevelChangeEvent += Refresh;
    }

    private void Update()
    {
        StageDTO dto = StageManager.Instance.StageDto;

        if (dto.StageType == EStageType.Hahahahaha)
        {
            AnimateHaLoop();
        }
    }

    private void Refresh()
    {
        StageDTO dto = StageManager.Instance.StageDto;

        float targetX = -pixelsPerStage * (dto.CurrentLevel - 1);
        difficultyBar.DOAnchorPosX(targetX, tweenDuration).SetEase(Ease.OutCubic);
    }

    private void AnimateHaLoop()
    {
        Vector2 pos = haLoop.anchoredPosition;
        pos.x -= haLoopSpeed * Time.deltaTime;

        if (pos.x <= haLoopResetThreshold)
        {
            pos.x += haLoopWidth;
        }

        haLoop.anchoredPosition = pos;
    }
}
