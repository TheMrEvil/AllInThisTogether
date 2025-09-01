using System;

namespace IKVM.Reflection
{
	// Token: 0x02000065 RID: 101
	internal sealed class MarkerType : Type
	{
		// Token: 0x060005CA RID: 1482 RVA: 0x0001177B File Offset: 0x0000F97B
		private MarkerType(byte sigElementType) : base(sigElementType)
		{
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public override Type BaseType
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public override TypeAttributes Attributes
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public override string Name
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public override string FullName
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public override Module Module
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x00002CD4 File Offset: 0x00000ED4
		internal override bool IsBaked
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool __IsMissing
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00011784 File Offset: 0x0000F984
		// Note: this type is marked as 'beforefieldinit'.
		static MarkerType()
		{
		}

		// Token: 0x0400020C RID: 524
		internal static readonly Type ModOpt = new MarkerType(32);

		// Token: 0x0400020D RID: 525
		internal static readonly Type ModReq = new MarkerType(31);

		// Token: 0x0400020E RID: 526
		internal static readonly Type Sentinel = new MarkerType(65);

		// Token: 0x0400020F RID: 527
		internal static readonly Type Pinned = new MarkerType(69);
	}
}
