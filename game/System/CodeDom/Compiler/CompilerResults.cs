using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Policy;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents the results of compilation that are returned from a compiler.</summary>
	// Token: 0x0200034D RID: 845
	[Serializable]
	public class CompilerResults
	{
		/// <summary>Indicates the evidence object that represents the security policy permissions of the compiled assembly.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.Evidence" /> object that represents the security policy permissions of the compiled assembly.</returns>
		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001BFB RID: 7163 RVA: 0x00066768 File Offset: 0x00064968
		// (set) Token: 0x06001BFC RID: 7164 RVA: 0x0006677B File Offset: 0x0006497B
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

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerResults" /> class that uses the specified temporary files.</summary>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation.</param>
		// Token: 0x06001BFD RID: 7165 RVA: 0x0006678F File Offset: 0x0006498F
		public CompilerResults(TempFileCollection tempFiles)
		{
			this._tempFiles = tempFiles;
		}

		/// <summary>Gets or sets the temporary file collection to use.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation.</returns>
		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001BFE RID: 7166 RVA: 0x000667B4 File Offset: 0x000649B4
		// (set) Token: 0x06001BFF RID: 7167 RVA: 0x000667BC File Offset: 0x000649BC
		public TempFileCollection TempFiles
		{
			get
			{
				return this._tempFiles;
			}
			set
			{
				this._tempFiles = value;
			}
		}

		/// <summary>Gets or sets the compiled assembly.</summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> that indicates the compiled assembly.</returns>
		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001C00 RID: 7168 RVA: 0x000667C5 File Offset: 0x000649C5
		// (set) Token: 0x06001C01 RID: 7169 RVA: 0x000667FF File Offset: 0x000649FF
		public Assembly CompiledAssembly
		{
			get
			{
				if (this._compiledAssembly == null && this.PathToAssembly != null)
				{
					this._compiledAssembly = Assembly.Load(new AssemblyName
					{
						CodeBase = this.PathToAssembly
					});
				}
				return this._compiledAssembly;
			}
			set
			{
				this._compiledAssembly = value;
			}
		}

		/// <summary>Gets the collection of compiler errors and warnings.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerErrorCollection" /> that indicates the errors and warnings resulting from compilation, if any.</returns>
		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001C02 RID: 7170 RVA: 0x00066808 File Offset: 0x00064A08
		public CompilerErrorCollection Errors
		{
			get
			{
				return this._errors;
			}
		}

		/// <summary>Gets the compiler output messages.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> that contains the output messages.</returns>
		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001C03 RID: 7171 RVA: 0x00066810 File Offset: 0x00064A10
		public StringCollection Output
		{
			get
			{
				return this._output;
			}
		}

		/// <summary>Gets or sets the path of the compiled assembly.</summary>
		/// <returns>The path of the assembly, or <see langword="null" /> if the assembly was generated in memory.</returns>
		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001C04 RID: 7172 RVA: 0x00066818 File Offset: 0x00064A18
		// (set) Token: 0x06001C05 RID: 7173 RVA: 0x00066820 File Offset: 0x00064A20
		public string PathToAssembly
		{
			[CompilerGenerated]
			get
			{
				return this.<PathToAssembly>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PathToAssembly>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the compiler's return value.</summary>
		/// <returns>The compiler's return value.</returns>
		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001C06 RID: 7174 RVA: 0x00066829 File Offset: 0x00064A29
		// (set) Token: 0x06001C07 RID: 7175 RVA: 0x00066831 File Offset: 0x00064A31
		public int NativeCompilerReturnValue
		{
			[CompilerGenerated]
			get
			{
				return this.<NativeCompilerReturnValue>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<NativeCompilerReturnValue>k__BackingField = value;
			}
		}

		// Token: 0x04000E45 RID: 3653
		private Evidence _evidence;

		// Token: 0x04000E46 RID: 3654
		private readonly CompilerErrorCollection _errors = new CompilerErrorCollection();

		// Token: 0x04000E47 RID: 3655
		private readonly StringCollection _output = new StringCollection();

		// Token: 0x04000E48 RID: 3656
		private Assembly _compiledAssembly;

		// Token: 0x04000E49 RID: 3657
		private TempFileCollection _tempFiles;

		// Token: 0x04000E4A RID: 3658
		[CompilerGenerated]
		private string <PathToAssembly>k__BackingField;

		// Token: 0x04000E4B RID: 3659
		[CompilerGenerated]
		private int <NativeCompilerReturnValue>k__BackingField;
	}
}
