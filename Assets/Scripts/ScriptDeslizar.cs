using UnityEngine;
using System.Collections;

public class ScriptDeslizar : MonoBehaviour {

    public BoxCollider2D zonaDeslizadora;
    Vector3 antiguaPos;
    Vector3 posicionRatonPC;
    public float velocidad;

    public Transform t1,t2,t3,t4;

	void Update () {
	
#if UNITY_EDITOR

        if (Input.GetMouseButton(0))
        posicionRatonPC = Camera.main.ScreenToWorldPoint(Input.mousePosition);

       
            t1.Translate((posicionRatonPC.x - antiguaPos.x) * velocidad, 0, 0);
            t2.Translate((posicionRatonPC.x - antiguaPos.x) * velocidad, 0, 0);
            t3.Translate((posicionRatonPC.x - antiguaPos.x) * velocidad, 0, 0);
            t4.Translate((posicionRatonPC.x - antiguaPos.x) * velocidad, 0, 0);


       if (Input.GetMouseButtonDown(0))
       {
           antiguaPos = posicionRatonPC;
       }
#endif

	}
}
