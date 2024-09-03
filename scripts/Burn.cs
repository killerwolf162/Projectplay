using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Burn : MonoBehaviour
{

    public UnityEvent StartBurn;


    [SerializeField] private int health;
    [SerializeField]  private SpriteRenderer baseRenderer, leaveRenderer, fireRenderer, ashRenderer;
    [SerializeField]  private PolygonCollider2D Collider;

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "FlamethrowerFlame")
            TakeDamage(1);
    }

    private void Update()
    {
        if(baseRenderer != null)
            if (health <= 0)
                StartBurn.Invoke();
    }

    private void TakeDamage(int _damage)
    {
        health -= _damage;
    }

    public void StartBurning()
    {
        Invoke("TurnFireOn", 0f);
        Invoke("TurnToAsh", 3f);
    }

    private void TurnFireOn()
    {
        fireRenderer.enabled = true;
    }

    private void TurnToAsh()
    {
        Destroy(baseRenderer);
        Destroy(leaveRenderer);
        Destroy(fireRenderer);
        Destroy(Collider);
        ashRenderer.enabled = true;
    }


    //public IEnumerator BurningCoroutine()
    //{

    //    yield return new WaitForSeconds(1);

    //    burnTime -= 1;
    //    fireRenderer.enabled = true;

    //    if (burnTime <= 0)
    //    {
            
    //        StopCoroutine(BurningCoroutine());
    //    }


    //}
}
