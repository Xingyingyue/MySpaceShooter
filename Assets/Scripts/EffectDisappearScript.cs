using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDisappearScript : MonoBehaviour {
    private float disappearTime = 2.0f;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(disappearTime);
        Destroy(gameObject);
    }
}
