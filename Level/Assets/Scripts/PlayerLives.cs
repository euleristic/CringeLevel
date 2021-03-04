using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLives : MonoBehaviour
{
    public int lives;
    private Checkpoint lastCheckpoint;

    public void Kill()
    {
        lives--;
        if (lives == 0 || lastCheckpoint == null)
            SceneManager.LoadScene("GameOver");
        transform.position = lastCheckpoint.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        lastCheckpoint = other.gameObject.GetComponent<Checkpoint>();
    }
}
