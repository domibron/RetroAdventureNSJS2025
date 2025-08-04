using Godot;
using System;

public partial class PlayerMovement : CharacterBody3D
{
	private Vector3 movementVector;

	private Camera3D currentCamera;

	public override void _Process(double delta)
	{
		if (currentCamera != GetViewport().GetCamera3D())
		{
			currentCamera = GetViewport().GetCamera3D();
		}

		// movement input vector.
		movementVector = new Vector3(Input.GetActionStrength("M_Left") + -Input.GetActionStrength("M_Right"), 
			0, 
			Input.GetActionStrength("M_Up") + -Input.GetActionStrength("M_Down")).Normalized();


		// we store the rotation, pos and all of the camera so we dont modify it directly.
		Node3D mathCalc = currentCamera;

		// reset the rotation of the camera.
		mathCalc.Rotation = new Vector3(0, mathCalc.Rotation.Y, mathCalc.Rotation.Z);

		// we can get a leveled movement direction.
		GD.Print(mathCalc.Basis.Z);

		//GD.Print(GetViewport().GetCamera3D().Name);
	}

	public override void _PhysicsProcess(double delta)
	{
		Velocity = movementVector;

		MoveAndSlide();

		
	}
}
