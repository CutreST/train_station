using Godot;
using MySystems;
using System;

public class FirstScriptt : Node
{
    DialogSystem dial;
    public override void _EnterTree()
    {
        SystemManager.GetInstance(this).TryGetSystem<DialogSystem>(out dial, true);
    }

    public override void _Process(float delta)
    {
        if(dial.DialInput.IsNext){
            dial.DisplayDialog("Hola caracola que tal estas");
        }
    }


}
