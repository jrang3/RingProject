using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour
{
    public GameObject prefab;
    private bool moveUp = true;
    private bool moveDown = true;
    private bool moveRight = true;


    //private GameObject[] instantiated_objs;
    private List<GameObject> gameObjects = new List<GameObject>(); //holds list of game objects that are instaitaed
    // Start is called before the first frame update
    void Start()
    {
        //Creates ring of circles
        this.CircleFormation();


        
    }

    // Update is called once per frame
    void Update()
    {
        //Used to move the circular objects up and down
        for(int i = 0; i < gameObjects.Count; i++) {
            

           if (i == 4 || i == 5) { //Represent circles at the top
                if (moveUp) {
                    gameObjects[i].transform.position+=new Vector3(0,0.001f, 0);

                }
                else {
                    gameObjects[i].transform.position+=new Vector3(0,-0.001f, 0);
                }

                if(gameObjects[i].transform.position.y > 13.0f) {
                    moveUp = false;
                }
                if(gameObjects[i].transform.position.y < 9.5f) {
                    moveUp = true;
                }
                // Debug.Log("Y pos: "+gameObjects[i].transform.position.y.ToString());

            }
            else if (i == 0 || i == 9) { //Represent circles at bottom
                //gameObjects[i].transform.position+=new Vector3(0,-0.000325f, 0);
                if (moveDown) {
                    gameObjects[i].transform.position+=new Vector3(0,-0.001f, 0);

                }
                else {
                    gameObjects[i].transform.position+=new Vector3(0,0.001f, 0);
                }

                if(gameObjects[i].transform.position.y < -2.0f) {
                    moveDown = false;
                }
                if(gameObjects[i].transform.position.y > 2.0f) {
                    moveDown = true;
                }
                // Debug.Log("Y pos: "+gameObjects[i].transform.position.y.ToString());
                
            }
            // if (i == 6 || i == 7 || i == 8) { //represents circles on left
            //     //gameObjects[i].transform.position+=new Vector3(0.000325f,0, 0);
            //     if (moveRight) {
            //         gameObjects[i].transform.position+=new Vector3(0.000325f,0, 0);

            //     }
            //     else {
            //         gameObjects[i].transform.position-=new Vector3(0.000325f,0, 0);
            //     }

            //     if(gameObjects[i].transform.position.x > -1.2f) {
            //         moveRight = false;
            //     }
            //     if(gameObjects[i].transform.position.x < -3.0f) {
            //         moveDown = true;
            //     }
            //     Debug.Log("X pos: "+gameObjects[i].transform.position.x.ToString());

            // }
            // else { //represents circles on right
            //     gameObjects[i].transform.position+=new Vector3(-0.000325f,0, 0);
            // }


            
            


        }
        
        
        
        
    }
    private void CircleFormation()
    {
        Vector3 targetPosition = Vector3.zero; //what we want the spheres to be circled around 
        for(int i = 0; i < 10; i++) { //populate 10 spheres on screen

            GameObject instance = Instantiate(prefab);
            gameObjects.Add(instance);


            float angle = i * (2 * 3.1459f / 10); //find an angle for every sphere that we want to populate, divide by 10 because that's number of spheres we want

            float x = Mathf.Cos(angle) * 3.5f; //offset for each sphere
            float y = Mathf.Sin(angle) * 3.5f;

            targetPosition = new Vector3(targetPosition.x + x, targetPosition.y + y, 0);

            instance.transform.position = targetPosition;

            
        }
        //prefab.transform.position += new Vector3(0, 2, 0);


    }
    // private void MoveDirection(bool direction, int ball_num) {
    //     GameObject obj =  gameObjects[ball_num];
    //     if (ball_num == 4 || ball_num == 5) {
    //         if (direction) {
    //             obj.transform.position+=new Vector3(0,0.000325f, 0);
    //         }
    //         else {
    //             obj.transform.position-=new Vector3(0,0.000325f, 0);

    //         }
    //         if (obj.transform.position.y > 12.0f) {
    //             direction = false;
    //         }
    //         else if (obj.transform.position.y <  7.5f) {
    //             direction = true;
    //         }


    //     }

    

    // }
    
    
}
