using UnityEngine;
using System.Collections;

public class Bala : MonoBehaviour {

	public float velocidadBala;
	public float variacion;
	private Quaternion rotacion;
	public ScriptPersonaje Personaje;
	private GameObject personaje;
	public Animator animador;
	
	// Use this for initialization
	
	void Awake(){
		
		personaje = GameObject.Find ("Personaje");
		Personaje = personaje.GetComponent <ScriptPersonaje>();
        animador = GetComponent<Animator>();

	}
	
	void Start () {
        if (Personaje.info.Armaid == 7 && GameObject.FindGameObjectWithTag("Enemigo"))
        {
            Objetivo = GameObject.FindGameObjectWithTag("Enemigo").GetComponent<Transform>();
        }
        animador.SetInteger("id", Personaje.info.arma.id);
        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z + Random.Range(-variacion, variacion), Quaternion.identity.w);
		rotacion = transform.rotation;
		
		
		
	}
	
	// Update is called once per frame
    public Transform Objetivo;
    
    void Update () {
        if (Personaje.info.Armaid == 7)
        {
            Vector3 Distancia = Objetivo.position - transform.position;
            Distancia.Normalize();
            float angulo = Mathf.Atan2(Distancia.x, -Distancia.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angulo - 90);
        }

        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * velocidadBala);
            if (rotacion != null)
                transform.rotation = rotacion;
        }
	}
	
	void OnTriggerEnter2D(Collider2D otro){
		if (otro.transform.tag == "DestruyeBalas")
			Destroy (gameObject);
	}
	
	void OnCollisionEnter2D(Collision2D otro){
		if (otro.transform.tag == "Enemigo") {
			Destroy (this.gameObject);
		}
		if (otro.transform.tag == "Suelo") {
			Destroy (this.gameObject);
		}
	}
}
