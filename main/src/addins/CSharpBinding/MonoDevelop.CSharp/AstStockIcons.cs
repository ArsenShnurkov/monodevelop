// 
// AstStockIcons.cs
//  
// Author:
//       Mike Krüger <mkrueger@xamarin.com>
// 
// Copyright (c) 2012 Xamarin Inc. (http://xamarin.com)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Linq;
using MonoDevelop.Ide.TypeSystem;
using MonoDevelop.Core;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using MonoDevelop.Components.MainToolbar;

namespace MonoDevelop.CSharp
{
	// Copied from StockIcons.cs and extended for the c# ast (c# AST shouldn't be in Ide)
	static class AstStockIcons
	{
		static readonly IconId Class = "md-class";
		static readonly IconId Enum = "md-enum";
		static readonly IconId Event = "md-event";
		static readonly IconId Field = "md-field";
		static readonly IconId Interface = "md-interface";
		static readonly IconId Method = "md-method";
		static readonly IconId Property = "md-property";
		static readonly IconId Struct = "md-struct";
		static readonly IconId Delegate = "md-delegate";
		public static readonly IconId Namespace = "md-name-space";

		static readonly IconId InternalClass = "md-internal-class";
		static readonly IconId InternalDelegate = "md-internal-delegate";
		static readonly IconId InternalEnum = "md-internal-enum";
		static readonly IconId InternalEvent = "md-internal-event";
		static readonly IconId InternalField = "md-internal-field";
		static readonly IconId InternalInterface = "md-internal-interface";
		static readonly IconId InternalMethod = "md-internal-method";
		static readonly IconId InternalProperty = "md-internal-property";
		static readonly IconId InternalStruct = "md-internal-struct";

		static readonly IconId InternalAndProtectedClass = "md-InternalAndProtected-class";
		static readonly IconId InternalAndProtectedDelegate = "md-InternalAndProtected-delegate";
		static readonly IconId InternalAndProtectedEnum = "md-InternalAndProtected-enum";
		static readonly IconId InternalAndProtectedEvent = "md-InternalAndProtected-event";
		static readonly IconId InternalAndProtectedField = "md-InternalAndProtected-field";
		static readonly IconId InternalAndProtectedInterface = "md-InternalAndProtected-interface";
		static readonly IconId InternalAndProtectedMethod = "md-InternalAndProtected-method";
		static readonly IconId InternalAndProtectedProperty = "md-InternalAndProtected-property";
		static readonly IconId InternalAndProtectedStruct = "md-InternalAndProtected-struct";

		static readonly IconId PrivateClass = "md-private-class";
		static readonly IconId PrivateDelegate = "md-private-delegate";
		static readonly IconId PrivateEnum = "md-private-enum";
		static readonly IconId PrivateEvent = "md-private-event";
		static readonly IconId PrivateField = "md-private-field";
		static readonly IconId PrivateInterface = "md-private-interface";
		static readonly IconId PrivateMethod = "md-private-method";
		static readonly IconId PrivateProperty = "md-private-property";
		static readonly IconId PrivateStruct = "md-private-struct";

		static readonly IconId ProtectedClass = "md-protected-class";
		static readonly IconId ProtectedDelegate = "md-protected-delegate";
		static readonly IconId ProtectedEnum = "md-protected-enum";
		static readonly IconId ProtectedEvent = "md-protected-event";
		static readonly IconId ProtectedField = "md-protected-field";
		static readonly IconId ProtectedInterface = "md-protected-interface";
		static readonly IconId ProtectedMethod = "md-protected-method";
		static readonly IconId ProtectedProperty = "md-protected-property";
		static readonly IconId ProtectedStruct = "md-protected-struct";
		
		static readonly IconId ProtectedOrInternalClass = "md-ProtectedOrInternal-class";
		static readonly IconId ProtectedOrInternalDelegate = "md-ProtectedOrInternal-delegate";
		static readonly IconId ProtectedOrInternalEnum = "md-ProtectedOrInternal-enum";
		static readonly IconId ProtectedOrInternalEvent = "md-ProtectedOrInternal-event";
		static readonly IconId ProtectedOrInternalField = "md-ProtectedOrInternal-field";
		static readonly IconId ProtectedOrInternalInterface = "md-ProtectedOrInternal-interface";
		static readonly IconId ProtectedOrInternalMethod = "md-ProtectedOrInternal-method";
		static readonly IconId ProtectedOrInternalProperty = "md-ProtectedOrInternal-property";
		static readonly IconId ProtectedOrInternalStruct = "md-ProtectedOrInternal-struct";
		
		static IconId[,] typeIconTable = new IconId[,] {
			{Class,     PrivateClass,		Class,		ProtectedClass,     InternalClass,		ProtectedOrInternalClass, 		InternalAndProtectedClass},     // class
			{Enum,      PrivateEnum,		Enum,		ProtectedEnum,      InternalEnum,		ProtectedOrInternalEnum, 		InternalAndProtectedEnum},      // enum
			{Interface, PrivateInterface,	Interface,	ProtectedInterface, InternalInterface,	ProtectedOrInternalInterface,	InternalAndProtectedInterface}, // interface
			{Struct,    PrivateStruct,		Struct,   	ProtectedStruct,    InternalStruct,		ProtectedOrInternalStruct,		InternalAndProtectedStruct},    // struct
			{Delegate,  PrivateDelegate,	Delegate, 	ProtectedDelegate,  InternalDelegate,	ProtectedOrInternalDelegate,	InternalAndProtectedDelegate}   // delegate
		};
		static readonly IconId[] fieldIconTable = {
			AstStockIcons.Field, AstStockIcons.PrivateField, AstStockIcons.Field, AstStockIcons.ProtectedField, AstStockIcons.InternalField, AstStockIcons.ProtectedOrInternalField, AstStockIcons.InternalAndProtectedField
		};
		static readonly IconId[] methodIconTable = {
			AstStockIcons.Method, AstStockIcons.PrivateMethod, AstStockIcons.Method, AstStockIcons.ProtectedMethod, AstStockIcons.InternalMethod, AstStockIcons.ProtectedOrInternalMethod, AstStockIcons.InternalAndProtectedMethod
		};
		static readonly IconId[] propertyIconTable = {
			AstStockIcons.Property, AstStockIcons.PrivateProperty, AstStockIcons.Property, AstStockIcons.ProtectedProperty, AstStockIcons.InternalProperty, AstStockIcons.ProtectedOrInternalProperty, AstStockIcons.InternalAndProtectedProperty
		};
		static readonly IconId[] eventIconTable = {
			AstStockIcons.Event, AstStockIcons.PrivateEvent, AstStockIcons.Event, AstStockIcons.ProtectedEvent, AstStockIcons.InternalEvent, AstStockIcons.ProtectedOrInternalEvent, AstStockIcons.InternalAndProtectedEvent
		};

		static void AdjustAccessibility (SyntaxTokenList modifiers, ref Accessibility acc, ref bool result)
		{
			if (modifiers.Any (mod => mod.Kind () == Microsoft.CodeAnalysis.CSharp.SyntaxKind.ProtectedKeyword) &&
				modifiers.Any (mod => mod.Kind () == Microsoft.CodeAnalysis.CSharp.SyntaxKind.InternalKeyword)) {
				acc = Accessibility.ProtectedOrInternal;
				result = true;
				return;
			}

			foreach (var mod in modifiers) {
				if (mod.Kind () == Microsoft.CodeAnalysis.CSharp.SyntaxKind.PublicKeyword) {
					acc = Accessibility.Public;
					result = true;
					return;
				}
				if (mod.Kind () == Microsoft.CodeAnalysis.CSharp.SyntaxKind.PrivateKeyword) {
					acc = Accessibility.Private;
					result = true;
					return;
				}
					if (mod.Kind () == Microsoft.CodeAnalysis.CSharp.SyntaxKind.ProtectedKeyword) {
					acc = Accessibility.Protected;
					result = true;
					return;
				}
				if (mod.Kind () == Microsoft.CodeAnalysis.CSharp.SyntaxKind.InternalKeyword) {
					acc = Accessibility.Internal;
					result = true;
					return;
				}
			}
		}

		static bool GetAccessibility (SyntaxNode element, out Accessibility acc)
		{
			if (element.Parent is TypeDeclarationSyntax && element.Parent is InterfaceDeclarationSyntax) {
				acc = Accessibility.Public;
				return true;
			}
			bool result = false;
			acc = Accessibility.Private;
			if (element is TypeDeclarationSyntax && !(element.Parent is TypeDeclarationSyntax))
				acc = Accessibility.Internal;

			if (element is TypeDeclarationSyntax)
				AdjustAccessibility (((TypeDeclarationSyntax)element).Modifiers, ref acc, ref result);
			if (element is BaseFieldDeclarationSyntax)
				AdjustAccessibility (((BaseFieldDeclarationSyntax)element).Modifiers, ref acc, ref result);
			if (element is BasePropertyDeclarationSyntax)
				AdjustAccessibility (((BasePropertyDeclarationSyntax)element).Modifiers, ref acc, ref result);
			if (element is BaseMethodDeclarationSyntax)
				AdjustAccessibility (((BaseMethodDeclarationSyntax)element).Modifiers, ref acc, ref result);
			return result;
		}
		
		public static string GetStockIcon (this SyntaxNode element)
		{
			Accessibility acc = Accessibility.Public;

			if (element is NamespaceDeclarationSyntax)
				return Namespace;
			
			if (element is AccessorDeclarationSyntax) {
				if (!GetAccessibility ((MemberDeclarationSyntax)element, out acc))
					GetAccessibility (element.Parent as MemberDeclarationSyntax, out acc);

				return methodIconTable [(int) (acc)];
			}

			GetAccessibility (element, out acc);

			if (element is TypeDeclarationSyntax) {
				var type = element as TypeDeclarationSyntax;
				switch (type.Keyword.Kind ()) {
				case SyntaxKind.ClassKeyword:
					return typeIconTable [0, (int) (acc)];
				case SyntaxKind.StructKeyword:
					return typeIconTable [3, (int) (acc)];
				case SyntaxKind.InterfaceKeyword:
					return typeIconTable [2, (int) (acc)];
				case SyntaxKind.EnumKeyword:
					return typeIconTable [1, (int) (acc)];
				default:
					throw new ArgumentOutOfRangeException ();
				}
			}
			if (element is DelegateDeclarationSyntax)
				return typeIconTable [4, (int) (acc)];

			// member accessibility
			GetAccessibility (element, out acc);

			if (element is BaseMethodDeclarationSyntax) {
				// TODO!
				// var method = element as MethodDeclarationSyntax;
				//				if (method.ParameterList.Parameters.First ())
				//	return extensionMethodIconTable [(int) (acc)];
				return methodIconTable [(int) (acc)];
			}

			if (element is PropertyDeclarationSyntax || element is IndexerDeclarationSyntax)
				return propertyIconTable [(int) (acc)];
			if (element is EventDeclarationSyntax || element is EventFieldDeclarationSyntax)
				return eventIconTable [(int) (acc)];
			if (element.Parent is EnumDeclarationSyntax)
				acc = Accessibility.Public;
			return fieldIconTable [(int) (acc)];
		}

		internal static IconId GetStockIconForSymbolInfo (this DeclaredSymbolInfo symbol)
		{
			switch (symbol.Kind) {
				case DeclaredSymbolInfoKind.Class:
				return AstStockIcons.Class;
				case DeclaredSymbolInfoKind.Constant:
				return AstStockIcons.Field;
				case DeclaredSymbolInfoKind.Constructor:
				return AstStockIcons.Method;
				case DeclaredSymbolInfoKind.Delegate:
				return AstStockIcons.Delegate;
				case DeclaredSymbolInfoKind.Enum:
				return AstStockIcons.Enum;
				case DeclaredSymbolInfoKind.EnumMember:
				return AstStockIcons.Field;
				case DeclaredSymbolInfoKind.Event:
				return AstStockIcons.Event;
				case DeclaredSymbolInfoKind.Field:
				return AstStockIcons.Field;
				case DeclaredSymbolInfoKind.Indexer:
				return AstStockIcons.Method;
				case DeclaredSymbolInfoKind.Interface:
				return AstStockIcons.Interface;
				case DeclaredSymbolInfoKind.Method:
				return AstStockIcons.Method;
				case DeclaredSymbolInfoKind.Module:
				return AstStockIcons.Method;
				case DeclaredSymbolInfoKind.Property:
				return AstStockIcons.Property;
				case DeclaredSymbolInfoKind.Struct:
				return AstStockIcons.Struct;
				default:
					throw new ArgumentOutOfRangeException ();
			}
		}

	}

}
