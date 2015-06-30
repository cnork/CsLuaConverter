﻿namespace CsLuaCompiler.SolutionAnalysis
{
    using System.Collections.Generic;
    using CsLuaAttributes;
    using Microsoft.CodeAnalysis;
    using SyntaxAnalysis;

    public class ProjectInfo
    {
        public string Name { get; set; }
        public ProjectType ProjectType { get; set; }
        public CsLuaAddOnAttribute CsLuaAddOnAttribute { get; set; }
        public Project Project { get; set; }
        public string ProjectPath { get; set; }
        public bool RequiresCsLuaMetaHeader { get; set; }
        public IList<string> ReferencedProjects {get; set; }

        public bool IsAddOn()
        {
            return this.ProjectType == ProjectType.CsLuaAddOn || this.ProjectType == ProjectType.LuaAddOn;
        }
    }
}