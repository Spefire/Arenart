using UnityEngine;
using System.Collections;

public class Objet_Grandeur : MonoBehaviour {

	public float speed;

	void Update () {
		transform.localScale += new Vector3 (Time.deltaTime*speed, Time.deltaTime*speed, 0);
	}
}
