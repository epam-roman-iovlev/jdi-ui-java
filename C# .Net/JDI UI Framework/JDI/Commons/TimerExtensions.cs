﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.JDI.Commons
{
    public static class TimerExtensions
    {
        public static bool AvoidExceptions(this Func<bool> waitFunc)
        {
            try { return waitFunc(); }
            catch { return false; }
        }

        public static bool ForceDone(this Action action)
        {
            return new Timer().Wait(() => { action.Invoke(); return true; });
        }

        public static T GetByCondition<T>(this Func<T> getFunc, Func<T, bool> conditionFunc)
        {
            return new Timer().GetResultByCondition(getFunc, conditionFunc);
        }
    }
}
