using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIBlocks : MonoBehaviour
{
    public UnityEvent FunctionToBeCalled;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            FunctionToBeCalled.Invoke();
        }
    }
}
