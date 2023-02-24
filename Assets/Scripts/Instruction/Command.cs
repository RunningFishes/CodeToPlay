using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command : MonoBehaviour
{
    public Command nextLinkedCommand;
    public Command prevLinkedCommand;
    public Command parentCommand;

    protected SpriteRenderer spriteRenderer;
    protected Color originalColor;
    protected float clockPerExecute;
    
    public abstract void Execute();
    public virtual void SetNextLinkedCommand(Command command)
    {
        nextLinkedCommand = command;
    }
    public virtual void SetPrevLinkedCommand(Command command)
    {
        prevLinkedCommand = command;
    }
    public virtual void SetParentCommand(Command command)
    {
        //set all parent below command
        Command tmpCommand = this;
        while (tmpCommand != null)
        {
            tmpCommand.parentCommand = command;
            tmpCommand = tmpCommand.nextLinkedCommand;
        }
    }
}