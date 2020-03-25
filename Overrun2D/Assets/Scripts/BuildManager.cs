using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {


    //stores build manager reference to itself so its one BM per turretArea
    public static BuildManager instance;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one BM in scene");
            return;
        }
        instance = this;
    }
    public GameObject standardTurretPrefab;
    public GameObject anotherTurretPrefab;

    //void Start()
    //{
     //   turretToBuild = standardTurretPrefab;
    //}

    private TurretBluePrint turretToBuild;

    public GameObject buildEffect;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void BuildTurretOn(TurretArea turretArea)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough money to build");
            return;
        }
        PlayerStats.Money -= turretToBuild.cost;

        GameObject turret = (GameObject) Instantiate(turretToBuild.prefab, turretArea.GetBuildPosition(), Quaternion.identity);
        turretArea.turret = turret;

        GameObject effect = (GameObject)Instantiate(buildEffect, turretArea.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
        
        Debug.Log("Turret Build Money Left: " + PlayerStats.Money);
    }

    public void SelectTurretToBuild(TurretBluePrint turretBluePrint)
    {
        turretToBuild = turretBluePrint;
    }
    
}
