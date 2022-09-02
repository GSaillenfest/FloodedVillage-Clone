using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationDeGrille : MonoBehaviour
{
    [SerializeField] GameObject tile;
    [SerializeField] GameObject parent;
    int width = 5;
    int height = 5;


    public void GenerateGrid()
    {

        for (int x = 0; x < width; x++)
        {
            for (int y = 0 ; y < height; y++)
            {
                GameObject spawnedTile = Instantiate(tile, new Vector3(x, -y), Quaternion.identity, parent.transform);
                spawnedTile.name = $"tile {x} {y}";
            }
        }
    }

    public void SupprGrid()
    {
        int childCount = parent.transform.childCount;
        for (int i = childCount; i > 0; i--)
        {
            DestroyImmediate(parent.transform.GetChild(0).gameObject);
        }
        
    }

    public enum TileType
    {
        sand,
        water,
        rock,
        seed
    }
}
