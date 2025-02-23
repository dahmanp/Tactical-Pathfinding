using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject[] path;
    public GameObject[] seekFlee;
    public GameObject[] pursueEvade;
    public GameObject[] objAvoid;
    public GameObject[] face;
    public GameObject[] look;

    void Start()
    {
        deactivateAll();
    }

    public void pathFollower()
    {
        deactivateAll();
        activateArray(path);
    }

    public void seekAndFlee()
    {
        deactivateAll();
        activateArray(seekFlee);
    }

    public void pursueAndEvade()
    {
        deactivateAll();
        activateArray(pursueEvade);
    }

    public void objectAvoid()
    {
        deactivateAll();
        activateArray(objAvoid);
    }

    public void faceTarget()
    {
        deactivateAll();
        activateArray(face);
    }

    public void lookWhereGoing()
    {
        deactivateAll();
        activateArray(look);
    }

    void activateArray(GameObject[] array)
    {
        foreach (GameObject item in array)
        {
            item.SetActive(true);
        }
    }

    void deactivateArray(GameObject[] array)
    {
        foreach (GameObject item in array)
        {
            item.SetActive(false);
        }
    }

    public void deactivateAll()
    {
        deactivateArray(path);
        deactivateArray(seekFlee);
        deactivateArray(pursueEvade);
        deactivateArray(objAvoid);
        deactivateArray(face);
        deactivateArray(look);
    }

    public void resetSim()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
