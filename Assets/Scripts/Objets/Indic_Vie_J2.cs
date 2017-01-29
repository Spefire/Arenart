using UnityEngine;
using System.Collections;

public class Indic_Vie_J2 : MonoBehaviour {

	public Texture texture_base;
	public Texture texture_vie_plus;
	public Texture texture_vie_moins;
	private static double tmp_vie = 1;
	private static double time_vie;
	private static bool vie;
	private static bool plus;
	private static bool moins;
	private Renderer render;

	void Start () {
		render = GetComponent<Renderer>();
		vie = false;
		moins = false;
		plus = false;
	}

	void Update () {
		if (vie) {
			double time_cours = (double)Time.time - time_vie;
			if(time_cours >= tmp_vie){
				vie = false;
				render.material.mainTexture = texture_base;
			} else if (moins) {
				moins = false;
				render.material.mainTexture = texture_vie_moins;
			} else if (plus) {
				plus = false;
				render.material.mainTexture = texture_vie_plus;
			}
		}
	}

	public static void set_Vie_Moins(){
		time_vie = (double)Time.time;
		vie = true;
		moins = true;
		plus = false;
	}
	public static void set_Vie_Plus(){
		time_vie = (double)Time.time;
		vie = true;
		moins = false;
		plus = true;
	}
}
