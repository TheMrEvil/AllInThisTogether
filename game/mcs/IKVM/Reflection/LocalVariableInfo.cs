using System;

namespace IKVM.Reflection
{
	// Token: 0x02000035 RID: 53
	public class LocalVariableInfo
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x000081E5 File Offset: 0x000063E5
		internal LocalVariableInfo(int index, Type type, bool pinned)
		{
			this.index = index;
			this.type = type;
			this.pinned = pinned;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00008202 File Offset: 0x00006402
		internal LocalVariableInfo(int index, Type type, bool pinned, CustomModifiers customModifiers) : this(index, type, pinned)
		{
			this.customModifiers = customModifiers;
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00008215 File Offset: 0x00006415
		public bool IsPinned
		{
			get
			{
				return this.pinned;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000821D File Offset: 0x0000641D
		public int LocalIndex
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00008225 File Offset: 0x00006425
		public Type LocalType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000822D File Offset: 0x0000642D
		public CustomModifiers __GetCustomModifiers()
		{
			return this.customModifiers;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00008235 File Offset: 0x00006435
		public override string ToString()
		{
			return string.Format(this.pinned ? "{0} ({1}) (pinned)" : "{0} ({1})", this.type, this.index);
		}

		// Token: 0x0400014A RID: 330
		private readonly int index;

		// Token: 0x0400014B RID: 331
		private readonly Type type;

		// Token: 0x0400014C RID: 332
		private readonly bool pinned;

		// Token: 0x0400014D RID: 333
		private readonly CustomModifiers customModifiers;
	}
}
