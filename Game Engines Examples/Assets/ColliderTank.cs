using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTank : MonoBehaviour
{

    void OnCollisionEnter(Collision c)
    {
        //Debug.Log("Collision enter!");
        //Debug.Log(c.gameObject.tag);
        if (c.gameObject.tag == "Bullet")
        {
            Debug.Log("Collides with Bullet");
            RemoveCollider();
        }
    }
    void DeSpawn(){
        GameObject currentTank = this.gameObject;
        Destroy(currentTank, 2);
    }
    
    void RemoveCollider()
    {
        //GameObject[] tanks = GameObject.FindGameObjectsWithTag("Cube");
        GameObject currentTank = this.gameObject;
        currentTank.GetComponent<Collider>().enabled = false;
        GameObject child = currentTank.transform.GetChild(0).gameObject;
        Rigidbody turret = child.AddComponent<Rigidbody>();
        turret.useGravity = true;
        child.GetComponent<Collider>().enabled = false;
        DeSpawn();
        

    }
    void OnCollisionStay(Collision c)
    {
        //Debug.Log("Collision Stay");
    }

    void OnCollisionExit(Collision c)
    {
        //Debug.Log("Collision Exit");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   


}
