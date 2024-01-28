using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlashingText : MonoBehaviour
{
    [SerializeField] float aplhaChangeAmount = 0.025f;
    [SerializeField] float lowestAlpha = 0.1f;
    [SerializeField] float highestAlpha = 1f;

    TextMeshProUGUI text;

    Coroutine flashRoutine;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        flashRoutine = StartCoroutine(TextFlash());
    }

    IEnumerator TextFlash()
    {
        while (text.alpha > lowestAlpha) 
        {
            yield return new WaitForFixedUpdate();
            text.alpha -= aplhaChangeAmount;
        }

        while (text.alpha < highestAlpha)
        {
            yield return new WaitForFixedUpdate();
            text.alpha += aplhaChangeAmount;
        }

        StartCoroutine(TextFlash());
    }

    private void OnEnable()
    {
        StopCoroutine(flashRoutine);
        flashRoutine = StartCoroutine(TextFlash());
    }

    private void OnDisable()
    {
        StopCoroutine(flashRoutine);
    }
}
