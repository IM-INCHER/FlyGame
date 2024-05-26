using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    public Text NickName;

    void Start()
    {
        NickName.text = FireBaseDatabase.Instance.loginUser.username;
    }

    void Update()
    {
        
    }
}
