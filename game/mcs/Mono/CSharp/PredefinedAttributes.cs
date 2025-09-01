using System;
using System.Reflection;

namespace Mono.CSharp
{
	// Token: 0x02000124 RID: 292
	public class PredefinedAttributes
	{
		// Token: 0x06000E71 RID: 3697 RVA: 0x00036E04 File Offset: 0x00035004
		public PredefinedAttributes(ModuleContainer module)
		{
			this.ParamArray = new PredefinedAttribute(module, "System", "ParamArrayAttribute");
			this.Out = new PredefinedAttribute(module, "System.Runtime.InteropServices", "OutAttribute");
			this.ParamArray.Resolve();
			this.Out.Resolve();
			this.Obsolete = new PredefinedAttribute(module, "System", "ObsoleteAttribute");
			this.DllImport = new PredefinedAttribute(module, "System.Runtime.InteropServices", "DllImportAttribute");
			this.MethodImpl = new PredefinedAttribute(module, "System.Runtime.CompilerServices", "MethodImplAttribute");
			this.MarshalAs = new PredefinedAttribute(module, "System.Runtime.InteropServices", "MarshalAsAttribute");
			this.In = new PredefinedAttribute(module, "System.Runtime.InteropServices", "InAttribute");
			this.IndexerName = new PredefinedAttribute(module, "System.Runtime.CompilerServices", "IndexerNameAttribute");
			this.Conditional = new PredefinedAttribute(module, "System.Diagnostics", "ConditionalAttribute");
			this.CLSCompliant = new PredefinedAttribute(module, "System", "CLSCompliantAttribute");
			this.Security = new PredefinedAttribute(module, "System.Security.Permissions", "SecurityAttribute");
			this.Required = new PredefinedAttribute(module, "System.Runtime.CompilerServices", "RequiredAttributeAttribute");
			this.Guid = new PredefinedAttribute(module, "System.Runtime.InteropServices", "GuidAttribute");
			this.AssemblyCulture = new PredefinedAttribute(module, "System.Reflection", "AssemblyCultureAttribute");
			this.AssemblyVersion = new PredefinedAttribute(module, "System.Reflection", "AssemblyVersionAttribute");
			this.AssemblyAlgorithmId = new PredefinedAttribute(module, "System.Reflection", "AssemblyAlgorithmIdAttribute");
			this.AssemblyFlags = new PredefinedAttribute(module, "System.Reflection", "AssemblyFlagsAttribute");
			this.AssemblyFileVersion = new PredefinedAttribute(module, "System.Reflection", "AssemblyFileVersionAttribute");
			this.ComImport = new PredefinedAttribute(module, "System.Runtime.InteropServices", "ComImportAttribute");
			this.CoClass = new PredefinedAttribute(module, "System.Runtime.InteropServices", "CoClassAttribute");
			this.AttributeUsage = new PredefinedAttribute(module, "System", "AttributeUsageAttribute");
			this.DefaultParameterValue = new PredefinedAttribute(module, "System.Runtime.InteropServices", "DefaultParameterValueAttribute");
			this.OptionalParameter = new PredefinedAttribute(module, "System.Runtime.InteropServices", "OptionalAttribute");
			this.UnverifiableCode = new PredefinedAttribute(module, "System.Security", "UnverifiableCodeAttribute");
			this.HostProtection = new PredefinedAttribute(module, "System.Security.Permissions", "HostProtectionAttribute");
			this.DefaultCharset = new PredefinedAttribute(module, "System.Runtime.InteropServices", "DefaultCharSetAttribute");
			this.TypeForwarder = new PredefinedAttribute(module, "System.Runtime.CompilerServices", "TypeForwardedToAttribute");
			this.FixedBuffer = new PredefinedAttribute(module, "System.Runtime.CompilerServices", "FixedBufferAttribute");
			this.CompilerGenerated = new PredefinedAttribute(module, "System.Runtime.CompilerServices", "CompilerGeneratedAttribute");
			this.InternalsVisibleTo = new PredefinedAttribute(module, "System.Runtime.CompilerServices", "InternalsVisibleToAttribute");
			this.RuntimeCompatibility = new PredefinedAttribute(module, "System.Runtime.CompilerServices", "RuntimeCompatibilityAttribute");
			this.DebuggerHidden = new PredefinedAttribute(module, "System.Diagnostics", "DebuggerHiddenAttribute");
			this.UnsafeValueType = new PredefinedAttribute(module, "System.Runtime.CompilerServices", "UnsafeValueTypeAttribute");
			this.UnmanagedFunctionPointer = new PredefinedAttribute(module, "System.Runtime.InteropServices", "UnmanagedFunctionPointerAttribute");
			this.DebuggerBrowsable = new PredefinedDebuggerBrowsableAttribute(module, "System.Diagnostics", "DebuggerBrowsableAttribute");
			this.DebuggerStepThrough = new PredefinedAttribute(module, "System.Diagnostics", "DebuggerStepThroughAttribute");
			this.Debuggable = new PredefinedDebuggableAttribute(module, "System.Diagnostics", "DebuggableAttribute");
			this.Extension = new PredefinedAttribute(module, "System.Runtime.CompilerServices", "ExtensionAttribute");
			this.Dynamic = new PredefinedDynamicAttribute(module, "System.Runtime.CompilerServices", "DynamicAttribute");
			this.DefaultMember = new PredefinedAttribute(module, "System.Reflection", "DefaultMemberAttribute");
			this.DecimalConstant = new PredefinedDecimalAttribute(module, "System.Runtime.CompilerServices", "DecimalConstantAttribute");
			this.StructLayout = new PredefinedAttribute(module, "System.Runtime.InteropServices", "StructLayoutAttribute");
			this.FieldOffset = new PredefinedAttribute(module, "System.Runtime.InteropServices", "FieldOffsetAttribute");
			this.AssemblyProduct = new PredefinedAttribute(module, "System.Reflection", "AssemblyProductAttribute");
			this.AssemblyCompany = new PredefinedAttribute(module, "System.Reflection", "AssemblyCompanyAttribute");
			this.AssemblyCopyright = new PredefinedAttribute(module, "System.Reflection", "AssemblyCopyrightAttribute");
			this.AssemblyTrademark = new PredefinedAttribute(module, "System.Reflection", "AssemblyTrademarkAttribute");
			this.AsyncStateMachine = new PredefinedStateMachineAttribute(module, "System.Runtime.CompilerServices", "AsyncStateMachineAttribute");
			this.CallerMemberNameAttribute = new PredefinedAttribute(module, "System.Runtime.CompilerServices", "CallerMemberNameAttribute");
			this.CallerLineNumberAttribute = new PredefinedAttribute(module, "System.Runtime.CompilerServices", "CallerLineNumberAttribute");
			this.CallerFilePathAttribute = new PredefinedAttribute(module, "System.Runtime.CompilerServices", "CallerFilePathAttribute");
			FieldInfo[] fields = base.GetType().GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < fields.Length; i++)
			{
				((PredefinedAttribute)fields[i].GetValue(this)).Define();
			}
		}

		// Token: 0x04000696 RID: 1686
		public readonly PredefinedAttribute ParamArray;

		// Token: 0x04000697 RID: 1687
		public readonly PredefinedAttribute Out;

		// Token: 0x04000698 RID: 1688
		public readonly PredefinedAttribute Obsolete;

		// Token: 0x04000699 RID: 1689
		public readonly PredefinedAttribute DllImport;

		// Token: 0x0400069A RID: 1690
		public readonly PredefinedAttribute MethodImpl;

		// Token: 0x0400069B RID: 1691
		public readonly PredefinedAttribute MarshalAs;

		// Token: 0x0400069C RID: 1692
		public readonly PredefinedAttribute In;

		// Token: 0x0400069D RID: 1693
		public readonly PredefinedAttribute IndexerName;

		// Token: 0x0400069E RID: 1694
		public readonly PredefinedAttribute Conditional;

		// Token: 0x0400069F RID: 1695
		public readonly PredefinedAttribute CLSCompliant;

		// Token: 0x040006A0 RID: 1696
		public readonly PredefinedAttribute Security;

		// Token: 0x040006A1 RID: 1697
		public readonly PredefinedAttribute Required;

		// Token: 0x040006A2 RID: 1698
		public readonly PredefinedAttribute Guid;

		// Token: 0x040006A3 RID: 1699
		public readonly PredefinedAttribute AssemblyCulture;

		// Token: 0x040006A4 RID: 1700
		public readonly PredefinedAttribute AssemblyVersion;

		// Token: 0x040006A5 RID: 1701
		public readonly PredefinedAttribute AssemblyAlgorithmId;

		// Token: 0x040006A6 RID: 1702
		public readonly PredefinedAttribute AssemblyFlags;

		// Token: 0x040006A7 RID: 1703
		public readonly PredefinedAttribute AssemblyFileVersion;

		// Token: 0x040006A8 RID: 1704
		public readonly PredefinedAttribute ComImport;

		// Token: 0x040006A9 RID: 1705
		public readonly PredefinedAttribute CoClass;

		// Token: 0x040006AA RID: 1706
		public readonly PredefinedAttribute AttributeUsage;

		// Token: 0x040006AB RID: 1707
		public readonly PredefinedAttribute DefaultParameterValue;

		// Token: 0x040006AC RID: 1708
		public readonly PredefinedAttribute OptionalParameter;

		// Token: 0x040006AD RID: 1709
		public readonly PredefinedAttribute UnverifiableCode;

		// Token: 0x040006AE RID: 1710
		public readonly PredefinedAttribute DefaultCharset;

		// Token: 0x040006AF RID: 1711
		public readonly PredefinedAttribute TypeForwarder;

		// Token: 0x040006B0 RID: 1712
		public readonly PredefinedAttribute FixedBuffer;

		// Token: 0x040006B1 RID: 1713
		public readonly PredefinedAttribute CompilerGenerated;

		// Token: 0x040006B2 RID: 1714
		public readonly PredefinedAttribute InternalsVisibleTo;

		// Token: 0x040006B3 RID: 1715
		public readonly PredefinedAttribute RuntimeCompatibility;

		// Token: 0x040006B4 RID: 1716
		public readonly PredefinedAttribute DebuggerHidden;

		// Token: 0x040006B5 RID: 1717
		public readonly PredefinedAttribute UnsafeValueType;

		// Token: 0x040006B6 RID: 1718
		public readonly PredefinedAttribute UnmanagedFunctionPointer;

		// Token: 0x040006B7 RID: 1719
		public readonly PredefinedDebuggerBrowsableAttribute DebuggerBrowsable;

		// Token: 0x040006B8 RID: 1720
		public readonly PredefinedAttribute DebuggerStepThrough;

		// Token: 0x040006B9 RID: 1721
		public readonly PredefinedDebuggableAttribute Debuggable;

		// Token: 0x040006BA RID: 1722
		public readonly PredefinedAttribute HostProtection;

		// Token: 0x040006BB RID: 1723
		public readonly PredefinedAttribute Extension;

		// Token: 0x040006BC RID: 1724
		public readonly PredefinedDynamicAttribute Dynamic;

		// Token: 0x040006BD RID: 1725
		public readonly PredefinedStateMachineAttribute AsyncStateMachine;

		// Token: 0x040006BE RID: 1726
		public readonly PredefinedAttribute DefaultMember;

		// Token: 0x040006BF RID: 1727
		public readonly PredefinedDecimalAttribute DecimalConstant;

		// Token: 0x040006C0 RID: 1728
		public readonly PredefinedAttribute StructLayout;

		// Token: 0x040006C1 RID: 1729
		public readonly PredefinedAttribute FieldOffset;

		// Token: 0x040006C2 RID: 1730
		public readonly PredefinedAttribute AssemblyProduct;

		// Token: 0x040006C3 RID: 1731
		public readonly PredefinedAttribute AssemblyCompany;

		// Token: 0x040006C4 RID: 1732
		public readonly PredefinedAttribute AssemblyCopyright;

		// Token: 0x040006C5 RID: 1733
		public readonly PredefinedAttribute AssemblyTrademark;

		// Token: 0x040006C6 RID: 1734
		public readonly PredefinedAttribute CallerMemberNameAttribute;

		// Token: 0x040006C7 RID: 1735
		public readonly PredefinedAttribute CallerLineNumberAttribute;

		// Token: 0x040006C8 RID: 1736
		public readonly PredefinedAttribute CallerFilePathAttribute;
	}
}
