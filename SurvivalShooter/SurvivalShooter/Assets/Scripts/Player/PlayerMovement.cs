using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;

	private Vector3 movement;
	private Animator anim;
	private Rigidbody playerRigidbody;
	private int floorMask;
	private float camRayLength = 100f;
	public int playerInput = 0;
	float playerRot = 0;

	void Awake()
	{
		floorMask = LayerMask.GetMask("Floor");
		anim = GetComponent<Animator>();
		playerRigidbody = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		float h = Input.GetAxisRaw("Horizontal" + playerInput);
		float v = Input.GetAxisRaw("Vertical" + playerInput);

		Move(h, v);
		//Turning();
		Animating(h, v);
	}
	
	void Move(float h, float v) //new move
	{
		movement.Set(h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;
		

		playerRigidbody.MovePosition(transform.position + movement);

		
		//new turning vv
		int changeAngle = 0;

		//if (h > 0 && (transform.rotation.y * 180) - 90 > 5) changeAngle +=1;
		//if (h < 0 && (transform.rotation.y * 180) + 90 > 5) changeAngle +=1;
		//unused

		if (h != 0) changeAngle += 1;
		if (v != 0) changeAngle += 1;



		if (changeAngle==1)
		{
			playerRot += h * 2.5f;
			playerRigidbody.MoveRotation(Quaternion.AngleAxis(playerRot, Vector3.up));
		}
	}

	/* OLD MOVE/TURNING
	void Move(float h, float v)
	{
		movement.Set(h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;

		playerRigidbody.MovePosition(transform.position + movement);
	}

	void Turning()
	{
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit floorHit;

		if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			playerRigidbody.MoveRotation(newRotation);
		}

		
		if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			playerRigidbody.MoveRotation(newRotation);
		}
		
	}
	*/

	void Animating(float h, float v)
	{
		bool walking = h != 0f || v != 0f;

		anim.SetBool("IsWalking", walking);
	}
}
