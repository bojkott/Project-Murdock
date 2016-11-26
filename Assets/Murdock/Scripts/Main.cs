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
        ERR,
        NONE
    };

    public static PhaseID lastPhase;
    public static PhaseID currentPhase;

    public GameObject preScenePrefab;

    // Phase1
    public GameObject phoneScenePrefab;

    // Phase2
    public GameObject receptionPrefab;

    // Phase3
    public GameObject shoeScenePrefab;

    private GameObject preSceneInstance;
    private GameObject phoneSceneInstance;
    private GameObject receptionSceneInstance;
    private GameObject shoeSceneInstance;

    private ScreenFader fader;
    private float time = 0;

    // Use this for initialization
    void Start()
    {
        fader = GetComponent<ScreenFader>();
        lastPhase = PhaseID.NONE;
        currentPhase = PhaseID.PRE;
        preSceneInstance = Instantiate(preScenePrefab);
        preSceneInstance.SetActive(true);
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
            if (fader.fadeIn == true)
                fader.fadeIn = false;

            switch (lastPhase)
            {
                case PhaseID.NONE:
                    nextPhase = PhaseID.PRE;
                    break;
                case PhaseID.PRE:
                    Deinit0Phase();
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
            time += Time.deltaTime;
            if(time > 2.0)
            {
                lastPhase = currentPhase;
                fader.fadeIn = true;
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
                
                currentPhase = nextPhase;
                               
            }
            

            // Update last phase
            

            

            // Update current phase.
            
        }
	}


    void Deinit0Phase()
    {
        Destroy(preSceneInstance);
    }

    void Init1Phase()
    {
        phoneSceneInstance = Instantiate(phoneScenePrefab);
        phoneSceneInstance.SetActive(true);
    }
    void Init2Phase()
    {
        receptionSceneInstance = Instantiate(receptionPrefab);
        receptionSceneInstance.SetActive(true);
        
    }

    void Init3Phase()
    {
        shoeSceneInstance = Instantiate(shoeScenePrefab);
        shoeSceneInstance.SetActive(true);
    }

    void Deinit1Phase()
    {
        Destroy(phoneSceneInstance);
    }

    void Deinit2Phase()
    {
        Destroy(receptionSceneInstance);
    }

    void Deinit3Phase()
    {
        Destroy(shoeSceneInstance);
    }

}
