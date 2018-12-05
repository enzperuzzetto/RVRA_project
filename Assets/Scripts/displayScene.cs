using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class displayScene : MonoBehaviour {

	private GameObject marker0;
	private GameObject marker1;
	private GameObject background;

	// Use this for initialization
	void Start () {
		marker0 = GameObject.Find("Marker0");
		marker1 = GameObject.Find("Marker1");
		background = GameObject.Find("background");
	}
	
	// Update is called once per frame
	void Update () {
		float dist = distance(marker0, marker1);
		setPosition(background, marker0, marker1);
		//setScale(background, marker0, marker1);
		print(dist);
		
	}

	void setPosition( GameObject toset, GameObject obj1, GameObject obj2){
		float x = (obj1.transform.position.x + obj2.transform.position.x)/2.0f;
		float y = (obj1.transform.position.y + obj2.transform.position.y)/2.0f;
		float z = (obj1.transform.position.z + obj2.transform.position.z)/2.0f;
		toset.transform.position = new Vector3(x, y, z);
	}

	void setScale( GameObject toset, GameObject obj1, GameObject obj2){
		float x = (obj1.transform.position.x - obj2.transform.position.x)/2.0f;
		float y = (obj1.transform.position.y - obj2.transform.position.y)/2.0f;
		float z = (obj1.transform.position.z - obj2.transform.position.z)/2.0f;
		x = Mathf.Sqrt(x * x);
		y = Mathf.Sqrt(y * y);
		z = Mathf.Sqrt(z * z);
		toset.transform.localScale = new Vector3(x, y, z);
	}

	float distance( GameObject obj1, GameObject obj2){
		float x = obj1.transform.position.x - obj2.transform.position.x;
		float y = obj1.transform.position.y - obj2.transform.position.y;
		float z = obj1.transform.position.z - obj2.transform.position.z;
		return Mathf.Sqrt ((x * x) + (y * y) + (z * z));
	}
}
