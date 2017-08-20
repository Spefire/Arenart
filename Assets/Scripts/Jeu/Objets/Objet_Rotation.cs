using UnityEngine;
using System.Collections;

public class Objet_Rotation : MonoBehaviour {

	public float speed;
	
	void Update () {
		transform.Rotate (0, 0, -Time.deltaTime*100*speed);
	}
}
