using Godot;
using System;

public partial class TrackingCamera : Camera3D
{
	[Export]
	private Node3D trackingTarget;

	public Transform3D CameraStartTransform;

	public override void _Ready()
	{
		trackingTarget = GetNode<Node3D>("../PlayerCharacter");
		
		Node3D calc = this;

		// reset the rotation of the camera.
		calc.Rotation = new Vector3(0, Rotation.Y, 0);

		CameraStartTransform = calc.GlobalTransform;
	}

	public override void _Process(double delta)
	{
		Transform3D newTransform = Transform.LookingAt(trackingTarget.GlobalPosition);

		Transform = newTransform;

	}
}
