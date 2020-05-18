using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustDestroy : MonoBehaviour {

    [SerializeField]
    private float stayTime = 1f;

    void OnEnable() {
        StartCoroutine("WaitToDestroy");
    }

    IEnumerator WaitToDestroy() {
        yield return new WaitForSeconds(stayTime);
        Destroy(gameObject);
    }
}
