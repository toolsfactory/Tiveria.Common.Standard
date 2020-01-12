using System;

namespace Tiveria.Common.Extensions
{
    public static class CharExtensions
    { 
        public static bool IsPrintable(this char c)
        {
            return (Char.IsLetterOrDigit(c) || Char.IsPunctuation(c) || Char.IsSymbol(c) || (Char.IsWhiteSpace(c)));
        }
    }
}