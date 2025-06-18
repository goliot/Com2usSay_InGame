using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine.SceneManagement;

public class RankingManager : Singleton<RankingManager>
{
    private Ranking _ranking;
    private RankingRepository _repository;

    private List<Ranking> _everyRankingList;
    private List<RankingDTO> _cachedSortedList;

    private bool _isDirty = true;
    private int CurrentKill = 0;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    private void Start()
    {
        EventManager.AddListener<MonsterKillEvent>(AddKillCount);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "IntroMenu")
        {
            Destroy(gameObject);
        }
    }

    private void Init()
    {
        _repository = new RankingRepository();
        _everyRankingList = new List<Ranking>();

        List<RankingDTO> loadRankings = _repository.Load()?.ConvertAll(data => new RankingDTO(data));
        if (loadRankings != null)
        {
            foreach (var data in loadRankings)
            {
                _everyRankingList.Add(new Ranking(data));
            }
        }

        _ranking = _everyRankingList.Find(data => data.Nickname == AccountManager.Instance.UserNickname);
        if (_ranking == null)
        {
            _ranking = new Ranking(AccountManager.Instance.UserNickname, 0);
            _everyRankingList.Add(_ranking);
        }

        _isDirty = true;
    }

    private void AddKillCount(MonsterKillEvent evt)
    {
        CurrentKill++;
    }

    public void SaveMyScore()
    {
        _ranking.IncreaseScore(CurrentKill);
        CurrentKill = 0;
        _isDirty = true;

        _repository.Save(_everyRankingList.ConvertAll((item) =>  new RankingDTO(item)));
    }

    public List<RankingDTO> GetSortedRankings()
    {
        if (!_isDirty && _cachedSortedList != null)
        {
            return _cachedSortedList;
        }

        _cachedSortedList = _everyRankingList.ConvertAll(item => new RankingDTO(item));
        _cachedSortedList.Sort((a, b) => b.Score.CompareTo(a.Score));
        _isDirty = false;

        return _cachedSortedList;
    }

    //RankingDTO 하고, RankIndex
    public RankingDTO GetMyRanking()
    {
        return new RankingDTO(_ranking);
    }
}
