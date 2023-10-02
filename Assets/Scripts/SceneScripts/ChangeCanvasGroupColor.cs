using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ChangeCanvasGroupColor : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private Color targetColor = new Color(0f, 0f, 0f);
    private float duration = 1f;

    private Color initialColor;

    private void Start()
    {
        canvasGroup = gameObject.GetComponentsInChildren<CanvasGroup>()[0];
        
        // Store the initial color of the Canvas Group
        initialColor = canvasGroup.GetComponent<Graphic>().color;
        // Start the color animation
        StartCoroutine(AnimateColor());
    }

    private IEnumerator<object> AnimateColor()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate the current color based on the elapsed time and duration
            float t = elapsedTime / duration;
            Color currentColor = Color.Lerp(initialColor, targetColor, t);

            // Get all child UI elements of the CanvasGroup
            Graphic[] graphics = canvasGroup.GetComponentsInChildren<Graphic>();

            // Change the color of each child UI element
            foreach (Graphic graphic in graphics)
            {
                graphic.color = currentColor;
            }
            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}