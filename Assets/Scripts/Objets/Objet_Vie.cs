using UnityEngine;
using System.Collections;

public class Objet_Vie : MonoBehaviour {

	public double tmp_vie;
	private double time_vie;

	// Use this for initialization
	void Start () {
		time_vie = (double)Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		double time_cours = (double)Time.time - time_vie;
		if(time_cours >= tmp_vie){
			DestroyObject (this.gameObject);
		}
	}
}
