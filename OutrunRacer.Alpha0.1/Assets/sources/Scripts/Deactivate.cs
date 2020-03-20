using UnityEngine;

public class Deactivate : MonoBehaviour {

    bool deactiveScheduled = false;

    private void OnCollisionExit(Collision collision) {

        if (collision.gameObject.tag == "Player" && !deactiveScheduled) {

            Invoke("SetInactive", 4.0f);
            deactiveScheduled = true;
        }
    }

    void SetInactive() {

        deactiveScheduled = false;
        this.gameObject.SetActive(false);
    }
}
