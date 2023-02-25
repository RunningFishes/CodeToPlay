using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private TextMeshPro text;
    private Color originalTextColor;
    public virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        clockPerExecute = Core.instance.clockPerExecute;

        text = GetComponentInChildren<TextMeshPro>();
        originalTextColor = text.color;
    }
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
        while (tmpCommand.nextLinkedCommand != null)
        {
            tmpCommand = tmpCommand.nextLinkedCommand;
        }
        return tmpCommand;
    }
    public virtual int GetSizeCommands()
    {
        int count = 0;
        Command tmpCommand = this;
        while (tmpCommand != null)
        {
            // get in loop command size
            if (tmpCommand.gameObject.tag == "Loop" && tmpCommand.GetComponent<Loop>().linkedLoopCommand != null)
            {
                count += tmpCommand.GetComponent<Loop>().linkedLoopCommand.GetSizeCommands();
            }
            else if (tmpCommand.gameObject.tag == "If" && tmpCommand.GetComponent<If>().linkedIfCommand != null)
            {
                count += tmpCommand.GetComponent<If>().linkedIfCommand.GetSizeCommands();
            }
            else if (tmpCommand.gameObject.tag == "Function" && tmpCommand.GetComponent<Function>().linkedFunctionCommand != null)
            {
                count += tmpCommand.GetComponent<Function>().linkedFunctionCommand.GetSizeCommands();
            }
            count += 1;
            tmpCommand = tmpCommand.nextLinkedCommand;
        }
        return count;
    }
    public virtual void SetTransparent(bool isTransparent)
    {
        if (isTransparent)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, ColorController.instance.onHoldedTransparent);
            text.color = new Color(text.color.r, text.color.g, text.color.b, ColorController.instance.onHoldedTransparent);
        }
        else
        {
            spriteRenderer.color = originalColor;
            text.color = originalTextColor;
        }
    }
}