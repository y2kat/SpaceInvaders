using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesDisplay : MonoBehaviour
{
    public PlayerController player;
    private TextMeshProUGUI livesText;

    void Start()
    {
        livesText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        livesText.text = "Lives: " + player.lives;
    }
}
