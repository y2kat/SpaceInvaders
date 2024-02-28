using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI highscoreText;

    void Start()
    {
        highscoreText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        int highscore = PlayerPrefs.GetInt("Highscore", 0);
        highscoreText.text = "Highscore: " + highscore;
    }
}
