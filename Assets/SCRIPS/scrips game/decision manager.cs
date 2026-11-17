using UnityEngine;

public class decisionmanager : MonoBehaviour
{
    public static decisionmanager Instance;

    [Header("Sistema de Puntuación")]
    private int puntosMalasDecisiones = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void TomarDecision(bool esBuenaDecision)
    {
        if (esBuenaDecision)
        {
            puntosMalasDecisiones += 0;
            Debug.Log("¡Buena decisión! Puntos de error: " + puntosMalasDecisiones);
        }
        else
        {
            puntosMalasDecisiones += 1;
            Debug.Log("Mala decisión... Puntos de error: " + puntosMalasDecisiones);
        }
    }

    public void EvaluarFinal()
    {
        if (puntosMalasDecisiones == 0)
        {
            MostrarFinal("Final Perfecto: Tomaste el camino ideal sin cometer errores.");
        }
        else if (puntosMalasDecisiones >= 1 && puntosMalasDecisiones <= 3)
        {
            MostrarFinal("Final Bueno: Cometiste algunos errores, pero lograste resolver el problema principal.");
        }
        else if (puntosMalasDecisiones >= 4 && puntosMalasDecisiones <= 6)
        {
            MostrarFinal("Final Neutral: Sobreviviste, pero las consecuencias de tus actos dejaron secuelas.");
        }
        else
        {
            MostrarFinal("Final Malo: Todo salió terriblemente mal debido a tus constantes malas decisiones.");
        }
    }

    private void MostrarFinal(string mensajeFinal)
    {
        Debug.Log("*** DESBLOQUEASTE UN FINAL ***\n" + mensajeFinal);
    }

    public int ObtenerPuntos() => puntosMalasDecisiones;
}
