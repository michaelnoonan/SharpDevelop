﻿// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Matthew Ward" email="mrward@users.sourceforge.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Collections;
using ICSharpCode.PythonBinding;
using ICSharpCode.SharpDevelop.Dom;
using NUnit.Framework;
using PythonBinding.Tests;
using PythonBinding.Tests.Utils;

namespace PythonBinding.Tests.Resolver
{
	/// <summary>
	/// Tests the PythonResolver correctly resolves the expression:
	/// "Console." when the System namespace is imported.
	/// </summary>
	[TestFixture]
	public class ResolveConsoleTestFixture
	{
		PythonResolver resolver;
		MockProjectContent mockProjectContent;
		ResolveResult resolveResult;
		MockClass testClass;
		ICompilationUnit compilationUnit;
		MockClass systemConsoleClass;
		
		[TestFixtureSetUp]
		public void SetUpFixture()
		{
			resolver = new PythonResolver();
			mockProjectContent = new MockProjectContent();
			
			// Do not return any class from GetClass call. This method
			// will not return anything in the real class since the
			// type is not fully qualified with its namespace.
			mockProjectContent.ClassToReturnFromGetClass = null;
			
			systemConsoleClass = new MockClass(mockProjectContent, "System.Console");
			mockProjectContent.ClassesInProjectContent.Add(systemConsoleClass);
			
			compilationUnit = new DefaultCompilationUnit(mockProjectContent) { ErrorsDuringCompile = true };
			testClass = new MockClass(compilationUnit, "Test");
			compilationUnit.Classes.Add(testClass);
			ParseInformation parseInfo = new ParseInformation(compilationUnit);
						
			string python = "import System\r\n" +
							"class Test:\r\n" +
							"\tdef __init__(self):\r\n" +
							"\tConsole\r\n";
			ExpressionResult expressionResult = new ExpressionResult("Console", new DomRegion(3, 7), null, null);
			resolveResult = resolver.Resolve(expressionResult, parseInfo, python);
		}
	
		[Test]
		public void IsTypeResolveResult()
		{
			Assert.IsInstanceOf(typeof(TypeResolveResult), resolveResult);
		}
		
		[Test]
		public void ResolvedClass()
		{
			TypeResolveResult typeResolveResult = resolveResult as TypeResolveResult;
			Assert.AreEqual(systemConsoleClass, typeResolveResult.ResolvedClass);
		}
//		
//		[Test]
//		public void IsGetClassCalled()
//		{
//			Assert.IsTrue(mockProjectContent.GetClassCalled);
//		}
//		
//		[Test]
//		public void GetClassName()
//		{
//			Assert.AreEqual("System.Console", mockProjectContent.GetClassName);
//		}		
	}
}