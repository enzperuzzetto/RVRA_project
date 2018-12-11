using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class editor : MonoBehaviour {

	public GameObject max;
	public GameObject min;
	public GameObject middle;
	
	private List<string> planets;

	private float passedTime;

    private const float UPDATE_TIME = 1.0f;

	//private List<string> edit = new List<string>(){"Scale", "Rotate", "Step"};


    // Use this for initialization
    void Start () {
		planets = new List<string>(9){"Earth","Sun","Mercury","Venus","Mars","Jupiter","Saturn","Uranus","Neptune"};
		float passedTime = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if( min.activeSelf && max.activeSelf)
		{
			passedTime += Time.deltaTime;
			if (passedTime > UPDATE_TIME){
				passedTime = 0;
				Scale();
			}
		}
		
	}

	private void Scale()
	{
		Transform child = null;
		for(int i=0; i<planets.Count; i++){
			child = middle.transform.Find(planets[i]);
			if( child != null )
				break;
		}
		if(child != null)
		{
			Vector3 init = child.localScale;
			float dist = distance(min, max);
			float dist2 = distance(min, middle);
			float d = dist2/dist;
			Vector3 s = new Vector3(d*2.0f, d*2.0f, d*2.0f);
			child.localScale = Vector3.Scale(init, s);
		}
	}

	// private bool eless(Vector3 v1, Vector3 v2, double inter){
	// 	if(v1.x <= v2.x && v1.y - inter<= v2.y)
	// 		return true;
		
	// 	return false;
	// }

	// private bool egreater(Vector3 v1, Vector3 v2, double inter){
	// 	if(v1.x >= v2.x && v1.y+inter >= v2.y)
	// 		return true;
		
	// 	return false;
	// }

	// /*
	//  * Considering that plus is at right and minus is at left
	//  */
	// private bool isBetween(GameObject obj){
		
	// 	Vector3 posObj  = obj.transform.position;
	// 	Vector3 posPlus = plus.transform.position;
	// 	Vector3 posMinus = minus.transform.position;

	// 	if(eless(posMinus, posObj, 0.1)  && egreater(posPlus, posObj, 0.1))
	// 		return true;

	// 	return false;
	// }

	// private string whichMethod(){
	// 	for(int i=0; i<markers.Count; i++){
	// 		if(isBetween(markers[i]))
	// 			return edit[i];
	// 	}
	// 	return "null";
	// }

	float distance( GameObject obj1, GameObject obj2)
    {
		float x = obj1.transform.position.x - obj2.transform.position.x;
		//float y = obj1.transform.position.y - obj2.transform.position.y;
		//float z = obj1.transform.position.z - obj2.transform.position.z;
		return Mathf.Sqrt ((x * x));// + (y * y) + (z * z));
	}


}
