using System;
using System.Threading.Tasks;

namespace DialogUtils.Helpers
{
    static class TaskExtensions
    {
        // A simplified version inspired by https://github.com/brminnick/AsyncAwaitBestPractices/blob/main/Src/AsyncAwaitBestPractices/SafeFireAndForgetExtensions.shared.cs
        public static async void SimpleFireAndForget(this Task task, bool rethrow = true)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch (Exception)
            {
                if (rethrow)
                {
                    throw;
                }
            }
        }
    }
}
