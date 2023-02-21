using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop : Command
{
    public int loopCount;
    Bracket bracket;
    List<Command> commands;
    
    private void Start()
    {
        commands = new List<Command>();
        bracket = GetComponentInChildren<Bracket>();
        foreach (GameObject child in bracket.child1)
        {
            commands.Add(child.GetComponent<Command>());
        }
    }

    public override void Execute()
    {
        StartCoroutine(Looping());
    }

    IEnumerator Looping()
    {
        for (int i = 0; i < loopCount; i++)
        {
            for (int j = 0; j < commands.Count; j++)
            {
                while (Core.instance.isRunning)
                {
                    yield return null;
                }
                commands[j].Execute();
            }
        }
    }
}
