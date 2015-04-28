using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Armas : MonoBehaviour {

    public Arma Trabuco, Pistola, Metralleta, Escopeta, Ametralladora, Bazooka, Lanzamisiles;

    public Dictionary<int, Arma> ListaArmas = new Dictionary<int, Arma> ();

	public Arma SeleccionarArma(int idarma){
        Arma ArmaSeleccionada = ListaArmas[idarma];
        return ArmaSeleccionada;
	}

	void Start () {
        ListaArmas.Add(1,Trabuco);
        ListaArmas.Add(2, Pistola);
        ListaArmas.Add(3, Metralleta);
        ListaArmas.Add(4, Escopeta);
        ListaArmas.Add(5, Ametralladora);
        ListaArmas.Add(6, Bazooka);
        ListaArmas.Add(7, Lanzamisiles);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
