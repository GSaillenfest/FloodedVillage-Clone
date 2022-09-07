using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridManager))]

public class GenerationDeGrilleEditor : Editor

{
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        GridManager generator = (GridManager) target;

        if (GUILayout.Button("Générer une grille"))
        {
            generator.GenerateGrid();
        }
        
        if (GUILayout.Button("Supprimer la grille"))
        {
            generator.SupprGrid();
        }
        
        if (GUILayout.Button("Reaction"))
        {
            generator.React(generator.generatedTiles);
        }
    }
}
