using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }

    // Example method to save a Vector3 position as JSON
    public void SavePosition(Vector3 position)
    {
        string filePath = Application.dataPath + "/Chackpoint";
        Vector3Data positionData = new Vector3Data(position);
        string json = JsonUtility.ToJson(positionData);
        System.IO.File.WriteAllText(filePath, json);
    }

    // Example method to load a Vector3 position from JSON
    public Vector3 LoadPosition()
    {
        string filePath = Application.dataPath + "/Chackpoint";
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            Vector3Data positionData = JsonUtility.FromJson<Vector3Data>(json);
            return positionData.ToVector3();
        }
        else
        {
            Debug.LogWarning("File not found: " + filePath);
            return Vector3.zero;
        }
    }
}