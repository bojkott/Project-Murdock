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
    public GameObject phoneScene;

    // Phase2
    public GameObject reception;

    // Phase3
    public GameObject shoeScene;

    // Use this for initialization
    void Start()
    {
        lastPhase = PhaseID.PRE;
        currentPhase = PhaseID.ONE;

        Deinit1Phase();
        Deinit2Phase();
        Deinit3Phase();
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
        phoneScene.SetActive(true);
    }
    void Init2Phase()
    {
        reception.SetActive(true);
        
    }

    void Init3Phase()
    {
        shoeScene.SetActive(true);
    }

    void Deinit1Phase()
    {
        phoneScene.SetActive(false);
    }

    void Deinit2Phase()
    {
        reception.SetActive(false);
    }

    void Deinit3Phase()
    {
        shoeScene.SetActive(false);
    }

}
