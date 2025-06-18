using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_RankingSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _rankIndexText;
    [SerializeField] private TextMeshProUGUI _userIdText;
    [SerializeField] private TextMeshProUGUI _userScoreText;

    private Image _slotPanel;

    private void Awake()
    {
        _slotPanel = GetComponent<Image>();
    }

    public void SetData(RankingDTO dto, int rankIndex)
    {
        _rankIndexText.text = $"# {rankIndex.ToString()}";
        _userIdText.text = dto.Nickname;
        _userScoreText.text = dto.Score.ToString();

        if(dto.Nickname == AccountManager.Instance.UserId)
        {
            if(_slotPanel == null)
            {
                _slotPanel = GetComponent<Image>();
            }
            _slotPanel.color = Color.yellow;
        }
        else
        {
            _slotPanel.color = Color.white;
        }
    }
}
