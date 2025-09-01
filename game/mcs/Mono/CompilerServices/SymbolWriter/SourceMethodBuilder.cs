using System;
using System.Collections.Generic;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x0200031F RID: 799
	public class SourceMethodBuilder
	{
		// Token: 0x06002550 RID: 9552 RVA: 0x000B29C3 File Offset: 0x000B0BC3
		public SourceMethodBuilder(ICompileUnit comp_unit)
		{
			this._comp_unit = comp_unit;
			this.method_lines = new List<LineNumberEntry>();
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x000B29DD File Offset: 0x000B0BDD
		public SourceMethodBuilder(ICompileUnit comp_unit, int ns_id, IMethodDef method) : this(comp_unit)
		{
			this.ns_id = ns_id;
			this.method = method;
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x000B29F4 File Offset: 0x000B0BF4
		public void MarkSequencePoint(int offset, SourceFileEntry file, int line, int column, bool is_hidden)
		{
			this.MarkSequencePoint(offset, file, line, column, -1, -1, is_hidden);
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x000B2A08 File Offset: 0x000B0C08
		public void MarkSequencePoint(int offset, SourceFileEntry file, int line, int column, int end_line, int end_column, bool is_hidden)
		{
			LineNumberEntry lineNumberEntry = new LineNumberEntry((file != null) ? file.Index : 0, line, column, end_line, end_column, offset, is_hidden);
			if (this.method_lines.Count > 0)
			{
				LineNumberEntry lineNumberEntry2 = this.method_lines[this.method_lines.Count - 1];
				if (lineNumberEntry2.Offset == offset)
				{
					if (LineNumberEntry.LocationComparer.Default.Compare(lineNumberEntry, lineNumberEntry2) > 0)
					{
						this.method_lines[this.method_lines.Count - 1] = lineNumberEntry;
					}
					return;
				}
			}
			this.method_lines.Add(lineNumberEntry);
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x000B2A98 File Offset: 0x000B0C98
		public void StartBlock(CodeBlockEntry.Type type, int start_offset)
		{
			if (this._block_stack == null)
			{
				this._block_stack = new Stack<CodeBlockEntry>();
			}
			if (this._blocks == null)
			{
				this._blocks = new List<CodeBlockEntry>();
			}
			int parent = (this.CurrentBlock != null) ? this.CurrentBlock.Index : -1;
			CodeBlockEntry item = new CodeBlockEntry(this._blocks.Count + 1, parent, type, start_offset);
			this._block_stack.Push(item);
			this._blocks.Add(item);
		}

		// Token: 0x06002555 RID: 9557 RVA: 0x000B2B10 File Offset: 0x000B0D10
		public void EndBlock(int end_offset)
		{
			this._block_stack.Pop().Close(end_offset);
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06002556 RID: 9558 RVA: 0x000B2B24 File Offset: 0x000B0D24
		public CodeBlockEntry[] Blocks
		{
			get
			{
				if (this._blocks == null)
				{
					return new CodeBlockEntry[0];
				}
				CodeBlockEntry[] array = new CodeBlockEntry[this._blocks.Count];
				this._blocks.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06002557 RID: 9559 RVA: 0x000B2B5F File Offset: 0x000B0D5F
		public CodeBlockEntry CurrentBlock
		{
			get
			{
				if (this._block_stack != null && this._block_stack.Count > 0)
				{
					return this._block_stack.Peek();
				}
				return null;
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06002558 RID: 9560 RVA: 0x000B2B84 File Offset: 0x000B0D84
		public LocalVariableEntry[] Locals
		{
			get
			{
				if (this._locals == null)
				{
					return new LocalVariableEntry[0];
				}
				return this._locals.ToArray();
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06002559 RID: 9561 RVA: 0x000B2BA0 File Offset: 0x000B0DA0
		public ICompileUnit SourceFile
		{
			get
			{
				return this._comp_unit;
			}
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x000B2BA8 File Offset: 0x000B0DA8
		public void AddLocal(int index, string name)
		{
			if (this._locals == null)
			{
				this._locals = new List<LocalVariableEntry>();
			}
			int block = (this.CurrentBlock != null) ? this.CurrentBlock.Index : 0;
			this._locals.Add(new LocalVariableEntry(index, name, block));
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x0600255B RID: 9563 RVA: 0x000B2BF2 File Offset: 0x000B0DF2
		public ScopeVariable[] ScopeVariables
		{
			get
			{
				if (this._scope_vars == null)
				{
					return new ScopeVariable[0];
				}
				return this._scope_vars.ToArray();
			}
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x000B2C0E File Offset: 0x000B0E0E
		public void AddScopeVariable(int scope, int index)
		{
			if (this._scope_vars == null)
			{
				this._scope_vars = new List<ScopeVariable>();
			}
			this._scope_vars.Add(new ScopeVariable(scope, index));
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x000B2C35 File Offset: 0x000B0E35
		public void DefineMethod(MonoSymbolFile file)
		{
			this.DefineMethod(file, this.method.Token);
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x000B2C4C File Offset: 0x000B0E4C
		public void DefineMethod(MonoSymbolFile file, int token)
		{
			MethodEntry entry = new MethodEntry(file, this._comp_unit.Entry, token, this.ScopeVariables, this.Locals, this.method_lines.ToArray(), this.Blocks, null, MethodEntry.Flags.ColumnsInfoIncluded, this.ns_id);
			file.AddMethod(entry);
		}

		// Token: 0x04000E1D RID: 3613
		private List<LocalVariableEntry> _locals;

		// Token: 0x04000E1E RID: 3614
		private List<CodeBlockEntry> _blocks;

		// Token: 0x04000E1F RID: 3615
		private List<ScopeVariable> _scope_vars;

		// Token: 0x04000E20 RID: 3616
		private Stack<CodeBlockEntry> _block_stack;

		// Token: 0x04000E21 RID: 3617
		private readonly List<LineNumberEntry> method_lines;

		// Token: 0x04000E22 RID: 3618
		private readonly ICompileUnit _comp_unit;

		// Token: 0x04000E23 RID: 3619
		private readonly int ns_id;

		// Token: 0x04000E24 RID: 3620
		private readonly IMethodDef method;
	}
}
