using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloppyCollecter : MonoBehaviour
{
    [SerializeField] List<FloppyDisc> floppyDiscs;


    void Update()
    {
        foreach (FloppyDisc disc in floppyDiscs)
        {
            if (!disc.collected) return;
        }
        SceneManager.LoadScene("Victory");
    }
}
