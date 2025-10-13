using System;

namespace _RaghuvanshAgarwal.Modules.Progress_Bar {
    public interface IHasProgress 
    {
        public class OnProgressChangedEventArgs : EventArgs {
            public float NormalizedProgress { get; private set; }

            public OnProgressChangedEventArgs(float normalizedProgress) {
                NormalizedProgress = normalizedProgress;
            }
        }
        public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    }
}
