using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexFlocker : Kinematic
{
    public GameObject primaryGoal;
    BlendedSteering mySteering;
    Kinematic[] kinematicBirds;

    PrioritySteering myAdvancedSteering;
    public bool avoidObstacles = false;

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

        ObstacleAvoidance myAvoid = new ObstacleAvoidance();
        myAvoid.character = this;
        myAvoid.target = primaryGoal;
        //myAvoid.flee = true;

        BlendedSteering myHighPrioritySteering = new BlendedSteering();
        myHighPrioritySteering.behaviors = new BehaviorAndWeight[1];
        myHighPrioritySteering.behaviors[0] = new BehaviorAndWeight();
        myHighPrioritySteering.behaviors[0].behavior = myAvoid;
        myHighPrioritySteering.behaviors[0].weight = 1f;

        myAdvancedSteering = new PrioritySteering();
        myAdvancedSteering.groups = new BlendedSteering[2];
        myAdvancedSteering.groups[0] = new BlendedSteering();
        myAdvancedSteering.groups[0] = myHighPrioritySteering;
        myAdvancedSteering.groups[1] = new BlendedSteering();
        myAdvancedSteering.groups[1] = mySteering;
    }

    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        if (!avoidObstacles)
        {
            steeringUpdate = mySteering.getSteering();
        }
        else
        {
            steeringUpdate = myAdvancedSteering.getSteering();
        }
        base.Update();
    }
}