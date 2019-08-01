using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// credit goes to user on StackExchange
public class AITree
{
    private float playerDistanceFromEnemy;
    public int playerPower;

    private ActionNode IsInAttackRange;
    private ActionNode IsVisible;
    private ActionNode EstimatePlayerPower;
    private Sequence Attack;
    private Inverter Patrol;
    private Sequence Escape;
    private Selector Root;

    bool PlayerIsInAttackRange() => playerDistanceFromEnemy < 5;
    bool PlayerIsVisible() => playerDistanceFromEnemy < 8;
    bool PlayerIsTooPowerful() => playerPower > 3;

    public AITree()
    {
        Random rnd = new Random();
        // calculate the distance from enemy
        //playerDistanceFromEnemy = 

        // setup actions
        IsInAttackRange = new ActionNode(PlayerIsInAttackRange);
        IsVisible = new ActionNode(PlayerIsVisible);
        EstimatePlayerPower = new ActionNode(PlayerIsTooPowerful);
        Attack = new Sequence(new List<TreeNode> { IsInAttackRange, IsVisible });
        Patrol = new Inverter(Attack);
        Escape = new Sequence(new List<TreeNode> { IsVisible, EstimatePlayerPower });
        Root = new Selector(new List<TreeNode> { Escape, Patrol, Attack });

        // Execute
        Root.Evaluate();
        ShowCommunicats();
    }

    private void ShowCommunicats()
    {
        // Write to the console decisions made by nodes
        
    }

}

public interface TreeNode
{
    bool nodeState { get; }
    bool Evaluate();
}


// Returns true when one of children return true
public class Selector : TreeNode
{
    private List<TreeNode> childNodes;
    public bool nodeState { get; private set; } = false;

    public Selector(List<TreeNode> childNodes) { this.childNodes = childNodes; }

    public bool Evaluate()
    {
        foreach (TreeNode node in childNodes)
            if(node.Evaluate())
            {
                nodeState = true;
                return true;
            }
        nodeState = false;
        return false;
    }

}

// Return true when ALL children return true
public class Sequence : TreeNode
{
    private List<TreeNode> childNodes;
    public bool nodeState { get; private set; } = false;

    public Sequence(List<TreeNode> childNodes) { this.childNodes = childNodes; }

    public bool Evaluate()
    {
        foreach(TreeNode node in childNodes)
            if(!node.Evaluate())
            {
                nodeState = false;
                return false;
            }
        nodeState = true;
        return true;
    }
}

// If has only one child, negate it
public class Inverter : TreeNode
{
    private TreeNode nodeToInvert;
    public bool nodeState { get; private set; } = false;

    public Inverter(TreeNode nodeToInvert) { this.nodeToInvert = nodeToInvert; }

    public bool Evaluate()
    {
        nodeState = !nodeToInvert.Evaluate();
        return !nodeToInvert.Evaluate();
    }
}

// Leaf of tree, returns delegate of bool function that is in constructor
public class ActionNode : TreeNode
{
    public delegate bool ActionNodeDelegate();
    private ActionNodeDelegate action;
    public bool nodeState { get; private set; } = false;

    public ActionNode(ActionNodeDelegate action)
    {
        this.action = action;
    }

    public bool Evaluate()
    {
        nodeState = action();
        return action();
    }
}

//public static class DecisionTree
//{
//    public static void Test()
//    {
//        if(true)
//        {
//            AITree tree = new AITree();
//            // Input?
//            Console.ReadKey();
//            Console.Clear();
//        }
//    }
//}
