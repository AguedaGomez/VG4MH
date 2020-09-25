using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    public const int INIT_VALUE = 50;

    public static Dictionary<Card.Resource, int> resources = new Dictionary<Card.Resource, int>
    {
        { Card.Resource.MOTIVATION, INIT_VALUE},
        { Card.Resource.FLEXIBILITY, INIT_VALUE},
        { Card.Resource.ACTIVATION, INIT_VALUE},
        { Card.Resource.POSITIVE, INIT_VALUE}

    };

}

