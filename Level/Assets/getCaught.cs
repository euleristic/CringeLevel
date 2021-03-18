using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class getCaught : MonoBehaviour
{
    public bool hasBeenCaught = false;
    void Update()
    {
        if (hasBeenCaught)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            hasBeenCaught = true;
        }
    }
}
