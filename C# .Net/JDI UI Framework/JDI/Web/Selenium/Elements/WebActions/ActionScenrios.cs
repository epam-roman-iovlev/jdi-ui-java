﻿using System;
using Epam.JDI.Commons;
using Epam.JDI.Core;
using Epam.JDI.Core.Logging;
using Epam.JDI.Core.Reporting;
using Epam.JDI.Core.Settings;

namespace Epam.JDI.Web.Selenium.Elements.WebActions
{
    public class ActionScenrios
    {
        private WebBaseElement _element;

        public ActionScenrios SetElement(WebBaseElement element)
        {
            _element = element;
            return this;
        }

        public void ActionScenario(string actionName, Action action, LogLevels logSettings)
        {
            _element.LogAction(actionName, logSettings);
            var timer = new Timer();
            new Timer(JDISettings.Timeouts.CurrentTimeoutSec).Wait(() => {
                action.Invoke();
                return true;
            });
            JDISettings.Logger.Info(actionName + " done");
            PerformanceStatistic.AddStatistic(timer.TimePassed.TotalMilliseconds);
        }

        public TResult ResultScenario<TResult>(string actionName, Func<TResult> action, Func<TResult, string> logResult, LogLevels level)
        {
            _element.LogAction(actionName);
            var timer = new Timer();
            var result =
                ExceptionUtils.ActionWithException(() => new Timer(JDISettings.Timeouts.CurrentTimeoutSec)
                    .GetResultByCondition(action.Invoke, res => true),
                    ex => $"Do action {actionName} failed. Can't got result. Reason: {ex}");
            if (result == null)
                throw JDISettings.Exception("Do action %s failed. Can't got result", actionName);
            var stringResult = logResult == null
                    ? result.ToString()
                    : logResult.Invoke(result);
            var timePassed = timer.TimePassed.TotalMilliseconds;
            PerformanceStatistic.AddStatistic(timer.TimePassed.TotalMilliseconds);
            JDISettings.ToLog($"Get result '{stringResult}' in {(timePassed / 1000).ToString("F")} seconds", level);
            return result;
        }
    }
}
