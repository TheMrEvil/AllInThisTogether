using System;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Security.Policy;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents the parameters used to invoke a compiler.</summary>
	// Token: 0x0200034C RID: 844
	[Serializable]
	public class CompilerParameters
	{
		/// <summary>Specifies an evidence object that represents the security policy permissions to grant the compiled assembly.</summary>
		/// <returns>An  object that represents the security policy permissions to grant the compiled assembly.</returns>
		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001BDA RID: 7130 RVA: 0x000665BA File Offset: 0x000647BA
		// (set) Token: 0x06001BDB RID: 7131 RVA: 0x000665CD File Offset: 0x000647CD
		[Obsolete("CAS policy is obsolete and will be removed in a future release of the .NET Framework. Please see http://go2.microsoft.com/fwlink/?LinkId=131738 for more information.")]
		public Evidence Evidence
		{
			get
			{
				Evidence evidence = this._evidence;
				if (evidence == null)
				{
					return null;
				}
				return evidence.Clone();
			}
			set
			{
				this._evidence = ((value != null) ? value.Clone() : null);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> class.</summary>
		// Token: 0x06001BDC RID: 7132 RVA: 0x000665E1 File Offset: 0x000647E1
		public CompilerParameters() : this(null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> class using the specified assembly names.</summary>
		/// <param name="assemblyNames">The names of the assemblies to reference.</param>
		// Token: 0x06001BDD RID: 7133 RVA: 0x000665EB File Offset: 0x000647EB
		public CompilerParameters(string[] assemblyNames) : this(assemblyNames, null, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> class using the specified assembly names and output file name.</summary>
		/// <param name="assemblyNames">The names of the assemblies to reference.</param>
		/// <param name="outputName">The output file name.</param>
		// Token: 0x06001BDE RID: 7134 RVA: 0x000665F6 File Offset: 0x000647F6
		public CompilerParameters(string[] assemblyNames, string outputName) : this(assemblyNames, outputName, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> class using the specified assembly names, output name, and a value indicating whether to include debug information.</summary>
		/// <param name="assemblyNames">The names of the assemblies to reference.</param>
		/// <param name="outputName">The output file name.</param>
		/// <param name="includeDebugInformation">
		///   <see langword="true" /> to include debug information; <see langword="false" /> to exclude debug information.</param>
		// Token: 0x06001BDF RID: 7135 RVA: 0x00066604 File Offset: 0x00064804
		public CompilerParameters(string[] assemblyNames, string outputName, bool includeDebugInformation)
		{
			if (assemblyNames != null)
			{
				this.ReferencedAssemblies.AddRange(assemblyNames);
			}
			this.OutputAssembly = outputName;
			this.IncludeDebugInformation = includeDebugInformation;
		}

		/// <summary>Gets or sets the name of the core or standard assembly that contains basic types such as <see cref="T:System.Object" />, <see cref="T:System.String" />, or <see cref="T:System.Int32" />.</summary>
		/// <returns>The name of the core assembly that contains basic types.</returns>
		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x00066667 File Offset: 0x00064867
		// (set) Token: 0x06001BE1 RID: 7137 RVA: 0x0006666F File Offset: 0x0006486F
		public string CoreAssemblyFileName
		{
			[CompilerGenerated]
			get
			{
				return this.<CoreAssemblyFileName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CoreAssemblyFileName>k__BackingField = value;
			}
		} = string.Empty;

		/// <summary>Gets or sets a value indicating whether to generate an executable.</summary>
		/// <returns>
		///   <see langword="true" /> if an executable should be generated; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x00066678 File Offset: 0x00064878
		// (set) Token: 0x06001BE3 RID: 7139 RVA: 0x00066680 File Offset: 0x00064880
		public bool GenerateExecutable
		{
			[CompilerGenerated]
			get
			{
				return this.<GenerateExecutable>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<GenerateExecutable>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to generate the output in memory.</summary>
		/// <returns>
		///   <see langword="true" /> if the compiler should generate the output in memory; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x00066689 File Offset: 0x00064889
		// (set) Token: 0x06001BE5 RID: 7141 RVA: 0x00066691 File Offset: 0x00064891
		public bool GenerateInMemory
		{
			[CompilerGenerated]
			get
			{
				return this.<GenerateInMemory>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<GenerateInMemory>k__BackingField = value;
			}
		}

		/// <summary>Gets the assemblies referenced by the current project.</summary>
		/// <returns>A collection that contains the assembly names that are referenced by the source to compile.</returns>
		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001BE6 RID: 7142 RVA: 0x0006669A File Offset: 0x0006489A
		public StringCollection ReferencedAssemblies
		{
			get
			{
				return this._assemblyNames;
			}
		}

		/// <summary>Gets or sets the name of the main class.</summary>
		/// <returns>The name of the main class.</returns>
		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001BE7 RID: 7143 RVA: 0x000666A2 File Offset: 0x000648A2
		// (set) Token: 0x06001BE8 RID: 7144 RVA: 0x000666AA File Offset: 0x000648AA
		public string MainClass
		{
			[CompilerGenerated]
			get
			{
				return this.<MainClass>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MainClass>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the name of the output assembly.</summary>
		/// <returns>The name of the output assembly.</returns>
		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001BE9 RID: 7145 RVA: 0x000666B3 File Offset: 0x000648B3
		// (set) Token: 0x06001BEA RID: 7146 RVA: 0x000666BB File Offset: 0x000648BB
		public string OutputAssembly
		{
			[CompilerGenerated]
			get
			{
				return this.<OutputAssembly>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<OutputAssembly>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the collection that contains the temporary files.</summary>
		/// <returns>A collection that contains the temporary files.</returns>
		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001BEB RID: 7147 RVA: 0x000666C4 File Offset: 0x000648C4
		// (set) Token: 0x06001BEC RID: 7148 RVA: 0x000666E9 File Offset: 0x000648E9
		public TempFileCollection TempFiles
		{
			get
			{
				TempFileCollection result;
				if ((result = this._tempFiles) == null)
				{
					result = (this._tempFiles = new TempFileCollection());
				}
				return result;
			}
			set
			{
				this._tempFiles = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to include debug information in the compiled executable.</summary>
		/// <returns>
		///   <see langword="true" /> if debug information should be generated; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001BED RID: 7149 RVA: 0x000666F2 File Offset: 0x000648F2
		// (set) Token: 0x06001BEE RID: 7150 RVA: 0x000666FA File Offset: 0x000648FA
		public bool IncludeDebugInformation
		{
			[CompilerGenerated]
			get
			{
				return this.<IncludeDebugInformation>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IncludeDebugInformation>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to treat warnings as errors.</summary>
		/// <returns>
		///   <see langword="true" /> if warnings should be treated as errors; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001BEF RID: 7151 RVA: 0x00066703 File Offset: 0x00064903
		// (set) Token: 0x06001BF0 RID: 7152 RVA: 0x0006670B File Offset: 0x0006490B
		public bool TreatWarningsAsErrors
		{
			[CompilerGenerated]
			get
			{
				return this.<TreatWarningsAsErrors>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TreatWarningsAsErrors>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the warning level at which the compiler aborts compilation.</summary>
		/// <returns>The warning level at which the compiler aborts compilation.</returns>
		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001BF1 RID: 7153 RVA: 0x00066714 File Offset: 0x00064914
		// (set) Token: 0x06001BF2 RID: 7154 RVA: 0x0006671C File Offset: 0x0006491C
		public int WarningLevel
		{
			[CompilerGenerated]
			get
			{
				return this.<WarningLevel>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<WarningLevel>k__BackingField = value;
			}
		} = -1;

		/// <summary>Gets or sets optional command-line arguments to use when invoking the compiler.</summary>
		/// <returns>Any additional command-line arguments for the compiler.</returns>
		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001BF3 RID: 7155 RVA: 0x00066725 File Offset: 0x00064925
		// (set) Token: 0x06001BF4 RID: 7156 RVA: 0x0006672D File Offset: 0x0006492D
		public string CompilerOptions
		{
			[CompilerGenerated]
			get
			{
				return this.<CompilerOptions>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CompilerOptions>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the file name of a Win32 resource file to link into the compiled assembly.</summary>
		/// <returns>A Win32 resource file that will be linked into the compiled assembly.</returns>
		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001BF5 RID: 7157 RVA: 0x00066736 File Offset: 0x00064936
		// (set) Token: 0x06001BF6 RID: 7158 RVA: 0x0006673E File Offset: 0x0006493E
		public string Win32Resource
		{
			[CompilerGenerated]
			get
			{
				return this.<Win32Resource>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Win32Resource>k__BackingField = value;
			}
		}

		/// <summary>Gets the .NET Framework resource files to include when compiling the assembly output.</summary>
		/// <returns>A collection that contains the file paths of .NET Framework resources to include in the generated assembly.</returns>
		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x00066747 File Offset: 0x00064947
		public StringCollection EmbeddedResources
		{
			get
			{
				return this._embeddedResources;
			}
		}

		/// <summary>Gets the .NET Framework resource files that are referenced in the current source.</summary>
		/// <returns>A collection that contains the file paths of .NET Framework resources that are referenced by the source.</returns>
		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001BF8 RID: 7160 RVA: 0x0006674F File Offset: 0x0006494F
		public StringCollection LinkedResources
		{
			get
			{
				return this._linkedResources;
			}
		}

		/// <summary>Gets or sets the user token to use when creating the compiler process.</summary>
		/// <returns>The user token to use.</returns>
		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x00066757 File Offset: 0x00064957
		// (set) Token: 0x06001BFA RID: 7162 RVA: 0x0006675F File Offset: 0x0006495F
		public IntPtr UserToken
		{
			[CompilerGenerated]
			get
			{
				return this.<UserToken>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UserToken>k__BackingField = value;
			}
		}

		// Token: 0x04000E35 RID: 3637
		private Evidence _evidence;

		// Token: 0x04000E36 RID: 3638
		private readonly StringCollection _assemblyNames = new StringCollection();

		// Token: 0x04000E37 RID: 3639
		private readonly StringCollection _embeddedResources = new StringCollection();

		// Token: 0x04000E38 RID: 3640
		private readonly StringCollection _linkedResources = new StringCollection();

		// Token: 0x04000E39 RID: 3641
		private TempFileCollection _tempFiles;

		// Token: 0x04000E3A RID: 3642
		[CompilerGenerated]
		private string <CoreAssemblyFileName>k__BackingField;

		// Token: 0x04000E3B RID: 3643
		[CompilerGenerated]
		private bool <GenerateExecutable>k__BackingField;

		// Token: 0x04000E3C RID: 3644
		[CompilerGenerated]
		private bool <GenerateInMemory>k__BackingField;

		// Token: 0x04000E3D RID: 3645
		[CompilerGenerated]
		private string <MainClass>k__BackingField;

		// Token: 0x04000E3E RID: 3646
		[CompilerGenerated]
		private string <OutputAssembly>k__BackingField;

		// Token: 0x04000E3F RID: 3647
		[CompilerGenerated]
		private bool <IncludeDebugInformation>k__BackingField;

		// Token: 0x04000E40 RID: 3648
		[CompilerGenerated]
		private bool <TreatWarningsAsErrors>k__BackingField;

		// Token: 0x04000E41 RID: 3649
		[CompilerGenerated]
		private int <WarningLevel>k__BackingField;

		// Token: 0x04000E42 RID: 3650
		[CompilerGenerated]
		private string <CompilerOptions>k__BackingField;

		// Token: 0x04000E43 RID: 3651
		[CompilerGenerated]
		private string <Win32Resource>k__BackingField;

		// Token: 0x04000E44 RID: 3652
		[CompilerGenerated]
		private IntPtr <UserToken>k__BackingField;
	}
}
