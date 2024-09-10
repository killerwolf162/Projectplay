using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Burn : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private SpriteRenderer spriteRenderer1, spriteRenderer2, ashRenderer;
    [SerializeField] private GameObject redParticles, yellowParticles, orangeParticles;
    [SerializeField] private Collider2D collider;
    [SerializeField] private float burnRate;
    [SerializeField] private float burnDelay;
    [SerializeField] private float stopBurnDelay;
    [SerializeField] private LayerMask burnLayer;

    private List<GameObject> particles = new List<GameObject>();
    private List<GameObject> burnAbleObjects = new List<GameObject>();

    public bool isBurning = false;

    private void Awake()
    {
        particles.Add(redParticles);
        particles.Add(yellowParticles);
        particles.Add(orangeParticles);
    }
    private void Update()
    {
        if (isBurning != true)
        {      
            if (spriteRenderer1 != null)
                if (health <= 0)
                    Invoke("StartBurning", 0f);
        }

    }
    private void TakeDamage(int _damage)
    {
        health -= _damage;
    }
    public void StartBurning()
    {
        isBurning = true;
        TurnFireOn();
        Invoke("SetOtherOnFire", 3f);
        Invoke("TurnToAsh", stopBurnDelay);
    }
    private void TurnFireOn()
    {      
        if(particles.Count > 0)
        {
            foreach (GameObject _particleSystem in particles)
                _particleSystem.SetActive(true);
        }   
        if(spriteRenderer1 != null)
            StartCoroutine(changeSpriteColorToBlack(spriteRenderer1));  
        if (spriteRenderer2 != null)
            StartCoroutine(changeSpriteColorToBlack(spriteRenderer2));           
    }
    private void TurnToAsh()
    {
        if (spriteRenderer1 != null)
            spriteRenderer1.enabled = false;
        if (spriteRenderer2 != null)
            spriteRenderer2.enabled = false;

        collider.enabled = false;

        foreach (GameObject _particleSystem in particles)
        {
            _particleSystem.SetActive(false);
        }

        ashRenderer.enabled = true;
        Destroy(this.gameObject.GetComponent<Burn>());
    }

    private Color changeColorToBlack(Color _color)
    {
        Color _newColor = new Color();

        _newColor = _color + new Color(-burnRate, -burnRate, -burnRate, 1);

        return _newColor;
    }

    private void CheckForOthers()
    {
        List<GameObject> _burnableObjects = new List<GameObject>();
        Collider2D[] allColliders;

        float _xPos = this.transform.position.x;
        float _yPos = this.transform.position.y;

        allColliders = Physics2D.OverlapCircleAll(new Vector2(_xPos,_yPos), 5f, burnLayer);

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
                _burn.isBurning = true;
                _burn.TurnFireOn();
                _burn.StartCoroutine(changeSpriteColorToBlack(_burn.spriteRenderer1));
                _burn.StartCoroutine(changeSpriteColorToBlack(_burn.spriteRenderer2));
                _burn.Invoke("SetOtherOnFire", 3f);
                _burn.Invoke("TurnToAsh", stopBurnDelay);
            }                        
        }
    }
    private IEnumerator changeSpriteColorToBlack(SpriteRenderer _spriteRenderer)
    {
        yield return new WaitForSeconds(burnDelay);

        Color _newColor = changeColorToBlack(_spriteRenderer.color);
        Debug.Log(_newColor);
        _spriteRenderer.color = _newColor;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "FlamethrowerFlame")
        {
            TakeDamage(1);
        }

        //if (spriteRenderer1 != null)
        //    if (health <= 0)
        //        StartBurning();
            


    }
}
