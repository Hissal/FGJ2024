using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoop : MonoBehaviour
{
    float forceMagnitude = 500f;
    System.Random random = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            float randomFloatX = 0.3f + (float)random.NextDouble() * 0.7f;
            randomFloatX *= Mathf.Pow(-1, random.Next(2)); // Randomly positive or negative
            float randomFloatZ = 0.3f + (float)random.NextDouble() * 0.7f;
            randomFloatZ *= Mathf.Pow(-1, random.Next(2)); // Randomly positive or negative
            Vector3 forceDirection = new Vector3(randomFloatX, 0, randomFloatZ);
            rb.AddForce(forceDirection * forceMagnitude);
            Debug.Log("Added force to the hoop!");
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
