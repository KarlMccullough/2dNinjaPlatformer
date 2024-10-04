using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallexing : MonoBehaviour {

    public Transform[] backgrounds;
    private float[] parallaxScales;
    public float smoothing;

    private Vector3 previousCameraposition;

	// Use this for initialization
	void Start () {
        previousCameraposition = transform.position;

        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < parallaxScales.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
	}
	
	// Update is called once per frame
	void LateUpdate () {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            Vector3 parallax = (previousCameraposition - transform.position) * (parallaxScales[i] / smoothing);

            backgrounds[i].position = new Vector3(backgrounds[i].position.x + parallax.x, backgrounds[i].position.y + parallax.y, backgrounds[i].position.z);
        }

        previousCameraposition = transform.position;

		
	}
}
