using UnityEngine;

public class Background : MonoBehaviour
{

    [SerializeField] float speed = 0.2f;

    MeshRenderer backgroundMeshRenderer;

    private void Awake()
    {
        backgroundMeshRenderer = GetComponent<MeshRenderer>();
    }
    void Update()
    {
        OffSetBackground();
    }

    void OffSetBackground()
    {
        backgroundMeshRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
        backgroundMeshRenderer.material.mainTextureOffset = new Vector2(Mathf.Repeat(backgroundMeshRenderer.material.mainTextureOffset.x, 1), 0);
    }

}
