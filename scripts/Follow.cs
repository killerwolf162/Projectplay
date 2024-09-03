using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

    [SerializeField] private Transform playerPOS;

    private void FixedUpdate()
    {
        FollowPlayer();
    }
    private void FollowPlayer()
    {
        this.transform.position = new Vector3(playerPOS.transform.position.x , playerPOS.transform.position.y, -10);

    }
    
}
