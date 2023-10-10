using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class Ants : MonoBehaviour
{
    private Vector2 coor_ants;
    public float speed_ant;
    private int direct;
    private int num_anterior;
    private float tiempo_de_espera;
    private float rota;
    private Vector3[] rec;
    private Vector3[] world_rec;

    public Camera camara;
    private SpriteRenderer sprite;


    private void Start()
    {
        coor_ants = transform.position;
        direct = 0;
        num_anterior = 0;
        tiempo_de_espera = 0;
        sprite = GetComponent<SpriteRenderer>();
        rota = transform.rotation.z;
        rec = new Vector3[4];
        world_rec = new Vector3[4];
        camara = Camera.main;


        coor_ants.x = 0;
        coor_ants.y = 0;

        /*boundsL = Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect;
        boundsR = boundsL + Camera.main.orthographicSize * Camera.main.aspect * 2;

        boundsD = Camera.main.transform.position.y - Camera.main.orthographicSize;
        boundsU = boundsL + Camera.main.orthographicSize * 2;*/
    }

    private void Update()
    {
        tiempo_de_espera += Time.deltaTime;

        rec[0] = new Vector3(0, Screen.height, camara.transform.position.z);                //superior izquierda
        rec[1] = new Vector3(Screen.width, Screen.height, camara.transform.position.z);     //superior derecha
        rec[2] = new Vector3(Screen.width, 0, camara.transform.position.z);                 //inferior derecha
        rec[3] = new Vector3(0, 0, camara.transform.position.z);                            //inferior izquierda

        world_rec[0] = camara.ScreenToWorldPoint(rec[0]);
        world_rec[1] = camara.ScreenToWorldPoint(rec[1]);
        world_rec[2] = camara.ScreenToWorldPoint(rec[2]);
        world_rec[3] = camara.ScreenToWorldPoint(rec[3]);


        switch (direct)
        {
            case 0:         //up
                if (transform.position.y > world_rec[0].y)
                {
                    direct = 1;
                }
                else
                {
                    coor_ants.y += speed_ant * Time.deltaTime;
                }
                rota = 0;
                break;
            case 1:         //down
                if (transform.position.y < world_rec[2].y)
                {
                    direct = 0;
                }
                else
                {
                    coor_ants.y -= speed_ant * Time.deltaTime;
                }
                rota = 180;
                break;
            case 2:         //right
                if (transform.position.x > world_rec[1].x)
                {
                    direct = 3;
                }
                else
                {
                    coor_ants.x += speed_ant * Time.deltaTime;
                }
                rota = 270;
                break;
            case 3:         //left
                if (transform.position.x < world_rec[0].x)
                {
                    direct = 2;
                }
                else
                {
                    coor_ants.x -= speed_ant * Time.deltaTime;
                }
                rota = 90;
                break;
            case 4:         //up right
                if (transform.position.y > world_rec[0].y && transform.position.x > world_rec[1].x)
                {
                    direct = 5;
                }
                else
                {
                    coor_ants.x += speed_ant * Time.deltaTime;
                    coor_ants.y += speed_ant * Time.deltaTime;
                }
                rota = 315;
                break;
            case 5:         //down left
                if (transform.position.y < world_rec[3].y && transform.position.x < world_rec[3].x)
                {
                    direct = 4;
                }
                else
                {
                    coor_ants.x -= speed_ant * Time.deltaTime;
                    coor_ants.y -= speed_ant * Time.deltaTime;
                }
                rota = 135;
                break;
            case 6:         //down right
                if (transform.position.y < world_rec[2].y && transform.position.x > world_rec[2].x)
                {
                    direct = 7;
                }
                else
                {
                    coor_ants.x += speed_ant * Time.deltaTime;
                    coor_ants.y -= speed_ant * Time.deltaTime;
                }
                rota = 225;
                break;
            case 7:         //up left
                if (transform.position.y > world_rec[0].y && transform.position.x < world_rec[0].x)
                {
                    direct = 6;
                }
                else
                {
                    coor_ants.x -= speed_ant * Time.deltaTime;
                    coor_ants.y += speed_ant * Time.deltaTime;
                }
                rota = 45;
                break;

        }
        transform.rotation = Quaternion.Euler(0, 0, rota);
        transform.position = coor_ants;
        if (tiempo_de_espera >= 3)
        {
            do
            {
                direct = UnityEngine.Random.Range(0, 8);
            } while (direct == num_anterior);
            num_anterior = direct;
            Debug.Log("" + direct);
            tiempo_de_espera = 0;
        }
    }
    /*
    private void OnDrawGizmos()
    {

        //Linea de la izquierda
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(boundsL, 0, 0), new Vector3(boundsL, 1, 0));

        //Linea de la derecha
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(boundsR, 0, 0), new Vector3(boundsR, 1, 0));

        //Linea de la arriba
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(0, boundsU, 0), new Vector3(1, boundsU, 0));

        //Linea de la abajo
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(0, boundsD, 0), new Vector3(1, boundsD, 0));
    }*/
}
