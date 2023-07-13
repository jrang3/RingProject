using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour
{

    public GameObject prefab;
    [SerializeField] private float amplitude;
    [SerializeField] private float speed;
    [SerializeField] private float phase_shift;
    [SerializeField] private float percent_plus;
    [SerializeField] private float percent_cross;
    [SerializeField] private float radius; //#Changing radius throughout doesn't have effect an output since radius was used only in the beginning

    //private GameObject[] instantiated_objs;
    private List<GameObject> gameObjects = new List<GameObject>(); //holds list of game objects that are instaitaed
    // Start is called before the first frame update
    //private UpdatePosSpheres update_pos_spheres;
    void Start()
    {
        //Creates ring of circles
        this.CircleFormation();
        //update_pos_spheres = GetComponent<UpdatePosSpheres>();
        //Debug.Log(gameObjects[0].transform.position);


        
    }

    // Update is called once per frame

    void Update()
    {
        //Used to move the circular objects up and down
        for(int i = 0; i < gameObjects.Count; i++) {
            
            // float gravitational_constant = 6.67430f * Mathf.Pow(10,-11);
            // float speed_of_light = 299792458.0f;
            // float radius = 10.0f;

            // float c = (gravitational_constant) / (2*Mathf.Pow(speed_of_light, 4 * radius));
            //c seems to be very small causing output to be 0 
            

            //Users have the ability to change phase shift, percent plus/cross mode, amp, and speed (Duplicate in the ring of spheres seems to be showing up)
            float h_plus = amplitude*Mathf.Cos(speed*Time.time + phase_shift);  
            float h_cross = amplitude*Mathf.Sin(speed*Time.time + phase_shift);
            float x_pos = gameObjects[i].transform.position.x;
            float y_pos = gameObjects[i].transform.position.y;
            gameObjects[i].transform.position+=new Vector3(( percent_plus/100 *h_plus*x_pos +  percent_cross/100*h_cross * y_pos) / 1250, (percent_cross/100 *h_cross * x_pos - percent_plus/100*h_plus*y_pos) / 10000 , 0); //multiplying it by c doesn't seem to have an effect since c is very small


        }
        
        
    }
    private void CircleFormation()
    {
        Vector3 targetPosition = Vector3.zero; //what we want the spheres to be circled around 
        for(int i = 0; i < 10; i++) { //populate 10 spheres on screen

            GameObject instance = Instantiate(prefab);
            gameObjects.Add(instance);


            float angle = i * (2 * 3.1459f / 10); //find an angle for every sphere that we want to populate, divide by 10 because that's number of spheres we want


            //Users have ability to change radius of ring (offset for each sphere)
            float x = Mathf.Cos(angle) * radius; 
            float y = Mathf.Sin(angle) * radius;
            targetPosition = new Vector3(targetPosition.x + x, targetPosition.y + y, 0); //moving (0,0,0) to the certain offset which defined by x and y

            instance.transform.position = targetPosition;
            
            
        }
        

    }


    
    
}
