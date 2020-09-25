using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

   public void UpdateResource(Card.Resource resource, int valueModifier)
    {
        PlayerData.resources[resource] += valueModifier;
        print(PlayerData.resources[resource]);
    }
}
