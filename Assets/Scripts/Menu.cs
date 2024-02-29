using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject level;
    [SerializeField] private GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        Time.timeScale = 1.0f;
        level.SetActive(true);
        menu.SetActive(false);
    }
}
