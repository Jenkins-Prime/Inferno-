using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    [SerializeField]
    private Transform[] backgrounds;
    [SerializeField]
    private float parallaxScale;
    [SerializeField]
    private float parallaxReductionAmount;
    [SerializeField]
    private float parallaxSmoothing;

    private Vector3 lastPosition;



    void Start ()
    {
        lastPosition = transform.position;
	}
	
	void LateUpdate ()
    {
        Parallax();
	}

    private void Parallax()
    {
        float parallaxAmount = (lastPosition.x - transform.position.x) * parallaxScale;

        for (int index = 0; index < backgrounds.Length; index++)
        {
            float parallaxPosition = backgrounds[index].transform.position.x + parallaxAmount * (index * parallaxReductionAmount + 1);
            backgrounds[index].position = Vector3.Lerp(backgrounds[index].position, new Vector3(parallaxPosition, backgrounds[index].position.y, backgrounds[index].position.z), parallaxSmoothing * Time.deltaTime);
        }

        lastPosition = transform.position;
    }
}
