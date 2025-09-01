using System;
using System.Collections.Generic;
using Mono.CompilerServices.SymbolWriter;

namespace Mono.CSharp
{
	// Token: 0x0200025C RID: 604
	public class CompilationSourceFile : NamespaceContainer
	{
		// Token: 0x06001DE1 RID: 7649 RVA: 0x000924AC File Offset: 0x000906AC
		public CompilationSourceFile(ModuleContainer parent, SourceFile sourceFile) : this(parent)
		{
			this.file = sourceFile;
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x000924BC File Offset: 0x000906BC
		public CompilationSourceFile(ModuleContainer parent) : base(parent)
		{
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x000924C5 File Offset: 0x000906C5
		public CompileUnitEntry SymbolUnitEntry
		{
			get
			{
				return this.comp_unit;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001DE4 RID: 7652 RVA: 0x000924CD File Offset: 0x000906CD
		public string FileName
		{
			get
			{
				return this.file.Name;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001DE5 RID: 7653 RVA: 0x000924DA File Offset: 0x000906DA
		public SourceFile SourceFile
		{
			get
			{
				return this.file;
			}
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x000924E4 File Offset: 0x000906E4
		public void AddIncludeFile(SourceFile file)
		{
			if (file == this.file)
			{
				return;
			}
			if (this.include_files == null)
			{
				this.include_files = new Dictionary<string, SourceFile>();
			}
			if (!this.include_files.ContainsKey(file.FullPathName))
			{
				this.include_files.Add(file.FullPathName, file);
			}
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x00092533 File Offset: 0x00090733
		public void AddDefine(string value)
		{
			if (this.conditionals == null)
			{
				this.conditionals = new Dictionary<string, bool>(2);
			}
			this.conditionals[value] = true;
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x00092556 File Offset: 0x00090756
		public void AddUndefine(string value)
		{
			if (this.conditionals == null)
			{
				this.conditionals = new Dictionary<string, bool>(2);
			}
			this.conditionals[value] = false;
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x0009257C File Offset: 0x0009077C
		public override void PrepareEmit()
		{
			MonoSymbolFile symbolWriter = this.Module.DeclaringAssembly.SymbolWriter;
			if (symbolWriter != null)
			{
				this.CreateUnitSymbolInfo(symbolWriter);
			}
			base.PrepareEmit();
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x000925AC File Offset: 0x000907AC
		private void CreateUnitSymbolInfo(MonoSymbolFile symwriter)
		{
			SourceFileEntry source = this.file.CreateSymbolInfo(symwriter);
			this.comp_unit = new CompileUnitEntry(symwriter, source);
			if (this.include_files != null)
			{
				foreach (SourceFile sourceFile in this.include_files.Values)
				{
					source = sourceFile.CreateSymbolInfo(symwriter);
					this.comp_unit.AddFile(source);
				}
			}
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x00092634 File Offset: 0x00090834
		public bool IsConditionalDefined(string value)
		{
			if (this.conditionals != null)
			{
				bool result;
				if (this.conditionals.TryGetValue(value, out result))
				{
					return result;
				}
				if (this.conditionals.ContainsKey(value))
				{
					return false;
				}
			}
			return this.Compiler.Settings.IsConditionalSymbolDefined(value);
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x0009267C File Offset: 0x0009087C
		public override void Accept(StructuralVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x04000B1D RID: 2845
		private readonly SourceFile file;

		// Token: 0x04000B1E RID: 2846
		private CompileUnitEntry comp_unit;

		// Token: 0x04000B1F RID: 2847
		private Dictionary<string, SourceFile> include_files;

		// Token: 0x04000B20 RID: 2848
		private Dictionary<string, bool> conditionals;
	}
}
