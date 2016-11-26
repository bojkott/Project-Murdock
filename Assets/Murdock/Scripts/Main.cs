using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

    // Phases
    public enum PhaseID
    {
        PRE = 0,
        ONE,
        TWO,
        POST,
        ERR
    };

    public static PhaseID lastPhase;
    public static PhaseID currentPhase;

    // Phase1
    private GameObject phoneBody;
    private GameObject phoneHead;
    private GameObject vulkanBord;

    // Phase2
    private GameObject shoeLeft;
    private GameObject room;


    // Use this for initialization
    void Start()
    {
        lastPhase = PhaseID.PRE;
        currentPhase = PhaseID.ONE;
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
        this.phoneBody = (GameObject)Instantiate(Resources.Load("prefabs/phoneBody"));
        this.phoneHead = (GameObject)Instantiate(Resources.Load("prefabs/phoneHead"));
        this.vulkanBord = (GameObject)Instantiate(Resources.Load("prefabs/vulkanBord"));
    }
    void Init2Phase()
    {
        this.shoeLeft = (GameObject)Instantiate(Resources.Load("prefabs/ShoeLeft"));
        this.room = (GameObject)Instantiate(Resources.Load("prefabs/Room"));

    }
    void Deinit1Phase()
    {
        Destroy(this.phoneBody);
        Destroy(this.vulkanBord);
    }

    void Deinit2Phase()
    {
        Destroy(this.shoeLeft);
        Destroy(this.room);
        Destroy(this.phoneHead);
    }

}
