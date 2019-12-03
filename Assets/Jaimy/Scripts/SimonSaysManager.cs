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
    // test game master
    public GameObject gameMaster;
    public GameObject flagA;
    public GameObject flagB;
    // test player
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
        Debug.Log("Game starts");
        yield return new WaitForSeconds(4);
        for (int i = 0; i < simonSaysTest.Count; i++)
        {
            currentFlags = simonSaysTest[i];
            
            NextFlag(currentFlags);
            FlagChange();
            yield return new WaitForSeconds(5);
            FlagReset();
            PlayersInputCheck(1, currentFlags);
            yield return new WaitForSeconds(2);
        }
    }

    public void FlagChange()
    {
         flagA = gameMaster.transform.GetChild(0).gameObject;
         flagB = gameMaster.transform.GetChild(1).gameObject;
         
         if (currentFlags == Flags.BlueFlag)
         {
             StartCoroutine(ShowFlag(flagB,flagB.transform.forward * 45,0.8f));
         }

         if (currentFlags == Flags.RedFlag)
         {
             StartCoroutine(ShowFlag(flagA,flagB.transform.forward * -45,0.8f));
         }

         if (currentFlags == Flags.BothFlags)
         {
             Debug.Log("test");
             StartCoroutine(ShowFlag(flagA,flagA.transform.forward * -45,0.8f));
             StartCoroutine(ShowFlag(flagB,flagB.transform.forward * 45,0.8f));
         }
    }

    public void FlagReset()
    {
        if (currentFlags == Flags.BlueFlag)
        {
            StartCoroutine(ShowFlag(flagB,flagB.transform.forward * -45,0.8f));
        }

        if (currentFlags == Flags.RedFlag)
        {
            StartCoroutine(ShowFlag(flagA,flagB.transform.forward * 45,0.8f));
        }

        if (currentFlags == Flags.BothFlags)
        {
            Debug.Log("test");
            StartCoroutine(ShowFlag(flagA,flagA.transform.forward * 45,0.8f));
            StartCoroutine(ShowFlag(flagB,flagB.transform.forward * -45,0.8f));
        }
    }

    IEnumerator ShowFlag(GameObject flagArm, Vector3 byAngles, float inTime) 
    {    
        Quaternion fromAngle = flagArm.transform.rotation;
        Quaternion toAngle = Quaternion.Euler(flagArm.transform.eulerAngles + byAngles);
        for(var t = 0f; t <= 1; t += Time.deltaTime/inTime) {
            flagArm.transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
        flagArm.transform.rotation = toAngle;
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
