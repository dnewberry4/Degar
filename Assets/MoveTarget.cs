using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    public LayerMask hitLayers;
    public GameObject player;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(castPoint,out hit,Mathf.Infinity,hitLayers))
            {
                this.transform.position = hit.point;
                if (player.GetComponent<Player_Movement>().isMoving == false)
                {
                    player.GetComponent<Player_Movement>().PlayerMove();
                }

            }


        }
    }
}
