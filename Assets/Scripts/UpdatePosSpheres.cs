using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePosSpheres : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 MovePlusMode(Vector3 sphere_pos) {
        float h_plus = 3.0f*Mathf.Cos(3.0f*Time.time);
        float h_cross = 3.0f*Mathf.Sin(3.0f*Time.time);
        Vector3 output;
        output.x = sphere_pos.x + (h_plus*sphere_pos.x) / 1250;
        output.y = (-h_plus*sphere_pos.y) / 10000;
        output.z = 0;
        return output;

    }


}
