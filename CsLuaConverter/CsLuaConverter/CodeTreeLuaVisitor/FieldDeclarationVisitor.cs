﻿namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System;
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Providers;

    public class FieldDeclarationVisitor : BaseVisitor
    {

        private readonly VariableDeclarationVisitor variableVisitor;

        public bool IsStatic { get; private set; }

        public bool IsConst { get; private set; }

        public Scope Scope { get; private set; }

        public FieldDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            var accessorNodes = this.GetFilteredNodes(new KindRangeFilter(null, SyntaxKind.VariableDeclaration));
            var scopeValue =
                ((CodeTreeLeaf) (new KindFilter(SyntaxKind.PrivateKeyword, SyntaxKind.PublicKeyword,
                    SyntaxKind.ProtectedKeyword, SyntaxKind.InternalKeyword).Filter(accessorNodes)).SingleOrDefault())?.Text;
            this.Scope = scopeValue != null ? (Scope) Enum.Parse(typeof (Scope), scopeValue, true) : Scope.Public;

            this.IsStatic = accessorNodes.Any(n => n.Kind.Equals(SyntaxKind.StaticKeyword));

            this.IsConst = accessorNodes.Any(n => n.Kind.Equals(SyntaxKind.ConstKeyword));

            this.variableVisitor = (VariableDeclarationVisitor) this.CreateVisitor(accessorNodes.Length);
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            throw new System.NotImplementedException();
        }

        public void WriteDefaultValue(IndentedTextWriter textWriter, IProviders providers, bool @static = false)
        {
            if ((this.IsStatic || this.IsConst) != @static)
            {
                return;
            }

            this.variableVisitor.WriteDefaultValue(textWriter, providers);
        }
    }
}