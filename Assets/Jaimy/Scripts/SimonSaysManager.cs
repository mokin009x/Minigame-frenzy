using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Timers;
using UnityEngine;

public class SimonSaysManager : MonoBehaviour
{

    public Flags currentFlags;
    public List<Flags> simonSaysTest;
    public List<float> normalTimers;
    // test
    public List<int> players;
    public bool player1Both = false;
    public bool player1RedFlag = false;
    public bool player1BlueFlag = false;
    public enum Flags
    {
        RedFlag,
        BlueFlag,
        BothFlags,
        NoFlags
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartSimonSays()
    {
        StartCoroutine(GameLoop());
    }

    public IEnumerator GameLoop()
    {
        for (int i = 0; i < simonSaysTest.Count; i++)
        {
            currentFlags = simonSaysTest[i];
            
            NextFlag(currentFlags);
            yield return new WaitForSeconds(4);
            for (int j = 0; j < players.Count; j++)
            {

            }
            PlayersInputCheck(1, currentFlags);

            
        }
    }

    public void NextFlag(Flags newFlag)
    {
        if (newFlag == Flags.BlueFlag)
        {
            Debug.Log("Blue");
        }

        if (newFlag == Flags.RedFlag)
        {
            Debug.Log("Red");
        }

        if (newFlag == Flags.BothFlags)
        {
            Debug.Log("Both flags");
        }
    }

    public bool PlayersInputCheck(int playerId, Flags currentFlag)
    {
        bool redPressed = false;
        bool bluePressed = false;
        Flags playerFlag = Flags.NoFlags;
       
        
        if (Input.GetKey("joystick button 4"))
        {
            redPressed = true;
        }
        
        if (Input.GetKey("joystick button 5"))
        {
            bluePressed = true;
        }

        if (redPressed == true && bluePressed == true)
        {
            playerFlag = Flags.BothFlags;
            if (playerFlag == currentFlag)
            {
                Debug.Log("Correct");
                return true;
            }
        }
        else
        {
            if (bluePressed)
            {
                playerFlag = Flags.BlueFlag;

            }

            if (redPressed)
            {
                playerFlag = Flags.RedFlag;

            }
            
            if (playerFlag == currentFlag)
            {
                Debug.Log("Correct");
                return true;
            }
        }
        Debug.Log("Wrong");
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("joystick button 0"))
        {
            StartSimonSays();
        }

        
    }
}
