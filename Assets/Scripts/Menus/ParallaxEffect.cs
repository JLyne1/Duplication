using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float startingPos;
    private float lengthOfSprite;
    [SerializeField] private float parallaxSpeed;
    [SerializeField] private Camera mainCamera;

    private void Start() 
    {
        startingPos = transform.position.x;
        lengthOfSprite = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        ParallaxScroll();
    }

    private void ParallaxScroll()
    {
        Vector3 currentPos = mainCamera.transform.position;
        float temp = currentPos.x * (1 - parallaxSpeed);
        float distance = currentPos.x * parallaxSpeed;

        Vector3 newPos = new(startingPos + distance, transform.position.y, transform.position.z);

        transform.position = newPos;

        if (temp > startingPos + (lengthOfSprite / 2))
            {
                startingPos += lengthOfSprite;
            }
        else if (temp < startingPos - (lengthOfSprite / 2))
            {
                startingPos -= lengthOfSprite;
            }
    }

}
