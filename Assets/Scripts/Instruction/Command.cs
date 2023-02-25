using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public virtual Command GetBottomCommand()
    {
        Command tmpCommand = this;
        while(tmpCommand.nextLinkedCommand != null)
        {
            tmpCommand = tmpCommand.nextLinkedCommand;
        }
        return tmpCommand;
    }
    public virtual int GetSizeCommands()
    {
        int count = 0;
        Command tmpCommand = this;
        while(tmpCommand != null)
        {
            // get in loop command size
            if(tmpCommand.gameObject.tag == "Loop" && tmpCommand.GetComponent<Loop>().linkedLoopCommand != null)
            {
                count += tmpCommand.GetComponent<Loop>().linkedLoopCommand.GetSizeCommands();
            }
            count += 1;
            tmpCommand = tmpCommand.nextLinkedCommand;
        }
        return count;
    }
}