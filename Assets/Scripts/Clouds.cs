using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		Vector3 angles = gameObject.transform.eulerAngles;
		angles.y += .01f;
		angles.z -= .005f;
		gameObject.transform.eulerAngles = angles;
	}
}
