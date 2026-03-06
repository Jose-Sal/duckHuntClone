using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    //this script will be used to "destroy" the game object (we'll implement object pooling after basic function works)
    //when the game object is destroyed , we'll add points within the UI and "destroy" the game object

    [SerializeField]UIScript uiScript;

    private void Start()
    {
        uiScript = FindObjectOfType<UIScript>();
    }

    //change name later
    public void DestroycurrentObject()
    {
        //add points to the UI here
        uiScript.AddMoney();
        StartCoroutine(DestroyAfterDelay(0.5f));
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
