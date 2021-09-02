namespace SharpBucketTests
{
    using System;

    internal static class Retry
    {
        internal static RetryFuncBuilder<TResult> Func<TResult>(Func<TResult> func)
        {
            return new RetryFuncBuilder<TResult>(func);
        }

        internal class RetryFuncBuilder<TResult>
        {
            private Func<TResult> Func { get; }

            internal RetryFuncBuilder(Func<TResult> func)
            {
                this.Func = func ?? throw new ArgumentNullException(nameof(func));
            }

            internal RetryFuncBuilder<TResult, TException> On<TException>(Func<TException, bool> predicate = null)
                where TException : Exception
            {
                return new RetryFuncBuilder<TResult, TException>(this.Func, predicate);
            }
        }

        internal class RetryFuncBuilder<TResult, TException>
            where TException : Exception
        {
            private Func<TResult> Func { get; }

            private Func<TException, bool> ExceptionPredicate { get; }

            internal RetryFuncBuilder(Func<TResult> func, Func<TException, bool> exceptionPredicate)
            {
                this.Func = func ?? throw new ArgumentNullException(nameof(func));
                this.ExceptionPredicate = exceptionPredicate;
            }

            internal TResult Times(uint times)
            {
                if (times == 0u)
                    throw new ArgumentOutOfRangeException(nameof(times), "Should be a positive number");
                var i = 0u;
                while (true)
                {
                    try
                    {
                        return this.Func();
                    }
                    catch (Exception e)
                        when(e is TException exception && this.ValidateExceptionPredicate(exception))
                    {
                        if (i + 1 == times)
                            throw;
                    }
                    i++;
                }
            }

            private bool ValidateExceptionPredicate(TException e)
            {
                return this.ExceptionPredicate == null || this.ExceptionPredicate(e);
            }
        }
    }
}
