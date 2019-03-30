using MachineLearningSoftware.DataAccess;
using System;
using System.ComponentModel.Composition;

namespace MachineLearningSoftware.Common
{
    [Export]
    public class DynamicFunctions
    {
        private readonly ExceptionLogDataAccess _exceptionLogDataAccess;

        [ImportingConstructor]
        public DynamicFunctions(ExceptionLogDataAccess exceptionLogDataAccess)
        {
            _exceptionLogDataAccess = exceptionLogDataAccess;
        }

        public object InvokeDynamicFunction(string function)
        {
            try
            {
                if (function.StartsWith("="))
                {
                    var methodStart = function.IndexOf('=');
                    var startIndex = function.IndexOf('(');
                    var endIndex = function.IndexOf(')');
                    var methodName = function.Substring(++methodStart, startIndex - methodStart);
                    var parameters = function.Substring(++startIndex, endIndex - startIndex).Split(',');
                    return InvokeFunction(methodName, parameters);
                }
            }
            catch (Exception ex)
            {
                _exceptionLogDataAccess.LogException(ex.ToString());
                return ex;
            }

            return string.Empty;
        }

        private object InvokeFunction(string methodName, object[] parameters)
        {
            var type = typeof(DynamicFunctions);
            var methodInfo = type.GetMethod(methodName);
            return methodInfo.Invoke(this, parameters);
        }

        public string IfEqualTo(object value1, object value2, string trueValue, string falseValue)
        {
            if (int.TryParse(value1.ToString(), out int resultValue1)
                && int.TryParse(value2.ToString(), out int resultValue2))
            {
                if (resultValue1 == resultValue2)
                {
                    return trueValue;
                }
                else
                {
                    return falseValue;
                }
            }
            else
            {
                if (value1.ToString() == value2.ToString())
                {
                    return trueValue;
                }
                else
                {
                    return falseValue;
                }
            }
        }

        public string IfHigherThan(object value1, object value2, string trueValue, string falseValue)
        {
            if (int.TryParse(value1.ToString(), out int resultValue1)
                && int.TryParse(value2.ToString(), out int resultValue2))
            {
                if (resultValue1 > resultValue2)
                {
                    return trueValue;
                }
                else
                {
                    return falseValue;
                }
            }
            else
            {
                return falseValue;
            }
        }
    }
}
