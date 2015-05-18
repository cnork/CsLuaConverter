﻿
namespace CsLuaCompiler.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using CsLuaCompiler.Providers;
    using Microsoft.CodeAnalysis;

    internal class New : ILuaElement
    {
        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            return;
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            return token;
        }
    }
}
