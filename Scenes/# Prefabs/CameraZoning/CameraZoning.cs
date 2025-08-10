using Godot;
using System;

public partial class CameraZoning : Node
{
	private TrackingCamera camera;
	private Area3D zone;
	
	public override void _Ready()
	{
		zone = (Area3D)GetParent();
		camera = (TrackingCamera)zone.GetParent();
	}
	
	public override void _Process(double delta)
	{
		if (!camera.Current && zone.OverlapsBody((PhysicsBody3D)camera.TrackingTarget))
		{
			camera.Current = true;
		}
	}
	
	
}
