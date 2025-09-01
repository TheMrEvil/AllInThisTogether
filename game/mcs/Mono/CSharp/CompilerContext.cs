using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x0200015C RID: 348
	public class CompilerContext
	{
		// Token: 0x06001157 RID: 4439 RVA: 0x00047AFB File Offset: 0x00045CFB
		public CompilerContext(CompilerSettings settings, ReportPrinter reportPrinter)
		{
			this.settings = settings;
			this.report = new Report(this, reportPrinter);
			this.builtin_types = new BuiltinTypes();
			this.TimeReporter = CompilerContext.DisabledTimeReporter;
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x00047B2D File Offset: 0x00045D2D
		public BuiltinTypes BuiltinTypes
		{
			get
			{
				return this.builtin_types;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x00047B35 File Offset: 0x00045D35
		// (set) Token: 0x0600115A RID: 4442 RVA: 0x00047B3D File Offset: 0x00045D3D
		public bool IsRuntimeBinder
		{
			[CompilerGenerated]
			get
			{
				return this.<IsRuntimeBinder>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsRuntimeBinder>k__BackingField = value;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x00047B46 File Offset: 0x00045D46
		public Report Report
		{
			get
			{
				return this.report;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x00047B4E File Offset: 0x00045D4E
		public CompilerSettings Settings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x00047B56 File Offset: 0x00045D56
		public List<SourceFile> SourceFiles
		{
			get
			{
				return this.settings.SourceFiles;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x00047B63 File Offset: 0x00045D63
		// (set) Token: 0x0600115F RID: 4447 RVA: 0x00047B6B File Offset: 0x00045D6B
		public TimeReporter TimeReporter
		{
			[CompilerGenerated]
			get
			{
				return this.<TimeReporter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TimeReporter>k__BackingField = value;
			}
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00047B74 File Offset: 0x00045D74
		public SourceFile LookupFile(CompilationSourceFile comp_unit, string name)
		{
			if (this.all_source_files == null)
			{
				this.all_source_files = new Dictionary<string, SourceFile>();
				foreach (SourceFile sourceFile in this.SourceFiles)
				{
					this.all_source_files[sourceFile.FullPathName] = sourceFile;
				}
			}
			string text;
			if (!Path.IsPathRooted(name))
			{
				SourceFile sourceFile2 = comp_unit.SourceFile;
				text = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(sourceFile2.FullPathName), name));
				string directoryName = Path.GetDirectoryName(sourceFile2.Name);
				if (!string.IsNullOrEmpty(directoryName))
				{
					name = Path.Combine(directoryName, name);
				}
			}
			else
			{
				text = name;
			}
			SourceFile sourceFile3;
			if (this.all_source_files.TryGetValue(text, out sourceFile3))
			{
				return sourceFile3;
			}
			sourceFile3 = new SourceFile(name, text, this.all_source_files.Count + 1, null);
			Location.AddFile(sourceFile3);
			this.all_source_files.Add(text, sourceFile3);
			return sourceFile3;
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00047C6C File Offset: 0x00045E6C
		// Note: this type is marked as 'beforefieldinit'.
		static CompilerContext()
		{
		}

		// Token: 0x04000761 RID: 1889
		private static readonly TimeReporter DisabledTimeReporter = new TimeReporter(false);

		// Token: 0x04000762 RID: 1890
		private readonly Report report;

		// Token: 0x04000763 RID: 1891
		private readonly BuiltinTypes builtin_types;

		// Token: 0x04000764 RID: 1892
		private readonly CompilerSettings settings;

		// Token: 0x04000765 RID: 1893
		private Dictionary<string, SourceFile> all_source_files;

		// Token: 0x04000766 RID: 1894
		[CompilerGenerated]
		private bool <IsRuntimeBinder>k__BackingField;

		// Token: 0x04000767 RID: 1895
		[CompilerGenerated]
		private TimeReporter <TimeReporter>k__BackingField;
	}
}
