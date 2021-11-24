using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decision: DecisionTreeNode
{
    ActionNode trueNode;
    ActionNode falseNode;

    public virtual ActionNode getBranch() 
    {
        return null;
    }
    public override DecisionTreeNode MakeDecision()
    {
        var branch = getBranch();
        return branch.MakeDecision();
    }
}
