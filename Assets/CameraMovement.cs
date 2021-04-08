using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    Vector3 currentEulerAngles;
    Quaternion currentRotation;
    public LayerMask hitLayer;
    GameObject camera;
    public GameObject player;
    private WallTransparency currentTransparentWall;
    public GameObject moveObject;

    // Start is called before the first frame update
    void Start()
    {
        camera = this.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovementFunc();
        PlayerTransparencyTracking();
    }

    private void PlayerTransparencyTracking()
    {
        Vector3 direction = player.transform.position - camera.transform.position;
        RaycastHit hit;
        Debug.DrawRay(camera.transform.position, direction * 10000, Color.red);
        if (Physics.Raycast(camera.transform.position, direction, out hit, hitLayer))
        {
            print("Hit " + hit.transform.name);
            var hitObject = hit.transform.GetComponent<WallTransparency>();

            if (hitObject)
            {
                //If there is a previous wall hit and it's different from this one
                if (currentTransparentWall && currentTransparentWall.gameObject != hitObject.gameObject)
                {
                    //Restore its transparency setting it not transparent
                    currentTransparentWall.ChangeTransparency(false);
                }
                //Change the object transparency in transparent.
                hitObject.ChangeTransparency(true);
                currentTransparentWall = hitObject;
            }
        }
        else
        {
            //If nothing is hit and there is a previous object hit
            if (currentTransparentWall)
            {
                //Restore its transparency setting it not transparent
                currentTransparentWall.ChangeTransparency(false);
            }
        }
    }

    private void CameraMovementFunc()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentRotation.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 30);
            transform.rotation = currentRotation;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            currentRotation.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 30);
            transform.rotation = currentRotation;
        }

        if (Input.GetKey(KeyCode.D))
        {
            var movePoint = Instantiate(moveObject,this.transform.position,camera.transform.rotation,camera.transform);
            movePoint.transform.localPosition = new Vector3(movePoint.transform.localPosition.x + (.2f), movePoint.transform.localPosition.y, movePoint.transform.localPosition.z);
            transform.position = movePoint.transform.position;
            Destroy(movePoint);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            var movePoint = Instantiate(moveObject, this.transform.position, camera.transform.rotation, camera.transform);
            movePoint.transform.localPosition = new Vector3(movePoint.transform.localPosition.x - (.2f), movePoint.transform.localPosition.y, movePoint.transform.localPosition.z);
            transform.position = movePoint.transform.position;
            Destroy(movePoint);
        }

        if (Input.GetKey(KeyCode.W))
        {
            var movePoint = Instantiate(moveObject, this.transform.position, camera.transform.rotation, camera.transform);
            movePoint.transform.localPosition = new Vector3(movePoint.transform.localPosition.x, movePoint.transform.localPosition.y, movePoint.transform.localPosition.z + (.2f));
            transform.position = movePoint.transform.position;
            Destroy(movePoint);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            var movePoint = Instantiate(moveObject, this.transform.position, camera.transform.rotation, camera.transform);
            movePoint.transform.localPosition = new Vector3(movePoint.transform.localPosition.x, movePoint.transform.localPosition.y, movePoint.transform.localPosition.z - (.2f));
            transform.position = movePoint.transform.position;
            Destroy(movePoint);
        }

        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            camera.GetComponent<Camera>().orthographicSize = camera.GetComponent<Camera>().orthographicSize - 1;
        }
    }
}
