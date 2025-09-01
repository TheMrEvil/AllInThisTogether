using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	// Token: 0x02000211 RID: 529
	[Serializable]
	internal class UnitySerializationHolder : ISerializable, IObjectReference
	{
		// Token: 0x0600174A RID: 5962 RVA: 0x0005AB3D File Offset: 0x00058D3D
		internal static void GetUnitySerializationInfo(SerializationInfo info, Missing missing)
		{
			info.SetType(typeof(UnitySerializationHolder));
			info.AddValue("UnityType", 3);
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x0005AB5C File Offset: 0x00058D5C
		internal static RuntimeType AddElementTypes(SerializationInfo info, RuntimeType type)
		{
			List<int> list = new List<int>();
			while (type.HasElementType)
			{
				if (type.IsSzArray)
				{
					list.Add(3);
				}
				else if (type.IsArray)
				{
					list.Add(type.GetArrayRank());
					list.Add(2);
				}
				else if (type.IsPointer)
				{
					list.Add(1);
				}
				else if (type.IsByRef)
				{
					list.Add(4);
				}
				type = (RuntimeType)type.GetElementType();
			}
			info.AddValue("ElementTypes", list.ToArray(), typeof(int[]));
			return type;
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x0005ABF0 File Offset: 0x00058DF0
		internal Type MakeElementTypes(Type type)
		{
			for (int i = this.m_elementTypes.Length - 1; i >= 0; i--)
			{
				if (this.m_elementTypes[i] == 3)
				{
					type = type.MakeArrayType();
				}
				else if (this.m_elementTypes[i] == 2)
				{
					type = type.MakeArrayType(this.m_elementTypes[--i]);
				}
				else if (this.m_elementTypes[i] == 1)
				{
					type = type.MakePointerType();
				}
				else if (this.m_elementTypes[i] == 4)
				{
					type = type.MakeByRefType();
				}
			}
			return type;
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x0005AC74 File Offset: 0x00058E74
		internal static void GetUnitySerializationInfo(SerializationInfo info, int unityType)
		{
			info.SetType(typeof(UnitySerializationHolder));
			info.AddValue("Data", null, typeof(string));
			info.AddValue("UnityType", unityType);
			info.AddValue("AssemblyName", string.Empty);
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x0005ACC4 File Offset: 0x00058EC4
		internal static void GetUnitySerializationInfo(SerializationInfo info, RuntimeType type)
		{
			if (type.GetRootElementType().IsGenericParameter)
			{
				type = UnitySerializationHolder.AddElementTypes(info, type);
				info.SetType(typeof(UnitySerializationHolder));
				info.AddValue("UnityType", 7);
				info.AddValue("GenericParameterPosition", type.GenericParameterPosition);
				info.AddValue("DeclaringMethod", type.DeclaringMethod, typeof(MethodBase));
				info.AddValue("DeclaringType", type.DeclaringType, typeof(Type));
				return;
			}
			int unityType = 4;
			if (!type.IsGenericTypeDefinition && type.ContainsGenericParameters)
			{
				unityType = 8;
				type = UnitySerializationHolder.AddElementTypes(info, type);
				info.AddValue("GenericArguments", type.GetGenericArguments(), typeof(Type[]));
				type = (RuntimeType)type.GetGenericTypeDefinition();
			}
			UnitySerializationHolder.GetUnitySerializationInfo(info, unityType, type.FullName, type.GetRuntimeAssembly());
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x0005ADA4 File Offset: 0x00058FA4
		internal static void GetUnitySerializationInfo(SerializationInfo info, int unityType, string data, RuntimeAssembly assembly)
		{
			info.SetType(typeof(UnitySerializationHolder));
			info.AddValue("Data", data, typeof(string));
			info.AddValue("UnityType", unityType);
			string value;
			if (assembly == null)
			{
				value = string.Empty;
			}
			else
			{
				value = assembly.FullName;
			}
			info.AddValue("AssemblyName", value);
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x0005AE08 File Offset: 0x00059008
		internal UnitySerializationHolder(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.m_unityType = info.GetInt32("UnityType");
			if (this.m_unityType == 3)
			{
				return;
			}
			if (this.m_unityType == 7)
			{
				this.m_declaringMethod = (info.GetValue("DeclaringMethod", typeof(MethodBase)) as MethodBase);
				this.m_declaringType = (info.GetValue("DeclaringType", typeof(Type)) as Type);
				this.m_genericParameterPosition = info.GetInt32("GenericParameterPosition");
				this.m_elementTypes = (info.GetValue("ElementTypes", typeof(int[])) as int[]);
				return;
			}
			if (this.m_unityType == 8)
			{
				this.m_instantiation = (info.GetValue("GenericArguments", typeof(Type[])) as Type[]);
				this.m_elementTypes = (info.GetValue("ElementTypes", typeof(int[])) as int[]);
			}
			this.m_data = info.GetString("Data");
			this.m_assemblyName = info.GetString("AssemblyName");
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x0005AF2A File Offset: 0x0005912A
		private void ThrowInsufficientInformation(string field)
		{
			throw new SerializationException(Environment.GetResourceString("Insufficient state to deserialize the object. Missing field '{0}'. More information is needed.", new object[]
			{
				field
			}));
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x0005AF45 File Offset: 0x00059145
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException(Environment.GetResourceString("The UnitySerializationHolder object is designed to transmit information about other types and is not serializable itself."));
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x0005AF58 File Offset: 0x00059158
		[SecurityCritical]
		public virtual object GetRealObject(StreamingContext context)
		{
			switch (this.m_unityType)
			{
			case 1:
				return Empty.Value;
			case 2:
				return DBNull.Value;
			case 3:
				return Missing.Value;
			case 4:
				if (this.m_data == null || this.m_data.Length == 0)
				{
					this.ThrowInsufficientInformation("Data");
				}
				if (this.m_assemblyName == null)
				{
					this.ThrowInsufficientInformation("AssemblyName");
				}
				if (this.m_assemblyName.Length == 0)
				{
					return Type.GetType(this.m_data, true, false);
				}
				return Assembly.Load(this.m_assemblyName).GetType(this.m_data, true, false);
			case 5:
			{
				if (this.m_data == null || this.m_data.Length == 0)
				{
					this.ThrowInsufficientInformation("Data");
				}
				if (this.m_assemblyName == null)
				{
					this.ThrowInsufficientInformation("AssemblyName");
				}
				Module module = Assembly.Load(this.m_assemblyName).GetModule(this.m_data);
				if (module == null)
				{
					throw new SerializationException(Environment.GetResourceString("The given module {0} cannot be found within the assembly {1}.", new object[]
					{
						this.m_data,
						this.m_assemblyName
					}));
				}
				return module;
			}
			case 6:
				if (this.m_data == null || this.m_data.Length == 0)
				{
					this.ThrowInsufficientInformation("Data");
				}
				if (this.m_assemblyName == null)
				{
					this.ThrowInsufficientInformation("AssemblyName");
				}
				return Assembly.Load(this.m_assemblyName);
			case 7:
				if (this.m_declaringMethod == null && this.m_declaringType == null)
				{
					this.ThrowInsufficientInformation("DeclaringMember");
				}
				if (this.m_declaringMethod != null)
				{
					return this.m_declaringMethod.GetGenericArguments()[this.m_genericParameterPosition];
				}
				return this.MakeElementTypes(this.m_declaringType.GetGenericArguments()[this.m_genericParameterPosition]);
			case 8:
			{
				this.m_unityType = 4;
				Type type = this.GetRealObject(context) as Type;
				this.m_unityType = 8;
				if (this.m_instantiation[0] == null)
				{
					return null;
				}
				return this.MakeElementTypes(type.MakeGenericType(this.m_instantiation));
			}
			default:
				throw new ArgumentException(Environment.GetResourceString("Invalid Unity type."));
			}
		}

		// Token: 0x04001633 RID: 5683
		internal const int EmptyUnity = 1;

		// Token: 0x04001634 RID: 5684
		internal const int NullUnity = 2;

		// Token: 0x04001635 RID: 5685
		internal const int MissingUnity = 3;

		// Token: 0x04001636 RID: 5686
		internal const int RuntimeTypeUnity = 4;

		// Token: 0x04001637 RID: 5687
		internal const int ModuleUnity = 5;

		// Token: 0x04001638 RID: 5688
		internal const int AssemblyUnity = 6;

		// Token: 0x04001639 RID: 5689
		internal const int GenericParameterTypeUnity = 7;

		// Token: 0x0400163A RID: 5690
		internal const int PartialInstantiationTypeUnity = 8;

		// Token: 0x0400163B RID: 5691
		internal const int Pointer = 1;

		// Token: 0x0400163C RID: 5692
		internal const int Array = 2;

		// Token: 0x0400163D RID: 5693
		internal const int SzArray = 3;

		// Token: 0x0400163E RID: 5694
		internal const int ByRef = 4;

		// Token: 0x0400163F RID: 5695
		private Type[] m_instantiation;

		// Token: 0x04001640 RID: 5696
		private int[] m_elementTypes;

		// Token: 0x04001641 RID: 5697
		private int m_genericParameterPosition;

		// Token: 0x04001642 RID: 5698
		private Type m_declaringType;

		// Token: 0x04001643 RID: 5699
		private MethodBase m_declaringMethod;

		// Token: 0x04001644 RID: 5700
		private string m_data;

		// Token: 0x04001645 RID: 5701
		private string m_assemblyName;

		// Token: 0x04001646 RID: 5702
		private int m_unityType;
	}
}
