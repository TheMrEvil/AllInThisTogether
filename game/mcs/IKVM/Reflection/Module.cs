using System;
using System.Collections.Generic;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Metadata;
using IKVM.Reflection.Reader;

namespace IKVM.Reflection
{
	// Token: 0x0200004C RID: 76
	public abstract class Module : ICustomAttributeProvider
	{
		// Token: 0x0600034F RID: 847 RVA: 0x0000A7F8 File Offset: 0x000089F8
		protected Module(Universe universe)
		{
			this.universe = universe;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000A9C0 File Offset: 0x00008BC0
		internal Table[] GetTables()
		{
			Table[] array = new Table[64];
			array[0] = this.ModuleTable;
			array[1] = this.TypeRef;
			array[2] = this.TypeDef;
			array[3] = this.FieldPtr;
			array[4] = this.Field;
			array[10] = this.MemberRef;
			array[11] = this.Constant;
			array[12] = this.CustomAttribute;
			array[13] = this.FieldMarshal;
			array[14] = this.DeclSecurity;
			array[15] = this.ClassLayout;
			array[16] = this.FieldLayout;
			array[7] = this.ParamPtr;
			array[8] = this.Param;
			array[9] = this.InterfaceImpl;
			array[17] = this.StandAloneSig;
			array[18] = this.EventMap;
			array[19] = this.EventPtr;
			array[20] = this.Event;
			array[21] = this.PropertyMap;
			array[22] = this.PropertyPtr;
			array[23] = this.Property;
			array[24] = this.MethodSemantics;
			array[25] = this.MethodImpl;
			array[26] = this.ModuleRef;
			array[27] = this.TypeSpec;
			array[28] = this.ImplMap;
			array[29] = this.FieldRVA;
			array[32] = this.AssemblyTable;
			array[35] = this.AssemblyRef;
			array[5] = this.MethodPtr;
			array[6] = this.MethodDef;
			array[41] = this.NestedClass;
			array[38] = this.File;
			array[39] = this.ExportedType;
			array[40] = this.ManifestResource;
			array[42] = this.GenericParam;
			array[43] = this.MethodSpec;
			array[44] = this.GenericParamConstraint;
			return array;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000225C File Offset: 0x0000045C
		public virtual void __GetDataDirectoryEntry(int index, out int rva, out int length)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000225C File Offset: 0x0000045C
		public virtual long __RelativeVirtualAddressToFileOffset(int rva)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000AB54 File Offset: 0x00008D54
		public bool __GetSectionInfo(int rva, out string name, out int characteristics)
		{
			int num;
			int num2;
			int num3;
			int num4;
			return this.__GetSectionInfo(rva, out name, out characteristics, out num, out num2, out num3, out num4);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000225C File Offset: 0x0000045C
		public virtual bool __GetSectionInfo(int rva, out string name, out int characteristics, out int virtualAddress, out int virtualSize, out int pointerToRawData, out int sizeOfRawData)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000225C File Offset: 0x0000045C
		public virtual int __ReadDataFromRVA(int rva, byte[] data, int offset, int length)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000225C File Offset: 0x0000045C
		public virtual void GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine machine)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000225C File Offset: 0x0000045C
		public virtual int __Subsystem
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000AB72 File Offset: 0x00008D72
		public FieldInfo GetField(string name)
		{
			return this.GetField(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000AB7D File Offset: 0x00008D7D
		public FieldInfo GetField(string name, BindingFlags bindingFlags)
		{
			if (!this.IsResource())
			{
				return this.GetModuleType().GetField(name, bindingFlags | BindingFlags.DeclaredOnly);
			}
			return null;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000AB98 File Offset: 0x00008D98
		public FieldInfo[] GetFields()
		{
			return this.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000ABA2 File Offset: 0x00008DA2
		public FieldInfo[] GetFields(BindingFlags bindingFlags)
		{
			if (!this.IsResource())
			{
				return this.GetModuleType().GetFields(bindingFlags | BindingFlags.DeclaredOnly);
			}
			return Empty<FieldInfo>.Array;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000ABC0 File Offset: 0x00008DC0
		public MethodInfo GetMethod(string name)
		{
			if (!this.IsResource())
			{
				return this.GetModuleType().GetMethod(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
			}
			return null;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000ABDA File Offset: 0x00008DDA
		public MethodInfo GetMethod(string name, Type[] types)
		{
			if (!this.IsResource())
			{
				return this.GetModuleType().GetMethod(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, types, null);
			}
			return null;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000ABF7 File Offset: 0x00008DF7
		public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConv, Type[] types, ParameterModifier[] modifiers)
		{
			if (!this.IsResource())
			{
				return this.GetModuleType().GetMethod(name, bindingAttr | BindingFlags.DeclaredOnly, binder, callConv, types, modifiers);
			}
			return null;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000AC19 File Offset: 0x00008E19
		public MethodInfo[] GetMethods()
		{
			return this.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000AC23 File Offset: 0x00008E23
		public MethodInfo[] GetMethods(BindingFlags bindingFlags)
		{
			if (!this.IsResource())
			{
				return this.GetModuleType().GetMethods(bindingFlags | BindingFlags.DeclaredOnly);
			}
			return Empty<MethodInfo>.Array;
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000AC41 File Offset: 0x00008E41
		public ConstructorInfo __ModuleInitializer
		{
			get
			{
				if (!this.IsResource())
				{
					return this.GetModuleType().TypeInitializer;
				}
				return null;
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000225C File Offset: 0x0000045C
		public virtual byte[] ResolveSignature(int metadataToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000225C File Offset: 0x0000045C
		public virtual __StandAloneMethodSig __ResolveStandAloneMethodSig(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000225C File Offset: 0x0000045C
		public virtual CustomModifiers __ResolveTypeSpecCustomModifiers(int typeSpecToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000AC58 File Offset: 0x00008E58
		public int MetadataToken
		{
			get
			{
				if (!this.IsResource())
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000366 RID: 870
		public abstract int MDStreamVersion { get; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000367 RID: 871
		public abstract Assembly Assembly { get; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000368 RID: 872
		public abstract string FullyQualifiedName { get; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000369 RID: 873
		public abstract string Name { get; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600036A RID: 874
		public abstract Guid ModuleVersionId { get; }

		// Token: 0x0600036B RID: 875
		public abstract MethodBase ResolveMethod(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments);

		// Token: 0x0600036C RID: 876
		public abstract FieldInfo ResolveField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments);

		// Token: 0x0600036D RID: 877
		public abstract MemberInfo ResolveMember(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments);

		// Token: 0x0600036E RID: 878
		public abstract string ResolveString(int metadataToken);

		// Token: 0x0600036F RID: 879
		public abstract Type[] __ResolveOptionalParameterTypes(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments, out CustomModifiers[] customModifiers);

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000370 RID: 880
		public abstract string ScopeName { get; }

		// Token: 0x06000371 RID: 881
		internal abstract void GetTypesImpl(List<Type> list);

		// Token: 0x06000372 RID: 882
		internal abstract Type FindType(TypeName name);

		// Token: 0x06000373 RID: 883
		internal abstract Type FindTypeIgnoreCase(TypeName lowerCaseName);

		// Token: 0x06000374 RID: 884 RVA: 0x0000AC68 File Offset: 0x00008E68
		[Obsolete("Please use __ResolveOptionalParameterTypes(int, Type[], Type[], out CustomModifiers[]) instead.")]
		public Type[] __ResolveOptionalParameterTypes(int metadataToken)
		{
			CustomModifiers[] array;
			return this.__ResolveOptionalParameterTypes(metadataToken, null, null, out array);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000AC80 File Offset: 0x00008E80
		public Type GetType(string className)
		{
			return this.GetType(className, false, false);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000AC8B File Offset: 0x00008E8B
		public Type GetType(string className, bool ignoreCase)
		{
			return this.GetType(className, false, ignoreCase);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000AC98 File Offset: 0x00008E98
		public Type GetType(string className, bool throwOnError, bool ignoreCase)
		{
			TypeNameParser typeNameParser = TypeNameParser.Parse(className, throwOnError);
			if (typeNameParser.Error)
			{
				return null;
			}
			if (typeNameParser.AssemblyName != null)
			{
				if (throwOnError)
				{
					throw new ArgumentException("Type names passed to Module.GetType() must not specify an assembly.");
				}
				return null;
			}
			else
			{
				TypeName name = TypeName.Split(TypeNameParser.Unescape(typeNameParser.FirstNamePart));
				Type type = ignoreCase ? this.FindTypeIgnoreCase(name.ToLowerInvariant()) : this.FindType(name);
				if (type == null && this.__IsMissing)
				{
					throw new MissingModuleException((MissingModule)this);
				}
				return typeNameParser.Expand(type, this, throwOnError, className, false, ignoreCase);
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000AD28 File Offset: 0x00008F28
		public Type[] GetTypes()
		{
			List<Type> list = new List<Type>();
			this.GetTypesImpl(list);
			return list.ToArray();
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000AD48 File Offset: 0x00008F48
		public Type[] FindTypes(TypeFilter filter, object filterCriteria)
		{
			List<Type> list = new List<Type>();
			foreach (Type type in this.GetTypes())
			{
				if (filter(type, filterCriteria))
				{
					list.Add(type);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600037A RID: 890 RVA: 0x000022F4 File Offset: 0x000004F4
		public virtual bool IsResource()
		{
			return false;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000AD8B File Offset: 0x00008F8B
		public Type ResolveType(int metadataToken)
		{
			return this.ResolveType(metadataToken, null, null);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000AD96 File Offset: 0x00008F96
		public Type ResolveType(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			if (metadataToken >> 24 == 27)
			{
				return this.ResolveType(metadataToken, new Module.GenericContext(genericTypeArguments, genericMethodArguments));
			}
			return this.ResolveType(metadataToken, null);
		}

		// Token: 0x0600037D RID: 893
		internal abstract Type ResolveType(int metadataToken, IGenericContext context);

		// Token: 0x0600037E RID: 894 RVA: 0x0000ADB7 File Offset: 0x00008FB7
		public MethodBase ResolveMethod(int metadataToken)
		{
			return this.ResolveMethod(metadataToken, null, null);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000ADC2 File Offset: 0x00008FC2
		public FieldInfo ResolveField(int metadataToken)
		{
			return this.ResolveField(metadataToken, null, null);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000ADCD File Offset: 0x00008FCD
		public MemberInfo ResolveMember(int metadataToken)
		{
			return this.ResolveMember(metadataToken, null, null);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000ADD8 File Offset: 0x00008FD8
		public bool IsDefined(Type attributeType, bool inherit)
		{
			return CustomAttributeData.__GetCustomAttributes(this, attributeType, inherit).Count != 0;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000ADEA File Offset: 0x00008FEA
		public IList<CustomAttributeData> __GetCustomAttributes(Type attributeType, bool inherit)
		{
			return CustomAttributeData.__GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000ADF4 File Offset: 0x00008FF4
		public IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributes(this);
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000ADFC File Offset: 0x00008FFC
		public IEnumerable<CustomAttributeData> CustomAttributes
		{
			get
			{
				return this.GetCustomAttributesData();
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000AE04 File Offset: 0x00009004
		public virtual IList<CustomAttributeData> __GetPlaceholderAssemblyCustomAttributes(bool multiple, bool security)
		{
			return Empty<CustomAttributeData>.Array;
		}

		// Token: 0x06000386 RID: 902
		public abstract AssemblyName[] __GetReferencedAssemblies();

		// Token: 0x06000387 RID: 903 RVA: 0x0000225C File Offset: 0x0000045C
		public virtual void __ResolveReferencedAssemblies(Assembly[] assemblies)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000388 RID: 904
		public abstract string[] __GetReferencedModules();

		// Token: 0x06000389 RID: 905
		public abstract Type[] __GetReferencedTypes();

		// Token: 0x0600038A RID: 906
		public abstract Type[] __GetExportedTypes();

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600038B RID: 907 RVA: 0x000022F4 File Offset: 0x000004F4
		public virtual bool __IsMissing
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000AE0B File Offset: 0x0000900B
		public long __ImageBase
		{
			get
			{
				return this.GetImageBaseImpl();
			}
		}

		// Token: 0x0600038D RID: 909
		protected abstract long GetImageBaseImpl();

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000AE13 File Offset: 0x00009013
		public long __StackReserve
		{
			get
			{
				return this.GetStackReserveImpl();
			}
		}

		// Token: 0x0600038F RID: 911
		protected abstract long GetStackReserveImpl();

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000AE1B File Offset: 0x0000901B
		public int __FileAlignment
		{
			get
			{
				return this.GetFileAlignmentImpl();
			}
		}

		// Token: 0x06000391 RID: 913
		protected abstract int GetFileAlignmentImpl();

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000AE23 File Offset: 0x00009023
		public DllCharacteristics __DllCharacteristics
		{
			get
			{
				return this.GetDllCharacteristicsImpl();
			}
		}

		// Token: 0x06000393 RID: 915
		protected abstract DllCharacteristics GetDllCharacteristicsImpl();

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000225C File Offset: 0x0000045C
		public virtual byte[] __ModuleHash
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000225C File Offset: 0x0000045C
		public virtual int __EntryPointRVA
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000225C File Offset: 0x0000045C
		public virtual int __EntryPointToken
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000225C File Offset: 0x0000045C
		public virtual string __ImageRuntimeVersion
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000AE2C File Offset: 0x0000902C
		public IEnumerable<CustomAttributeData> __EnumerateCustomAttributeTable()
		{
			List<CustomAttributeData> list = new List<CustomAttributeData>(this.CustomAttribute.RowCount);
			for (int i = 0; i < this.CustomAttribute.RowCount; i++)
			{
				list.Add(new CustomAttributeData(this, i));
			}
			return list;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000AE6E File Offset: 0x0000906E
		[Obsolete]
		public List<CustomAttributeData> __GetCustomAttributesFor(int token)
		{
			return CustomAttributeData.GetCustomAttributesImpl(new List<CustomAttributeData>(), this, token, null);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000AE80 File Offset: 0x00009080
		public bool __TryGetImplMap(int token, out ImplMapFlags mappingFlags, out string importName, out string importScope)
		{
			SortedTable<ImplMapTable.Record>.Enumerator enumerator = this.ImplMap.Filter(token).GetEnumerator();
			if (!enumerator.MoveNext())
			{
				mappingFlags = ImplMapFlags.CharSetNotSpec;
				importName = null;
				importScope = null;
				return false;
			}
			int num = enumerator.Current;
			mappingFlags = (ImplMapFlags)((ushort)this.ImplMap.records[num].MappingFlags);
			importName = this.GetString(this.ImplMap.records[num].ImportName);
			importScope = this.GetString(this.ModuleRef.records[(this.ImplMap.records[num].ImportScope & 16777215) - 1]);
			return true;
		}

		// Token: 0x0600039B RID: 923
		internal abstract Type GetModuleType();

		// Token: 0x0600039C RID: 924
		internal abstract ByteReader GetBlob(int blobIndex);

		// Token: 0x0600039D RID: 925 RVA: 0x0000AF2C File Offset: 0x0000912C
		internal IList<CustomAttributeData> GetDeclarativeSecurity(int metadataToken)
		{
			List<CustomAttributeData> list = new List<CustomAttributeData>();
			foreach (int index in this.DeclSecurity.Filter(metadataToken))
			{
				CustomAttributeData.ReadDeclarativeSecurity(this, index, list);
			}
			return list;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000AF70 File Offset: 0x00009170
		internal virtual void Dispose()
		{
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000AF70 File Offset: 0x00009170
		internal virtual void ExportTypes(int fileToken, ModuleBuilder manifestModule)
		{
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000225C File Offset: 0x0000045C
		internal virtual string GetString(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0400018C RID: 396
		internal readonly Universe universe;

		// Token: 0x0400018D RID: 397
		internal readonly ModuleTable ModuleTable = new ModuleTable();

		// Token: 0x0400018E RID: 398
		internal readonly TypeRefTable TypeRef = new TypeRefTable();

		// Token: 0x0400018F RID: 399
		internal readonly TypeDefTable TypeDef = new TypeDefTable();

		// Token: 0x04000190 RID: 400
		internal readonly FieldPtrTable FieldPtr = new FieldPtrTable();

		// Token: 0x04000191 RID: 401
		internal readonly FieldTable Field = new FieldTable();

		// Token: 0x04000192 RID: 402
		internal readonly MemberRefTable MemberRef = new MemberRefTable();

		// Token: 0x04000193 RID: 403
		internal readonly ConstantTable Constant = new ConstantTable();

		// Token: 0x04000194 RID: 404
		internal readonly CustomAttributeTable CustomAttribute = new CustomAttributeTable();

		// Token: 0x04000195 RID: 405
		internal readonly FieldMarshalTable FieldMarshal = new FieldMarshalTable();

		// Token: 0x04000196 RID: 406
		internal readonly DeclSecurityTable DeclSecurity = new DeclSecurityTable();

		// Token: 0x04000197 RID: 407
		internal readonly ClassLayoutTable ClassLayout = new ClassLayoutTable();

		// Token: 0x04000198 RID: 408
		internal readonly FieldLayoutTable FieldLayout = new FieldLayoutTable();

		// Token: 0x04000199 RID: 409
		internal readonly ParamPtrTable ParamPtr = new ParamPtrTable();

		// Token: 0x0400019A RID: 410
		internal readonly ParamTable Param = new ParamTable();

		// Token: 0x0400019B RID: 411
		internal readonly InterfaceImplTable InterfaceImpl = new InterfaceImplTable();

		// Token: 0x0400019C RID: 412
		internal readonly StandAloneSigTable StandAloneSig = new StandAloneSigTable();

		// Token: 0x0400019D RID: 413
		internal readonly EventMapTable EventMap = new EventMapTable();

		// Token: 0x0400019E RID: 414
		internal readonly EventPtrTable EventPtr = new EventPtrTable();

		// Token: 0x0400019F RID: 415
		internal readonly EventTable Event = new EventTable();

		// Token: 0x040001A0 RID: 416
		internal readonly PropertyMapTable PropertyMap = new PropertyMapTable();

		// Token: 0x040001A1 RID: 417
		internal readonly PropertyPtrTable PropertyPtr = new PropertyPtrTable();

		// Token: 0x040001A2 RID: 418
		internal readonly PropertyTable Property = new PropertyTable();

		// Token: 0x040001A3 RID: 419
		internal readonly MethodSemanticsTable MethodSemantics = new MethodSemanticsTable();

		// Token: 0x040001A4 RID: 420
		internal readonly MethodImplTable MethodImpl = new MethodImplTable();

		// Token: 0x040001A5 RID: 421
		internal readonly ModuleRefTable ModuleRef = new ModuleRefTable();

		// Token: 0x040001A6 RID: 422
		internal readonly TypeSpecTable TypeSpec = new TypeSpecTable();

		// Token: 0x040001A7 RID: 423
		internal readonly ImplMapTable ImplMap = new ImplMapTable();

		// Token: 0x040001A8 RID: 424
		internal readonly FieldRVATable FieldRVA = new FieldRVATable();

		// Token: 0x040001A9 RID: 425
		internal readonly AssemblyTable AssemblyTable = new AssemblyTable();

		// Token: 0x040001AA RID: 426
		internal readonly AssemblyRefTable AssemblyRef = new AssemblyRefTable();

		// Token: 0x040001AB RID: 427
		internal readonly MethodPtrTable MethodPtr = new MethodPtrTable();

		// Token: 0x040001AC RID: 428
		internal readonly MethodDefTable MethodDef = new MethodDefTable();

		// Token: 0x040001AD RID: 429
		internal readonly NestedClassTable NestedClass = new NestedClassTable();

		// Token: 0x040001AE RID: 430
		internal readonly FileTable File = new FileTable();

		// Token: 0x040001AF RID: 431
		internal readonly ExportedTypeTable ExportedType = new ExportedTypeTable();

		// Token: 0x040001B0 RID: 432
		internal readonly ManifestResourceTable ManifestResource = new ManifestResourceTable();

		// Token: 0x040001B1 RID: 433
		internal readonly GenericParamTable GenericParam = new GenericParamTable();

		// Token: 0x040001B2 RID: 434
		internal readonly MethodSpecTable MethodSpec = new MethodSpecTable();

		// Token: 0x040001B3 RID: 435
		internal readonly GenericParamConstraintTable GenericParamConstraint = new GenericParamConstraintTable();

		// Token: 0x02000328 RID: 808
		internal sealed class GenericContext : IGenericContext
		{
			// Token: 0x0600259B RID: 9627 RVA: 0x000B3EEC File Offset: 0x000B20EC
			internal GenericContext(Type[] genericTypeArguments, Type[] genericMethodArguments)
			{
				this.genericTypeArguments = genericTypeArguments;
				this.genericMethodArguments = genericMethodArguments;
			}

			// Token: 0x0600259C RID: 9628 RVA: 0x000B3F02 File Offset: 0x000B2102
			public Type GetGenericTypeArgument(int index)
			{
				return this.genericTypeArguments[index];
			}

			// Token: 0x0600259D RID: 9629 RVA: 0x000B3F0C File Offset: 0x000B210C
			public Type GetGenericMethodArgument(int index)
			{
				return this.genericMethodArguments[index];
			}

			// Token: 0x04000E46 RID: 3654
			private readonly Type[] genericTypeArguments;

			// Token: 0x04000E47 RID: 3655
			private readonly Type[] genericMethodArguments;
		}
	}
}
