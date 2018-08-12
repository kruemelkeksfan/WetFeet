using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;

public class BuildableStructure : MonoBehaviour
{


    public List<Collider> collidingEnviroment = new List<Collider>();
    List<Collider> collidingStructures = new List<Collider>();
    [SerializeField] string environmentTag = "Environment";
    [SerializeField] string structureTag = "Structure";
    [SerializeField] SubterraneanTest subterraneanTest;
    SimpleCameraController cameraController;

    bool hasPlaced = false;


    private void Start()
    {
        cameraController = GameObject.FindObjectOfType<SimpleCameraController>();
    }
    private void FixedUpdate()
    {
        if (!hasPlaced)
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y);
            Vector3 mouseRPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            print(mouseRPosition);
            transform.position = new Vector3(mouseRPosition.x, transform.position.y, mouseRPosition.z);
            if (Input.GetMouseButtonDown(0) && collidingEnviroment.Count >= 4 && collidingStructures.Count == 0)
            {
                hasPlaced = true;
            }
            if (Input.GetMouseButtonDown(1))
            {
                Object.Destroy(gameObject);
            }
        }
        
    }
    private void Update()
    {
        print(transform.position.y);
        if (transform.position.y < 0)
        {
            print("correct 0");
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        else if (subterraneanTest.colliding && transform.position.y >= 0)
        {
            transform.position = transform.position + Vector3.up * 3;
            print("correct up" + transform.position.y);
        }
        else if (collidingEnviroment.Count == 0 && transform.position.y > 4)
        {
            int count = 0;
            while (collidingEnviroment.Count == 0 && transform.position.y > 4 && count < 25)
            {
                count++;
                transform.position = transform.position + (Vector3.down * 5);
            }
        }
        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y / 5) * 5, Mathf.RoundToInt(transform.position.z));
    }
    public void StartPositioning()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == environmentTag)
        {
            collidingEnviroment.Add(other);
        }
        else if (other.tag == structureTag)
        {
            collidingStructures.Add(other);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == environmentTag)
        {
            collidingEnviroment.Remove(other);
        }
        else if (other.tag == structureTag)
        {
            collidingStructures.Remove(other);
        }
    }
}
