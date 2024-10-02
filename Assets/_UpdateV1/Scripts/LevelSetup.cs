using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetup : MonoBehaviour
{
    [SerializeField]
    private Transform playerSpawnPosition;

    [SerializeField]
    private Transform[] AISpawnPosition;

    [SerializeField]
    private RCC_AIWaypointsContainer[] rccAIWayPointContainer;

    [SerializeField]
    private List<RCC_AICarController> rccAICarControllers;

    public string playerNameToLoad;

    public int carToLoad;

    public List<string> AICarNames;

    public string AICarResourcesPath;

    [SerializeField]
    private float rotationAngleY;

    private void Awake()
    {
        var playerCar = Resources.Load(playerNameToLoad + (carToLoad + 1));
        CarManager.Instance.RCC_PlayerCar = Instantiate(playerCar, new Vector3(playerSpawnPosition.position.x, playerSpawnPosition.position.y,
            playerSpawnPosition.position.z), Quaternion.Euler(new Vector3(0, rotationAngleY, 0))) as GameObject;

        for(int i = 0; i < AISpawnPosition.Length; i++)
        {
            var AIPlayerCar = Resources.Load(AICarResourcesPath + AICarNames[i]);
            GameObject AIObject = Instantiate(AIPlayerCar, AISpawnPosition[i].position, Quaternion.Euler(new Vector3(0, rotationAngleY, 0))) as GameObject;
            AIObject.GetComponent<RCC_CarControllerV3>().canControl = false;
            /*rccAICarControllers.Add(rccAICAr);*/
            AIObject.GetComponent<RCC_AICarController>().waypointsContainer = rccAIWayPointContainer[i];

            AIObject.GetComponent<RCC_CarControllerV3>().canControl = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
