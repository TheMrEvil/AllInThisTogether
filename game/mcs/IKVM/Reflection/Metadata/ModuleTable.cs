using System;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000AD RID: 173
	internal sealed class ModuleTable : Table<ModuleTable.Record>
	{
		// Token: 0x060008D8 RID: 2264 RVA: 0x0001E820 File Offset: 0x0001CA20
		internal override void Read(MetadataReader mr)
		{
			for (int i = 0; i < this.records.Length; i++)
			{
				this.records[i].Generation = mr.ReadInt16();
				this.records[i].Name = mr.ReadStringIndex();
				this.records[i].Mvid = mr.ReadGuidIndex();
				this.records[i].EncId = mr.ReadGuidIndex();
				this.records[i].EncBaseId = mr.ReadGuidIndex();
			}
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0001E8B8 File Offset: 0x0001CAB8
		internal override void Write(MetadataWriter mw)
		{
			for (int i = 0; i < this.rowCount; i++)
			{
				mw.Write(this.records[i].Generation);
				mw.WriteStringIndex(this.records[i].Name);
				mw.WriteGuidIndex(this.records[i].Mvid);
				mw.WriteGuidIndex(this.records[i].EncId);
				mw.WriteGuidIndex(this.records[i].EncBaseId);
			}
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0001E949 File Offset: 0x0001CB49
		protected override int GetRowSize(Table.RowSizeCalc rsc)
		{
			return rsc.AddFixed(2).WriteStringIndex().WriteGuidIndex().WriteGuidIndex().WriteGuidIndex().Value;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0001E96C File Offset: 0x0001CB6C
		internal void Add(short generation, int name, int mvid, int encid, int encbaseid)
		{
			base.AddRecord(new ModuleTable.Record
			{
				Generation = generation,
				Name = name,
				Mvid = mvid,
				EncId = encid,
				EncBaseId = encbaseid
			});
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0001E9B3 File Offset: 0x0001CBB3
		public ModuleTable()
		{
		}

		// Token: 0x040003CB RID: 971
		internal const int Index = 0;

		// Token: 0x02000344 RID: 836
		internal struct Record
		{
			// Token: 0x04000E95 RID: 3733
			internal short Generation;

			// Token: 0x04000E96 RID: 3734
			internal int Name;

			// Token: 0x04000E97 RID: 3735
			internal int Mvid;

			// Token: 0x04000E98 RID: 3736
			internal int EncId;

			// Token: 0x04000E99 RID: 3737
			internal int EncBaseId;
		}
	}
}
