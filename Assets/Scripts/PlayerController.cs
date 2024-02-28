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
        Vector2 direction = new Vector2(inputActions.Standard.Movement.ReadValue<float>(),0).normalized;
        rb.AddForce(direction * speed);
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
        }
        else
        {
            currentHealth = maxHealth;
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
    }
}
