using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocker : Kinematic
{
    public GameObject primaryGoal;
    BlendedSteering mySteering;
    Kinematic[] kinematicBirds;

    void Start()
    {
        Separation separate = new Separation();
        separate.character = this;
        GameObject[] birdTime = GameObject.FindGameObjectsWithTag("BIRD");
        kinematicBirds = new Kinematic[birdTime.Length - 1];

        int x = 0;
        for (int i = 0; i < birdTime.Length - 1; i++)
        {
            if (birdTime[i] == this)
            {
                continue;
            }
            kinematicBirds[x++] = birdTime[i].GetComponent<Kinematic>();
        }
        separate.targets = kinematicBirds;

        LookWhereGoing myRotateType = new LookWhereGoing();
        myRotateType.character = this;

        Separation separation = new Separation();
        separation.character = this;

        Arrive arrive = new Arrive();
        arrive.character = this;
        arrive.target = primaryGoal;

        mySteering = new BlendedSteering();
        mySteering.behaviors = new BehaviorAndWeight[3];

        mySteering.behaviors[0] = new BehaviorAndWeight();
        mySteering.behaviors[0].behavior = separate;
        mySteering.behaviors[0].weight = 1f;

        mySteering.behaviors[1] = new BehaviorAndWeight();
        mySteering.behaviors[1].behavior = arrive;
        mySteering.behaviors[1].weight = 1f;

        mySteering.behaviors[2] = new BehaviorAndWeight();
        mySteering.behaviors[2].behavior = myRotateType;
        mySteering.behaviors[2].weight = 1f;
    }

    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate = mySteering.getSteering();
        base.Update();
    }
}