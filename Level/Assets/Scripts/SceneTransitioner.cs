using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (SceneManager.GetActiveScene().name == "Mattis")
        {
            SceneManager.LoadScene("Ivan");
        }
        else if (SceneManager.GetActiveScene().name == "Ivan")
        {
            SceneManager.LoadScene("Valter");
        }
        else if (SceneManager.GetActiveScene().name == "Valter")
        {
            SceneManager.LoadScene("Victory");
        }
    }
}
