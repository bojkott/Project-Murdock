using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

    // Phases
    public enum PhaseID
    {
        PRE = 0,
        ONE,
        TWO,
        THREE,
        POST,
        ERR
    };

    public static PhaseID lastPhase;
    public static PhaseID currentPhase;

    // Phase1
    public GameObject phoneBody;
    public GameObject phoneHead;
    public GameObject vulkanBord1;

    // Phase2
    public GameObject vulkanBord2;
    public GameObject callBell;
    public GameObject room;
    public GameObject fan1;

    // Phase3
    public GameObject shoe1;
    public GameObject shoe2;

    // Use this for initialization
    void Start()
    {
        lastPhase = PhaseID.PRE;
        currentPhase = PhaseID.ONE;

        Deinit1Phase();
        Deinit2Phase();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            currentPhase = PhaseID.ONE;
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            currentPhase = PhaseID.TWO;
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            currentPhase = PhaseID.THREE;
        }

        if (lastPhase != currentPhase)
        {
            PhaseID nextPhase;

            // Deinit
            switch (lastPhase)
            {
                case PhaseID.PRE:
                    nextPhase = PhaseID.ONE;
                    break;
                case PhaseID.ONE:
                    Deinit1Phase();
                    nextPhase = PhaseID.TWO;
                    break;
                case PhaseID.TWO:
                    Deinit2Phase();
                    nextPhase = PhaseID.THREE;
                    break;
                case PhaseID.THREE:
                    Deinit3Phase();
                    nextPhase = PhaseID.ONE;
                    break;
                default:
                    nextPhase = PhaseID.ERR;
                    break;
            }

            // Update last phase
            lastPhase = currentPhase;

            // Init
            switch (nextPhase)
            {
                case PhaseID.PRE:
                    // NONE
                    break;
                case PhaseID.ONE:
                    Init1Phase();
                    break;
                case PhaseID.TWO:
                    Init2Phase();
                    break;
                case PhaseID.THREE:
                    Init3Phase();
                    break;
                case PhaseID.ERR:
                    // NONE
                    break;
                default:
                    // NONE
                    break;
            }

            // Update current phase.
            currentPhase = nextPhase;
        }
	}

    void Init1Phase()
    {
        phoneBody.SetActive(true);
        phoneHead.SetActive(true);
        vulkanBord1.SetActive(true);
    }
    void Init2Phase()
    {
        vulkanBord2.SetActive(true);
        callBell.SetActive(true);
        room.SetActive(true);
        fan1.SetActive(true);
    }
    void Deinit1Phase()
    {
        phoneBody.SetActive(false);
        phoneHead.SetActive(false);
        vulkanBord1.SetActive(false);
    }

    void Deinit2Phase()
    {
        vulkanBord2.SetActive(false);
        callBell.SetActive(false);
        room.SetActive(false);
        fan1.SetActive(false);
    }

    void Init3Phase()
    {
        shoe1.SetActive(true);
        shoe2.SetActive(true);
    }

    void Deinit3Phase()
    {
        shoe1.SetActive(false);
        shoe2.SetActive(false);
    }

}
