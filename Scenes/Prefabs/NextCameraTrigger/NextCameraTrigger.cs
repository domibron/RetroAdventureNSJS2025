using Godot;
using System;

public partial class NextCameraTrigger : Area3D
{
	[Export]
	private Camera3D targetCameraSwitch;
	
	[Export]
	private Vector3 AreaSize = new Vector3(1, 1, 1);

	private bool hasTriggered = false;
	
	public override void _Ready()
	{
		BoxShape3D triggerBox = (BoxShape3D) GetChild<CollisionShape3D>(0).GetShape();
		triggerBox.Size = AreaSize;
	}

	public override void _Process(double delta)
	{
		
		if (HasOverlappingBodies())
		{
			
			Godot.Collections.Array<Node3D> bodies = GetOverlappingBodies();

			foreach (Node3D node in bodies)
			{
				if (node.Name == new StringName("PlayerCharacter") && !hasTriggered)
				{
					hasTriggered = true;
					
					if (targetCameraSwitch == null){
						GD.PrintErr("Cannot switch with a null cam!");
						return;
					}
					
					GetViewport().GetCamera3D().ClearCurrent(false);
					targetCameraSwitch.MakeCurrent();
				}
			}
		}
		else
		{
			hasTriggered = false;
		}
	}
}
