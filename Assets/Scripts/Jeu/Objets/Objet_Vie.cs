using UnityEngine;
using System.Collections;

public class Objet_Vie : MonoBehaviour {

	public double tmp_vie;
	private double time_vie;

	void OnEnable () {
		time_vie = (double)Time.time;
	}
		
	void Update () {
		double time_cours = (double)Time.time - time_vie;
		if(time_cours >= tmp_vie){
			Destroy (this.gameObject);
		}
	}
}