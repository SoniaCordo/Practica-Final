using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public const string PLAYER_LAYER_NAME = "Player";


    public static bool IsInLayerMask(GameObject go, LayerMask mask)
    {
        return mask == (mask | (1 << go.layer));
    }



}