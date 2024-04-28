using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPaint : MonoBehaviour
{
    private new SpriteRenderer renderer;
    [SerializeField] private Camera cam1;
    [SerializeField] private Camera cam2;
    [SerializeField] private Sprite paint1;
    [SerializeField] private Sprite paint2;
    private Coroutine updateSprite;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        PlayerInvestigationState.PlayerInInvestigationState += InInvestigation;
    }

    private void OnDisable()
    {
        PlayerInvestigationState.PlayerInInvestigationState -= InInvestigation;
    }

    private void InInvestigation()
    {
        if (updateSprite == null) updateSprite = StartCoroutine(UpdateSprite());
    }
    private IEnumerator UpdateSprite()
    {
        //Debug.Log("UpdateSprite begins");
        if (cam1 == null && cam2 == null)
        {
            Debug.LogWarning("Should have at least 1 camera activated");
            StopCoroutine(UpdateSprite());
        }
        Camera cam;
        if (cam1 == null)
        {
            cam = cam1;
        }
        else
        {
            cam = cam2;
        }
        Vector3 uv = cam.WorldToViewportPoint(transform.position);
        if (ObjectIsVisible(uv))
        {
            renderer.sprite = paint1;
        }
        else
        {
            renderer.sprite = paint2;
        }
        yield return new WaitForSeconds(0.5f);
        updateSprite = null;
    }
    private bool ObjectIsVisible(Vector3 vector)
    {
        float min = 0.05f;
        float max = 0.95f;
        return (vector.x >= min && vector.x <= max) &&
            (vector.y >= min && vector.y <= max) &&
            vector.z > 0;
    }
}
