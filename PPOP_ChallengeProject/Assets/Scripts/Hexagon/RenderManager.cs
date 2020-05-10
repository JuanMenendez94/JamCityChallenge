using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderManager : MonoBehaviour, ITintable
{
    private Renderer _renderer;

    void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Tint(Color color)//tints the tile
    {
        _renderer.material.SetColor(Utility.Constants.MaterialReferences.AlbedoColor, color);
    }

    public void SetTexture(Texture text)
    {
        _renderer.material.SetTexture(Utility.Constants.MaterialReferences.MainTexture, text);
    }
}
