using System.Collections.Generic;
using UnityEngine;

public class AgentCastleUnitDefend : IGoap
{
    private AgentData _agentData;
    private AgentDataPatrol _dataPatrol;
    private AgentDataBase _dataAtk;
    private AssemblyCache _assemblyCache;
    private AssemblyCache _assemblyCacheCastle;

    public HashSet<KeyValuePair<string, object>> createGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();
        goal.Add(AgentHelper.CreateKeyValue(AgentHelper.ACTION_DESTROY_ENEMY, true));
        return goal;
    }

    public HashSet<KeyValuePair<string, object>> getWorldState()
    {
        HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();
        worldData.Add(AgentHelper.CreateKeyValue(AgentHelper.ACTION_PATROL, _agentData));
        worldData.Add(AgentHelper.CreateKeyValue(AgentHelper.ACTION_UNIT_MOVE, _agentData));
        worldData.Add(AgentHelper.CreateKeyValue(AgentHelper.ACTION_ATTACK_NORMAL, _agentData));
        return worldData;
    }

    public void InitialAction(AssemblyGoapAgent assembly, EntityAssembly castle)
    {

        ActionPatrol patrol = new ActionPatrol();
        patrol.addPrecondition(AgentHelper.ACTION_PATROL, true);
        patrol.addEffect(AgentHelper.ACTION_DESTROY_ENEMY, true);

        ActionNormalAttack attack = new ActionNormalAttack();
        attack.addPrecondition(AgentHelper.ACTION_ATTACK_NORMAL, true);
        attack.addEffect(AgentHelper.ACTION_DESTROY_ENEMY, true);


        _assemblyCache = assembly.Owner.GetData<AssemblyCache>();
        _assemblyCacheCastle = castle.GetData<AssemblyCache>();
        //默认进入状态巡逻Action
        _dataPatrol = AgentHelper.CreateData<AgentDataPatrol>(AgentHelper.ACTION_PATROL, true);
        _dataPatrol.SetPatrol(RandomPatrolIndex(_assemblyCacheCastle));
        _dataPatrol.SetState(true);


        _dataAtk = AgentHelper.CreateData(AgentHelper.ACTION_ATTACK_NORMAL, false);

        _agentData = new AgentData();

        _agentData.AddData(AgentHelper.CreateData(assembly.Owner));
        _agentData.AddData(_dataPatrol);
        _agentData.AddData(_dataAtk);


        GoapAction[] gas = new GoapAction[] { patrol, attack };
        GoapAgent agent = new GoapAgent();
        agent.OnInit(_agentData, this, gas);
        assembly.SetValue(agent, this);

    }
    public bool moveAgent(GoapAction nextAction)
    {

        return true;
    }
    public void actionsFinished()
    {
        if (_dataPatrol.State)//巡逻找到目标
        {
            if (_assemblyCache.AssyEyeSensor.GetTarget() != null)//找到目标
            {
                _dataPatrol.SetState(false);
                _dataAtk.SetState(true);
            }
            else
            {
                _dataPatrol.SetPatrol(RandomPatrolIndex(_assemblyCacheCastle));
            }
        }
        if (_dataAtk.State)//攻击完成
        {
            if (_assemblyCache.AssyEyeSensor.GetTarget() == null)
            {
                _dataAtk.SetState(false);
                _dataPatrol.SetState(true);//接着巡逻
            }
        }

    }

    public void planAborted(GoapAction aborter)
    {
        Log.Info("Plan Aborted " + GoapAgent.prettyPrint(aborter));
    }

    public void planFailed(HashSet<KeyValuePair<string, object>> failedGoal)
    {
        Log.Info("Plan failed " + GoapAgent.prettyPrint(failedGoal));
    }

    public void planFound(HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> actions)
    {
        Log.Info(" Plan found " + GoapAgent.prettyPrint(actions));
    }

    private Vector3 RandomPatrolIndex(AssemblyCache assemblyCache)
    {
        float rangeX = assemblyCache.Position.x + Random.Range(-5, 5);
        float rangeZ = assemblyCache.Position.z + Random.Range(-5, 5);
        return new Vector3(rangeX, assemblyCache.Position.y, rangeZ);
    }

}
