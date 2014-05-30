using UnityEngine;
using System.Collections;

public class Trajectory : MonoBehaviour {

	float force;
	public GameObject spriteSnowball;
	GameObject snowballTemp;
	public GameObject colliderSnowball;
	GameObject[] colliderSnowballTemp = new GameObject[3];

	void Start (){
		NewSnowball ();
	}

	void Update (){
		snowballTemp.transform.position = transform.position;
		if(Input.GetMouseButton(0)){
			Vector3 target = new Vector3(Input.mousePosition.x - 455, Input.mousePosition.y - 77.2f, 0);
			Vector3 norTar = (target-transform.position).normalized;
			float angle = Mathf.Atan2(-norTar.x,-norTar.y)*Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);		

			Vector3 distance = new Vector3(Input.mousePosition.x -455, Input.mousePosition.y - 77.2f, 0) - transform.position;
			float teste = Vector3.Distance(distance, transform.position);
			force = teste * 10;
		}

		if(Input.GetMouseButtonUp(0)){
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
			rigidbody2D.isKinematic = false;
			rigidbody2D.AddForce(transform.up * force);
			Debug.Log (force);
			force = 250;
		}
	}	

	void OnTriggerEnter2D(Collider2D other) {
		switch (other.tag){
			case "Sphere1":
				ActiveColliders ();
				NewSnowball ();
			break;
			case "Sphere2":			
				ActiveColliders ();
				NewSnowball ();
			break;
			case "Sphere3":
				ActiveColliders ();
				NewSnowball ();
			break;
		}
	
		if(other.CompareTag ("Area")){
			ActiveColliders ();
			NewSnowball ();
			other.enabled = false;
		}
		 
		rigidbody2D.isKinematic = true;
		snowballTemp.transform.localPosition = new Vector3(5, -3, 0);
		transform.position = new Vector3(5, -3, 0);
		gameObject.GetComponent<SpriteRenderer>().enabled = true;
	}

	void NewSnowball (){
		snowballTemp = Instantiate(spriteSnowball) as GameObject;
		snowballTemp.transform.localPosition = new Vector3(5, -3, 0);

		for(int x = 0;x < 3;x++){
			colliderSnowballTemp[x] = Instantiate(colliderSnowball) as GameObject;
			colliderSnowballTemp[x].transform.parent = snowballTemp.transform;				
		}
			
		colliderSnowballTemp[0].transform.localPosition = new Vector3(0.9809444f, 1.052599f, 0);
		colliderSnowballTemp[1].transform.localPosition = new Vector3(0.0007137237f, 1.343605f, 0);
		colliderSnowballTemp[2].transform.localPosition = new Vector3(-0.9798661f, 0.9082487f, 0);
	}

	void ActiveColliders (){
		colliderSnowballTemp[0].collider2D.enabled = true;
		colliderSnowballTemp[1].collider2D.enabled = true;
		colliderSnowballTemp[2].collider2D.enabled = true;
		snowballTemp.collider2D.enabled = true;
	}	
}