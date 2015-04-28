using UnityEngine;
using System.Collections;

public class GeneradorEnemigos : MonoBehaviour {

	public float TiempoEspera;
	float numero;
	public GameObject Enemigo;
	public ScriptPersonaje Personaje;
	bool partidaEmpezada;
	
	void OnEnable(){
		ManagerEventos.PartidaEmpezada += Empezar;
	}
	void OnDisable(){
		ManagerEventos.PartidaEmpezada -= Empezar;
	}	

	void Empezar () {
		TiempoEspera = Random.Range (1, 3);
		partidaEmpezada = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(partidaEmpezada){
		TiempoEspera -= Time.deltaTime;
		if(TiempoEspera <=0){
			TiempoEspera = Random.Range (1, 3);
			Instantiate (Enemigo, transform.position, Quaternion.identity);

			}
		}

	}
}