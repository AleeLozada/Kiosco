using System.Collections.Generic;
using UnityEngine;


public class GeneradorClientes : MonoBehaviour
{
    [Header("Configuracion")]
    public GameObject clientePrefab;
    public Transform puntoSpawn;

    [Header("Posiciones de la fila")]
    public List<Transform> posicionesFila;

    [Header("Tiempos")]
    public float tiemposEntreClientes = 4.0f; //Cada cuanto aparece un cliente en segundos!

    private List<Cliente> clientesEnFila = new List<Cliente>();
    public int maxClientesEnPantalla = 3; //Cantidad de clientes maximo en pantalla
    private float temporizador = 0f;

    void Start()
    {
        if (puntoSpawn == null) puntoSpawn = transform;
        SpawnearCliente();
    }
    void Update()
    {
        // Limpiamos la lista sacando a los clientes que la caja ya destruyo
        clientesEnFila.RemoveAll(item => item == null);

        //Actualiza el lugar de la fila de cada cliente vivo
        ActualizarPosicionesDeFila();

        //Solo creamos un cliente si hay espacio libre en las posiciones de la fila
        if (clientesEnFila.Count < posicionesFila.Count)
        {
            temporizador += Time.deltaTime;
            if (temporizador >= tiemposEntreClientes)
            {
                SpawnearCliente();
                temporizador = 0f;
            }
        }
    }
    void SpawnearCliente()
    {
        if (clientePrefab != null)
        {
            GameObject nuevoClienteObj = Instantiate(clientePrefab, puntoSpawn.position, Quaternion.identity);
            Cliente nuevoCliente = nuevoClienteObj.GetComponent<Cliente>();

            if (nuevoCliente != null)
            {
                clientesEnFila.Add(nuevoCliente);
            }
        }
    }

    void ActualizarPosicionesDeFila()
    {
        for (int i = 0; i < clientesEnFila.Count; i++)
        {
            if (clientesEnFila[i] != null && i < posicionesFila.Count)
            {
                //Asigna a cada cliente su destino correspondiente en la fila
                clientesEnFila[i].AsignarNuevoDestino(posicionesFila[i]);
            }
        }
    }
}
