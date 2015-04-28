using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class InformacionJuego : MonoBehaviour {


	float frames;
	public float fps;
    public Text textoPrueba;

   
    //Cargando

    public Text textoPrefab;
    Text textoCargando;
    public void CargarNivel(string NombreNivel)
    {
        textoCargando = (Text)Instantiate(textoPrefab);
        textoCargando.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>());
        textoCargando.gameObject.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(Vector3.zero);
        StartCoroutine(CargandoNivel(NombreNivel));
    }
    IEnumerator CargandoNivel(string nivel)
    {
        AsyncOperation async = Application.LoadLevelAsync(nivel);
        while (!async.isDone)
        {
            textoCargando.text = async.progress * 100 + "%";
            yield return null;
        }
        yield break;
    }
    
    public void Guardar() 
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream archivo = File.Open(Application.persistentDataPath+"/datosJuego.dat", FileMode.Open);
        VariablesAGuardar Datos = new VariablesAGuardar();

        //Pasar datos de esta clase a la clase de guardado

        Datos.GuardarArmaSeleccionada = ArmaSeleccionada;
        Datos.ArmasAGuardar = ArmasCompradas;

        bf.Serialize(archivo, Datos);
        archivo.Close();
    }

    public void Cargar()
    {
        if (File.Exists(Application.persistentDataPath + "/DatosJuego.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream archivo = File.Open(Application.persistentDataPath + "/DatosJuego.dat", FileMode.Open);
            VariablesAGuardar Datos = (VariablesAGuardar)bf.Deserialize(archivo);
            archivo.Close();

            ArmaSeleccionada = Datos.GuardarArmaSeleccionada;
            ArmasCompradas = Datos.ArmasAGuardar;

            //Pasar datos de la clase de guardado a esta clase
        }
    }

    public Armas armas;
    public Dictionary<int, bool> ArmasCompradas = new Dictionary<int, bool>();
    public Dictionary<string, bool> SkinsCompradas = new Dictionary<string, bool>();

    public void ComprarArma(int idArma)
    {
        
        if (ArmasCompradas[idArma].CompareTo(true) == 0)
        {
            Debug.Log("Arma ya comprada");
        }
        else
        {
            //Debug.Log("Arma comprada");
            Debug.Log("Te has comprado el siguiente arma : " + "id : "+idArma + gameObject.GetComponent<Armas>().ListaArmas[idArma].nombre);
            ArmasCompradas[idArma] = true;
            Guardar();
        }
        
    }

    public int ArmaSeleccionada;

	public void SeleccionarArma(int idArma)
    {
        if (ArmasCompradas[idArma].CompareTo(true) == 0)
        {
            ArmaSeleccionada = idArma;
            Debug.Log("Arma seleccionada : " + gameObject.GetComponent<Armas>().ListaArmas[idArma].nombre);
        }
        else
            Debug.Log("Arma no comprada");
	}

    public static InformacionJuego instancia = null;

    void Awake () {
		if (instancia == null) {
            instancia = this;
            DontDestroyOnLoad(gameObject);
		}
		else
        {
            if (instancia != this)
            {
				Destroy(gameObject);
			}  
		}
		}

	
	void Start()
    {
        ArmasCompradas.Add(1, false);
        ArmasCompradas.Add(2, false);
        ArmasCompradas.Add(3, false);
    }

	public void ReiniciarNivel(){
        CargarNivel(Application.loadedLevelName);
	}

	public static void MatarJugador (ScriptPersonaje Jugador){
		Destroy (Jugador.gameObject);
	}

	public void CogerArma(ScriptPersonaje personaje){
        personaje.info.arma = GetComponent<Armas>().ListaArmas[ArmaSeleccionada];
       
	}
    public float longitudArray;
    public float aceleracion;
    public int multiplicador = 1;

	void Update () {
        
        frames++;
		fps = frames / Time.time;

        GameObject[] otrosTextos = GameObject.FindGameObjectsWithTag("TextoFlotante");
        if (otrosTextos.Length - 1 > longitudArray)
        {
            foreach (GameObject otroTexto in otrosTextos)
            {
                otroTexto.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 60f + aceleracion * multiplicador/(otrosTextos.Length));
                multiplicador +=1;
            }
            multiplicador = 1;
        }
        longitudArray = otrosTextos.Length - 1;
	}

[Serializable]
    public class VariablesAGuardar
    {
    public Dictionary<int, bool> ArmasAGuardar = new Dictionary<int, bool>();
    public int GuardarArmaSeleccionada;
    public Dictionary<string, bool> SkinsCompradas = new Dictionary<string, bool>();
    }

}
