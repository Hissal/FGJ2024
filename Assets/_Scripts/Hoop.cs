using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoop : MonoBehaviour
{
    System.Random random = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // float randomX = (float)random.NextDouble() * 2 - 1; // Random float between -1 and 1
            // float randomZ = (float)random.NextDouble() * 2 - 1; // Random float between -1 and 1
            // Vector3 forceDirection = new Vector3(randomX, 0, randomZ);
            // float forceMagnitude = 1500f;
            // rb.AddForce(forceDirection * forceMagnitude);
            // Debug.Log("Added force to the hoop!");
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HoopAnchor hoopAnchor = transform.parent.GetComponent<HoopAnchor>();
            if (hoopAnchor.GetTryingPlayer() != other.transform)
            {
                Debug.Log("Player is not the one who was trying!");
                return;
            }
            Debug.Log("Player has passed through the hoop!");
            // Increase the score of the player who passed through the hoop
            GameManager.Instance.IncreasePlayerScore(other.transform, 1);
            // Detach all hoops
            if (hoopAnchor != null)
            {
                hoopAnchor.DetachHoop();
            }
        }
    }
}
