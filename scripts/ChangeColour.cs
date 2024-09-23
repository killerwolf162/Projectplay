using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColour : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rend1;
    [SerializeField] private SpriteRenderer rend2;
    [SerializeField] private SpriteRenderer ashRend;
    [SerializeField] private float burnTime;
    [SerializeField] private float colorChangeTime;

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
            Debug.Log(startColor1);
        }
        if (rend2 != null)
        {
            startColor2 = rend2.color;
            Debug.Log(startColor2);
        }
        endColor = Color.black;

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

            burnTime = burnTime - Time.deltaTime;

            if (burnTime <= 0)
            {
                burn.TurnFireOFF();
                if (rend1 != null)
                    rend1.enabled = false;
                if (rend2 != null)
                    rend2.enabled = false;
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

        yield return new WaitForSeconds(1);
        changeColourInterval += 0.0008f;
        yield return new WaitForSeconds(1);

    }




}
