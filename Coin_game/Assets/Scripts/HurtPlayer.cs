
using UnityEngine;
using UnityEngine.SceneManagement;

public class HurtPlayer : MonoBehaviour
{
    private float _waitToload = 2f;
    private bool _reloading;

    private void Start()
    {

    }

    private void Update()
    {
        if (_reloading)
        {
            _waitToload -= Time.deltaTime;
            if (_waitToload <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<HealthNew>().HurtPlayer(10);
            Health playerHealth = other.gameObject.GetComponent<Health>();
            playerHealth.health--;
        }
    }
}