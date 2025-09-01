using System;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000F2 RID: 242
	public struct EventToken
	{
		// Token: 0x06000BDC RID: 3036 RVA: 0x0002B11F File Offset: 0x0002931F
		internal EventToken(int token)
		{
			this.token = token;
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x0002B128 File Offset: 0x00029328
		public int Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0002B130 File Offset: 0x00029330
		public override bool Equals(object obj)
		{
			EventToken? eventToken = obj as EventToken?;
			EventToken et = this;
			return eventToken != null && (eventToken == null || eventToken.GetValueOrDefault() == et);
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0002B128 File Offset: 0x00029328
		public override int GetHashCode()
		{
			return this.token;
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0002B173 File Offset: 0x00029373
		public bool Equals(EventToken other)
		{
			return this == other;
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0002B181 File Offset: 0x00029381
		public static bool operator ==(EventToken et1, EventToken et2)
		{
			return et1.token == et2.token;
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0002B191 File Offset: 0x00029391
		public static bool operator !=(EventToken et1, EventToken et2)
		{
			return et1.token != et2.token;
		}

		// Token: 0x040005EE RID: 1518
		public static readonly EventToken Empty;

		// Token: 0x040005EF RID: 1519
		private readonly int token;
	}
}
