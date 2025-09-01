using System;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using IKVM.Reflection.Impl;
using IKVM.Reflection.Metadata;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000E9 RID: 233
	public sealed class ModuleBuilder : Module, ITypeOwner
	{
		// Token: 0x06000ADF RID: 2783 RVA: 0x000263B4 File Offset: 0x000245B4
		internal ModuleBuilder(AssemblyBuilder asm, string moduleName, string fileName, bool emitSymbolInfo) : base(asm.universe)
		{
			this.asm = asm;
			this.moduleName = moduleName;
			this.fileName = fileName;
			if (emitSymbolInfo)
			{
				this.symbolWriter = SymbolSupport.CreateSymbolWriterFor(this);
				if (this.universe.Deterministic && !this.symbolWriter.IsDeterministic)
				{
					throw new NotSupportedException();
				}
			}
			if (!this.universe.Deterministic)
			{
				this.__PEHeaderTimeDateStamp = DateTime.UtcNow;
				this.mvid = Guid.NewGuid();
			}
			this.moduleType = new TypeBuilder(this, null, "<Module>");
			this.types.Add(this.moduleType);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0002655C File Offset: 0x0002475C
		internal void PopulatePropertyAndEventTables()
		{
			foreach (TypeBuilder typeBuilder in this.types)
			{
				typeBuilder.PopulatePropertyAndEventTables();
			}
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x000265AC File Offset: 0x000247AC
		internal void WriteTypeDefTable(MetadataWriter mw)
		{
			int num = 1;
			int num2 = 1;
			foreach (TypeBuilder typeBuilder in this.types)
			{
				typeBuilder.WriteTypeDefRecord(mw, ref num, ref num2);
			}
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00026608 File Offset: 0x00024808
		internal void WriteMethodDefTable(int baseRVA, MetadataWriter mw)
		{
			int num = 1;
			foreach (TypeBuilder typeBuilder in this.types)
			{
				typeBuilder.WriteMethodDefRecords(baseRVA, mw, ref num);
			}
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00026660 File Offset: 0x00024860
		internal void WriteParamTable(MetadataWriter mw)
		{
			foreach (TypeBuilder typeBuilder in this.types)
			{
				typeBuilder.WriteParamRecords(mw);
			}
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x000266B4 File Offset: 0x000248B4
		internal void WriteFieldTable(MetadataWriter mw)
		{
			foreach (TypeBuilder typeBuilder in this.types)
			{
				typeBuilder.WriteFieldRecords(mw);
			}
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00026708 File Offset: 0x00024908
		internal int AllocPseudoToken()
		{
			int num = this.nextPseudoToken;
			this.nextPseudoToken = num - 1;
			return num;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00026726 File Offset: 0x00024926
		public TypeBuilder DefineType(string name)
		{
			return this.DefineType(name, TypeAttributes.AnsiClass);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00026730 File Offset: 0x00024930
		public TypeBuilder DefineType(string name, TypeAttributes attr)
		{
			return this.DefineType(name, attr, null);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002673B File Offset: 0x0002493B
		public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent)
		{
			return this.DefineType(name, attr, parent, PackingSize.Unspecified, 0);
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00026748 File Offset: 0x00024948
		public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, int typesize)
		{
			return this.DefineType(name, attr, parent, PackingSize.Unspecified, typesize);
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00026756 File Offset: 0x00024956
		public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, PackingSize packsize)
		{
			return this.DefineType(name, attr, parent, packsize, 0);
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00026764 File Offset: 0x00024964
		public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, Type[] interfaces)
		{
			TypeBuilder typeBuilder = this.DefineType(name, attr, parent);
			foreach (Type interfaceType in interfaces)
			{
				typeBuilder.AddInterfaceImplementation(interfaceType);
			}
			return typeBuilder;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00026798 File Offset: 0x00024998
		public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, PackingSize packingSize, int typesize)
		{
			string ns = null;
			int num = name.LastIndexOf('.');
			if (num > 0)
			{
				ns = name.Substring(0, num);
				name = name.Substring(num + 1);
			}
			TypeBuilder typeBuilder = this.__DefineType(ns, name);
			typeBuilder.__SetAttributes(attr);
			typeBuilder.SetParent(parent);
			if (packingSize != PackingSize.Unspecified || typesize != 0)
			{
				typeBuilder.__SetLayout((int)packingSize, typesize);
			}
			return typeBuilder;
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x000267F2 File Offset: 0x000249F2
		public TypeBuilder __DefineType(string ns, string name)
		{
			return this.DefineType(this, ns, name);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00026800 File Offset: 0x00024A00
		internal TypeBuilder DefineType(ITypeOwner owner, string ns, string name)
		{
			TypeBuilder typeBuilder = new TypeBuilder(owner, ns, name);
			this.types.Add(typeBuilder);
			return typeBuilder;
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00026824 File Offset: 0x00024A24
		public EnumBuilder DefineEnum(string name, TypeAttributes visibility, Type underlyingType)
		{
			TypeBuilder typeBuilder = this.DefineType(name, (visibility & TypeAttributes.VisibilityMask) | TypeAttributes.Sealed, this.universe.System_Enum);
			FieldBuilder fieldBuilder = typeBuilder.DefineField("value__", underlyingType, FieldAttributes.FamANDAssem | FieldAttributes.Family | FieldAttributes.SpecialName | FieldAttributes.RTSpecialName);
			return new EnumBuilder(typeBuilder, fieldBuilder);
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00026864 File Offset: 0x00024A64
		public FieldBuilder __DefineField(string name, Type type, CustomModifiers customModifiers, FieldAttributes attributes)
		{
			return this.moduleType.__DefineField(name, type, customModifiers, attributes);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00026876 File Offset: 0x00024A76
		[Obsolete("Please use __DefineField(string, Type, CustomModifiers, FieldAttributes) instead.")]
		public FieldBuilder __DefineField(string name, Type type, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers, FieldAttributes attributes)
		{
			return this.moduleType.DefineField(name, type, requiredCustomModifiers, optionalCustomModifiers, attributes);
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0002688A File Offset: 0x00024A8A
		public ConstructorBuilder __DefineModuleInitializer(MethodAttributes visibility)
		{
			return this.moduleType.DefineConstructor(visibility | MethodAttributes.Static | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName, CallingConventions.Standard, Type.EmptyTypes);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x000268AD File Offset: 0x00024AAD
		public FieldBuilder DefineUninitializedData(string name, int size, FieldAttributes attributes)
		{
			return this.moduleType.DefineUninitializedData(name, size, attributes);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x000268BD File Offset: 0x00024ABD
		public FieldBuilder DefineInitializedData(string name, byte[] data, FieldAttributes attributes)
		{
			return this.moduleType.DefineInitializedData(name, data, attributes);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x000268CD File Offset: 0x00024ACD
		public MethodBuilder DefineGlobalMethod(string name, MethodAttributes attributes, Type returnType, Type[] parameterTypes)
		{
			return this.moduleType.DefineMethod(name, attributes, returnType, parameterTypes);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x000268DF File Offset: 0x00024ADF
		public MethodBuilder DefineGlobalMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			return this.moduleType.DefineMethod(name, attributes, callingConvention, returnType, parameterTypes);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x000268F4 File Offset: 0x00024AF4
		public MethodBuilder DefineGlobalMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
		{
			return this.moduleType.DefineMethod(name, attributes, callingConvention, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers, parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0002691C File Offset: 0x00024B1C
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			return this.moduleType.DefinePInvokeMethod(name, dllName, attributes, callingConvention, returnType, parameterTypes, nativeCallConv, nativeCharSet);
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00026944 File Offset: 0x00024B44
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, string entryName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			return this.moduleType.DefinePInvokeMethod(name, dllName, entryName, attributes, callingConvention, returnType, parameterTypes, nativeCallConv, nativeCharSet);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0002696B File Offset: 0x00024B6B
		public void CreateGlobalFunctions()
		{
			this.moduleType.CreateType();
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0002697C File Offset: 0x00024B7C
		internal void AddTypeForwarder(Type type, bool includeNested)
		{
			this.ExportType(type);
			if (includeNested && !type.__IsMissing)
			{
				foreach (Type type2 in type.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic))
				{
					this.AddTypeForwarder(type2, true);
				}
			}
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x000269C0 File Offset: 0x00024BC0
		private int ExportType(Type type)
		{
			ExportedTypeTable.Record rec = default(ExportedTypeTable.Record);
			if (this.asm.ImageRuntimeVersion == "v2.0.50727")
			{
				rec.TypeDefId = type.MetadataToken;
			}
			this.SetTypeNameAndTypeNamespace(type.TypeName, out rec.TypeName, out rec.TypeNamespace);
			if (type.IsNested)
			{
				rec.Flags = 0;
				rec.Implementation = this.ExportType(type.DeclaringType);
			}
			else
			{
				rec.Flags = 2097152;
				rec.Implementation = this.ImportAssemblyRef(type.Assembly);
			}
			return 654311424 | this.ExportedType.FindOrAddRecord(rec);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00026A69 File Offset: 0x00024C69
		private void SetTypeNameAndTypeNamespace(TypeName name, out int typeName, out int typeNamespace)
		{
			typeName = this.Strings.Add(name.Name);
			typeNamespace = ((name.Namespace == null) ? 0 : this.Strings.Add(name.Namespace));
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00026A9F File Offset: 0x00024C9F
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00026AAE File Offset: 0x00024CAE
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			this.SetCustomAttribute(1, customBuilder);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00026AB8 File Offset: 0x00024CB8
		internal void SetCustomAttribute(int token, CustomAttributeBuilder customBuilder)
		{
			CustomAttributeTable.Record newRecord = default(CustomAttributeTable.Record);
			newRecord.Parent = token;
			newRecord.Type = (this.asm.IsWindowsRuntime ? customBuilder.Constructor.ImportTo(this) : this.GetConstructorToken(customBuilder.Constructor).Token);
			newRecord.Value = customBuilder.WriteBlob(this);
			this.CustomAttribute.AddRecord(newRecord);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00026B28 File Offset: 0x00024D28
		private void AddDeclSecurityRecord(int token, int action, int blob)
		{
			DeclSecurityTable.Record newRecord = default(DeclSecurityTable.Record);
			newRecord.Action = (short)action;
			newRecord.Parent = token;
			newRecord.PermissionSet = blob;
			this.DeclSecurity.AddRecord(newRecord);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00026B63 File Offset: 0x00024D63
		internal void AddDeclarativeSecurity(int token, SecurityAction securityAction, PermissionSet permissionSet)
		{
			this.AddDeclSecurityRecord(token, (int)securityAction, this.Blobs.Add(ByteBuffer.Wrap(Encoding.Unicode.GetBytes(permissionSet.ToXml().ToString()))));
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00026B94 File Offset: 0x00024D94
		internal void AddDeclarativeSecurity(int token, List<CustomAttributeBuilder> declarativeSecurity)
		{
			Dictionary<int, List<CustomAttributeBuilder>> dictionary = new Dictionary<int, List<CustomAttributeBuilder>>();
			foreach (CustomAttributeBuilder customAttributeBuilder in declarativeSecurity)
			{
				int num;
				if (customAttributeBuilder.ConstructorArgumentCount == 0)
				{
					num = 6;
				}
				else
				{
					num = (int)customAttributeBuilder.GetConstructorArgument(0);
				}
				if (customAttributeBuilder.IsLegacyDeclSecurity)
				{
					this.AddDeclSecurityRecord(token, num, customAttributeBuilder.WriteLegacyDeclSecurityBlob(this));
				}
				else
				{
					List<CustomAttributeBuilder> list;
					if (!dictionary.TryGetValue(num, out list))
					{
						list = new List<CustomAttributeBuilder>();
						dictionary.Add(num, list);
					}
					list.Add(customAttributeBuilder);
				}
			}
			foreach (KeyValuePair<int, List<CustomAttributeBuilder>> keyValuePair in dictionary)
			{
				this.AddDeclSecurityRecord(token, keyValuePair.Key, this.WriteDeclSecurityBlob(keyValuePair.Value));
			}
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00026C8C File Offset: 0x00024E8C
		private int WriteDeclSecurityBlob(List<CustomAttributeBuilder> list)
		{
			ByteBuffer byteBuffer = new ByteBuffer(100);
			ByteBuffer byteBuffer2 = new ByteBuffer(list.Count * 100);
			byteBuffer2.Write(46);
			byteBuffer2.WriteCompressedUInt(list.Count);
			foreach (CustomAttributeBuilder customAttributeBuilder in list)
			{
				byteBuffer2.Write(customAttributeBuilder.Constructor.DeclaringType.AssemblyQualifiedName);
				byteBuffer.Clear();
				customAttributeBuilder.WriteNamedArgumentsForDeclSecurity(this, byteBuffer);
				byteBuffer2.WriteCompressedUInt(byteBuffer.Length);
				byteBuffer2.Write(byteBuffer);
			}
			return this.Blobs.Add(byteBuffer2);
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00026D44 File Offset: 0x00024F44
		public void DefineManifestResource(string name, Stream stream, ResourceAttributes attribute)
		{
			this.resourceWriters.Add(new ModuleBuilder.ResourceWriterRecord(name, stream, attribute));
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00026D59 File Offset: 0x00024F59
		public IResourceWriter DefineResource(string name, string description)
		{
			return this.DefineResource(name, description, ResourceAttributes.Public);
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00026D64 File Offset: 0x00024F64
		public IResourceWriter DefineResource(string name, string description, ResourceAttributes attribute)
		{
			MemoryStream stream = new MemoryStream();
			ResourceWriter resourceWriter = new ResourceWriter(stream);
			this.resourceWriters.Add(new ModuleBuilder.ResourceWriterRecord(name, resourceWriter, stream, attribute));
			return resourceWriter;
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00026D94 File Offset: 0x00024F94
		internal void EmitResources()
		{
			int num = 0;
			foreach (ModuleBuilder.ResourceWriterRecord resourceWriterRecord in this.resourceWriters)
			{
				num = (num + 7 & -8);
				resourceWriterRecord.Emit(this, num);
				num += resourceWriterRecord.GetLength();
			}
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00026DFC File Offset: 0x00024FFC
		internal void WriteResources(MetadataWriter mw)
		{
			int num = 0;
			foreach (ModuleBuilder.ResourceWriterRecord resourceWriterRecord in this.resourceWriters)
			{
				int num2 = (num + 7 & -8) - num;
				for (int i = 0; i < num2; i++)
				{
					mw.Write(0);
				}
				resourceWriterRecord.Write(mw);
				num += resourceWriterRecord.GetLength() + num2;
			}
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00026E80 File Offset: 0x00025080
		internal void CloseResources()
		{
			foreach (ModuleBuilder.ResourceWriterRecord resourceWriterRecord in this.resourceWriters)
			{
				resourceWriterRecord.Close();
			}
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00026ED4 File Offset: 0x000250D4
		internal int GetManifestResourcesLength()
		{
			int num = 0;
			foreach (ModuleBuilder.ResourceWriterRecord resourceWriterRecord in this.resourceWriters)
			{
				num = (num + 7 & -8);
				num += resourceWriterRecord.GetLength();
			}
			return num;
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x00026F34 File Offset: 0x00025134
		public override Assembly Assembly
		{
			get
			{
				return this.asm;
			}
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00026F3C File Offset: 0x0002513C
		internal override Type FindType(TypeName name)
		{
			foreach (Type type in this.types)
			{
				if (type.TypeName == name)
				{
					return type;
				}
			}
			return null;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00026FA0 File Offset: 0x000251A0
		internal override Type FindTypeIgnoreCase(TypeName lowerCaseName)
		{
			foreach (Type type in this.types)
			{
				if (type.TypeName.ToLowerInvariant() == lowerCaseName)
				{
					return type;
				}
			}
			return null;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0002700C File Offset: 0x0002520C
		internal override void GetTypesImpl(List<Type> list)
		{
			foreach (Type type in this.types)
			{
				if (type != this.moduleType)
				{
					list.Add(type);
				}
			}
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00027070 File Offset: 0x00025270
		public int __GetAssemblyToken(Assembly assembly)
		{
			return this.ImportAssemblyRef(assembly);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00027079 File Offset: 0x00025279
		public TypeToken GetTypeToken(string name)
		{
			return new TypeToken(base.GetType(name, true, false).MetadataToken);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0002708E File Offset: 0x0002528E
		public TypeToken GetTypeToken(Type type)
		{
			if (type.Module == this && !this.asm.IsWindowsRuntime)
			{
				return new TypeToken(type.GetModuleBuilderToken());
			}
			return new TypeToken(this.ImportType(type));
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x000270C0 File Offset: 0x000252C0
		internal int GetTypeTokenForMemberRef(Type type)
		{
			if (type.__IsMissing)
			{
				return this.ImportType(type);
			}
			if (type.IsGenericTypeDefinition)
			{
				int num;
				if (!this.memberRefTypeTokens.TryGetValue(type, out num))
				{
					ByteBuffer bb = new ByteBuffer(5);
					Signature.WriteTypeSpec(this, bb, type);
					num = (452984832 | this.TypeSpec.AddRecord(this.Blobs.Add(bb)));
					this.memberRefTypeTokens.Add(type, num);
				}
				return num;
			}
			if (type.IsModulePseudoType)
			{
				return 436207616 | this.ModuleRef.FindOrAddRecord(this.Strings.Add(type.Module.ScopeName));
			}
			return this.GetTypeToken(type).Token;
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00027170 File Offset: 0x00025370
		private static bool IsFromGenericTypeDefinition(MemberInfo member)
		{
			Type declaringType = member.DeclaringType;
			return declaringType != null && !declaringType.__IsMissing && declaringType.IsGenericTypeDefinition;
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x000271A0 File Offset: 0x000253A0
		public FieldToken GetFieldToken(FieldInfo field)
		{
			FieldBuilder fieldBuilder = field as FieldBuilder;
			if (fieldBuilder != null && fieldBuilder.Module == this && !ModuleBuilder.IsFromGenericTypeDefinition(fieldBuilder))
			{
				return new FieldToken(fieldBuilder.MetadataToken);
			}
			return new FieldToken(field.ImportTo(this));
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x000271E8 File Offset: 0x000253E8
		public MethodToken GetMethodToken(MethodInfo method)
		{
			MethodBuilder methodBuilder = method as MethodBuilder;
			if (methodBuilder != null && methodBuilder.ModuleBuilder == this)
			{
				return new MethodToken(methodBuilder.MetadataToken);
			}
			return new MethodToken(method.ImportTo(this));
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00027226 File Offset: 0x00025426
		public MethodToken GetMethodToken(MethodInfo method, IEnumerable<Type> optionalParameterTypes)
		{
			return this.__GetMethodToken(method, Util.ToArray<Type>(optionalParameterTypes), null);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00027238 File Offset: 0x00025438
		public MethodToken __GetMethodToken(MethodInfo method, Type[] optionalParameterTypes, CustomModifiers[] customModifiers)
		{
			ByteBuffer bb = new ByteBuffer(16);
			method.MethodSignature.WriteMethodRefSig(this, bb, optionalParameterTypes, customModifiers);
			MemberRefTable.Record record = default(MemberRefTable.Record);
			if (method.Module == this)
			{
				record.Class = method.MetadataToken;
			}
			else
			{
				record.Class = this.GetTypeTokenForMemberRef(method.DeclaringType ?? method.Module.GetModuleType());
			}
			record.Name = this.Strings.Add(method.Name);
			record.Signature = this.Blobs.Add(bb);
			return new MethodToken(167772160 | this.MemberRef.FindOrAddRecord(record));
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x000272E0 File Offset: 0x000254E0
		internal MethodToken GetMethodTokenForIL(MethodInfo method)
		{
			if (method.IsGenericMethodDefinition)
			{
				MethodInfo methodInfo = method;
				method = methodInfo.MakeGenericMethod(methodInfo.GetGenericArguments());
			}
			if (ModuleBuilder.IsFromGenericTypeDefinition(method))
			{
				return new MethodToken(method.ImportTo(this));
			}
			return this.GetMethodToken(method);
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00027314 File Offset: 0x00025514
		internal int GetMethodTokenWinRT(MethodInfo method)
		{
			if (!this.asm.IsWindowsRuntime)
			{
				return this.GetMethodToken(method).Token;
			}
			return method.ImportTo(this);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00027345 File Offset: 0x00025545
		public MethodToken GetConstructorToken(ConstructorInfo constructor)
		{
			return this.GetMethodToken(constructor.GetMethodInfo());
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00027353 File Offset: 0x00025553
		public MethodToken GetConstructorToken(ConstructorInfo constructor, IEnumerable<Type> optionalParameterTypes)
		{
			return this.GetMethodToken(constructor.GetMethodInfo(), optionalParameterTypes);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00027362 File Offset: 0x00025562
		public MethodToken __GetConstructorToken(ConstructorInfo constructor, Type[] optionalParameterTypes, CustomModifiers[] customModifiers)
		{
			return this.__GetMethodToken(constructor.GetMethodInfo(), optionalParameterTypes, customModifiers);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00027374 File Offset: 0x00025574
		internal int ImportMethodOrField(Type declaringType, string name, Signature sig)
		{
			ModuleBuilder.MemberRefKey key = new ModuleBuilder.MemberRefKey(declaringType, name, sig);
			int num;
			if (!this.importedMemberRefs.TryGetValue(key, out num))
			{
				MemberRefTable.Record newRecord = default(MemberRefTable.Record);
				newRecord.Class = this.GetTypeTokenForMemberRef(declaringType);
				newRecord.Name = this.Strings.Add(name);
				ByteBuffer bb = new ByteBuffer(16);
				sig.WriteSig(this, bb);
				newRecord.Signature = this.Blobs.Add(bb);
				num = (167772160 | this.MemberRef.AddRecord(newRecord));
				this.importedMemberRefs.Add(key, num);
			}
			return num;
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00027408 File Offset: 0x00025608
		internal int ImportMethodSpec(Type declaringType, MethodInfo method, Type[] genericParameters)
		{
			ModuleBuilder.MethodSpecKey key = new ModuleBuilder.MethodSpecKey(declaringType, method.Name, method.MethodSignature, genericParameters);
			int num;
			if (!this.importedMethodSpecs.TryGetValue(key, out num))
			{
				MethodSpecTable.Record record = default(MethodSpecTable.Record);
				MethodBuilder methodBuilder = method as MethodBuilder;
				if (methodBuilder != null && methodBuilder.ModuleBuilder == this && !declaringType.IsGenericType)
				{
					record.Method = methodBuilder.MetadataToken;
				}
				else
				{
					record.Method = this.ImportMethodOrField(declaringType, method.Name, method.MethodSignature);
				}
				ByteBuffer bb = new ByteBuffer(10);
				Signature.WriteMethodSpec(this, bb, genericParameters);
				record.Instantiation = this.Blobs.Add(bb);
				num = (721420288 | this.MethodSpec.FindOrAddRecord(record));
				this.importedMethodSpecs.Add(key, num);
			}
			return num;
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x000274D8 File Offset: 0x000256D8
		internal int ImportType(Type type)
		{
			int num;
			if (!this.typeTokens.TryGetValue(type, out num))
			{
				if (type.HasElementType || type.IsConstructedGenericType || type.__IsFunctionPointer)
				{
					ByteBuffer bb = new ByteBuffer(5);
					Signature.WriteTypeSpec(this, bb, type);
					num = (452984832 | this.TypeSpec.AddRecord(this.Blobs.Add(bb)));
				}
				else
				{
					TypeRefTable.Record newRecord = default(TypeRefTable.Record);
					if (type.IsNested)
					{
						newRecord.ResolutionScope = this.GetTypeToken(type.DeclaringType).Token;
					}
					else if (type.Module == this)
					{
						newRecord.ResolutionScope = 1;
					}
					else
					{
						newRecord.ResolutionScope = this.ImportAssemblyRef(type.Assembly);
					}
					this.SetTypeNameAndTypeNamespace(type.TypeName, out newRecord.TypeName, out newRecord.TypeNamespace);
					num = (16777216 | this.TypeRef.AddRecord(newRecord));
				}
				this.typeTokens.Add(type, num);
			}
			return num;
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x000275D0 File Offset: 0x000257D0
		private int ImportAssemblyRef(Assembly asm)
		{
			int num;
			if (!this.referencedAssemblies.TryGetValue(asm, out num))
			{
				num = this.AllocPseudoToken();
				this.referencedAssemblies.Add(asm, num);
			}
			return num;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00027604 File Offset: 0x00025804
		internal void FillAssemblyRefTable()
		{
			foreach (KeyValuePair<Assembly, int> keyValuePair in this.referencedAssemblies)
			{
				if (ModuleBuilder.IsPseudoToken(keyValuePair.Value))
				{
					this.RegisterTokenFixup(keyValuePair.Value, this.FindOrAddAssemblyRef(keyValuePair.Key.GetName(), false));
				}
			}
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00027680 File Offset: 0x00025880
		private int FindOrAddAssemblyRef(AssemblyName name, bool alwaysAdd)
		{
			AssemblyRefTable.Record record = default(AssemblyRefTable.Record);
			Version version = name.Version ?? new Version(0, 0, 0, 0);
			record.MajorVersion = (ushort)version.Major;
			record.MinorVersion = (ushort)version.Minor;
			record.BuildNumber = (ushort)version.Build;
			record.RevisionNumber = (ushort)version.Revision;
			record.Flags = (int)(name.Flags & ~AssemblyNameFlags.PublicKey);
			if ((name.RawFlags & (AssemblyNameFlags)128) != AssemblyNameFlags.None)
			{
				record.Flags |= (int)(name.RawFlags & (AssemblyNameFlags)112);
			}
			if (name.ContentType == AssemblyContentType.WindowsRuntime)
			{
				record.Flags |= 512;
			}
			byte[] array = null;
			if (ModuleBuilder.usePublicKeyAssemblyReference)
			{
				array = name.GetPublicKey();
			}
			if (array == null || array.Length == 0)
			{
				array = (name.GetPublicKeyToken() ?? Empty<byte>.Array);
			}
			else
			{
				record.Flags |= 1;
			}
			record.PublicKeyOrToken = this.Blobs.Add(ByteBuffer.Wrap(array));
			record.Name = this.Strings.Add(name.Name);
			record.Culture = ((name.Culture == null) ? 0 : this.Strings.Add(name.Culture));
			if (name.hash != null)
			{
				record.HashValue = this.Blobs.Add(ByteBuffer.Wrap(name.hash));
			}
			else
			{
				record.HashValue = 0;
			}
			return 587202560 | (alwaysAdd ? this.AssemblyRef.AddRecord(record) : this.AssemblyRef.FindOrAddRecord(record));
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00027808 File Offset: 0x00025A08
		internal void WriteSymbolTokenMap()
		{
			for (int i = 0; i < this.resolvedTokens.Count; i++)
			{
				int num = this.resolvedTokens[i];
				int oldToken = i + 1 | (num & -16777216);
				SymbolSupport.RemapToken(this.symbolWriter, oldToken, num);
			}
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00027854 File Offset: 0x00025A54
		internal void RegisterTokenFixup(int pseudoToken, int realToken)
		{
			int num = -(pseudoToken + 1);
			while (this.resolvedTokens.Count <= num)
			{
				this.resolvedTokens.Add(0);
			}
			this.resolvedTokens[num] = realToken;
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0002788F File Offset: 0x00025A8F
		internal static bool IsPseudoToken(int token)
		{
			return token < 0;
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00027898 File Offset: 0x00025A98
		internal int ResolvePseudoToken(int pseudoToken)
		{
			int index = -(pseudoToken + 1);
			return this.resolvedTokens[index];
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x000278B8 File Offset: 0x00025AB8
		internal void ApplyUnmanagedExports(ImageFileMachine imageFileMachine)
		{
			if (this.unmanagedExports.Count != 0)
			{
				int type;
				int num;
				if (imageFileMachine != ImageFileMachine.I386 && imageFileMachine != ImageFileMachine.ARM)
				{
					if (imageFileMachine != ImageFileMachine.AMD64)
					{
						throw new NotSupportedException();
					}
					type = 6;
					num = 8;
				}
				else
				{
					type = 5;
					num = 4;
				}
				List<MethodBuilder> list = new List<MethodBuilder>();
				for (int i = 0; i < this.unmanagedExports.Count; i++)
				{
					if (this.unmanagedExports[i].mb != null)
					{
						list.Add(this.unmanagedExports[i].mb);
					}
				}
				if (list.Count != 0)
				{
					RelativeVirtualAddress relativeVirtualAddress = this.__AddVTableFixups(list.ToArray(), type);
					for (int j = 0; j < this.unmanagedExports.Count; j++)
					{
						if (this.unmanagedExports[j].mb != null)
						{
							UnmanagedExport value = this.unmanagedExports[j];
							value.rva = new RelativeVirtualAddress(relativeVirtualAddress.initializedDataOffset + (uint)(list.IndexOf(this.unmanagedExports[j].mb) * num));
							this.unmanagedExports[j] = value;
						}
					}
				}
			}
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x000279E8 File Offset: 0x00025BE8
		internal void FixupMethodBodyTokens()
		{
			int num = 100663297;
			int num2 = 67108865;
			int num3 = 134217729;
			foreach (TypeBuilder typeBuilder in this.types)
			{
				typeBuilder.ResolveMethodAndFieldTokens(ref num, ref num2, ref num3);
			}
			foreach (int position in this.tokenFixupOffsets)
			{
				this.methodBodies.Position = position;
				int int32AtCurrentPosition = this.methodBodies.GetInt32AtCurrentPosition();
				this.methodBodies.Write(this.ResolvePseudoToken(int32AtCurrentPosition));
			}
			foreach (ModuleBuilder.VTableFixups vtableFixups in this.vtablefixups)
			{
				for (int i = 0; i < (int)vtableFixups.count; i++)
				{
					this.initializedData.Position = (int)(vtableFixups.initializedDataOffset + (uint)(i * vtableFixups.SlotWidth));
					this.initializedData.Write(this.ResolvePseudoToken(this.initializedData.GetInt32AtCurrentPosition()));
				}
			}
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00027B48 File Offset: 0x00025D48
		private int GetHeaderLength()
		{
			return 16 + ModuleBuilder.StringToPaddedUTF8Length(this.asm.ImageRuntimeVersion) + 2 + 2 + 4 + 4 + 4 + 4 + 4 + 12 + 4 + 4 + 4 + 4 + 4 + 8 + (this.Blobs.IsEmpty ? 0 : 16);
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x00027B98 File Offset: 0x00025D98
		internal int MetadataLength
		{
			get
			{
				return this.GetHeaderLength() + (this.Blobs.IsEmpty ? 0 : this.Blobs.Length) + this.Tables.Length + this.Strings.Length + this.UserStrings.Length + this.Guids.Length;
			}
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00027BF8 File Offset: 0x00025DF8
		internal void WriteMetadata(MetadataWriter mw, out int guidHeapOffset)
		{
			mw.Write(1112167234);
			mw.Write(1);
			mw.Write(1);
			mw.Write(0);
			byte[] array = ModuleBuilder.StringToPaddedUTF8(this.asm.ImageRuntimeVersion);
			mw.Write(array.Length);
			mw.Write(array);
			mw.Write(0);
			if (this.Blobs.IsEmpty)
			{
				mw.Write(4);
			}
			else
			{
				mw.Write(5);
			}
			int num = this.GetHeaderLength();
			mw.Write(num);
			mw.Write(this.Tables.Length);
			mw.Write(ModuleBuilder.StringToPaddedUTF8("#~"));
			num += this.Tables.Length;
			mw.Write(num);
			mw.Write(this.Strings.Length);
			mw.Write(ModuleBuilder.StringToPaddedUTF8("#Strings"));
			num += this.Strings.Length;
			mw.Write(num);
			mw.Write(this.UserStrings.Length);
			mw.Write(ModuleBuilder.StringToPaddedUTF8("#US"));
			num += this.UserStrings.Length;
			mw.Write(num);
			mw.Write(this.Guids.Length);
			mw.Write(ModuleBuilder.StringToPaddedUTF8("#GUID"));
			num += this.Guids.Length;
			if (!this.Blobs.IsEmpty)
			{
				mw.Write(num);
				mw.Write(this.Blobs.Length);
				mw.Write(ModuleBuilder.StringToPaddedUTF8("#Blob"));
			}
			this.Tables.Write(mw);
			this.Strings.Write(mw);
			this.UserStrings.Write(mw);
			guidHeapOffset = mw.Position;
			this.Guids.Write(mw);
			if (!this.Blobs.IsEmpty)
			{
				this.Blobs.Write(mw);
			}
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00027DCF File Offset: 0x00025FCF
		private static int StringToPaddedUTF8Length(string str)
		{
			return Encoding.UTF8.GetByteCount(str) + 4 & -4;
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00027DE4 File Offset: 0x00025FE4
		private static byte[] StringToPaddedUTF8(string str)
		{
			byte[] array = new byte[Encoding.UTF8.GetByteCount(str) + 4 & -4];
			Encoding.UTF8.GetBytes(str, 0, str.Length, array, 0);
			return array;
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00027E1D File Offset: 0x0002601D
		internal override void ExportTypes(int fileToken, ModuleBuilder manifestModule)
		{
			manifestModule.ExportTypes(this.types.ToArray(), fileToken);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00027E34 File Offset: 0x00026034
		internal void ExportTypes(Type[] types, int fileToken)
		{
			Dictionary<Type, int> dictionary = new Dictionary<Type, int>();
			foreach (Type type in types)
			{
				if (!type.IsModulePseudoType && ModuleBuilder.IsVisible(type))
				{
					ExportedTypeTable.Record newRecord = default(ExportedTypeTable.Record);
					newRecord.Flags = (int)type.Attributes;
					newRecord.TypeDefId = type.MetadataToken;
					this.SetTypeNameAndTypeNamespace(type.TypeName, out newRecord.TypeName, out newRecord.TypeNamespace);
					if (type.IsNested)
					{
						newRecord.Implementation = dictionary[type.DeclaringType];
					}
					else
					{
						newRecord.Implementation = fileToken;
					}
					int value = 654311424 | this.ExportedType.AddRecord(newRecord);
					dictionary.Add(type, value);
				}
			}
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00027EF3 File Offset: 0x000260F3
		private static bool IsVisible(Type type)
		{
			return type.IsPublic || ((type.IsNestedFamily || type.IsNestedFamORAssem || type.IsNestedPublic) && ModuleBuilder.IsVisible(type.DeclaringType));
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00027F24 File Offset: 0x00026124
		internal void AddConstant(int parentToken, object defaultValue)
		{
			ConstantTable.Record newRecord = default(ConstantTable.Record);
			newRecord.Parent = parentToken;
			ByteBuffer byteBuffer = new ByteBuffer(16);
			if (defaultValue == null)
			{
				newRecord.Type = 18;
				byteBuffer.Write(0);
			}
			else if (defaultValue is bool)
			{
				newRecord.Type = 2;
				byteBuffer.Write(((bool)defaultValue) ? 1 : 0);
			}
			else if (defaultValue is char)
			{
				newRecord.Type = 3;
				byteBuffer.Write((ushort)((char)defaultValue));
			}
			else if (defaultValue is sbyte)
			{
				newRecord.Type = 4;
				byteBuffer.Write((sbyte)defaultValue);
			}
			else if (defaultValue is byte)
			{
				newRecord.Type = 5;
				byteBuffer.Write((byte)defaultValue);
			}
			else if (defaultValue is short)
			{
				newRecord.Type = 6;
				byteBuffer.Write((short)defaultValue);
			}
			else if (defaultValue is ushort)
			{
				newRecord.Type = 7;
				byteBuffer.Write((ushort)defaultValue);
			}
			else if (defaultValue is int)
			{
				newRecord.Type = 8;
				byteBuffer.Write((int)defaultValue);
			}
			else if (defaultValue is uint)
			{
				newRecord.Type = 9;
				byteBuffer.Write((uint)defaultValue);
			}
			else if (defaultValue is long)
			{
				newRecord.Type = 10;
				byteBuffer.Write((long)defaultValue);
			}
			else if (defaultValue is ulong)
			{
				newRecord.Type = 11;
				byteBuffer.Write((ulong)defaultValue);
			}
			else if (defaultValue is float)
			{
				newRecord.Type = 12;
				byteBuffer.Write((float)defaultValue);
			}
			else if (defaultValue is double)
			{
				newRecord.Type = 13;
				byteBuffer.Write((double)defaultValue);
			}
			else if (defaultValue is string)
			{
				newRecord.Type = 14;
				foreach (char value in (string)defaultValue)
				{
					byteBuffer.Write((ushort)value);
				}
			}
			else
			{
				if (!(defaultValue is DateTime))
				{
					throw new ArgumentException();
				}
				newRecord.Type = 10;
				byteBuffer.Write(((DateTime)defaultValue).Ticks);
			}
			newRecord.Value = this.Blobs.Add(byteBuffer);
			this.Constant.AddRecord(newRecord);
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x00005936 File Offset: 0x00003B36
		ModuleBuilder ITypeOwner.ModuleBuilder
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0002817F File Offset: 0x0002637F
		internal override Type ResolveType(int metadataToken, IGenericContext context)
		{
			if (metadataToken >> 24 != 2)
			{
				throw new NotImplementedException();
			}
			return this.types[(metadataToken & 16777215) - 1];
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x000281A4 File Offset: 0x000263A4
		public override MethodBase ResolveMethod(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			if (genericTypeArguments != null || genericMethodArguments != null)
			{
				throw new NotImplementedException();
			}
			if (metadataToken >> 24 == 10)
			{
				foreach (KeyValuePair<ModuleBuilder.MemberRefKey, int> keyValuePair in this.importedMemberRefs)
				{
					if (keyValuePair.Value == metadataToken)
					{
						return keyValuePair.Key.LookupMethod();
					}
				}
			}
			if (((long)metadataToken & (long)((ulong)-16777216)) == 100663296L)
			{
				metadataToken = -(metadataToken & 16777215);
			}
			foreach (TypeBuilder typeBuilder in this.types)
			{
				MethodBase methodBase = ((TypeBuilder)typeBuilder).LookupMethod(metadataToken);
				if (methodBase != null)
				{
					return methodBase;
				}
			}
			return this.moduleType.LookupMethod(metadataToken);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override FieldInfo ResolveField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override MemberInfo ResolveMember(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override string ResolveString(int metadataToken)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x000282A4 File Offset: 0x000264A4
		public override string FullyQualifiedName
		{
			get
			{
				return Path.GetFullPath(Path.Combine(this.asm.dir, this.fileName));
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x000282C1 File Offset: 0x000264C1
		public override string Name
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x000282C9 File Offset: 0x000264C9
		internal Guid GetModuleVersionIdOrEmpty()
		{
			return this.mvid;
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x000282D1 File Offset: 0x000264D1
		public override Guid ModuleVersionId
		{
			get
			{
				if (this.mvid == Guid.Empty && this.universe.Deterministic)
				{
					throw new InvalidOperationException();
				}
				return this.mvid;
			}
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x000282FE File Offset: 0x000264FE
		public void __SetModuleVersionId(Guid guid)
		{
			if (guid == Guid.Empty && this.universe.Deterministic)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.mvid = guid;
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00028327 File Offset: 0x00026527
		internal uint GetTimeDateStamp()
		{
			return this.timestamp;
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x00028330 File Offset: 0x00026530
		// (set) Token: 0x06000B40 RID: 2880 RVA: 0x00028360 File Offset: 0x00026560
		public DateTime __PEHeaderTimeDateStamp
		{
			get
			{
				return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(this.timestamp);
			}
			set
			{
				if (value < new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc) || value > new DateTime(2106, 2, 7, 6, 28, 15, DateTimeKind.Utc))
				{
					throw new ArgumentOutOfRangeException();
				}
				this.timestamp = (uint)(value - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
			}
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override Type[] __ResolveOptionalParameterTypes(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments, out CustomModifiers[] customModifiers)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x000283CA File Offset: 0x000265CA
		public override string ScopeName
		{
			get
			{
				return this.moduleName;
			}
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x000283D2 File Offset: 0x000265D2
		public void DefineUnmanagedResource(string resourceFileName)
		{
			this.unmanagedResources = new ResourceSection();
			this.unmanagedResources.ExtractResources(System.IO.File.ReadAllBytes(resourceFileName));
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x000022F4 File Offset: 0x000004F4
		public bool IsTransient()
		{
			return false;
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x000283F0 File Offset: 0x000265F0
		public void SetUserEntryPoint(MethodInfo entryPoint)
		{
			int num = entryPoint.MetadataToken;
			if (num < 0)
			{
				num = (-num | 100663296);
			}
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00028411 File Offset: 0x00026611
		public StringToken GetStringConstant(string str)
		{
			return new StringToken(this.UserStrings.Add(str) | 1879048192);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0002842A File Offset: 0x0002662A
		public SignatureToken GetSignatureToken(SignatureHelper sigHelper)
		{
			return new SignatureToken(this.StandAloneSig.FindOrAddRecord(this.Blobs.Add(sigHelper.GetSignature(this))) | 285212672);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00028454 File Offset: 0x00026654
		public SignatureToken GetSignatureToken(byte[] sigBytes, int sigLength)
		{
			return new SignatureToken(this.StandAloneSig.FindOrAddRecord(this.Blobs.Add(ByteBuffer.Wrap(sigBytes, sigLength))) | 285212672);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0002847E File Offset: 0x0002667E
		public MethodInfo GetArrayMethod(Type arrayClass, string methodName, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			return new ArrayMethod(this, arrayClass, methodName, callingConvention, returnType, parameterTypes);
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0002848D File Offset: 0x0002668D
		public MethodToken GetArrayMethodToken(Type arrayClass, string methodName, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			return this.GetMethodToken(this.GetArrayMethod(arrayClass, methodName, callingConvention, returnType, parameterTypes));
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x000284A2 File Offset: 0x000266A2
		internal override Type GetModuleType()
		{
			return this.moduleType;
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x000284AA File Offset: 0x000266AA
		internal override ByteReader GetBlob(int blobIndex)
		{
			return this.Blobs.GetBlob(blobIndex);
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x000284B8 File Offset: 0x000266B8
		internal int GetSignatureBlobIndex(Signature sig)
		{
			ByteBuffer bb = new ByteBuffer(16);
			sig.WriteSig(this, bb);
			return this.Blobs.Add(bb);
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x000284E1 File Offset: 0x000266E1
		// (set) Token: 0x06000B4F RID: 2895 RVA: 0x000284E9 File Offset: 0x000266E9
		public new long __ImageBase
		{
			get
			{
				return this.imageBaseAddress;
			}
			set
			{
				this.imageBaseAddress = value;
			}
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x000284E1 File Offset: 0x000266E1
		protected override long GetImageBaseImpl()
		{
			return this.imageBaseAddress;
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x000284F2 File Offset: 0x000266F2
		// (set) Token: 0x06000B52 RID: 2898 RVA: 0x000284FA File Offset: 0x000266FA
		public new long __StackReserve
		{
			get
			{
				return this.stackReserve;
			}
			set
			{
				this.stackReserve = value;
			}
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x000284F2 File Offset: 0x000266F2
		protected override long GetStackReserveImpl()
		{
			return this.stackReserve;
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00028503 File Offset: 0x00026703
		[Obsolete("Use __StackReserve property.")]
		public void __SetStackReserve(long stackReserve)
		{
			this.__StackReserve = stackReserve;
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0002850C File Offset: 0x0002670C
		internal ulong GetStackReserve(ulong defaultValue)
		{
			if (this.stackReserve != -1L)
			{
				return (ulong)this.stackReserve;
			}
			return defaultValue;
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x00028520 File Offset: 0x00026720
		// (set) Token: 0x06000B57 RID: 2903 RVA: 0x00028528 File Offset: 0x00026728
		public new int __FileAlignment
		{
			get
			{
				return this.fileAlignment;
			}
			set
			{
				this.fileAlignment = value;
			}
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00028520 File Offset: 0x00026720
		protected override int GetFileAlignmentImpl()
		{
			return this.fileAlignment;
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x00028531 File Offset: 0x00026731
		// (set) Token: 0x06000B5A RID: 2906 RVA: 0x00028539 File Offset: 0x00026739
		public new DllCharacteristics __DllCharacteristics
		{
			get
			{
				return this.dllCharacteristics;
			}
			set
			{
				this.dllCharacteristics = value;
			}
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00028531 File Offset: 0x00026731
		protected override DllCharacteristics GetDllCharacteristicsImpl()
		{
			return this.dllCharacteristics;
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x00028542 File Offset: 0x00026742
		public override int MDStreamVersion
		{
			get
			{
				return this.asm.mdStreamVersion;
			}
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x00028550 File Offset: 0x00026750
		private int AddTypeRefByName(int resolutionScope, string ns, string name)
		{
			TypeRefTable.Record newRecord = default(TypeRefTable.Record);
			newRecord.ResolutionScope = resolutionScope;
			this.SetTypeNameAndTypeNamespace(new TypeName(ns, name), out newRecord.TypeName, out newRecord.TypeNamespace);
			return 16777216 | this.TypeRef.AddRecord(newRecord);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0002859A File Offset: 0x0002679A
		public void __Save(PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
		{
			this.SaveImpl(null, portableExecutableKind, imageFileMachine);
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x000285A5 File Offset: 0x000267A5
		public void __Save(Stream stream, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
		{
			if (!stream.CanRead || !stream.CanWrite || !stream.CanSeek || stream.Position != 0L)
			{
				throw new ArgumentException("Stream must support read/write/seek and current position must be zero.", "stream");
			}
			this.SaveImpl(stream, portableExecutableKind, imageFileMachine);
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x000285E0 File Offset: 0x000267E0
		private void SaveImpl(Stream streamOrNull, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
		{
			this.SetIsSaved();
			this.PopulatePropertyAndEventTables();
			IList<CustomAttributeData> customAttributesData = this.asm.GetCustomAttributesData(null);
			if (customAttributesData.Count > 0)
			{
				int resolutionScope = this.ImportAssemblyRef(this.universe.Mscorlib);
				int[] array = new int[4];
				string[] array2 = new string[]
				{
					"AssemblyAttributesGoHere",
					"AssemblyAttributesGoHereM",
					"AssemblyAttributesGoHereS",
					"AssemblyAttributesGoHereSM"
				};
				foreach (CustomAttributeData customAttributeData in customAttributesData)
				{
					int num;
					if (customAttributeData.Constructor.DeclaringType.BaseType == this.universe.System_Security_Permissions_CodeAccessSecurityAttribute)
					{
						if (customAttributeData.Constructor.DeclaringType.IsAllowMultipleCustomAttribute)
						{
							num = 3;
						}
						else
						{
							num = 2;
						}
					}
					else if (customAttributeData.Constructor.DeclaringType.IsAllowMultipleCustomAttribute)
					{
						num = 1;
					}
					else
					{
						num = 0;
					}
					if (array[num] == 0)
					{
						array[num] = this.AddTypeRefByName(resolutionScope, "System.Runtime.CompilerServices", array2[num]);
					}
					this.SetCustomAttribute(array[num], customAttributeData.__ToBuilder());
				}
			}
			this.FillAssemblyRefTable();
			this.EmitResources();
			ModuleWriter.WriteModule(null, null, this, PEFileKinds.Dll, portableExecutableKind, imageFileMachine, this.unmanagedResources, 0, streamOrNull);
			this.CloseResources();
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x00028740 File Offset: 0x00026940
		public void __AddAssemblyReference(AssemblyName assemblyName)
		{
			this.__AddAssemblyReference(assemblyName, null);
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0002874C File Offset: 0x0002694C
		public void __AddAssemblyReference(AssemblyName assemblyName, Assembly assembly)
		{
			if (this.referencedAssemblyNames == null)
			{
				this.referencedAssemblyNames = new List<AssemblyName>();
			}
			this.referencedAssemblyNames.Add((AssemblyName)assemblyName.Clone());
			int value = this.FindOrAddAssemblyRef(assemblyName, true);
			if (assembly != null)
			{
				this.referencedAssemblies.Add(assembly, value);
			}
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0002879C File Offset: 0x0002699C
		public override AssemblyName[] __GetReferencedAssemblies()
		{
			List<AssemblyName> list = new List<AssemblyName>();
			if (this.referencedAssemblyNames != null)
			{
				foreach (AssemblyName item in this.referencedAssemblyNames)
				{
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
			}
			foreach (Assembly assembly in this.referencedAssemblies.Keys)
			{
				AssemblyName name = assembly.GetName();
				if (!list.Contains(name))
				{
					list.Add(name);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x00028864 File Offset: 0x00026A64
		public void __AddModuleReference(string module)
		{
			this.ModuleRef.FindOrAddRecord((module == null) ? 0 : this.Strings.Add(module));
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00028884 File Offset: 0x00026A84
		public override string[] __GetReferencedModules()
		{
			string[] array = new string[this.ModuleRef.RowCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.Strings.Find(this.ModuleRef.records[i]);
			}
			return array;
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x000288CC File Offset: 0x00026ACC
		public override Type[] __GetReferencedTypes()
		{
			List<Type> list = new List<Type>();
			foreach (KeyValuePair<Type, int> keyValuePair in this.typeTokens)
			{
				if (keyValuePair.Value >> 24 == 1)
				{
					list.Add(keyValuePair.Key);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override Type[] __GetExportedTypes()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x00028940 File Offset: 0x00026B40
		public int __AddModule(int flags, string name, byte[] hash)
		{
			FileTable.Record newRecord = default(FileTable.Record);
			newRecord.Flags = flags;
			newRecord.Name = this.Strings.Add(name);
			newRecord.HashValue = this.Blobs.Add(ByteBuffer.Wrap(hash));
			return 637534208 + this.File.AddRecord(newRecord);
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0002899C File Offset: 0x00026B9C
		public int __AddManifestResource(int offset, ResourceAttributes flags, string name, int implementation)
		{
			ManifestResourceTable.Record newRecord = default(ManifestResourceTable.Record);
			newRecord.Offset = offset;
			newRecord.Flags = (int)flags;
			newRecord.Name = this.Strings.Add(name);
			newRecord.Implementation = implementation;
			return 671088640 + this.ManifestResource.AddRecord(newRecord);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x000289EF File Offset: 0x00026BEF
		public void __SetCustomAttributeFor(int token, CustomAttributeBuilder customBuilder)
		{
			this.SetCustomAttribute(token, customBuilder);
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x000289FC File Offset: 0x00026BFC
		public RelativeVirtualAddress __AddVTableFixups(MethodBuilder[] methods, int type)
		{
			this.initializedData.Align(8);
			ModuleBuilder.VTableFixups vtableFixups;
			vtableFixups.initializedDataOffset = (uint)this.initializedData.Position;
			vtableFixups.count = (ushort)methods.Length;
			vtableFixups.type = (ushort)type;
			foreach (MethodBuilder methodBuilder in methods)
			{
				this.initializedData.Write(methodBuilder.MetadataToken);
				if (vtableFixups.SlotWidth == 8)
				{
					this.initializedData.Write(0);
				}
			}
			this.vtablefixups.Add(vtableFixups);
			return new RelativeVirtualAddress(vtableFixups.initializedDataOffset);
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x00028A8D File Offset: 0x00026C8D
		public void __AddUnmanagedExportStub(string name, int ordinal, RelativeVirtualAddress rva)
		{
			this.AddUnmanagedExport(name, ordinal, null, rva);
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00028A9C File Offset: 0x00026C9C
		internal void AddUnmanagedExport(string name, int ordinal, MethodBuilder methodBuilder, RelativeVirtualAddress rva)
		{
			UnmanagedExport item;
			item.name = name;
			item.ordinal = ordinal;
			item.mb = methodBuilder;
			item.rva = rva;
			this.unmanagedExports.Add(item);
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00028AD8 File Offset: 0x00026CD8
		internal void SetInterfaceImplementationCustomAttribute(TypeBuilder typeBuilder, Type interfaceType, CustomAttributeBuilder cab)
		{
			if (this.interfaceImplCustomAttributes == null)
			{
				this.interfaceImplCustomAttributes = new List<ModuleBuilder.InterfaceImplCustomAttribute>();
			}
			ModuleBuilder.InterfaceImplCustomAttribute interfaceImplCustomAttribute;
			interfaceImplCustomAttribute.type = typeBuilder.MetadataToken;
			int num = this.GetTypeToken(interfaceType).Token;
			int num2 = num >> 24;
			if (num2 != 1)
			{
				if (num2 != 2)
				{
					if (num2 != 27)
					{
						throw new InvalidOperationException();
					}
					num = ((num & 16777215) << 2 | 2);
				}
				else
				{
					num = ((num & 16777215) << 2 | 0);
				}
			}
			else
			{
				num = ((num & 16777215) << 2 | 1);
			}
			interfaceImplCustomAttribute.interfaceType = num;
			interfaceImplCustomAttribute.pseudoToken = this.AllocPseudoToken();
			this.interfaceImplCustomAttributes.Add(interfaceImplCustomAttribute);
			this.SetCustomAttribute(interfaceImplCustomAttribute.pseudoToken, cab);
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x00028B88 File Offset: 0x00026D88
		internal void ResolveInterfaceImplPseudoTokens()
		{
			if (this.interfaceImplCustomAttributes != null)
			{
				foreach (ModuleBuilder.InterfaceImplCustomAttribute interfaceImplCustomAttribute in this.interfaceImplCustomAttributes)
				{
					for (int i = 0; i < this.InterfaceImpl.records.Length; i++)
					{
						if (this.InterfaceImpl.records[i].Class == interfaceImplCustomAttribute.type && this.InterfaceImpl.records[i].Interface == interfaceImplCustomAttribute.interfaceType)
						{
							this.RegisterTokenFixup(interfaceImplCustomAttribute.pseudoToken, 9 << 24 | i + 1);
							break;
						}
					}
				}
			}
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00028C4C File Offset: 0x00026E4C
		internal void FixupPseudoToken(ref int token)
		{
			if (ModuleBuilder.IsPseudoToken(token))
			{
				token = this.ResolvePseudoToken(token);
			}
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00028C61 File Offset: 0x00026E61
		internal void SetIsSaved()
		{
			if (this.saved)
			{
				throw new InvalidOperationException();
			}
			this.saved = true;
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x00028C78 File Offset: 0x00026E78
		internal bool IsSaved
		{
			get
			{
				return this.saved;
			}
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00028C80 File Offset: 0x00026E80
		internal override string GetString(int index)
		{
			return this.Strings.Find(index);
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0000AF70 File Offset: 0x00009170
		// Note: this type is marked as 'beforefieldinit'.
		static ModuleBuilder()
		{
		}

		// Token: 0x040004C2 RID: 1218
		private static readonly bool usePublicKeyAssemblyReference;

		// Token: 0x040004C3 RID: 1219
		private Guid mvid;

		// Token: 0x040004C4 RID: 1220
		private uint timestamp;

		// Token: 0x040004C5 RID: 1221
		private long imageBaseAddress = 4194304L;

		// Token: 0x040004C6 RID: 1222
		private long stackReserve = -1L;

		// Token: 0x040004C7 RID: 1223
		private int fileAlignment = 512;

		// Token: 0x040004C8 RID: 1224
		private DllCharacteristics dllCharacteristics = DllCharacteristics.DynamicBase | DllCharacteristics.NoSEH | DllCharacteristics.NXCompat | DllCharacteristics.TerminalServerAware;

		// Token: 0x040004C9 RID: 1225
		private readonly AssemblyBuilder asm;

		// Token: 0x040004CA RID: 1226
		internal readonly string moduleName;

		// Token: 0x040004CB RID: 1227
		internal readonly string fileName;

		// Token: 0x040004CC RID: 1228
		internal readonly ISymbolWriterImpl symbolWriter;

		// Token: 0x040004CD RID: 1229
		private readonly TypeBuilder moduleType;

		// Token: 0x040004CE RID: 1230
		private readonly List<TypeBuilder> types = new List<TypeBuilder>();

		// Token: 0x040004CF RID: 1231
		private readonly Dictionary<Type, int> typeTokens = new Dictionary<Type, int>();

		// Token: 0x040004D0 RID: 1232
		private readonly Dictionary<Type, int> memberRefTypeTokens = new Dictionary<Type, int>();

		// Token: 0x040004D1 RID: 1233
		internal readonly ByteBuffer methodBodies = new ByteBuffer(131072);

		// Token: 0x040004D2 RID: 1234
		internal readonly List<int> tokenFixupOffsets = new List<int>();

		// Token: 0x040004D3 RID: 1235
		internal readonly ByteBuffer initializedData = new ByteBuffer(512);

		// Token: 0x040004D4 RID: 1236
		internal ResourceSection unmanagedResources;

		// Token: 0x040004D5 RID: 1237
		private readonly Dictionary<ModuleBuilder.MemberRefKey, int> importedMemberRefs = new Dictionary<ModuleBuilder.MemberRefKey, int>();

		// Token: 0x040004D6 RID: 1238
		private readonly Dictionary<ModuleBuilder.MethodSpecKey, int> importedMethodSpecs = new Dictionary<ModuleBuilder.MethodSpecKey, int>();

		// Token: 0x040004D7 RID: 1239
		private readonly Dictionary<Assembly, int> referencedAssemblies = new Dictionary<Assembly, int>();

		// Token: 0x040004D8 RID: 1240
		private List<AssemblyName> referencedAssemblyNames;

		// Token: 0x040004D9 RID: 1241
		private int nextPseudoToken = -1;

		// Token: 0x040004DA RID: 1242
		private readonly List<int> resolvedTokens = new List<int>();

		// Token: 0x040004DB RID: 1243
		internal readonly TableHeap Tables = new TableHeap();

		// Token: 0x040004DC RID: 1244
		internal readonly StringHeap Strings = new StringHeap();

		// Token: 0x040004DD RID: 1245
		internal readonly UserStringHeap UserStrings = new UserStringHeap();

		// Token: 0x040004DE RID: 1246
		internal readonly GuidHeap Guids = new GuidHeap();

		// Token: 0x040004DF RID: 1247
		internal readonly BlobHeap Blobs = new BlobHeap();

		// Token: 0x040004E0 RID: 1248
		internal readonly List<ModuleBuilder.VTableFixups> vtablefixups = new List<ModuleBuilder.VTableFixups>();

		// Token: 0x040004E1 RID: 1249
		internal readonly List<UnmanagedExport> unmanagedExports = new List<UnmanagedExport>();

		// Token: 0x040004E2 RID: 1250
		private List<ModuleBuilder.InterfaceImplCustomAttribute> interfaceImplCustomAttributes;

		// Token: 0x040004E3 RID: 1251
		private readonly List<ModuleBuilder.ResourceWriterRecord> resourceWriters = new List<ModuleBuilder.ResourceWriterRecord>();

		// Token: 0x040004E4 RID: 1252
		private bool saved;

		// Token: 0x0200036D RID: 877
		private struct ResourceWriterRecord
		{
			// Token: 0x0600265E RID: 9822 RVA: 0x000B621C File Offset: 0x000B441C
			internal ResourceWriterRecord(string name, Stream stream, ResourceAttributes attributes)
			{
				this = new ModuleBuilder.ResourceWriterRecord(name, null, stream, attributes);
			}

			// Token: 0x0600265F RID: 9823 RVA: 0x000B6228 File Offset: 0x000B4428
			internal ResourceWriterRecord(string name, ResourceWriter rw, Stream stream, ResourceAttributes attributes)
			{
				this.name = name;
				this.rw = rw;
				this.stream = stream;
				this.attributes = attributes;
			}

			// Token: 0x06002660 RID: 9824 RVA: 0x000B6248 File Offset: 0x000B4448
			internal void Emit(ModuleBuilder mb, int offset)
			{
				if (this.rw != null)
				{
					this.rw.Generate();
				}
				ManifestResourceTable.Record newRecord = default(ManifestResourceTable.Record);
				newRecord.Offset = offset;
				newRecord.Flags = (int)this.attributes;
				newRecord.Name = mb.Strings.Add(this.name);
				newRecord.Implementation = 0;
				mb.ManifestResource.AddRecord(newRecord);
			}

			// Token: 0x06002661 RID: 9825 RVA: 0x000B62B2 File Offset: 0x000B44B2
			internal int GetLength()
			{
				return 4 + (int)this.stream.Length;
			}

			// Token: 0x06002662 RID: 9826 RVA: 0x000B62C4 File Offset: 0x000B44C4
			internal void Write(MetadataWriter mw)
			{
				mw.Write((int)this.stream.Length);
				this.stream.Position = 0L;
				byte[] array = new byte[8192];
				int count;
				while ((count = this.stream.Read(array, 0, array.Length)) != 0)
				{
					mw.Write(array, 0, count);
				}
			}

			// Token: 0x06002663 RID: 9827 RVA: 0x000B631A File Offset: 0x000B451A
			internal void Close()
			{
				if (this.rw != null)
				{
					this.rw.Close();
				}
			}

			// Token: 0x04000F21 RID: 3873
			private readonly string name;

			// Token: 0x04000F22 RID: 3874
			private readonly ResourceWriter rw;

			// Token: 0x04000F23 RID: 3875
			private readonly Stream stream;

			// Token: 0x04000F24 RID: 3876
			private readonly ResourceAttributes attributes;
		}

		// Token: 0x0200036E RID: 878
		internal struct VTableFixups
		{
			// Token: 0x170008CF RID: 2255
			// (get) Token: 0x06002664 RID: 9828 RVA: 0x000B632F File Offset: 0x000B452F
			internal int SlotWidth
			{
				get
				{
					if ((this.type & 2) != 0)
					{
						return 8;
					}
					return 4;
				}
			}

			// Token: 0x04000F25 RID: 3877
			internal uint initializedDataOffset;

			// Token: 0x04000F26 RID: 3878
			internal ushort count;

			// Token: 0x04000F27 RID: 3879
			internal ushort type;
		}

		// Token: 0x0200036F RID: 879
		private struct InterfaceImplCustomAttribute
		{
			// Token: 0x04000F28 RID: 3880
			internal int type;

			// Token: 0x04000F29 RID: 3881
			internal int interfaceType;

			// Token: 0x04000F2A RID: 3882
			internal int pseudoToken;
		}

		// Token: 0x02000370 RID: 880
		private struct MemberRefKey : IEquatable<ModuleBuilder.MemberRefKey>
		{
			// Token: 0x06002665 RID: 9829 RVA: 0x000B633E File Offset: 0x000B453E
			internal MemberRefKey(Type type, string name, Signature signature)
			{
				this.type = type;
				this.name = name;
				this.signature = signature;
			}

			// Token: 0x06002666 RID: 9830 RVA: 0x000B6355 File Offset: 0x000B4555
			public bool Equals(ModuleBuilder.MemberRefKey other)
			{
				return other.type.Equals(this.type) && other.name == this.name && other.signature.Equals(this.signature);
			}

			// Token: 0x06002667 RID: 9831 RVA: 0x000B6390 File Offset: 0x000B4590
			public override bool Equals(object obj)
			{
				ModuleBuilder.MemberRefKey? memberRefKey = obj as ModuleBuilder.MemberRefKey?;
				return memberRefKey != null && this.Equals(memberRefKey.Value);
			}

			// Token: 0x06002668 RID: 9832 RVA: 0x000B63C1 File Offset: 0x000B45C1
			public override int GetHashCode()
			{
				return this.type.GetHashCode() + this.name.GetHashCode() + this.signature.GetHashCode();
			}

			// Token: 0x06002669 RID: 9833 RVA: 0x000B63E6 File Offset: 0x000B45E6
			internal MethodBase LookupMethod()
			{
				return this.type.FindMethod(this.name, (MethodSignature)this.signature);
			}

			// Token: 0x04000F2B RID: 3883
			private readonly Type type;

			// Token: 0x04000F2C RID: 3884
			private readonly string name;

			// Token: 0x04000F2D RID: 3885
			private readonly Signature signature;
		}

		// Token: 0x02000371 RID: 881
		private struct MethodSpecKey : IEquatable<ModuleBuilder.MethodSpecKey>
		{
			// Token: 0x0600266A RID: 9834 RVA: 0x000B6404 File Offset: 0x000B4604
			internal MethodSpecKey(Type type, string name, MethodSignature signature, Type[] genericParameters)
			{
				this.type = type;
				this.name = name;
				this.signature = signature;
				this.genericParameters = genericParameters;
			}

			// Token: 0x0600266B RID: 9835 RVA: 0x000B6424 File Offset: 0x000B4624
			public bool Equals(ModuleBuilder.MethodSpecKey other)
			{
				return other.type.Equals(this.type) && other.name == this.name && other.signature.Equals(this.signature) && Util.ArrayEquals(other.genericParameters, this.genericParameters);
			}

			// Token: 0x0600266C RID: 9836 RVA: 0x000B6480 File Offset: 0x000B4680
			public override bool Equals(object obj)
			{
				ModuleBuilder.MethodSpecKey? methodSpecKey = obj as ModuleBuilder.MethodSpecKey?;
				return methodSpecKey != null && this.Equals(methodSpecKey.Value);
			}

			// Token: 0x0600266D RID: 9837 RVA: 0x000B64B1 File Offset: 0x000B46B1
			public override int GetHashCode()
			{
				return this.type.GetHashCode() + this.name.GetHashCode() + this.signature.GetHashCode() + Util.GetHashCode(this.genericParameters);
			}

			// Token: 0x04000F2E RID: 3886
			private readonly Type type;

			// Token: 0x04000F2F RID: 3887
			private readonly string name;

			// Token: 0x04000F30 RID: 3888
			private readonly MethodSignature signature;

			// Token: 0x04000F31 RID: 3889
			private readonly Type[] genericParameters;
		}
	}
}
