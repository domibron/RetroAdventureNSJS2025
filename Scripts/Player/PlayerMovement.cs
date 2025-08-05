using Godot;
using System;

public partial class PlayerMovement : CharacterBody3D
{
	private Vector3 movementVector;

	private Camera3D currentCamera;
	private CameraLookAt cameraLookAt;

	public override void _Process(double delta)
	{
		if (currentCamera != GetViewport().GetCamera3D())
		{
			currentCamera = GetViewport().GetCamera3D();
			cameraLookAt = currentCamera as CameraLookAt;

		}

		// movement input vector.
		Vector3 inputVector = new Vector3(-Input.GetActionStrength("M_Left") + Input.GetActionStrength("M_Right"), 
			0, 
			-Input.GetActionStrength("M_Up") + Input.GetActionStrength("M_Down")).Normalized();


		// we can get a leveled movement direction.
		//GD.Print(cameraLookAt.CameraStartTransform.Basis);

		movementVector = cameraLookAt.CameraStartTransform.Basis.X * inputVector.X + cameraLookAt.CameraStartTransform.Basis.Z * inputVector.Z;


		//GD.Print(GetViewport().GetCamera3D().Name);
	}

	public override void _PhysicsProcess(double delta)
	{
		Velocity = movementVector;

		MoveAndSlide();

		
	}
}
