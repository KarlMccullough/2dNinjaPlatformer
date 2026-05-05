using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    private static Door instance;

    public GameManager1 gm;

    public SceneFader sceneFader;

    public static Door Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Door>();
            }
            return instance;
        }
    }

    Animator anim;

    [SerializeField]
    public GameObject DoorType;

    public int stateOfDoor = 1;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (GameManager1.Instance.noOfSwitches <= 0)
        {
            GameManager1.Instance.WinLvl();
        }
    }

    public void Start()
    {
        gm = FindObjectOfType<GameManager1>();
        anim = GetComponent<Animator>();

        if (DoorType.name == "EntryDoor")
        {
            anim.SetFloat("DoorState", 3);
        }
        if (DoorType.name == "ExitDoor")
        {
            LockDoor();
        }
    }

    public void LockDoor()
    {
        if (DoorType.name == "ExitDoor")
        {
            anim.SetFloat("DoorState", 1);
            stateOfDoor = 1;
        }
    }

    public void UnlockDoor()
    {
        if (DoorType.name == "ExitDoor")
        {
            anim.SetFloat("DoorState", 2);
            stateOfDoor = 2;
        }
    }

    public void OpenDoor()
    {
        if (DoorType.name == "ExitDoor")
        {
            anim.SetFloat("DoorState", 3);
            stateOfDoor = 3;
        }
    }

    public void SetDoorState(int state)
    {
        if (state == 1 && DoorType.name == "ExitDoor")
        {
            LockDoor();
        }
        if (state == 2 && DoorType.name == "ExitDoor")
        {
            UnlockDoor();
        }
        if (state == 3 && DoorType.name == "ExitDoor")
        {
            OpenDoor();
        }
    }

    public int GetDoorState()
    {
        return stateOfDoor;
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("Score", gm.collectedCoins);
    }
}
