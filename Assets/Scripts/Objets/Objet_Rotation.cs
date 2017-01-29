using UnityEngine;
using System.Collections;

public class Objet_Rotation : MonoBehaviour {


	// Update is called once per frame
	void Update () {
		transform.Rotate (0, 0, -Time.deltaTime*300);
	}
}
