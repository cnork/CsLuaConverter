﻿
namespace CsLuaConverter.Providers
{
    using System.Collections.Generic;

    using CsLuaConverter.CodeTreeLuaVisitor;
    using CsLuaConverter.MethodSignature;
    using CsLuaConverter.Providers.TypeProvider.TypeCollections;

    using GenericsRegistry;
    using Microsoft.CodeAnalysis;
    using NameProvider;
    using PartialElement;
    using TypeKnowledgeRegistry;
    using TypeProvider;

    public class Providers : IProviders
    {
        private readonly IGenericsRegistry genericsRegistry;
        private readonly ITypeProvider typeProvider;
        private readonly INameProvider nameProvider;
        private readonly IPartialElementState partialElementState;

        public Providers(IEnumerable<BaseTypeCollection> typeCollections)
        {
            this.typeProvider = new TypeNameProvider(typeCollections);
            this.genericsRegistry = new GenericsRegistry.GenericsRegistry();
            this.nameProvider = new NameProvider.NameProvider(this.typeProvider);
            this.partialElementState = new PartialElementState();
            TypeKnowledge.Providers = this;
        }

        public Providers()
        {
            this.partialElementState = new PartialElementState();
            this.SemanticAdaptor = new TypeSymbolSemanticAdaptor();
            this.TypeReferenceWriter = new TypeReferenceWriter<ITypeSymbol>(this.SemanticAdaptor);
            this.SignatureWriter = new SignatureWriter<ITypeSymbol>(new SignatureComposer<ITypeSymbol>(this.SemanticAdaptor), this.TypeReferenceWriter);
            this.partialElementState = new PartialElementState();
            TypeKnowledge.Providers = this;
        }

        public IGenericsRegistry GenericsRegistry
        {
            get
            {
                return this.genericsRegistry;
            }
        }

        public INameProvider NameProvider
        {
            get
            {
                return this.nameProvider;
            }
        }

        public ITypeProvider TypeProvider
        {
            get
            {
                return this.typeProvider;
            }
        }

        public IPartialElementState PartialElementState
        {
            get
            {
                return this.partialElementState;
            }
        }

        public SignatureWriter<ITypeSymbol> SignatureWriter { get; }

        public ITypeReferenceWriter<ITypeSymbol> TypeReferenceWriter { get; }

        public ISemanticAdaptor<ITypeSymbol> SemanticAdaptor { get; }

        public SemanticModel SemanticModel { get; set; }

        public INamedTypeSymbol CurrentClass { get; set; }
    }
}
