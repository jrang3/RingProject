using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class FormationController : MonoBehaviour
{

    public GameObject prefab;
    [SerializeField] private float amplitude;
    [SerializeField] private float speed;
    [SerializeField] private float phase_shift;
    [SerializeField] private float percent_plus;
    [SerializeField] private float percent_cross;
    [SerializeField] private float radius; //#Changing radius throughout doesn't have effect an output since radius was used only in the beginning

  
    private List<GameObject> gameObjects = new List<GameObject>(); //holds list of game objects that are instaitaed
    // Start is called before the first frame update
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
            if(targetPosition != compare) { //For some reason an extra sphere at compare location is being instatiated (prevents that from happening)
                GameObject instance = Instantiate(prefab);
                gameObjects.Add(instance);
                instance.transform.position = targetPosition;
            }
        }
    }

    // Update is called once per frame

    void Update()
    {
        //Used to move the circular objects up and down
        for(int i = 0; i < gameObjects.Count; i++) {

            //take difference between game object pos and the empty ring position and normalize the vector (converting that to unit vector)
         
            Vector3 unitVector = (gameObjects[i].transform.position - transform.position).normalized;
            float h_plus = amplitude*unitVector.magnitude*Mathf.Cos(speed*Time.time + phase_shift);  
            float h_cross = amplitude*unitVector.magnitude*Mathf.Sin(speed*Time.time + phase_shift);
            float x_pos = gameObjects[i].transform.position.x;
            float y_pos = gameObjects[i].transform.position.y;
            gameObjects[i].transform.position += new Vector3((percent_plus / 100 * h_plus * x_pos + percent_cross / 100 * h_cross * y_pos) / 1000, (percent_cross / 100 * h_cross * x_pos - percent_plus / 100 * h_plus * y_pos) / 10000, 0);


            
        }


    }
}