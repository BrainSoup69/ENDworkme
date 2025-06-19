using UnityEngine;

public class AnimateUV : MonoBehaviour
{
    [SerializeField] Material faceMaterial;
    [SerializeField] string textureNameInShader = "_BaseMap";

    // Update is called once per frame
    public void UpdateU(float u)
    {
        Vector2 uv = faceMaterial.GetTextureOffset(textureNameInShader);
        faceMaterial.SetTextureOffset(textureNameInShader, new Vector2(u, uv.y));
    }
    // Update is called once per frame
    public void UpdateV(float v)
    {
        Vector2 uv = faceMaterial.GetTextureOffset(textureNameInShader);
        faceMaterial.SetTextureOffset(textureNameInShader, new Vector2(uv.x, v));
    }
}