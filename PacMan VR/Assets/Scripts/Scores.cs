using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scores : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    protected int score;

    public TextMeshProUGUI highScoreText;
    protected int highScore;


    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {

        // Update current score and high score
        scoreText.text = GameManager.Instance.score.ToString();
        highScoreText.text = GameManager.Instance.highScore.ToString();
    }
}
