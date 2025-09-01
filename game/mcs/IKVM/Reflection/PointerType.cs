using System;

namespace IKVM.Reflection
{
	// Token: 0x02000062 RID: 98
	internal sealed class PointerType : ElementHolderType
	{
		// Token: 0x0600058F RID: 1423 RVA: 0x00011018 File Offset: 0x0000F218
		internal static Type Make(Type type, CustomModifiers mods)
		{
			return type.Universe.CanonicalizeType(new PointerType(type, mods));
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0001102C File Offset: 0x0000F22C
		private PointerType(Type type, CustomModifiers mods) : base(type, mods, 15)
		{
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00011038 File Offset: 0x0000F238
		public override bool Equals(object o)
		{
			return base.EqualsHelper(o as PointerType);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00011046 File Offset: 0x0000F246
		public override int GetHashCode()
		{
			return this.elementType.GetHashCode() * 7;
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x000055E7 File Offset: 0x000037E7
		public override Type BaseType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x000022F4 File Offset: 0x000004F4
		public override TypeAttributes Attributes
		{
			get
			{
				return TypeAttributes.AnsiClass;
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00011055 File Offset: 0x0000F255
		internal override string GetSuffix()
		{
			return "*";
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001105C File Offset: 0x0000F25C
		protected override Type Wrap(Type type, CustomModifiers mods)
		{
			return PointerType.Make(type, mods);
		}
	}
}
