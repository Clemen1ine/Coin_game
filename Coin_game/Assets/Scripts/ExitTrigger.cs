    using UnityEngine.SceneManagement;
    using UnityEngine;

    public class ExitTrigger : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ( collision.CompareTag("Player"))
            {
                // Reload the current scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }