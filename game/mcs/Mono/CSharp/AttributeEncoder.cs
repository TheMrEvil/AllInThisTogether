using System;
using System.Text;

namespace Mono.CSharp
{
	// Token: 0x02000122 RID: 290
	public sealed class AttributeEncoder
	{
		// Token: 0x06000E58 RID: 3672 RVA: 0x000366E0 File Offset: 0x000348E0
		static AttributeEncoder()
		{
			AttributeEncoder.Empty[0] = 1;
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x000366F5 File Offset: 0x000348F5
		public AttributeEncoder()
		{
			this.buffer = new byte[32];
			this.Encode(1);
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00036711 File Offset: 0x00034911
		public void Encode(bool value)
		{
			this.Encode(value ? 1 : 0);
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00036720 File Offset: 0x00034920
		public void Encode(byte value)
		{
			if (this.pos == this.buffer.Length)
			{
				this.Grow(1);
			}
			byte[] array = this.buffer;
			int num = this.pos;
			this.pos = num + 1;
			array[num] = value;
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x0003675D File Offset: 0x0003495D
		public void Encode(sbyte value)
		{
			this.Encode((byte)value);
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00036768 File Offset: 0x00034968
		public void Encode(short value)
		{
			if (this.pos + 2 > this.buffer.Length)
			{
				this.Grow(2);
			}
			byte[] array = this.buffer;
			int num = this.pos;
			this.pos = num + 1;
			array[num] = (byte)value;
			byte[] array2 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			array2[num] = (byte)(value >> 8);
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x000367C4 File Offset: 0x000349C4
		public void Encode(ushort value)
		{
			this.Encode((short)value);
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x000367D0 File Offset: 0x000349D0
		public void Encode(int value)
		{
			if (this.pos + 4 > this.buffer.Length)
			{
				this.Grow(4);
			}
			byte[] array = this.buffer;
			int num = this.pos;
			this.pos = num + 1;
			array[num] = (byte)value;
			byte[] array2 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			array2[num] = (byte)(value >> 8);
			byte[] array3 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			array3[num] = (byte)(value >> 16);
			byte[] array4 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			array4[num] = (byte)(value >> 24);
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00036866 File Offset: 0x00034A66
		public void Encode(uint value)
		{
			this.Encode((int)value);
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00036870 File Offset: 0x00034A70
		public void Encode(long value)
		{
			if (this.pos + 8 > this.buffer.Length)
			{
				this.Grow(8);
			}
			byte[] array = this.buffer;
			int num = this.pos;
			this.pos = num + 1;
			array[num] = (byte)value;
			byte[] array2 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			array2[num] = (byte)(value >> 8);
			byte[] array3 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			array3[num] = (byte)(value >> 16);
			byte[] array4 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			array4[num] = (byte)(value >> 24);
			byte[] array5 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			array5[num] = (byte)(value >> 32);
			byte[] array6 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			array6[num] = (byte)(value >> 40);
			byte[] array7 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			array7[num] = (byte)(value >> 48);
			byte[] array8 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			array8[num] = (byte)(value >> 56);
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x0003697A File Offset: 0x00034B7A
		public void Encode(ulong value)
		{
			this.Encode((long)value);
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00036983 File Offset: 0x00034B83
		public void Encode(float value)
		{
			this.Encode(SingleConverter.SingleToInt32Bits(value));
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x00036991 File Offset: 0x00034B91
		public void Encode(double value)
		{
			this.Encode(BitConverter.DoubleToInt64Bits(value));
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x000369A0 File Offset: 0x00034BA0
		public void Encode(string value)
		{
			if (value == null)
			{
				this.Encode(byte.MaxValue);
				return;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			this.WriteCompressedValue(bytes.Length);
			if (this.pos + bytes.Length > this.buffer.Length)
			{
				this.Grow(bytes.Length);
			}
			Buffer.BlockCopy(bytes, 0, this.buffer, this.pos, bytes.Length);
			this.pos += bytes.Length;
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00036A14 File Offset: 0x00034C14
		public AttributeEncoder.EncodedTypeProperties Encode(TypeSpec type)
		{
			switch (type.BuiltinType)
			{
			case BuiltinTypeSpec.Type.FirstPrimitive:
				this.Encode(2);
				return AttributeEncoder.EncodedTypeProperties.None;
			case BuiltinTypeSpec.Type.Byte:
				this.Encode(5);
				return AttributeEncoder.EncodedTypeProperties.None;
			case BuiltinTypeSpec.Type.SByte:
				this.Encode(4);
				return AttributeEncoder.EncodedTypeProperties.None;
			case BuiltinTypeSpec.Type.Char:
				this.Encode(3);
				return AttributeEncoder.EncodedTypeProperties.None;
			case BuiltinTypeSpec.Type.Short:
				this.Encode(6);
				return AttributeEncoder.EncodedTypeProperties.None;
			case BuiltinTypeSpec.Type.UShort:
				this.Encode(7);
				return AttributeEncoder.EncodedTypeProperties.None;
			case BuiltinTypeSpec.Type.Int:
				this.Encode(8);
				return AttributeEncoder.EncodedTypeProperties.None;
			case BuiltinTypeSpec.Type.UInt:
				this.Encode(9);
				return AttributeEncoder.EncodedTypeProperties.None;
			case BuiltinTypeSpec.Type.Long:
				this.Encode(10);
				return AttributeEncoder.EncodedTypeProperties.None;
			case BuiltinTypeSpec.Type.ULong:
				this.Encode(11);
				return AttributeEncoder.EncodedTypeProperties.None;
			case BuiltinTypeSpec.Type.Float:
				this.Encode(12);
				return AttributeEncoder.EncodedTypeProperties.None;
			case BuiltinTypeSpec.Type.Double:
				this.Encode(13);
				return AttributeEncoder.EncodedTypeProperties.None;
			case BuiltinTypeSpec.Type.Object:
				this.Encode(81);
				return AttributeEncoder.EncodedTypeProperties.None;
			case BuiltinTypeSpec.Type.Dynamic:
				this.Encode(81);
				return AttributeEncoder.EncodedTypeProperties.DynamicType;
			case BuiltinTypeSpec.Type.String:
				this.Encode(14);
				return AttributeEncoder.EncodedTypeProperties.None;
			case BuiltinTypeSpec.Type.Type:
				this.Encode(80);
				return AttributeEncoder.EncodedTypeProperties.None;
			}
			if (type.IsArray)
			{
				this.Encode(29);
				return this.Encode(TypeManager.GetElementType(type));
			}
			if (type.Kind == MemberKind.Enum)
			{
				this.Encode(85);
				this.EncodeTypeName(type);
			}
			return AttributeEncoder.EncodedTypeProperties.None;
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x00036B6C File Offset: 0x00034D6C
		public void EncodeTypeName(TypeSpec type)
		{
			Type metaInfo = type.GetMetaInfo();
			this.Encode(type.MemberDefinition.IsImported ? metaInfo.AssemblyQualifiedName : metaInfo.FullName);
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x00036BA1 File Offset: 0x00034DA1
		public void EncodeTypeName(TypeContainer type)
		{
			this.Encode(type.GetSignatureForMetadata());
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x00036BAF File Offset: 0x00034DAF
		public void EncodeNamedPropertyArgument(PropertySpec property, Constant value)
		{
			this.Encode(1);
			this.Encode(84);
			this.Encode(property.MemberType);
			this.Encode(property.Name);
			value.EncodeAttributeValue(null, this, property.MemberType, property.MemberType);
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x00036BED File Offset: 0x00034DED
		public void EncodeNamedFieldArgument(FieldSpec field, Constant value)
		{
			this.Encode(1);
			this.Encode(83);
			this.Encode(field.MemberType);
			this.Encode(field.Name);
			value.EncodeAttributeValue(null, this, field.MemberType, field.MemberType);
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x00036C2C File Offset: 0x00034E2C
		public void EncodeNamedArguments<T>(T[] members, Constant[] values) where T : MemberSpec, IInterfaceMemberSpec
		{
			this.Encode((ushort)members.Length);
			for (int i = 0; i < members.Length; i++)
			{
				T t = members[i];
				if (t.Kind == MemberKind.Field)
				{
					this.Encode(83);
				}
				else
				{
					if (t.Kind != MemberKind.Property)
					{
						throw new NotImplementedException(t.Kind.ToString());
					}
					this.Encode(84);
				}
				this.Encode(t.MemberType);
				this.Encode(t.Name);
				values[i].EncodeAttributeValue(null, this, t.MemberType, t.MemberType);
			}
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x00036CF2 File Offset: 0x00034EF2
		public void EncodeEmptyNamedArguments()
		{
			this.Encode(0);
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x00036CFC File Offset: 0x00034EFC
		private void Grow(int inc)
		{
			int newSize = Math.Max(this.pos * 4, this.pos + inc + 2);
			Array.Resize<byte>(ref this.buffer, newSize);
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x00036D2D File Offset: 0x00034F2D
		private void WriteCompressedValue(int value)
		{
			if (value < 128)
			{
				this.Encode((byte)value);
				return;
			}
			if (value < 16384)
			{
				this.Encode((byte)(128 | value >> 8));
				this.Encode((byte)value);
				return;
			}
			this.Encode(value);
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x00036D68 File Offset: 0x00034F68
		public byte[] ToArray()
		{
			byte[] array = new byte[this.pos];
			Array.Copy(this.buffer, array, this.pos);
			return array;
		}

		// Token: 0x04000692 RID: 1682
		public static readonly byte[] Empty = new byte[4];

		// Token: 0x04000693 RID: 1683
		private byte[] buffer;

		// Token: 0x04000694 RID: 1684
		private int pos;

		// Token: 0x04000695 RID: 1685
		private const ushort Version = 1;

		// Token: 0x02000384 RID: 900
		[Flags]
		public enum EncodedTypeProperties
		{
			// Token: 0x04000F4F RID: 3919
			None = 0,
			// Token: 0x04000F50 RID: 3920
			DynamicType = 1,
			// Token: 0x04000F51 RID: 3921
			TypeParameter = 2
		}
	}
}
