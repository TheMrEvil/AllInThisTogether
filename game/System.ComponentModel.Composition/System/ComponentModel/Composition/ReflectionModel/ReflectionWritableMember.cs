using System;

namespace System.ComponentModel.Composition.ReflectionModel
{
	// Token: 0x02000088 RID: 136
	internal abstract class ReflectionWritableMember : ReflectionMember
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600039D RID: 925
		public abstract bool CanWrite { get; }

		// Token: 0x0600039E RID: 926
		public abstract void SetValue(object instance, object value);

		// Token: 0x0600039F RID: 927 RVA: 0x0000AB75 File Offset: 0x00008D75
		protected ReflectionWritableMember()
		{
		}
	}
}
