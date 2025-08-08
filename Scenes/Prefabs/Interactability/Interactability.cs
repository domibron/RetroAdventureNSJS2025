using Godot;
using System;

public partial class Interactability : Area3D
{
	[Export]
	private float interactionRadius = 1;
	
	[Export]
	private string name = "something"; 
	
	public int timesInteracted = 0;
	public bool interacted { get { return timesInteracted > 0; } }
	
	public override void _Ready()
	{
		SphereShape3D shape = (SphereShape3D) GetChild<CollisionShape3D>(0).GetShape();
		shape.Radius = interactionRadius;
	}
	
	public virtual void Interact()
	{
		GD.Print("Interacted with: " + name);
		timesInteracted++;
	}
	
}
