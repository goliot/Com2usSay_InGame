using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

public class UI_AchievementNotification : MonoBehaviour
{
    [SerializeField] private Transform NotificationParent;
    [SerializeField] private GameObject NotifiationPopup;

    private Queue<AchievementDTO> _popupQueue = new Queue<AchievementDTO>();
    private bool _isShowing = false;

    private void Start()
    {
        EventManager.AddListener<NewAchievementNotificationEvent>(MakeNotification);
        EventManager.AddListener<NotificationPopupEndEvent>(ShowNotification);
    }

    private void MakeNotification(NewAchievementNotificationEvent evt)
    {
        _popupQueue.Enqueue(evt.AchievementDto);

        if (!_isShowing)
        {
            ShowNotification();
        }
    }

    private void ShowNotification(NotificationPopupEndEvent evt = null)
    {
        if (_popupQueue.Count == 0)
        {
            _isShowing = false;
            return;
        }

        _isShowing = true;

        AchievementDTO data = _popupQueue.Dequeue();
        GameObject notification = Instantiate(NotifiationPopup, NotificationParent);
        notification.GetComponent<UI_AchievementNotificationPopup>().Init(data);
    }
}
