using System;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000C9 RID: 201
	internal sealed class ImplMapTable : SortedTable<ImplMapTable.Record>
	{
		// Token: 0x06000955 RID: 2389 RVA: 0x000205FC File Offset: 0x0001E7FC
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].MappingFlags = mr.ReadInt16();
				this.records[i].MemberForwarded = mr.ReadMemberForwarded();
				this.records[i].ImportName = mr.ReadStringIndex();
				this.records[i].ImportScope = mr.ReadModuleRef();
			}
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00020678 File Offset: 0x0001E878
		internal override void Write(MetadataWriter mw)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.Write(this.records[i].MappingFlags);
				mw.WriteMemberForwarded(this.records[i].MemberForwarded);
				mw.WriteStringIndex(this.records[i].ImportName);
				mw.WriteModuleRef(this.records[i].ImportScope);
			}
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x000206F2 File Offset: 0x0001E8F2
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.AddFixed(2).WriteMemberForwarded().WriteStringIndex().WriteModuleRef().Value;
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00020710 File Offset: 0x0001E910
		internal void Fixup(ModuleBuilder moduleBuilder)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				moduleBuilder.FixupPseudoToken(ref this.records[i].MemberForwarded);
			}
			base.Sort();
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0002074B File Offset: 0x0001E94B
		public ImplMapTable()
		{
		}

		// Token: 0x040003EE RID: 1006
		internal const int Index = 28;

		// Token: 0x02000358 RID: 856
		internal struct Record : SortedTable<ImplMapTable.Record>.IRecord
		{
			// Token: 0x170008BC RID: 2236
			// (get) Token: 0x06002632 RID: 9778 RVA: 0x000B56AF File Offset: 0x000B38AF
			int SortedTable<ImplMapTable.Record>.IRecord.SortKey
			{
				get
				{
					return this.MemberForwarded;
				}
			}

			// Token: 0x170008BD RID: 2237
			// (get) Token: 0x06002633 RID: 9779 RVA: 0x000B56AF File Offset: 0x000B38AF
			int SortedTable<ImplMapTable.Record>.IRecord.FilterKey
			{
				get
				{
					return this.MemberForwarded;
				}
			}

			// Token: 0x04000ED4 RID: 3796
			internal short MappingFlags;

			// Token: 0x04000ED5 RID: 3797
			internal int MemberForwarded;

			// Token: 0x04000ED6 RID: 3798
			internal int ImportName;

			// Token: 0x04000ED7 RID: 3799
			internal int ImportScope;
		}
	}
}
