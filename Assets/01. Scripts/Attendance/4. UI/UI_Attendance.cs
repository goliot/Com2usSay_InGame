using System.Collections.Generic;
using UnityEngine;

public class UI_Attendance : MonoBehaviour
{
    public List<UI_AttendanceRewardSlot> Slots;


    private void Start()
    {
        Refresh();

        AttendanceManager.Instance.OnDateChanged += Refresh;
    }

    private void Refresh()
    {
        AttendanceDTO attendance = AttendanceManager.Instance.GetAttendance(nameof(EAttendanceEventType.ThreeDay));

        int index = 0;
        foreach(var slot in Slots)
        {
            slot.Refresh(attendance.ID, index, attendance.Rewards[0]);

            index++;
        }
    }
}
