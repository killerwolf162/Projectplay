using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColour : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rend1;
    [SerializeField] private SpriteRenderer rend2;
    [SerializeField] private SpriteRenderer ashRend;
    [SerializeField] private SpriteRenderer branch1;
    [SerializeField] private SpriteRenderer branch2;
    [SerializeField] private SpriteRenderer branch3;
    [SerializeField] private SpriteRenderer branch4;
    [SerializeField] private SpriteRenderer branch5;
    [SerializeField] private SpriteRenderer branch6;
    [SerializeField] private SpriteRenderer branch7;

    [SerializeField] private float burnTime;
    [SerializeField] private float colorChangeTime;

    private List<SpriteRenderer> spriteList = new List<SpriteRenderer>();

    private float changeColourInterval = 0.0008f;
    private Burn burn;
    private Collider2D collider;
    private float health;


    private Color startColor1;
    private Color startColor2;
    private Color endColor;

    private void Awake()
    {
        if (rend1 != null)
        {
            startColor1 = rend1.color;
        }
        if (rend2 != null)
        {
            startColor2 = rend2.color;
        }
        endColor = Color.black;

        if(branch1 != null)
        {
            spriteList.Add(branch1); spriteList.Add(branch2); spriteList.Add(branch3); spriteList.Add(branch4); spriteList.Add(branch5); spriteList.Add(branch6); spriteList.Add(branch7);
        }

        burn = GetComponent<Burn>();
        collider = GetComponent<Collider2D>();

        health = burn.health;
        health = health / 10;
    }

    private void Update()
    {
        if (burn.isBurning == true)
        {
            StartCoroutine(changeColourToBlack());

            burnTime -= Time.deltaTime;

            if(burnTime <= 5)
            {
                if (rend2 != null)
                    rend2.enabled = false;
            }

            if (burnTime <= 0)
            {
                burn.TurnFireOFF();
                if (rend1 != null)
                    rend1.enabled = false;

                foreach (SpriteRenderer sprite in spriteList)
                {
                    sprite.enabled = false;
                }

                ashRend.enabled = true;
                this.enabled = false;
                burn.enabled = false;
                collider.enabled = false;
                
            }
        }
    }

    private IEnumerator changeColourToBlack()
    {
        if (rend1 != null)
            rend1.color = Color.Lerp(startColor1, endColor, changeColourInterval/health);
        if (rend2 != null)
            rend2.color = Color.Lerp(startColor2, endColor, changeColourInterval/health);
        if(branch1 != null)
        {
            foreach (SpriteRenderer sprite in spriteList)
            {
                sprite.color = Color.Lerp(startColor1, endColor, changeColourInterval / health);
            }
        }

        yield return new WaitForSeconds(1);
        changeColourInterval += 0.0008f;
        yield return new WaitForSeconds(1);

    }




}
