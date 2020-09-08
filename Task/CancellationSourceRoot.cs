using System.Threading;
using UnityEditor;

namespace Extensions.ExTask
{
    public static class CancellationSourceRoot
    {
        private static CancellationTokenSource rootSource;
        public static CancellationToken Token => rootSource.Token;
        static CancellationSourceRoot()
        {
            rootSource = new CancellationTokenSource();
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
#endif
        }
#if UNITY_EDITOR
        private static void OnPlayModeChanged(PlayModeStateChange mode)
        {
            if (mode == PlayModeStateChange.ExitingPlayMode)
            {
                rootSource.Cancel();
                rootSource = new CancellationTokenSource();
            }
        }
#endif
    }
}