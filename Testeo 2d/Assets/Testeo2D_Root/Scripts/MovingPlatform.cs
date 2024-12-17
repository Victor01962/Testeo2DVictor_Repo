using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public float speed; //Velocida d ela plataforma
    [SerializeField] int startingPoint;// nÚMERO PARA DETERMINAR EL INDEX DE INICIO DEL MOVIMIENTO
    [SerializeField] Transform[] points;//Array de puntos  de posicion a los que la plataforma "perseguirá"
    int i;//Index que determina que numero de plataforma se persigue actualmente



    // Start is called before the first frame update
    void Start()
    {
        //Seytear la posicion inicial de laq plataform,a en uno de los puntos 
        transform.position = points[startingPoint].position;
    }

    // Update is called once per frame
    void Update()
    {
        PlatformMove();
    }

void PlatformMove()
{//Detector de si la plataforma ha llegado al destino, cambiando el destinoi
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;//Aumenta en uno el index, cambia de objetivo
            if (i == points.Length) i = 0;
        }
        //Mobimiento: SIEMPREDESPUËS DE LA DETECCIÖN
        //MUebve la playaforma el punto mdel Array que coincide con el valor de 1
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
     }

}