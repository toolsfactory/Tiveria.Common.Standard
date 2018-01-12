using System.Diagnostics;

namespace Tiveria.Common.Ensure
{
    public static class EnsureIntExtensions
    {
        [DebuggerStepThrough]
        public static Param<int> IsLowerThan(this Param<int> param, int limit)
        {
            if (param.Value >= limit)
                throw ExceptionFactory.CreateForParamValidation(param, EnsureRes.Ensure_IsNotLt.Inject(param.Value, limit));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<int> IsLowerOrEqual(this Param<int> param, int limit)
        {
            if (!(param.Value <= limit))
                throw ExceptionFactory.CreateForParamValidation(param, EnsureRes.Ensure_IsNotLte.Inject(param.Value, limit));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<int> IsGreaterThan(this Param<int> param, int limit)
        {
            if (param.Value <= limit)
                throw ExceptionFactory.CreateForParamValidation(param, EnsureRes.Ensure_IsNotGt.Inject(param.Value, limit));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<int> IsGreaterOrEqual(this Param<int> param, int limit)
        {
            if (!(param.Value >= limit))
                throw ExceptionFactory.CreateForParamValidation(param, EnsureRes.Ensure_IsNotGte.Inject(param.Value, limit));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<int> IsInRange(this Param<int> param, int min, int max)
        {
            if (param.Value < min)
                throw ExceptionFactory.CreateForParamValidation(param, EnsureRes.Ensure_IsNotInRange_ToLow.Inject(param.Value, min));

            if (param.Value > max)
                throw ExceptionFactory.CreateForParamValidation(param, EnsureRes.Ensure_IsNotInRange_ToHigh.Inject(param.Value, max));

            return param;
        }
    }
}