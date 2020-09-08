using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Extensions.ExTask;

namespace Extensions.ExMath
{
    public static class MathExtensions
    {
        public static async Task InterpolateTime(float time, Action<float> normalizedCallback,
            CancellationToken cancellationToken = default)
        {
            if (cancellationToken == default)
            {
                cancellationToken = CancellationSourceRoot.Token;
            }

            try
            {
                var remain = time;
                while (remain > 0)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    normalizedCallback?.Invoke(1 - remain / time);
                    remain -= Time.deltaTime;
                    await Task.Yield();
                }

                normalizedCallback?.Invoke(1);
            }
            catch (OperationCanceledException _)
            {
            }
        }
    }
}