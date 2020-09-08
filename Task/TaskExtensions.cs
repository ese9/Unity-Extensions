using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Extensions.ExTask
{
    public static class TaskExtensions
    {
        public static Task ContinueWithCurrentContext(this Task task, Action callback)
        {
            return task.ContinueWith(t =>
            {
                if (t.Status == TaskStatus.RanToCompletion)
                    callback.Invoke();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public static async Task WaitUnityOperation<T>(this T asyncOperation, CancellationToken token = default)
            where T : AsyncOperation
        {
            if (token == default)
            {
                token = CancellationSourceRoot.Token;
            }

            while (asyncOperation.isDone && !token.IsCancellationRequested)
            {
                await Task.Yield();
            }
        }
    }
}