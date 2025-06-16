using System;
using System.Collections.Generic;
using System.Transactions;

public class Attendance
{
    public readonly string ID;
    public readonly DateTime StartDate; // 출석 이벤트를 언제부터 시작할 것인가 -> 패치 없이 UI를 노출하기 위함
    public int DayCount { get; private set; } // 출석 횟수
    public DateTime LastAttendanceDate { get; private set; } // 마지막 출석일

    private List<AttendanceReward> _rewards;

    public Attendance(string id, DateTime startDate, DateTime lastAttendanceDate, int dayCount)
    {
        if(string.IsNullOrEmpty(id))
        {
            throw new Exception("ID는 비어있을 수 없습니다");
        }
        if(startDate == default)
        {
            throw new Exception("출석 시작일이 지정되지 않았습니다.");
        }
        if(dayCount <= 0)
        {
            throw new Exception("출석 횟수는 0 이하일 수 없습니다.");
        }
        if (lastAttendanceDate == default)
        {
            throw new Exception("마지막 출석일이 지정되지 않았습니다.");
        }
        if (lastAttendanceDate < startDate)
        {
            throw new Exception("마지막 출석일은 시작일 이전일 수 없습니다.");
        }
        if (lastAttendanceDate > DateTime.Today)
        {
            throw new Exception("마지막 출석일은 오늘 이후일 수 없습니다.");
        }

        ID = id;
        StartDate = startDate;
        DayCount = dayCount;
        LastAttendanceDate = lastAttendanceDate;

        _rewards = new List<AttendanceReward>();
    }

    public void Check(DateTime date)
    {
        if (date == default)
        {
            throw new Exception("출석 체크 날짜가 지정되지 않았습니다.");
        }

        // 날짜가 마지막 출석일보다 이후일 경우에만 출석 인정
        if (date.Date > LastAttendanceDate.Date)
        {
            DayCount++;
            LastAttendanceDate = date.Date;
        }
    }

    public void AddRewardData(AttendanceReward reward)
    {
        if (reward == null)
        {
            throw new Exception("보상 데이터가 null입니다.");
        }

        _rewards.Add(reward);
    }

    public bool TryClaim(int day)
    {
        if(day < 0 || _rewards.Count <= day)
        {
            throw new Exception("출석 인덱스가 올바르지 않습니다.");
        }
        if(DayCount < day)
        {
            return false;
        }

        return _rewards[day - 1].TryClaim();
    }


    public AttendanceDTO ToDTO()
    {
        return new AttendanceDTO(ID, StartDate, DayCount, LastAttendanceDate, _rewards);
    }

    public AttendanceRewardDTO GetReward(int index)
    {
        return null;
    }
}