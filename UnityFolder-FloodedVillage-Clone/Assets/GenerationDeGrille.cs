using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationDeGrille : MonoBehaviour
{
    [SerializeField] GameObject[] tilesPrefabs;
    [SerializeField] GameObject parent;
    [SerializeField] int width_X = 5;
    [SerializeField] int height_Y = 5;

    public List<GameObject> generatedTiles = new List<GameObject>();


    public void GenerateGrid()
    {

        for (int x = 0; x < width_X; x++)
        {
            for (int y = 0; y < height_Y; y++)
            {
                GameObject spawnedTile = Instantiate(tilesPrefabs[Random.Range(0, tilesPrefabs.Length)], new Vector3(x - 0.5f, y - 0.5f), Quaternion.identity, parent.transform);
                spawnedTile.name = $"tile {x} {y}";
                if (spawnedTile.CompareTag("water"))
                {
                    //Debug.Log("water");
                    generatedTiles.Add(spawnedTile);
                }

            }
        }

        //Debug.Log(generatedTiles.Count);
    }

    public void SupprGrid()
    {

        generatedTiles = new List<GameObject>();

        int childCount = parent.transform.childCount;
        for (int i = childCount; i > 0; i--)
        {
            DestroyImmediate(parent.transform.GetChild(0).gameObject);
        }
    }

    public void React(List<GameObject> list)
    {
            Debug.Log("React appelé");
            Debug.Log(list.Count);

        if (list.Count == 0) return;
        else
        {
            List<GameObject> newGeneratedTiles = new List<GameObject>();
            List<GameObject> toDestroyTiles = new List<GameObject>();

            foreach (GameObject tile in list)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector3 offSet = Vector3.zero;
                    switch (i)
                    {
                        case 0:
                            offSet = new Vector3(1.5f, 0.5f, -1f);
                            break;
                        case 1:
                            offSet = new Vector3(0.5f, 1.5f, -1f);
                            break;
                        case 2:
                            offSet = new Vector3(-.5f, 0.5f, -1f);
                            break;
                        case 3:
                            offSet = new Vector3(0.5f, -0.5f, -1f);
                            break;
                        default:
                            break;
                    }

                    Ray ray = new Ray(tile.transform.position + offSet, Vector3.forward * 10f);

                    //Debug.DrawRay(tile.transform.position + offSet, Vector3.forward * 2f, Color.green, 20f, false);
                    RaycastHit hit;
                    Physics.Raycast(ray, out hit);

                    if (hit.collider != null)
                    {
                        switch (hit.collider.tag)
                        {
                            case "empty":
                                Vector3 newTilePos = hit.collider.transform.position;
                                toDestroyTiles.Add(hit.collider.gameObject);
                                GameObject newTile = Instantiate(tilesPrefabs[5], newTilePos, Quaternion.identity, parent.transform);
                                newTile.name = $"tile {newTile.transform.position.x +0.5f} {newTile.transform.position.y + 0.5f}";
                                newGeneratedTiles.Add(newTile);
                                break;
                            case "sand":
                                break;
                            case "water":
                                break;
                            case "seeds":
                                GameObject newTileCrops = Instantiate(tilesPrefabs[0], hit.collider.transform.position, Quaternion.identity, parent.transform);
                                newTileCrops.name = $"tile {newTileCrops.transform.position.x +0.5f} {newTileCrops.transform.position.y + 0.5f}";
                                DestroyImmediate(hit.collider.gameObject);
                                break;
                            case "crops":
                                break;
                            case "rock":
                                break;
                            default:
                                break;
                        }
                    }

                }

            }
            for (int i = 0; i < toDestroyTiles.Count; i++)
            {
            DestroyImmediate(toDestroyTiles[0]);
            }
            React(newGeneratedTiles);
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
