using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    private PlayerInputActions inputActions;

    [SerializeField] private float speed;

    private PoolScript bulletPool;
    private Rigidbody2D rb;

    public int maxHealth = 100;
    public int lives = 3;
    private int currentHealth;

    public int score = 0;

    public PanelManager panelManager;

    public Menu menu;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        bulletPool = GameObject.Find("BulletPool").GetComponent<PoolScript>();
    }

    void Start()
    {
        inputActions.Enable();
        rb = GetComponent<Rigidbody2D>();

        inputActions.Standard.Shoot.performed += Shoot;
        currentHealth = maxHealth;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 direction = new Vector2(inputActions.Standard.Movement.ReadValue<float>(), 0).normalized;
        rb.AddForce(direction * speed);

        float screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        float screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        float clampedX = Mathf.Clamp(rb.position.x, screenLeft, screenRight);

        rb.position = new Vector2(clampedX, rb.position.y);
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        GameObject bullet = bulletPool.RequestObject();
        bullet.SetActive(true);
        bullet.transform.position = transform.position;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            LoseLife();
        }
    }

    private void LoseLife()
    {
        lives--;
        if (lives <= 0)
        {
            GameOver();
            panelManager.EnablePanel(3);
        }
        else
        {
            currentHealth = maxHealth;
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0;

        int highScore = PlayerPrefs.GetInt("Highscore", 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("Highscore", score);
        }

        menu.showDeathScreen();
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score: " + score);

        int highScore = PlayerPrefs.GetInt("Highscore", 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
    }
}

