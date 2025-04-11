using System;
using StateForgeX.Code.Interfaces;

namespace StateForgeX.Code.Core
{
    public class FuncPredicate : IPredicate
    {
        private readonly Func<bool> func;

        public FuncPredicate(Func<bool> predicate) => func = predicate;

        public bool Evaluate() => func.Invoke();
    }
}