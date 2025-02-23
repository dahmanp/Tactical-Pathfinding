using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoider : Kinematic
{
    CollisionAvoidance myMoveType;
    public Kinematic[] myTargets;

    void Start()
    {
        myMoveType = new CollisionAvoidance();
        myMoveType.character = this;
        myMoveType.targets = myTargets;
    }

    protected override void Update()
    {
        //steeringUpdate = new SteeringOutput();
        steeringUpdate = myMoveType.getSteering();
        base.Update();
    }
}
