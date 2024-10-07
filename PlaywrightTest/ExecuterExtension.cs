namespace PlaywrightTest
{
    public static class ExecuterExtension
    {
        public static async Task<TOut> Then<TIn, TOut>(this Task<TIn> inputTask, Func<TIn, Task<TOut>> followedByFunc)
        {
            var inputResult = await inputTask;
            return await followedByFunc(inputResult); // Added null-forgiving operator
        }
    }
}
