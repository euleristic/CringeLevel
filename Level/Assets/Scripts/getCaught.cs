using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class getCaught : MonoBehaviour
{
    [SerializeField] buttonScript bs;
    public bool hasBeenCaught = false;
    void Update()
    {
        if (hasBeenCaught)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        if (bs.pressedButton)
        {
            this.gameObject.SetActive(false);
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
