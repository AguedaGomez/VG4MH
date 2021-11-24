using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : DecisionTreeNode
{
    public bool activated = false;

    public override DecisionTreeNode MakeDecision()
    {
        return this;
    }
    public virtual void LateUpdate() 
    {
        if (!activated) return;
        //Implement behaviour
    }
}
