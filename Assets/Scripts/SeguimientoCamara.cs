using UnityEngine;
using System.Collections;

public class SeguimientoCamara : MonoBehaviour {

	public GameObject Personaje;
	public float separacion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Personaje != null) {
						transform.position = new Vector3 (Personaje.transform.position.x + separacion, transform.position.y, transform.position.z);
				}
		}
}
