using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    [SerializeField]
    private bool isInvisible;
    [SerializeField]
    private float invisibleTransition;

    private SpriteRenderer sr;
    private Color originalColor;
    private Color invisibleColor;


    private void Awake()
    {
        instance = this;
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        invisibleColor = new Color(originalColor.r, originalColor.g, originalColor.b, invisibleTransition);
    }



    public void Invisible(float invisibleTime)
    {
        if (isInvisible) return;
        isInvisible = true;
        StartCoroutine(InvisibleAnimate(invisibleTime));
    }

    public void visible(float invisibleTime)
    {
        if (!isInvisible) return;
        isInvisible = false;
        StartCoroutine(InvisibleAnimate(invisibleTime));    
    }
    
    private IEnumerator InvisibleAnimate(float invisibleTime)
    {
        float timeFade = 0f;
        // become to invisible
        if (isInvisible)
        {
            while(timeFade < invisibleTime)
            {
                timeFade += Time.deltaTime;
                sr.color = Color.Lerp(originalColor, invisibleColor, timeFade / invisibleTime);
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
        // become to visible
        else
        {
            while(timeFade < invisibleTime)
            {
                timeFade += Time.deltaTime;
                sr.color = Color.Lerp(invisibleColor, originalColor, timeFade / invisibleTime);
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
    }

}
