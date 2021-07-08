using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AgentNormal : IGoap
{
    private AgentData _goapAgentData;
    public HashSet<KeyValuePair<string, object>> createGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();
        goal.Add(AgentHelper.CreateKeyValue(AgentHelper.ACTION_DESTROY_ENEMY, true));
        return goal;
    }

    public HashSet<KeyValuePair<string, object>> getWorldState()
    {
        HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();
        worldData.Add(CreateKeyValue(AgentHelper.ACTION_MOVE_AUTO));
        worldData.Add(CreateKeyValue(AgentHelper.ACTION_UNIT_MOVE));
        worldData.Add(CreateKeyValue(AgentHelper.ACTION_MOVE_FINISH));
        return worldData;
    }

    public bool moveAgent(GoapAction nextAction)
    {
        return true;
    }

    public void actionsFinished()
    {
        AgentDataEntity entity = _goapAgentData.GetData<AgentDataEntity>(AgentHelper.AD_ENTITY);
        AgentDataPosition move = _goapAgentData.GetData<AgentDataPosition>(AgentHelper.AD_POSITION);
        AgentDataBase value = _goapAgentData.GetData<AgentDataBase>(AgentHelper.AD_MOVE_STATE);
        if (value.State)//移动到中心点了 
        {
            value.SetState(false);
            //返回
            //AssemblyUnitCastle castle = entity.Entity.GetData<AssemblyUnitCastle>();
            //move.SetValue(castle.CastleData.GetData<AssemblyPosition>().Position);
        }
        else//移动到中心点。
        {
            value.SetState(true);
           // move.SetValue(FindPathManager.Instance.GetCenterPos());
        }
        // Log.Info("Actions completed");
    }

    public void planAborted(GoapAction aborter)
    {
        //Log.Info("Plan Aborted " + GoapAgent.prettyPrint(aborter));
    }

    public void planFailed(HashSet<KeyValuePair<string, object>> failedGoal)
    {
        // Log.Info("Plan failed " + GoapAgent.prettyPrint(failedGoal));
    }

    public void planFound(HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> actions)
    {
        // Log.Info(" Plan found " + GoapAgent.prettyPrint(actions));
    }

    public void InitialAction(AssemblyGoapAgent assembly)
    {


        //ActionAutoMove actionAutoMove = new ActionAutoMove();
        //actionAutoMove.addPrecondition(AgentHelper.ACTION_MOVE_AUTO, true);
        //actionAutoMove.addEffect(AgentHelper.ACTION_DESTROY_ENEMY, true);


        ActionUnitMove actionMove = new ActionUnitMove();
        actionMove.addPrecondition(AgentHelper.ACTION_UNIT_MOVE, true);
        actionMove.addEffect(AgentHelper.ACTION_DESTROY_ENEMY, true);


        _goapAgentData = new AgentData();
        //默认移动状态
        _goapAgentData.AddData(AgentHelper.CreateData(AgentHelper.ACTION_MOVE_AUTO, true));
        _goapAgentData.AddData(AgentHelper.CreateData(AgentHelper.ACTION_UNIT_MOVE, true));
        _goapAgentData.AddData(AgentHelper.CreateData(assembly.Owner));
        _goapAgentData.AddData(AgentHelper.CreateData(Vector3.zero));
        _goapAgentData.AddData(AgentHelper.CreateData(AgentHelper.AD_MOVE_STATE, true));


        GoapAction[] gas = new GoapAction[] { actionMove };
        GoapAgent agent = new GoapAgent();
        agent.OnInit(_goapAgentData, this, gas);
        assembly.SetValue(agent, this);


    }

    private KeyValuePair<string, object> CreateKeyValue(string key)
    {
        return AgentHelper.CreateKeyValue(key, _goapAgentData.Contain(key));
    }


}
