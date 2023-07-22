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


    private List<GameObject> gameObjects = new List<GameObject>(); //holds list of game objects that are instaitaed
    // Start is called before the first frame update
    private List<Vector3> points_ = new List<Vector3>();
    //private UpdatePosSpheres update_pos_spheres;
    void Start()
    {
        //Creates ring of circles
        Vector3 targetPosition = transform.position; //what we want the spheres to be circled around
        for (int i = 0; i < 10; i++)
        { //populate 10 spheres on screen
            float angle = i * (2 * Mathf.PI / 10); //find an angle for every sphere that we want to populate, divide by 10 because that's number of spheres we want

            //Users have ability to change radius of ring (offset for each sphere)
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            targetPosition = new Vector3(targetPosition.x + x, targetPosition.y + y, 0);
            Vector3 compare = new Vector3(9.72f, 6.45f, 0.00f);
            if (targetPosition != compare)
            { //For some reason an extra sphere at compare location is being instatiated (prevents that from happening)
                GameObject instance = Instantiate(prefab);
                gameObjects.Add(instance);
                instance.transform.position = targetPosition;
                points_.Add(targetPosition);
            }

        }
    }

    // Update is called once per frame

    void Update()
    {
        //Used to move the circular objects up and down
        for (int i = 0; i < gameObjects.Count; i++)
        {

            //take difference between game object pos and the empty ring position and normalize the vector (converting that to unit vector)

            Vector3 unitVector = (points_[i] - transform.position).normalized;

            float x_plus_output = unitVector.x * amplitude * Mathf.Cos(Time.time * speed * Mathf.Deg2Rad / Mathf.PI);
            float y_plus_output = unitVector.y * amplitude * Mathf.Cos(Time.time * speed * Mathf.Deg2Rad / Mathf.PI);
            Vector3 plus_output = new Vector3(x_plus_output, -y_plus_output, points_[i].z);


            float x_cross_output = unitVector.y * amplitude * Mathf.Sin(Time.time * speed * Mathf.Deg2Rad / Mathf.PI);
            float y_cross_output = unitVector.x * amplitude * Mathf.Sin(Time.time * speed * Mathf.Deg2Rad / Mathf.PI);
            Vector3 cross_output = new Vector3(x_cross_output, y_cross_output, points_[i].z);

            gameObjects[i].transform.position = points_[i] + percent_plus / 100 * plus_output + percent_cross / 100 * cross_output;


            //New Code (Dhruv's Implementation also doesn't seem to work very similar to what I did above
            //Vector3 new_plus_output = MovePlusMode(points_[i], transform.position);
            //Vector3 new_cross_ouput = MoveCrossMode(points_[i], transform.position);

            // gameObjects[i].transform.position.x = points_[i].x + (percent_plus / 100 * new_plus_output.x + percent_cross / 100 * new_cross_ouput.x) / 100;
            //gameObjects[i].transform.position = points_[i] + percent_plus / 100 * new_plus_output / 100 + percent_cross / 100 * new_cross_ouput / 1000;



        }


    }

    public Vector3 MovePlusMode(Vector3 pos, Vector3 center)
    {
        
        Vector3 unitVector = (pos - center).normalized;


        Vector3 output;
        output.x = pos.x
            + unitVector.x * amplitude * Mathf.Cos(Time.time * speed * Mathf.Deg2Rad);
        output.y = pos.y
            - unitVector.y * amplitude * Mathf.Cos(Time.time * speed * Mathf.Deg2Rad);
        output.z = pos.z;

        return output;
    }

    public Vector3 MoveCrossMode(Vector3 pos, Vector3 center)
    {
      

        //Create normal vector of mesh; mesh is translated along this direction
        Vector3 unitVector = (pos - center).normalized;
        //Vector3 unitVector = new Vector3(-unitVectory.y, unitVectory.x, 0);


        Vector3 output;
        output.x = pos.x
        + unitVector.y * amplitude * Mathf.Sin(Time.time * speed * Mathf.Deg2Rad);
        output.y = pos.y
            + unitVector.x * amplitude * Mathf.Sin(Time.time * speed * Mathf.Deg2Rad);
        output.z = pos.z;

        return output;
    }


}

