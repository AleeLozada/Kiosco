using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class Caja : MonoBehaviour
{
    [Header("UI Componentes")]
    public Slider barraCobro;
    public GameObject canvasCobro;

    [Header("Configuración")]
    public float tiempoCobranza = 2.0f;
    private bool estaCobrando = false;

    private Cliente clienteActual; // Guarda al cliente que esta en el mostrador

    private void Start()
    {
        // Validar para evitar errores
        if (canvasCobro != null) canvasCobro.SetActive(false);
        if (barraCobro != null) barraCobro.value = 0;
    }
    private void Update()
    {
        //Detecta de si presiona la barra
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (clienteActual != null) 
            {
                IniciarCobranza();
            }
            else
            {
                Debug.LogWarning("Intentaste cobrar, pero No hay ningún cliente");
            }
        }
    }
    

    // Detecta de forma fisica cuando el cliente toca la caja
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (clienteActual==null && collision.CompareTag("Cliente"))
        {
            clienteActual = collision.GetComponent<Cliente>();
        }
    }
    public void IniciarCobranza()
    {
        if (barraCobro == null || canvasCobro == null)
        {
            Debug.LogError("Por favor, asignar la barra de cobro y el canvas en el inspector de la caja.");
            return;
        }
        if (!estaCobrando)
        {
            StartCoroutine(ProcesoCobroCo());
        }
    }
    private IEnumerator ProcesoCobroCo()
    {
        estaCobrando = true;
        canvasCobro.SetActive(true);
        barraCobro.value = 0;

        //Guarda una copia segura del cliente antes de que empiece a cargar la barra
        Cliente clienteAFrenarYborrar = clienteActual;

        float tiempoTranscurrido = 0;
        while (tiempoTranscurrido < tiempoCobranza)
        {
            tiempoTranscurrido += Time.deltaTime;
            barraCobro.value = tiempoTranscurrido / tiempoCobranza;
            yield return null;
        }

        //finaliza la carga visual
        estaCobrando = false;
        canvasCobro.SetActive(false);
        Debug.Log("Barra de cobro llena");

        // Destrucción: Dar la orden de desaparecer al cliente guardado
        if(clienteAFrenarYborrar != null)
        {
            clienteAFrenarYborrar.DesaparecerCliente();
            clienteActual = null; // Vacia la caja para el sieguiente cliente!
        }
        else
        {
            Debug.LogError("Error: ka barra terminó pero (ClienteActual) se volvió NULL durnte el proceso.");
        }
    }
}