using UnityEngine;

public class PlayerController : MonoBehaviour {
    Animator anim;
    public static GameObject player;
    public static GameObject currentPlatform;
    bool canTurn = false;
    Vector3 startPosition;

    void OnCollisionEnter(Collision other) {
        currentPlatform = other.gameObject;
    }

    // Start is called before the first frame update
    void Start() {
        anim = this.GetComponent<Animator>();
        player = this.gameObject;
        startPosition = player.transform.position;
        GenerateWorld.RunDummy();
    }

    void OnTriggerEnter(Collider other) {
        if (other is BoxCollider && GenerateWorld.lastPlatform.tag != "platformTSection")
            GenerateWorld.RunDummy();

        if (other is SphereCollider)
            canTurn = true;
    }

    void OnTriggerExit(Collider other) {
        if (other is SphereCollider)
            canTurn = false;
    }

    void StopJump() {
        anim.SetBool("isJumping", false);
    }

    void StopMagic() {
        anim.SetBool("isMagic", false);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && anim.GetBool("isMagic") == false) {
            anim.SetBool("isJumping", true);
        } else if (Input.GetKeyDown(KeyCode.M) && anim.GetBool("isJumping") == false) {
            anim.SetBool("isMagic", true);
        } else if (Input.GetKeyDown(KeyCode.RightArrow) && canTurn) {
            this.transform.Rotate(Vector3.up * 90);
            GenerateWorld.dummyTraveller.transform.forward = -this.transform.forward;
            GenerateWorld.RunDummy();

            if (GenerateWorld.lastPlatform.tag != "platformTSection")
                GenerateWorld.RunDummy();

            this.transform.position = new Vector3(startPosition.x,
                                            this.transform.position.y,
                                            startPosition.z);
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) && canTurn) {
            this.transform.Rotate(Vector3.up * -90);
            GenerateWorld.dummyTraveller.transform.forward = -this.transform.forward;
            GenerateWorld.RunDummy();

            if (GenerateWorld.lastPlatform.tag != "platformTSection")
                GenerateWorld.RunDummy();

            this.transform.position = new Vector3(startPosition.x,
                                this.transform.position.y,
                                startPosition.z);
        } else if (Input.GetKeyDown(KeyCode.A)) {
            this.transform.Translate(-0.5f, 0, 0);
        } else if (Input.GetKeyDown(KeyCode.D)) {
            this.transform.Translate(0.5f, 0, 0);
        }
    }
}
