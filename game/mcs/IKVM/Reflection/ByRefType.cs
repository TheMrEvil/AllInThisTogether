using System;

namespace IKVM.Reflection
{
	// Token: 0x02000061 RID: 97
	internal sealed class ByRefType : ElementHolderType
	{
		// Token: 0x06000587 RID: 1415 RVA: 0x00010FCB File Offset: 0x0000F1CB
		internal static Type Make(Type type, CustomModifiers mods)
		{
			return type.Universe.CanonicalizeType(new ByRefType(type, mods));
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00010FDF File Offset: 0x0000F1DF
		private ByRefType(Type type, CustomModifiers mods) : base(type, mods, 16)
		{
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00010FEB File Offset: 0x0000F1EB
		public override bool Equals(object o)
		{
			return base.EqualsHelper(o as ByRefType);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00010FF9 File Offset: 0x0000F1F9
		public override int GetHashCode()
		{
			return this.elementType.GetHashCode() * 3;
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x000055E7 File Offset: 0x000037E7
		public override Type BaseType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x000022F4 File Offset: 0x000004F4
		public override TypeAttributes Attributes
		{
			get
			{
				return TypeAttributes.AnsiClass;
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00011008 File Offset: 0x0000F208
		internal override string GetSuffix()
		{
			return "&";
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0001100F File Offset: 0x0000F20F
		protected override Type Wrap(Type type, CustomModifiers mods)
		{
			return ByRefType.Make(type, mods);
		}
	}
}
