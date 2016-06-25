﻿namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using Providers;

    public class ThisExpressionVisitor : BaseVisitor
    {
        public ThisExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write("element");
            providers.TypeKnowledgeRegistry.CurrentType = providers.NameProvider.GetScopeElement("this").Type;
        }
    }
}