﻿// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="David Srbecký" email="dsrbecky@gmail.com"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Collections.Generic;
using Debugger.Wrappers.CorDebug;
using Debugger.Wrappers.MetaData;

namespace Debugger
{
	/// <summary>
	/// Provides information about a field of some class.
	/// </summary>
	public class FieldInfo: MemberInfo
	{
		FieldProps fieldProps;
		
		/// <summary> Gets a value indicating whether this field is literal field </summary>
		public bool IsLiteral {
			get {
				return fieldProps.IsLiteral;
			}
		}
		
		/// <summary> Gets a value indicating whether this field is private </summary>
		public override bool IsPrivate {
			get {
				return !fieldProps.IsPublic;
			}
		}
		
		/// <summary> Gets a value indicating whether this field is public </summary>
		public override bool IsPublic {
			get {
				return fieldProps.IsPublic;
			}
		}
		
		/// <summary> Gets a value indicating whether this field is static </summary>
		public override bool IsStatic {
			get {
				return fieldProps.IsStatic;
			}
		}
		
		/// <summary> Gets the metadata token associated with this field </summary>
		[Debugger.Tests.Ignore]
		public override uint MetadataToken {
			get {
				return fieldProps.Token;
			}
		}
		
		/// <summary> Gets the name of this field </summary>
		public override string Name {
			get {
				return fieldProps.Name;
			}
		}
		
		internal FieldInfo(DebugType declaringType, FieldProps fieldProps):base (declaringType)
		{
			this.fieldProps = fieldProps;
		}
	}
}
