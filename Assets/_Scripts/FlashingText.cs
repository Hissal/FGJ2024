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

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(TextFlash());
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
        StartCoroutine(TextFlash());
    }

    private void OnDisable()
    {
        StopCoroutine(TextFlash());
    }
}
