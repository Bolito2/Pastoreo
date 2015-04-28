using UnityEngine;
using System.Collections;

public class Barra : MonoBehaviour {
	
	public RectTransform barra;
	private RectTransform estaBarra;
	
	public float maxX;
	public float valorActual;
	public float minX;
	public float minV;
	public float valorMaximo;
	public float posicionActual;	
	public Transform Objetivo;
	public Vector3 posPantalla;
	public float subida;

	public void Engancharse(Transform posicion, float subida){
		Objetivo = posicion;
		this.subida = subida;
	}

	void Awake() {
		estaBarra = gameObject.GetComponent<RectTransform>();
	}

	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate(){
		if (Objetivo != null) {
			posPantalla = Camera.main.WorldToScreenPoint(Objetivo.position);
			barra.position = new Vector2(posPantalla.x, posPantalla.y + subida);
		}
	}

	void Update () {
		maxX = barra.position.x;
		minX = barra.position.x - barra.rect.width;
		posicionActual = (valorActual-minV) * (maxX-minX) / (valorMaximo-minV) + minX;
		if(valorActual <= valorMaximo)
		estaBarra.position = new Vector2 (posicionActual, barra.position.y);
	}

}
