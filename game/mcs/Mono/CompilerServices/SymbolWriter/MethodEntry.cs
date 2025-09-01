using System;
using System.Collections.Generic;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x0200031D RID: 797
	public class MethodEntry : IComparable
	{
		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x0600253C RID: 9532 RVA: 0x000B1F73 File Offset: 0x000B0173
		public MethodEntry.Flags MethodFlags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x0600253D RID: 9533 RVA: 0x000B1F7B File Offset: 0x000B017B
		// (set) Token: 0x0600253E RID: 9534 RVA: 0x000B1F83 File Offset: 0x000B0183
		public int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x000B1F8C File Offset: 0x000B018C
		internal MethodEntry(MonoSymbolFile file, MyBinaryReader reader, int index)
		{
			this.SymbolFile = file;
			this.index = index;
			this.Token = reader.ReadInt32();
			this.DataOffset = reader.ReadInt32();
			this.LineNumberTableOffset = reader.ReadInt32();
			long position = reader.BaseStream.Position;
			reader.BaseStream.Position = (long)this.DataOffset;
			this.CompileUnitIndex = reader.ReadLeb128();
			this.LocalVariableTableOffset = reader.ReadLeb128();
			this.NamespaceID = reader.ReadLeb128();
			this.CodeBlockTableOffset = reader.ReadLeb128();
			this.ScopeVariableTableOffset = reader.ReadLeb128();
			this.RealNameOffset = reader.ReadLeb128();
			this.flags = (MethodEntry.Flags)reader.ReadLeb128();
			reader.BaseStream.Position = position;
			this.CompileUnit = file.GetCompileUnit(this.CompileUnitIndex);
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x000B2064 File Offset: 0x000B0264
		internal MethodEntry(MonoSymbolFile file, CompileUnitEntry comp_unit, int token, ScopeVariable[] scope_vars, LocalVariableEntry[] locals, LineNumberEntry[] lines, CodeBlockEntry[] code_blocks, string real_name, MethodEntry.Flags flags, int namespace_id)
		{
			this.SymbolFile = file;
			this.real_name = real_name;
			this.locals = locals;
			this.code_blocks = code_blocks;
			this.scope_vars = scope_vars;
			this.flags = flags;
			this.index = -1;
			this.Token = token;
			this.CompileUnitIndex = comp_unit.Index;
			this.CompileUnit = comp_unit;
			this.NamespaceID = namespace_id;
			MethodEntry.CheckLineNumberTable(lines);
			this.lnt = new LineNumberTable(file, lines);
			file.NumLineNumbers += lines.Length;
			int num = (locals != null) ? locals.Length : 0;
			if (num <= 32)
			{
				for (int i = 0; i < num; i++)
				{
					string name = locals[i].Name;
					for (int j = i + 1; j < num; j++)
					{
						if (locals[j].Name == name)
						{
							flags |= MethodEntry.Flags.LocalNamesAmbiguous;
							return;
						}
					}
				}
				return;
			}
			Dictionary<string, LocalVariableEntry> dictionary = new Dictionary<string, LocalVariableEntry>();
			foreach (LocalVariableEntry localVariableEntry in locals)
			{
				if (dictionary.ContainsKey(localVariableEntry.Name))
				{
					flags |= MethodEntry.Flags.LocalNamesAmbiguous;
					return;
				}
				dictionary.Add(localVariableEntry.Name, localVariableEntry);
			}
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x000B219C File Offset: 0x000B039C
		private static void CheckLineNumberTable(LineNumberEntry[] line_numbers)
		{
			int num = -1;
			int num2 = -1;
			if (line_numbers == null)
			{
				return;
			}
			foreach (LineNumberEntry lineNumberEntry in line_numbers)
			{
				if (lineNumberEntry.Equals(LineNumberEntry.Null))
				{
					throw new MonoSymbolFileException();
				}
				if (lineNumberEntry.Offset < num)
				{
					throw new MonoSymbolFileException();
				}
				if (lineNumberEntry.Offset > num)
				{
					num2 = lineNumberEntry.Row;
					num = lineNumberEntry.Offset;
				}
				else if (lineNumberEntry.Row > num2)
				{
					num2 = lineNumberEntry.Row;
				}
			}
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x000B220E File Offset: 0x000B040E
		internal void Write(MyBinaryWriter bw)
		{
			if (this.index <= 0 || this.DataOffset == 0)
			{
				throw new InvalidOperationException();
			}
			bw.Write(this.Token);
			bw.Write(this.DataOffset);
			bw.Write(this.LineNumberTableOffset);
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x000B224C File Offset: 0x000B044C
		internal void WriteData(MonoSymbolFile file, MyBinaryWriter bw)
		{
			if (this.index <= 0)
			{
				throw new InvalidOperationException();
			}
			this.LocalVariableTableOffset = (int)bw.BaseStream.Position;
			int num = (this.locals != null) ? this.locals.Length : 0;
			bw.WriteLeb128(num);
			for (int i = 0; i < num; i++)
			{
				this.locals[i].Write(file, bw);
			}
			file.LocalCount += num;
			this.CodeBlockTableOffset = (int)bw.BaseStream.Position;
			int num2 = (this.code_blocks != null) ? this.code_blocks.Length : 0;
			bw.WriteLeb128(num2);
			for (int j = 0; j < num2; j++)
			{
				this.code_blocks[j].Write(bw);
			}
			this.ScopeVariableTableOffset = (int)bw.BaseStream.Position;
			int num3 = (this.scope_vars != null) ? this.scope_vars.Length : 0;
			bw.WriteLeb128(num3);
			for (int k = 0; k < num3; k++)
			{
				this.scope_vars[k].Write(bw);
			}
			if (this.real_name != null)
			{
				this.RealNameOffset = (int)bw.BaseStream.Position;
				bw.Write(this.real_name);
			}
			foreach (LineNumberEntry lineNumberEntry in this.lnt.LineNumbers)
			{
				if (lineNumberEntry.EndRow != -1 || lineNumberEntry.EndColumn != -1)
				{
					this.flags |= MethodEntry.Flags.EndInfoIncluded;
				}
			}
			this.LineNumberTableOffset = (int)bw.BaseStream.Position;
			this.lnt.Write(file, bw, (this.flags & MethodEntry.Flags.ColumnsInfoIncluded) > (MethodEntry.Flags)0, (this.flags & MethodEntry.Flags.EndInfoIncluded) > (MethodEntry.Flags)0);
			this.DataOffset = (int)bw.BaseStream.Position;
			bw.WriteLeb128(this.CompileUnitIndex);
			bw.WriteLeb128(this.LocalVariableTableOffset);
			bw.WriteLeb128(this.NamespaceID);
			bw.WriteLeb128(this.CodeBlockTableOffset);
			bw.WriteLeb128(this.ScopeVariableTableOffset);
			bw.WriteLeb128(this.RealNameOffset);
			bw.WriteLeb128((int)this.flags);
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x000B2468 File Offset: 0x000B0668
		public void ReadAll()
		{
			this.GetLineNumberTable();
			this.GetLocals();
			this.GetCodeBlocks();
			this.GetScopeVariables();
			this.GetRealName();
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x000B2490 File Offset: 0x000B0690
		public LineNumberTable GetLineNumberTable()
		{
			MonoSymbolFile symbolFile = this.SymbolFile;
			LineNumberTable result;
			lock (symbolFile)
			{
				if (this.lnt != null)
				{
					result = this.lnt;
				}
				else if (this.LineNumberTableOffset == 0)
				{
					result = null;
				}
				else
				{
					MyBinaryReader binaryReader = this.SymbolFile.BinaryReader;
					long position = binaryReader.BaseStream.Position;
					binaryReader.BaseStream.Position = (long)this.LineNumberTableOffset;
					this.lnt = LineNumberTable.Read(this.SymbolFile, binaryReader, (this.flags & MethodEntry.Flags.ColumnsInfoIncluded) > (MethodEntry.Flags)0, (this.flags & MethodEntry.Flags.EndInfoIncluded) > (MethodEntry.Flags)0);
					binaryReader.BaseStream.Position = position;
					result = this.lnt;
				}
			}
			return result;
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x000B2548 File Offset: 0x000B0748
		public LocalVariableEntry[] GetLocals()
		{
			MonoSymbolFile symbolFile = this.SymbolFile;
			LocalVariableEntry[] result;
			lock (symbolFile)
			{
				if (this.locals != null)
				{
					result = this.locals;
				}
				else if (this.LocalVariableTableOffset == 0)
				{
					result = null;
				}
				else
				{
					MyBinaryReader binaryReader = this.SymbolFile.BinaryReader;
					long position = binaryReader.BaseStream.Position;
					binaryReader.BaseStream.Position = (long)this.LocalVariableTableOffset;
					int num = binaryReader.ReadLeb128();
					this.locals = new LocalVariableEntry[num];
					for (int i = 0; i < num; i++)
					{
						this.locals[i] = new LocalVariableEntry(this.SymbolFile, binaryReader);
					}
					binaryReader.BaseStream.Position = position;
					result = this.locals;
				}
			}
			return result;
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x000B261C File Offset: 0x000B081C
		public CodeBlockEntry[] GetCodeBlocks()
		{
			MonoSymbolFile symbolFile = this.SymbolFile;
			CodeBlockEntry[] result;
			lock (symbolFile)
			{
				if (this.code_blocks != null)
				{
					result = this.code_blocks;
				}
				else if (this.CodeBlockTableOffset == 0)
				{
					result = null;
				}
				else
				{
					MyBinaryReader binaryReader = this.SymbolFile.BinaryReader;
					long position = binaryReader.BaseStream.Position;
					binaryReader.BaseStream.Position = (long)this.CodeBlockTableOffset;
					int num = binaryReader.ReadLeb128();
					this.code_blocks = new CodeBlockEntry[num];
					for (int i = 0; i < num; i++)
					{
						this.code_blocks[i] = new CodeBlockEntry(i, binaryReader);
					}
					binaryReader.BaseStream.Position = position;
					result = this.code_blocks;
				}
			}
			return result;
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x000B26E8 File Offset: 0x000B08E8
		public ScopeVariable[] GetScopeVariables()
		{
			MonoSymbolFile symbolFile = this.SymbolFile;
			ScopeVariable[] result;
			lock (symbolFile)
			{
				if (this.scope_vars != null)
				{
					result = this.scope_vars;
				}
				else if (this.ScopeVariableTableOffset == 0)
				{
					result = null;
				}
				else
				{
					MyBinaryReader binaryReader = this.SymbolFile.BinaryReader;
					long position = binaryReader.BaseStream.Position;
					binaryReader.BaseStream.Position = (long)this.ScopeVariableTableOffset;
					int num = binaryReader.ReadLeb128();
					this.scope_vars = new ScopeVariable[num];
					for (int i = 0; i < num; i++)
					{
						this.scope_vars[i] = new ScopeVariable(binaryReader);
					}
					binaryReader.BaseStream.Position = position;
					result = this.scope_vars;
				}
			}
			return result;
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x000B27B4 File Offset: 0x000B09B4
		public string GetRealName()
		{
			MonoSymbolFile symbolFile = this.SymbolFile;
			string result;
			lock (symbolFile)
			{
				if (this.real_name != null)
				{
					result = this.real_name;
				}
				else if (this.RealNameOffset == 0)
				{
					result = null;
				}
				else
				{
					this.real_name = this.SymbolFile.BinaryReader.ReadString(this.RealNameOffset);
					result = this.real_name;
				}
			}
			return result;
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x000B2828 File Offset: 0x000B0A28
		public int CompareTo(object obj)
		{
			MethodEntry methodEntry = (MethodEntry)obj;
			if (methodEntry.Token < this.Token)
			{
				return 1;
			}
			if (methodEntry.Token > this.Token)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x000B2860 File Offset: 0x000B0A60
		public override string ToString()
		{
			return string.Format("[Method {0}:{1:x}:{2}:{3}]", new object[]
			{
				this.index,
				this.Token,
				this.CompileUnitIndex,
				this.CompileUnit
			});
		}

		// Token: 0x04000E06 RID: 3590
		public readonly int CompileUnitIndex;

		// Token: 0x04000E07 RID: 3591
		public readonly int Token;

		// Token: 0x04000E08 RID: 3592
		public readonly int NamespaceID;

		// Token: 0x04000E09 RID: 3593
		private int DataOffset;

		// Token: 0x04000E0A RID: 3594
		private int LocalVariableTableOffset;

		// Token: 0x04000E0B RID: 3595
		private int LineNumberTableOffset;

		// Token: 0x04000E0C RID: 3596
		private int CodeBlockTableOffset;

		// Token: 0x04000E0D RID: 3597
		private int ScopeVariableTableOffset;

		// Token: 0x04000E0E RID: 3598
		private int RealNameOffset;

		// Token: 0x04000E0F RID: 3599
		private MethodEntry.Flags flags;

		// Token: 0x04000E10 RID: 3600
		private int index;

		// Token: 0x04000E11 RID: 3601
		public readonly CompileUnitEntry CompileUnit;

		// Token: 0x04000E12 RID: 3602
		private LocalVariableEntry[] locals;

		// Token: 0x04000E13 RID: 3603
		private CodeBlockEntry[] code_blocks;

		// Token: 0x04000E14 RID: 3604
		private ScopeVariable[] scope_vars;

		// Token: 0x04000E15 RID: 3605
		private LineNumberTable lnt;

		// Token: 0x04000E16 RID: 3606
		private string real_name;

		// Token: 0x04000E17 RID: 3607
		public readonly MonoSymbolFile SymbolFile;

		// Token: 0x04000E18 RID: 3608
		public const int Size = 12;

		// Token: 0x02000414 RID: 1044
		[Flags]
		public enum Flags
		{
			// Token: 0x040011A2 RID: 4514
			LocalNamesAmbiguous = 1,
			// Token: 0x040011A3 RID: 4515
			ColumnsInfoIncluded = 2,
			// Token: 0x040011A4 RID: 4516
			EndInfoIncluded = 4
		}
	}
}
