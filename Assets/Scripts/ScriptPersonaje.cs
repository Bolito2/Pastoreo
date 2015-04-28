using UnityEngine;
using System.Collections;
using UnityEngine.UI;


//Manager de los eventos de la partida
public class ManagerEventos
{
	public delegate void EventosPartida();
	public static event EventosPartida PartidaEmpezada;

	public static void EmpezarPartida(){

		PartidaEmpezada ();

	}
}

public class ScriptPersonaje : MonoBehaviour {


    //Informacion del personaje
	[System.Serializable]
	public class InfoPersonaje{
		public int vida = 100;

		public int balasCargador;
		public float tiempoEntreDisparos;
        public Arma arma;
		public int Armaid;
	}

    public InfoPersonaje info = new InfoPersonaje ();



	public GameObject Bala;
	public GameObject Brazo;
    public Animator animadorBrazo;

	//Variables Movimiento
	public float velocidad, FuerzaSalto;

	//Controles
    public Transform posicionArma;
	public Vector2 posicionMOUSE,posicionMOUSE2;
	public BoxCollider2D Saltar, Disparar;
	public bool Saltar1, Saltar2, Disparar1, Disparar2,haDisparado, haSaltado;
	public SpriteRenderer Pulsadoresxd;
	public RotacionBrazo ScriptBrazo;

	//Fisicas
	public Rigidbody2D PersonajeRB2D;
	bool PrimeraPulsacion = true;
	public bool EnSuelo;
	public LayerMask mascaraSuelo;
	public Transform comprobadorSuelo;
	public float radio;

	//Armas
	public Armas ManagerArmas;
	public GameObject PrefabBala;
	public bool ArmaCargada = true;
	private bool yaRecargo;

	public float t;

	//Barras
	public GameObject textoRecarga;
	public GameObject barraRecargar;
	public Barra barraRecarga, barraVida;
    public float subida;

	//Textos
	public Text TextoVida;
	public Text textoRecargando;

	public bool yaAnim;

	Animator animador;

	void OnEnable(){
		ManagerEventos.PartidaEmpezada += PartidaEmpezada; 
	}

	void OnDisable(){
		ManagerEventos.PartidaEmpezada -= PartidaEmpezada;
	}

	void PartidaEmpezada(){
		animador.SetBool ("PartidaEmpezada", true);
		PartidaEmpezadaBool = true;
		PersonajeRB2D.velocity = new Vector2 (velocidad, PersonajeRB2D.velocity.y);
		Pulsadoresxd.sprite = null;
	}

	void Awake(){
		animador = GetComponent<Animator> ();
	}


	public void Daño(int DañoCausado){
		info.vida -= DañoCausado;
		barraVida.valorActual = info.vida;
		TextoVida.text = info.vida.ToString();
		if (info.vida <= 0) {
			Debug.Log ("Muerte");
            GameObject.FindGameObjectWithTag("Info").GetComponent<InformacionJuego>().CargarNivel("MenuAlpha");
		}

	}

	public GeneradorEnemigos GenEnemigos;

	public bool PartidaEmpezadaBool;

	void ComprobarBotones(){

        if (!Application.isEditor)
        {
            Saltar1 = Saltar.OverlapPoint(posicionMOUSE);
            Saltar2 = Saltar.OverlapPoint(posicionMOUSE2);
            if (Saltar1 || Saltar2)
                haSaltado = true;
            else
                haSaltado = false;

            Disparar1 = Disparar.OverlapPoint(posicionMOUSE);
            Disparar2 = Disparar.OverlapPoint(posicionMOUSE2);
            if (Disparar1 || Disparar2)
                haDisparado = true;
            else
                haDisparado = false;


            if (haSaltado && EnSuelo)
            {
                haSaltado = false;
                PersonajeRB2D.velocity = new Vector2(PersonajeRB2D.velocity.x, FuerzaSalto);
            }

            if (haDisparado)
            {

                DispararBalas();
                haDisparado = false;

            }


        }
        else
        {


            if (Input.GetMouseButton(0))
            {
                Debug.Log("saltoxd");
                haSaltado = Saltar.OverlapPoint(posicionMOUSE);
                haDisparado = Disparar.OverlapPoint(posicionMOUSE);

                if (haSaltado && EnSuelo)
                {
                    haSaltado = false;
                    PersonajeRB2D.velocity = new Vector2(PersonajeRB2D.velocity.x, FuerzaSalto);
                }

                if (haDisparado)
                {

                    DispararBalas();
                    haDisparado = false;

                }
            }
        }
	
	}


	void Start () {

        InformacionJuego informacionJuego = GameObject.FindGameObjectWithTag("Info").GetComponent<InformacionJuego>();
        informacionJuego.CogerArma(this);

		info.balasCargador = info.arma.balasCargadorMax;
		info.tiempoEntreDisparos = 0;
		info.vida = 100;

        animadorBrazo.SetInteger("id", info.Armaid);


	}

	void FixedUpdate(){
    

        EnSuelo = Physics2D.OverlapCircle (comprobadorSuelo.position, radio, mascaraSuelo);
		animador.SetBool ("EnSuelo", EnSuelo);

	}

	public void DispararBalas(){
		if (ArmaCargada) {
			if(info.tiempoEntreDisparos <= 0){
				info.tiempoEntreDisparos = info.arma.tiempoEntreBalas;
				info.balasCargador--;
				for(int i = info.arma.numBalas; i > 0; i--){
					GameObject BalaClon =(GameObject)Instantiate(PrefabBala,posicionArma.transform.position, Brazo.transform.rotation);
					Bala ScriptBala = BalaClon.GetComponent<Bala>();
					ScriptBala.velocidadBala = info.arma.velocidadbala;
					ScriptBala.variacion = info.arma.dispersion;
				}

			}

		}
	
	}

	

	public IEnumerator RecargandoBucle(){
		textoRecargando.text = "RECARGANDO";
		while (!ArmaCargada) {
			yield return new WaitForSeconds(0.2f);
			switch(textoRecargando.text){
			case "RECARGANDO" : {
				textoRecargando.text = "RECARGANDO.";
				break;
			}
			case "RECARGANDO." : {
				textoRecargando.text = "RECARGANDO..";
				break;
			}
			case "RECARGANDO.." : {
				textoRecargando.text = "RECARGANDO...";
				break;
			}
			case "RECARGANDO..." : {
				textoRecargando.text = "RECARGANDO";
				break;
			}
			default : {
				Debug.LogError("Texto de recarga incorrecto");
				break;
			}
			}
			yield return null;
				
		}
		yield break;
	}


	void Update () {


		if (info.balasCargador <= 0) {
			
			ArmaCargada = false;
			
			yaRecargo = false;
		}

		if (PersonajeRB2D.velocity.x < velocidad && PartidaEmpezadaBool)
			PersonajeRB2D.velocity = new Vector2(velocidad,PersonajeRB2D.velocity.y);

        if (Application.isEditor)
            DetectarPulsacion.Ordenador(out posicionMOUSE);
        else
            DetectarPulsacion.Android(out posicionMOUSE, out posicionMOUSE2);

		    ScriptBrazo.MoverBrazo ();
		    info.tiempoEntreDisparos -= Time.deltaTime;
		    ComprobarBotones ();

		if (Input.GetMouseButtonDown(0) && PrimeraPulsacion) {
						ManagerEventos.EmpezarPartida(); 
						PrimeraPulsacion = false;
				}
	
		if (ArmaCargada == false && yaRecargo == false) {
			barraRecargar.SetActive(true);
			textoRecarga.SetActive(true);
			if(t<1){
				t += Time.deltaTime/ info.arma.TiempoRecargaArma;
			}
			barraRecarga.valorActual = Mathf.Lerp (0, 100, t);
			if(!yaAnim){
			StartCoroutine(RecargandoBucle());
				yaAnim = true;
			}

			if(barraRecarga.valorActual == barraRecarga.valorMaximo){
				textoRecarga.SetActive(false);
				barraRecargar.SetActive(false);
				barraRecarga.valorActual = 0;
				t = 0;
				info.balasCargador = info.arma.balasCargadorMax;
				ArmaCargada = true;
				yaRecargo = true;
				yaAnim = false;
			}

			
		}

	

		}
	}


