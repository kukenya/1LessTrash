using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleBin : MonoBehaviour
{
    public enum RecycleBinType
    {
        Paper,
        Can,
        GlassBottle,
        Plastic,
        Vinyl,
        Styrofoam,
        NormalTrash
    }

    public RecycleBinType recycleBinType = RecycleBinType.Paper;

    private void Update()
    {
        
    }
}
