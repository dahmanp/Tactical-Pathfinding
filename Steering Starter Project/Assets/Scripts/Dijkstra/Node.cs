using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node[] ConnectsTo;
    public bool blocked;
    public GameObject block;
    public GameObject[] hunters;

    public void Update()
    {
        if (blocked && block != null)
        {
            block.SetActive(true);
        }
    }

    public void changeStat()
    {
        if (!blocked && block != null)
        {
            blocked = true;
            block.SetActive(true);
            foreach (var hunter in hunters)
            {
                hunter.GetComponent<Pathfinder>().ClearPath();
                hunter.GetComponent<Pathfinder>().remap();
            }
        }
        else if (blocked && block != null)
        {
            blocked = false;
            block.SetActive(false);
            foreach (var hunter in hunters)
            {
                hunter.GetComponent<Pathfinder>().ClearPath();
                hunter.GetComponent<Pathfinder>().remap();
            }
        }
    }
}
