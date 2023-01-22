using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(Stash))]

public class Payer : MonoBehaviour
{
    private Stash stashScript;
    private float nextTimeToPay = 0;
    private float delayTimePayment = 0.1f;
    private void Awake()
    {
        stashScript = GetComponent<Stash>();
        nextTimeToPay = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (stashScript.CollectedCount <= 0)
            return;


        if (other.CompareTag("Unlockable"))
        {
            nextTimeToPay = Time.time + delayTimePayment;

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (stashScript.CollectedCount <= 0)
            return;


        if (other.CompareTag("Unlockable"))
        {
            if (Time.time < nextTimeToPay)
                return;

            nextTimeToPay = Time.time + delayTimePayment;

            if (other.TryGetComponent(out UnlockArea unlockable))
            {
                StartPayment(unlockable);
            }
        }
    }

    private void StartPayment(UnlockArea unlockable)
    {
        if (unlockable.unlockableData.RemainingPrice <= 0)
            return;

        var stashable = stashScript.RemoveStash();
        if (stashable == null)
            return;

        unlockable.Pay(stashable);
    }
}
