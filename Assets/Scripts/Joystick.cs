using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Joystick : MonoBehaviour {
    public Transform player;
    public Transform viewContainer;

    public Transform move_circle;
    public Transform rotate_circle;

    public float speed = 5.0f;
    public float radius = 200f;

    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;

 

	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0)){
            pointA = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        }	
        if(Input.GetMouseButton(0)){
            touchStart = true;
            pointB = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        }else{
            touchStart = false;
        }
	}

	private void FixedUpdate(){
        if(touchStart) {
            bool isLeftHalfofScreen = pointA.x < Screen.width / 2.0f;
            showControls(isLeftHalfofScreen ? move_circle : rotate_circle);
            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, radius);
            
            if(isLeftHalfofScreen) {
                moveCharacter(direction);
            } else {
                rotateCharacter(direction);
            }

        }else{

        	move_circle.GetComponent<Image>().enabled = false;
            rotate_circle.GetComponent<Image>().enabled = false;
            rotate_circle.transform.GetChild(0).GetComponent<Image>().enabled = false;
            move_circle.transform.GetChild(0).GetComponent<Image>().enabled = false;

        }

	}

    void showControls(Transform circle) {
        Transform child = circle.transform.GetChild(0);

        child.transform.position = pointA;
        circle.transform.position = pointA;
        
        child.GetComponent<Image>().enabled = true;
        circle.GetComponent<Image>().enabled = true;
    }

	void moveCharacter(Vector2 direction){
        player.Translate(direction/radius * speed * Time.deltaTime);
        move_circle.transform.GetChild(0).transform.position = new Vector2(pointA.x + direction.x, pointA.y + direction.y);

        if(direction.x > 0) {
            player.GetComponent<SpriteRenderer>().flipX = false;
        } else {
            player.GetComponent<SpriteRenderer>().flipX = true;
        }
        
        if (direction.x != 0 || direction.y != 0) {
            player.GetComponent<Animator>().SetBool ("isWalking", true);
        }
        else {
            player.GetComponent<Animator>().SetBool ("isWalking", false);
        }
    }

    void rotateCharacter(Vector2 direction){
        rotate_circle.transform.GetChild(0).transform.position = new Vector2(pointA.x + direction.x, pointA.y + direction.y);
        float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        viewContainer.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90f);
    }
}