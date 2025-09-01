using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that the attributed method is exposed by an unmanaged dynamic-link library (DLL) as a static entry point.</summary>
	// Token: 0x02000706 RID: 1798
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	public sealed class DllImportAttribute : Attribute
	{
		// Token: 0x0600409E RID: 16542 RVA: 0x000E123C File Offset: 0x000DF43C
		[SecurityCritical]
		internal static Attribute GetCustomAttribute(RuntimeMethodInfo method)
		{
			if ((method.Attributes & MethodAttributes.PinvokeImpl) == MethodAttributes.PrivateScope)
			{
				return null;
			}
			string dllName = null;
			int metadataToken = method.MetadataToken;
			PInvokeAttributes pinvokeAttributes = PInvokeAttributes.CharSetNotSpec;
			string entryPoint;
			method.GetPInvoke(out pinvokeAttributes, out entryPoint, out dllName);
			CharSet charSet = CharSet.None;
			switch (pinvokeAttributes & PInvokeAttributes.CharSetMask)
			{
			case PInvokeAttributes.CharSetNotSpec:
				charSet = CharSet.None;
				break;
			case PInvokeAttributes.CharSetAnsi:
				charSet = CharSet.Ansi;
				break;
			case PInvokeAttributes.CharSetUnicode:
				charSet = CharSet.Unicode;
				break;
			case PInvokeAttributes.CharSetMask:
				charSet = CharSet.Auto;
				break;
			}
			CallingConvention callingConvention = CallingConvention.Cdecl;
			PInvokeAttributes pinvokeAttributes2 = pinvokeAttributes & PInvokeAttributes.CallConvMask;
			if (pinvokeAttributes2 <= PInvokeAttributes.CallConvCdecl)
			{
				if (pinvokeAttributes2 != PInvokeAttributes.CallConvWinapi)
				{
					if (pinvokeAttributes2 == PInvokeAttributes.CallConvCdecl)
					{
						callingConvention = CallingConvention.Cdecl;
					}
				}
				else
				{
					callingConvention = CallingConvention.Winapi;
				}
			}
			else if (pinvokeAttributes2 != PInvokeAttributes.CallConvStdcall)
			{
				if (pinvokeAttributes2 != PInvokeAttributes.CallConvThiscall)
				{
					if (pinvokeAttributes2 == PInvokeAttributes.CallConvFastcall)
					{
						callingConvention = CallingConvention.FastCall;
					}
				}
				else
				{
					callingConvention = CallingConvention.ThisCall;
				}
			}
			else
			{
				callingConvention = CallingConvention.StdCall;
			}
			bool exactSpelling = (pinvokeAttributes & PInvokeAttributes.NoMangle) > PInvokeAttributes.CharSetNotSpec;
			bool setLastError = (pinvokeAttributes & PInvokeAttributes.SupportsLastError) > PInvokeAttributes.CharSetNotSpec;
			bool bestFitMapping = (pinvokeAttributes & PInvokeAttributes.BestFitMask) == PInvokeAttributes.BestFitEnabled;
			bool throwOnUnmappableChar = (pinvokeAttributes & PInvokeAttributes.ThrowOnUnmappableCharMask) == PInvokeAttributes.ThrowOnUnmappableCharEnabled;
			bool preserveSig = (method.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) > MethodImplAttributes.IL;
			return new DllImportAttribute(dllName, entryPoint, charSet, exactSpelling, setLastError, preserveSig, callingConvention, bestFitMapping, throwOnUnmappableChar);
		}

		// Token: 0x0600409F RID: 16543 RVA: 0x000E1357 File Offset: 0x000DF557
		internal static bool IsDefined(RuntimeMethodInfo method)
		{
			return (method.Attributes & MethodAttributes.PinvokeImpl) > MethodAttributes.PrivateScope;
		}

		// Token: 0x060040A0 RID: 16544 RVA: 0x000E1368 File Offset: 0x000DF568
		internal DllImportAttribute(string dllName, string entryPoint, CharSet charSet, bool exactSpelling, bool setLastError, bool preserveSig, CallingConvention callingConvention, bool bestFitMapping, bool throwOnUnmappableChar)
		{
			this._val = dllName;
			this.EntryPoint = entryPoint;
			this.CharSet = charSet;
			this.ExactSpelling = exactSpelling;
			this.SetLastError = setLastError;
			this.PreserveSig = preserveSig;
			this.CallingConvention = callingConvention;
			this.BestFitMapping = bestFitMapping;
			this.ThrowOnUnmappableChar = throwOnUnmappableChar;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.DllImportAttribute" /> class with the name of the DLL containing the method to import.</summary>
		/// <param name="dllName">The name of the DLL that contains the unmanaged method. This can include an assembly display name, if the DLL is included in an assembly.</param>
		// Token: 0x060040A1 RID: 16545 RVA: 0x000E13C0 File Offset: 0x000DF5C0
		public DllImportAttribute(string dllName)
		{
			this._val = dllName;
		}

		/// <summary>Gets the name of the DLL file that contains the entry point.</summary>
		/// <returns>The name of the DLL file that contains the entry point.</returns>
		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x060040A2 RID: 16546 RVA: 0x000E13CF File Offset: 0x000DF5CF
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002AD1 RID: 10961
		internal string _val;

		/// <summary>Indicates the name or ordinal of the DLL entry point to be called.</summary>
		// Token: 0x04002AD2 RID: 10962
		public string EntryPoint;

		/// <summary>Indicates how to marshal string parameters to the method and controls name mangling.</summary>
		// Token: 0x04002AD3 RID: 10963
		public CharSet CharSet;

		/// <summary>Indicates whether the callee calls the <see langword="SetLastError" /> Win32 API function before returning from the attributed method.</summary>
		// Token: 0x04002AD4 RID: 10964
		public bool SetLastError;

		/// <summary>Controls whether the <see cref="F:System.Runtime.InteropServices.DllImportAttribute.CharSet" /> field causes the common language runtime to search an unmanaged DLL for entry-point names other than the one specified.</summary>
		// Token: 0x04002AD5 RID: 10965
		public bool ExactSpelling;

		/// <summary>Indicates whether unmanaged methods that have <see langword="HRESULT" /> or <see langword="retval" /> return values are directly translated or whether <see langword="HRESULT" /> or <see langword="retval" /> return values are automatically converted to exceptions.</summary>
		// Token: 0x04002AD6 RID: 10966
		public bool PreserveSig;

		/// <summary>Indicates the calling convention of an entry point.</summary>
		// Token: 0x04002AD7 RID: 10967
		public CallingConvention CallingConvention;

		/// <summary>Enables or disables best-fit mapping behavior when converting Unicode characters to ANSI characters.</summary>
		// Token: 0x04002AD8 RID: 10968
		public bool BestFitMapping;

		/// <summary>Enables or disables the throwing of an exception on an unmappable Unicode character that is converted to an ANSI "?" character.</summary>
		// Token: 0x04002AD9 RID: 10969
		public bool ThrowOnUnmappableChar;
	}
}
