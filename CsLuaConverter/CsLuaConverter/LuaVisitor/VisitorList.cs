﻿namespace CsLuaConverter.LuaVisitor
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Providers;

    public static class VisitorList
    {
        private static readonly List<IVisitor> Visitors = new List<IVisitor>()
        {
            new NamespaceVisitor(),
            new ClassVisitor(),
            new TypeVisitor(),
            new FieldDeclarationVisitor(),
            new NumericLiteralExpressionVisitor(),
            new MethodDeclarationVisitor(),
            new ParameterListVisitor(),
            new BlockVisitor(),
            new PredefinedTypeVisitor(),
            new ParameterVisitor(),
            new IdentifierNameVisitor(),
            new GenericNameVisitor(),
            new StringLiteralExpressionVisitor(),
            new SimpleVisitors(),
            new PropertyDeclarationVisitor(),
            new InterfaceDeclarationVisitor(),
            new AttributeListVisitor(),
            new StatementVisitor(),
            new VariableDeclaratorVisitor(),
            new ObjectCreationExpressionVisitor(),
            new ArgumentListVisitor(),
        };

        private static IndentedTextWriter writer;
        private static IProviders providers;

        [System.Diagnostics.DebuggerNonUserCode]
        public static void Visit<T>(T element)
        {
            Visit<T>(element, writer, providers);
        }

        [System.Diagnostics.DebuggerNonUserCode]
        public static void Visit<T>(T element, IndentedTextWriter writer, IProviders providers)
        {
            VisitorList.writer = writer;
            VisitorList.providers = providers;

            var visitorType = typeof (IVisitor<>).MakeGenericType(element.GetType());
            var visitor = Visitors.SingleOrDefault(v => visitorType.IsInstanceOfType(v));

            if (visitor == null)
            {
                throw new Exception(string.Format("No visitor found for type {0}", element.GetType().Name));
            }

            var m = visitorType.GetMethod("Visit", BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod);

            m.Invoke(visitor, new object[] {element, writer, providers });
        }
    }
}