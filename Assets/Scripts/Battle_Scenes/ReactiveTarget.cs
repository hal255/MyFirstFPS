using UnityEngine;
using System.Collections;

public class ReactiveTarget : MonoBehaviour {

    public float kill_time = 0.05f;

	public void ReactToHit() {
        WanderingAI behavior = GetComponent<WanderingAI>();
        if (behavior != null)
        {
            behavior.SetAlive(false);
        }
        StartCoroutine(Die());
    }

    private IEnumerator Die() {
        float rotate_value = -3f;

        // object dies in 1.5 seconds (15 loops)
        for (int x = 0; x > -75; x -= 5)
        {
            this.transform.Rotate(rotate_value, 0, 0);
            yield return new WaitForSeconds(kill_time);
        }

        Destroy(this.gameObject);
    }
}
