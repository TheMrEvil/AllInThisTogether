using System;
using System.Collections.Generic;

namespace IKVM.Reflection
{
	// Token: 0x02000068 RID: 104
	public abstract class TypeInfo : Type, IReflectableType
	{
		// Token: 0x060005D5 RID: 1493 RVA: 0x000117BE File Offset: 0x0000F9BE
		internal TypeInfo()
		{
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000117C6 File Offset: 0x0000F9C6
		internal TypeInfo(Type underlyingType) : base(underlyingType)
		{
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001177B File Offset: 0x0000F97B
		internal TypeInfo(byte sigElementType) : base(sigElementType)
		{
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x000117CF File Offset: 0x0000F9CF
		public IEnumerable<ConstructorInfo> DeclaredConstructors
		{
			get
			{
				return base.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x000117D9 File Offset: 0x0000F9D9
		public IEnumerable<EventInfo> DeclaredEvents
		{
			get
			{
				return base.GetEvents(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x000117E3 File Offset: 0x0000F9E3
		public IEnumerable<FieldInfo> DeclaredFields
		{
			get
			{
				return base.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x000117ED File Offset: 0x0000F9ED
		public IEnumerable<MemberInfo> DeclaredMembers
		{
			get
			{
				return base.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x000117F7 File Offset: 0x0000F9F7
		public IEnumerable<MethodInfo> DeclaredMethods
		{
			get
			{
				return base.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x00011804 File Offset: 0x0000FA04
		public IEnumerable<TypeInfo> DeclaredNestedTypes
		{
			get
			{
				Type[] nestedTypes = base.GetNestedTypes(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				TypeInfo[] array = new TypeInfo[nestedTypes.Length];
				for (int i = 0; i < nestedTypes.Length; i++)
				{
					array[i] = nestedTypes[i].GetTypeInfo();
				}
				return array;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x0001183D File Offset: 0x0000FA3D
		public IEnumerable<PropertyInfo> DeclaredProperties
		{
			get
			{
				return base.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x00011847 File Offset: 0x0000FA47
		public Type[] GenericTypeParameters
		{
			get
			{
				if (!this.IsGenericTypeDefinition)
				{
					return Type.EmptyTypes;
				}
				return this.GetGenericArguments();
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0001185D File Offset: 0x0000FA5D
		public IEnumerable<Type> ImplementedInterfaces
		{
			get
			{
				return this.__GetDeclaredInterfaces();
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00005936 File Offset: 0x00003B36
		public Type AsType()
		{
			return this;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00011865 File Offset: 0x0000FA65
		public EventInfo GetDeclaredEvent(string name)
		{
			return base.GetEvent(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00011870 File Offset: 0x0000FA70
		public FieldInfo GetDeclaredField(string name)
		{
			return base.GetField(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001187B File Offset: 0x0000FA7B
		public MethodInfo GetDeclaredMethod(string name)
		{
			return base.GetMethod(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00011888 File Offset: 0x0000FA88
		public IEnumerable<MethodInfo> GetDeclaredMethods(string name)
		{
			List<MethodInfo> list = new List<MethodInfo>();
			foreach (MethodInfo methodInfo in base.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (methodInfo.Name == name)
				{
					list.Add(methodInfo);
				}
			}
			return list;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x000118CC File Offset: 0x0000FACC
		public TypeInfo GetDeclaredNestedType(string name)
		{
			return base.GetNestedType(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).GetTypeInfo();
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x000118DC File Offset: 0x0000FADC
		public PropertyInfo GetDeclaredProperty(string name)
		{
			return base.GetProperty(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x000118E7 File Offset: 0x0000FAE7
		public bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return base.IsAssignableFrom(typeInfo);
		}

		// Token: 0x04000210 RID: 528
		private const BindingFlags Flags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
	}
}
