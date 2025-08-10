using Godot;
using System;

public partial class TrackingCamera : Camera3D
{
	[Export]
	private float minDistance = 5f;
	[Export]
	private float minFOV = 1f;
	[Export]
	private float maxFOV = 120f;
	[Export]
	private float offsetFromCenter = 10f;
	[Export]
	public Node3D TrackingTarget;

	public Transform3D CameraStartTransform;

	public override void _Ready()
	{
		Node3D calc = this;

		// reset the rotation of the camera.
		calc.Rotation = new Vector3(0, Rotation.Y, 0);

		CameraStartTransform = calc.GlobalTransform;
	}

	public override void _Process(double delta)
	{
		Transform3D newTransform = Transform.LookingAt(TrackingTarget.GlobalPosition);

		Transform = newTransform;


		float distance = TrackingTarget.GlobalPosition.DistanceTo(GlobalPosition);


		if (distance < minDistance)
		{
			distance = minDistance;
		}

		
		Fov = Mathf.Clamp(Mathf.RadToDeg(Mathf.Atan(offsetFromCenter / distance)) * 2f, (minFOV < 1f ? 1f : minFOV), (maxFOV > 179f ? 179f : maxFOV));

	}

	//private float Distance(Vector3 a, Vector3 b)
	//{
		
	//	return Mathf.Sqrt(Mathf.Pow((a.X - b.X), 2f) + Mathf.Pow((a.Y - b.Y), 2f) + Mathf.Pow((a.Z - b.Z), 2f));
	//}
}
