using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;

public class Ringleader : MonoBehaviour
{
    private Dictionary<string, Action> eventsSpecifics;
    public GameObject hoopPrefab;
    private System.Random random = new System.Random();
    public Coroutine rings;
    public static Ringleader Instance;
    public Animator handsAnimator;
    public Animator faceAnimator;
    GameManager gameManager;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip ringSpawnedAudio;
    [SerializeField] float ringSpawnDelay = 1.2f;
    [SerializeField] AudioSource laughSource;

    private readonly List<string> events = new List<string>
    {
        "ring_of_fire"
    };
    void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    void Start()
    {
        gameManager = GameManager.Instance;

        eventsSpecifics = new Dictionary<string, Action>
        {
            { "ring_of_fire", RingOfFire }
        };
        // Call RandomEvent immediately and then every 5 seconds
        //StartRings();
    }
    public void StartRings()
    {
        rings = StartCoroutine(InvokeRandomEvent());
        laughSource.Play();
    }
    public void StopRings()
    {
        StopCoroutine(rings);
        print("StopRings");
        DetachHoops();
    }
    private IEnumerator<object> InvokeRandomEvent()
    {
        while (true)
        {
            // Call RandomEvent
            RandomEvent();

            // Wait for a random amount of time between 4 and 8 seconds
            yield return new WaitForSeconds(UnityEngine.Random.Range(6f, 12f));
        }
    }
    public List<GameObject> hoops;
    public int hoopCount = 0;

    public void ReduceHoopCount()
    {
        hoopCount -= 1;
    }
    public void RingOfFire()
    {
        print(hoopCount);
        if (hoopCount >= 4)
        {
            return;
        }

        StartCoroutine(RingSpawnRoutine());

        IEnumerator RingSpawnRoutine()
        {
            handsAnimator.SetTrigger("RingOfFireTrigger");
            faceAnimator.SetTrigger("RingOfFireTrigger");

            yield return new WaitForSeconds(ringSpawnDelay);

            hoopCount++;

            Debug.Log("Ring of Fire");
            Vector3 startPosition = transform.position; // Start position for the first hoop
                                                        // Instantiate the hoops at the start position and 15 units to the right and left
            var hoop1 = Instantiate(hoopPrefab,
                new Vector3(0, 10f, -21.2f), Quaternion.Euler(40, 0, 0), transform);
            hoops.Add(hoop1);

            audioSource.PlayOneShot(ringSpawnedAudio);
        }
    }

    private void RandomEvent()
    {
        var eventIndex = random.Next(events.Count);
        eventsSpecifics[events[eventIndex]]();
    }
    public void DetachHoops()
    {
        foreach (GameObject hoop in hoops)
        {
            Destroy(hoop);
        }
        hoops.Clear();
        hoopCount = 0;
    }
}