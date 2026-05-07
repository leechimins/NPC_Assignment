using UnityEngine;

public class Sometime_Turn : MonoBehaviour
{
    public float angle = 90;
    public int maxCount = 50;

    int count = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        count = 0;

    }

    void FixedUpdate() {
        count++;
        if (count >= maxCount) {
            this.transform.Rotate(0, 0, angle);
            count = 0;
        }
    }
}
