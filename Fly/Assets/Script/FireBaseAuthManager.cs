using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using UnityEditor.VersionControl;

public class FireBaseAuthManager
{
    private static FireBaseAuthManager instance = null;

    public static FireBaseAuthManager Instance
    {
        get
        {
            if (instance == null) instance = new FireBaseAuthManager();
            return instance;
        }
    }

    private FirebaseAuth auth;
    private FirebaseUser user;

    public Action<bool> LoginState;

    public string UserId => user.UserId;

    public void init()
    {
        auth = FirebaseAuth.DefaultInstance;
        if (auth.CurrentUser != null) Logout();

        auth.StateChanged += OnChanged;
    }

    private void OnChanged(object sender, EventArgs e)
    {
        if (auth.CurrentUser != user)
        {
            bool signed = (auth.CurrentUser != user && auth.CurrentUser != null);
            if (!signed && user != null)
            {
                Debug.Log("�α׾ƿ�");
                LoginState?.Invoke(false);
            }

            user = auth.CurrentUser;
            if (signed)
            {
                Debug.Log("�α���");
                LoginState?.Invoke(true);
            }

        }
    }

    public async Task<bool> Create(string email, string password)
    {
        var task = auth.CreateUserWithEmailAndPasswordAsync(email, password);

        try
        {
            await task;
            Debug.Log("ȸ������ �Ϸ�");
            return true; // ȸ������ ����
        }
        catch (System.Exception ex)
        {
            foreach (var exception in task.Exception.InnerExceptions)
            {
                Debug.LogError(ex.Message);
            }
            Debug.LogError("ȸ������ ����");
            return false; // ȸ������ ����
        }
    }

    //public void Login(string email, string password)
    //{
    //    auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
    //    {
    //        if (task.IsCanceled)
    //        {
    //            Debug.LogError("�α��� ���");
    //            return;
    //        }
    //        if (task.IsFaulted)
    //        {
    //            Debug.LogError("�α��� ����");
    //            return;
    //        }
    //        Debug.Log("�α��� �Ϸ�");
    //    });
    //}

    public async Task<bool> Login(string email, string password)
    {
        var task = auth.SignInWithEmailAndPasswordAsync(email, password);

        try
        {
            await task;
            Debug.Log("�α��� �Ϸ�");
            return true; // �α��� ����
        }
        catch (System.Exception ex)
        {
            if (task.IsCanceled)
            {
                Debug.LogError("�α��� ���");
                return false;
            }
            else
            {
                Debug.LogError("�α��� ����");
                return false;
            }
        }
    }

    public void Logout()
    {
        auth.SignOut();
        Debug.Log("�α׾ƿ�");
    }
}
