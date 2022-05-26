using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    private const int MAXCUANTITY = 100;
    private const int MINCUANTITY = 0;


   public void UpdateResource(Card.Resource resource, int valueModifier)
    {
        if (PlayerData.resources.ContainsKey(resource)) // NONE y BOTH cases
        {
            if (Mathf.Sign(valueModifier) == 1 && PlayerData.resources[resource] < MAXCUANTITY ||
            Mathf.Sign(valueModifier) == -1 && PlayerData.resources[resource] > MINCUANTITY)
            {
                PlayerData.resources[resource] += valueModifier;
                //print(PlayerData.resources[resource]);
            }
        }

    }
}
