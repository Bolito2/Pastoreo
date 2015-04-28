using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemigo : MonoBehaviour {
	
	public float velocidadEnemigo;
	public float velocidadLerp;
	public float rotacion;
	public ScriptPersonaje Personaje;
	private Barra barra;
	public int vida;
	public int vidaMax = 100;
	public float subidaBarraEnemigo;
	public GameObject barraPrefab;
	GameObject barraVida;
	public GameObject Canvas;
	public int Daño;
	public GameObject textoPrefab;
	public float separacion;
	



	void Awake(){
		Personaje = GameObject.FindGameObjectWithTag ("Personaje").GetComponent<ScriptPersonaje>();
	}

	public void DañarEnemigo(int DañoCausado){
		vida -= DañoCausado;
	}


	void Start () {
        Canvas = GameObject.FindGameObjectWithTag ("Canvas");
		vida = vidaMax;
		gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-velocidadEnemigo,gameObject.GetComponent<Rigidbody2D> ().velocity.y );
	}
	
	// Update is called once per frame
    
    void Update () {

		if (vida < vidaMax) {
			if(barraVida==null){
                barraVida = (GameObject)Instantiate(barraPrefab, new Vector3(1000,1000,1000), Quaternion.identity);
			barraVida.transform.SetParent(Canvas.transform);
			Transform BarraChild = barraVida.transform.GetChild(0);
			barra = BarraChild.gameObject.GetComponent<Barra>();
			}
			barra.Engancharse(this.transform, subidaBarraEnemigo);
			barra.valorActual = vida;
		}
		if (vida <= 0) {
            Destroy(barraVida);	
			Destroy (gameObject);
		}

		rotacion += Time.deltaTime*velocidadLerp;
		transform.rotation = Quaternion.Euler (new Vector3 (0f, 0f, rotacion));
	}
	
	void OnTriggerEnter2D(Collider2D otro){
        if (otro.transform.tag == "DestructorEnemigos")
        {
            Destroy(barraVida);
            Destroy(gameObject);
        }
	}

	void OnCollisionEnter2D(Collision2D otro){
		if (otro.transform.tag == "Personaje") {
			Personaje.Daño(Random.Range(10,20));
			Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), otro.collider);
		}

		if (otro.transform.tag == "Bala") {
           
                Daño = Random.Range(Personaje.info.arma.dañoMin, Personaje.info.arma.dañoMax);
                GameObject textoDañoGO = (GameObject)Instantiate(textoPrefab, transform.position, Quaternion.identity);
                textoDañoGO.transform.SetParent(Canvas.transform);
                textoDañoGO.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 60f);
                Text textoDaño = textoDañoGO.GetComponent<Text>();
                textoDaño.text = "-" + Daño;
                TextoFlotante textoFlotante = textoDañoGO.GetComponent<TextoFlotante>();
                textoFlotante.ColocarTexto(this.transform, separacion);
                vida -= Daño;
        }
	}
}
