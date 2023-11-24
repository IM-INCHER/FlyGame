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
        Debug.Log("�α����� ������ �г����� = " + NickName);
    }

    void Update()
    {
        NickName = PlayerPrefs.GetString("username");
        Debug.Log("�α����� ������ �г����� = " + NickName);
    }
}
