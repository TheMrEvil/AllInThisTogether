using System;
using System.Reflection;

namespace System.Runtime.Serialization
{
	// Token: 0x0200065A RID: 1626
	internal sealed class ValueTypeFixupInfo
	{
		// Token: 0x06003CB8 RID: 15544 RVA: 0x000D1DB4 File Offset: 0x000CFFB4
		public ValueTypeFixupInfo(long containerID, FieldInfo member, int[] parentIndex)
		{
			if (member == null && parentIndex == null)
			{
				throw new ArgumentException("When supplying the ID of a containing object, the FieldInfo that identifies the current field within that object must also be supplied.");
			}
			if (containerID == 0L && member == null)
			{
				this._containerID = containerID;
				this._parentField = member;
				this._parentIndex = parentIndex;
			}
			if (member != null)
			{
				if (parentIndex != null)
				{
					throw new ArgumentException("Cannot supply both a MemberInfo and an Array to indicate the parent of a value type.");
				}
				if (member.FieldType.IsValueType && containerID == 0L)
				{
					throw new ArgumentException("When supplying a FieldInfo for fixing up a nested type, a valid ID for that containing object must also be supplied.");
				}
			}
			this._containerID = containerID;
			this._parentField = member;
			this._parentIndex = parentIndex;
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06003CB9 RID: 15545 RVA: 0x000D1E46 File Offset: 0x000D0046
		public long ContainerID
		{
			get
			{
				return this._containerID;
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06003CBA RID: 15546 RVA: 0x000D1E4E File Offset: 0x000D004E
		public FieldInfo ParentField
		{
			get
			{
				return this._parentField;
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06003CBB RID: 15547 RVA: 0x000D1E56 File Offset: 0x000D0056
		public int[] ParentIndex
		{
			get
			{
				return this._parentIndex;
			}
		}

		// Token: 0x0400272E RID: 10030
		private readonly long _containerID;

		// Token: 0x0400272F RID: 10031
		private readonly FieldInfo _parentField;

		// Token: 0x04002730 RID: 10032
		private readonly int[] _parentIndex;
	}
}
