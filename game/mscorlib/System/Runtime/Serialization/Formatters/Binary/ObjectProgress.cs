using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020006A7 RID: 1703
	internal sealed class ObjectProgress
	{
		// Token: 0x06003E82 RID: 16002 RVA: 0x000D7C38 File Offset: 0x000D5E38
		internal ObjectProgress()
		{
		}

		// Token: 0x06003E83 RID: 16003 RVA: 0x000D7C54 File Offset: 0x000D5E54
		[Conditional("SER_LOGGING")]
		private void Counter()
		{
			lock (this)
			{
				this.opRecordId = ObjectProgress.opRecordIdCount++;
				if (ObjectProgress.opRecordIdCount > 1000)
				{
					ObjectProgress.opRecordIdCount = 1;
				}
			}
		}

		// Token: 0x06003E84 RID: 16004 RVA: 0x000D7CB0 File Offset: 0x000D5EB0
		internal void Init()
		{
			this.isInitial = false;
			this.count = 0;
			this.expectedType = BinaryTypeEnum.ObjectUrt;
			this.expectedTypeInformation = null;
			this.name = null;
			this.objectTypeEnum = InternalObjectTypeE.Empty;
			this.memberTypeEnum = InternalMemberTypeE.Empty;
			this.memberValueEnum = InternalMemberValueE.Empty;
			this.dtType = null;
			this.numItems = 0;
			this.nullCount = 0;
			this.typeInformation = null;
			this.memberLength = 0;
			this.binaryTypeEnumA = null;
			this.typeInformationA = null;
			this.memberNames = null;
			this.memberTypes = null;
			this.pr.Init();
		}

		// Token: 0x06003E85 RID: 16005 RVA: 0x000D7D3F File Offset: 0x000D5F3F
		internal void ArrayCountIncrement(int value)
		{
			this.count += value;
		}

		// Token: 0x06003E86 RID: 16006 RVA: 0x000D7D50 File Offset: 0x000D5F50
		internal bool GetNext(out BinaryTypeEnum outBinaryTypeEnum, out object outTypeInformation)
		{
			outBinaryTypeEnum = BinaryTypeEnum.Primitive;
			outTypeInformation = null;
			if (this.objectTypeEnum == InternalObjectTypeE.Array)
			{
				if (this.count == this.numItems)
				{
					return false;
				}
				outBinaryTypeEnum = this.binaryTypeEnum;
				outTypeInformation = this.typeInformation;
				if (this.count == 0)
				{
					this.isInitial = false;
				}
				this.count++;
				return true;
			}
			else
			{
				if (this.count == this.memberLength && !this.isInitial)
				{
					return false;
				}
				outBinaryTypeEnum = this.binaryTypeEnumA[this.count];
				outTypeInformation = this.typeInformationA[this.count];
				if (this.count == 0)
				{
					this.isInitial = false;
				}
				this.name = this.memberNames[this.count];
				Type[] array = this.memberTypes;
				this.dtType = this.memberTypes[this.count];
				this.count++;
				return true;
			}
		}

		// Token: 0x06003E87 RID: 16007 RVA: 0x000D7E2C File Offset: 0x000D602C
		// Note: this type is marked as 'beforefieldinit'.
		static ObjectProgress()
		{
		}

		// Token: 0x0400288A RID: 10378
		internal static int opRecordIdCount = 1;

		// Token: 0x0400288B RID: 10379
		internal int opRecordId;

		// Token: 0x0400288C RID: 10380
		internal bool isInitial;

		// Token: 0x0400288D RID: 10381
		internal int count;

		// Token: 0x0400288E RID: 10382
		internal BinaryTypeEnum expectedType = BinaryTypeEnum.ObjectUrt;

		// Token: 0x0400288F RID: 10383
		internal object expectedTypeInformation;

		// Token: 0x04002890 RID: 10384
		internal string name;

		// Token: 0x04002891 RID: 10385
		internal InternalObjectTypeE objectTypeEnum;

		// Token: 0x04002892 RID: 10386
		internal InternalMemberTypeE memberTypeEnum;

		// Token: 0x04002893 RID: 10387
		internal InternalMemberValueE memberValueEnum;

		// Token: 0x04002894 RID: 10388
		internal Type dtType;

		// Token: 0x04002895 RID: 10389
		internal int numItems;

		// Token: 0x04002896 RID: 10390
		internal BinaryTypeEnum binaryTypeEnum;

		// Token: 0x04002897 RID: 10391
		internal object typeInformation;

		// Token: 0x04002898 RID: 10392
		internal int nullCount;

		// Token: 0x04002899 RID: 10393
		internal int memberLength;

		// Token: 0x0400289A RID: 10394
		internal BinaryTypeEnum[] binaryTypeEnumA;

		// Token: 0x0400289B RID: 10395
		internal object[] typeInformationA;

		// Token: 0x0400289C RID: 10396
		internal string[] memberNames;

		// Token: 0x0400289D RID: 10397
		internal Type[] memberTypes;

		// Token: 0x0400289E RID: 10398
		internal ParseRecord pr = new ParseRecord();
	}
}
