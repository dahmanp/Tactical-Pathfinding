using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separator : Kinematic
{
    Separation myMoveType;
    Align myRotateType;

    void Start()
    {
        myMoveType = new Separation();
        myMoveType.character = this;
        Kinematic[] kinematics = GameObject.FindObjectsOfType<Kinematic>();
        int count = kinematics.Length - 1;
        Kinematic[] filteredKinematics = new Kinematic[count];
        int i = 0;
        foreach (var k in kinematics)
        {
            if (k != this)
            {
                filteredKinematics[i] = k;
                i++;
            }
        }
        myMoveType.targets = filteredKinematics;

        myRotateType = new Align();
        myRotateType.character = this;
        myRotateType.target = myTarget;
    }

    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.linear = myMoveType.getSteering().linear;
        steeringUpdate.angular = myRotateType.getSteering().angular;
        base.Update();
    }
}
