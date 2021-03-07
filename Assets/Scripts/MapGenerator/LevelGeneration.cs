using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{	
	public Transform[] startingPosition;
	public GameObject[] rooms; // index 0 --> LR, index 1 --> LRB, index 2 --> LRT, index 3 --> LRBT, 

	private int direction;
	public float moveAmount;

	private float timeBtwRoom;
	public float statTimeBtwRoom = 0.25f;

    public float mapSize;
    private bool stopGeneration;

    // Start is called before the first frame update
    private void Start()
    {	
    	print(startingPosition.Length);
     	int randStartingPos = Random.Range(0, startingPosition.Length);
     	if(startingPosition.Length > 0) {
     	transform.position = startingPosition[randStartingPos].position;
     	}
     	if(rooms.Length > 0) {
     	Instantiate(rooms[0], transform.position, Quaternion.identity);
     	}
     	direction = Random.Range(1, 4);   
    }

    private void Update()
    {
     	if(timeBtwRoom <= 0 && !stopGeneration) {
     		Move();
     		timeBtwRoom = statTimeBtwRoom;
 		} else {
     		timeBtwRoom -= Time.deltaTime;
 		}
    }


    private void Move()
    {      
        float offset = -moveAmount/2;
        float unit = (mapSize -1) * moveAmount + offset;

        if(rooms.Length > 0) {

            int randRoom = Random.Range(0, rooms.Length);  

         	if(direction == 1) {
         		// right
                if(transform.position.x < unit) {
             		Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
             		transform.position = newPos;

                    Instantiate(rooms[randRoom], transform.position, Quaternion.identity);
                    direction = Random.Range(1, 4);  
                    if(direction == 2) {
                        direction = 1;
                    }
                } else {
                    direction = 3;
                }
         	} else if(direction == 2) {
         		// left
                if(transform.position.x > 0) {
             		Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
             		transform.position = newPos;

                    Instantiate(rooms[randRoom], transform.position, Quaternion.identity);
                    direction = Random.Range(2, 4);  
                } else {
                    direction = 3;
                }
         	} else if(direction == 3) {
         		// bottom
                if(transform.position.y > -unit) {
             		Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
             		transform.position = newPos;

                    Instantiate(rooms[randRoom], transform.position, Quaternion.identity);
                    direction = Random.Range(1, 4); 
                } else {
                    // STOP LEVEL GENERATION
                    stopGeneration = true;
                }
         	} 

     	}

    }
}
