using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void StartBtn()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LogoutBtn()
    {
        SceneManager.LoadScene("LoginScene");
    }

    public async void RankingBtn()
    {
        if(await FireBaseDatabase.Instance.ReadRanking())
            SceneManager.LoadScene("RankingScene");
    }

    public void MainBtn()
    {
        SceneManager.LoadScene("MainScene");
    }
}
