using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : Singleton<AchievementManager>
{
    private List<Achievement> _achievements;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _achievements = new List<Achievement>
        {
            // ① 첫 수익 ─ 100골드 모으면 300골드 지급
            new Achievement(
                id: "ACH_MONEY_001",
                name: "첫 수익",
                description: "100 골드 획득",
                condition: EAchievementCondition.GoldCollect,
                goalValue: 100,
                rewardCurrencyType: ECurrencyType.Gold,
                rewardAmount: 300),

            // ② 부자 되기 1단계 ─ 누적 1 000골드 모으면 다이아 1개 지급
            new Achievement(
                id: "ACH_MONEY_002",
                name: "부자 되기 1단계",
                description: "총 1000 골드 획득",
                condition: EAchievementCondition.GoldCollect,
                goalValue: 1000,
                rewardCurrencyType: ECurrencyType.Diamond,
                rewardAmount: 1),

            // ③ 드론 헌터 ─ 드론 10마리 처치 시 골드 1 000지급
            new Achievement(
                id: "ACH_KILL_DRONE_001",
                name: "드론 헌터",
                description: "드론 타입 몬스터 10마리 처치",
                condition: EAchievementCondition.DroneKillCount,
                goalValue: 10,
                rewardCurrencyType: ECurrencyType.Gold,
                rewardAmount: 1000),

            // ④ 드론 학살자 ─ 드론 50마리(GoalValue=100 표에 맞춤) 처치 시 다이아 2개
            new Achievement(
                id: "ACH_KILL_DRONE_002",
                name: "드론 학살자",
                description: "드론 타입 몬스터 50마리 처치",
                condition: EAchievementCondition.DroneKillCount,
                goalValue: 100,         // 이미지 표 기준 값
                rewardCurrencyType: ECurrencyType.Diamond,
                rewardAmount: 2),

            // ⑤ 보스 저격수 ─ 보스 1기 격추 시 다이아 3개
            new Achievement(
                id: "ACH_KILL_BOSS_001",
                name: "보스 저격수",
                description: "보스 몬스터 1기 격추",
                condition: EAchievementCondition.BossKillCount,
                goalValue: 1,
                rewardCurrencyType: ECurrencyType.Diamond,
                rewardAmount: 3),

            // ⑥ 진득하게 ─ 플레이 누적 600초(10분) 달성 시 다이아 5개
            new Achievement(
                id: "ACH_TIME_001",
                name: "진득하게",
                description: "플레이 누적 시간 10분 달성",
                condition: EAchievementCondition.PlayTime,
                goalValue: 600,
                rewardCurrencyType: ECurrencyType.Diamond,
                rewardAmount: 5),

            // ⑦ 눈썰미 ─ 숨겨진 장소(트리거) 발견 시 다이아 10개
            new Achievement(
                id: "ACH_HIDDEN_001",
                name: "눈썰미",
                description: "특정 위치의 숨겨진 장소",
                condition: EAchievementCondition.Trigger,
                goalValue: 777,
                rewardCurrencyType: ECurrencyType.Diamond,
                rewardAmount: 10),
        };
    }
}
