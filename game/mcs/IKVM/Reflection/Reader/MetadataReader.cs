using System;
using System.IO;
using IKVM.Reflection.Metadata;

namespace IKVM.Reflection.Reader
{
	// Token: 0x02000098 RID: 152
	internal sealed class MetadataReader : MetadataRW
	{
		// Token: 0x060007E0 RID: 2016 RVA: 0x00019B41 File Offset: 0x00017D41
		internal MetadataReader(ModuleReader module, Stream stream, byte heapSizes) : base(module, (heapSizes & 1) > 0, (heapSizes & 2) > 0, (heapSizes & 4) > 0)
		{
			this.stream = stream;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00019B80 File Offset: 0x00017D80
		private void FillBuffer(int needed)
		{
			int i = 2048 - this.pos;
			if (i != 0)
			{
				Buffer.BlockCopy(this.buffer, this.pos, this.buffer, 0, i);
			}
			this.pos = 0;
			while (i < needed)
			{
				int num = this.stream.Read(this.buffer, i, 2048 - i);
				if (num == 0)
				{
					throw new BadImageFormatException();
				}
				i += num;
			}
			if (i != 2048)
			{
				Buffer.BlockCopy(this.buffer, 0, this.buffer, 2048 - i, i);
				this.pos = 2048 - i;
			}
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00019C19 File Offset: 0x00017E19
		internal ushort ReadUInt16()
		{
			return (ushort)this.ReadInt16();
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00019C24 File Offset: 0x00017E24
		internal short ReadInt16()
		{
			if (this.pos > 2046)
			{
				this.FillBuffer(2);
			}
			byte[] array = this.buffer;
			int num = this.pos;
			this.pos = num + 1;
			int num2 = array[num];
			byte[] array2 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			byte b = array2[num];
			return (short)(num2 | (int)b << 8);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00019C7C File Offset: 0x00017E7C
		internal int ReadInt32()
		{
			if (this.pos > 2044)
			{
				this.FillBuffer(4);
			}
			byte[] array = this.buffer;
			int num = this.pos;
			this.pos = num + 1;
			int num2 = array[num];
			byte[] array2 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			byte b = array2[num];
			byte[] array3 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			byte b2 = array3[num];
			byte[] array4 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			byte b3 = array4[num];
			return num2 | (int)b << 8 | (int)b2 << 16 | (int)b3 << 24;
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00019D0E File Offset: 0x00017F0E
		private int ReadIndex(bool big)
		{
			if (big)
			{
				return this.ReadInt32();
			}
			return (int)this.ReadUInt16();
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00019D20 File Offset: 0x00017F20
		internal int ReadStringIndex()
		{
			return this.ReadIndex(this.bigStrings);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00019D2E File Offset: 0x00017F2E
		internal int ReadGuidIndex()
		{
			return this.ReadIndex(this.bigGuids);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00019D3C File Offset: 0x00017F3C
		internal int ReadBlobIndex()
		{
			return this.ReadIndex(this.bigBlobs);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00019D4C File Offset: 0x00017F4C
		internal int ReadResolutionScope()
		{
			int num = this.ReadIndex(this.bigResolutionScope);
			switch (num & 3)
			{
			case 0:
				return (0 << 24) + (num >> 2);
			case 1:
				return (26 << 24) + (num >> 2);
			case 2:
				return (35 << 24) + (num >> 2);
			case 3:
				return (1 << 24) + (num >> 2);
			default:
				throw new BadImageFormatException();
			}
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00019DB0 File Offset: 0x00017FB0
		internal int ReadTypeDefOrRef()
		{
			int num = this.ReadIndex(this.bigTypeDefOrRef);
			switch (num & 3)
			{
			case 0:
				return (2 << 24) + (num >> 2);
			case 1:
				return (1 << 24) + (num >> 2);
			case 2:
				return (27 << 24) + (num >> 2);
			default:
				throw new BadImageFormatException();
			}
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00019E04 File Offset: 0x00018004
		internal int ReadMemberRefParent()
		{
			int num = this.ReadIndex(this.bigMemberRefParent);
			switch (num & 7)
			{
			case 0:
				return (2 << 24) + (num >> 3);
			case 1:
				return (1 << 24) + (num >> 3);
			case 2:
				return (26 << 24) + (num >> 3);
			case 3:
				return (6 << 24) + (num >> 3);
			case 4:
				return (27 << 24) + (num >> 3);
			default:
				throw new BadImageFormatException();
			}
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00019E74 File Offset: 0x00018074
		internal int ReadHasCustomAttribute()
		{
			int num = this.ReadIndex(this.bigHasCustomAttribute);
			switch (num & 31)
			{
			case 0:
				return (6 << 24) + (num >> 5);
			case 1:
				return (4 << 24) + (num >> 5);
			case 2:
				return (1 << 24) + (num >> 5);
			case 3:
				return (2 << 24) + (num >> 5);
			case 4:
				return (8 << 24) + (num >> 5);
			case 5:
				return (9 << 24) + (num >> 5);
			case 6:
				return (10 << 24) + (num >> 5);
			case 7:
				return (0 << 24) + (num >> 5);
			case 8:
				throw new BadImageFormatException();
			case 9:
				return (23 << 24) + (num >> 5);
			case 10:
				return (20 << 24) + (num >> 5);
			case 11:
				return (17 << 24) + (num >> 5);
			case 12:
				return (26 << 24) + (num >> 5);
			case 13:
				return (27 << 24) + (num >> 5);
			case 14:
				return (32 << 24) + (num >> 5);
			case 15:
				return (35 << 24) + (num >> 5);
			case 16:
				return (38 << 24) + (num >> 5);
			case 17:
				return (39 << 24) + (num >> 5);
			case 18:
				return (40 << 24) + (num >> 5);
			case 19:
				return (42 << 24) + (num >> 5);
			default:
				throw new BadImageFormatException();
			}
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00019FB4 File Offset: 0x000181B4
		internal int ReadCustomAttributeType()
		{
			int num = this.ReadIndex(this.bigCustomAttributeType);
			int num2 = num & 7;
			if (num2 == 2)
			{
				return (6 << 24) + (num >> 3);
			}
			if (num2 != 3)
			{
				throw new BadImageFormatException();
			}
			return (10 << 24) + (num >> 3);
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00019FF4 File Offset: 0x000181F4
		internal int ReadMethodDefOrRef()
		{
			int num = this.ReadIndex(this.bigMethodDefOrRef);
			int num2 = num & 1;
			if (num2 == 0)
			{
				return (6 << 24) + (num >> 1);
			}
			if (num2 != 1)
			{
				throw new BadImageFormatException();
			}
			return (10 << 24) + (num >> 1);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0001A034 File Offset: 0x00018234
		internal int ReadHasConstant()
		{
			int num = this.ReadIndex(this.bigHasConstant);
			switch (num & 3)
			{
			case 0:
				return (4 << 24) + (num >> 2);
			case 1:
				return (8 << 24) + (num >> 2);
			case 2:
				return (23 << 24) + (num >> 2);
			default:
				throw new BadImageFormatException();
			}
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0001A088 File Offset: 0x00018288
		internal int ReadHasSemantics()
		{
			int num = this.ReadIndex(this.bigHasSemantics);
			int num2 = num & 1;
			if (num2 == 0)
			{
				return (20 << 24) + (num >> 1);
			}
			if (num2 != 1)
			{
				throw new BadImageFormatException();
			}
			return (23 << 24) + (num >> 1);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0001A0C8 File Offset: 0x000182C8
		internal int ReadHasFieldMarshal()
		{
			int num = this.ReadIndex(this.bigHasFieldMarshal);
			int num2 = num & 1;
			if (num2 == 0)
			{
				return (4 << 24) + (num >> 1);
			}
			if (num2 != 1)
			{
				throw new BadImageFormatException();
			}
			return (8 << 24) + (num >> 1);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0001A108 File Offset: 0x00018308
		internal int ReadHasDeclSecurity()
		{
			int num = this.ReadIndex(this.bigHasDeclSecurity);
			switch (num & 3)
			{
			case 0:
				return (2 << 24) + (num >> 2);
			case 1:
				return (6 << 24) + (num >> 2);
			case 2:
				return (32 << 24) + (num >> 2);
			default:
				throw new BadImageFormatException();
			}
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0001A15C File Offset: 0x0001835C
		internal int ReadTypeOrMethodDef()
		{
			int num = this.ReadIndex(this.bigTypeOrMethodDef);
			int num2 = num & 1;
			if (num2 == 0)
			{
				return (2 << 24) + (num >> 1);
			}
			if (num2 != 1)
			{
				throw new BadImageFormatException();
			}
			return (6 << 24) + (num >> 1);
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0001A19C File Offset: 0x0001839C
		internal int ReadMemberForwarded()
		{
			int num = this.ReadIndex(this.bigMemberForwarded);
			int num2 = num & 1;
			if (num2 == 0)
			{
				return (4 << 24) + (num >> 1);
			}
			if (num2 != 1)
			{
				throw new BadImageFormatException();
			}
			return (6 << 24) + (num >> 1);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001A1DC File Offset: 0x000183DC
		internal int ReadImplementation()
		{
			int num = this.ReadIndex(this.bigImplementation);
			switch (num & 3)
			{
			case 0:
				return (38 << 24) + (num >> 2);
			case 1:
				return (35 << 24) + (num >> 2);
			case 2:
				return (39 << 24) + (num >> 2);
			default:
				throw new BadImageFormatException();
			}
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001A231 File Offset: 0x00018431
		internal int ReadField()
		{
			return this.ReadIndex(this.bigField);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0001A23F File Offset: 0x0001843F
		internal int ReadMethodDef()
		{
			return this.ReadIndex(this.bigMethodDef);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0001A24D File Offset: 0x0001844D
		internal int ReadParam()
		{
			return this.ReadIndex(this.bigParam);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0001A25B File Offset: 0x0001845B
		internal int ReadProperty()
		{
			return this.ReadIndex(this.bigProperty);
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0001A269 File Offset: 0x00018469
		internal int ReadEvent()
		{
			return this.ReadIndex(this.bigEvent);
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0001A277 File Offset: 0x00018477
		internal int ReadTypeDef()
		{
			return this.ReadIndex(this.bigTypeDef) | 33554432;
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0001A28B File Offset: 0x0001848B
		internal int ReadGenericParam()
		{
			return this.ReadIndex(this.bigGenericParam) | 704643072;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001A29F File Offset: 0x0001849F
		internal int ReadModuleRef()
		{
			return this.ReadIndex(this.bigModuleRef) | 436207616;
		}

		// Token: 0x04000316 RID: 790
		private readonly Stream stream;

		// Token: 0x04000317 RID: 791
		private const int bufferLength = 2048;

		// Token: 0x04000318 RID: 792
		private readonly byte[] buffer = new byte[2048];

		// Token: 0x04000319 RID: 793
		private int pos = 2048;
	}
}
