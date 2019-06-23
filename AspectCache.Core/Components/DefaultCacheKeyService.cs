using AspectCache.Core.Interfaces;
using Castle.DynamicProxy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AspectCache.Core.Components
{
    /// <summary>
    /// Generate a unique key to be used for caching responses from methods.
    /// Based on work by Mark Rogers (m4bwav) on github. I thought it was really well done and I didn't see a need to reinvent the wheel.
    /// </summary>
    public class DefaultCacheKeyService : IKeyService
    {
        public string GenerateUniqueKeyForCall(IInvocation invocation, string method = null)
        {
            StringBuilder builder = new StringBuilder();

            ProcessClassAndMethod(invocation, builder);

            ProcessGenericArguments(invocation, builder);

            ProcessArguments(invocation, builder);


            return builder.ToString();
        }

        private static void ProcessArguments(IInvocation invocation, StringBuilder builder)
        {
            ProcessArgumentTypes(invocation, builder);

            ProcessArgumentValues(invocation, builder);
        }

        private static void ProcessArgumentValues(IInvocation invocation, StringBuilder builder)
        {
            var parameterCount = invocation.Method.GetParameters().Count();

            if (parameterCount == 0)
                return;

            builder.Append("values:");

            for (int i = 0; i < parameterCount; i++)
            {
                object value = invocation.GetArgumentValue(i);

                string jsonValue = JsonConvert.SerializeObject(value);

                builder.Append(jsonValue + "|");
            }

            builder.Append(";");
        }

        private static void ProcessArgumentTypes(IInvocation invocation, StringBuilder builder)
        {
            ParameterInfo[] parameters = invocation.Method.GetParameters();

            if (!parameters.Any())
            {
                return;
            }

            IEnumerable<string> argumentTypes = parameters.Select(x => x.ParameterType.ToString() + ",");

            builder.Append("Argument Types:");

            foreach (string argumentTypeName in argumentTypes)
            {
                builder.Append(argumentTypeName);
            }

            builder.Append(";");
        }

        private static void ProcessClassAndMethod(IInvocation invocation, StringBuilder builder)
        {
            string className = invocation.InvocationTarget + ";";

            builder.Append(className);

            string methodName = invocation.Method.Name + ";";

            builder.Append(methodName);
        }

        private static void ProcessGenericArguments(IInvocation invocation, StringBuilder builder)
        {
            Type[] genericArguments = invocation.GenericArguments;

            if (genericArguments == null || !genericArguments.Any())
            {
                return;
            }

            builder.Append("Generic Arguments:");

            foreach (Type arguments in genericArguments)
            {
                builder.Append(arguments + ",");
            }

            builder.Append(";");
        }
    }
}
