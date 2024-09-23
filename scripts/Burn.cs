using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Burn : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] private GameObject redParticles, yellowParticles, orangeParticles;
    [SerializeField] private Collider2D collider;
    [SerializeField] private LayerMask burnLayer;

    private int frames = 0;

    private List<GameObject> particles = new List<GameObject>();
    private List<GameObject> burnAbleObjects = new List<GameObject>();

    public bool isBurning = false;

    private void Awake()
    {
        particles.Add(redParticles);
        particles.Add(yellowParticles);
        particles.Add(orangeParticles);        
    }
    private void FixedUpdate()
    {
        if (isBurning != true)
        {      
          if (health <= 0)
            {
                StartBurning();
            }            
        }
        if(isBurning == true)
        {
            SetOtherOnFire();
        }

    }
    private void TakeDamage(float _damage)
    {
        health -= _damage;
    }
    public void StartBurning()
    {
        isBurning = true;
        TurnFireOn();
        
    }
    private void TurnFireOn()
    {      
        if(particles.Count > 0)
        {
            foreach (GameObject _particleSystem in particles)
                _particleSystem.SetActive(true);
        }                     
    }
    public void TurnFireOFF()
    {
        if (particles.Count > 0)
        {
            foreach (GameObject _particleSystem in particles)
                _particleSystem.SetActive(false);
        }
    }

    private void CheckForOthers()
    {
        List<GameObject> _burnableObjects = new List<GameObject>();
        Collider2D[] allColliders;

        float _xPos = this.transform.position.x;
        float _yPos = this.transform.position.y;

        allColliders = Physics2D.OverlapCircleAll(new Vector2(_xPos,_yPos), 2.5f, burnLayer);

        Debug.Log(allColliders.Length);

        foreach (Collider2D _collider in allColliders)
        {
            burnAbleObjects.Add(_collider.transform.gameObject);
        }
    }

    private void SetOtherOnFire()
    {
        CheckForOthers();
        foreach (GameObject burnAbleObject in burnAbleObjects)
        {    
            Burn _burn = burnAbleObject.GetComponent<Burn>();
            if(_burn.isBurning != true)
            {
                frames++;
                if( frames % 100 == 0)
                {
                    Debug.Log("I did damage");
                    _burn.TakeDamage(1f);
                    frames = 0;
                }            
                
            }                        
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "FlamethrowerFlame")
        {
            TakeDamage(1);
        }
    }
}
