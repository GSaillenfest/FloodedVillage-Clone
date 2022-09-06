using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationDeGrille : MonoBehaviour
{
    [SerializeField] public GameObject[] tilesPrefabs;
    [SerializeField] GameObject parent;
    [SerializeField] int width_X = 5;
    [SerializeField] int height_Y = 5;

    public List<Vector2Int> generatedTiles = new List<Vector2Int>();
    List<Vector2Int> newGeneratedTiles = new List<Vector2Int>();
    List<Vector2Int> toDestroyTiles = new List<Vector2Int>();
    int[,] table = new int[100, 100];

    public void GenerateGrid()
    {

        for (int x = 0; x < width_X; x++)
        {
            for (int y = 0; y < height_Y; y++)
            {
                int randomIndex = Random.Range(0, 6);
                table[x, y] = randomIndex;
                GameObject spawnedTile = Instantiate(tilesPrefabs[randomIndex], new Vector3(x, y), Quaternion.identity, parent.transform);
                spawnedTile.name = $"tile {x} {y}";
                if (spawnedTile.CompareTag("water"))
                {
                    //Debug.Log("water");
                    generatedTiles.Add(new Vector2Int(x,y));
                }

            }
        }

        //Debug.Log(table[2,3]);
    }

    public void SupprGrid()
    {

        generatedTiles = new List<Vector2Int>();

        int childCount = parent.transform.childCount;
        for (int i = childCount; i > 0; i--)
        {
            DestroyImmediate(parent.transform.GetChild(0).gameObject);
        }
    }

    public void React(List<Vector2Int> coordonatesList)
    {
            Debug.Log("React appelé");
            Debug.Log(coordonatesList.Count);

        if (coordonatesList.Count == 0) return;
        else
        {
            newGeneratedTiles = new List<Vector2Int>();
            toDestroyTiles = new List<Vector2Int>();

            foreach (Vector2Int coordonates in coordonatesList)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2Int offSet = Vector2Int.zero;
                    switch (i)
                    {
                        case 0:
                            offSet = new Vector2Int(1 , 0);
                            break;
                        case 1:
                            offSet = new Vector2Int(0, 1);
                            break;
                        case 2:
                            offSet = new Vector2Int(-1, 0);
                            break;
                        case 3:
                            offSet = new Vector2Int(0, -1);
                            break;
                        default:
                            break;
                    }

                    Vector2Int nextTile = coordonates + offSet;

                    if (table[nextTile.x,nextTile.y] == (int) TileType.empty)
                    {
                        table[nextTile.x, nextTile.y] = (int) TileType.water;
                        GameObject newTile = Instantiate(tilesPrefabs[(int) TileType.water], new Vector3(nextTile.x, nextTile.y), Quaternion.identity, parent.transform);
                        newTile.name = $"tile {nextTile.x} {nextTile.y}";
                        newGeneratedTiles.Add(nextTile);
                        //Supprimer le gameObject à ces coordonnées (avec ou sans raycast ?)
                    }

                    /*Ray ray = new Ray(coordonates.transform.position, Vector3.forward * 10f);

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
                            case "stone":
                                break;
                            default:
                                break;
                        }
                    }*/

                }

            }
            for (int i = 0; i < toDestroyTiles.Count; i++)
            {
            DestroyImmediate(toDestroyTiles[0]);
            }
            StartCoroutine(Waiting());

        }
    
        IEnumerator Waiting()
        {
            yield return new WaitForSeconds(0.5f);
            React(newGeneratedTiles);
        }
    }


    public enum TileType
    {
        empty,
        sand,
        water,
        stone,
        seed,
        crops,
    }
}
