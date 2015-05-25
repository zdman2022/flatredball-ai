## Prerequisites ##

  * FlatRedBall December 2010  - Since this is a FlatRedBall library, FlatRedBall will be required.

# How To Use #

The library is setup so that getting it setup should be quick.

## Creating an Agent ##

The SteeringAgent class is the container for the AI.  To set it up, you will need to call Initialize.

```
using FlatRedBallAI.AI.SteeringAgents.Agents;

SteeringAgent mSA;

mSA = new SteeringAgent();

//pEntity is the PositionedObject that the agent is controlling.
//The second parameter is the maximum force that can be applied.
mSA.Initialize(pPositionedObject, 10);
```

## Adding behaviors ##
Once an agent has been created, behaviors must be added in order for the agent to do anything.

Every behavior has 2 Properties, Weight and Probability.  Weight is used for WTRSP force calculation (the default) and probability is used for PD force calculations.

The other properties are behavior specific.  Change them to tweak the behavior to how you want it to behave.

```
using FlatRedBallAI.AI.SteeringAgents.Behaviors;

//Wandering
WanderBehavior wander = new WanderBehavior();

wander.WanderDistance = 20;
wander.WanderRadius = 5;
wander.WanderJitter = .2f;
wander.Weight = .3f;

mSA.Behaviors.Add(wander);
```

## Updating AI ##

Once the agent has been setup, call Calculate to retrieve the force it think needs to be applied to the PositionedObject.  You can then modify it how you see fit or just apply it to the PositionedObject.

```
//This just applies the AI's force to the velocity.
pEntity.Velocity += mSA.Calculate();
```