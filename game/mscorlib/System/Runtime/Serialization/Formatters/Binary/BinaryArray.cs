using System;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020006A1 RID: 1697
	internal sealed class BinaryArray : IStreamable
	{
		// Token: 0x06003E5E RID: 15966 RVA: 0x0000259F File Offset: 0x0000079F
		internal BinaryArray()
		{
		}

		// Token: 0x06003E5F RID: 15967 RVA: 0x000D746A File Offset: 0x000D566A
		internal BinaryArray(BinaryHeaderEnum binaryHeaderEnum)
		{
			this.binaryHeaderEnum = binaryHeaderEnum;
		}

		// Token: 0x06003E60 RID: 15968 RVA: 0x000D747C File Offset: 0x000D567C
		internal void Set(int objectId, int rank, int[] lengthA, int[] lowerBoundA, BinaryTypeEnum binaryTypeEnum, object typeInformation, BinaryArrayTypeEnum binaryArrayTypeEnum, int assemId)
		{
			this.objectId = objectId;
			this.binaryArrayTypeEnum = binaryArrayTypeEnum;
			this.rank = rank;
			this.lengthA = lengthA;
			this.lowerBoundA = lowerBoundA;
			this.binaryTypeEnum = binaryTypeEnum;
			this.typeInformation = typeInformation;
			this.assemId = assemId;
			this.binaryHeaderEnum = BinaryHeaderEnum.Array;
			if (binaryArrayTypeEnum == BinaryArrayTypeEnum.Single)
			{
				if (binaryTypeEnum == BinaryTypeEnum.Primitive)
				{
					this.binaryHeaderEnum = BinaryHeaderEnum.ArraySinglePrimitive;
					return;
				}
				if (binaryTypeEnum == BinaryTypeEnum.String)
				{
					this.binaryHeaderEnum = BinaryHeaderEnum.ArraySingleString;
					return;
				}
				if (binaryTypeEnum == BinaryTypeEnum.Object)
				{
					this.binaryHeaderEnum = BinaryHeaderEnum.ArraySingleObject;
				}
			}
		}

		// Token: 0x06003E61 RID: 15969 RVA: 0x000D74FC File Offset: 0x000D56FC
		public void Write(__BinaryWriter sout)
		{
			switch (this.binaryHeaderEnum)
			{
			case BinaryHeaderEnum.ArraySinglePrimitive:
				sout.WriteByte((byte)this.binaryHeaderEnum);
				sout.WriteInt32(this.objectId);
				sout.WriteInt32(this.lengthA[0]);
				sout.WriteByte((byte)((InternalPrimitiveTypeE)this.typeInformation));
				return;
			case BinaryHeaderEnum.ArraySingleObject:
				sout.WriteByte((byte)this.binaryHeaderEnum);
				sout.WriteInt32(this.objectId);
				sout.WriteInt32(this.lengthA[0]);
				return;
			case BinaryHeaderEnum.ArraySingleString:
				sout.WriteByte((byte)this.binaryHeaderEnum);
				sout.WriteInt32(this.objectId);
				sout.WriteInt32(this.lengthA[0]);
				return;
			default:
				sout.WriteByte((byte)this.binaryHeaderEnum);
				sout.WriteInt32(this.objectId);
				sout.WriteByte((byte)this.binaryArrayTypeEnum);
				sout.WriteInt32(this.rank);
				for (int i = 0; i < this.rank; i++)
				{
					sout.WriteInt32(this.lengthA[i]);
				}
				if (this.binaryArrayTypeEnum == BinaryArrayTypeEnum.SingleOffset || this.binaryArrayTypeEnum == BinaryArrayTypeEnum.JaggedOffset || this.binaryArrayTypeEnum == BinaryArrayTypeEnum.RectangularOffset)
				{
					for (int j = 0; j < this.rank; j++)
					{
						sout.WriteInt32(this.lowerBoundA[j]);
					}
				}
				sout.WriteByte((byte)this.binaryTypeEnum);
				BinaryConverter.WriteTypeInfo(this.binaryTypeEnum, this.typeInformation, this.assemId, sout);
				return;
			}
		}

		// Token: 0x06003E62 RID: 15970 RVA: 0x000D7664 File Offset: 0x000D5864
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			switch (this.binaryHeaderEnum)
			{
			case BinaryHeaderEnum.ArraySinglePrimitive:
				this.objectId = input.ReadInt32();
				this.lengthA = new int[1];
				this.lengthA[0] = input.ReadInt32();
				this.binaryArrayTypeEnum = BinaryArrayTypeEnum.Single;
				this.rank = 1;
				this.lowerBoundA = new int[this.rank];
				this.binaryTypeEnum = BinaryTypeEnum.Primitive;
				this.typeInformation = (InternalPrimitiveTypeE)input.ReadByte();
				return;
			case BinaryHeaderEnum.ArraySingleObject:
				this.objectId = input.ReadInt32();
				this.lengthA = new int[1];
				this.lengthA[0] = input.ReadInt32();
				this.binaryArrayTypeEnum = BinaryArrayTypeEnum.Single;
				this.rank = 1;
				this.lowerBoundA = new int[this.rank];
				this.binaryTypeEnum = BinaryTypeEnum.Object;
				this.typeInformation = null;
				return;
			case BinaryHeaderEnum.ArraySingleString:
				this.objectId = input.ReadInt32();
				this.lengthA = new int[1];
				this.lengthA[0] = input.ReadInt32();
				this.binaryArrayTypeEnum = BinaryArrayTypeEnum.Single;
				this.rank = 1;
				this.lowerBoundA = new int[this.rank];
				this.binaryTypeEnum = BinaryTypeEnum.String;
				this.typeInformation = null;
				return;
			default:
				this.objectId = input.ReadInt32();
				this.binaryArrayTypeEnum = (BinaryArrayTypeEnum)input.ReadByte();
				this.rank = input.ReadInt32();
				this.lengthA = new int[this.rank];
				this.lowerBoundA = new int[this.rank];
				for (int i = 0; i < this.rank; i++)
				{
					this.lengthA[i] = input.ReadInt32();
				}
				if (this.binaryArrayTypeEnum == BinaryArrayTypeEnum.SingleOffset || this.binaryArrayTypeEnum == BinaryArrayTypeEnum.JaggedOffset || this.binaryArrayTypeEnum == BinaryArrayTypeEnum.RectangularOffset)
				{
					for (int j = 0; j < this.rank; j++)
					{
						this.lowerBoundA[j] = input.ReadInt32();
					}
				}
				this.binaryTypeEnum = (BinaryTypeEnum)input.ReadByte();
				this.typeInformation = BinaryConverter.ReadTypeInfo(this.binaryTypeEnum, input, out this.assemId);
				return;
			}
		}

		// Token: 0x04002872 RID: 10354
		internal int objectId;

		// Token: 0x04002873 RID: 10355
		internal int rank;

		// Token: 0x04002874 RID: 10356
		internal int[] lengthA;

		// Token: 0x04002875 RID: 10357
		internal int[] lowerBoundA;

		// Token: 0x04002876 RID: 10358
		internal BinaryTypeEnum binaryTypeEnum;

		// Token: 0x04002877 RID: 10359
		internal object typeInformation;

		// Token: 0x04002878 RID: 10360
		internal int assemId;

		// Token: 0x04002879 RID: 10361
		private BinaryHeaderEnum binaryHeaderEnum;

		// Token: 0x0400287A RID: 10362
		internal BinaryArrayTypeEnum binaryArrayTypeEnum;
	}
}
