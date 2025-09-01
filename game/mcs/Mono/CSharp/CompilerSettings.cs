using System;
using System.Collections.Generic;
using System.Text;

namespace Mono.CSharp
{
	// Token: 0x02000295 RID: 661
	public class CompilerSettings
	{
		// Token: 0x06001FCC RID: 8140 RVA: 0x0009BEF4 File Offset: 0x0009A0F4
		public CompilerSettings()
		{
			this.StdLib = true;
			this.Target = Target.Exe;
			this.TargetExt = ".exe";
			this.Platform = Platform.AnyCPU;
			this.Version = LanguageVersion.V_6;
			this.VerifyClsCompliance = true;
			this.Encoding = Encoding.UTF8;
			this.LoadDefaultReferences = true;
			this.StdLibRuntimeVersion = RuntimeVersion.v4;
			this.WarningLevel = 4;
			this.TabSize = 1;
			this.AssemblyReferences = new List<string>();
			this.AssemblyReferencesAliases = new List<Tuple<string, string>>();
			this.Modules = new List<string>();
			this.ReferencesLookupPaths = new List<string>();
			this.conditional_symbols = new List<string>();
			this.conditional_symbols.Add("__MonoCS__");
			this.source_files = new List<SourceFile>();
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001FCD RID: 8141 RVA: 0x0009BFAE File Offset: 0x0009A1AE
		public SourceFile FirstSourceFile
		{
			get
			{
				if (this.source_files.Count <= 0)
				{
					return null;
				}
				return this.source_files[0];
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001FCE RID: 8142 RVA: 0x0009BFCC File Offset: 0x0009A1CC
		public bool HasKeyFileOrContainer
		{
			get
			{
				return this.StrongNameKeyFile != null || this.StrongNameKeyContainer != null;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001FCF RID: 8143 RVA: 0x0009BFE1 File Offset: 0x0009A1E1
		public bool NeedsEntryPoint
		{
			get
			{
				return this.Target == Target.Exe || this.Target == Target.WinExe;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001FD0 RID: 8144 RVA: 0x0009BFF7 File Offset: 0x0009A1F7
		public List<SourceFile> SourceFiles
		{
			get
			{
				return this.source_files;
			}
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x0009BFFF File Offset: 0x0009A1FF
		public void AddConditionalSymbol(string symbol)
		{
			if (!this.conditional_symbols.Contains(symbol))
			{
				this.conditional_symbols.Add(symbol);
			}
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x0009C01B File Offset: 0x0009A21B
		public void AddWarningAsError(int id)
		{
			if (this.warnings_as_error == null)
			{
				this.warnings_as_error = new List<int>();
			}
			this.warnings_as_error.Add(id);
		}

		// Token: 0x06001FD3 RID: 8147 RVA: 0x0009C03C File Offset: 0x0009A23C
		public void AddWarningOnly(int id)
		{
			if (this.warnings_only == null)
			{
				this.warnings_only = new List<int>();
			}
			this.warnings_only.Add(id);
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x0009C05D File Offset: 0x0009A25D
		public bool IsConditionalSymbolDefined(string symbol)
		{
			return this.conditional_symbols.Contains(symbol);
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x0009C06C File Offset: 0x0009A26C
		public bool IsWarningAsError(int code)
		{
			bool flag = this.WarningsAreErrors;
			if (this.warnings_as_error != null)
			{
				flag |= this.warnings_as_error.Contains(code);
			}
			if (this.warnings_only != null && this.warnings_only.Contains(code))
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001FD6 RID: 8150 RVA: 0x0009C0B0 File Offset: 0x0009A2B0
		public bool IsWarningEnabled(int code, int level)
		{
			return this.WarningLevel >= level && !this.IsWarningDisabledGlobally(code);
		}

		// Token: 0x06001FD7 RID: 8151 RVA: 0x0009C0C7 File Offset: 0x0009A2C7
		public bool IsWarningDisabledGlobally(int code)
		{
			return this.warning_ignore_table != null && this.warning_ignore_table.Contains(code);
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x0009C0DF File Offset: 0x0009A2DF
		public void SetIgnoreWarning(int code)
		{
			if (this.warning_ignore_table == null)
			{
				this.warning_ignore_table = new HashSet<int>();
			}
			this.warning_ignore_table.Add(code);
		}

		// Token: 0x04000BB9 RID: 3001
		public Target Target;

		// Token: 0x04000BBA RID: 3002
		public Platform Platform;

		// Token: 0x04000BBB RID: 3003
		public string TargetExt;

		// Token: 0x04000BBC RID: 3004
		public bool VerifyClsCompliance;

		// Token: 0x04000BBD RID: 3005
		public bool Optimize;

		// Token: 0x04000BBE RID: 3006
		public LanguageVersion Version;

		// Token: 0x04000BBF RID: 3007
		public bool EnhancedWarnings;

		// Token: 0x04000BC0 RID: 3008
		public bool LoadDefaultReferences;

		// Token: 0x04000BC1 RID: 3009
		public string SdkVersion;

		// Token: 0x04000BC2 RID: 3010
		public string StrongNameKeyFile;

		// Token: 0x04000BC3 RID: 3011
		public string StrongNameKeyContainer;

		// Token: 0x04000BC4 RID: 3012
		public bool StrongNameDelaySign;

		// Token: 0x04000BC5 RID: 3013
		public int TabSize;

		// Token: 0x04000BC6 RID: 3014
		public bool WarningsAreErrors;

		// Token: 0x04000BC7 RID: 3015
		public int WarningLevel;

		// Token: 0x04000BC8 RID: 3016
		public List<string> AssemblyReferences;

		// Token: 0x04000BC9 RID: 3017
		public List<Tuple<string, string>> AssemblyReferencesAliases;

		// Token: 0x04000BCA RID: 3018
		public List<string> Modules;

		// Token: 0x04000BCB RID: 3019
		public List<string> ReferencesLookupPaths;

		// Token: 0x04000BCC RID: 3020
		public Encoding Encoding;

		// Token: 0x04000BCD RID: 3021
		public string DocumentationFile;

		// Token: 0x04000BCE RID: 3022
		public string MainClass;

		// Token: 0x04000BCF RID: 3023
		public string OutputFile;

		// Token: 0x04000BD0 RID: 3024
		public bool Checked;

		// Token: 0x04000BD1 RID: 3025
		public bool StatementMode;

		// Token: 0x04000BD2 RID: 3026
		public bool Unsafe;

		// Token: 0x04000BD3 RID: 3027
		public string Win32ResourceFile;

		// Token: 0x04000BD4 RID: 3028
		public string Win32IconFile;

		// Token: 0x04000BD5 RID: 3029
		public List<AssemblyResource> Resources;

		// Token: 0x04000BD6 RID: 3030
		public bool GenerateDebugInfo;

		// Token: 0x04000BD7 RID: 3031
		public bool ParseOnly;

		// Token: 0x04000BD8 RID: 3032
		public bool TokenizeOnly;

		// Token: 0x04000BD9 RID: 3033
		public bool Timestamps;

		// Token: 0x04000BDA RID: 3034
		public int DebugFlags;

		// Token: 0x04000BDB RID: 3035
		public int VerboseParserFlag;

		// Token: 0x04000BDC RID: 3036
		public int FatalCounter;

		// Token: 0x04000BDD RID: 3037
		public bool Stacktrace;

		// Token: 0x04000BDE RID: 3038
		public bool BreakOnInternalError;

		// Token: 0x04000BDF RID: 3039
		public List<string> GetResourceStrings;

		// Token: 0x04000BE0 RID: 3040
		public bool ShowFullPaths;

		// Token: 0x04000BE1 RID: 3041
		public bool StdLib;

		// Token: 0x04000BE2 RID: 3042
		public RuntimeVersion StdLibRuntimeVersion;

		// Token: 0x04000BE3 RID: 3043
		public string RuntimeMetadataVersion;

		// Token: 0x04000BE4 RID: 3044
		public bool WriteMetadataOnly;

		// Token: 0x04000BE5 RID: 3045
		private readonly List<string> conditional_symbols;

		// Token: 0x04000BE6 RID: 3046
		private readonly List<SourceFile> source_files;

		// Token: 0x04000BE7 RID: 3047
		private List<int> warnings_as_error;

		// Token: 0x04000BE8 RID: 3048
		private List<int> warnings_only;

		// Token: 0x04000BE9 RID: 3049
		private HashSet<int> warning_ignore_table;
	}
}
