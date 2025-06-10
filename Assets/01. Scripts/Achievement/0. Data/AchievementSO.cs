using UnityEngine;

[CreateAssetMenu(fileName = "AchievementSO", menuName = "Scriptable Objects/AchievementSO")]
public class AchievementSO : ScriptableObject
{
    [Header("# 기본 정보")]
    [SerializeField] private string _id;
    public string ID => _id;

    [SerializeField] private string _name;
    public string Name => _name;

    [SerializeField] private string _description;
    public string Description => _description;

    [Header("# 달성 조건")]
    [SerializeField] private EAchievementCondition _condition;
    public EAchievementCondition Condition => _condition;

    [SerializeField] private int _goalValue;
    public int GoalValue => _goalValue;

    [Header("# 보상")]
    [SerializeField] private ECurrencyType _rewardCurrencyType;
    public ECurrencyType RewardCurrencyType => _rewardCurrencyType;

    [SerializeField] private int _rewardAmount;
    public int RewardAmount => _rewardAmount;
}