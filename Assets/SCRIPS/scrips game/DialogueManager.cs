using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Si usas TextMeshPro

public class DialogueManager : MonoBehaviour
{
    [Header("Conexión con Base de Datos")]
    public DatabaseReader dbReader;

    [Header("Referencias de UI")]
    public TMP_Text textoSpeaker;
    public TMP_Text textoDialogo;
    public Button botonSiguiente;
    public Transform opcionesContainer;

    [Header("Prefabs UI")]
    public GameObject botonDecisionPrefab;

    private void Start()
    {
        MostrarDialogo(1); // Inicia en el diálogo ID 1
    }

    private int idSiguienteGuardado = -1;

    public void MostrarDialogo(int dialogueId)
    {
        if (dialogueId == -1)
        {
            Debug.Log("[DIÁLOGO] Se alcanzó el fin de la narrativa (ID: -1). Evaluando final...");
            decisionmanager.Instance.EvaluarFinal();
            return;
        }

        LimpiarOpciones();

        DialogueData dialogoActual = dbReader.ObtenerDialogoPorId(dialogueId);

        if (dialogoActual != null)
        {
            // LOG DE CONTROL: Mira este mensaje en la consola de Unity al presionar Play
            Debug.Log($"<color=cyan>[CARGADO]</color> ID Actual: {dialogoActual.id} | Siguiente ID registrado en BD: {dialogoActual.nextId}");

            if (textoSpeaker != null) textoSpeaker.text = dialogoActual.speaker;
            if (textoDialogo != null) textoDialogo.text = dialogoActual.text;

            int idDecision = dialogoActual.decisionId ?? 0;

            if (idDecision > 0)
            {
                if (botonSiguiente != null) botonSiguiente.gameObject.SetActive(false);
                List<OptionData> opciones = dbReader.ObtenerOpcionesPorDecisionId(idDecision);
                Debug.Log($"Se encontraron {opciones?.Count ?? 0} opciones para la decisión {idDecision}");

                if (opciones != null && opciones.Count > 0)
                {
                    DesplegarOpciones(opciones);
                }
            }
            else
            {
                // Diálogo lineal
                if (botonSiguiente != null)
                {
                    botonSiguiente.gameObject.SetActive(true);

                    // Guardamos el siguiente ID en la variable global de la clase
                    idSiguienteGuardado = dialogoActual.nextId ?? -1;

                    // Limpiamos los listeners y asignamos el método auxiliar sin expresiones Lambda () =>
                    botonSiguiente.onClick.RemoveAllListeners();
                    botonSiguiente.onClick.AddListener(AvanzarSiguienteDialogo);
                }
            }
        }
        else
        {
            Debug.LogError($"<color=red>[ERROR BD]</color> No se encontró el diálogo ID {dialogueId}");
        }
    }

    // Método auxiliar para el botón
    private void AvanzarSiguienteDialogo()
    {
        MostrarDialogo(idSiguienteGuardado);
    }

    private void DesplegarOpciones(List<OptionData> opciones)
    {
        foreach (var opcion in opciones)
        {
            GameObject nuevoBoton = Instantiate(botonDecisionPrefab, opcionesContainer);
            TMP_Text textoBoton = nuevoBoton.GetComponentInChildren<TMP_Text>();
            textoBoton.text = opcion.optionText;

            Button btnComponent = nuevoBoton.GetComponent<Button>();
            btnComponent.onClick.AddListener(() => SeleccionarOpcion(opcion));
        }
    }

    public void SeleccionarOpcion(OptionData opcion)
    {
        decisionmanager.Instance.TomarDecision(opcion.esBuenaDecision);
        MostrarDialogo(opcion.targetDialogueId);
    }

    private void LimpiarOpciones()
    {
        foreach (Transform child in opcionesContainer)
        {
            Destroy(child.gameObject);
        }
    }
}