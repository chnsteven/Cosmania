using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCamera : MonoBehaviour
{
    private float delay = .2f;
    private void OnEnable()
    {
        transform.rotation = Quaternion.identity;
        StartCoroutine(LookAtCamera());
    }
    private IEnumerator LookAtCamera() {
        while (true) {
            transform.LookAt(Camera.main.transform);
            yield return new WaitForSeconds(delay);
        }
        
    }
}
