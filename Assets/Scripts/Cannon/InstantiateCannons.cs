using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCannons : MonoBehaviour
{
    [SerializeField] private GameObject platforms;
    private Transform[] platformTransforms;
    [SerializeField] private GameObject cannonPrefab;
    private Vector3 offset = new Vector3(0f, -0.4f, 0f);
    // Start is called before the first frame update
    void OnEnable()
    {
        if (platforms == null) return;
        if (cannonPrefab == null) return;

        StartCoroutine(InstantiateCannon());

    }

    IEnumerator InstantiateCannon()
    {
        yield return new WaitForSeconds(1f);
        
        //Debug.Log(platforms.transform.childCount);
        platformTransforms = new Transform[platforms.transform.childCount];
        for (int i = 0; i < platforms.transform.childCount; i++)
        {
            platformTransforms[i] = platforms.transform.GetChild(i);
        }
        if (platformTransforms.Length == 0) StopCoroutine(InstantiateCannon());
        
        foreach (Transform platformTransform in platformTransforms)
        {
            Instantiate(cannonPrefab, platformTransform.transform.position + offset, platformTransform.transform.rotation);
        }

    }

}
