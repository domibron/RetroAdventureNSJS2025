using Godot;
using System;

public partial class Main : Node
{
	[Export]
	private String startLevel = "res://Scenes/Level1/Level1.tscn";
	
	private Button startButton;
	
	public override void _Ready()
	{
		startButton = GetNode<Button>("GUI/CanvasLayer/Menu/ButtonStart");
		startButton.Connect(Button.SignalName.Pressed, Callable.From(OnButtonStartPressed));
	}
	
	private void OnButtonStartPressed()
	{
		startButton.QueueFree();
		StartGame();
	}
	
	private void StartGame()
	{
		GetNode<LevelLoader>("LevelLoader").Load(startLevel);
	}
	
}
