using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public InputField email;
    public InputField password;
    public InputField NickName;

    public GameObject loginUI;
    public GameObject nicknameUI;

    public GoogleSheetsManager GoogleSheet;

    public Text outputText;

    void Start()
    {
        FireBaseAuthManager.Instance.init();
        FireBaseAuthManager.Instance.LoginState += OnChangedState;

        FireBaseDatabase.Instance.init();
    }

    private void OnChangedState(bool sign)
    {
        //outputText.text = sign ? "�α��� : " : "�α׾ƿ� : ";
        //outputText.text += FireBaseAuthManager.Instance.UserId;
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
    }

    public async void Login()
    {
        string e = email.text;
        string p = password.text;

        if (await FireBaseAuthManager.Instance.Login(e, p))
        {
            ResetText();

            if (await FireBaseDatabase.Instance.readUser(FireBaseAuthManager.Instance.UserId.ToString()))
            {
                GoogleSheet.LogIn();
                SceneManager.LoadScene(1);
            }
        }
        else
        {
            outputText.text = "�α��ο� ���� �ϼ̽��ϴ�.";
        }
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

    public async void SetNickName()
    {
        if (NickName.text.Length > 0)
        {
            Debug.Log(NickName.text);
            Debug.Log(email.text);
            Debug.Log(FireBaseAuthManager.Instance.UserId);

            FireBaseDatabase.Instance.writeNewUser(FireBaseAuthManager.Instance.UserId, NickName.text, email.text);
            outputText.text = NickName.text + "�� ȯ���մϴ�.";
            NickName.text = "";

            if (await FireBaseDatabase.Instance.readUser(FireBaseAuthManager.Instance.UserId.ToString()))
            {
                GoogleSheet.LogIn();
                SceneManager.LoadScene(1);
            }
        }
        else
            outputText.text = "�����ϼ̽��ϴ�. �г����� �ٽ� �Է����ּ���";
    }

    public void Exit()
    {
        Application.Quit();
    }
}
