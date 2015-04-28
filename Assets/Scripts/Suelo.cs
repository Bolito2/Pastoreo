using UnityEngine;
using System.Collections;

public class Suelo : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D otro){
		if (otro.transform.tag == "DestruyeEdificios") {
			Destroy (gameObject);
				}
		}
}
