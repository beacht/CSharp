using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // public static ScoreManager instance;
    
    public Text scoreText;
    public Text highScoreText;
    //public Animator scoreAnimation;

    int score = 0;
    int highScore = 0;

    /*private void Awake()
    {
        instance = this;
    }*/

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);
        scoreText.text = score.ToString() + " POINTS";
        highScoreText.text = "HIGHSCORE: " + highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoint()
    {
        // Increase score
        score += 1;

        // Animate score and change text
        scoreText.text = score.ToString() + " POINTS";
        //scoreAnimation.SetTrigger("animateScore");

        // Update high score
        if(highScore < score)
        {
            PlayerPrefs.SetInt("highScore", score);
            highScoreText.text = "HIGHSCORE: " + highScore.ToString();
        } 
    }
}
