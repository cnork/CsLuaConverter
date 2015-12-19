﻿namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class SimpleVisitors : 
        IVisitor<FalseLiteralExpression>, IVisitor<EqualsValueClause>, IVisitor<SimpleMemberAccessExpression>, IVisitor<TrueLiteralExpression>,
        IVisitor<NullLiteralExpression>, IVisitor<SimpleAssignmentExpression>
    {
        public void Visit(FalseLiteralExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("false");
        }

        public void Visit(EqualsValueClause element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(" = ");
        }

        public void Visit(SimpleMemberAccessExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(".");
            if (element.InnerElement is IdentifierName)
            {
                IdentifierNameVisitor.Visit((IdentifierName) element.InnerElement, textWriter, providers, true);
            }
            else
            {
                VisitorList.Visit(element.InnerElement);
            }
        }

        public void Visit(TrueLiteralExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("true");
        }

        public void Visit(NullLiteralExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("nil");
        }

        public void Visit(SimpleAssignmentExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(" = ");
        }
    }
}