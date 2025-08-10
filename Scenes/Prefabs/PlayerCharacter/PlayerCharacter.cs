using Godot;
using System;

public partial class PlayerCharacter : CharacterBody3D
{
	[Export]
	private float walkSpeed = 3; // m/s
	[Export]
	private float sprintSpeed = 12; // m/s
	[Export]
	private float turnSpeed = 60; // degrees/s

	private Vector3 gravity; // m/s^2
	private Area3D interactionArea;
	
	public override void _Ready()
	{
		gravity = (Vector3)ProjectSettings.GetSetting("physics/3d/default_gravity_vector")
					 * (float)ProjectSettings.GetSetting("physics/3d/default_gravity");
		interactionArea = GetNode<Area3D>("InteractionArea");
	}

	public override void _PhysicsProcess(double delta)
	{
		HandleMovement((float)delta);
	}
	
	private void HandleMovement(float delta)
	{
		float move = Input.GetAxis("M_Backwards", "M_Forwards"); // -1..1
		float turn = Input.GetAxis("M_Right", "M_Left");		 // -1..1
		Vector3 horizontalMovement = move * Transform.Basis.Z * GetSpeedFactor();
		
		RotateY(turn * Mathf.DegToRad(turnSpeed) * delta);
		
		Velocity = new Vector3(horizontalMovement.X, gravity.Y, horizontalMovement.Z);
		MoveAndSlide();
	}
	
	private float GetSpeedFactor()
	{
		return (Input.IsActionPressed("Sprint") ? sprintSpeed : walkSpeed);
	}
	
	public override void _Process(double delta)
	{
		HandleInteraction();
	}
	
	private void HandleInteraction()
	{
		if (Input.IsActionJustPressed("Interact") && interactionArea.HasOverlappingAreas())
		{
			Godot.Collections.Array<Area3D> potentialInteracts = interactionArea.GetOverlappingAreas();
			Interactability interactionTarget;
			if (potentialInteracts.Count > 1)
			{
				interactionTarget = (Interactability)GetClosest(potentialInteracts);
			}
			else
			{
				interactionTarget = (Interactability)potentialInteracts[0];
			}
			interactionTarget.Interact();
		}
	}
	
	private Area3D GetClosest(Godot.Collections.Array<Area3D> potentialInteracts)
	{
		Area3D currentClosest = potentialInteracts[0];
		foreach (Area3D interact in potentialInteracts)
		{
			if (GlobalPosition.DistanceTo(interact.GlobalPosition) < GlobalPosition.DistanceTo(currentClosest.GlobalPosition))
			{
				currentClosest = interact;
			}
		}
		return currentClosest;
	}
}
