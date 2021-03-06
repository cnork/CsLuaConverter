﻿namespace CsLuaSyntaxTranslator
{
    using System.Collections.Generic;
    using System.Linq;
    using CsLuaSyntaxTranslator.MethodSignature;
    using Microsoft.CodeAnalysis;

    public class TypeSymbolSemanticAdaptor : ISemanticAdaptor<ITypeSymbol>
    {
        public string GetFullName(ITypeSymbol symbol)
        {
            return this.GetFullNamespace(symbol.ContainingNamespace) + "." + symbol.Name;
        }

        public string GetFullNamespace(ITypeSymbol symbol)
        {
            return this.GetFullNamespace(symbol.ContainingNamespace);
        }

        private string GetFullNamespace(INamespaceSymbol nameSpace)
        {
            return (nameSpace.ContainingNamespace.IsGlobalNamespace ? "" : this.GetFullNamespace(nameSpace.ContainingNamespace) + ".") + nameSpace.Name;
        }

        public string GetName(ITypeSymbol symbol)
        {
            return symbol.Name;
        }

        public bool HasTypeGenerics(ITypeSymbol symbol)
        {
            var namedModel = symbol as INamedTypeSymbol;
            return namedModel != null && namedModel.TypeArguments.Any();
        }

        public bool IsGenericType(ITypeSymbol symbol)
        {
            return symbol.Kind == SymbolKind.TypeParameter;
        }

        public bool IsMethodGeneric(ITypeSymbol symbol)
        {
            return (symbol as ITypeParameterSymbol)?.TypeParameterKind == TypeParameterKind.Method;
        }

        public ITypeSymbol[] GetGenerics(ITypeSymbol symbol)
        {
            return ((INamedTypeSymbol)symbol).TypeArguments.ToArray();
        }

        public bool IsArray(ITypeSymbol symbol)
        {
            return symbol.TypeKind == TypeKind.Array;
        }

        public ITypeSymbol GetArrayGeneric(ITypeSymbol symbol)
        {
            return ((IArrayTypeSymbol)symbol).ElementType;
        }

        public bool IsInterface(ITypeSymbol symbol)
        {
            return symbol.TypeKind == TypeKind.Interface;
        }

        public IDictionary<ITypeSymbol, ITypeSymbol> GetAppliedClassGenerics(ITypeSymbol classTypeObj)
        {
            var namedTypeSymbol = classTypeObj as INamedTypeSymbol;

            return namedTypeSymbol?.TypeParameters.Zip(namedTypeSymbol.TypeArguments, (param, arg) => new {param, arg})
                .ToDictionary((v) => (ITypeSymbol)v.param, v => v.arg);
        }
    }
}