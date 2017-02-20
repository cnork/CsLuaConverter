﻿namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;

    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class EqualsValueClauseVisitor : SyntaxVisitorBase<EqualsValueClauseSyntax>
    {
        public EqualsValueClauseVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public EqualsValueClauseVisitor(EqualsValueClauseSyntax syntax) : base(syntax)
        {
            
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            this.Syntax.Write(textWriter, context);
        }
    }
}