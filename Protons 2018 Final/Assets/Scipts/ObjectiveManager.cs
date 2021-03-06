﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour {
    public GameObject locationObjective;
    public GameObject killObjctive;
    int nextObj;
    ScoreCounter scoreCounter;
    KillObjctive killObj;
    private GameObject[]locSpawnPoint;
	GameObject loc;
	public Text text;
	public Text objDone;
    public int ObjectivesComplete;
	public bool objtype;
	int type;
	GameObject kill;
	bool firstrun = true;

	// Use this for initialization
	void Start () {
        scoreCounter = (ScoreCounter)FindObjectOfType(typeof(ScoreCounter));
        locSpawnPoint = GameObject.FindGameObjectsWithTag("ObjectiveSpawnPoint");//Finds the positions where a location objective can be spawned
        StartCoroutine(setObj());
    }
	
	// Update is called once per frame
	void Update () {

        if(loc == null && kill == null)
        {
            StartCoroutine(setObj());
            //if there are no objectives, make one
        }
        if (type == 1) { 
            text.text = ("GO TO:" + loc.transform.position + " or just head to the beacon :)"); 
        }
		else {
		KillObjctive mykill = kill.GetComponent<KillObjctive>();
            text.text = (scoreCounter.kills+"/" + mykill.requiredScore+" Kills");
		
		}
		objDone.text = (ObjectivesComplete+" Objectives Complete");
        //prints text in UI
    }
    IEnumerator setObj()
    {
        nextObj = Random.Range(0, 2);
        //choose randomly between a location objective and a kill objective
        if (nextObj == 0)
        {
			objtype = false;
            int spawnPointIndex = Random.Range(0, locSpawnPoint.Length);
            loc = Instantiate(locationObjective, locSpawnPoint[spawnPointIndex].transform.position, locSpawnPoint[spawnPointIndex].transform.rotation);
            text.text = ("New location Objective");
            yield return new WaitForSeconds(2);
            text.text = ("GO TO:" + loc.transform.position + " or just head to the beacon :)");
			//creates a new location objective at one of the set positions
			type = 1;
        }
        if(nextObj == 1)
        {
			objtype = true;
            int spawnPointIndex = Random.Range(0, locSpawnPoint.Length);
            kill = Instantiate(killObjctive);
            scoreCounter.primaryGun.myTargets = 0;
            scoreCounter.secondaryGun.myTargets = 0;
            KillObjctive mykill = kill.GetComponent<KillObjctive>();
            mykill.requiredScore = Random.Range(5, 20);
            text.text = ("New Kill Objective: " + mykill.requiredScore);
            //Creates a kill objective with a random amount of required kills between 5 and 20
			type = 0;
            yield return new WaitForSeconds(2);
            text.text = (scoreCounter.kills + "/" + mykill.requiredScore + " Kills");
        }
        print(ObjectivesComplete);
		if (!firstrun){
        ObjectivesComplete += 1;
		}
		firstrun = false;
        // adds to number of completed objectives

    }

}
