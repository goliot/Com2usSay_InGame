using UnityEngine;
using System;

namespace Unity.FPS.UI
{
    public class RankingPopUP : MonoBehaviour
    {
        public GameObject RankingPanel;

        private void Start()
        {
            if (RankingPanel == null) throw new Exception("RankingPanel is Invaild");
        }

        public void OnRankingButtonClick()
        {
            RankingPanel.SetActive(true);
        }

        public void OnExitButtonClick()
        {
            RankingPanel.SetActive(false);
        }
    }
}
