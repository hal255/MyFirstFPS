using UnityEngine;
using System.Collections;

public class WanderingAI : MonoBehaviour {
    public int _health = 10;
    public int max_health = 10;

    public const float baseSpeed = 6.0f;

    public float speed = 3.0f;
	public float obstacleRange = 5.0f;
    public float rotate_time = 0.5f;
    public int rotate_amount = 45;
    public int damage = 10;
	
	[SerializeField] private GameObject fireballPrefab;
	private GameObject _fireball;
    private PlayerCharacter player;
	
	private bool _alive, _isRotating;
	
	void Start() {
		_alive = true;
        _isRotating = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
        switch (player.getMapLocation())
        {
            case 0:
                max_health *= 1;
                break;
            case 1:
                max_health *= 2;
                break;
            case 2:
                max_health *= 5;
                break;
            default:
                max_health = 10;
                break;
        }
        _health = max_health;
	}

    void Awake()
    {
        Messenger<float>.AddListener(GameEvent.ENEMY_SPEED_CHANGED, OnSpeedChanged);
    }
    void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.ENEMY_SPEED_CHANGED, OnSpeedChanged);
    }


    // this function controls enemy attack motion
    // rotates twice before shooting
    private IEnumerator enemyAttacks(Vector3 byAngles)
    {
        // rotate angle byAngles amount
        Quaternion fromAngle = transform.rotation;
        Quaternion toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (float t = 0f; t < 1; t += Time.deltaTime / rotate_time)
        {
            transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
        }

        // rotate object back to original angle
        fromAngle = Quaternion.Euler(transform.eulerAngles - byAngles);
        for (float t = 0f; t < 1; t += Time.deltaTime / rotate_time)
        {
            transform.rotation = Quaternion.Lerp(toAngle, fromAngle, t);
            yield return null;
        }


        if (_fireball == null)
        {
            _fireball = Instantiate(fireballPrefab) as GameObject;
            _fireball.GetComponent<Fireball>().setDamage(damage);
            //Vector3 temp_position = transform.TransformPoint(Vector3.forward * 1.5f);
            Vector3 temp_position = transform.TransformPoint(Vector3.forward);
            //temp_position.y = player.transform.position.y;
            _fireball.transform.position = temp_position;
            _fireball.transform.rotation = transform.rotation;
        }
        _isRotating = false;
        yield return new WaitForSeconds(0.1f);
    }

    void Update() {

		if (_alive && !_isRotating) {
    		transform.Translate(0, 0, speed * Time.deltaTime);
            Vector3 temp_position = transform.position;
            temp_position.y = player.transform.position.y;
			Ray ray = new Ray(temp_position, transform.forward);
			RaycastHit hit;
			if (Physics.SphereCast(ray, 0.75f, out hit)) {
				GameObject hitObject = hit.transform.gameObject;
				if (hitObject.GetComponent<PlayerCharacter>()) {

                    // rotating set to true
                    _isRotating = true;

				}
				else if (hit.distance < obstacleRange) {
					float angle = Random.Range(-110, 110);
					transform.Rotate(0, angle, 0);
				}
			}
		}
        else if (_alive && _isRotating)
        {
//            old_angles = transform.rotation;
            StartCoroutine(enemyAttacks(Vector3.up * rotate_amount));
        }
    }

    public void SetAlive(bool alive)
    {
        _alive = alive;
    }

    public bool getAlive()
    {
        return _alive;
    }

    public void Hurt(int damage)
    {
        Debug.Log(gameObject + " is hit. HP = " + _health);
        _health -= damage;
        if (_health <= 0)
        {
            Debug.Log(name + " HP is 0");
            SetAlive(false);
            gameObject.GetComponent<Animator>().Stop();                 // stop animation
            gameObject.GetComponent<ReactiveTarget>().ReactToHit();     // kill off object
            Messenger.Broadcast(GameEvent.ENEMY_DEAD);                  // broadcast enemy is dead
        }
    }
    public int getHealth()
    {
        return _health;
    }

    /*
    public void setHealth(int hp)
    {
        _health = hp;
    }
    
    public void setMaxHealth(int hp)
    {
        max_health = hp;
    }
    */
    public int getMaxHealth()
    {
        return max_health;
    }
    
    private void OnSpeedChanged(float value)
    {
        speed = baseSpeed * value;
    }
}
