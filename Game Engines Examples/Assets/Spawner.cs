using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    public float radius = 40;

    public int spawnRate = 1;
    
    public int despawnRate = 4;

    public int max = 5;

    public GameObject spawnedTank;

    void Spawn()
    {
        GameObject g = GameObject.Instantiate<GameObject>(spawnedTank);
        Vector3 pos = new Vector3(Random.Range(-radius, radius), 0, Random.Range(-radius, radius));

        g.AddComponent<Rigidbody>();
        g.transform.position = transform.TransformPoint(pos);

        g.transform.parent = this.transform;
        g.tag = "Cube";
    }

    void DeSpawn(){
        GameObject[] tanks = GameObject.FindGameObjectsWithTag("Cube");
        GameObject child = tanks[0].transform.GetChild(0).gameObject;
        Destroy(tanks[0], 7);
    }
    void RemoveCollider()
    {
        GameObject[] tanks = GameObject.FindGameObjectsWithTag("Cube");
        tanks[0].GetComponent<Collider>().enabled = false;
        GameObject child = tanks[0].transform.GetChild(0).gameObject;
        child.GetComponent<Collider>().enabled = false;
        DeSpawn();
        

    }
    // Start is called before the first frame update
    void Start()
    {
        //Invoke("Spawn", 5);
    }

    void OnEnable()
    {
        StartCoroutine(SpawnCoroutine());
        //StartCoroutine(DeSpawnCoroutine());
    }

    int count = 0;

    System.Collections.IEnumerator SpawnCoroutine()
    {
        while(true)
        {
            /*
            count ++;
            if (transform.childCount == max)
            {
                break;
            }
            */
            GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");
            if (cubes.Length < max)
            {
                Spawn();
            }
            yield return new WaitForSeconds(1.0f / (float)spawnRate); 
        }
    }
     System.Collections.IEnumerator DeSpawnCoroutine()
    {
        while(true)
        {
            GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");
            if (cubes.Length  > 0)
            {
                RemoveCollider();
            }

            yield return new WaitForSeconds(despawnRate); 
        }
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
