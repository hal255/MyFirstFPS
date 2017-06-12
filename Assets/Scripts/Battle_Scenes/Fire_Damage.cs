using UnityEngine;
using System.Collections;

public class Fire_Damage : MonoBehaviour {

    private bool inFire = false;        // determines if player is in fire

    void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();

        // player steps in fire, call function to damage player
        if (player != null)
        {
            inFire = true;
            Debug.Log("You touched fire!!");
            StartCoroutine(DamagePlayer());
        }
    }

    // player exits fire
    void OnTriggerExit()
    {
        // player steps out of fire, player no longer takes damage
        inFire = false;
    }

    private IEnumerator DamagePlayer()
    {
        // while player stays in fire, continue damaging player
        while (inFire)
        {
            Debug.Log("Get out of the fire!!");
            Managers.Player.ChangeHealth(-1);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
