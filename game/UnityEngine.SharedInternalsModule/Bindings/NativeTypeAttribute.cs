using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Bindings
{
	// Token: 0x0200001C RID: 28
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum)]
	[VisibleToOtherModules]
	internal class NativeTypeAttribute : Attribute, IBindingsHeaderProviderAttribute, IBindingsAttribute, IBindingsGenerateMarshallingTypeAttribute
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000054 RID: 84 RVA: 0x0000240F File Offset: 0x0000060F
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002417 File Offset: 0x00000617
		public string Header
		{
			[CompilerGenerated]
			get
			{
				return this.<Header>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Header>k__BackingField = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002420 File Offset: 0x00000620
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002428 File Offset: 0x00000628
		public string IntermediateScriptingStructName
		{
			[CompilerGenerated]
			get
			{
				return this.<IntermediateScriptingStructName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IntermediateScriptingStructName>k__BackingField = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002431 File Offset: 0x00000631
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002439 File Offset: 0x00000639
		public CodegenOptions CodegenOptions
		{
			[CompilerGenerated]
			get
			{
				return this.<CodegenOptions>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CodegenOptions>k__BackingField = value;
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002442 File Offset: 0x00000642
		public NativeTypeAttribute()
		{
			this.CodegenOptions = CodegenOptions.Auto;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002454 File Offset: 0x00000654
		public NativeTypeAttribute(CodegenOptions codegenOptions)
		{
			this.CodegenOptions = codegenOptions;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002468 File Offset: 0x00000668
		public NativeTypeAttribute(string header)
		{
			bool flag = header == null;
			if (flag)
			{
				throw new ArgumentNullException("header");
			}
			bool flag2 = header == "";
			if (flag2)
			{
				throw new ArgumentException("header cannot be empty", "header");
			}
			this.CodegenOptions = CodegenOptions.Auto;
			this.Header = header;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000024BF File Offset: 0x000006BF
		public NativeTypeAttribute(string header, CodegenOptions codegenOptions) : this(header)
		{
			this.CodegenOptions = codegenOptions;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000024D2 File Offset: 0x000006D2
		public NativeTypeAttribute(CodegenOptions codegenOptions, string intermediateStructName) : this(codegenOptions)
		{
			this.IntermediateScriptingStructName = intermediateStructName;
		}

		// Token: 0x0400001A RID: 26
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string <Header>k__BackingField;

		// Token: 0x0400001B RID: 27
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string <IntermediateScriptingStructName>k__BackingField;

		// Token: 0x0400001C RID: 28
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private CodegenOptions <CodegenOptions>k__BackingField;
	}
}
