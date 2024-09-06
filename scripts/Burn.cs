using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Burn : MonoBehaviour
{
    public UnityEvent StartBurn;

    [SerializeField] private float health;
    [SerializeField] private SpriteRenderer spriteRenderer1, spriteRenderer2, ashRenderer;
    [SerializeField] private GameObject redParticles, yellowParticles, orangeParticles;
    [SerializeField] private Collider2D collider;
    [SerializeField] private float burnRate;
    [SerializeField] private float burnDelay;

    private List<GameObject> particles = new List<GameObject>();

    private void Awake()
    {
        particles.Add(redParticles);
        particles.Add(yellowParticles);
        particles.Add(orangeParticles);
    }
    private void Update()
    {
        if(spriteRenderer1 != null)
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
        Invoke("TurnToAsh", 5f);
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

    private IEnumerator changeSpriteColorToBlack(SpriteRenderer _spriteRenderer)
    {
        if (_spriteRenderer.color.g < 0)
        {
            StopAllCoroutines();
        }
        yield return new WaitForSeconds(burnDelay);

        Debug.Log(_spriteRenderer.color);
        _spriteRenderer.color = changeColorToBlack(_spriteRenderer.color);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "FlamethrowerFlame")
            TakeDamage(1);
    }
}
