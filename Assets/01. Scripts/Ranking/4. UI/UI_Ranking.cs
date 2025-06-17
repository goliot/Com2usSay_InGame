using System.Collections.Generic;
using UnityEngine;

public class UI_Ranking : MonoBehaviour
{
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private Transform _contentParent;

    private List<GameObject> _slotPool = new List<GameObject>();

    private void OnEnable()
    {
        Refresh();
    }

    public void OnCliCkRefreshButton()
    {
        Refresh();
    }

    private void Refresh()
    {
        List<RankingDTO> sortedRanking = RankingManager.Instance.GetSortedRankings();

        // 슬롯 부족하면 새로 생성
        while (_slotPool.Count < sortedRanking.Count)
        {
            GameObject newSlot = Instantiate(_slotPrefab, _contentParent);
            _slotPool.Add(newSlot);
        }

        // 슬롯 초기화
        for (int i = 0; i < _slotPool.Count; i++)
        {
            if (i < sortedRanking.Count)
            {
                _slotPool[i].SetActive(true);

                // 슬롯에 데이터 바인딩
                var uiSlot = _slotPool[i].GetComponent<UI_RankingSlot>();
                uiSlot.SetData(sortedRanking[i], i + 1); // 랭킹 번호 1부터 시작
            }
            else
            {
                _slotPool[i].SetActive(false); // 필요 없는 슬롯은 숨김
            }
        }
    }
}
