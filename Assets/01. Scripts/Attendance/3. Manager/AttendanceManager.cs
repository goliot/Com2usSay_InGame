using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttendanceManager : Singleton<AttendanceManager>
{
    public event Action OnDateChanged;

    [SerializeField] private List<AttendanceSO> _attendanceSOList;
    private List<Attendance> _attendances;
    public List<AttendanceDTO> AttendanceDTOs;

    protected override void Awake()
    {
        base.Awake();

        Init();
    }

    private void Init()
    {
        _attendances = new List<Attendance>(_attendanceSOList.Count);

        DateTime today = DateTime.Today;

        foreach(var attendanceSO in _attendanceSOList)
        {
            if(attendanceSO.StartDate < today) // 아직 시작한 이벤트가 아니라면 건너뛰기
            {
                continue;
            }

            Attendance attendance = new Attendance(attendanceSO.ID, attendanceSO.StartDate, DateTime.Today, 1);
            foreach(var attendanceRewardSO in attendanceSO.AttendanceRewards)
            {
                attendance.AddRewardData(new AttendanceReward(attendanceRewardSO.CurrencyType, attendanceRewardSO.Amount, false));
            }
        }

        StartCoroutine(CoCheck());
    }

    public AttendanceDTO GetAttendance(int index)
    {
        if(index < 0 || _attendances.Count <= index)
        {
            throw new ArgumentOutOfRangeException("출석 이벤트 인덱스가 범위를 벗어났습니다.");
        }
        return _attendances[index].ToDTO();
    }

    public AttendanceDTO GetAttendance(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new Exception("아이디가 비어있습니다.");
        }

        var attendance = FindByID(id);

        if (attendance == null)
        {
            throw new Exception($"해당 ID({id})에 대한 출석이벤트 정보가 없습니다.");
        }

        return attendance.ToDTO();
    }

    public Attendance FindByID(string id)
    {
        return _attendances.Find(item => item.ID == id);
    }

    public bool TryRewardClaim(string attendanceId, int index)
    {
        var attendance = FindByID(attendanceId);
        if (attendance == null)
        {
            return false;
        }

        if (attendance.TryClaim(index))
        {
            AttendanceRewardDTO reward = attendance.GetReward(index);
            CurrencyManager.Instance.AddCurrency(reward.CurrencyType, reward.Amount);
            OnDateChanged?.Invoke();
            return true;
        }

        return false;
    }

    private IEnumerator CoCheck()
    {
        var hourTimeWait = new WaitForSecondsRealtime(60 * 60);

        while (true)
        {
            DateTime today = DateTime.Today;

            foreach (Attendance attendance in _attendances)
            {
                attendance.Check(today);
            }

            OnDateChanged?.Invoke();

            yield return hourTimeWait;
        }
    }
}