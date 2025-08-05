using Godot;
using System;

public partial class CameraLookAt : Camera3D
{
	private Node3D playerNode;

	public Transform3D CameraStartTransform;

	public override void _Ready()
	{
		Node3D calc = this;

		// reset the rotation of the camera.
		calc.Rotation = new Vector3(0, Rotation.Y, 0);

		CameraStartTransform = calc.GlobalTransform;

		



		Godot.Collections.Array<Node> children = GetTree().Root.GetChildren();

		foreach (var item in children)
		{
			Godot.Collections.Array<Node> childrenChildren = item.GetChildren();

			foreach (var child in childrenChildren)
			{
				if (child.Name == new StringName("PlayerCharacter"))
				{
					playerNode = (Node3D)child;
					//GD.Print("Yes");

				}
			}
		}

		

		//GD.Print("Done" + playerNode == null);
	}

	public override void _Process(double delta)
	{
		Transform3D newTransform = Transform.LookingAt(playerNode.GlobalPosition);

		Transform = newTransform;

	}
}
