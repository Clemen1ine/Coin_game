using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthNew : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    private bool _flashActive;
    [SerializeField] private float flashLeght = 0f;
    private float _flashCounter;
    private SpriteRenderer _playerSprite;

    private void Start()
    {
        _playerSprite = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        if (_flashActive)
        {
            if (_flashCounter > flashLeght * .99f)
            {
                _playerSprite.color = new Color(_playerSprite.color.r, _playerSprite.color.g, _playerSprite.color.b, 0f);
            }else if(_flashCounter > flashLeght * .82f)
            {
                _playerSprite.color = new Color(_playerSprite.color.r, _playerSprite.color.g, _playerSprite.color.b, 1f);
            }
            else if(_flashCounter > flashLeght * .66f)
            {
                _playerSprite.color = new Color(_playerSprite.color.r, _playerSprite.color.g, _playerSprite.color.b, 0f);
            }
            else if(_flashCounter > flashLeght * .49f)
            {
                _playerSprite.color = new Color(_playerSprite.color.r, _playerSprite.color.g, _playerSprite.color.b, 1f);
            }
            else if(_flashCounter > flashLeght * .33f)
            {
                _playerSprite.color = new Color(_playerSprite.color.r, _playerSprite.color.g, _playerSprite.color.b, 0f);
            }
            else if(_flashCounter > flashLeght * .16f)
            {
                _playerSprite.color = new Color(_playerSprite.color.r, _playerSprite.color.g, _playerSprite.color.b, 1f);
            }
            else if(_flashCounter > 0f)
            {
                _playerSprite.color = new Color(_playerSprite.color.r, _playerSprite.color.g, _playerSprite.color.b, 0f);
            }
            else
            {
                _playerSprite.color = new Color(_playerSprite.color.r, _playerSprite.color.g, _playerSprite.color.b, 1f);
                _flashActive = false;
            }
            _flashCounter -= Time.deltaTime;
        }
    }

    public void HurtPlayer(int damageTolive)
    {
        currentHealth -= damageTolive;
        _flashActive = true;
        _flashCounter = flashLeght;
        
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HealthPotion"))
        {
            Health healthComponent = GetComponent<Health>();
            if (healthComponent != null)
            {
                healthComponent.AddHealth(1);
                Destroy(other.gameObject);
            }
        }
    }
}