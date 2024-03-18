using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class MapGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [RuntimeInitializeOnLoadMethod]
    void Start()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "Maps/Data/stage_1.json");
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            TileMapConfig loadedData = JsonUtility.FromJson<TileMapConfig>(dataAsJson);

            for (int y = 0; y < loadedData.mapSize["height"]; y++)
            {
                for (int x = 0; x < loadedData.mapSize["width"]; x++)
                {
                    int tileID = loadedData.map[y][x];
                    Tile tile = loadedData.tiles.Find(t => t.id == tileID);
                    if (tile != null)
                    {
                        GameObject go = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                        SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
                        sr.sprite = Resources.Load<Sprite>($"Maps/Tiles/{tile.path}");
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Cannot find map configuration file!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Serializable]
    public class Tile
    {
        public int id;
        public string name;
        public string path;
    }

    [System.Serializable]
    public class TileMapConfig
    {
        public int tileSize;
        public Dictionary<string, int> mapSize = new Dictionary<string, int>();
        public List<Tile> tiles;
        public List<List<int>> map;
        public List<object> objects; // Modifica según la estructura de tus objetos
    }

    public GameObject tilePrefab;
}
