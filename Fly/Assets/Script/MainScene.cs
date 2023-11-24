using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    public string NickName;

    void Start()
    {
        Debug.Log(FireBaseAuthManager.Instance.UserId);
        FireBaseDatabase.Instance.readUser(FireBaseAuthManager.Instance.UserId.ToString());

        NickName = PlayerPrefs.GetString("username");
        Debug.Log("로그인한 유저의 닉네임은 = " + NickName);
    }

    void Update()
    {
        NickName = PlayerPrefs.GetString("username");
        Debug.Log("로그인한 유저의 닉네임은 = " + NickName);
    }
}
