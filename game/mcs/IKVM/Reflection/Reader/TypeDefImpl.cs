using System;
using System.Collections.Generic;
using System.Text;
using IKVM.Reflection.Metadata;

namespace IKVM.Reflection.Reader
{
	// Token: 0x020000A6 RID: 166
	internal sealed class TypeDefImpl : TypeInfo
	{
		// Token: 0x060008A3 RID: 2211 RVA: 0x0001D6CC File Offset: 0x0001B8CC
		internal TypeDefImpl(ModuleReader module, int index)
		{
			this.module = module;
			this.index = index;
			this.typeName = module.GetString(module.TypeDef.records[index].TypeName);
			this.typeNamespace = module.GetString(module.TypeDef.records[index].TypeNamespace);
			base.MarkKnownType(this.typeNamespace, this.typeName);
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x0001D744 File Offset: 0x0001B944
		public override Type BaseType
		{
			get
			{
				int extends = this.module.TypeDef.records[this.index].Extends;
				if ((extends & 16777215) == 0)
				{
					return null;
				}
				return this.module.ResolveType(extends, this);
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x0001D78A File Offset: 0x0001B98A
		public override TypeAttributes Attributes
		{
			get
			{
				return (TypeAttributes)this.module.TypeDef.records[this.index].Flags;
			}
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0001D7AC File Offset: 0x0001B9AC
		public override EventInfo[] __GetDeclaredEvents()
		{
			SortedTable<EventMapTable.Record>.Enumerator enumerator = this.module.EventMap.Filter(this.MetadataToken).GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return Empty<EventInfo>.Array;
			}
			int num = enumerator.Current;
			int i = this.module.EventMap.records[num].EventList - 1;
			int num2 = (this.module.EventMap.records.Length > num + 1) ? (this.module.EventMap.records[num + 1].EventList - 1) : this.module.Event.records.Length;
			EventInfo[] array = new EventInfo[num2 - i];
			if (this.module.EventPtr.RowCount == 0)
			{
				int num3 = 0;
				while (i < num2)
				{
					array[num3] = new EventInfoImpl(this.module, this, i);
					i++;
					num3++;
				}
			}
			else
			{
				int num4 = 0;
				while (i < num2)
				{
					array[num4] = new EventInfoImpl(this.module, this, this.module.EventPtr.records[i] - 1);
					i++;
					num4++;
				}
			}
			return array;
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0001D8E4 File Offset: 0x0001BAE4
		public override FieldInfo[] __GetDeclaredFields()
		{
			int i = this.module.TypeDef.records[this.index].FieldList - 1;
			int num = (this.module.TypeDef.records.Length > this.index + 1) ? (this.module.TypeDef.records[this.index + 1].FieldList - 1) : this.module.Field.records.Length;
			FieldInfo[] array = new FieldInfo[num - i];
			if (this.module.FieldPtr.RowCount == 0)
			{
				int num2 = 0;
				while (i < num)
				{
					array[num2] = this.module.GetFieldAt(this, i);
					num2++;
					i++;
				}
			}
			else
			{
				int num3 = 0;
				while (i < num)
				{
					array[num3] = this.module.GetFieldAt(this, this.module.FieldPtr.records[i] - 1);
					num3++;
					i++;
				}
			}
			return array;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0001D9E0 File Offset: 0x0001BBE0
		public override Type[] __GetDeclaredInterfaces()
		{
			List<Type> list = null;
			foreach (int num in this.module.InterfaceImpl.Filter(this.MetadataToken))
			{
				if (list == null)
				{
					list = new List<Type>();
				}
				list.Add(this.module.ResolveType(this.module.InterfaceImpl.records[num].Interface, this));
			}
			return Util.ToArray<Type, Type>(list, Type.EmptyTypes);
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0001DA64 File Offset: 0x0001BC64
		public override MethodBase[] __GetDeclaredMethods()
		{
			int i = this.module.TypeDef.records[this.index].MethodList - 1;
			int num = (this.module.TypeDef.records.Length > this.index + 1) ? (this.module.TypeDef.records[this.index + 1].MethodList - 1) : this.module.MethodDef.records.Length;
			MethodBase[] array = new MethodBase[num - i];
			if (this.module.MethodPtr.RowCount == 0)
			{
				int num2 = 0;
				while (i < num)
				{
					array[num2] = this.module.GetMethodAt(this, i);
					i++;
					num2++;
				}
			}
			else
			{
				int num3 = 0;
				while (i < num)
				{
					array[num3] = this.module.GetMethodAt(this, this.module.MethodPtr.records[i] - 1);
					i++;
					num3++;
				}
			}
			return array;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0001DB60 File Offset: 0x0001BD60
		public override __MethodImplMap __GetMethodImplMap()
		{
			this.PopulateGenericArguments();
			List<MethodInfo> list = new List<MethodInfo>();
			List<List<MethodInfo>> list2 = new List<List<MethodInfo>>();
			foreach (int num in this.module.MethodImpl.Filter(this.MetadataToken))
			{
				MethodInfo item = (MethodInfo)this.module.ResolveMethod(this.module.MethodImpl.records[num].MethodBody, this.typeArgs, null);
				int num2 = list.IndexOf(item);
				if (num2 == -1)
				{
					num2 = list.Count;
					list.Add(item);
					list2.Add(new List<MethodInfo>());
				}
				MethodInfo item2 = (MethodInfo)this.module.ResolveMethod(this.module.MethodImpl.records[num].MethodDeclaration, this.typeArgs, null);
				list2[num2].Add(item2);
			}
			__MethodImplMap _MethodImplMap = default(__MethodImplMap);
			_MethodImplMap.TargetType = this;
			_MethodImplMap.MethodBodies = list.ToArray();
			_MethodImplMap.MethodDeclarations = new MethodInfo[list2.Count][];
			for (int i = 0; i < _MethodImplMap.MethodDeclarations.Length; i++)
			{
				_MethodImplMap.MethodDeclarations[i] = list2[i].ToArray();
			}
			return _MethodImplMap;
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0001DCBC File Offset: 0x0001BEBC
		public override Type[] __GetDeclaredTypes()
		{
			int metadataToken = this.MetadataToken;
			List<Type> list = new List<Type>();
			for (int i = 0; i < this.module.NestedClass.records.Length; i++)
			{
				if (this.module.NestedClass.records[i].EnclosingClass == metadataToken)
				{
					list.Add(this.module.ResolveType(this.module.NestedClass.records[i].NestedClass));
				}
			}
			return list.ToArray();
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0001DD44 File Offset: 0x0001BF44
		public override PropertyInfo[] __GetDeclaredProperties()
		{
			SortedTable<PropertyMapTable.Record>.Enumerator enumerator = this.module.PropertyMap.Filter(this.MetadataToken).GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return Empty<PropertyInfo>.Array;
			}
			int num = enumerator.Current;
			int i = this.module.PropertyMap.records[num].PropertyList - 1;
			int num2 = (this.module.PropertyMap.records.Length > num + 1) ? (this.module.PropertyMap.records[num + 1].PropertyList - 1) : this.module.Property.records.Length;
			PropertyInfo[] array = new PropertyInfo[num2 - i];
			if (this.module.PropertyPtr.RowCount == 0)
			{
				int num3 = 0;
				while (i < num2)
				{
					array[num3] = new PropertyInfoImpl(this.module, this, i);
					i++;
					num3++;
				}
			}
			else
			{
				int num4 = 0;
				while (i < num2)
				{
					array[num4] = new PropertyInfoImpl(this.module, this, this.module.PropertyPtr.records[i] - 1);
					i++;
					num4++;
				}
			}
			return array;
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x0001DE79 File Offset: 0x0001C079
		internal override TypeName TypeName
		{
			get
			{
				return new TypeName(this.typeNamespace, this.typeName);
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x0001DE8C File Offset: 0x0001C08C
		public override string Name
		{
			get
			{
				return TypeNameParser.Escape(this.typeName);
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x00009EDD File Offset: 0x000080DD
		public override string FullName
		{
			get
			{
				return base.GetFullName();
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x0001DE99 File Offset: 0x0001C099
		public override int MetadataToken
		{
			get
			{
				return (2 << 24) + this.index + 1;
			}
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0001DEA8 File Offset: 0x0001C0A8
		public override Type[] GetGenericArguments()
		{
			this.PopulateGenericArguments();
			return Util.Copy(this.typeArgs);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0001DEBC File Offset: 0x0001C0BC
		private void PopulateGenericArguments()
		{
			if (this.typeArgs == null)
			{
				int metadataToken = this.MetadataToken;
				int num = this.module.GenericParam.FindFirstByOwner(metadataToken);
				if (num == -1)
				{
					this.typeArgs = Type.EmptyTypes;
					return;
				}
				List<Type> list = new List<Type>();
				int num2 = this.module.GenericParam.records.Length;
				int num3 = num;
				while (num3 < num2 && this.module.GenericParam.records[num3].Owner == metadataToken)
				{
					list.Add(new GenericTypeParameter(this.module, num3, 19));
					num3++;
				}
				this.typeArgs = list.ToArray();
			}
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0001DF66 File Offset: 0x0001C166
		internal override Type GetGenericTypeArgument(int index)
		{
			this.PopulateGenericArguments();
			return this.typeArgs[index];
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0001DF76 File Offset: 0x0001C176
		public override CustomModifiers[] __GetGenericArgumentsCustomModifiers()
		{
			this.PopulateGenericArguments();
			return new CustomModifiers[this.typeArgs.Length];
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0001DF8B File Offset: 0x0001C18B
		public override bool IsGenericType
		{
			get
			{
				return this.IsGenericTypeDefinition;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x0001DF94 File Offset: 0x0001C194
		public override bool IsGenericTypeDefinition
		{
			get
			{
				if ((this.typeFlags & (Type.TypeFlags.IsGenericTypeDefinition | Type.TypeFlags.NotGenericTypeDefinition)) == Type.TypeFlags.ContainsMissingType_Unknown)
				{
					this.typeFlags |= ((this.module.GenericParam.FindFirstByOwner(this.MetadataToken) == -1) ? Type.TypeFlags.NotGenericTypeDefinition : Type.TypeFlags.IsGenericTypeDefinition);
				}
				return (this.typeFlags & Type.TypeFlags.IsGenericTypeDefinition) > Type.TypeFlags.ContainsMissingType_Unknown;
			}
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0001DFE8 File Offset: 0x0001C1E8
		public override Type GetGenericTypeDefinition()
		{
			if (this.IsGenericTypeDefinition)
			{
				return this;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0001DFFC File Offset: 0x0001C1FC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.FullName);
			string text = "[";
			foreach (Type value in this.GetGenericArguments())
			{
				stringBuilder.Append(text);
				stringBuilder.Append(value);
				text = ",";
			}
			if (text != "[")
			{
				stringBuilder.Append(']');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x0001E068 File Offset: 0x0001C268
		internal bool IsNestedByFlags
		{
			get
			{
				return (this.Attributes & TypeAttributes.VisibilityMask & ~TypeAttributes.Public) > TypeAttributes.AnsiClass;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x0001E078 File Offset: 0x0001C278
		public override Type DeclaringType
		{
			get
			{
				if (!this.IsNestedByFlags)
				{
					return null;
				}
				SortedTable<NestedClassTable.Record>.Enumerator enumerator = this.module.NestedClass.Filter(this.MetadataToken).GetEnumerator();
				if (!enumerator.MoveNext())
				{
					throw new InvalidOperationException();
				}
				int num = enumerator.Current;
				return this.module.ResolveType(this.module.NestedClass.records[num].EnclosingClass, null, null);
			}
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0001E0F0 File Offset: 0x0001C2F0
		public override bool __GetLayout(out int packingSize, out int typeSize)
		{
			SortedTable<ClassLayoutTable.Record>.Enumerator enumerator = this.module.ClassLayout.Filter(this.MetadataToken).GetEnumerator();
			if (!enumerator.MoveNext())
			{
				packingSize = 0;
				typeSize = 0;
				return false;
			}
			int num = enumerator.Current;
			packingSize = (int)this.module.ClassLayout.records[num].PackingSize;
			typeSize = this.module.ClassLayout.records[num].ClassSize;
			return true;
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x0001E172 File Offset: 0x0001C372
		public override Module Module
		{
			get
			{
				return this.module;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x0001E17A File Offset: 0x0001C37A
		internal override bool IsModulePseudoType
		{
			get
			{
				return this.index == 0;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x0000212D File Offset: 0x0000032D
		internal override bool IsBaked
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000398 RID: 920
		private readonly ModuleReader module;

		// Token: 0x04000399 RID: 921
		private readonly int index;

		// Token: 0x0400039A RID: 922
		private readonly string typeName;

		// Token: 0x0400039B RID: 923
		private readonly string typeNamespace;

		// Token: 0x0400039C RID: 924
		private Type[] typeArgs;
	}
}
