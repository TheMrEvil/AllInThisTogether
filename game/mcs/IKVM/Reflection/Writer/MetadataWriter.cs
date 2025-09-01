using System;
using System.IO;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Metadata;

namespace IKVM.Reflection.Writer
{
	// Token: 0x02000084 RID: 132
	internal sealed class MetadataWriter : MetadataRW
	{
		// Token: 0x060006DE RID: 1758 RVA: 0x00014C74 File Offset: 0x00012E74
		internal MetadataWriter(ModuleBuilder module, Stream stream) : base(module, module.Strings.IsBig, module.Guids.IsBig, module.Blobs.IsBig)
		{
			this.moduleBuilder = module;
			this.stream = stream;
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x00014CC3 File Offset: 0x00012EC3
		internal ModuleBuilder ModuleBuilder
		{
			get
			{
				return this.moduleBuilder;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x00014CCB File Offset: 0x00012ECB
		internal int Position
		{
			get
			{
				return (int)this.stream.Position;
			}
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00014CD9 File Offset: 0x00012ED9
		internal void Write(ByteBuffer bb)
		{
			bb.WriteTo(this.stream);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00014CE8 File Offset: 0x00012EE8
		internal void WriteAsciiz(string value)
		{
			foreach (char c in value)
			{
				this.stream.WriteByte((byte)c);
			}
			this.stream.WriteByte(0);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00014D29 File Offset: 0x00012F29
		internal void Write(byte[] value)
		{
			this.stream.Write(value, 0, value.Length);
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00014D3B File Offset: 0x00012F3B
		internal void Write(byte[] buffer, int offset, int count)
		{
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00014D4B File Offset: 0x00012F4B
		internal void Write(byte value)
		{
			this.stream.WriteByte(value);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00014D59 File Offset: 0x00012F59
		internal void Write(ushort value)
		{
			this.Write((short)value);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00014D63 File Offset: 0x00012F63
		internal void Write(short value)
		{
			this.stream.WriteByte((byte)value);
			this.stream.WriteByte((byte)(value >> 8));
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00014D81 File Offset: 0x00012F81
		internal void Write(uint value)
		{
			this.Write((int)value);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00014D8C File Offset: 0x00012F8C
		internal void Write(int value)
		{
			this.buffer[0] = (byte)value;
			this.buffer[1] = (byte)(value >> 8);
			this.buffer[2] = (byte)(value >> 16);
			this.buffer[3] = (byte)(value >> 24);
			this.stream.Write(this.buffer, 0, 4);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00014DDC File Offset: 0x00012FDC
		internal void Write(ulong value)
		{
			this.Write((long)value);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00014DE8 File Offset: 0x00012FE8
		internal void Write(long value)
		{
			this.buffer[0] = (byte)value;
			this.buffer[1] = (byte)(value >> 8);
			this.buffer[2] = (byte)(value >> 16);
			this.buffer[3] = (byte)(value >> 24);
			this.buffer[4] = (byte)(value >> 32);
			this.buffer[5] = (byte)(value >> 40);
			this.buffer[6] = (byte)(value >> 48);
			this.buffer[7] = (byte)(value >> 56);
			this.stream.Write(this.buffer, 0, 8);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00014E6C File Offset: 0x0001306C
		internal void WriteCompressedUInt(int value)
		{
			if (value <= 127)
			{
				this.Write((byte)value);
				return;
			}
			if (value <= 16383)
			{
				this.Write((byte)(128 | value >> 8));
				this.Write((byte)value);
				return;
			}
			this.Write((byte)(192 | value >> 24));
			this.Write((byte)(value >> 16));
			this.Write((byte)(value >> 8));
			this.Write((byte)value);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00014ED6 File Offset: 0x000130D6
		internal static int GetCompressedUIntLength(int value)
		{
			if (value <= 127)
			{
				return 1;
			}
			if (value <= 16383)
			{
				return 2;
			}
			return 4;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00014EEA File Offset: 0x000130EA
		internal void WriteStringIndex(int index)
		{
			if (this.bigStrings)
			{
				this.Write(index);
				return;
			}
			this.Write((short)index);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00014F04 File Offset: 0x00013104
		internal void WriteGuidIndex(int index)
		{
			if (this.bigGuids)
			{
				this.Write(index);
				return;
			}
			this.Write((short)index);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00014F1E File Offset: 0x0001311E
		internal void WriteBlobIndex(int index)
		{
			if (this.bigBlobs)
			{
				this.Write(index);
				return;
			}
			this.Write((short)index);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00014F38 File Offset: 0x00013138
		internal void WriteTypeDefOrRef(int token)
		{
			int num = token >> 24;
			switch (num)
			{
			case 0:
				break;
			case 1:
				token = ((token & 16777215) << 2 | 1);
				break;
			case 2:
				token = ((token & 16777215) << 2 | 0);
				break;
			default:
				if (num != 27)
				{
					throw new InvalidOperationException();
				}
				token = ((token & 16777215) << 2 | 2);
				break;
			}
			if (this.bigTypeDefOrRef)
			{
				this.Write(token);
				return;
			}
			this.Write((short)token);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00014FAE File Offset: 0x000131AE
		internal void WriteEncodedTypeDefOrRef(int encodedToken)
		{
			if (this.bigTypeDefOrRef)
			{
				this.Write(encodedToken);
				return;
			}
			this.Write((short)encodedToken);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00014FC8 File Offset: 0x000131C8
		internal void WriteHasCustomAttribute(int token)
		{
			int num = CustomAttributeTable.EncodeHasCustomAttribute(token);
			if (this.bigHasCustomAttribute)
			{
				this.Write(num);
				return;
			}
			this.Write((short)num);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00014FF4 File Offset: 0x000131F4
		internal void WriteCustomAttributeType(int token)
		{
			int num = token >> 24;
			if (num != 6)
			{
				if (num != 10)
				{
					throw new InvalidOperationException();
				}
				token = ((token & 16777215) << 3 | 3);
			}
			else
			{
				token = ((token & 16777215) << 3 | 2);
			}
			if (this.bigCustomAttributeType)
			{
				this.Write(token);
				return;
			}
			this.Write((short)token);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001504D File Offset: 0x0001324D
		internal void WriteField(int index)
		{
			if (this.bigField)
			{
				this.Write(index & 16777215);
				return;
			}
			this.Write((short)index);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001506D File Offset: 0x0001326D
		internal void WriteMethodDef(int index)
		{
			if (this.bigMethodDef)
			{
				this.Write(index & 16777215);
				return;
			}
			this.Write((short)index);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001508D File Offset: 0x0001328D
		internal void WriteParam(int index)
		{
			if (this.bigParam)
			{
				this.Write(index & 16777215);
				return;
			}
			this.Write((short)index);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x000150AD File Offset: 0x000132AD
		internal void WriteTypeDef(int index)
		{
			if (this.bigTypeDef)
			{
				this.Write(index & 16777215);
				return;
			}
			this.Write((short)index);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x000150CD File Offset: 0x000132CD
		internal void WriteEvent(int index)
		{
			if (this.bigEvent)
			{
				this.Write(index & 16777215);
				return;
			}
			this.Write((short)index);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x000150ED File Offset: 0x000132ED
		internal void WriteProperty(int index)
		{
			if (this.bigProperty)
			{
				this.Write(index & 16777215);
				return;
			}
			this.Write((short)index);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001510D File Offset: 0x0001330D
		internal void WriteGenericParam(int index)
		{
			if (this.bigGenericParam)
			{
				this.Write(index & 16777215);
				return;
			}
			this.Write((short)index);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001512D File Offset: 0x0001332D
		internal void WriteModuleRef(int index)
		{
			if (this.bigModuleRef)
			{
				this.Write(index & 16777215);
				return;
			}
			this.Write((short)index);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00015150 File Offset: 0x00013350
		internal void WriteResolutionScope(int token)
		{
			int num = token >> 24;
			if (num <= 1)
			{
				if (num == 0)
				{
					token = ((token & 16777215) << 2 | 0);
					goto IL_60;
				}
				if (num == 1)
				{
					token = ((token & 16777215) << 2 | 3);
					goto IL_60;
				}
			}
			else
			{
				if (num == 26)
				{
					token = ((token & 16777215) << 2 | 1);
					goto IL_60;
				}
				if (num == 35)
				{
					token = ((token & 16777215) << 2 | 2);
					goto IL_60;
				}
			}
			throw new InvalidOperationException();
			IL_60:
			if (this.bigResolutionScope)
			{
				this.Write(token);
				return;
			}
			this.Write((short)token);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x000151D8 File Offset: 0x000133D8
		internal void WriteMemberRefParent(int token)
		{
			int num = token >> 24;
			if (num <= 2)
			{
				if (num == 1)
				{
					token = ((token & 16777215) << 3 | 1);
					goto IL_74;
				}
				if (num == 2)
				{
					token = ((token & 16777215) << 3 | 0);
					goto IL_74;
				}
			}
			else
			{
				if (num == 6)
				{
					token = ((token & 16777215) << 3 | 3);
					goto IL_74;
				}
				if (num == 26)
				{
					token = ((token & 16777215) << 3 | 2);
					goto IL_74;
				}
				if (num == 27)
				{
					token = ((token & 16777215) << 3 | 4);
					goto IL_74;
				}
			}
			throw new InvalidOperationException();
			IL_74:
			if (this.bigMemberRefParent)
			{
				this.Write(token);
				return;
			}
			this.Write((short)token);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00015274 File Offset: 0x00013474
		internal void WriteMethodDefOrRef(int token)
		{
			int num = token >> 24;
			if (num != 6)
			{
				if (num != 10)
				{
					throw new InvalidOperationException();
				}
				token = ((token & 16777215) << 1 | 1);
			}
			else
			{
				token = ((token & 16777215) << 1 | 0);
			}
			if (this.bigMethodDefOrRef)
			{
				this.Write(token);
				return;
			}
			this.Write((short)token);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x000152D0 File Offset: 0x000134D0
		internal void WriteHasConstant(int token)
		{
			int num = ConstantTable.EncodeHasConstant(token);
			if (this.bigHasConstant)
			{
				this.Write(num);
				return;
			}
			this.Write((short)num);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x000152FC File Offset: 0x000134FC
		internal void WriteHasSemantics(int encodedToken)
		{
			if (this.bigHasSemantics)
			{
				this.Write(encodedToken);
				return;
			}
			this.Write((short)encodedToken);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00015318 File Offset: 0x00013518
		internal void WriteImplementation(int token)
		{
			int num = token >> 24;
			if (num != 0)
			{
				switch (num)
				{
				case 35:
					token = ((token & 16777215) << 2 | 1);
					goto IL_5A;
				case 38:
					token = ((token & 16777215) << 2 | 0);
					goto IL_5A;
				case 39:
					token = ((token & 16777215) << 2 | 2);
					goto IL_5A;
				}
				throw new InvalidOperationException();
			}
			IL_5A:
			if (this.bigImplementation)
			{
				this.Write(token);
				return;
			}
			this.Write((short)token);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00015397 File Offset: 0x00013597
		internal void WriteTypeOrMethodDef(int encodedToken)
		{
			if (this.bigTypeOrMethodDef)
			{
				this.Write(encodedToken);
				return;
			}
			this.Write((short)encodedToken);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x000153B1 File Offset: 0x000135B1
		internal void WriteHasDeclSecurity(int encodedToken)
		{
			if (this.bigHasDeclSecurity)
			{
				this.Write(encodedToken);
				return;
			}
			this.Write((short)encodedToken);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x000153CC File Offset: 0x000135CC
		internal void WriteMemberForwarded(int token)
		{
			int num = token >> 24;
			if (num != 4)
			{
				if (num != 6)
				{
					throw new InvalidOperationException();
				}
				token = ((token & 16777215) << 1 | 1);
			}
			else
			{
				token = ((token & 16777215) << 1 | 0);
			}
			if (this.bigMemberForwarded)
			{
				this.Write(token);
				return;
			}
			this.Write((short)token);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00015424 File Offset: 0x00013624
		internal void WriteHasFieldMarshal(int token)
		{
			int num = FieldMarshalTable.EncodeHasFieldMarshal(token);
			if (this.bigHasFieldMarshal)
			{
				this.Write(num);
				return;
			}
			this.Write((short)num);
		}

		// Token: 0x0400028F RID: 655
		private readonly ModuleBuilder moduleBuilder;

		// Token: 0x04000290 RID: 656
		private readonly Stream stream;

		// Token: 0x04000291 RID: 657
		private readonly byte[] buffer = new byte[8];
	}
}
