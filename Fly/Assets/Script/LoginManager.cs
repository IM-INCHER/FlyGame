using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public InputField email;
    public InputField password;

    public GameObject loginUI;
    public GameObject nicknameUI;

    public Text outputText;
    void Start()
    {
        FireBaseAuthManager.Instance.init();
        FireBaseAuthManager.Instance.LoginState += OnChangedState;
    }

    private void OnChangedState(bool sign)
    {
        outputText.text = sign ? "로그인 : " : "로그아웃 : ";
        outputText.text += FireBaseAuthManager.Instance.UserId;
    }

    public async void Create()
    {
        string e = email.text;
        string p = password.text;

        if(await FireBaseAuthManager.Instance.Create(e, p))
        {
            loginUI.SetActive(false);
            nicknameUI.SetActive(true);
        }
        ResetText();
    }

    public void Login()
    {
        string e = email.text;
        string p = password.text;

        FireBaseAuthManager.Instance.Login(e, p);
        ResetText();
    }

    private void ResetText()
    {
        email.text = "";
        password.text = "";
    }

    public void Logout()
    {
        FireBaseAuthManager.Instance.Logout();
        ResetText();
    }
}
