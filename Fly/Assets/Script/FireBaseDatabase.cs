using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using UnityEngine.UI;

public class User
{
    public string username;
    public string email;

    public User()
    {
    }

    public User(string username, string email)
    {
        this.username = username;
        this.email = email;
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

    private void OnChangedState(bool sign)
    { }

    DatabaseReference reference;

    public void init()
    {
        FirebaseApp app = FirebaseApp.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void writeNewUser(string userId, string name, string email)
    {
        User user = new User(name, email);
        string json = JsonUtility.ToJson(user);

        Debug.Log(userId);
        Debug.Log(json);

        reference.Child("users/").Child(userId).SetValueAsync(json);
    }

    public void readUser(string userID)
    {
        Debug.Log(userID);

        reference.Child("users/").Child(userID).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {

            }
            else if (task.IsCompleted) 
            {
                DataSnapshot snapshot = task.Result;
                Debug.Log("value = " + snapshot.Value);
            }
        });
    }
}
