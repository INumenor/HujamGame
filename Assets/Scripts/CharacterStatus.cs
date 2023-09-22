using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    static public int GrassNumbers = 1, DirtNumbers = 1, MineNumbers = 1, StoneNumbers = 1, MicMineNumbers = 1, MicDirtNumbers = 1, MicStoneNumbers = 1;
    static public bool dirt_grass = false, dirt = false, gravel_stone = false, stone = false;

    public int _GrassNumber
    {
        get
        {
            return GrassNumbers;
        }
        set
        {
            GrassNumbers += 1;
        }
    }
    public int _DirtNumber
    {
        get
        {
            return DirtNumbers;
        }
        set
        {
            DirtNumbers += 1;
        }
    }
    public int _MineNumber
    {
        get
        {
            return MineNumbers;
        }
        set
        {
            MineNumbers += 1;
        }
    }public int _StoneNumbers
    {
        get
        {
            return StoneNumbers;
        }
        set
        {
            StoneNumbers += 1;
        }
    }

}
