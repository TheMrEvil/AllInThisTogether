using System;
using System.Collections.Generic;
using System.IO;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x0200031A RID: 794
	public class CompileUnitEntry : ICompileUnit
	{
		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x0600251B RID: 9499 RVA: 0x00008E8B File Offset: 0x0000708B
		public static int Size
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x0600251C RID: 9500 RVA: 0x00005936 File Offset: 0x00003B36
		CompileUnitEntry ICompileUnit.Entry
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x000B1383 File Offset: 0x000AF583
		public CompileUnitEntry(MonoSymbolFile file, SourceFileEntry source)
		{
			this.file = file;
			this.source = source;
			this.Index = file.AddCompileUnit(this);
			this.creating = true;
			this.namespaces = new List<NamespaceEntry>();
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x000B13B8 File Offset: 0x000AF5B8
		public void AddFile(SourceFileEntry file)
		{
			if (!this.creating)
			{
				throw new InvalidOperationException();
			}
			if (this.include_files == null)
			{
				this.include_files = new List<SourceFileEntry>();
			}
			this.include_files.Add(file);
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x0600251F RID: 9503 RVA: 0x000B13E7 File Offset: 0x000AF5E7
		public SourceFileEntry SourceFile
		{
			get
			{
				if (this.creating)
				{
					return this.source;
				}
				this.ReadData();
				return this.source;
			}
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x000B1404 File Offset: 0x000AF604
		public int DefineNamespace(string name, string[] using_clauses, int parent)
		{
			if (!this.creating)
			{
				throw new InvalidOperationException();
			}
			int nextNamespaceIndex = this.file.GetNextNamespaceIndex();
			NamespaceEntry item = new NamespaceEntry(name, nextNamespaceIndex, using_clauses, parent);
			this.namespaces.Add(item);
			return nextNamespaceIndex;
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x000B1444 File Offset: 0x000AF644
		internal void WriteData(MyBinaryWriter bw)
		{
			this.DataOffset = (int)bw.BaseStream.Position;
			bw.WriteLeb128(this.source.Index);
			int value = (this.include_files != null) ? this.include_files.Count : 0;
			bw.WriteLeb128(value);
			if (this.include_files != null)
			{
				foreach (SourceFileEntry sourceFileEntry in this.include_files)
				{
					bw.WriteLeb128(sourceFileEntry.Index);
				}
			}
			bw.WriteLeb128(this.namespaces.Count);
			foreach (NamespaceEntry namespaceEntry in this.namespaces)
			{
				namespaceEntry.Write(this.file, bw);
			}
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x000B1540 File Offset: 0x000AF740
		internal void Write(BinaryWriter bw)
		{
			bw.Write(this.Index);
			bw.Write(this.DataOffset);
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x000B155A File Offset: 0x000AF75A
		internal CompileUnitEntry(MonoSymbolFile file, MyBinaryReader reader)
		{
			this.file = file;
			this.Index = reader.ReadInt32();
			this.DataOffset = reader.ReadInt32();
		}

		// Token: 0x06002524 RID: 9508 RVA: 0x000B1581 File Offset: 0x000AF781
		public void ReadAll()
		{
			this.ReadData();
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x000B158C File Offset: 0x000AF78C
		private void ReadData()
		{
			if (this.creating)
			{
				throw new InvalidOperationException();
			}
			MonoSymbolFile obj = this.file;
			lock (obj)
			{
				if (this.namespaces == null)
				{
					MyBinaryReader binaryReader = this.file.BinaryReader;
					int num = (int)binaryReader.BaseStream.Position;
					binaryReader.BaseStream.Position = (long)this.DataOffset;
					int index = binaryReader.ReadLeb128();
					this.source = this.file.GetSourceFile(index);
					int num2 = binaryReader.ReadLeb128();
					if (num2 > 0)
					{
						this.include_files = new List<SourceFileEntry>();
						for (int i = 0; i < num2; i++)
						{
							this.include_files.Add(this.file.GetSourceFile(binaryReader.ReadLeb128()));
						}
					}
					int num3 = binaryReader.ReadLeb128();
					this.namespaces = new List<NamespaceEntry>();
					for (int j = 0; j < num3; j++)
					{
						this.namespaces.Add(new NamespaceEntry(this.file, binaryReader));
					}
					binaryReader.BaseStream.Position = (long)num;
				}
			}
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06002526 RID: 9510 RVA: 0x000B16AC File Offset: 0x000AF8AC
		public NamespaceEntry[] Namespaces
		{
			get
			{
				this.ReadData();
				NamespaceEntry[] array = new NamespaceEntry[this.namespaces.Count];
				this.namespaces.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06002527 RID: 9511 RVA: 0x000B16E0 File Offset: 0x000AF8E0
		public SourceFileEntry[] IncludeFiles
		{
			get
			{
				this.ReadData();
				if (this.include_files == null)
				{
					return new SourceFileEntry[0];
				}
				SourceFileEntry[] array = new SourceFileEntry[this.include_files.Count];
				this.include_files.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04000DE6 RID: 3558
		public readonly int Index;

		// Token: 0x04000DE7 RID: 3559
		private int DataOffset;

		// Token: 0x04000DE8 RID: 3560
		private MonoSymbolFile file;

		// Token: 0x04000DE9 RID: 3561
		private SourceFileEntry source;

		// Token: 0x04000DEA RID: 3562
		private List<SourceFileEntry> include_files;

		// Token: 0x04000DEB RID: 3563
		private List<NamespaceEntry> namespaces;

		// Token: 0x04000DEC RID: 3564
		private bool creating;
	}
}
