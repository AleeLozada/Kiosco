using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
public class Cliente : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 3.0f;
    private Transform destinoActual;
    void Start()
    {

    }
    void Update()
    {
        //El generador le da un punto de fila, se movera hacia el
        if(destinoActual != null)
        {
            float nuevoX = Mathf.MoveTowards(
                transform.position.x,
                destinoActual.position.x,
                velocidad * Time.deltaTime
            );
            transform.position = new Vector3(
                nuevoX,
                transform.position.y,
                transform.position.z
            );
        }
    }
    //Metodo para detener al cliente cuando choque con el mostrador
    public void AsignarNuevoDestino(Transform nuevoPunto)
    {
        destinoActual = nuevoPunto;
    }
    // Este metodo lo llamara la caja registradora al terminar el cobro
    public void DesaparecerCliente()
    {
        // Acá podrian sumar puntos antes de destruirlo
        Debug.Log("Cliente satisfecho se retira.");
        Destroy(gameObject);
    }
}
