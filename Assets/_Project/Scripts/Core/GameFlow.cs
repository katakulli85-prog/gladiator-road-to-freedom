using UnityEngine;

// Sahneler arasi gecis yoneticisi (kalici)
public static class GameFlow
{
    public const string MenuScene = "Assets/_Project/Scenes/MainMenu.unity";
    public const string ArenaScene = "Assets/_Project/Scenes/Boot.unity";
}

public class GameFlowRunner : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Debug.Log("[Flow] GameFlowRunner aktif");
    }
}
