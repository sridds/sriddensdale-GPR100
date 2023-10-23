using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    TopDownMovement player;

    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);
    [SerializeField] private Vector2 cameraBounds;

    private void Start()
    {
        player = FindObjectOfType<TopDownMovement>();
    }

    void Update()
    {
        transform.position = player.transform.position + offset;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -cameraBounds.x, cameraBounds.x), Mathf.Clamp(transform.position.y, -cameraBounds.y, cameraBounds.y), offset.z);
    }
}
