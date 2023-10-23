using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    [SerializeField] private float time;

    void Start()
    {
        Invoke(nameof(Destroy), time);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
