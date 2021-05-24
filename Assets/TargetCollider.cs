using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //if laser hits target
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals("Lasers"))
        {
            ScoreScript.scoreValue += 1;
            Destroy(col.gameObject);
        }
    }
}
