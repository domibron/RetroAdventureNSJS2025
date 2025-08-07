using Godot;
using System;

public partial class TriggerCameraSwitch : Area3D
{
	[Export]
	private Camera3D targetCameraSwitch;

	private bool hasTriggered = false;

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
