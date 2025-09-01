using System;

namespace Mono
{
	// Token: 0x0200004C RID: 76
	internal struct RuntimePropertyHandle
	{
		// Token: 0x06000112 RID: 274 RVA: 0x000048CC File Offset: 0x00002ACC
		internal RuntimePropertyHandle(IntPtr v)
		{
			this.value = v;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000113 RID: 275 RVA: 0x000048D5 File Offset: 0x00002AD5
		public IntPtr Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000048E0 File Offset: 0x00002AE0
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.value == ((RuntimePropertyHandle)obj).Value;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004928 File Offset: 0x00002B28
		public bool Equals(RuntimePropertyHandle handle)
		{
			return this.value == handle.Value;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000493C File Offset: 0x00002B3C
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004949 File Offset: 0x00002B49
		public static bool operator ==(RuntimePropertyHandle left, RuntimePropertyHandle right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004953 File Offset: 0x00002B53
		public static bool operator !=(RuntimePropertyHandle left, RuntimePropertyHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000DE3 RID: 3555
		private IntPtr value;
	}
}
