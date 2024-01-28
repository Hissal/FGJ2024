using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopAnchor : MonoBehaviour
{
    private Transform tryingPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StorePlayer(Transform player)
    {
        Debug.Log("Storing player!");
        tryingPlayer = player;
    }
    public Transform GetTryingPlayer()
    {
        return tryingPlayer;
    }

    public void DetachHoop()
    {
        Debug.Log("Detaching hoop!");
        transform.parent = null;
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }
}