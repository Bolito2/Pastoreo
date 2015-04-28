using UnityEngine;
using System.Collections;

public class TextoFlotante : MonoBehaviour {

	public void ColocarTexto(Transform posicion, float separacion){
		Vector2 Objetivo =(Vector2)Camera.main.WorldToScreenPoint (posicion.position);
		this.GetComponent<RectTransform> ().position = new Vector2 (Objetivo.x, Objetivo.y + separacion);
		StartCoroutine (DestruirTexto ());
	}

    public float aceleracion;
    GameObject[] otrosTextos = new GameObject[10];
    public int longitudArray = 0;
    
    IEnumerator DestruirTexto(){
		while (true) {
			yield return new WaitForSeconds(0.8f);
			Destroy (this.gameObject);
		}
	}

}
