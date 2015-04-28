using UnityEngine;
using System.Collections;

public class GeneradorSuelo : MonoBehaviour {

	public float separacion;
	public GameObject SueloPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x,transform.position.y, transform.position.z - Time.time/10000);
	}

	void OnTriggerExit2D(Collider2D otro){
		if (otro.transform.tag == "Suelo") {
			Instantiate(SueloPrefab,new Vector2(transform.position.x-separacion, transform.position.y),Quaternion.identity);		

	}
}
}