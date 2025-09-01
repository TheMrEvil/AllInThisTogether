using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000311 RID: 785
	public class MonoSymbolFile : IDisposable
	{
		// Token: 0x060024CD RID: 9421 RVA: 0x000AFCB8 File Offset: 0x000ADEB8
		public MonoSymbolFile()
		{
			this.ot = new OffsetTable();
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x000AFCF4 File Offset: 0x000ADEF4
		public int AddSource(SourceFileEntry source)
		{
			this.sources.Add(source);
			return this.sources.Count;
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x000AFD0D File Offset: 0x000ADF0D
		public int AddCompileUnit(CompileUnitEntry entry)
		{
			this.comp_units.Add(entry);
			return this.comp_units.Count;
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x000AFD26 File Offset: 0x000ADF26
		public void AddMethod(MethodEntry entry)
		{
			this.methods.Add(entry);
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x000AFD34 File Offset: 0x000ADF34
		public MethodEntry DefineMethod(CompileUnitEntry comp_unit, int token, ScopeVariable[] scope_vars, LocalVariableEntry[] locals, LineNumberEntry[] lines, CodeBlockEntry[] code_blocks, string real_name, MethodEntry.Flags flags, int namespace_id)
		{
			if (this.reader != null)
			{
				throw new InvalidOperationException();
			}
			MethodEntry methodEntry = new MethodEntry(this, comp_unit, token, scope_vars, locals, lines, code_blocks, real_name, flags, namespace_id);
			this.AddMethod(methodEntry);
			return methodEntry;
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x000AFD6D File Offset: 0x000ADF6D
		internal void DefineAnonymousScope(int id)
		{
			if (this.reader != null)
			{
				throw new InvalidOperationException();
			}
			if (this.anonymous_scopes == null)
			{
				this.anonymous_scopes = new Dictionary<int, AnonymousScopeEntry>();
			}
			this.anonymous_scopes.Add(id, new AnonymousScopeEntry(id));
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x000AFDA2 File Offset: 0x000ADFA2
		internal void DefineCapturedVariable(int scope_id, string name, string captured_name, CapturedVariable.CapturedKind kind)
		{
			if (this.reader != null)
			{
				throw new InvalidOperationException();
			}
			this.anonymous_scopes[scope_id].AddCapturedVariable(name, captured_name, kind);
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x000AFDC7 File Offset: 0x000ADFC7
		internal void DefineCapturedScope(int scope_id, int id, string captured_name)
		{
			if (this.reader != null)
			{
				throw new InvalidOperationException();
			}
			this.anonymous_scopes[scope_id].AddCapturedScope(id, captured_name);
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x000AFDEC File Offset: 0x000ADFEC
		internal int GetNextTypeIndex()
		{
			int result = this.last_type_index + 1;
			this.last_type_index = result;
			return result;
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x000AFE0C File Offset: 0x000AE00C
		internal int GetNextMethodIndex()
		{
			int result = this.last_method_index + 1;
			this.last_method_index = result;
			return result;
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x000AFE2C File Offset: 0x000AE02C
		internal int GetNextNamespaceIndex()
		{
			int result = this.last_namespace_index + 1;
			this.last_namespace_index = result;
			return result;
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x000AFE4C File Offset: 0x000AE04C
		private void Write(MyBinaryWriter bw, Guid guid)
		{
			bw.Write(5037318119232611860L);
			bw.Write(this.MajorVersion);
			bw.Write(this.MinorVersion);
			bw.Write(guid.ToByteArray());
			long position = bw.BaseStream.Position;
			this.ot.Write(bw, this.MajorVersion, this.MinorVersion);
			this.methods.Sort();
			for (int i = 0; i < this.methods.Count; i++)
			{
				this.methods[i].Index = i + 1;
			}
			this.ot.DataSectionOffset = (int)bw.BaseStream.Position;
			foreach (SourceFileEntry sourceFileEntry in this.sources)
			{
				sourceFileEntry.WriteData(bw);
			}
			foreach (CompileUnitEntry compileUnitEntry in this.comp_units)
			{
				compileUnitEntry.WriteData(bw);
			}
			foreach (MethodEntry methodEntry in this.methods)
			{
				methodEntry.WriteData(this, bw);
			}
			this.ot.DataSectionSize = (int)bw.BaseStream.Position - this.ot.DataSectionOffset;
			this.ot.MethodTableOffset = (int)bw.BaseStream.Position;
			for (int j = 0; j < this.methods.Count; j++)
			{
				this.methods[j].Write(bw);
			}
			this.ot.MethodTableSize = (int)bw.BaseStream.Position - this.ot.MethodTableOffset;
			this.ot.SourceTableOffset = (int)bw.BaseStream.Position;
			for (int k = 0; k < this.sources.Count; k++)
			{
				this.sources[k].Write(bw);
			}
			this.ot.SourceTableSize = (int)bw.BaseStream.Position - this.ot.SourceTableOffset;
			this.ot.CompileUnitTableOffset = (int)bw.BaseStream.Position;
			for (int l = 0; l < this.comp_units.Count; l++)
			{
				this.comp_units[l].Write(bw);
			}
			this.ot.CompileUnitTableSize = (int)bw.BaseStream.Position - this.ot.CompileUnitTableOffset;
			this.ot.AnonymousScopeCount = ((this.anonymous_scopes != null) ? this.anonymous_scopes.Count : 0);
			this.ot.AnonymousScopeTableOffset = (int)bw.BaseStream.Position;
			if (this.anonymous_scopes != null)
			{
				foreach (AnonymousScopeEntry anonymousScopeEntry in this.anonymous_scopes.Values)
				{
					anonymousScopeEntry.Write(bw);
				}
			}
			this.ot.AnonymousScopeTableSize = (int)bw.BaseStream.Position - this.ot.AnonymousScopeTableOffset;
			this.ot.TypeCount = this.last_type_index;
			this.ot.MethodCount = this.methods.Count;
			this.ot.SourceCount = this.sources.Count;
			this.ot.CompileUnitCount = this.comp_units.Count;
			this.ot.TotalFileSize = (int)bw.BaseStream.Position;
			bw.Seek((int)position, SeekOrigin.Begin);
			this.ot.Write(bw, this.MajorVersion, this.MinorVersion);
			bw.Seek(0, SeekOrigin.End);
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x000B025C File Offset: 0x000AE45C
		public void CreateSymbolFile(Guid guid, FileStream fs)
		{
			if (this.reader != null)
			{
				throw new InvalidOperationException();
			}
			this.Write(new MyBinaryWriter(fs), guid);
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x000B027C File Offset: 0x000AE47C
		private MonoSymbolFile(Stream stream)
		{
			this.reader = new MyBinaryReader(stream);
			try
			{
				long num = this.reader.ReadInt64();
				int num2 = this.reader.ReadInt32();
				int num3 = this.reader.ReadInt32();
				if (num != 5037318119232611860L)
				{
					throw new MonoSymbolFileException("Symbol file is not a valid", new object[0]);
				}
				if (num2 != 50)
				{
					throw new MonoSymbolFileException("Symbol file has version {0} but expected {1}", new object[]
					{
						num2,
						50
					});
				}
				if (num3 != 0)
				{
					throw new MonoSymbolFileException("Symbol file has version {0}.{1} but expected {2}.{3}", new object[]
					{
						num2,
						num3,
						50,
						0
					});
				}
				this.MajorVersion = num2;
				this.MinorVersion = num3;
				this.guid = new Guid(this.reader.ReadBytes(16));
				this.ot = new OffsetTable(this.reader, num2, num3);
			}
			catch (Exception innerException)
			{
				throw new MonoSymbolFileException("Cannot read symbol file", innerException);
			}
			this.source_file_hash = new Dictionary<int, SourceFileEntry>();
			this.compile_unit_hash = new Dictionary<int, CompileUnitEntry>();
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x000B03D4 File Offset: 0x000AE5D4
		public static MonoSymbolFile ReadSymbolFile(Assembly assembly)
		{
			string mdbFilename = assembly.Location + ".mdb";
			Guid moduleVersionId = assembly.GetModules()[0].ModuleVersionId;
			return MonoSymbolFile.ReadSymbolFile(mdbFilename, moduleVersionId);
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x000B0405 File Offset: 0x000AE605
		public static MonoSymbolFile ReadSymbolFile(string mdbFilename)
		{
			return MonoSymbolFile.ReadSymbolFile(new FileStream(mdbFilename, FileMode.Open, FileAccess.Read));
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x000B0414 File Offset: 0x000AE614
		public static MonoSymbolFile ReadSymbolFile(string mdbFilename, Guid assemblyGuid)
		{
			MonoSymbolFile monoSymbolFile = MonoSymbolFile.ReadSymbolFile(mdbFilename);
			if (assemblyGuid != monoSymbolFile.guid)
			{
				throw new MonoSymbolFileException("Symbol file `{0}' does not match assembly", new object[]
				{
					mdbFilename
				});
			}
			return monoSymbolFile;
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x000B044C File Offset: 0x000AE64C
		public static MonoSymbolFile ReadSymbolFile(Stream stream)
		{
			return new MonoSymbolFile(stream);
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x060024DF RID: 9439 RVA: 0x000B0454 File Offset: 0x000AE654
		public int CompileUnitCount
		{
			get
			{
				return this.ot.CompileUnitCount;
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x060024E0 RID: 9440 RVA: 0x000B0461 File Offset: 0x000AE661
		public int SourceCount
		{
			get
			{
				return this.ot.SourceCount;
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x060024E1 RID: 9441 RVA: 0x000B046E File Offset: 0x000AE66E
		public int MethodCount
		{
			get
			{
				return this.ot.MethodCount;
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x060024E2 RID: 9442 RVA: 0x000B047B File Offset: 0x000AE67B
		public int TypeCount
		{
			get
			{
				return this.ot.TypeCount;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x060024E3 RID: 9443 RVA: 0x000B0488 File Offset: 0x000AE688
		public int AnonymousScopeCount
		{
			get
			{
				return this.ot.AnonymousScopeCount;
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x060024E4 RID: 9444 RVA: 0x000B0495 File Offset: 0x000AE695
		public int NamespaceCount
		{
			get
			{
				return this.last_namespace_index;
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x060024E5 RID: 9445 RVA: 0x000B049D File Offset: 0x000AE69D
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x060024E6 RID: 9446 RVA: 0x000B04A5 File Offset: 0x000AE6A5
		public OffsetTable OffsetTable
		{
			get
			{
				return this.ot;
			}
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x000B04B0 File Offset: 0x000AE6B0
		public SourceFileEntry GetSourceFile(int index)
		{
			if (index < 1 || index > this.ot.SourceCount)
			{
				throw new ArgumentException();
			}
			if (this.reader == null)
			{
				throw new InvalidOperationException();
			}
			SourceFileEntry result;
			lock (this)
			{
				SourceFileEntry sourceFileEntry;
				if (this.source_file_hash.TryGetValue(index, out sourceFileEntry))
				{
					result = sourceFileEntry;
				}
				else
				{
					long position = this.reader.BaseStream.Position;
					this.reader.BaseStream.Position = (long)(this.ot.SourceTableOffset + SourceFileEntry.Size * (index - 1));
					sourceFileEntry = new SourceFileEntry(this, this.reader);
					this.source_file_hash.Add(index, sourceFileEntry);
					this.reader.BaseStream.Position = position;
					result = sourceFileEntry;
				}
			}
			return result;
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x060024E8 RID: 9448 RVA: 0x000B0580 File Offset: 0x000AE780
		public SourceFileEntry[] Sources
		{
			get
			{
				if (this.reader == null)
				{
					throw new InvalidOperationException();
				}
				SourceFileEntry[] array = new SourceFileEntry[this.SourceCount];
				for (int i = 0; i < this.SourceCount; i++)
				{
					array[i] = this.GetSourceFile(i + 1);
				}
				return array;
			}
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x000B05C8 File Offset: 0x000AE7C8
		public CompileUnitEntry GetCompileUnit(int index)
		{
			if (index < 1 || index > this.ot.CompileUnitCount)
			{
				throw new ArgumentException();
			}
			if (this.reader == null)
			{
				throw new InvalidOperationException();
			}
			CompileUnitEntry result;
			lock (this)
			{
				CompileUnitEntry compileUnitEntry;
				if (this.compile_unit_hash.TryGetValue(index, out compileUnitEntry))
				{
					result = compileUnitEntry;
				}
				else
				{
					long position = this.reader.BaseStream.Position;
					this.reader.BaseStream.Position = (long)(this.ot.CompileUnitTableOffset + CompileUnitEntry.Size * (index - 1));
					compileUnitEntry = new CompileUnitEntry(this, this.reader);
					this.compile_unit_hash.Add(index, compileUnitEntry);
					this.reader.BaseStream.Position = position;
					result = compileUnitEntry;
				}
			}
			return result;
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x060024EA RID: 9450 RVA: 0x000B0698 File Offset: 0x000AE898
		public CompileUnitEntry[] CompileUnits
		{
			get
			{
				if (this.reader == null)
				{
					throw new InvalidOperationException();
				}
				CompileUnitEntry[] array = new CompileUnitEntry[this.CompileUnitCount];
				for (int i = 0; i < this.CompileUnitCount; i++)
				{
					array[i] = this.GetCompileUnit(i + 1);
				}
				return array;
			}
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x000B06E0 File Offset: 0x000AE8E0
		private void read_methods()
		{
			lock (this)
			{
				if (this.method_token_hash == null)
				{
					this.method_token_hash = new Dictionary<int, MethodEntry>();
					this.method_list = new List<MethodEntry>();
					long position = this.reader.BaseStream.Position;
					this.reader.BaseStream.Position = (long)this.ot.MethodTableOffset;
					for (int i = 0; i < this.MethodCount; i++)
					{
						MethodEntry methodEntry = new MethodEntry(this, this.reader, i + 1);
						this.method_token_hash.Add(methodEntry.Token, methodEntry);
						this.method_list.Add(methodEntry);
					}
					this.reader.BaseStream.Position = position;
				}
			}
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x000B07B0 File Offset: 0x000AE9B0
		public MethodEntry GetMethodByToken(int token)
		{
			if (this.reader == null)
			{
				throw new InvalidOperationException();
			}
			MethodEntry result;
			lock (this)
			{
				this.read_methods();
				MethodEntry methodEntry;
				this.method_token_hash.TryGetValue(token, out methodEntry);
				result = methodEntry;
			}
			return result;
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x000B0804 File Offset: 0x000AEA04
		public MethodEntry GetMethod(int index)
		{
			if (index < 1 || index > this.ot.MethodCount)
			{
				throw new ArgumentException();
			}
			if (this.reader == null)
			{
				throw new InvalidOperationException();
			}
			MethodEntry result;
			lock (this)
			{
				this.read_methods();
				result = this.method_list[index - 1];
			}
			return result;
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x060024EE RID: 9454 RVA: 0x000B0870 File Offset: 0x000AEA70
		public MethodEntry[] Methods
		{
			get
			{
				if (this.reader == null)
				{
					throw new InvalidOperationException();
				}
				MethodEntry[] result;
				lock (this)
				{
					this.read_methods();
					MethodEntry[] array = new MethodEntry[this.MethodCount];
					this.method_list.CopyTo(array, 0);
					result = array;
				}
				return result;
			}
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x000B08D0 File Offset: 0x000AEAD0
		public int FindSource(string file_name)
		{
			if (this.reader == null)
			{
				throw new InvalidOperationException();
			}
			int result;
			lock (this)
			{
				if (this.source_name_hash == null)
				{
					this.source_name_hash = new Dictionary<string, int>();
					for (int i = 0; i < this.ot.SourceCount; i++)
					{
						SourceFileEntry sourceFile = this.GetSourceFile(i + 1);
						this.source_name_hash.Add(sourceFile.FileName, i);
					}
				}
				int num;
				if (!this.source_name_hash.TryGetValue(file_name, out num))
				{
					result = -1;
				}
				else
				{
					result = num;
				}
			}
			return result;
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x000B096C File Offset: 0x000AEB6C
		public AnonymousScopeEntry GetAnonymousScope(int id)
		{
			if (this.reader == null)
			{
				throw new InvalidOperationException();
			}
			AnonymousScopeEntry result;
			lock (this)
			{
				if (this.anonymous_scopes != null)
				{
					AnonymousScopeEntry anonymousScopeEntry;
					this.anonymous_scopes.TryGetValue(id, out anonymousScopeEntry);
					result = anonymousScopeEntry;
				}
				else
				{
					this.anonymous_scopes = new Dictionary<int, AnonymousScopeEntry>();
					this.reader.BaseStream.Position = (long)this.ot.AnonymousScopeTableOffset;
					for (int i = 0; i < this.ot.AnonymousScopeCount; i++)
					{
						AnonymousScopeEntry anonymousScopeEntry = new AnonymousScopeEntry(this.reader);
						this.anonymous_scopes.Add(anonymousScopeEntry.ID, anonymousScopeEntry);
					}
					result = this.anonymous_scopes[id];
				}
			}
			return result;
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x060024F1 RID: 9457 RVA: 0x000B0A2C File Offset: 0x000AEC2C
		internal MyBinaryReader BinaryReader
		{
			get
			{
				if (this.reader == null)
				{
					throw new InvalidOperationException();
				}
				return this.reader;
			}
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x000B0A42 File Offset: 0x000AEC42
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x000B0A4B File Offset: 0x000AEC4B
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.reader != null)
			{
				this.reader.Close();
				this.reader = null;
			}
		}

		// Token: 0x04000D9E RID: 3486
		private List<MethodEntry> methods = new List<MethodEntry>();

		// Token: 0x04000D9F RID: 3487
		private List<SourceFileEntry> sources = new List<SourceFileEntry>();

		// Token: 0x04000DA0 RID: 3488
		private List<CompileUnitEntry> comp_units = new List<CompileUnitEntry>();

		// Token: 0x04000DA1 RID: 3489
		private Dictionary<int, AnonymousScopeEntry> anonymous_scopes;

		// Token: 0x04000DA2 RID: 3490
		private OffsetTable ot;

		// Token: 0x04000DA3 RID: 3491
		private int last_type_index;

		// Token: 0x04000DA4 RID: 3492
		private int last_method_index;

		// Token: 0x04000DA5 RID: 3493
		private int last_namespace_index;

		// Token: 0x04000DA6 RID: 3494
		public readonly int MajorVersion = 50;

		// Token: 0x04000DA7 RID: 3495
		public readonly int MinorVersion;

		// Token: 0x04000DA8 RID: 3496
		public int NumLineNumbers;

		// Token: 0x04000DA9 RID: 3497
		private MyBinaryReader reader;

		// Token: 0x04000DAA RID: 3498
		private Dictionary<int, SourceFileEntry> source_file_hash;

		// Token: 0x04000DAB RID: 3499
		private Dictionary<int, CompileUnitEntry> compile_unit_hash;

		// Token: 0x04000DAC RID: 3500
		private List<MethodEntry> method_list;

		// Token: 0x04000DAD RID: 3501
		private Dictionary<int, MethodEntry> method_token_hash;

		// Token: 0x04000DAE RID: 3502
		private Dictionary<string, int> source_name_hash;

		// Token: 0x04000DAF RID: 3503
		private Guid guid;

		// Token: 0x04000DB0 RID: 3504
		internal int LineNumberCount;

		// Token: 0x04000DB1 RID: 3505
		internal int LocalCount;

		// Token: 0x04000DB2 RID: 3506
		internal int StringSize;

		// Token: 0x04000DB3 RID: 3507
		internal int LineNumberSize;

		// Token: 0x04000DB4 RID: 3508
		internal int ExtendedLineNumberSize;
	}
}
