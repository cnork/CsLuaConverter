﻿namespace CsLuaConverterTests
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using CsLuaConverter.CodeTreeLuaVisitor;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CoverageTests
    {
        private static readonly Regex[] Filters = {
            new Regex(@"Token$"),
            new Regex(@"Keyword$"),
            new Regex(@"Trivia$"),
            new Regex(@"UsingStatement"), // Using is being handled inside the file or namespace visitors and does not need a separate visitor.
        };

        [TestMethod]
        public void VerifyAllVisitorsImplemented()
        {
            var visitorImplementations = typeof(BaseVisitor).Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(BaseVisitor))).ToArray();

            var syntaxKinds = (SyntaxKind[]) Enum.GetValues(typeof (SyntaxKind));
            
            var missingImplementations = syntaxKinds.Where(kind => !visitorImplementations.Any(t => t.Name.Equals(kind.ToString() + "Visitor")))
                .Where(kind => !Filters.Any(filter => filter.IsMatch(kind.ToString())))
                .ToArray();

            Assert.AreEqual(0, missingImplementations.Length, $"{missingImplementations.Length} visitors have not been implemented.");
        }
    }
}
