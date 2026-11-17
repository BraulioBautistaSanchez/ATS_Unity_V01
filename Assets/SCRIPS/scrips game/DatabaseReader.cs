using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SQLite; 

public class DatabaseReader : MonoBehaviour
{
    private string dbPath;

    private void Awake()
    {
        string fileName = "JuegoDialogos.db";
        dbPath = Path.Combine(Application.streamingAssetsPath, fileName);
    }

    
    public DialogueData ObtenerDialogoPorId(int idBuscado)
    {
        using (var dbConnection = new SQLiteConnection(dbPath))
        {
            // Al usar SELECT *, SQLite.cs aplica automáticamente las etiquetas [Column("next_id")]
            string query = "SELECT * FROM Dialogos WHERE id = ?";
            var resultados = dbConnection.Query<DialogueData>(query, idBuscado);

            return resultados.Count > 0 ? resultados[0] : null;
        }
    }

    
    public List<OptionData> ObtenerOpcionesPorDecisionId(int decisionIdBuscado)
    {
        using (var dbConnection = new SQLiteConnection(dbPath))
        {
            string query = "SELECT decision_id as decisionId, option_text as optionText, target_dialogue_id as targetDialogueId, es_buena_decision as esBuenaDecision FROM Opciones WHERE decision_id = ?";
            return dbConnection.Query<OptionData>(query, decisionIdBuscado);
        }
    }
}