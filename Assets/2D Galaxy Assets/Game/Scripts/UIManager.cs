using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Sprite[] lives; 
    public Image livesImageDisplay;
    public Text scoreText;

    public GameObject title;
    public int Score;



    public void UpdateLives(int currentLives)
    {
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        Score += 10;

        scoreText.text = "Score: " + Score;
    }

    public void ShowTitle(bool show)
    {
        if(show)
        {
            title.SetActive(true);
        }else
        {
            title.SetActive(false);
            scoreText.text = "Score: ";
        }
    }
   
}
