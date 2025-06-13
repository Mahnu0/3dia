using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.LowLevel;

public class Enemy : Entity, ITargeteable
{
    [Header("Configuration")]
    [SerializeField] float shootingDistance = 10f;
    [SerializeField] ITargeteable.Faction faction = ITargeteable.Faction.Enemy;

    [Header("AI")]
    [SerializeField] DecissionTreeNode decissionTreeRoot;

    NavMeshAgent agent;
    WeaponManager weaponManager;
    Orientator orientator;
    Sight sight;

    BaseState[] allStates;

    BaseState currentState;

    DecissionTreeNode[] allDecissionTreeNodes;

    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();

        allStates = GetComponents<BaseState>();
        foreach (BaseState s in allStates)
            { s.Init(this); }

        weaponManager = GetComponentInChildren<WeaponManager>();

        orientator = GetComponent<Orientator>();
        sight = GetComponentInChildren<Sight>();

        allDecissionTreeNodes = GetComponentsInChildren<DecissionTreeNode>();
        foreach (DecissionTreeNode node in allDecissionTreeNodes)
            { node.SetEnemy(this); }
    }

    private void Start()
    {
        decissionTreeRoot.Execute();
    }

    Transform target;
    private void Update()
    {
        target = CheckSenses();
        decissionTreeRoot.Execute();
        UpdateAnimation();
    }

    private Transform CheckSenses()
    {
        ITargeteable targeteable = sight.GetClosestTarget();
        if (targeteable != null)
        {
            hasAlreadyVisitedTheLastTargetPosition = false;
            lastTargetPosition = targeteable.GetTransform().position;
        }

        return (targeteable != null) ? targeteable.GetTransform() : null;
    }

    #region Entity Implementation
    protected override float GetCurrentVerticalSpeed()
    {
        return 0f;
    }

    protected override float GetJumpSpeed()
    {
        return 0f;
    }

    protected override bool IsRunning()
    {
        return false;
    }

    protected override bool IsGrounded()
    {
        return true;
    }

    protected override Vector3 GetLastNormalizedVelocity()
    {
        return agent.velocity.normalized;
    }
    #endregion

    #region AI Getters
    internal NavMeshAgent GetAgent() { return agent; }
    internal Transform GetTarget() { return target; }
    internal WeaponManager GetWeaponManager() { return weaponManager; }
    internal Orientator GetOrientator() { return orientator; }
    #endregion

    #region Decission Tree Implementation
    internal void ChangeStateTo(BaseState newState)
    {
        if (currentState != newState)
        {
            if (currentState != null) { currentState.enabled = false; }
            currentState = newState;
            if (currentState != null) { currentState.enabled = true; }
        }
    }

    public bool HasTarget()
    {
        return target != null;
    }

    public bool TargetIsInRange()
    {
        return
            (target != null) &&
            (Vector3.Distance(target.position, transform.position) < shootingDistance);
    }

    Vector3 lastTargetPosition;
    bool hasAlreadyVisitedTheLastTargetPosition = true;
    internal bool HasAlreadyVisitedTheLastTargetPosition()
    {
        return hasAlreadyVisitedTheLastTargetPosition;
    }

    public Vector3 GetLastTargetPosition () { return lastTargetPosition; }

    internal void NotifyLastTargetPositionReached()
    {
        hasAlreadyVisitedTheLastTargetPosition = true;
    }
    #endregion

    #region ITargeteable Implementation
    public ITargeteable.Faction GetFaction()
    {
        return faction;
    }

    public Transform GetTransform()
    {
        return transform;
    }
    #endregion
}
