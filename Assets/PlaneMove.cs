using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMove : MonoBehaviour
{
    public float speed;
    private float maxSpeed;
    public float grav;
    public float speedGain;
    public float brakePower;
    public float rotSpeed;
    public float tailSpeed;

    public bool hasCrashed;

    //public GameObject cam;

    public GameObject ammo;
    public GameObject ammo2;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("plane pilot script added to: " + gameObject.name);
        maxSpeed = 300;
        ammo = GameObject.Find("SingleLine-LightSaber Blue");
        ammo2 = GameObject.Find("SingleLine-LightSaber Purble");

        hasCrashed = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlaneMovement();
        if (hasCrashed)
        {
            crash();
        }
    }

    void crash()
    {
        GameObject[] parts = GameObject.FindGameObjectsWithTag("Parts");
         for(int i = 0; i < parts.Length; i++)
        {
            parts[i].GetComponent<Rigidbody>().isKinematic = false;
            parts[i].transform.SetParent(null);
            speed = 0;
        }
    }

    void PlaneMovement()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        speed -= transform.forward.y * grav * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && hasCrashed == false)
        {
            if (speed < maxSpeed)
            {
                speed += speedGain * Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) && hasCrashed == false)
        {
            if (speed > 0)
            {
                speed -= brakePower * Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.W) && hasCrashed == false)
        {
            transform.Rotate(Vector3.right * rotSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S) && hasCrashed == false)
        {
            transform.Rotate(-Vector3.right * rotSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A) && hasCrashed == false)
        {
            transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) && hasCrashed == false)
        {
            transform.Rotate(-Vector3.forward * rotSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Q) && hasCrashed == false)
        {
            transform.Rotate(-Vector3.up * tailSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E) && hasCrashed == false)
        {
            transform.Rotate(Vector3.up * tailSpeed * Time.deltaTime);
        }

        //does the shooty shooty
        if (Input.GetMouseButtonDown(0) && hasCrashed != true)
        {
            GameObject clone = GameObject.Instantiate(ammo, transform.position, transform.rotation);
            //clone.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
            clone.GetComponent<Rigidbody>().AddForce(transform.forward * 30000);
            //if hit target
            if (clone.gameObject.tag.Equals("Targets"))
            {
                Destroy(clone);
            }
            if (clone.gameObject.tag.Equals("Terrain"))
            {
                Destroy(clone);
            }
            Destroy(clone, 3);
        }

        if (Input.GetMouseButtonDown(1) && hasCrashed != true)
        {
            GameObject clone = GameObject.Instantiate(ammo2, transform.position, transform.rotation);
            //clone.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
            clone.GetComponent<Rigidbody>().AddForce(transform.forward * 30000);
            //if hit targets
            if (clone.gameObject.tag.Equals("Targets"))
            {
                Destroy(clone);
            }
            if (clone.gameObject.tag.Equals("Terrain"))
            {
                Destroy(clone);
            }
            Destroy(clone, 3);
        }

    }

    //crash animation where parts break
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Collider>().name.Contains("Terrain"))
        {
            hasCrashed = true;
        }
    }
}
