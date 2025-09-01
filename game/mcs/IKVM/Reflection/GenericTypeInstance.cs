using System;
using System.Text;
using IKVM.Reflection.Emit;

namespace IKVM.Reflection
{
	// Token: 0x02000063 RID: 99
	internal sealed class GenericTypeInstance : TypeInfo
	{
		// Token: 0x06000597 RID: 1431 RVA: 0x00011068 File Offset: 0x0000F268
		internal static Type Make(Type type, Type[] typeArguments, CustomModifiers[] mods)
		{
			bool flag = true;
			if (type is TypeBuilder || type is BakedType || type.__IsMissing)
			{
				flag = false;
			}
			else
			{
				for (int i = 0; i < typeArguments.Length; i++)
				{
					if (typeArguments[i] != type.GetGenericTypeArgument(i) || !GenericTypeInstance.IsEmpty(mods, i))
					{
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				return type;
			}
			return type.Universe.CanonicalizeType(new GenericTypeInstance(type, typeArguments, mods));
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x000110D7 File Offset: 0x0000F2D7
		private static bool IsEmpty(CustomModifiers[] mods, int i)
		{
			return mods == null || mods[i].IsEmpty;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x000110EA File Offset: 0x0000F2EA
		private GenericTypeInstance(Type type, Type[] args, CustomModifiers[] mods)
		{
			this.type = type;
			this.args = args;
			this.mods = mods;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00011108 File Offset: 0x0000F308
		public override bool Equals(object o)
		{
			GenericTypeInstance genericTypeInstance = o as GenericTypeInstance;
			return genericTypeInstance != null && genericTypeInstance.type.Equals(this.type) && Util.ArrayEquals(genericTypeInstance.args, this.args) && Util.ArrayEquals(genericTypeInstance.mods, this.mods);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0001115E File Offset: 0x0000F35E
		public override int GetHashCode()
		{
			return this.type.GetHashCode() * 3 ^ Util.GetHashCode(this.args);
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x0001117C File Offset: 0x0000F37C
		public override string AssemblyQualifiedName
		{
			get
			{
				string fullName = this.FullName;
				if (fullName != null)
				{
					return fullName + ", " + this.type.Assembly.FullName;
				}
				return null;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x000111B0 File Offset: 0x0000F3B0
		public override Type BaseType
		{
			get
			{
				if (this.baseType == null)
				{
					Type type = this.type.BaseType;
					if (type == null)
					{
						this.baseType = type;
					}
					else
					{
						this.baseType = type.BindTypeParameters(this);
					}
				}
				return this.baseType;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x000111FC File Offset: 0x0000F3FC
		public override bool IsValueType
		{
			get
			{
				return this.type.IsValueType;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0001120C File Offset: 0x0000F40C
		public override bool IsVisible
		{
			get
			{
				if (base.IsVisible)
				{
					Type[] array = this.args;
					for (int i = 0; i < array.Length; i++)
					{
						if (!array[i].IsVisible)
						{
							return false;
						}
					}
					return true;
				}
				return false;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x00011245 File Offset: 0x0000F445
		public override Type DeclaringType
		{
			get
			{
				return this.type.DeclaringType;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00011252 File Offset: 0x0000F452
		public override TypeAttributes Attributes
		{
			get
			{
				return this.type.Attributes;
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0001125F File Offset: 0x0000F45F
		internal override void CheckBaked()
		{
			this.type.CheckBaked();
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0001126C File Offset: 0x0000F46C
		public override FieldInfo[] __GetDeclaredFields()
		{
			FieldInfo[] array = this.type.__GetDeclaredFields();
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array[i].BindTypeParameters(this);
			}
			return array;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x000112A0 File Offset: 0x0000F4A0
		public override Type[] __GetDeclaredInterfaces()
		{
			Type[] array = this.type.__GetDeclaredInterfaces();
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array[i].BindTypeParameters(this);
			}
			return array;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x000112D4 File Offset: 0x0000F4D4
		public override MethodBase[] __GetDeclaredMethods()
		{
			MethodBase[] array = this.type.__GetDeclaredMethods();
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array[i].BindTypeParameters(this);
			}
			return array;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00011308 File Offset: 0x0000F508
		public override Type[] __GetDeclaredTypes()
		{
			return this.type.__GetDeclaredTypes();
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00011318 File Offset: 0x0000F518
		public override EventInfo[] __GetDeclaredEvents()
		{
			EventInfo[] array = this.type.__GetDeclaredEvents();
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array[i].BindTypeParameters(this);
			}
			return array;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001134C File Offset: 0x0000F54C
		public override PropertyInfo[] __GetDeclaredProperties()
		{
			PropertyInfo[] array = this.type.__GetDeclaredProperties();
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array[i].BindTypeParameters(this);
			}
			return array;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00011380 File Offset: 0x0000F580
		public override __MethodImplMap __GetMethodImplMap()
		{
			__MethodImplMap _MethodImplMap = this.type.__GetMethodImplMap();
			_MethodImplMap.TargetType = this;
			for (int i = 0; i < _MethodImplMap.MethodBodies.Length; i++)
			{
				_MethodImplMap.MethodBodies[i] = (MethodInfo)_MethodImplMap.MethodBodies[i].BindTypeParameters(this);
				for (int j = 0; j < _MethodImplMap.MethodDeclarations[i].Length; j++)
				{
					if (_MethodImplMap.MethodDeclarations[i][j].DeclaringType.IsGenericType)
					{
						_MethodImplMap.MethodDeclarations[i][j] = (MethodInfo)_MethodImplMap.MethodDeclarations[i][j].BindTypeParameters(this);
					}
				}
			}
			return _MethodImplMap;
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0001141A File Offset: 0x0000F61A
		public override string Namespace
		{
			get
			{
				return this.type.Namespace;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x00011427 File Offset: 0x0000F627
		public override string Name
		{
			get
			{
				return this.type.Name;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x00011434 File Offset: 0x0000F634
		public override string FullName
		{
			get
			{
				if (!base.__ContainsMissingType && this.ContainsGenericParameters)
				{
					return null;
				}
				StringBuilder stringBuilder = new StringBuilder(this.type.FullName);
				stringBuilder.Append('[');
				string value = "";
				foreach (Type type in this.args)
				{
					stringBuilder.Append(value).Append('[').Append(type.FullName).Append(", ").Append(type.Assembly.FullName.Replace("]", "\\]")).Append(']');
					value = ",";
				}
				stringBuilder.Append(']');
				return stringBuilder.ToString();
			}
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x000114F0 File Offset: 0x0000F6F0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.type.FullName);
			stringBuilder.Append('[');
			string value = "";
			foreach (Type value2 in this.args)
			{
				stringBuilder.Append(value);
				stringBuilder.Append(value2);
				value = ",";
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0001155D File Offset: 0x0000F75D
		public override Module Module
		{
			get
			{
				return this.type.Module;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsGenericType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsConstructedGenericType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0001156A File Offset: 0x0000F76A
		public override Type GetGenericTypeDefinition()
		{
			return this.type;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00011572 File Offset: 0x0000F772
		public override Type[] GetGenericArguments()
		{
			return Util.Copy(this.args);
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001157F File Offset: 0x0000F77F
		public override CustomModifiers[] __GetGenericArgumentsCustomModifiers()
		{
			if (this.mods == null)
			{
				return new CustomModifiers[this.args.Length];
			}
			return (CustomModifiers[])this.mods.Clone();
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x000115A7 File Offset: 0x0000F7A7
		internal override Type GetGenericTypeArgument(int index)
		{
			return this.args[index];
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x000115B4 File Offset: 0x0000F7B4
		public override bool ContainsGenericParameters
		{
			get
			{
				Type[] array = this.args;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].ContainsGenericParameters)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x000115E3 File Offset: 0x0000F7E3
		protected override bool ContainsMissingTypeImpl
		{
			get
			{
				return this.type.__ContainsMissingType || Type.ContainsMissingType(this.args);
			}
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x000115FF File Offset: 0x0000F7FF
		public override bool __GetLayout(out int packingSize, out int typeSize)
		{
			return this.type.__GetLayout(out packingSize, out typeSize);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0001160E File Offset: 0x0000F80E
		internal override int GetModuleBuilderToken()
		{
			if (this.token == 0)
			{
				this.token = ((ModuleBuilder)this.type.Module).ImportType(this);
			}
			return this.token;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0001163C File Offset: 0x0000F83C
		internal override Type BindTypeParameters(IGenericBinder binder)
		{
			for (int i = 0; i < this.args.Length; i++)
			{
				Type type = this.args[i].BindTypeParameters(binder);
				if (type != this.args[i])
				{
					Type[] array = new Type[this.args.Length];
					Array.Copy(this.args, array, i);
					array[i++] = type;
					while (i < this.args.Length)
					{
						array[i] = this.args[i].BindTypeParameters(binder);
						i++;
					}
					return GenericTypeInstance.Make(this.type, array, null);
				}
			}
			return this;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x000116CA File Offset: 0x0000F8CA
		internal override int GetCurrentToken()
		{
			return this.type.GetCurrentToken();
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x000116D7 File Offset: 0x0000F8D7
		internal override bool IsBaked
		{
			get
			{
				return this.type.IsBaked;
			}
		}

		// Token: 0x04000205 RID: 517
		private readonly Type type;

		// Token: 0x04000206 RID: 518
		private readonly Type[] args;

		// Token: 0x04000207 RID: 519
		private readonly CustomModifiers[] mods;

		// Token: 0x04000208 RID: 520
		private Type baseType;

		// Token: 0x04000209 RID: 521
		private int token;
	}
}
