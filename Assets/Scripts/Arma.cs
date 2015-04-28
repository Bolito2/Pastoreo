using UnityEngine;
using System.Collections;

[System.Serializable]
public class Arma {
	public int id;
	public string nombre;
	public float TiempoRecargaArma;
	public int balasCargadorMax;
	public float dispersion;
	public float velocidadbala;
	public float tiempoEntreBalas;
	public int numBalas;
	public int dañoMax;
	public int dañoMin;
    public string skin;

	public Arma(int newid,string newnombre, float newTiempoRecargaArma, int newbalasCargadorMax, float newdispersion, float newvelocidadbala, float newtiempoEntreBalas, int newnumBalas,int newdañoMin,int newdañoMax, string newskin){
		id = newid;
		nombre = newnombre;
		TiempoRecargaArma = newTiempoRecargaArma;
		balasCargadorMax = newbalasCargadorMax;
		dispersion = newdispersion;
		velocidadbala = newvelocidadbala;
		tiempoEntreBalas = newtiempoEntreBalas;
		numBalas = newnumBalas;
		dañoMax = newdañoMax;
		dañoMin = newdañoMin;
        skin = newskin;
	}
}