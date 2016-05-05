﻿namespace CsLuaConverter.Providers.TypeKnowledgeRegistry
{
    public interface ITypeKnowledgeRegistry
    {
        TypeKnowledge CurrentType { get; set; }
        TypeKnowledge ExpectedType { get; set; }
        PossibleMethods PossibleMethods { get; set; }
    }
}