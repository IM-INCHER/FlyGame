using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    public void Create(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("ȸ���������");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("ȸ�����Խ���");
                foreach (var exception in task.Exception.InnerExceptions)
                {
                    Debug.LogError($"Error: {exception.Message}");
                }
                return;
            }

            Debug.Log("ȸ������ �Ϸ�");
        });
    }

    public void Login(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("�α��� ���");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("�α��� ����");
                return;
            }

            Debug.Log("�α��� �Ϸ�");
        });
    }

    public void Logout()
    {
        auth.SignOut();
        Debug.Log("�α׾ƿ�");
    }
}
