using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponAttack : MonoBehaviour
{

    private GameObject Flame;

    public bool isAttacking;

    private void Awake()
    {
        Flame = GameObject.FindGameObjectWithTag("FlamethrowerFlame");
    }

    private void FixedUpdate()
    {
        Attack();
    }

    public void Attack()
    {
        if (isAttacking == true)
            Flame.SetActive(true);
        else
            Flame.SetActive(false);
    }

    public void OnAttack(InputAction.CallbackContext input_value)
    {
        if (input_value.ReadValue<float>() > 0)
            isAttacking = true;
        else
            isAttacking = false;
    }
    
}
