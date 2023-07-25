using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameLib.Window
{
    public static class WindowButtonExtensions
    {
        public static void Subscribe(this Button button, Func<UniTask> action, CancellationTokenSource cts)
        {
            button.onClick.AddListener(() => action().Forget());
            WaitUnsubscribeCancelAsync(button, cts).Forget();
        }
        
        public static void Subscribe(this Button button, UnityAction action, CancellationTokenSource cts)
        {
            button.onClick.AddListener(action);
            WaitUnsubscribeCancelAsync(button, cts).Forget();
        }
        
        private static async UniTask WaitUnsubscribeCancelAsync(Button button, CancellationTokenSource cts)
        {
            await cts.Token.WaitUntilCanceled();
			
            button.onClick.RemoveAllListeners();
        }
    }
}