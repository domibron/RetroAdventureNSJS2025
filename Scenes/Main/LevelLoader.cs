using Godot;
using System;

public partial class LevelLoader : Node3D
{
	private PackedScene currentPackedScene;
	
	public void Load(String path)
	{
		Unload();
		currentPackedScene = GD.Load<PackedScene>(path);
		Node currentScene = currentPackedScene.Instantiate();
		AddChild(currentScene);
	}
	
	private void Unload()
	{
		if (GetChildCount() > 0)
		{
			foreach (Node child in GetChildren())
			{
				child.QueueFree();
			}
		}
	}
}
