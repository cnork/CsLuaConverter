﻿namespace CsLuaConverter
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using CodeTree;
    using CodeTreeLuaVisitor;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using ProjectInfo = ProjectAnalysis.ProjectInfo;

    public class Analyzer : ISyntaxAnalyser
    {
        private readonly CodeTreeVisitor codeTreeVisitor;

        public Analyzer(IProviders providers)
        {
            this.codeTreeVisitor = new CodeTreeVisitor(providers);
        }

        public AnalyzedProjectInfo AnalyzeProject(ProjectInfo projectInfo)
        {
            return new AnalyzedProjectInfo()
            {
                Info = projectInfo,
                Namespaces = projectInfo.IsCsLua() ? this.GetNamespaces(projectInfo.Project) : null,
            };
        }

        private Dictionary<string, Action<IIndentedTextWriterWrapper>> GetNamespaces(Project project)
        {
            if (Debugger.IsAttached)
            {
                return this.GetNamespacesFromProject(project);
            }

            try
            {
                return this.GetNamespacesFromProject(project);
            }
            catch (Exception ex)
            {

                throw new WrappingException(string.Format("In project: {0}.", project.Name), ex);
            }
        }

        private Dictionary<string, Action<IIndentedTextWriterWrapper>> GetNamespacesFromProject(Project project)
        {
            IEnumerable<Document> docs = project.Documents
                .Where(doc => doc.Folders.FirstOrDefault() != "Properties"
                              && !doc.FilePath.EndsWith("AssemblyAttributes.cs")
                );

            var codeTrees = docs.Select(GetCodeTree).ToArray();

            return this.codeTreeVisitor.CreateNamespaceBasedVisitorActions(codeTrees);
        }

        private static CodeTreeBranch GetCodeTree(Document document)
        {
            SyntaxNode syntaxTreeRoot = GetSyntaxTreeRoot(document);
            return new CodeTreeBranch(syntaxTreeRoot, document.FilePath);
        }

        
        private static SyntaxNode GetSyntaxTreeRoot(Document doc)
        {
            Task<SyntaxTree> task = doc.GetSyntaxTreeAsync();
            task.Wait();
            SyntaxTree tree = task.Result;
            Task<SyntaxNode> rootTask = tree.GetRootAsync();
            rootTask.Wait();
            return rootTask.Result;
        }
    }
}