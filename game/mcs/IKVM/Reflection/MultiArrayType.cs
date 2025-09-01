using System;

namespace IKVM.Reflection
{
	// Token: 0x0200005F RID: 95
	internal sealed class MultiArrayType : ElementHolderType
	{
		// Token: 0x06000573 RID: 1395 RVA: 0x00010CC0 File Offset: 0x0000EEC0
		internal static Type Make(Type type, int rank, int[] sizes, int[] lobounds, CustomModifiers mods)
		{
			return type.Universe.CanonicalizeType(new MultiArrayType(type, rank, sizes, lobounds, mods));
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00010CD8 File Offset: 0x0000EED8
		private MultiArrayType(Type type, int rank, int[] sizes, int[] lobounds, CustomModifiers mods) : base(type, mods, 20)
		{
			this.rank = rank;
			this.sizes = sizes;
			this.lobounds = lobounds;
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00010A7B File Offset: 0x0000EC7B
		public override Type BaseType
		{
			get
			{
				return this.elementType.Module.universe.System_Array;
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00010CFC File Offset: 0x0000EEFC
		public override MethodBase[] __GetDeclaredMethods()
		{
			Type system_Int = this.Module.universe.System_Int32;
			Type[] array = new Type[this.rank + 1];
			Type[] array2 = new Type[this.rank];
			Type[] array3 = new Type[this.rank * 2];
			for (int i = 0; i < this.rank; i++)
			{
				array[i] = system_Int;
				array2[i] = system_Int;
				array3[i * 2 + 0] = system_Int;
				array3[i * 2 + 1] = system_Int;
			}
			array[this.rank] = this.elementType;
			return new MethodBase[]
			{
				new ConstructorInfoImpl(new BuiltinArrayMethod(this.Module, this, ".ctor", CallingConventions.Standard | CallingConventions.HasThis, this.Module.universe.System_Void, array2)),
				new ConstructorInfoImpl(new BuiltinArrayMethod(this.Module, this, ".ctor", CallingConventions.Standard | CallingConventions.HasThis, this.Module.universe.System_Void, array3)),
				new BuiltinArrayMethod(this.Module, this, "Set", CallingConventions.Standard | CallingConventions.HasThis, this.Module.universe.System_Void, array),
				new BuiltinArrayMethod(this.Module, this, "Address", CallingConventions.Standard | CallingConventions.HasThis, this.elementType.MakeByRefType(), array2),
				new BuiltinArrayMethod(this.Module, this, "Get", CallingConventions.Standard | CallingConventions.HasThis, this.elementType, array2)
			};
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00010C8C File Offset: 0x0000EE8C
		public override TypeAttributes Attributes
		{
			get
			{
				return TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.Serializable;
			}
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00010E45 File Offset: 0x0000F045
		public override int GetArrayRank()
		{
			return this.rank;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00010E4D File Offset: 0x0000F04D
		public override int[] __GetArraySizes()
		{
			return Util.Copy(this.sizes);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00010E5A File Offset: 0x0000F05A
		public override int[] __GetArrayLowerBounds()
		{
			return Util.Copy(this.lobounds);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00010E68 File Offset: 0x0000F068
		public override bool Equals(object o)
		{
			MultiArrayType multiArrayType = o as MultiArrayType;
			return base.EqualsHelper(multiArrayType) && multiArrayType.rank == this.rank && MultiArrayType.ArrayEquals(multiArrayType.sizes, this.sizes) && MultiArrayType.ArrayEquals(multiArrayType.lobounds, this.lobounds);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00010EBC File Offset: 0x0000F0BC
		private static bool ArrayEquals(int[] i1, int[] i2)
		{
			if (i1.Length == i2.Length)
			{
				for (int j = 0; j < i1.Length; j++)
				{
					if (i1[j] != i2[j])
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00010EEC File Offset: 0x0000F0EC
		public override int GetHashCode()
		{
			return this.elementType.GetHashCode() * 9 + this.rank;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00010F03 File Offset: 0x0000F103
		internal override string GetSuffix()
		{
			if (this.rank == 1)
			{
				return "[*]";
			}
			return "[" + new string(',', this.rank - 1) + "]";
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00010F32 File Offset: 0x0000F132
		protected override Type Wrap(Type type, CustomModifiers mods)
		{
			return MultiArrayType.Make(type, this.rank, this.sizes, this.lobounds, mods);
		}

		// Token: 0x04000202 RID: 514
		private readonly int rank;

		// Token: 0x04000203 RID: 515
		private readonly int[] sizes;

		// Token: 0x04000204 RID: 516
		private readonly int[] lobounds;
	}
}
