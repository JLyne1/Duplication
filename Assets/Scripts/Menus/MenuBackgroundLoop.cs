using UnityEngine;

public class MenuBackgroundLoop : MonoBehaviour
{
   [SerializeField] Vector2 moveSpeed;
   [SerializeField] Material material;

    Vector2 offset;

    private void Update() 
    {
        offset = moveSpeed * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
