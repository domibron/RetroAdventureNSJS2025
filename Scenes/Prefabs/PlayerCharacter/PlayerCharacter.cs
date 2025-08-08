using Godot;
using System;

public partial class PlayerCharacter : CharacterBody3D
{
	[Export]
	private float movementSpeed = 1;
	
	private Vector3 movementVector;

	private Camera3D currentCamera;
	private TrackingCamera trackingCamera;
	
	private Area3D interactionArea;
	
	public override void _Ready()
	{
		interactionArea = (Area3D) GetChild(1); // Maybe think of a cleaner, less hard-coded way.
	}

	public override void _Process(double delta)
	{
		if (currentCamera != GetViewport().GetCamera3D())
		{
			currentCamera = GetViewport().GetCamera3D();
			trackingCamera = currentCamera as TrackingCamera;

		}

		// movement input vector.
		Vector3 inputVector = new Vector3(-Input.GetActionStrength("M_Left") + Input.GetActionStrength("M_Right"), 
			0, 
			-Input.GetActionStrength("M_Up") + Input.GetActionStrength("M_Down")).Normalized();


		// we can get a leveled movement direction.
		movementVector = trackingCamera.CameraStartTransform.Basis.X * inputVector.X + trackingCamera.CameraStartTransform.Basis.Z * inputVector.Z;
		
		HandleInteraction();
	}

	public override void _PhysicsProcess(double delta)
	{
		Velocity = movementVector * movementSpeed;
		MoveAndSlide();
	}
	
	private void HandleInteraction()
	{
		if (Input.IsActionJustPressed("Interact") && interactionArea.HasOverlappingAreas())
		{
			Godot.Collections.Array<Area3D> potentialInteracts = interactionArea.GetOverlappingAreas();
			Interactability interactionTarget;
			if (potentialInteracts.Count > 1)
			{
				interactionTarget = GetClosest(potentialInteracts);
			}
			else
			{
				interactionTarget = (Interactability) potentialInteracts[0];
			}
			interactionTarget.Interact();
		}
	}
	
	private Interactability GetClosest(Godot.Collections.Array<Area3D> potentialInteracts)
	{
		Area3D currentClosest = potentialInteracts[0];
		foreach (Area3D interact in potentialInteracts)
		{
			if (GlobalPosition.DistanceTo(interact.GlobalPosition) < GlobalPosition.DistanceTo(currentClosest.GlobalPosition))
			{
				currentClosest = interact;
			}
		}
		return (Interactability) currentClosest;
	}
}
