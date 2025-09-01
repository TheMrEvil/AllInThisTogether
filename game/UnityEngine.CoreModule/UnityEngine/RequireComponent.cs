using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001EF RID: 495
	[RequiredByNativeCode]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class RequireComponent : Attribute
	{
		// Token: 0x06001659 RID: 5721 RVA: 0x00023E5A File Offset: 0x0002205A
		public RequireComponent(Type requiredComponent)
		{
			this.m_Type0 = requiredComponent;
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x00023E6B File Offset: 0x0002206B
		public RequireComponent(Type requiredComponent, Type requiredComponent2)
		{
			this.m_Type0 = requiredComponent;
			this.m_Type1 = requiredComponent2;
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x00023E83 File Offset: 0x00022083
		public RequireComponent(Type requiredComponent, Type requiredComponent2, Type requiredComponent3)
		{
			this.m_Type0 = requiredComponent;
			this.m_Type1 = requiredComponent2;
			this.m_Type2 = requiredComponent3;
		}

		// Token: 0x040007CF RID: 1999
		public Type m_Type0;

		// Token: 0x040007D0 RID: 2000
		public Type m_Type1;

		// Token: 0x040007D1 RID: 2001
		public Type m_Type2;
	}
}
