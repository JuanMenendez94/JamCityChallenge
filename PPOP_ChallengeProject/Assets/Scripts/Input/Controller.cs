using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Controller : MonoBehaviour
{
    private Camera _camera;
    private int tileHitLayerMask = 1 << Utility.Constants.Layers.Tile;
    public float rayDistance;

    private void Start()
    {
        _camera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        //On mouse click search for a tile to select.
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit _rh;
            if (Physics.Raycast(ray, out _rh, rayDistance, tileHitLayerMask))
            {
                IClickable hit = _rh.transform.GetComponent<IClickable>();
                if (hit != null)
                {
                    hit.OnClick();
                }
            }
        }
    }
}
