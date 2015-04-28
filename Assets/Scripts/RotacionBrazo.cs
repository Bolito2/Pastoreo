using UnityEngine;
using System.Collections;

public class RotacionBrazo : MonoBehaviour {

	public BoxCollider2D colliderDisparar;
	float angulo;
	bool movioBrazo;
	Quaternion direccion;
	bool yaGiro;
    public Vector2 posMouse, posMouse2;

	// Use this for initialization
	


	void Start () {
	
	}
	
	// Update is called once per frame
	public void MoverBrazo () {
        
if(!Application.isEditor){

    DetectarPulsacion.Android(out posMouse,out posMouse2);

        if (colliderDisparar.OverlapPoint (posMouse)) {
			Vector3 Distancia = Camera.main.ScreenToWorldPoint (Input.GetTouch(0).position) - transform.position;
			Distancia.Normalize ();
			angulo = Mathf.Atan2 (Distancia.x, -Distancia.y) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler (0f, 0f, angulo - 90);
				}
		
        else
        {
            if(colliderDisparar.OverlapPoint (posMouse2))
            {
				Vector3 Distancia = Camera.main.ScreenToWorldPoint (Input.GetTouch(1).position) - transform.position;
				Distancia.Normalize ();
				angulo = Mathf.Atan2 (Distancia.x, -Distancia.y) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Euler (0f, 0f, angulo - 90);
			}
		}
    }


if (Application.isEditor)
{
    DetectarPulsacion.Ordenador(out posMouse);

    if (colliderDisparar.OverlapPoint (posMouse)) {
    Vector3 Distancia = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
    Distancia.Normalize ();
    angulo = Mathf.Atan2 (Distancia.x, -Distancia.y) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.Euler (0f,0f,angulo-90);
             }
        }
    }
}
