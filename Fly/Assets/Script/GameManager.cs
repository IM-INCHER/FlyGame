using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI EndScoreText;

    public bool isShot = false;
    public int score;

    private void Start()
    {
        score = 0;
    }

    private void Update()
    {
        ScoreText.text = "Score : " + score.ToString();
    }
}
