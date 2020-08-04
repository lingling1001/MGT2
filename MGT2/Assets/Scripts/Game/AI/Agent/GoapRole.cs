using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoapRole : IGoap
{

    private GAData _goapAgentData;
    public HashSet<KeyValuePair<string, object>> createGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();
        goal.Add(GANameHelper.CreateKeyValue(GANameHelper.ENTITY_DES, true));
        return goal;
    }

    public HashSet<KeyValuePair<string, object>> getWorldState()
    {
        HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();
        worldData.Add(GANameHelper.CreateKeyValue(GANameHelper.ROLE_PATROL, GetValue(EnumGADType.Patrol)));
        worldData.Add(GANameHelper.CreateKeyValue(GANameHelper.ROLE_ATTACK, GetValue(EnumGADType.Patrol)));
        return worldData;
    }

    public bool moveAgent(GoapAction nextAction)
    {
        //Log.Info("  move Agent  ");
        return true;
    }

    public void actionsFinished()
    {

        //Log.Debug("<color=blue>Actions completed</color>");
    }

    public void planAborted(GoapAction aborter)
    {

        Log.Debug("<color=red>Plan Aborted</color> " + GoapAgent.prettyPrint(aborter));
    }

    public void planFailed(HashSet<KeyValuePair<string, object>> failedGoal)
    {
        Log.Debug("<color=red>Plan failed</color> " + GoapAgent.prettyPrint(failedGoal));

    }

    public void planFound(HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> actions)
    {
        //Log.Debug("<color=green>Plan found</color> " + GoapAgent.prettyPrint(actions));
    }

    public void InitialRoleAction(AssemblyRole role)
    {
        GAData goapData = new GAData();



        GARolePatrol gARolePatrol = FactoryGAction.Create<GARolePatrol>();//搜寻敌人
        gARolePatrol.addPrecondition(GANameHelper.ROLE_PATROL, true);
        gARolePatrol.addEffect(GANameHelper.ENTITY_DES, true);

        GARoleAttack gARoleAttack = FactoryGAction.Create<GARoleAttack>(); //攻击
        gARoleAttack.addPrecondition(GANameHelper.ROLE_ATTACK, true);
        gARoleAttack.addEffect(GANameHelper.ENTITY_DES, true);


        goapData.AddData(FactoryGoapActionData.CreateGADRolePatrol(true));
        goapData.AddData(FactoryGoapActionData.CreateSelfEntity(role));

        _goapAgentData = goapData;
        GoapAction[] gas = new GoapAction[] { gARolePatrol, gARoleAttack };

        role.AssyGoapAgent.Value.OnInit(goapData, this, gas);
    }


    private bool GetValue(EnumGADType type)
    {
        return _goapAgentData.GetValue(type);
    }


}
