using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerManager : MonoBehaviour {

    public static SinglePlayerManager instance;
    public Joystick joystick;

    private int maxHealth = 100;
    private int currentHealth = 100;
    private float playerSpeed = 8.0f;
    private float bulletSpeed = 5.0f;

    //public GameObject bulletPlayer;
    //public GameObject explosionPlayer;

    private Rigidbody playerRigidbody;
    private Rigidbody bulletPlayerRigidbody;
    private Rigidbody explosionPlayerRigidbody;


    private float moveHorz;
    private float moveVert;

    

    private void Awake()
    {
        if (instance != null && instance !=this) {
            Destroy(this.gameObject);
        }
        else {

            instance = this;
        }
    }


    // Use this for initialization
    void Start () {

        Initialize();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Movement();
        //Fire();
  
	}

    public void Initialize()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        //bulletPlayerRigidbody = bulletPlayer.GetComponent<Rigidbody>();
        //explosionPlayerRigidbody = explosionPlayer.GetComponent<Rigidbody>();
    }

    public void Movement() {
#if UNITY_STANDALONE

        #region LOOKAT_MOUSE
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        //generate a ray from cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDistance = 0.0f;
        //if the ray is paralel to the plane, raycast will return false
        if (playerPlane.Raycast(ray, out hitDistance)) {
            //get a point along the ray that hits the calculated distance
            Vector3 targetpoint = ray.GetPoint(hitDistance);
            //determine the target rotation.  this is the rotation if the transform looks at the target point
            Quaternion targetrotation = Quaternion.LookRotation(targetpoint - transform.position);
            //smoothly rotate towards the target point
            transform.rotation = Quaternion.Lerp(transform.rotation, targetrotation, playerSpeed * Time.deltaTime);}
        #endregion

        moveHorz = Input.GetAxis("Horizontal") * playerSpeed;
        moveVert = Input.GetAxis("Vertical") * playerSpeed / 2;
        Vector3 movement = new Vector3(moveHorz, 0, moveVert);
        playerRigidbody.velocity = movement;

#elif UNITY_ANDROID

        Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);

        if (moveVector != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveVector);
            transform.Translate(moveVector * playerSpeed * Time.deltaTime, Space.World);
        }   

#endif
    }

    public void Fire(GameObject bulletPrefab , Transform bulletSpawn ) {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation) as GameObject;

        bulletPlayerRigidbody = bullet.GetComponent<Rigidbody>();
        bulletPlayerRigidbody.velocity = bullet.transform.forward * bulletSpeed;

        Destroy(bulletPlayerRigidbody, 3.0f);

        





    }


}
