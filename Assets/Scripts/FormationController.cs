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
            float x_plus_output = unitVector.x * amplitude * Mathf.Cos(Time.time * speed * 20 * Mathf.Deg2Rad);
            float y_plus_output = unitVector.y * amplitude * Mathf.Cos(Time.time * speed * 20 *Mathf.Deg2Rad);
            Vector3 plus_output = new Vector3(x_plus_output, -y_plus_output, points_[i].z);


            float x_cross_output = unitVector.y * amplitude * Mathf.Sin(Time.time * speed * 20 * Mathf.Deg2Rad);
            float y_cross_output = unitVector.x * amplitude * Mathf.Sin(Time.time * speed * 20 * Mathf.Deg2Rad);
            Vector3 cross_output = new Vector3(x_cross_output, y_cross_output, points_[i].z);

            gameObjects[i].transform.position = points_[i] + percent_plus / 100 * plus_output + percent_cross / 100 * cross_output;
        }


    }



}

