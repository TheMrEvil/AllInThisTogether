using System;

namespace Mono
{
	// Token: 0x0200004B RID: 75
	internal struct RuntimeEventHandle
	{
		// Token: 0x0600010B RID: 267 RVA: 0x0000483A File Offset: 0x00002A3A
		internal RuntimeEventHandle(IntPtr v)
		{
			this.value = v;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00004843 File Offset: 0x00002A43
		public IntPtr Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000484C File Offset: 0x00002A4C
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.value == ((RuntimeEventHandle)obj).Value;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004894 File Offset: 0x00002A94
		public bool Equals(RuntimeEventHandle handle)
		{
			return this.value == handle.Value;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000048A8 File Offset: 0x00002AA8
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000048B5 File Offset: 0x00002AB5
		public static bool operator ==(RuntimeEventHandle left, RuntimeEventHandle right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000048BF File Offset: 0x00002ABF
		public static bool operator !=(RuntimeEventHandle left, RuntimeEventHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000DE2 RID: 3554
		private IntPtr value;
	}
}
