using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurretArea : MonoBehaviour
{

    public Color hoverColor;
    public Vector3 positionOffSet;
    public Color notEnoughMoney;

    [Header("Optional")]
    public GameObject turret;

    private Renderer rend;
    //saves start color
    private Color startColor;

    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return new Vector3(transform.position.x, transform.position.y, -0.2f);
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if(turret != null)
        {
            Debug.Log("Can't build there!  TODO: Display on screen");
            return;
        }
        //build a turret
        //ref prefab
        //instantiate prefab
        //add position and rotation
        buildManager.BuildTurretOn(this);
    }


    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        //when mouse hovers over area where turrets go
        //area turns a specific color to let player know they can put one there
        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoney; ;
        }
        
    }

    void OnMouseExit()
    {
        //when mouse is not hovering with turret over area where turrets go
        //area returns to original color
        rend.material.color = startColor;
    }

}
