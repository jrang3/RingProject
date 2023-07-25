using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class FormationController : MonoBehaviour
{

    public GameObject prefab;
    [SerializeField] private float amplitude;
    [SerializeField] private float speed;
    //[SerializeField] private float phase_shift;
    [SerializeField] private float percent_plus;
    [SerializeField] private float percent_cross;
    [SerializeField] private float radius; //#Changing radius throughout doesn't have effect an output since radius was used only in the beginning
    [SerializeField] private float percent_breathing;
    [SerializeField] private float percent_longitudunal;
    [SerializeField] private float ampIndex;
    [SerializeField] private float percent_x;
    [SerializeField] private float percent_y;

    private List<GameObject> gameObjects = new List<GameObject>(); //holds list of game objects that are instaitaed
    // Start is called before the first frame update
    private List<Vector3> points_ = new List<Vector3>();
    //private UpdatePosSpheres update_pos_spheres;
    void Start()
    {
        //Creates ring of circles
        Vector3 targetPosition = transform.position; //what we want the spheres to be circled around
        Vector3 compare = new Vector3(9.72f, 6.45f, -11.39f);
        if (targetPosition != compare) { //condition is needed to prevent a sphere being placed at a odd location
            for (int i = 0; i < 10; i++)
            { //populate 10 spheres on screen
                float angle = i * (2 * Mathf.PI / 10); //find an angle for every sphere that we want to populate, divide by 10 because that's number of spheres we want
                float x = targetPosition.x + Mathf.Cos(angle) * radius;
                float y = targetPosition.y + Mathf.Sin(angle) * radius;
                Vector3 pos = new Vector3(x, y, 0);
                GameObject instance = Instantiate(prefab, pos, Quaternion.identity) as GameObject; //best to cast this to Game Object
                gameObjects.Add(instance);
                points_.Add(pos);
            }
        }
    }

    // Update is called once per frame

    void Update()
    {
        //Used to move the circular objects up and down
        for (int i = 0; i < gameObjects.Count; i++)
        {
            Vector3 unitVector = (points_[i] - transform.position).normalized;
            if (speed >= 50)
            {
                speed = 50;
            }
         
            float z_pos = points_[i].z;
            Vector3 plus_output = movePlusMode(unitVector, z_pos);

            Vector3 cross_output =  moveCrossMode(unitVector,z_pos);

            Vector3 breathing_output = moveBreathingMode(unitVector, z_pos);

            Vector3 long_output = MoveLongitudinalMode(points_[i]);

            Vector3 move_x_mode = MoveXMode(unitVector,points_[i]);

            Vector3 move_y_mode = MoveYMode(unitVector, points_[i]);

            gameObjects[i].transform.position = points_[i] + plus_output + cross_output + breathing_output +long_output + move_x_mode +move_y_mode;
        }
    }


    public Vector3 movePlusMode(Vector3 unitVector, float z_pos)
    {
        float x_plus_output = unitVector.x * amplitude * Mathf.Cos(Time.time * speed * 20 * Mathf.Deg2Rad);
        float y_plus_output = unitVector.y * amplitude * Mathf.Cos(Time.time * speed * 20 * Mathf.Deg2Rad);
        Vector3 plus_output = percent_plus /100 *new Vector3(x_plus_output, -y_plus_output, z_pos);
        return plus_output;

    }

    public Vector3 moveCrossMode(Vector3 unitVector, float z_pos)
    {
        float x_cross_output = unitVector.y * amplitude * Mathf.Sin(Time.time * speed * 20 * Mathf.Deg2Rad);
        float y_cross_output = unitVector.x * amplitude * Mathf.Sin(Time.time * speed * 20 * Mathf.Deg2Rad);
        Vector3 cross_output = percent_cross / 100 * new Vector3(x_cross_output, y_cross_output, z_pos);
        return cross_output;
    }

    public Vector3 moveBreathingMode(Vector3 unitVector, float z_pos)
    {
        /**
         * Motion of a particle based on Breathing Mode polarization of a gravitational wave
         * Works on the principle of x = x_0 + (direction of normal to the mesh) * cos(omega * t)
         * And y = y_0 + (direction of normal to the mesh) * cos(omega * t)
         * (Gives out both x and y oscillations, given propagation on z axis)
         * **/

        //Create normal vector of mesh; mesh is translated along this direction
      
        float x_breathing_output = unitVector.x * amplitude * Mathf.Cos(Time.time * speed * 20 * Mathf.Deg2Rad);
        float y_breathing_output = unitVector.y * amplitude * Mathf.Cos(Time.time * speed * 20 * Mathf.Deg2Rad);
        Vector3 breathing_output = percent_breathing / 100 * new Vector3(x_breathing_output, y_breathing_output, z_pos);
        return breathing_output;
    }

    public Vector3 MoveLongitudinalMode(Vector3 initial_pos)
    {
        /**
         * Motion of a particle based on Longitudinal Mode polarization of a gravitational wave
         * (Gives out z oscillations, given propagation on z axis)
         * **/

        //Create normal vector of mesh; mesh is translated along this direction


        float updated_z_pos = initial_pos.z
            + Vector3.forward.z * amplitude * Mathf.Cos(Time.time * speed * 20 * Mathf.Deg2Rad + ampIndex); //got rid of / by Pi and added * 20 in
        Vector3 long_output = percent_longitudunal / 100 * new Vector3(initial_pos.x, initial_pos.y, updated_z_pos);
        return long_output;
    }

    public Vector3 MoveXMode(Vector3 unitVector, Vector3 initial_pos)
    {
        /**
         * Motion of a particle based on Breathing Mode polarization of a gravitational wave
         * Works on the principle of x = x_0 + (direction of normal to the mesh) * cos(omega * t)
         * And y = y_0 + (direction of normal to the mesh) * cos(omega * t)
         * (Gives out both x and y oscillations, given propagation on z axis)
         * **/

        //Create normal vector of mesh; mesh is translated along this direction
        //Vector3 unitVector = (pos - center).normalized;



        Vector3 output;
        output.x = initial_pos.x
            + Vector3.forward.z * Mathf.Cos(ampIndex) * amplitude * Mathf.Cos(Time.time * speed *20* Mathf.Deg2Rad );
        output.y = initial_pos.y;
        output.z = initial_pos.z
            + unitVector.x * amplitude * Mathf.Cos(Time.time * speed * Mathf.Deg2Rad);

        return percent_x / 100 * output;
    }

    public Vector3 MoveYMode(Vector3 unitVector, Vector3 initial_pos)
    {
        /**
         * Motion of a particle based on Breathing Mode polarization of a gravitational wave
         * Works on the principle of x = x_0 + (direction of normal to the mesh) * cos(omega * t)
         * And y = y_0 + (direction of normal to the mesh) * cos(omega * t)
         * (Gives out both x and y oscillations, given propagation on z axis)
         * **/

        //Create normal vector of mesh; mesh is translated along this direction

        Vector3 output;
        output.x = initial_pos.x;
        output.y = initial_pos.y
            - Vector3.forward.z * Mathf.Cos(ampIndex) * amplitude * Mathf.Cos(Time.time * speed * Mathf.Deg2Rad / Mathf.PI);
        output.z = initial_pos.z
            + unitVector.y * amplitude * Mathf.Cos(Time.time * speed * 20 * Mathf.Deg2Rad);

        return percent_y / 100 * output;
    }
}

