using UnityEngine;

public class FlagSpawner : MonoBehaviour
{
    public GameObject blueFlagPrefab;
    public GameObject redFlagPrefab;
    public Transform blueBase;
    public Transform redBase;
    public ScoreManager scoreManager;

    void Start()
    {
        SpawnFlags();   
    }

    public void SpawnFlags()
    {
        // Instantiate blue flag at red base
        Instantiate(blueFlagPrefab, redBase.position, Quaternion.identity);

        // Instantiate red flag at blue base
        Instantiate(redFlagPrefab, blueBase.position, Quaternion.identity);
    }

    public void RespawnFlags()
    {
        // Destroy existing flags
        Destroy(GameObject.FindWithTag("BlueFlag"));
        Destroy(GameObject.FindWithTag("RedFlag"));

        
        
    }
}