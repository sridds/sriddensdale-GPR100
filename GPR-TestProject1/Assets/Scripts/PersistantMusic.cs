using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantMusic : MonoBehaviour
{
    private static PersistantMusic instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if(instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }
}
