using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [Range(0f, 2f)]
    public float Intensity;

    Transform target;
    Vector3 initPos;


    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<Transform>();
        initPos = target.localPosition;
    }

    float pendingShakeDuration = 0f;

    public void shake(float duration)
    {
        if (duration > 0)
        {
            pendingShakeDuration += duration;
        }
    }

    public bool isShaking = false;

    void Update()
    {
        if (pendingShakeDuration > 0 && !isShaking)
        {
            StartCoroutine(DoShake());
        }


    }

    IEnumerator DoShake()
    {
        isShaking = true;

        var startTime = Time.realtimeSinceStartup;
        while(Time.realtimeSinceStartup < startTime + pendingShakeDuration)
        {
            var randomPoint = new Vector3(initPos.x + Random.Range(-1f, 1f) * Intensity, initPos.y + Random.Range(-1f, 1f) * Intensity, initPos.z);
            target.localPosition = randomPoint;
            yield return null;
        }

        pendingShakeDuration = 0f;
        target.localPosition = initPos;
        isShaking = false;
    }
}
