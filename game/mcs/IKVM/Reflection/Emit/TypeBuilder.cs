using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using IKVM.Reflection.Impl;
using IKVM.Reflection.Metadata;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000F9 RID: 249
	public sealed class TypeBuilder : TypeInfo, ITypeOwner
	{
		// Token: 0x06000C26 RID: 3110 RVA: 0x0002B740 File Offset: 0x00029940
		internal TypeBuilder(ITypeOwner owner, string ns, string name)
		{
			this.owner = owner;
			this.token = this.ModuleBuilder.TypeDef.AllocToken();
			this.ns = ns;
			this.name = name;
			this.typeNameSpace = ((ns == null) ? 0 : this.ModuleBuilder.Strings.Add(ns));
			this.typeName = this.ModuleBuilder.Strings.Add(name);
			base.MarkKnownType(ns, name);
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0002B7D0 File Offset: 0x000299D0
		public ConstructorBuilder DefineDefaultConstructor(MethodAttributes attributes)
		{
			ConstructorBuilder constructorBuilder = this.DefineConstructor(attributes, CallingConventions.Standard, Type.EmptyTypes);
			ILGenerator ilgenerator = constructorBuilder.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Call, this.BaseType.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null));
			ilgenerator.Emit(OpCodes.Ret);
			return constructorBuilder;
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x0002B824 File Offset: 0x00029A24
		public ConstructorBuilder DefineConstructor(MethodAttributes attribs, CallingConventions callConv, Type[] parameterTypes)
		{
			return this.DefineConstructor(attribs, callConv, parameterTypes, null, null);
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0002B834 File Offset: 0x00029A34
		public ConstructorBuilder DefineConstructor(MethodAttributes attribs, CallingConventions callingConvention, Type[] parameterTypes, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers)
		{
			attribs |= (MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
			string text = ((attribs & MethodAttributes.Static) == MethodAttributes.PrivateScope) ? ConstructorInfo.ConstructorName : ConstructorInfo.TypeConstructorName;
			return new ConstructorBuilder(this.DefineMethod(text, attribs, callingConvention, null, null, null, parameterTypes, requiredCustomModifiers, optionalCustomModifiers));
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0002B873 File Offset: 0x00029A73
		public ConstructorBuilder DefineTypeInitializer()
		{
			return new ConstructorBuilder(this.DefineMethod(ConstructorInfo.TypeConstructorName, MethodAttributes.Private | MethodAttributes.Static | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName, null, Type.EmptyTypes));
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0002B890 File Offset: 0x00029A90
		private MethodBuilder CreateMethodBuilder(string name, MethodAttributes attributes, CallingConventions callingConvention)
		{
			this.ModuleBuilder.MethodDef.AddVirtualRecord();
			MethodBuilder methodBuilder = new MethodBuilder(this, name, attributes, callingConvention);
			this.methods.Add(methodBuilder);
			return methodBuilder;
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0002B8C5 File Offset: 0x00029AC5
		public MethodBuilder DefineMethod(string name, MethodAttributes attribs)
		{
			return this.DefineMethod(name, attribs, CallingConventions.Standard);
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0002B8D0 File Offset: 0x00029AD0
		public MethodBuilder DefineMethod(string name, MethodAttributes attribs, CallingConventions callingConvention)
		{
			return this.CreateMethodBuilder(name, attribs, callingConvention);
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x0002B8DC File Offset: 0x00029ADC
		public MethodBuilder DefineMethod(string name, MethodAttributes attribs, Type returnType, Type[] parameterTypes)
		{
			return this.DefineMethod(name, attribs, CallingConventions.Standard, returnType, null, null, parameterTypes, null, null);
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0002B8FC File Offset: 0x00029AFC
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			return this.DefineMethod(name, attributes, callingConvention, returnType, null, null, parameterTypes, null, null);
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0002B91A File Offset: 0x00029B1A
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			MethodBuilder methodBuilder = this.CreateMethodBuilder(name, attributes, callingConvention);
			methodBuilder.SetSignature(returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
			return methodBuilder;
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x0002B938 File Offset: 0x00029B38
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			return this.DefinePInvokeMethod(name, dllName, null, attributes, callingConvention, returnType, null, null, parameterTypes, null, null, nativeCallConv, nativeCharSet);
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0002B960 File Offset: 0x00029B60
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, string entryName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			return this.DefinePInvokeMethod(name, dllName, entryName, attributes, callingConvention, returnType, null, null, parameterTypes, null, null, nativeCallConv, nativeCharSet);
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0002B988 File Offset: 0x00029B88
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, string entryName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			MethodBuilder methodBuilder = this.DefineMethod(name, attributes | MethodAttributes.PinvokeImpl, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
			methodBuilder.SetDllImportPseudoCustomAttribute(dllName, entryName, new CallingConvention?(nativeCallConv), new CharSet?(nativeCharSet), null, null, null, null, null);
			return methodBuilder;
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0002B9F8 File Offset: 0x00029BF8
		public void DefineMethodOverride(MethodInfo methodInfoBody, MethodInfo methodInfoDeclaration)
		{
			MethodImplTable.Record newRecord = default(MethodImplTable.Record);
			newRecord.Class = this.token;
			newRecord.MethodBody = this.ModuleBuilder.GetMethodToken(methodInfoBody).Token;
			newRecord.MethodDeclaration = this.ModuleBuilder.GetMethodTokenWinRT(methodInfoDeclaration);
			this.ModuleBuilder.MethodImpl.AddRecord(newRecord);
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x0002BA5A File Offset: 0x00029C5A
		public FieldBuilder DefineField(string name, Type fieldType, FieldAttributes attribs)
		{
			return this.DefineField(name, fieldType, null, null, attribs);
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0002BA67 File Offset: 0x00029C67
		public FieldBuilder DefineField(string fieldName, Type type, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers, FieldAttributes attributes)
		{
			return this.__DefineField(fieldName, type, CustomModifiers.FromReqOpt(requiredCustomModifiers, optionalCustomModifiers), attributes);
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0002BA7C File Offset: 0x00029C7C
		public FieldBuilder __DefineField(string fieldName, Type type, CustomModifiers customModifiers, FieldAttributes attributes)
		{
			FieldBuilder fieldBuilder = new FieldBuilder(this, fieldName, type, customModifiers, attributes);
			this.fields.Add(fieldBuilder);
			return fieldBuilder;
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0002BAA4 File Offset: 0x00029CA4
		public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, Type returnType, Type[] parameterTypes)
		{
			return this.DefineProperty(name, attributes, returnType, null, null, parameterTypes, null, null);
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0002BAC0 File Offset: 0x00029CC0
		public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			return this.DefineProperty(name, attributes, callingConvention, returnType, null, null, parameterTypes, null, null);
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0002BAE0 File Offset: 0x00029CE0
		public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			return this.DefinePropertyImpl(name, attributes, CallingConventions.Standard, true, returnType, parameterTypes, PackedCustomModifiers.CreateFromExternal(returnTypeOptionalCustomModifiers, returnTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers, parameterTypeRequiredCustomModifiers, Util.NullSafeLength<Type>(parameterTypes)));
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0002BB10 File Offset: 0x00029D10
		public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			return this.DefinePropertyImpl(name, attributes, callingConvention, false, returnType, parameterTypes, PackedCustomModifiers.CreateFromExternal(returnTypeOptionalCustomModifiers, returnTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers, parameterTypeRequiredCustomModifiers, Util.NullSafeLength<Type>(parameterTypes)));
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0002BB40 File Offset: 0x00029D40
		public PropertyBuilder __DefineProperty(string name, PropertyAttributes attributes, CallingConventions callingConvention, Type returnType, CustomModifiers returnTypeCustomModifiers, Type[] parameterTypes, CustomModifiers[] parameterTypeCustomModifiers)
		{
			return this.DefinePropertyImpl(name, attributes, callingConvention, false, returnType, parameterTypes, PackedCustomModifiers.CreateFromExternal(returnTypeCustomModifiers, parameterTypeCustomModifiers, Util.NullSafeLength<Type>(parameterTypes)));
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0002BB6C File Offset: 0x00029D6C
		private PropertyBuilder DefinePropertyImpl(string name, PropertyAttributes attributes, CallingConventions callingConvention, bool patchCallingConvention, Type returnType, Type[] parameterTypes, PackedCustomModifiers customModifiers)
		{
			if (this.properties == null)
			{
				this.properties = new List<PropertyBuilder>();
			}
			PropertySignature sig = PropertySignature.Create(callingConvention, returnType, parameterTypes, customModifiers);
			PropertyBuilder propertyBuilder = new PropertyBuilder(this, name, attributes, sig, patchCallingConvention);
			this.properties.Add(propertyBuilder);
			return propertyBuilder;
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0002BBB4 File Offset: 0x00029DB4
		public EventBuilder DefineEvent(string name, EventAttributes attributes, Type eventtype)
		{
			if (this.events == null)
			{
				this.events = new List<EventBuilder>();
			}
			EventBuilder eventBuilder = new EventBuilder(this, name, attributes, eventtype);
			this.events.Add(eventBuilder);
			return eventBuilder;
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0002BBEB File Offset: 0x00029DEB
		public TypeBuilder DefineNestedType(string name)
		{
			return this.DefineNestedType(name, TypeAttributes.NestedPrivate);
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x0002BBF5 File Offset: 0x00029DF5
		public TypeBuilder DefineNestedType(string name, TypeAttributes attribs)
		{
			return this.DefineNestedType(name, attribs, null);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0002BC00 File Offset: 0x00029E00
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, Type[] interfaces)
		{
			TypeBuilder typeBuilder = this.DefineNestedType(name, attr, parent);
			if (interfaces != null)
			{
				foreach (Type interfaceType in interfaces)
				{
					typeBuilder.AddInterfaceImplementation(interfaceType);
				}
			}
			return typeBuilder;
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0002BC38 File Offset: 0x00029E38
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent)
		{
			return this.DefineNestedType(name, attr, parent, 0);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0002BC44 File Offset: 0x00029E44
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, int typeSize)
		{
			return this.DefineNestedType(name, attr, parent, PackingSize.Unspecified, typeSize);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0002BC52 File Offset: 0x00029E52
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, PackingSize packSize)
		{
			return this.DefineNestedType(name, attr, parent, packSize, 0);
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0002BC60 File Offset: 0x00029E60
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, PackingSize packSize, int typeSize)
		{
			string text = null;
			int num = name.LastIndexOf('.');
			if (num > 0)
			{
				text = name.Substring(0, num);
				name = name.Substring(num + 1);
			}
			TypeBuilder typeBuilder = this.__DefineNestedType(text, name);
			typeBuilder.__SetAttributes(attr);
			typeBuilder.SetParent(parent);
			if (packSize != PackingSize.Unspecified || typeSize != 0)
			{
				typeBuilder.__SetLayout((int)packSize, typeSize);
			}
			return typeBuilder;
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x0002BCBC File Offset: 0x00029EBC
		public TypeBuilder __DefineNestedType(string ns, string name)
		{
			this.typeFlags |= Type.TypeFlags.HasNestedTypes;
			TypeBuilder typeBuilder = this.ModuleBuilder.DefineType(this, ns, name);
			NestedClassTable.Record newRecord = default(NestedClassTable.Record);
			newRecord.NestedClass = typeBuilder.MetadataToken;
			newRecord.EnclosingClass = this.MetadataToken;
			this.ModuleBuilder.NestedClass.AddRecord(newRecord);
			return typeBuilder;
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0002BD1B File Offset: 0x00029F1B
		public void SetParent(Type parent)
		{
			this.lazyBaseType = parent;
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0002BD24 File Offset: 0x00029F24
		public void AddInterfaceImplementation(Type interfaceType)
		{
			if (this.interfaces == null)
			{
				this.interfaces = new List<Type>();
			}
			this.interfaces.Add(interfaceType);
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0002BD45 File Offset: 0x00029F45
		public void __SetInterfaceImplementationCustomAttribute(Type interfaceType, CustomAttributeBuilder cab)
		{
			this.ModuleBuilder.SetInterfaceImplementationCustomAttribute(this, interfaceType, cab);
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x0002BD55 File Offset: 0x00029F55
		public int Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x0002BD5D File Offset: 0x00029F5D
		public PackingSize PackingSize
		{
			get
			{
				return (PackingSize)this.pack;
			}
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0002BD65 File Offset: 0x00029F65
		public override bool __GetLayout(out int packingSize, out int typeSize)
		{
			packingSize = (int)this.pack;
			typeSize = this.size;
			return this.hasLayout;
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0002BD7D File Offset: 0x00029F7D
		public void __SetLayout(int packingSize, int typesize)
		{
			this.pack = (short)packingSize;
			this.size = typesize;
			this.hasLayout = true;
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0002BD98 File Offset: 0x00029F98
		private void SetStructLayoutPseudoCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			object constructorArgument = customBuilder.GetConstructorArgument(0);
			LayoutKind layoutKind;
			if (constructorArgument is short)
			{
				layoutKind = (LayoutKind)((short)constructorArgument);
			}
			else
			{
				layoutKind = (LayoutKind)constructorArgument;
			}
			this.pack = (short)(((int?)customBuilder.GetFieldValue("Pack")) ?? 0);
			this.size = (((int?)customBuilder.GetFieldValue("Size")) ?? 0);
			CharSet charSet = customBuilder.GetFieldValue<CharSet>("CharSet") ?? CharSet.None;
			this.attribs &= ~(TypeAttributes.SequentialLayout | TypeAttributes.ExplicitLayout);
			switch (layoutKind)
			{
			case LayoutKind.Sequential:
				this.attribs |= TypeAttributes.SequentialLayout;
				break;
			case LayoutKind.Explicit:
				this.attribs |= TypeAttributes.ExplicitLayout;
				break;
			case LayoutKind.Auto:
				this.attribs |= TypeAttributes.AnsiClass;
				break;
			}
			this.attribs &= ~(TypeAttributes.UnicodeClass | TypeAttributes.AutoClass);
			switch (charSet)
			{
			case CharSet.None:
			case CharSet.Ansi:
				this.attribs |= TypeAttributes.AnsiClass;
				break;
			case CharSet.Unicode:
				this.attribs |= TypeAttributes.UnicodeClass;
				break;
			case CharSet.Auto:
				this.attribs |= TypeAttributes.AutoClass;
				break;
			}
			this.hasLayout = (this.pack != 0 || this.size != 0);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0002BF0D File Offset: 0x0002A10D
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x0002BF1C File Offset: 0x0002A11C
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			KnownCA knownCA = customBuilder.KnownCA;
			if (knownCA == KnownCA.ComImportAttribute)
			{
				this.attribs |= TypeAttributes.Import;
				return;
			}
			if (knownCA != KnownCA.SerializableAttribute)
			{
				switch (knownCA)
				{
				case KnownCA.StructLayoutAttribute:
					this.SetStructLayoutPseudoCustomAttribute(customBuilder.DecodeBlob(base.Assembly));
					return;
				case KnownCA.SpecialNameAttribute:
					this.attribs |= TypeAttributes.SpecialName;
					return;
				case KnownCA.SuppressUnmanagedCodeSecurityAttribute:
					this.attribs |= TypeAttributes.HasSecurity;
					break;
				}
				this.ModuleBuilder.SetCustomAttribute(this.token, customBuilder);
				return;
			}
			this.attribs |= TypeAttributes.Serializable;
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0002BFC3 File Offset: 0x0002A1C3
		public void __AddDeclarativeSecurity(CustomAttributeBuilder customBuilder)
		{
			this.attribs |= TypeAttributes.HasSecurity;
			if (this.declarativeSecurity == null)
			{
				this.declarativeSecurity = new List<CustomAttributeBuilder>();
			}
			this.declarativeSecurity.Add(customBuilder);
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0002BFF6 File Offset: 0x0002A1F6
		public void AddDeclarativeSecurity(SecurityAction securityAction, PermissionSet permissionSet)
		{
			this.ModuleBuilder.AddDeclarativeSecurity(this.token, securityAction, permissionSet);
			this.attribs |= TypeAttributes.HasSecurity;
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0002C020 File Offset: 0x0002A220
		public GenericTypeParameterBuilder[] DefineGenericParameters(params string[] names)
		{
			this.typeFlags |= Type.TypeFlags.IsGenericTypeDefinition;
			this.gtpb = new GenericTypeParameterBuilder[names.Length];
			for (int i = 0; i < names.Length; i++)
			{
				this.gtpb[i] = new GenericTypeParameterBuilder(names[i], this, i);
			}
			return (GenericTypeParameterBuilder[])this.gtpb.Clone();
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0002C079 File Offset: 0x0002A279
		public override Type[] GetGenericArguments()
		{
			return Util.Copy(this.gtpb);
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0002C086 File Offset: 0x0002A286
		public override CustomModifiers[] __GetGenericArgumentsCustomModifiers()
		{
			if (this.gtpb != null)
			{
				return new CustomModifiers[this.gtpb.Length];
			}
			return Empty<CustomModifiers>.Array;
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0002C0A3 File Offset: 0x0002A2A3
		internal override Type GetGenericTypeArgument(int index)
		{
			return this.gtpb[index];
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x0002C0AD File Offset: 0x0002A2AD
		public override bool ContainsGenericParameters
		{
			get
			{
				return this.gtpb != null;
			}
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x00005936 File Offset: 0x00003B36
		public override Type GetGenericTypeDefinition()
		{
			return this;
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0002C0B8 File Offset: 0x0002A2B8
		public TypeInfo CreateTypeInfo()
		{
			if ((this.typeFlags & Type.TypeFlags.Baked) != Type.TypeFlags.ContainsMissingType_Unknown)
			{
				throw new NotImplementedException();
			}
			this.typeFlags |= Type.TypeFlags.Baked;
			if (this.hasLayout)
			{
				ClassLayoutTable.Record newRecord = default(ClassLayoutTable.Record);
				newRecord.PackingSize = this.pack;
				newRecord.ClassSize = this.size;
				newRecord.Parent = this.token;
				this.ModuleBuilder.ClassLayout.AddRecord(newRecord);
			}
			bool flag = false;
			foreach (MethodBuilder methodBuilder in this.methods)
			{
				flag |= (methodBuilder.IsSpecialName && methodBuilder.Name == ConstructorInfo.ConstructorName);
				methodBuilder.Bake();
			}
			if (!flag && !this.IsModulePseudoType && !base.IsInterface && !this.IsValueType && (!base.IsAbstract || !base.IsSealed) && this.Universe.AutomaticallyProvideDefaultConstructor)
			{
				((MethodBuilder)this.DefineDefaultConstructor(MethodAttributes.Public).GetMethodInfo()).Bake();
			}
			if (this.declarativeSecurity != null)
			{
				this.ModuleBuilder.AddDeclarativeSecurity(this.token, this.declarativeSecurity);
			}
			if (!this.IsModulePseudoType)
			{
				Type baseType = this.BaseType;
				if (baseType != null)
				{
					this.extends = this.ModuleBuilder.GetTypeToken(baseType).Token;
				}
			}
			if (this.interfaces != null)
			{
				foreach (Type type in this.interfaces)
				{
					InterfaceImplTable.Record newRecord2 = default(InterfaceImplTable.Record);
					newRecord2.Class = this.token;
					newRecord2.Interface = this.ModuleBuilder.GetTypeToken(type).Token;
					this.ModuleBuilder.InterfaceImpl.AddRecord(newRecord2);
				}
			}
			return new BakedType(this);
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0002C2C4 File Offset: 0x0002A4C4
		public Type CreateType()
		{
			return this.CreateTypeInfo();
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0002C2CC File Offset: 0x0002A4CC
		internal void PopulatePropertyAndEventTables()
		{
			if (this.properties != null)
			{
				PropertyMapTable.Record newRecord = default(PropertyMapTable.Record);
				newRecord.Parent = this.token;
				newRecord.PropertyList = this.ModuleBuilder.Property.RowCount + 1;
				this.ModuleBuilder.PropertyMap.AddRecord(newRecord);
				foreach (PropertyBuilder propertyBuilder in this.properties)
				{
					propertyBuilder.Bake();
				}
			}
			if (this.events != null)
			{
				EventMapTable.Record newRecord2 = default(EventMapTable.Record);
				newRecord2.Parent = this.token;
				newRecord2.EventList = this.ModuleBuilder.Event.RowCount + 1;
				this.ModuleBuilder.EventMap.AddRecord(newRecord2);
				foreach (EventBuilder eventBuilder in this.events)
				{
					eventBuilder.Bake();
				}
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x0002C3EC File Offset: 0x0002A5EC
		public override Type BaseType
		{
			get
			{
				if (this.lazyBaseType == null && !base.IsInterface)
				{
					Type system_Object = this.Module.universe.System_Object;
					if (this != system_Object)
					{
						this.lazyBaseType = system_Object;
					}
				}
				return this.lazyBaseType;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x0002C438 File Offset: 0x0002A638
		public override string FullName
		{
			get
			{
				if (base.IsNested)
				{
					return this.DeclaringType.FullName + "+" + TypeNameParser.Escape(this.name);
				}
				if (this.ns == null)
				{
					return TypeNameParser.Escape(this.name);
				}
				return TypeNameParser.Escape(this.ns) + "." + TypeNameParser.Escape(this.name);
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x0002C4A2 File Offset: 0x0002A6A2
		internal override TypeName TypeName
		{
			get
			{
				return new TypeName(this.ns, this.name);
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x0002C4B5 File Offset: 0x0002A6B5
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x0002C4BD File Offset: 0x0002A6BD
		public override string Namespace
		{
			get
			{
				return this.ns ?? "";
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x0002C4CE File Offset: 0x0002A6CE
		public override TypeAttributes Attributes
		{
			get
			{
				return this.attribs;
			}
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0002C4D6 File Offset: 0x0002A6D6
		public void __SetAttributes(TypeAttributes attributes)
		{
			this.attribs = attributes;
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0002C4DF File Offset: 0x0002A6DF
		public override Type[] __GetDeclaredInterfaces()
		{
			return Util.ToArray<Type, Type>(this.interfaces, Type.EmptyTypes);
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0002C4F4 File Offset: 0x0002A6F4
		public override MethodBase[] __GetDeclaredMethods()
		{
			MethodBase[] array = new MethodBase[this.methods.Count];
			for (int i = 0; i < array.Length; i++)
			{
				MethodBuilder methodBuilder = this.methods[i];
				if (methodBuilder.IsConstructor)
				{
					array[i] = new ConstructorInfoImpl(methodBuilder);
				}
				else
				{
					array[i] = methodBuilder;
				}
			}
			return array;
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x0002C545 File Offset: 0x0002A745
		public override Type DeclaringType
		{
			get
			{
				return this.owner as TypeBuilder;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x0001DF8B File Offset: 0x0001C18B
		public override bool IsGenericType
		{
			get
			{
				return this.IsGenericTypeDefinition;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x0002C552 File Offset: 0x0002A752
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return (this.typeFlags & Type.TypeFlags.IsGenericTypeDefinition) > Type.TypeFlags.ContainsMissingType_Unknown;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x0002C55F File Offset: 0x0002A75F
		public override int MetadataToken
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0002C567 File Offset: 0x0002A767
		public FieldBuilder DefineUninitializedData(string name, int size, FieldAttributes attributes)
		{
			return this.DefineInitializedData(name, new byte[size], attributes);
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0002C578 File Offset: 0x0002A778
		public FieldBuilder DefineInitializedData(string name, byte[] data, FieldAttributes attributes)
		{
			Type type = this.ModuleBuilder.GetType("$ArrayType$" + data.Length);
			if (type == null)
			{
				TypeBuilder typeBuilder = this.ModuleBuilder.DefineType("$ArrayType$" + data.Length, TypeAttributes.Public | TypeAttributes.ExplicitLayout | TypeAttributes.Sealed, this.Module.universe.System_ValueType, PackingSize.Size1, data.Length);
				typeBuilder.CreateType();
				type = typeBuilder;
			}
			FieldBuilder fieldBuilder = this.DefineField(name, type, attributes | FieldAttributes.Static);
			fieldBuilder.__SetDataAndRVA(data);
			return fieldBuilder;
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0002C5FD File Offset: 0x0002A7FD
		public static MethodInfo GetMethod(Type type, MethodInfo method)
		{
			return new GenericMethodInstance(type, method, null);
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0002C607 File Offset: 0x0002A807
		public static ConstructorInfo GetConstructor(Type type, ConstructorInfo constructor)
		{
			return new ConstructorInfoImpl(TypeBuilder.GetMethod(type, constructor.GetMethodInfo()));
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0002C61A File Offset: 0x0002A81A
		public static FieldInfo GetField(Type type, FieldInfo field)
		{
			return new GenericFieldInstance(type, field);
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x0002C623 File Offset: 0x0002A823
		public override Module Module
		{
			get
			{
				return this.owner.ModuleBuilder;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x0002C630 File Offset: 0x0002A830
		public TypeToken TypeToken
		{
			get
			{
				return new TypeToken(this.token);
			}
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0002C640 File Offset: 0x0002A840
		internal void WriteTypeDefRecord(MetadataWriter mw, ref int fieldList, ref int methodList)
		{
			mw.Write((int)this.attribs);
			mw.WriteStringIndex(this.typeName);
			mw.WriteStringIndex(this.typeNameSpace);
			mw.WriteTypeDefOrRef(this.extends);
			mw.WriteField(fieldList);
			mw.WriteMethodDef(methodList);
			methodList += this.methods.Count;
			fieldList += this.fields.Count;
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0002C6B0 File Offset: 0x0002A8B0
		internal void WriteMethodDefRecords(int baseRVA, MetadataWriter mw, ref int paramList)
		{
			foreach (MethodBuilder methodBuilder in this.methods)
			{
				methodBuilder.WriteMethodDefRecord(baseRVA, mw, ref paramList);
			}
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0002C704 File Offset: 0x0002A904
		internal void ResolveMethodAndFieldTokens(ref int methodToken, ref int fieldToken, ref int parameterToken)
		{
			foreach (MethodBuilder methodBuilder in this.methods)
			{
				int num = methodToken;
				methodToken = num + 1;
				methodBuilder.FixupToken(num, ref parameterToken);
			}
			foreach (FieldBuilder fieldBuilder in this.fields)
			{
				int num = fieldToken;
				fieldToken = num + 1;
				fieldBuilder.FixupToken(num);
			}
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0002C7A8 File Offset: 0x0002A9A8
		internal void WriteParamRecords(MetadataWriter mw)
		{
			foreach (MethodBuilder methodBuilder in this.methods)
			{
				methodBuilder.WriteParamRecords(mw);
			}
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x0002C7FC File Offset: 0x0002A9FC
		internal void WriteFieldRecords(MetadataWriter mw)
		{
			foreach (FieldBuilder fieldBuilder in this.fields)
			{
				fieldBuilder.WriteFieldRecords(mw);
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x0002C623 File Offset: 0x0002A823
		internal ModuleBuilder ModuleBuilder
		{
			get
			{
				return this.owner.ModuleBuilder;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x0002C623 File Offset: 0x0002A823
		ModuleBuilder ITypeOwner.ModuleBuilder
		{
			get
			{
				return this.owner.ModuleBuilder;
			}
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x0002C55F File Offset: 0x0002A75F
		internal override int GetModuleBuilderToken()
		{
			return this.token;
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x0002C850 File Offset: 0x0002AA50
		internal bool HasNestedTypes
		{
			get
			{
				return (this.typeFlags & Type.TypeFlags.HasNestedTypes) > Type.TypeFlags.ContainsMissingType_Unknown;
			}
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x0002C860 File Offset: 0x0002AA60
		internal MethodBase LookupMethod(int token)
		{
			foreach (MethodBuilder methodBuilder in this.methods)
			{
				if (methodBuilder.MetadataToken == token)
				{
					return methodBuilder;
				}
			}
			return null;
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0002C8BC File Offset: 0x0002AABC
		public bool IsCreated()
		{
			return (this.typeFlags & Type.TypeFlags.Baked) > Type.TypeFlags.ContainsMissingType_Unknown;
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x0002C8C9 File Offset: 0x0002AAC9
		internal override void CheckBaked()
		{
			if ((this.typeFlags & Type.TypeFlags.Baked) == Type.TypeFlags.ContainsMissingType_Unknown)
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0002C8DC File Offset: 0x0002AADC
		public override Type[] __GetDeclaredTypes()
		{
			if (this.HasNestedTypes)
			{
				List<Type> list = new List<Type>();
				foreach (int metadataToken in this.ModuleBuilder.NestedClass.GetNestedClasses(this.token))
				{
					list.Add(this.ModuleBuilder.ResolveType(metadataToken));
				}
				return list.ToArray();
			}
			return Type.EmptyTypes;
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0002C964 File Offset: 0x0002AB64
		public override FieldInfo[] __GetDeclaredFields()
		{
			return Util.ToArray<FieldInfo, FieldBuilder>(this.fields, Empty<FieldInfo>.Array);
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0002C976 File Offset: 0x0002AB76
		public override EventInfo[] __GetDeclaredEvents()
		{
			return Util.ToArray<EventInfo, EventBuilder>(this.events, Empty<EventInfo>.Array);
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0002C988 File Offset: 0x0002AB88
		public override PropertyInfo[] __GetDeclaredProperties()
		{
			return Util.ToArray<PropertyInfo, PropertyBuilder>(this.properties, Empty<PropertyInfo>.Array);
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x0002C99A File Offset: 0x0002AB9A
		internal override bool IsModulePseudoType
		{
			get
			{
				return this.token == 33554433;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x0002C9A9 File Offset: 0x0002ABA9
		internal override bool IsBaked
		{
			get
			{
				return this.IsCreated();
			}
		}

		// Token: 0x04000601 RID: 1537
		public const int UnspecifiedTypeSize = 0;

		// Token: 0x04000602 RID: 1538
		private readonly ITypeOwner owner;

		// Token: 0x04000603 RID: 1539
		private readonly int token;

		// Token: 0x04000604 RID: 1540
		private int extends;

		// Token: 0x04000605 RID: 1541
		private Type lazyBaseType;

		// Token: 0x04000606 RID: 1542
		private readonly int typeName;

		// Token: 0x04000607 RID: 1543
		private readonly int typeNameSpace;

		// Token: 0x04000608 RID: 1544
		private readonly string ns;

		// Token: 0x04000609 RID: 1545
		private readonly string name;

		// Token: 0x0400060A RID: 1546
		private readonly List<MethodBuilder> methods = new List<MethodBuilder>();

		// Token: 0x0400060B RID: 1547
		private readonly List<FieldBuilder> fields = new List<FieldBuilder>();

		// Token: 0x0400060C RID: 1548
		private List<PropertyBuilder> properties;

		// Token: 0x0400060D RID: 1549
		private List<EventBuilder> events;

		// Token: 0x0400060E RID: 1550
		private TypeAttributes attribs;

		// Token: 0x0400060F RID: 1551
		private GenericTypeParameterBuilder[] gtpb;

		// Token: 0x04000610 RID: 1552
		private List<CustomAttributeBuilder> declarativeSecurity;

		// Token: 0x04000611 RID: 1553
		private List<Type> interfaces;

		// Token: 0x04000612 RID: 1554
		private int size;

		// Token: 0x04000613 RID: 1555
		private short pack;

		// Token: 0x04000614 RID: 1556
		private bool hasLayout;
	}
}
