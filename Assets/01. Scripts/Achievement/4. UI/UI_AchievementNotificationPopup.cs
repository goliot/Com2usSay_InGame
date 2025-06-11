using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Unity.FPS.Game;

public class UI_AchievementNotificationPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private Image _background;

    private void OnEnable()
    {
        SetAlpha(1f);

        _description.DOFade(0f, 3f)
            .SetDelay(3f);

        _background.DOFade(0f, 3f)
            .SetDelay(3f)
            .OnComplete(() =>
            {
                EventManager.Broadcast(Events.NotificationPopupEndEvent);
                Destroy(gameObject);
            });
    }

    public void Init(AchievementDTO achievementDto)
    {
        _description.text = $"업적 달성! : {achievementDto.Name}";
    }

    private void SetAlpha(float alpha)
    {
        Color bgColor = _background.color;
        bgColor.a = alpha;
        _background.color = bgColor;

        Color textColor = _description.color;
        textColor.a = alpha;
        _description.color = textColor;
    }
}