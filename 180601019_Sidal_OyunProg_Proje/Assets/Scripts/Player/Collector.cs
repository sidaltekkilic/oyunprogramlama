using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Stash))]
public class Collector : MonoBehaviour
{

    private Stash stashScript;

    private void Awake()
    {
        stashScript = GetComponent<Stash>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            if (other.TryGetComponent(out Collectable collected))
            {
                stashScript.AddStash(collected);
            }
        }
    }

}
