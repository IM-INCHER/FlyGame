using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using UnityEditor.Networking;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheetsManager : MonoBehaviour
{
    const string URL = "https://script.google.com/macros/s/AKfycbwIRagKxtc-1Ad-p7SWjrUKB5gN_rNukIgcTtIHVPRzC1RztkhO1B31lwyDP2heDvWcdA/exec";

    public void LogIn()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "log");
        form.AddField("email", FireBaseDatabase.Instance.loginUser.email);
        form.AddField("name", FireBaseDatabase.Instance.loginUser.username);
        form.AddField("time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt"));
        form.AddField("state", "로그인");

        StartCoroutine(Post(form));
    }

    public void LogOut()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "log");
        form.AddField("email", FireBaseDatabase.Instance.loginUser.email);
        form.AddField("name", FireBaseDatabase.Instance.loginUser.username);
        form.AddField("time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt"));
        form.AddField("state", "로그아웃");

        StartCoroutine(Post(form));
    }

    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();

            if (www.isDone) print(www.downloadHandler.text);
            else print("웹의 응답이 없습니다.");
        }
    }

}
