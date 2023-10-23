using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private Vector2 scrollSpeed;
    [SerializeField] private RawImage image;

    private void Update()
    {
        image.material.mainTextureOffset = new Vector2(image.material.mainTextureOffset.x + scrollSpeed.x * Time.deltaTime, image.material.mainTextureOffset.y + scrollSpeed.y * Time.deltaTime);
    }

    private void OnDisable()
    {
        // reset offset
        image.material.mainTextureOffset = Vector2.zero;
    }
}
