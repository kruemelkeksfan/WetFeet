using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubterraneanTest : MonoBehaviour
{

    public bool Colliding
    {
        get { return colliding; }
    }

    [SerializeField] string environmentTag = "Environment";

    bool colliding = false;

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
