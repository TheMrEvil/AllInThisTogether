using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace System.Xml.Serialization
{
	// Token: 0x020002EA RID: 746
	internal class XmlSerializationILGen
	{
		// Token: 0x06001D6A RID: 7530 RVA: 0x000AC318 File Offset: 0x000AA518
		internal XmlSerializationILGen(TypeScope[] scopes, string access, string className)
		{
			this.scopes = scopes;
			if (scopes.Length != 0)
			{
				this.stringTypeDesc = scopes[0].GetTypeDesc(typeof(string));
				this.qnameTypeDesc = scopes[0].GetTypeDesc(typeof(XmlQualifiedName));
			}
			this.raCodeGen = new ReflectionAwareILGen();
			this.className = className;
			this.typeAttributes = TypeAttributes.Public;
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001D6B RID: 7531 RVA: 0x000AC3B6 File Offset: 0x000AA5B6
		// (set) Token: 0x06001D6C RID: 7532 RVA: 0x000AC3BE File Offset: 0x000AA5BE
		internal int NextMethodNumber
		{
			get
			{
				return this.nextMethodNumber;
			}
			set
			{
				this.nextMethodNumber = value;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001D6D RID: 7533 RVA: 0x000AC3C7 File Offset: 0x000AA5C7
		internal ReflectionAwareILGen RaCodeGen
		{
			get
			{
				return this.raCodeGen;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001D6E RID: 7534 RVA: 0x000AC3CF File Offset: 0x000AA5CF
		internal TypeDesc StringTypeDesc
		{
			get
			{
				return this.stringTypeDesc;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001D6F RID: 7535 RVA: 0x000AC3D7 File Offset: 0x000AA5D7
		internal TypeDesc QnameTypeDesc
		{
			get
			{
				return this.qnameTypeDesc;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001D70 RID: 7536 RVA: 0x000AC3DF File Offset: 0x000AA5DF
		internal string ClassName
		{
			get
			{
				return this.className;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001D71 RID: 7537 RVA: 0x000AC3E7 File Offset: 0x000AA5E7
		internal TypeScope[] Scopes
		{
			get
			{
				return this.scopes;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001D72 RID: 7538 RVA: 0x000AC3EF File Offset: 0x000AA5EF
		internal Hashtable MethodNames
		{
			get
			{
				return this.methodNames;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001D73 RID: 7539 RVA: 0x000AC3F7 File Offset: 0x000AA5F7
		internal Hashtable GeneratedMethods
		{
			get
			{
				return this.generatedMethods;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001D74 RID: 7540 RVA: 0x000AC3FF File Offset: 0x000AA5FF
		// (set) Token: 0x06001D75 RID: 7541 RVA: 0x000AC407 File Offset: 0x000AA607
		internal ModuleBuilder ModuleBuilder
		{
			get
			{
				return this.moduleBuilder;
			}
			set
			{
				this.moduleBuilder = value;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001D76 RID: 7542 RVA: 0x000AC410 File Offset: 0x000AA610
		internal TypeAttributes TypeAttributes
		{
			get
			{
				return this.typeAttributes;
			}
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x000AC418 File Offset: 0x000AA618
		internal static Regex NewRegex(string pattern)
		{
			Dictionary<string, Regex> obj = XmlSerializationILGen.regexs;
			Regex regex;
			lock (obj)
			{
				if (!XmlSerializationILGen.regexs.TryGetValue(pattern, out regex))
				{
					regex = new Regex(pattern);
					XmlSerializationILGen.regexs.Add(pattern, regex);
				}
			}
			return regex;
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x000AC474 File Offset: 0x000AA674
		internal MethodBuilder EnsureMethodBuilder(TypeBuilder typeBuilder, string methodName, MethodAttributes attributes, Type returnType, Type[] parameterTypes)
		{
			MethodBuilderInfo methodBuilderInfo;
			if (!this.methodBuilders.TryGetValue(methodName, out methodBuilderInfo))
			{
				methodBuilderInfo = new MethodBuilderInfo(typeBuilder.DefineMethod(methodName, attributes, returnType, parameterTypes), parameterTypes);
				this.methodBuilders.Add(methodName, methodBuilderInfo);
			}
			return methodBuilderInfo.MethodBuilder;
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x000AC4B8 File Offset: 0x000AA6B8
		internal MethodBuilderInfo GetMethodBuilder(string methodName)
		{
			return this.methodBuilders[methodName];
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x0000B528 File Offset: 0x00009728
		internal virtual void GenerateMethod(TypeMapping mapping)
		{
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x000AC4C8 File Offset: 0x000AA6C8
		internal void GenerateReferencedMethods()
		{
			while (this.references > 0)
			{
				TypeMapping[] array = this.referencedMethods;
				int num = this.references - 1;
				this.references = num;
				TypeMapping mapping = array[num];
				this.GenerateMethod(mapping);
			}
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x000AC500 File Offset: 0x000AA700
		internal string ReferenceMapping(TypeMapping mapping)
		{
			if (this.generatedMethods[mapping] == null)
			{
				this.referencedMethods = this.EnsureArrayIndex(this.referencedMethods, this.references);
				TypeMapping[] array = this.referencedMethods;
				int num = this.references;
				this.references = num + 1;
				array[num] = mapping;
			}
			return (string)this.methodNames[mapping];
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x000AC560 File Offset: 0x000AA760
		private TypeMapping[] EnsureArrayIndex(TypeMapping[] a, int index)
		{
			if (a == null)
			{
				return new TypeMapping[32];
			}
			if (index < a.Length)
			{
				return a;
			}
			TypeMapping[] array = new TypeMapping[a.Length + 32];
			Array.Copy(a, array, index);
			return array;
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x000AC598 File Offset: 0x000AA798
		internal FieldBuilder GenerateHashtableGetBegin(string privateName, string publicName, TypeBuilder serializerContractTypeBuilder)
		{
			FieldBuilder fieldBuilder = serializerContractTypeBuilder.DefineField(privateName, typeof(Hashtable), FieldAttributes.Private);
			this.ilg = new CodeGenerator(serializerContractTypeBuilder);
			PropertyBuilder propertyBuilder = serializerContractTypeBuilder.DefineProperty(publicName, PropertyAttributes.None, CallingConventions.HasThis, typeof(Hashtable), null, null, null, null, null);
			this.ilg.BeginMethod(typeof(Hashtable), "get_" + publicName, CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, CodeGenerator.PublicOverrideMethodAttributes | MethodAttributes.SpecialName);
			propertyBuilder.SetGetMethod(this.ilg.MethodBuilder);
			this.ilg.Ldarg(0);
			this.ilg.LoadMember(fieldBuilder);
			this.ilg.Load(null);
			this.ilg.If(Cmp.EqualTo);
			ConstructorInfo constructor = typeof(Hashtable).GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			LocalBuilder local = this.ilg.DeclareLocal(typeof(Hashtable), "_tmp");
			this.ilg.New(constructor);
			this.ilg.Stloc(local);
			return fieldBuilder;
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x000AC6A4 File Offset: 0x000AA8A4
		internal void GenerateHashtableGetEnd(FieldBuilder fieldBuilder)
		{
			this.ilg.Ldarg(0);
			this.ilg.LoadMember(fieldBuilder);
			this.ilg.Load(null);
			this.ilg.If(Cmp.EqualTo);
			this.ilg.Ldarg(0);
			this.ilg.Ldloc(typeof(Hashtable), "_tmp");
			this.ilg.StoreMember(fieldBuilder);
			this.ilg.EndIf();
			this.ilg.EndIf();
			this.ilg.Ldarg(0);
			this.ilg.LoadMember(fieldBuilder);
			this.ilg.GotoMethodEnd();
			this.ilg.EndMethod();
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x000AC75C File Offset: 0x000AA95C
		internal FieldBuilder GeneratePublicMethods(string privateName, string publicName, string[] methods, XmlMapping[] xmlMappings, TypeBuilder serializerContractTypeBuilder)
		{
			FieldBuilder fieldBuilder = this.GenerateHashtableGetBegin(privateName, publicName, serializerContractTypeBuilder);
			if (methods != null && methods.Length != 0 && xmlMappings != null && xmlMappings.Length == methods.Length)
			{
				MethodInfo method = typeof(Hashtable).GetMethod("set_Item", CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(object),
					typeof(object)
				}, null);
				for (int i = 0; i < methods.Length; i++)
				{
					if (methods[i] != null)
					{
						this.ilg.Ldloc(typeof(Hashtable), "_tmp");
						this.ilg.Ldstr(xmlMappings[i].Key);
						this.ilg.Ldstr(methods[i]);
						this.ilg.Call(method);
					}
				}
			}
			this.GenerateHashtableGetEnd(fieldBuilder);
			return fieldBuilder;
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x000AC834 File Offset: 0x000AAA34
		internal void GenerateSupportedTypes(Type[] types, TypeBuilder serializerContractTypeBuilder)
		{
			this.ilg = new CodeGenerator(serializerContractTypeBuilder);
			this.ilg.BeginMethod(typeof(bool), "CanSerialize", new Type[]
			{
				typeof(Type)
			}, new string[]
			{
				"type"
			}, CodeGenerator.PublicOverrideMethodAttributes);
			Hashtable hashtable = new Hashtable();
			foreach (Type type in types)
			{
				if (!(type == null) && (type.IsPublic || type.IsNestedPublic) && hashtable[type] == null && !type.IsGenericType && !type.ContainsGenericParameters)
				{
					hashtable[type] = type;
					this.ilg.Ldarg("type");
					this.ilg.Ldc(type);
					this.ilg.If(Cmp.EqualTo);
					this.ilg.Ldc(true);
					this.ilg.GotoMethodEnd();
					this.ilg.EndIf();
				}
			}
			this.ilg.Ldc(false);
			this.ilg.GotoMethodEnd();
			this.ilg.EndMethod();
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x000AC954 File Offset: 0x000AAB54
		internal string GenerateBaseSerializer(string baseSerializer, string readerClass, string writerClass, CodeIdentifiers classes)
		{
			baseSerializer = CodeIdentifier.MakeValid(baseSerializer);
			baseSerializer = classes.AddUnique(baseSerializer, baseSerializer);
			TypeBuilder typeBuilder = CodeGenerator.CreateTypeBuilder(this.moduleBuilder, CodeIdentifier.GetCSharpName(baseSerializer), TypeAttributes.Public | TypeAttributes.Abstract | TypeAttributes.BeforeFieldInit, typeof(XmlSerializer), CodeGenerator.EmptyTypeArray);
			ConstructorInfo constructor = this.CreatedTypes[readerClass].GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg = new CodeGenerator(typeBuilder);
			this.ilg.BeginMethod(typeof(XmlSerializationReader), "CreateReader", CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, CodeGenerator.ProtectedOverrideMethodAttributes);
			this.ilg.New(constructor);
			this.ilg.EndMethod();
			ConstructorInfo constructor2 = this.CreatedTypes[writerClass].GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg.BeginMethod(typeof(XmlSerializationWriter), "CreateWriter", CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, CodeGenerator.ProtectedOverrideMethodAttributes);
			this.ilg.New(constructor2);
			this.ilg.EndMethod();
			typeBuilder.DefineDefaultConstructor(MethodAttributes.Family | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
			Type type = typeBuilder.CreateType();
			this.CreatedTypes.Add(type.Name, type);
			return baseSerializer;
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x000ACA8C File Offset: 0x000AAC8C
		internal string GenerateTypedSerializer(string readMethod, string writeMethod, XmlMapping mapping, CodeIdentifiers classes, string baseSerializer, string readerClass, string writerClass)
		{
			string text = CodeIdentifier.MakeValid(Accessor.UnescapeName(mapping.Accessor.Mapping.TypeDesc.Name));
			text = classes.AddUnique(text + "Serializer", mapping);
			TypeBuilder typeBuilder = CodeGenerator.CreateTypeBuilder(this.moduleBuilder, CodeIdentifier.GetCSharpName(text), TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit, this.CreatedTypes[baseSerializer], CodeGenerator.EmptyTypeArray);
			this.ilg = new CodeGenerator(typeBuilder);
			this.ilg.BeginMethod(typeof(bool), "CanDeserialize", new Type[]
			{
				typeof(XmlReader)
			}, new string[]
			{
				"xmlReader"
			}, CodeGenerator.PublicOverrideMethodAttributes);
			if (mapping.Accessor.Any)
			{
				this.ilg.Ldc(true);
				this.ilg.Stloc(this.ilg.ReturnLocal);
				this.ilg.Br(this.ilg.ReturnLabel);
			}
			else
			{
				MethodInfo method = typeof(XmlReader).GetMethod("IsStartElement", CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(string),
					typeof(string)
				}, null);
				this.ilg.Ldarg(this.ilg.GetArg("xmlReader"));
				this.ilg.Ldstr(mapping.Accessor.Name);
				this.ilg.Ldstr(mapping.Accessor.Namespace);
				this.ilg.Call(method);
				this.ilg.Stloc(this.ilg.ReturnLocal);
				this.ilg.Br(this.ilg.ReturnLabel);
			}
			this.ilg.MarkLabel(this.ilg.ReturnLabel);
			this.ilg.Ldloc(this.ilg.ReturnLocal);
			this.ilg.EndMethod();
			if (writeMethod != null)
			{
				this.ilg = new CodeGenerator(typeBuilder);
				this.ilg.BeginMethod(typeof(void), "Serialize", new Type[]
				{
					typeof(object),
					typeof(XmlSerializationWriter)
				}, new string[]
				{
					"objectToSerialize",
					"writer"
				}, CodeGenerator.ProtectedOverrideMethodAttributes);
				MethodInfo method2 = this.CreatedTypes[writerClass].GetMethod(writeMethod, CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					(mapping is XmlMembersMapping) ? typeof(object[]) : typeof(object)
				}, null);
				this.ilg.Ldarg("writer");
				this.ilg.Castclass(this.CreatedTypes[writerClass]);
				this.ilg.Ldarg("objectToSerialize");
				if (mapping is XmlMembersMapping)
				{
					this.ilg.ConvertValue(typeof(object), typeof(object[]));
				}
				this.ilg.Call(method2);
				this.ilg.EndMethod();
			}
			if (readMethod != null)
			{
				this.ilg = new CodeGenerator(typeBuilder);
				this.ilg.BeginMethod(typeof(object), "Deserialize", new Type[]
				{
					typeof(XmlSerializationReader)
				}, new string[]
				{
					"reader"
				}, CodeGenerator.ProtectedOverrideMethodAttributes);
				MethodInfo method3 = this.CreatedTypes[readerClass].GetMethod(readMethod, CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				this.ilg.Ldarg("reader");
				this.ilg.Castclass(this.CreatedTypes[readerClass]);
				this.ilg.Call(method3);
				this.ilg.EndMethod();
			}
			typeBuilder.DefineDefaultConstructor(CodeGenerator.PublicMethodAttributes);
			Type type = typeBuilder.CreateType();
			this.CreatedTypes.Add(type.Name, type);
			return type.Name;
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x000ACE80 File Offset: 0x000AB080
		private FieldBuilder GenerateTypedSerializers(Hashtable serializers, TypeBuilder serializerContractTypeBuilder)
		{
			string privateName = "typedSerializers";
			FieldBuilder fieldBuilder = this.GenerateHashtableGetBegin(privateName, "TypedSerializers", serializerContractTypeBuilder);
			MethodInfo method = typeof(Hashtable).GetMethod("Add", CodeGenerator.InstanceBindingFlags, null, new Type[]
			{
				typeof(object),
				typeof(object)
			}, null);
			foreach (object obj in serializers.Keys)
			{
				string text = (string)obj;
				ConstructorInfo constructor = this.CreatedTypes[(string)serializers[text]].GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				this.ilg.Ldloc(typeof(Hashtable), "_tmp");
				this.ilg.Ldstr(text);
				this.ilg.New(constructor);
				this.ilg.Call(method);
			}
			this.GenerateHashtableGetEnd(fieldBuilder);
			return fieldBuilder;
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x000ACF9C File Offset: 0x000AB19C
		private void GenerateGetSerializer(Hashtable serializers, XmlMapping[] xmlMappings, TypeBuilder serializerContractTypeBuilder)
		{
			this.ilg = new CodeGenerator(serializerContractTypeBuilder);
			this.ilg.BeginMethod(typeof(XmlSerializer), "GetSerializer", new Type[]
			{
				typeof(Type)
			}, new string[]
			{
				"type"
			}, CodeGenerator.PublicOverrideMethodAttributes);
			for (int i = 0; i < xmlMappings.Length; i++)
			{
				if (xmlMappings[i] is XmlTypeMapping)
				{
					Type type = xmlMappings[i].Accessor.Mapping.TypeDesc.Type;
					if (!(type == null) && (type.IsPublic || type.IsNestedPublic) && !type.IsGenericType && !type.ContainsGenericParameters)
					{
						this.ilg.Ldarg("type");
						this.ilg.Ldc(type);
						this.ilg.If(Cmp.EqualTo);
						ConstructorInfo constructor = this.CreatedTypes[(string)serializers[xmlMappings[i].Key]].GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
						this.ilg.New(constructor);
						this.ilg.Stloc(this.ilg.ReturnLocal);
						this.ilg.Br(this.ilg.ReturnLabel);
						this.ilg.EndIf();
					}
				}
			}
			this.ilg.Load(null);
			this.ilg.Stloc(this.ilg.ReturnLocal);
			this.ilg.Br(this.ilg.ReturnLabel);
			this.ilg.MarkLabel(this.ilg.ReturnLabel);
			this.ilg.Ldloc(this.ilg.ReturnLocal);
			this.ilg.EndMethod();
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x000AD170 File Offset: 0x000AB370
		internal void GenerateSerializerContract(string className, XmlMapping[] xmlMappings, Type[] types, string readerType, string[] readMethods, string writerType, string[] writerMethods, Hashtable serializers)
		{
			TypeBuilder typeBuilder = CodeGenerator.CreateTypeBuilder(this.moduleBuilder, "XmlSerializerContract", TypeAttributes.Public | TypeAttributes.BeforeFieldInit, typeof(XmlSerializerImplementation), CodeGenerator.EmptyTypeArray);
			this.ilg = new CodeGenerator(typeBuilder);
			PropertyBuilder propertyBuilder = typeBuilder.DefineProperty("Reader", PropertyAttributes.None, CallingConventions.HasThis, typeof(XmlSerializationReader), null, null, null, null, null);
			this.ilg.BeginMethod(typeof(XmlSerializationReader), "get_Reader", CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, CodeGenerator.PublicOverrideMethodAttributes | MethodAttributes.SpecialName);
			propertyBuilder.SetGetMethod(this.ilg.MethodBuilder);
			ConstructorInfo constructor = this.CreatedTypes[readerType].GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg.New(constructor);
			this.ilg.EndMethod();
			this.ilg = new CodeGenerator(typeBuilder);
			PropertyBuilder propertyBuilder2 = typeBuilder.DefineProperty("Writer", PropertyAttributes.None, CallingConventions.HasThis, typeof(XmlSerializationWriter), null, null, null, null, null);
			this.ilg.BeginMethod(typeof(XmlSerializationWriter), "get_Writer", CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, CodeGenerator.PublicOverrideMethodAttributes | MethodAttributes.SpecialName);
			propertyBuilder2.SetGetMethod(this.ilg.MethodBuilder);
			constructor = this.CreatedTypes[writerType].GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg.New(constructor);
			this.ilg.EndMethod();
			FieldBuilder memberInfo = this.GeneratePublicMethods("readMethods", "ReadMethods", readMethods, xmlMappings, typeBuilder);
			FieldBuilder memberInfo2 = this.GeneratePublicMethods("writeMethods", "WriteMethods", writerMethods, xmlMappings, typeBuilder);
			FieldBuilder memberInfo3 = this.GenerateTypedSerializers(serializers, typeBuilder);
			this.GenerateSupportedTypes(types, typeBuilder);
			this.GenerateGetSerializer(serializers, xmlMappings, typeBuilder);
			ConstructorInfo constructor2 = typeof(XmlSerializerImplementation).GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg = new CodeGenerator(typeBuilder);
			this.ilg.BeginMethod(typeof(void), ".ctor", CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, CodeGenerator.PublicMethodAttributes | MethodAttributes.RTSpecialName | MethodAttributes.SpecialName);
			this.ilg.Ldarg(0);
			this.ilg.Load(null);
			this.ilg.StoreMember(memberInfo);
			this.ilg.Ldarg(0);
			this.ilg.Load(null);
			this.ilg.StoreMember(memberInfo2);
			this.ilg.Ldarg(0);
			this.ilg.Load(null);
			this.ilg.StoreMember(memberInfo3);
			this.ilg.Ldarg(0);
			this.ilg.Call(constructor2);
			this.ilg.EndMethod();
			Type type = typeBuilder.CreateType();
			this.CreatedTypes.Add(type.Name, type);
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x000AC2F4 File Offset: 0x000AA4F4
		internal static bool IsWildcard(SpecialMapping mapping)
		{
			if (mapping is SerializableMapping)
			{
				return ((SerializableMapping)mapping).IsAny;
			}
			return mapping.TypeDesc.CanBeElementValue;
		}

		// Token: 0x06001D88 RID: 7560 RVA: 0x000AD433 File Offset: 0x000AB633
		internal void ILGenLoad(string source)
		{
			this.ILGenLoad(source, null);
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x000AD440 File Offset: 0x000AB640
		internal void ILGenLoad(string source, Type type)
		{
			if (source.StartsWith("o.@", StringComparison.Ordinal))
			{
				MemberInfo memberInfo = this.memberInfos[source.Substring(3)];
				this.ilg.LoadMember(this.ilg.GetVariable("o"), memberInfo);
				if (type != null)
				{
					Type source2 = (memberInfo.MemberType == MemberTypes.Field) ? ((FieldInfo)memberInfo).FieldType : ((PropertyInfo)memberInfo).PropertyType;
					this.ilg.ConvertValue(source2, type);
					return;
				}
			}
			else
			{
				new SourceInfo(source, null, null, null, this.ilg).Load(type);
			}
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x000AD4D9 File Offset: 0x000AB6D9
		// Note: this type is marked as 'beforefieldinit'.
		static XmlSerializationILGen()
		{
		}

		// Token: 0x04001A54 RID: 6740
		private int nextMethodNumber;

		// Token: 0x04001A55 RID: 6741
		private Hashtable methodNames = new Hashtable();

		// Token: 0x04001A56 RID: 6742
		private Dictionary<string, MethodBuilderInfo> methodBuilders = new Dictionary<string, MethodBuilderInfo>();

		// Token: 0x04001A57 RID: 6743
		internal Dictionary<string, Type> CreatedTypes = new Dictionary<string, Type>();

		// Token: 0x04001A58 RID: 6744
		internal Dictionary<string, MemberInfo> memberInfos = new Dictionary<string, MemberInfo>();

		// Token: 0x04001A59 RID: 6745
		private ReflectionAwareILGen raCodeGen;

		// Token: 0x04001A5A RID: 6746
		private TypeScope[] scopes;

		// Token: 0x04001A5B RID: 6747
		private TypeDesc stringTypeDesc;

		// Token: 0x04001A5C RID: 6748
		private TypeDesc qnameTypeDesc;

		// Token: 0x04001A5D RID: 6749
		private string className;

		// Token: 0x04001A5E RID: 6750
		private TypeMapping[] referencedMethods;

		// Token: 0x04001A5F RID: 6751
		private int references;

		// Token: 0x04001A60 RID: 6752
		private Hashtable generatedMethods = new Hashtable();

		// Token: 0x04001A61 RID: 6753
		private ModuleBuilder moduleBuilder;

		// Token: 0x04001A62 RID: 6754
		private TypeAttributes typeAttributes;

		// Token: 0x04001A63 RID: 6755
		protected TypeBuilder typeBuilder;

		// Token: 0x04001A64 RID: 6756
		protected CodeGenerator ilg;

		// Token: 0x04001A65 RID: 6757
		private static Dictionary<string, Regex> regexs = new Dictionary<string, Regex>();
	}
}
