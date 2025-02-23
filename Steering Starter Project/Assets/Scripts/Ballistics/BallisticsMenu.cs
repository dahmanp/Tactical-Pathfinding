using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticsMenu : MonoBehaviour
{
    public GameObject target;
    public GameObject[] locations;
    public int count = 0;

    public void changeLocation()
    {
        if (count < 2)
        {
            count++;
        }
        else
        {
            count = 0;
        }
        target.transform.position = locations[count].transform.position;
    }
}
