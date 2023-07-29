using System;
using FireKeeper.Config;
using FireKeeper.Core.Engine;
using UnityEngine;

public sealed class EnemyController : IDisposable
{
    private readonly IEffect _effect;
    private readonly IMovement _movement;
    private readonly IPlayerController _playerController;
    private readonly IEnemyDefinition _definition;
    private readonly ICoreTimeController _coreTimeController;
    private readonly IEnemyFactory _enemyFactory;
    private readonly EnemyView _view;

    private readonly float _chaseRangeSquared;
    private readonly float _attackRangeSquared;
    public IEnemyDefinition Definition => _definition;

    public EnemyView GetView() => _view;

    public EnemyController(IEnemyDefinition definition, 
        IEnemyFactory enemyFactory,
        IPlayerController playerController,
        ICoreTimeController coreTimeController,
        EnemyView view)
    {
        _definition = definition;
        _playerController = playerController;
        _coreTimeController = coreTimeController;
        _enemyFactory = enemyFactory;
        _view = view;
        
        _effect = _definition.EffectDefinition.GetEffect();
        _movement = _definition.MovementDefinition.GetMovement();
        _chaseRangeSquared = _definition.ChaseRange * _definition.ChaseRange;
        _attackRangeSquared = _definition.AttackRange * _definition.AttackRange;
        
        _coreTimeController.TickAction += Tick;
    }
    
    public void Dispose()
    {
        _coreTimeController.TickAction -= Tick;
    }

    private void Tick(float deltaTime)
    {
        Vector3 offset = _playerController.Position - _view.Position;
        float sqrLen = offset.sqrMagnitude;
        
        if (sqrLen <= _chaseRangeSquared)
            _movement.Move(_view.transform,_playerController.Position);
        
        if (sqrLen <= _attackRangeSquared)
            ApplyEffect();
    }

    private void ApplyEffect()
    {
        _playerController.ApplyEffect(_effect);
        _enemyFactory.DestroyEnemy(this);
    }
}