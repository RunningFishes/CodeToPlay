using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class If : Command
{
    public Command linkedIfCommand;
    enum Condition
    {
        isTrue,
        isFalse,
        isHaveSword,
        isHaveShield,
        isHaveBow,
        isHaveKey,
        isHavePotion,
        isHaveItem,
        
    }
    [SerializeField]
    private Condition condition;

    public override void Execute()
    {
        StartCoroutine(Ifing());
    }

    IEnumerator Ifing()
    {
        // if is running
        Core.instance.SetBool(true);

        // create inner status
        int layer = Core.instance.Size();
        Core.instance.Push(false);

        spriteRenderer.color = ColorController.instance.onExecuteColor;
        yield return new WaitForSeconds(clockPerExecute);
        spriteRenderer.color = originalColor;

        //##### Check If #####
        if (IfHandler())
        {
            // work on this command
            linkedIfCommand?.Execute();
            // wait until upper command complete
            while (Core.instance.GetBool(layer))
            {
                yield return null;
            }
        }
        //####################

        // remove inner status
        Core.instance.Pop();

        // if have next work on next command
        if (nextLinkedCommand != null)
            nextLinkedCommand.Execute();
        // no next command so this indent is complete
        else
            Core.instance.SetBool(false);
    }

    public void SetLinkedIfCommand(Command command)
    {
        linkedIfCommand = command;
    }

    public int GetSizeLinkedIfCommand()
    {
        if (linkedIfCommand == null)
        {
            return 0;
        }
        return linkedIfCommand.GetSizeCommands();
    }
    private bool IfHandler()
    {
        if (condition == Condition.isTrue)
        {
            return true;
        }
        else if(condition == Condition.isFalse)
        {
            return false;
        }
        else if (condition == Condition.isHaveSword)
        {

        }
        else if (condition == Condition.isHaveShield)
        {

        }
        else if (condition == Condition.isHaveBow)
        {

        }
        else if (condition == Condition.isHaveKey)
        {

        }
        else if (condition == Condition.isHavePotion)
        {

        }
        else if (condition == Condition.isHaveItem)
        {

        }
        return false;
    }
}
