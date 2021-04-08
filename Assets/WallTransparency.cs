using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTransparency : MonoBehaviour
{
    //Variables
    private Renderer rend;
    Color materialColor;
    private bool transparent = false;

    private void Start()
    {
        //Get the renderer of the object
        rend = GetComponent<MeshRenderer>();
        if (!rend)
        {
            rend = GetComponent<Renderer>();
        }
        //Get the material color
        materialColor = rend.material.color;
    }

    public void ChangeTransparency(bool transparent)
    {
        //Avoid to set the same transparency twice
        if (this.transparent == transparent) return;

        //Set the new configuration
        this.transparent = transparent;

        //Check if should be transparent or not
        if (transparent)
        {
            //Change the alpha of the color
            Destroy(this);
        }
        else
        {
            //Change the alpha of the color
            materialColor.a = 1.0f;
        }
        //Set the new Color
        rend.material.color = materialColor;
    }
}