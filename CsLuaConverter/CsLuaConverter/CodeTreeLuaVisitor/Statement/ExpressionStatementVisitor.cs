﻿namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class ExpressionStatementVisitor : BaseVisitor
    {
        private readonly BaseVisitor innerVisitor;
        public ExpressionStatementVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(1, SyntaxKind.SemicolonToken);
            this.innerVisitor = this.CreateVisitor(0);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.innerVisitor.Visit(textWriter, providers);
            providers.TypeKnowledgeRegistry.CurrentType = null;
            textWriter.WriteLine(";");
        }
    }
}