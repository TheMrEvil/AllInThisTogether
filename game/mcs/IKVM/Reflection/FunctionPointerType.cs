using System;

namespace IKVM.Reflection
{
	// Token: 0x02000064 RID: 100
	internal sealed class FunctionPointerType : TypeInfo
	{
		// Token: 0x060005BC RID: 1468 RVA: 0x000116E4 File Offset: 0x0000F8E4
		internal static Type Make(Universe universe, __StandAloneMethodSig sig)
		{
			return universe.CanonicalizeType(new FunctionPointerType(universe, sig));
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x000116F3 File Offset: 0x0000F8F3
		private FunctionPointerType(Universe universe, __StandAloneMethodSig sig) : base(27)
		{
			this.universe = universe;
			this.sig = sig;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0001170C File Offset: 0x0000F90C
		public override bool Equals(object obj)
		{
			FunctionPointerType functionPointerType = obj as FunctionPointerType;
			return functionPointerType != null && functionPointerType.universe == this.universe && functionPointerType.sig.Equals(this.sig);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0001174A File Offset: 0x0000F94A
		public override int GetHashCode()
		{
			return this.sig.GetHashCode();
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x00011757 File Offset: 0x0000F957
		public override __StandAloneMethodSig __MethodSignature
		{
			get
			{
				return this.sig;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x000055E7 File Offset: 0x000037E7
		public override Type BaseType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x000022F4 File Offset: 0x000004F4
		public override TypeAttributes Attributes
		{
			get
			{
				return TypeAttributes.AnsiClass;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public override string Name
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public override string FullName
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public override Module Module
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0001175F File Offset: 0x0000F95F
		internal override Universe Universe
		{
			get
			{
				return this.universe;
			}
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00011767 File Offset: 0x0000F967
		public override string ToString()
		{
			return "<FunctionPtr>";
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0001176E File Offset: 0x0000F96E
		protected override bool ContainsMissingTypeImpl
		{
			get
			{
				return this.sig.ContainsMissingType;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0000212D File Offset: 0x0000032D
		internal override bool IsBaked
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0400020A RID: 522
		private readonly Universe universe;

		// Token: 0x0400020B RID: 523
		private readonly __StandAloneMethodSig sig;
	}
}
