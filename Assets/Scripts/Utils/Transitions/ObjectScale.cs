using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace BotScheduler.Utils
{
    enum SetGameObjectActiveModes
    {
        YES,
        NO,
        REVERSE
    }

    public class ObjectScale : MonoBehaviour
    {
        [SerializeField] private bool _initialState = false;
        [SerializeField] private SetGameObjectActiveModes _setGameObjectActive = SetGameObjectActiveModes.NO;
        [SerializeField] private float _speed = 1f;

        [SerializeField] private AnimationCurve _animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        [SerializeField] private Vector3 _minScale = Vector3.zero;
        [SerializeField] private Vector3 _maxScale = Vector3.one;

        [SerializeField] private Transform scaleActor = null;

        private float _currentT = 0;
        private float _targetT = 0;

        private Task _currentTask;
        private CancellationTokenSource _cancellationTokenSource = null;

        void Start()
        {
            if (scaleActor == null)
            {
                scaleActor = transform;
            }

            _targetT = _initialState ? 1f : 0f;
            _currentT = _targetT;

            TickAnimation();
            SetGameObjectActive();
        }

        async public void SetActive(bool active)
        {
            CancelPreviousTask();

            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                await SetActiveAsync(active, _cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                // noop
            }
        }

        async private Task SetActiveAsync(bool active, CancellationToken cancellationToken)
        {
            _targetT = active ? 1f : 0f;

            while (_currentT != _targetT)
            {
                cancellationToken.ThrowIfCancellationRequested();

                _currentT = Mathf.MoveTowards(_currentT, _targetT, _speed * Time.deltaTime);

                SetGameObjectActive();
                TickAnimation();

                await Task.Yield();
            }

            SetGameObjectActive();
            TickAnimation();

            _cancellationTokenSource = null;
        }

        private void SetGameObjectActive()
        {
            if (_setGameObjectActive == SetGameObjectActiveModes.NO)
            {
                return;
            }

            var isActive = _setGameObjectActive == SetGameObjectActiveModes.YES ? _currentT > 0 : _currentT < 1;

            if (isActive == scaleActor.gameObject.activeSelf) {
                return;
            }

            scaleActor.gameObject.SetActive(isActive);
        }

        private void TickAnimation()
        {
            scaleActor.localScale = Vector3.Lerp(_minScale, _maxScale, _animationCurve.Evaluate(_currentT));
        }

        private void CancelPreviousTask()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource = null;
            }
        }

        void Destroy()
        {
            CancelPreviousTask();
        }
    }
}