using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubterraneanTest : MonoBehaviour
{

    public bool colliding = false;
    List<Collider> colliders = new List<Collider>();
    [SerializeField] string environmentTag = "Environment";

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == environmentTag)
        {
            colliding = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == environmentTag)
        {
            colliding = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == environmentTag)
        {
            colliding = true;
        }
    }
    
}
