using UnityEngine;


using SQLite;

[System.Serializable]
public class DialogueData
{
    [PrimaryKey]
    public int id { get; set; }

    public string speaker { get; set; }

    public string text { get; set; }

    [Column("next_id")] // Asocia la propiedad 'nextId' con la columna 'next_id' de SQLite
    public int ? nextId  { get; set; }

    [Column("decision_id")] // Asocia 'decisionId' con 'decision_id'
    public int ? decisionId  { get; set; }
}

[System.Serializable]
public class OptionData
{
    [Column("decision_id")]
    public int decisionId { get; set; }

    [Column("option_text")]
    public string optionText { get; set; }

    [Column("target_dialogue_id")]
    public int targetDialogueId { get; set; }

    [Column("es_buena_decision")]
    public bool esBuenaDecision { get; set; }
}