using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020001D9 RID: 473
	internal ref struct ByReference<T>
	{
		// Token: 0x060014A1 RID: 5281 RVA: 0x000472C8 File Offset: 0x000454C8
		[Intrinsic]
		public ByReference(ref T value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x000472C8 File Offset: 0x000454C8
		public ref T Value
		{
			[Intrinsic]
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0400146E RID: 5230
		private IntPtr _value;
	}
}
