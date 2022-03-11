using Autojector.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Services;

public interface ISimpleInjectedByAttribute
{
    public string GetData();
}
[Transient(typeof(ISimpleInjectedByAttribute))]
internal class SimpleInjectedByAttribute : ISimpleInjectedByAttribute
{
    public string GetData()
    {
        return "SimpleInjectedByAttribute";
    }
}
