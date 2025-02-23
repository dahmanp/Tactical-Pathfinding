using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTP : MonoBehaviour
{
    public GameObject blockedNode;

    public void Switch()
    {
        blockedNode.SetActive(false);
    }
}
