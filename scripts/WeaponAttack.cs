using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponAttack : MonoBehaviour
{

    
    [SerializeField] private GameObject redParticles, yellowParticles, orangeParticles;
    [SerializeField] private GameObject FlameHitbox;

    private List<GameObject> particles = new List<GameObject>();

    public bool isAttacking;

    private void Awake()
    {
        particles.Add(redParticles);
        particles.Add(yellowParticles);
        particles.Add(orangeParticles);
    }

    private void FixedUpdate()
    {
        Attack();
    }

    public void Attack()
    {
        if (isAttacking == true)
        {
            foreach(GameObject particleSystem in particles)
            {
                particleSystem.SetActive(true);
            }
            FlameHitbox.SetActive(true);
        }
        else
        {
            foreach (GameObject particleSystem in particles)
            {
                particleSystem.SetActive(false);
            }
            FlameHitbox.SetActive(false);
        }

    }

    public void OnAttack(InputAction.CallbackContext input_value)
    {
        if (input_value.ReadValue<float>() > 0)
            isAttacking = true;
        else
            isAttacking = false;
    }
    
}
