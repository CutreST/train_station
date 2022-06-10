using Godot;
using MySystems;
using System;

public class FirstScriptt : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        DialogSystem dial;
        SystemManager.GetInstance(this).TryGetSystem<DialogSystem>(out dial, true);
        dial.DisplayDialog("Puta mierda tete");

        InGameSystem game;
        SystemManager.GetInstance(this).TryGetSystem<InGameSystem>(out game, true);
        SystemManager.GetInstance(this).AddToStack(game);

    }

    public override void _Process(float delta)
    {
        Messages.Print("Mieeerda");
    }
}
