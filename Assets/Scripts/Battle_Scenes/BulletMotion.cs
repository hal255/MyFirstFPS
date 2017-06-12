using UnityEngine;
using System.Collections;

public class BulletMotion : MonoBehaviour {

    public void RayDirection(Vector3 start_pos, Vector3 end_pos)
    {
        StartCoroutine(MoveOut());
    }

    private IEnumerator MoveOut()
    {
        float rotate_value = -2;

        // object dies in 1.5 seconds (15 loops)
        for (int x = 0; x > -75; x -= 5)
        {
            this.transform.Rotate(rotate_value, 0, 0);
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(this.gameObject);
    }
}
