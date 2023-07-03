using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SphereOriginal;
    void Start()
    {
        //GameObject SphereClone = Instantiate(SphereOriginal);
        Debug.Log("Original X pos: "+SphereOriginal.transform.position.x.ToString());
        Debug.Log("Original Z pos: "+SphereOriginal.transform.position.z.ToString());
        CreateSpheres(2);
        
        
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    // int x_increment = 2;
    // int z_increment = 5;
    // float x_pos = SphereOriginal.transform.position.x;
    // float z_pos = SphereOriginal.transform.position.z;
    float x_increment = 4.0f;
    float z_increment = 4.0f;
    void CreateSpheres(int numSpheres) {
        for (int i = 0; i < numSpheres; i++) {
            GameObject SphereClone = Instantiate(SphereOriginal, new Vector3(SphereOriginal.transform.position.x + x_increment, SphereOriginal.transform.position.y, SphereOriginal.transform.position.z + z_increment), SphereOriginal.transform.rotation);
           
            Debug.Log("Clone X pos: "+SphereClone.transform.position.x.ToString());
            Debug.Log("Clone Z pos: "+SphereClone.transform.position.z.ToString());
            //Debug.Log(SphereClone.transform.position.z);
            x_increment +=8.0f;
            // x_pos = SphereClone.transform.position.x;
            // z_pos = SphereClone.transform.position.z;
        }
    }
}
