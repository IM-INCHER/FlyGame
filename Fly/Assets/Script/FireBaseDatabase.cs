using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class User
{
    public string ID;
    public string username;
    public string email;

    public User()
    {
    }

    public User(string iD, string username, string email)
    {
        this.ID = iD;    
        this.username = username;
        this.email = email;
    }
}

public class Rank
{
    public string ID;
    public string username;
    public int score;

    public Rank()
    {
    }

    public Rank(string id, string username, int score)
    {
        ID = id;
        this.username = username;
        this.score = score;
    }
}

public class FireBaseDatabase
{
    private static FireBaseDatabase instance = null;

    public static FireBaseDatabase Instance
    {
        get
        {
            if (instance == null) instance = new FireBaseDatabase();
            return instance;
        }
    }

    public User loginUser;
    public List<Rank> rankings;

    private void OnChangedState(bool sign)
    { }

    DatabaseReference reference;

    public void init()
    {
        FirebaseApp app = FirebaseApp.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        rankings = new List<Rank>();
    }

    public void writeNewUser(string userId, string name, string email)
    {
        User user = new User(userId, name, email);
        string json = JsonUtility.ToJson(user);

        Debug.Log(json);

        reference.Child("users/").Child(userId).SetValueAsync(json);
    }

    public void writeNewRank(string userId, string name, int scroe)
    {
        Rank rank = new Rank(userId, name, scroe);
        string json = JsonUtility.ToJson(rank);

        Debug.Log(json);

        string key = reference.Child("rank").Push().Key;
        reference.Child("Ranking/").Child(key).SetValueAsync(json);
    }

    public async Task<bool> readUser(string userID)
    {
        var task = reference.Child("users/").Child(userID).GetValueAsync();

        try
        {
            await task;

            DataSnapshot snapshot = task.Result;
            Debug.Log("value = " + snapshot.Value);

            string json = snapshot.Value.ToString();
            loginUser = JsonUtility.FromJson<User>(json);

            return true;
        }
        catch (System.Exception ex)
        {
            foreach (var exception in task.Exception.InnerExceptions)
            {
                Debug.LogError(ex.Message);
            }
            return false; // 회원가입 실패
        }
    }

    public async Task<bool> ReadRanking()
    {
        var task = reference.Child("Ranking/").GetValueAsync();

        if (rankings != null) rankings.Clear();

        try
        {
            await task;

            DataSnapshot snapshot = task.Result;

            Debug.Log(snapshot.ChildrenCount);
            foreach (DataSnapshot data in snapshot.Children)
            {
                Debug.Log(data);

                string json = data.Value.ToString();
                Rank rank = JsonUtility.FromJson<Rank>(json);
                rankings.Add(rank);
            }

            rankings.Sort(delegate (Rank a, Rank b)
            {
                if (a.score < b.score) return 1;
                else if (a.score > b.score) return -1;
                return 0;
            });

            return true;
        }
        catch (System.Exception ex)
        {
            foreach (var exception in task.Exception.InnerExceptions)
            {
                Debug.LogError(ex.Message);
            }
            return false;
        }
    }

    //public void ReadRanking()
    //{
    //    //if (rankings != null) rankings.Clear();

    //    //rankings = new List<Rank>();

    //    reference.Child("Ranking/").GetValueAsync().ContinueWith(task =>
    //    {
    //        if (task.IsFaulted)
    //        {
    //            //에러 데이터로드 실패 시 다시 데이터 로드
    //            Debug.Log("실패");
    //        }
    //        else if (task.IsCompleted)
    //        {
    //            DataSnapshot snapshot = task.Result;

    //            Debug.Log(snapshot.ChildrenCount);
    //            foreach (DataSnapshot data in snapshot.Children)
    //            {
    //                Debug.Log(data);

    //                string json = data.Value.ToString();
    //                Rank rank = JsonUtility.FromJson<Rank>(json);
    //                rankings.Add(rank);
    //            }

    //            rankings.Sort(delegate (Rank a, Rank b)
    //            {
    //                if (a.score < b.score) return 1;
    //                else if(a.score > b.score) return -1;
    //                return 0;
    //            });

    //            isLoad = true;
    //        }
    //    });
    //}
}


