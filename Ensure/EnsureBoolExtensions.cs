using System.Diagnostics;

namespace Tiveria.Common.Ensure
{
    public static class EnsureBoolExtensions
    {
        [DebuggerStepThrough]
        public static Param<bool> IsTrue(this Param<bool> param)
        {
            if (!param.Value)
                throw ExceptionFactory.CreateForParamValidation(param, EnsureRes.Ensure_IsNotTrue);

            return param;
        }

        [DebuggerStepThrough]
        public static Param<bool> IsFalse(this Param<bool> param)
        {
            if (param.Value)
                throw ExceptionFactory.CreateForParamValidation(param, EnsureRes.Ensure_IsNotFalse);

            return param;
        }
    }
}