using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : Seek
{
    public GameObject[] path;
    int currTarget = 0;
    float targetRadius = 0.5f;

    int closestIndex = 0;
    float closestDistance = float.MaxValue;

    public override SteeringOutput getSteering()
    {
        if (target == null)
        {
            for (int i = 0; i < path.Length; i++)
            {
                float distance = Vector3.Distance(character.transform.position, path[i].transform.position);
                if (distance < closestDistance)
                {
                    closestIndex = i;
                    closestDistance = distance;
                }
            }
            target = path[closestIndex];
        }

        float radDistance = Vector3.Distance(target.transform.position, character.transform.position);
        if (radDistance < targetRadius)
        {
            currTarget = (currTarget + 1) % path.Length;
            target = path[currTarget];
        }

        return base.getSteering();
    }
}