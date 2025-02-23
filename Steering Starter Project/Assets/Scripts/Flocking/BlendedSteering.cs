using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorAndWeight
{
    public float weight;
    public SteeringBehavior behavior;
}

public class BlendedSteering
{
    public BehaviorAndWeight[] behaviors;
    float maxAcceleration = 1f;
    float maxRotation = 3f;

    public SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        foreach (BehaviorAndWeight b in behaviors)
        {
            SteeringOutput output = b.behavior.getSteering();

            if (output != null)
            {
                result.linear += output.linear * b.weight;
                result.angular += output.angular * b.weight;
            }
        }

        result.linear = result.linear.normalized * maxAcceleration;
        float angularAcceleration = Mathf.Abs(result.angular);

        if (angularAcceleration > maxRotation)
        {
            result.angular /= angularAcceleration;
            result.angular *= maxRotation;
        }

        return result;
    }
}
