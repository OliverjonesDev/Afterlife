
//Interactable system made by Olly - Student Number: 33697643

To use the interactable script.
We will use this as the master class for all of our interactable objects.
This means we can use this to have one call, and if any object has any kind of interactable script on it
we can easily access this from the planned raytracing script that is to be implimented.

The use case of this should look like.

//Pseudocode

// on object clicked
if (tag = interactable)
	getComponents(interactable).Interaction();

// if object clicked again whilst interacting with
if (tag = interactable)
	getComponents(interactable).Interaction();

This is handled in the Interactable parent script, so it should figure out what to do.

Now to implement the functionality in the custom interactable objects.

Use the methods made in the Interactable parent class.

    public override void InteractionBehaviour()
    {
        RunAnimation();
    }

    public override void InteractionExitBehaviour()
    {
        ResetAnimation();
    }

This is the example from the AnimatiableXYZ Script. If you code your functionality in a method and call it inside these overrides or,
code directly into the overrides. It will work since these are being called in the update function in the Interactable parent.

