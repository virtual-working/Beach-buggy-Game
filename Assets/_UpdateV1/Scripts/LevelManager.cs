using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private NavMeshSurface navMeshBuilder;

    [SerializeField]
    private string levelToLoad = "Level_";

    public int indexToLoadScene;

    public GameObject levelObj;

    private void Awake()
    {
        var levelObj = Resources.Load(levelToLoad + (indexToLoadScene+1));
        this.levelObj = Instantiate(levelObj, Vector3.zero, Quaternion.identity) as GameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        navMeshBuilder.BuildNavMesh();
    }
}
