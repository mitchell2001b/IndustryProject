using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameTransitionHandler : MonoBehaviour
{
    [SerializeField] List<Room> rooms;
    [System.Serializable]
    public class Room
    {
        public RoomType room;
        public GameObject preGameEnviroment;
        public GameObject preGameCanvas;

        public GameObject inGameCanvas;
        public GameObject inGameEnviroment;

        public void TransitionToPreGame()
        {
            preGameCanvas.SetActive(true);
            preGameEnviroment.SetActive(true);
            preGameCanvas.GetComponent<Canvas>().enabled = true;
            inGameEnviroment.SetActive(false);
            inGameCanvas.SetActive(false);
        }
        
    }


    public enum RoomType { 
        sumPuzzle,
        fractionPuzzle,
        metricPuzzle,       
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RoomTransition(RoomType roomType)
    {
        rooms.Find(x => x.room == roomType).TransitionToPreGame();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
