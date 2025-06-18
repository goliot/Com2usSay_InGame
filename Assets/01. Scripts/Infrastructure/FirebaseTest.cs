using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using Firebase.Firestore;
using UnityEngine;
using System.Collections.Generic;
using System;

public class FirebaseTest : MonoBehaviour
{
    private FirebaseApp _app;
    private FirebaseAuth _auth;
    private FirebaseFirestore _db;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                Debug.Log("파이어베이스 연결에 성공했습니다.");
                _app = FirebaseApp.DefaultInstance;
                _auth = FirebaseAuth.DefaultInstance;
                _db = FirebaseFirestore.DefaultInstance;

                //Register();
                Login();
            }
            else
            {
                Debug.LogError(string.Format($"파이어베이스 연결에 실패했습니다. : {dependencyStatus}"));
            }
        });
    }

    private void Register()
    {
        string email = "teemo@teemo.com";
        string password = "123456";

        _auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError($"회원가입에 실패했습니다. : {task.Exception.Message}");
                return;
            }

            // Firebase user has been created.
            AuthResult result = task.Result;
            Debug.Log($"회원가입에 성공했습니다.: {result.User.DisplayName} ({result.User.UserId})");
        });
    }

    private void Login()
    {
        string email = "teemo@teemo.com";
        string password = "123456";

        _auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError($"로그인에 실패했습니다.: {task.Exception.Message}");
                return;
            }

            AuthResult result = task.Result;
            Debug.Log($"로그인 성공 : {result.User.DisplayName} ({result.User.UserId})");
            NicknameChange();
        });
    }

    private void NicknameChange()
    {
        FirebaseUser user = _auth.CurrentUser;
        if (user == null)
        {
            return;
        }

        UserProfile profile = new UserProfile
        {
            DisplayName = "Teemo",
        };
        user.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(task => {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("닉네임 변경에 실패했습니다.: " + task.Exception);
                return;
            }

            Debug.Log("닉네임 변경에 성공했습니다.");
            //AddMyRanking();
            //GetMyRanking();
            GetRankings();
        });
    }

    private void AddMyRanking()
    {
        Account account = GetProfile();
        Ranking ranking = new Ranking(account.Email, account.Nickname, 2300);

        Dictionary<string, object> rankingDict = new Dictionary<string, object>
        {
            { "Email", ranking.Email },
            { "Nickname", ranking.Nickname },
            { "Score", ranking.Score },
        };

        /*Debug.Log("Firestore에 랭킹 데이터 추가 시도 중...");

        _db.Collection("rankings").AddAsync(rankingDict).ContinueWithOnMainThread(task =>
        {
            Debug.Log("AddAsync 콜백 진입");

            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Firestore 데이터 쓰기에 실패했습니다: " + task.Exception);
                return;
            }
            Debug.Log("데이터 추가/업데이트 성공");
        });*/

        _db.Collection("rankings").Document(ranking.Email).SetAsync(rankingDict).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Firestore 데이터 쓰기에 실패했습니다: " + task.Exception);
                return;
            }

            Debug.Log("데이터 추가/업데이트 성공");
        });
    }

    private void GetMyRanking()
    {
        string email = "teemo@teemo.com"; // ID 역할

        DocumentReference docRef = _db.Collection("rankings").Document(email);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            var snapshot = task.Result;

            if (snapshot.Exists)
            {
                Debug.Log(String.Format("Document data for {0} document:", snapshot.Id));
                var rankingDict = snapshot.ToDictionary();
                foreach (var pair in rankingDict)
                {
                    Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
                }
            }
            else
            {
                Debug.Log(String.Format("Document {0} does not exist!", snapshot.Id));
            }
        });
    }

    private void GetRankings()
    {
        Query allRankingsQuery = _db.Collection("rankings").OrderByDescending("Score");
        allRankingsQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot allCitiesQuerySnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in allCitiesQuerySnapshot.Documents)
            {
                Debug.Log(String.Format("Document data for {0} document:", documentSnapshot.Id));
                Dictionary<string, object> city = documentSnapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in city)
                {
                    Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
                }

                // Newline to separate entries
                Debug.Log("");
            }
        });
    }

    private Account GetProfile()
    {
        FirebaseUser user = _auth.CurrentUser;
        if(user == null)
        {
            return null;
        }

        string nickname = user.DisplayName;
        string email = user.Email;

        Account account = new Account(email, nickname, "firebase");

        return account;
    }
}
