using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible : Command
{
    public override void Execute()
    {
        StartCoroutine(BecomeInvisible());
    }
    IEnumerator BecomeInvisible()
    {
        // before execute
        spriteRenderer.color = ColorController.instance.onExecuteColor;

        // execute
        Core.instance.SetBool(true);

        Player.instance.Invisible(clockPerExecute);
        yield return new WaitForSeconds(clockPerExecute);

        // after execute
        spriteRenderer.color = originalColor;

        if (nextLinkedCommand != null)
            nextLinkedCommand.Execute();
        else
            Core.instance.SetBool(false);
    }
}
