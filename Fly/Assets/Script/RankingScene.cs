using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RankingScene : MonoBehaviour
{
    public Text[] Name;
    public Text[] Score;

    void Start()
    {
        int count = 0;

        Debug.Log(FireBaseDatabase.Instance.rankings[0].username);
        
        foreach (var Rank in FireBaseDatabase.Instance.rankings)
        {
            Name[count].text = "Name : " + Rank.username;
            Score[count].text = "Score : " + Rank.score.ToString();

            Debug.Log("Ω««‡¡ﬂ");

            count++;
        }
    }
}
