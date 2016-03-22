using System;
using System.Collections.Generic;
using System.Linq;
using Epam.JDI.Commons;
using Epam.JDI.Core;
using RestSharp.Extensions;
using static System.String;
using static System.Text.RegularExpressions.Regex;
using static Epam.JDI.Core.Settings.JDISettings;

namespace Epam.JDI.Web.Selenium.Elements.WebActions
{
    public class ElementsActions
    {
        private readonly WebBaseElement _element;

        public ElementsActions(WebBaseElement element)
        {
            _element = element;
        }

        public ActionInvoker Invoker => _element.Invoker;

        // Element Actions
        public bool IsDisplayed(Func<bool> isDisplayed)
        {
            return Invoker.DoJActionResult("Is element displayed", isDisplayed);
        }

        public bool IsHidden(Func<bool> isHidden)
        {
            return Invoker.DoJActionResult("Is element hidden", isHidden);
        }

        public void WaitDisplayed(Func<bool> isDisplayed)
        {
            Invoker.DoJActionResult("Wait element displayed", isDisplayed);
        }

        public void WaitVanished(Func<bool> isVanished)
        {
            Invoker.DoJActionResult("Wait element vanished", isVanished);
        }

        // Value Actions
        public string GetValue(Func<string> getValueFunc)
        {
            return Invoker.DoJActionResult("Get value", getValueFunc);
        }

        public void SetValue(string value, Action<string> setValueAction)
        {
            Invoker.DoJAction("Get value", () => setValueAction.Invoke(value));
        }

        // Click Action
        public void Click(Action clickAction)
        {
            Invoker.DoJAction("Click on Element", clickAction);
        }

        // Text Actions
        public string GetText(Func<string> getTextAction)
        {
            return Invoker.DoJActionResult("Get text", getTextAction);
        }

        public string WaitText(string text, Func<string> getTextAction)
        {
            return Invoker.DoJActionResult($"Wait text contains '{text}'",
                () => getTextAction.GetByCondition(t => t.Contains(text)));
        }

        public string WaitMatchText(string regEx, Func<string> getTextAction)
        {
            return Invoker.DoJActionResult($"Wait text match regex '{regEx}'",
                () => getTextAction.GetByCondition(t => t.Matches(regEx)));
        }

        // Check/Select Actions
        public bool IsSelected(Func<bool> isSelectedAction)
        {
            return Invoker.DoJActionResult("Is Selected", isSelectedAction);
        }

        public void Check(Action checkAction)
        {
            Invoker.DoJAction("Check Checkbox", checkAction);
        }

        public void Uncheck(Action uncheckAction)
        {
            Invoker.DoJAction("Uncheck Checkbox", uncheckAction);
        }

        public bool IsChecked(Func<bool> isCheckedAction)
        {
            return Invoker.DoJActionResult("IsChecked",
                isCheckedAction,
                result => "Checkbox is " + (result ? "checked" : "unchecked"));
        }

        // Input Actions
        public void InputLines(Action clearAction, Action<string> inputAction, params string[] textLines)
        {
            Invoker.DoJAction("Input several lines of text in textarea",
                () =>
                {
                    clearAction.Invoke();
                    inputAction.Invoke(Join("\n", textLines));
                });
        }

        public void AddNewLine(string textLine, Action<string> inputAction)
        {
            Invoker.DoJAction("Add text from new line in textarea",
                () => inputAction.Invoke("\n" + textLine));
        }

        public string[] GetLines(Func<string> getTextAction)
        {
            return Invoker.DoJActionResult("Get text as lines", () => Split(getTextAction.Invoke(),"\\\\n"));
        }

        public void Input(string text, Action<string> inputAction)
        {
            Invoker.DoJAction("Input text '" + text + "' in text field",
                () => inputAction.Invoke(text));
        }

        public void Clear(Action clearAction)
        {
            Invoker.DoJAction("Clear text field", clearAction);
        }

        public void Focus(Action focusAction)
        {
            Invoker.DoJAction("Focus on text field", focusAction);
        }

        // Selector
        public void Select(string name, Action<string> selectAction)
        {
            Invoker.DoJAction($"Select '{name}'", () => selectAction.Invoke(name));
        }

        public void Select(int index, Action<int> selectByIndexAction)
        {
            Invoker.DoJAction($"Select '{index}'", () => selectByIndexAction.Invoke(index));
        }

        public bool IsSelected(string name, Func<string, bool> isSelectedAction)
        {
            return Invoker.DoJActionResult($"Wait is '{name}' selected", () => isSelectedAction.Invoke(name));
        }

        public string GetSelected(Func<string> isSelectedAction)
        {
            return Invoker.DoJActionResult("Get Selected element name", isSelectedAction);
        }

        public int GetSelectedIndex(Func<int> isSelectedAction)
        {
            return Invoker.DoJActionResult("Get Selected element index", isSelectedAction);
        }

        //MultiSelector
        public void Select(Action<string[]> selectListAction, params string[] names)
        {
            Invoker.DoJAction($"Select '{names.Print()}'", () => selectListAction.Invoke(names));
        }

        public void Select(Action<int[]> selectListAction, int[] indexes)
        {
            var listIndexes = indexes.Select(i => i.ToString()).ToList();
            Invoker.DoJAction($"Select '{listIndexes.Print()}'", () => selectListAction.Invoke(indexes));
        }

        public List<string> AreSelected(Func<List<string>> getNames, Func<string, bool> waitSelectedAction)
        {
            return Invoker.DoJActionResult("Are selected", () =>
                getNames.Invoke().Where(waitSelectedAction).ToList());
        }

        public void WaitSelected(Func<string, bool> waitSelectedAction, params string[] names)
        {
            var result = Invoker.DoJActionResult($"Are deselected '{names.Print()}'", 
                () => names.All(waitSelectedAction) );
            Asserter.IsTrue(result);
        }

        public List<string> AreDeselected(Func<List<string>> getNames, Func<string, bool> waitSelectedAction)
        {
            return Invoker.DoJActionResult("Are deselected", () =>
                getNames.Invoke().Where(name => !waitSelectedAction.Invoke(name)).ToList());
        }

        public void WaitDeselected(Func<string, bool> waitSelectedAction, params string[] names)
        {
            bool result = Invoker.DoJActionResult($"Are deselected '{names.Print()}'", 
                () => names.All(name => !waitSelectedAction.Invoke(name)));
            Asserter.IsTrue(result);
        }

        public T FindImmediately<T>(Func<T> func, T ifError)
        {
            _element.SetWaitTimeout(0);
            var temp = _element.WebAvatar.LocalElementSearchCriteria;
            _element.WebAvatar.LocalElementSearchCriteria = el => true;
            T result;
            try { result = func.Invoke(); }
            catch { result = ifError; }
            _element.WebAvatar.LocalElementSearchCriteria = temp;
            _element.RestoreWaitTimeout();
            return result;
        }
    }
}
