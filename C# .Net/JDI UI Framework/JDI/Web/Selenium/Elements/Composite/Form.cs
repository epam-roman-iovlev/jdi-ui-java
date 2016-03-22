using System;
using System.Collections.Generic;
using Epam.JDI.Core.Interfaces.Base;
using Epam.JDI.Core.Interfaces.Complex;
using Epam.JDI.Web.Selenium.Elements.Base;

namespace Epam.JDI.Web.Selenium.Elements.Composite
{
    public class Form<T> : WebElement, IForm<T>
    {
        public string GetValue()
        {
            throw new NotImplementedException();
        }
        void ISetValue.SetValue(string value)
        {
            throw new NotImplementedException();
        }

        public void Fill(T entity)
        {
            throw new NotImplementedException();
        }
        public void Fill(Dictionary<string, string> map)
        {
            throw new NotImplementedException();
        }

        public List<string> Verify(T entity)
        {
            throw new NotImplementedException();
        }
        public List<string> Verify(Dictionary<string, string> map)
        {
            throw new NotImplementedException();
        }

        public void Check(T entity)
        {
            throw new NotImplementedException();
        }
        public void Check(Dictionary<string, string> map)
        {
            throw new NotImplementedException();
        }

        public void Submit(string text)
        {
            throw new NotImplementedException();
        }
        public void Submit(string text, string buttonName)
        {
            throw new NotImplementedException();
        }

        public void Login(string text)
        {
            throw new NotImplementedException();
        }
        public void Add(string text)
        {
            throw new NotImplementedException();
        }
        public void Publish(string text)
        {
            throw new NotImplementedException();
        }
        public void Save(string text)
        {
            throw new NotImplementedException();
        }
        public void Update(string text)
        {
            throw new NotImplementedException();
        }
        public void Cancel(string text)
        {
            throw new NotImplementedException();
        }
        public void Close(string text)
        {
            throw new NotImplementedException();
        }
        public void Back(string text)
        {
            throw new NotImplementedException();
        }
        public void Select(string text)
        {
            throw new NotImplementedException();
        }
        public void Next(string text)
        {
            throw new NotImplementedException();
        }
        public void Search(string text)
        {
            throw new NotImplementedException();
        }
        public void Submit(T entity)
        {
            throw new NotImplementedException();
        }
        public void Login(T entity)
        {
            throw new NotImplementedException();
        }
        public void Add(T entity)
        {
            throw new NotImplementedException();
        }
        public void Publish(T entity)
        {
            throw new NotImplementedException();
        }
        public void Save(T entity)
        {
            throw new NotImplementedException();
        }
        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
        public void Cancel(T entity)
        {
            throw new NotImplementedException();
        }
        public void Close(T entity)
        {
            throw new NotImplementedException();
        }
        public void Back(T entity)
        {
            throw new NotImplementedException();
        }
        public void Select(T entity)
        {
            throw new NotImplementedException();
        }
        public void Next(T entity)
        {
            throw new NotImplementedException();
        }
        public void Search(T entity)
        {
            throw new NotImplementedException();
        }
        public void Submit(T entity, string buttonName)
        {
            throw new NotImplementedException();
        }
        public void Submit(T entity, Enum buttonName)
        {
            throw new NotImplementedException();
        }
        public void Submit(Dictionary<string, string> objstrings)
        {
            throw new NotImplementedException();
        }

        
    }
}
