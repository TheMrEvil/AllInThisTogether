using System;
using System.IO;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000A8 RID: 168
	internal sealed class CliHeader
	{
		// Token: 0x060008C1 RID: 2241 RVA: 0x0001E1BC File Offset: 0x0001C3BC
		internal void Read(BinaryReader br)
		{
			this.Cb = br.ReadUInt32();
			this.MajorRuntimeVersion = br.ReadUInt16();
			this.MinorRuntimeVersion = br.ReadUInt16();
			this.MetaData.Read(br);
			this.Flags = br.ReadUInt32();
			this.EntryPointToken = br.ReadUInt32();
			this.Resources.Read(br);
			this.StrongNameSignature.Read(br);
			this.CodeManagerTable.Read(br);
			this.VTableFixups.Read(br);
			this.ExportAddressTableJumps.Read(br);
			this.ManagedNativeHeader.Read(br);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0001E25C File Offset: 0x0001C45C
		internal void Write(MetadataWriter mw)
		{
			mw.Write(this.Cb);
			mw.Write(this.MajorRuntimeVersion);
			mw.Write(this.MinorRuntimeVersion);
			this.MetaData.Write(mw);
			mw.Write(this.Flags);
			mw.Write(this.EntryPointToken);
			this.Resources.Write(mw);
			this.StrongNameSignature.Write(mw);
			this.CodeManagerTable.Write(mw);
			this.VTableFixups.Write(mw);
			this.ExportAddressTableJumps.Write(mw);
			this.ManagedNativeHeader.Write(mw);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0001E2F9 File Offset: 0x0001C4F9
		public CliHeader()
		{
		}

		// Token: 0x0400039F RID: 927
		internal const uint COMIMAGE_FLAGS_ILONLY = 1U;

		// Token: 0x040003A0 RID: 928
		internal const uint COMIMAGE_FLAGS_32BITREQUIRED = 2U;

		// Token: 0x040003A1 RID: 929
		internal const uint COMIMAGE_FLAGS_STRONGNAMESIGNED = 8U;

		// Token: 0x040003A2 RID: 930
		internal const uint COMIMAGE_FLAGS_NATIVE_ENTRYPOINT = 16U;

		// Token: 0x040003A3 RID: 931
		internal const uint COMIMAGE_FLAGS_32BITPREFERRED = 131072U;

		// Token: 0x040003A4 RID: 932
		internal uint Cb = 72U;

		// Token: 0x040003A5 RID: 933
		internal ushort MajorRuntimeVersion;

		// Token: 0x040003A6 RID: 934
		internal ushort MinorRuntimeVersion;

		// Token: 0x040003A7 RID: 935
		internal RvaSize MetaData;

		// Token: 0x040003A8 RID: 936
		internal uint Flags;

		// Token: 0x040003A9 RID: 937
		internal uint EntryPointToken;

		// Token: 0x040003AA RID: 938
		internal RvaSize Resources;

		// Token: 0x040003AB RID: 939
		internal RvaSize StrongNameSignature;

		// Token: 0x040003AC RID: 940
		internal RvaSize CodeManagerTable;

		// Token: 0x040003AD RID: 941
		internal RvaSize VTableFixups;

		// Token: 0x040003AE RID: 942
		internal RvaSize ExportAddressTableJumps;

		// Token: 0x040003AF RID: 943
		internal RvaSize ManagedNativeHeader;
	}
}
