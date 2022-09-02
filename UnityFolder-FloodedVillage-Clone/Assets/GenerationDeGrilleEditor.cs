using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GenerationDeGrille))]

public class GenerationDeGrilleEditor : Editor

{
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        GenerationDeGrille generator = (GenerationDeGrille) target;

        if (GUILayout.Button("Générer une grille"))
        {
            generator.GenerateGrid();
        }
        
        if (GUILayout.Button("Supprimer la grille"))
        {
            generator.SupprGrid();
        }
    }
}
