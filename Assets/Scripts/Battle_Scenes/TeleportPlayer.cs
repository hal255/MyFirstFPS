using UnityEngine;
using System.Collections;

public class TeleportPlayer : MonoBehaviour {

    private PlayerCharacter player;
    private bool quiz_open = false;
    public string teleport_id;

    void Start()
    {
        teleport_id = gameObject.name;
        player = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerCharacter>();
    }


    void Awake()
    {
        Messenger<string>.AddListener(GameEvent.TELEPORT_PLAYER, teleportPlayer);
    }
    void OnDestroy()
    {
        Messenger<string>.RemoveListener(GameEvent.TELEPORT_PLAYER, teleportPlayer);
    }

    void OnTriggerEnter(Collider other)
    {
        teleport_id = gameObject.name;
        Debug.Log("TeleportID=" + teleport_id);

        player = other.GetComponent<PlayerCharacter>();

        // player steps in teleporter, broadcast quiz
        if (player != null && !quiz_open)
        {
            quiz_open = true;
            Messenger<string>.Broadcast(GameEvent.START_QUIZ, teleport_id);
            Messenger.Broadcast(GameEvent.PAUSE_GAME);
            Debug.Log("Quiz open");
        }
        /*
        if (player != null)
        {
            teleportPlayer();
        }
        */
    }

    void teleportPlayer(string id)
    {
        if (player != null)
        {
            Debug.Log("TeleportID=" + teleport_id);

            switch (id)
            {
                case ("Teleporter_0_1"):        // safety zone to stage 1
                    player.setMapLocation(1);
                    player.respawnPlayer(1);
                    break;
                case ("Teleporter_0_2"):        // safety zone to stage 2
                    player.setMapLocation(2);
                    player.respawnPlayer(2);
                    break;
                default:                        // to safety zone
                    player.setMapLocation(0);
                    player.respawnPlayer(0);
                    break;
            }
        }
        else
            Debug.Log("in teleport player, player is null");
        quiz_open = false;
        Debug.Log("Quiz close");
        Messenger.Broadcast(GameEvent.CHANGE_BGM);
    }

}
