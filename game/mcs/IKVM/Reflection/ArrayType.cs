using System;
using System.Collections.Generic;

namespace IKVM.Reflection
{
	// Token: 0x0200005E RID: 94
	internal sealed class ArrayType : ElementHolderType
	{
		// Token: 0x06000568 RID: 1384 RVA: 0x00010A5B File Offset: 0x0000EC5B
		internal static Type Make(Type type, CustomModifiers mods)
		{
			return type.Universe.CanonicalizeType(new ArrayType(type, mods));
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00010A6F File Offset: 0x0000EC6F
		private ArrayType(Type type, CustomModifiers mods) : base(type, mods, 29)
		{
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00010A7B File Offset: 0x0000EC7B
		public override Type BaseType
		{
			get
			{
				return this.elementType.Module.universe.System_Array;
			}
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00010A94 File Offset: 0x0000EC94
		public override Type[] __GetDeclaredInterfaces()
		{
			return new Type[]
			{
				this.Module.universe.Import(typeof(IList<>)).MakeGenericType(new Type[]
				{
					this.elementType
				}),
				this.Module.universe.Import(typeof(ICollection<>)).MakeGenericType(new Type[]
				{
					this.elementType
				}),
				this.Module.universe.Import(typeof(IEnumerable<>)).MakeGenericType(new Type[]
				{
					this.elementType
				})
			};
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00010B3C File Offset: 0x0000ED3C
		public override MethodBase[] __GetDeclaredMethods()
		{
			Type[] array = new Type[]
			{
				this.Module.universe.System_Int32
			};
			List<MethodBase> list = new List<MethodBase>();
			list.Add(new BuiltinArrayMethod(this.Module, this, "Set", CallingConventions.Standard | CallingConventions.HasThis, this.Module.universe.System_Void, new Type[]
			{
				this.Module.universe.System_Int32,
				this.elementType
			}));
			list.Add(new BuiltinArrayMethod(this.Module, this, "Address", CallingConventions.Standard | CallingConventions.HasThis, this.elementType.MakeByRefType(), array));
			list.Add(new BuiltinArrayMethod(this.Module, this, "Get", CallingConventions.Standard | CallingConventions.HasThis, this.elementType, array));
			list.Add(new ConstructorInfoImpl(new BuiltinArrayMethod(this.Module, this, ".ctor", CallingConventions.Standard | CallingConventions.HasThis, this.Module.universe.System_Void, array)));
			Type elementType = this.elementType;
			while (elementType.__IsVector)
			{
				Array.Resize<Type>(ref array, array.Length + 1);
				Type[] array2 = array;
				array2[array2.Length - 1] = array[0];
				list.Add(new ConstructorInfoImpl(new BuiltinArrayMethod(this.Module, this, ".ctor", CallingConventions.Standard | CallingConventions.HasThis, this.Module.universe.System_Void, array)));
				elementType = elementType.GetElementType();
			}
			return list.ToArray();
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x00010C8C File Offset: 0x0000EE8C
		public override TypeAttributes Attributes
		{
			get
			{
				return TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.Serializable;
			}
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0000212D File Offset: 0x0000032D
		public override int GetArrayRank()
		{
			return 1;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00010C93 File Offset: 0x0000EE93
		public override bool Equals(object o)
		{
			return base.EqualsHelper(o as ArrayType);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00010CA1 File Offset: 0x0000EEA1
		public override int GetHashCode()
		{
			return this.elementType.GetHashCode() * 5;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00010CB0 File Offset: 0x0000EEB0
		internal override string GetSuffix()
		{
			return "[]";
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00010CB7 File Offset: 0x0000EEB7
		protected override Type Wrap(Type type, CustomModifiers mods)
		{
			return ArrayType.Make(type, mods);
		}
	}
}
