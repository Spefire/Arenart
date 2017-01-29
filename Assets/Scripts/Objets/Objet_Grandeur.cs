using UnityEngine;
using System.Collections;

public class Objet_Grandeur : MonoBehaviour {


	// Update is called once per frame
	void Update () {
		transform.localScale += new Vector3 (0.03f, 0.03f, 0);
	}
}
