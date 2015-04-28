using UnityEngine;
using System.Collections;

public class DetectarPulsacion : MonoBehaviour {


    public static void Ordenador(out Vector2 posPc)
    {
        posPc = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public static void Android(out Vector2 posMouse, out Vector2 posMouse2)
    {
        if (Input.touchCount == 1)
        {

            posMouse = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            posMouse2 = Vector2.zero;


        }
        else
        {

            if (Input.touchCount == 2)
            {

                posMouse = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                posMouse2 = Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position);
            }
            else
            {
                posMouse = Vector2.zero;
                posMouse2 = Vector2.zero;

            }
        }
    }
}
