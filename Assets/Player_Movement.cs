using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float moveSpeed = .5f;
    public Transform movePoint;
    public GameObject gridManager;

    public bool isMoving;
    int index = 0;
    Vector3 currentWaypoint;
    List<Node> finalPath;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        movePoint.transform.position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.Lerp(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (isMoving == true)
        {
            var vel = this.GetComponent<Rigidbody>().velocity;      //to get a Vector3 representation of the velocity
            var speed = Mathf.Clamp01(vel.magnitude*100);             // to get magnitude
            this.transform.Find("Soldier_demo").GetComponent<Animator>().speed = speed;
            if (index <= finalPath.Count - 1) // if we in range of array
            {
                if (Vector3.Distance(movePoint.transform.position, currentWaypoint) < .1f)  // and we near the current target-waypoint
                {
                    movePoint.transform.position = currentWaypoint;
                    currentWaypoint = finalPath[index].vPosition; // set new target-waypoint     
                    index++; // increase array index of waypoints
                }
            } else
            {
                isMoving = false;
                this.transform.Find("Soldier_demo").GetComponent<Animator>().SetBool("IsRunning", false);
            }

            //movePoint.transform.position = Vector3.Lerp(movePoint.transform.position, currentWaypoint, Time.deltaTime * 2); // move to the current target-waypoint
            movePoint.transform.position = Vector3.MoveTowards(movePoint.transform.position, currentWaypoint, .2f);


            /*foreach (Node node in finalPath)
            {
                while (Vector3.Distance(movePoint.position, node.vPosition) > .01)
                {
                    movePoint.position = Vector3.Lerp(movePoint.position, node.vPosition, 1 * Time.deltaTime);
                    print(Vector3.Distance(movePoint.position, node.vPosition));
                }

            }*/


        }

        var lookPoint = new Vector3(movePoint.position.x, this.transform.position.y, movePoint.position.z);
        this.transform.LookAt(lookPoint);
        
        
    }

    public void PlayerMove()
    {
        isMoving = true;
        gridManager.GetComponent<Pathfinding>().FindPath(gridManager.GetComponent<Pathfinding>().StartPosition.position, gridManager.GetComponent<Pathfinding>().TargetPosition.position);
        finalPath = new List<Node>(gridManager.GetComponent<Grid>().FinalPath);
        currentWaypoint = finalPath[0].vPosition;
        index = 0;
        this.transform.Find("Soldier_demo").GetComponent<Animator>().SetBool("IsRunning", true);
    }
}
