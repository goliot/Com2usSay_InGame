using System;

public class Attendance
{
    public DateTime StartDate; // 출석 이벤트를 언제부터 시작할 것인가 -> 패치 없이 UI를 노출하기 위함
    public int DayCount; // 출석 횟수
    public DateTime LastAttendanceDate; // 마지막 출석일

    public Attendance(DateTime startDate, int dayCount, DateTime lastAttendanceDate)
    {
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

        StartDate = startDate;
        DayCount = dayCount;
        LastAttendanceDate = lastAttendanceDate;
    }
}