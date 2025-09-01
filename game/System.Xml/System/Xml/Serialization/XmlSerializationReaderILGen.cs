using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Security;
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x020002F5 RID: 757
	internal class XmlSerializationReaderILGen : XmlSerializationILGen
	{
		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001E69 RID: 7785 RVA: 0x000B8CDB File Offset: 0x000B6EDB
		internal Hashtable Enums
		{
			get
			{
				if (this.enums == null)
				{
					this.enums = new Hashtable();
				}
				return this.enums;
			}
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x000B8CF6 File Offset: 0x000B6EF6
		internal XmlSerializationReaderILGen(TypeScope[] scopes, string access, string className) : base(scopes, access, className)
		{
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x000B8D18 File Offset: 0x000B6F18
		internal void GenerateBegin()
		{
			this.typeBuilder = CodeGenerator.CreateTypeBuilder(base.ModuleBuilder, base.ClassName, base.TypeAttributes | TypeAttributes.BeforeFieldInit, typeof(XmlSerializationReader), CodeGenerator.EmptyTypeArray);
			foreach (TypeScope typeScope in base.Scopes)
			{
				foreach (object obj in typeScope.TypeMappings)
				{
					TypeMapping typeMapping = (TypeMapping)obj;
					if (typeMapping is StructMapping || typeMapping is EnumMapping || typeMapping is NullableMapping)
					{
						base.MethodNames.Add(typeMapping, this.NextMethodName(typeMapping.TypeDesc.Name));
					}
				}
				base.RaCodeGen.WriteReflectionInit(typeScope);
			}
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x000B8E08 File Offset: 0x000B7008
		internal override void GenerateMethod(TypeMapping mapping)
		{
			if (base.GeneratedMethods.Contains(mapping))
			{
				return;
			}
			base.GeneratedMethods[mapping] = mapping;
			if (mapping is StructMapping)
			{
				this.WriteStructMethod((StructMapping)mapping);
				return;
			}
			if (mapping is EnumMapping)
			{
				this.WriteEnumMethod((EnumMapping)mapping);
				return;
			}
			if (mapping is NullableMapping)
			{
				this.WriteNullableMethod((NullableMapping)mapping);
			}
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x000B8E70 File Offset: 0x000B7070
		internal void GenerateEnd(string[] methods, XmlMapping[] xmlMappings, Type[] types)
		{
			base.GenerateReferencedMethods();
			this.GenerateInitCallbacksMethod();
			this.ilg = new CodeGenerator(this.typeBuilder);
			this.ilg.BeginMethod(typeof(void), "InitIDs", CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, CodeGenerator.ProtectedOverrideMethodAttributes);
			MethodInfo method = typeof(XmlSerializationReader).GetMethod("get_Reader", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			MethodInfo method2 = typeof(XmlReader).GetMethod("get_NameTable", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			MethodInfo method3 = typeof(XmlNameTable).GetMethod("Add", CodeGenerator.InstanceBindingFlags, null, new Type[]
			{
				typeof(string)
			}, null);
			foreach (object obj in this.idNames.Keys)
			{
				string text = (string)obj;
				this.ilg.Ldarg(0);
				this.ilg.Ldarg(0);
				this.ilg.Call(method);
				this.ilg.Call(method2);
				this.ilg.Ldstr(text);
				this.ilg.Call(method3);
				this.ilg.StoreMember(this.idNameFields[text]);
			}
			this.ilg.EndMethod();
			this.typeBuilder.DefineDefaultConstructor(CodeGenerator.PublicMethodAttributes);
			Type type = this.typeBuilder.CreateType();
			this.CreatedTypes.Add(type.Name, type);
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x000B9024 File Offset: 0x000B7224
		internal string GenerateElement(XmlMapping xmlMapping)
		{
			if (!xmlMapping.IsReadable)
			{
				return null;
			}
			if (!xmlMapping.GenerateSerializer)
			{
				throw new ArgumentException(Res.GetString("Internal error."), "xmlMapping");
			}
			if (xmlMapping is XmlTypeMapping)
			{
				return this.GenerateTypeElement((XmlTypeMapping)xmlMapping);
			}
			if (xmlMapping is XmlMembersMapping)
			{
				return this.GenerateMembersElement((XmlMembersMapping)xmlMapping);
			}
			throw new ArgumentException(Res.GetString("Internal error."), "xmlMapping");
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x000B9098 File Offset: 0x000B7298
		private void WriteIsStartTag(string name, string ns)
		{
			this.WriteID(name);
			this.WriteID(ns);
			MethodInfo method = typeof(XmlSerializationReader).GetMethod("get_Reader", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			MethodInfo method2 = typeof(XmlReader).GetMethod("IsStartElement", CodeGenerator.InstanceBindingFlags, null, new Type[]
			{
				typeof(string),
				typeof(string)
			}, null);
			this.ilg.Ldarg(0);
			this.ilg.Call(method);
			this.ilg.Ldarg(0);
			this.ilg.LoadMember(this.idNameFields[name ?? string.Empty]);
			this.ilg.Ldarg(0);
			this.ilg.LoadMember(this.idNameFields[ns ?? string.Empty]);
			this.ilg.Call(method2);
			this.ilg.If();
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x000B919C File Offset: 0x000B739C
		private void WriteUnknownNode(string func, string node, ElementAccessor e, bool anyIfs)
		{
			if (anyIfs)
			{
				this.ilg.Else();
			}
			List<Type> list = new List<Type>();
			this.ilg.Ldarg(0);
			if (node == "null")
			{
				this.ilg.Load(null);
			}
			else
			{
				object variable = this.ilg.GetVariable("p");
				this.ilg.Load(variable);
				this.ilg.ConvertValue(this.ilg.GetVariableType(variable), typeof(object));
			}
			list.Add(typeof(object));
			if (e != null)
			{
				string text = (e.Form == XmlSchemaForm.Qualified) ? e.Namespace : "";
				text += ":";
				text += e.Name;
				this.ilg.Ldstr(ReflectionAwareILGen.GetCSharpString(text));
				list.Add(typeof(string));
			}
			MethodInfo method = typeof(XmlSerializationReader).GetMethod(func, CodeGenerator.InstanceBindingFlags, null, list.ToArray(), null);
			this.ilg.Call(method);
			if (anyIfs)
			{
				this.ilg.EndIf();
			}
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x000B92C0 File Offset: 0x000B74C0
		private void GenerateInitCallbacksMethod()
		{
			this.ilg = new CodeGenerator(this.typeBuilder);
			this.ilg.BeginMethod(typeof(void), "InitCallbacks", CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, CodeGenerator.ProtectedOverrideMethodAttributes);
			string methodName = this.NextMethodName("Array");
			bool flag = false;
			this.ilg.EndMethod();
			if (flag)
			{
				this.ilg.BeginMethod(typeof(object), base.GetMethodBuilder(methodName), CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, CodeGenerator.PrivateMethodAttributes);
				MethodInfo method = typeof(XmlSerializationReader).GetMethod("UnknownNode", CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(object)
				}, null);
				this.ilg.Ldarg(0);
				this.ilg.Load(null);
				this.ilg.Call(method);
				this.ilg.Load(null);
				this.ilg.EndMethod();
			}
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x000B93BB File Offset: 0x000B75BB
		private string GenerateMembersElement(XmlMembersMapping xmlMembersMapping)
		{
			return this.GenerateLiteralMembersElement(xmlMembersMapping);
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x000B93C4 File Offset: 0x000B75C4
		private string GetChoiceIdentifierSource(MemberMapping[] mappings, MemberMapping member)
		{
			string result = null;
			if (member.ChoiceIdentifier != null)
			{
				for (int i = 0; i < mappings.Length; i++)
				{
					if (mappings[i].Name == member.ChoiceIdentifier.MemberName)
					{
						result = "p[" + i.ToString(CultureInfo.InvariantCulture) + "]";
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x000B9422 File Offset: 0x000B7622
		private string GetChoiceIdentifierSource(MemberMapping mapping, string parent, TypeDesc parentTypeDesc)
		{
			if (mapping.ChoiceIdentifier == null)
			{
				return "";
			}
			CodeIdentifier.CheckValidIdentifier(mapping.ChoiceIdentifier.MemberName);
			return base.RaCodeGen.GetStringForMember(parent, mapping.ChoiceIdentifier.MemberName, parentTypeDesc);
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x000B945C File Offset: 0x000B765C
		private string GenerateLiteralMembersElement(XmlMembersMapping xmlMembersMapping)
		{
			ElementAccessor accessor = xmlMembersMapping.Accessor;
			MemberMapping[] members = ((MembersMapping)accessor.Mapping).Members;
			bool hasWrapperElement = ((MembersMapping)accessor.Mapping).HasWrapperElement;
			string text = this.NextMethodName(accessor.Name);
			this.ilg = new CodeGenerator(this.typeBuilder);
			this.ilg.BeginMethod(typeof(object[]), text, CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, CodeGenerator.PublicMethodAttributes);
			this.ilg.Load(null);
			this.ilg.Stloc(this.ilg.ReturnLocal);
			MethodInfo method = typeof(XmlSerializationReader).GetMethod("get_Reader", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			MethodInfo method2 = typeof(XmlReader).GetMethod("MoveToContent", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg.Ldarg(0);
			this.ilg.Call(method);
			this.ilg.Call(method2);
			this.ilg.Pop();
			LocalBuilder localBuilder = this.ilg.DeclareLocal(typeof(object[]), "p");
			this.ilg.NewArray(typeof(object), members.Length);
			this.ilg.Stloc(localBuilder);
			this.InitializeValueTypes("p", members);
			int loopIndex = 0;
			if (hasWrapperElement)
			{
				loopIndex = this.WriteWhileNotLoopStart();
				this.WriteIsStartTag(accessor.Name, (accessor.Form == XmlSchemaForm.Qualified) ? accessor.Namespace : "");
			}
			XmlSerializationReaderILGen.Member anyText = null;
			XmlSerializationReaderILGen.Member anyElement = null;
			XmlSerializationReaderILGen.Member anyAttribute = null;
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			ArrayList arrayList3 = new ArrayList();
			for (int i = 0; i < members.Length; i++)
			{
				MemberMapping memberMapping = members[i];
				string text2 = "p[" + i.ToString(CultureInfo.InvariantCulture) + "]";
				string arraySource = text2;
				if (memberMapping.Xmlns != null)
				{
					arraySource = string.Concat(new string[]
					{
						"((",
						memberMapping.TypeDesc.CSharpName,
						")",
						text2,
						")"
					});
				}
				string choiceIdentifierSource = this.GetChoiceIdentifierSource(members, memberMapping);
				XmlSerializationReaderILGen.Member member = new XmlSerializationReaderILGen.Member(this, text2, arraySource, "a", i, memberMapping, choiceIdentifierSource);
				XmlSerializationReaderILGen.Member member2 = new XmlSerializationReaderILGen.Member(this, text2, null, "a", i, memberMapping, choiceIdentifierSource);
				if (!memberMapping.IsSequence)
				{
					member.ParamsReadSource = "paramsRead[" + i.ToString(CultureInfo.InvariantCulture) + "]";
				}
				if (memberMapping.CheckSpecified == SpecifiedAccessor.ReadWrite)
				{
					string b = memberMapping.Name + "Specified";
					for (int j = 0; j < members.Length; j++)
					{
						if (members[j].Name == b)
						{
							member.CheckSpecifiedSource = "p[" + j.ToString(CultureInfo.InvariantCulture) + "]";
							break;
						}
					}
				}
				bool flag = false;
				if (memberMapping.Text != null)
				{
					anyText = member2;
				}
				if (memberMapping.Attribute != null && memberMapping.Attribute.Any)
				{
					anyAttribute = member2;
				}
				if (memberMapping.Attribute != null || memberMapping.Xmlns != null)
				{
					arrayList3.Add(member);
				}
				else if (memberMapping.Text != null)
				{
					arrayList2.Add(member);
				}
				if (!memberMapping.IsSequence)
				{
					for (int k = 0; k < memberMapping.Elements.Length; k++)
					{
						if (memberMapping.Elements[k].Any && memberMapping.Elements[k].Name.Length == 0)
						{
							anyElement = member2;
							if (memberMapping.Attribute == null && memberMapping.Text == null)
							{
								arrayList2.Add(member2);
							}
							flag = true;
							break;
						}
					}
				}
				if (memberMapping.Attribute != null || memberMapping.Text != null || flag)
				{
					arrayList.Add(member2);
				}
				else if (memberMapping.TypeDesc.IsArrayLike && (memberMapping.Elements.Length != 1 || !(memberMapping.Elements[0].Mapping is ArrayMapping)))
				{
					arrayList.Add(member2);
					arrayList2.Add(member2);
				}
				else
				{
					if (memberMapping.TypeDesc.IsArrayLike && !memberMapping.TypeDesc.IsArray)
					{
						member.ParamsReadSource = null;
					}
					arrayList.Add(member);
				}
			}
			XmlSerializationReaderILGen.Member[] array = (XmlSerializationReaderILGen.Member[])arrayList.ToArray(typeof(XmlSerializationReaderILGen.Member));
			XmlSerializationReaderILGen.Member[] members2 = (XmlSerializationReaderILGen.Member[])arrayList2.ToArray(typeof(XmlSerializationReaderILGen.Member));
			if (array.Length != 0 && array[0].Mapping.IsReturnValue)
			{
				MethodInfo method3 = typeof(XmlSerializationReader).GetMethod("set_IsReturnValue", CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(bool)
				}, null);
				this.ilg.Ldarg(0);
				this.ilg.Ldc(true);
				this.ilg.Call(method3);
			}
			this.WriteParamsRead(members.Length);
			if (arrayList3.Count > 0)
			{
				XmlSerializationReaderILGen.Member[] members3 = (XmlSerializationReaderILGen.Member[])arrayList3.ToArray(typeof(XmlSerializationReaderILGen.Member));
				this.WriteMemberBegin(members3);
				this.WriteAttributes(members3, anyAttribute, "UnknownNode", localBuilder);
				this.WriteMemberEnd(members3);
				MethodInfo method4 = typeof(XmlReader).GetMethod("MoveToElement", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				this.ilg.Ldarg(0);
				this.ilg.Call(method);
				this.ilg.Call(method4);
				this.ilg.Pop();
			}
			this.WriteMemberBegin(members2);
			if (hasWrapperElement)
			{
				MethodInfo method5 = typeof(XmlReader).GetMethod("get_IsEmptyElement", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				this.ilg.Ldarg(0);
				this.ilg.Call(method);
				this.ilg.Call(method5);
				this.ilg.If();
				MethodInfo method6 = typeof(XmlReader).GetMethod("Skip", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				this.ilg.Ldarg(0);
				this.ilg.Call(method);
				this.ilg.Call(method6);
				this.ilg.Ldarg(0);
				this.ilg.Call(method);
				this.ilg.Call(method2);
				this.ilg.Pop();
				this.ilg.WhileContinue();
				this.ilg.EndIf();
				MethodInfo method7 = typeof(XmlReader).GetMethod("ReadStartElement", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				this.ilg.Ldarg(0);
				this.ilg.Call(method);
				this.ilg.Call(method7);
			}
			if (this.IsSequence(array))
			{
				this.ilg.Ldc(0);
				this.ilg.Stloc(typeof(int), "state");
			}
			int loopIndex2 = this.WriteWhileNotLoopStart();
			string text3 = "UnknownNode((object)p, " + this.ExpectedElements(array) + ");";
			this.WriteMemberElements(array, text3, text3, anyElement, anyText);
			this.ilg.Ldarg(0);
			this.ilg.Call(method);
			this.ilg.Call(method2);
			this.ilg.Pop();
			this.WriteWhileLoopEnd(loopIndex2);
			this.WriteMemberEnd(members2);
			if (hasWrapperElement)
			{
				MethodInfo method8 = typeof(XmlSerializationReader).GetMethod("ReadEndElement", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				this.ilg.Ldarg(0);
				this.ilg.Call(method8);
				this.WriteUnknownNode("UnknownNode", "null", accessor, true);
				this.ilg.Ldarg(0);
				this.ilg.Call(method);
				this.ilg.Call(method2);
				this.ilg.Pop();
				this.WriteWhileLoopEnd(loopIndex);
			}
			this.ilg.Ldloc(this.ilg.GetLocal("p"));
			this.ilg.EndMethod();
			return text;
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x000B9C84 File Offset: 0x000B7E84
		private void InitializeValueTypes(string arrayName, MemberMapping[] mappings)
		{
			for (int i = 0; i < mappings.Length; i++)
			{
				if (mappings[i].TypeDesc.IsValueType)
				{
					LocalBuilder local = this.ilg.GetLocal(arrayName);
					this.ilg.Ldloc(local);
					this.ilg.Ldc(i);
					base.RaCodeGen.ILGenForCreateInstance(this.ilg, mappings[i].TypeDesc.Type, false, false);
					this.ilg.ConvertValue(mappings[i].TypeDesc.Type, typeof(object));
					this.ilg.Stelem(local.LocalType.GetElementType());
				}
			}
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x000B9D34 File Offset: 0x000B7F34
		private string GenerateTypeElement(XmlTypeMapping xmlTypeMapping)
		{
			ElementAccessor accessor = xmlTypeMapping.Accessor;
			TypeMapping mapping = accessor.Mapping;
			string text = this.NextMethodName(accessor.Name);
			this.ilg = new CodeGenerator(this.typeBuilder);
			this.ilg.BeginMethod(typeof(object), text, CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, CodeGenerator.PublicMethodAttributes);
			LocalBuilder localBuilder = this.ilg.DeclareLocal(typeof(object), "o");
			this.ilg.Load(null);
			this.ilg.Stloc(localBuilder);
			XmlSerializationReaderILGen.Member[] array = new XmlSerializationReaderILGen.Member[]
			{
				new XmlSerializationReaderILGen.Member(this, "o", "o", "a", 0, new MemberMapping
				{
					TypeDesc = mapping.TypeDesc,
					Elements = new ElementAccessor[]
					{
						accessor
					}
				})
			};
			MethodInfo method = typeof(XmlSerializationReader).GetMethod("get_Reader", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			MethodInfo method2 = typeof(XmlReader).GetMethod("MoveToContent", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg.Ldarg(0);
			this.ilg.Call(method);
			this.ilg.Call(method2);
			this.ilg.Pop();
			string elseString = "UnknownNode(null, " + this.ExpectedElements(array) + ");";
			this.WriteMemberElements(array, "throw CreateUnknownNodeException();", elseString, accessor.Any ? array[0] : null, null);
			this.ilg.Ldloc(localBuilder);
			this.ilg.Stloc(this.ilg.ReturnLocal);
			this.ilg.Ldloc(this.ilg.ReturnLocal);
			this.ilg.EndMethod();
			return text;
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x000B9F00 File Offset: 0x000B8100
		private string NextMethodName(string name)
		{
			string str = "Read";
			int nextMethodNumber = base.NextMethodNumber + 1;
			base.NextMethodNumber = nextMethodNumber;
			return str + nextMethodNumber.ToString(CultureInfo.InvariantCulture) + "_" + CodeIdentifier.MakeValidInternal(name);
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x000B9F40 File Offset: 0x000B8140
		private string NextIdName(string name)
		{
			string str = "id";
			int num = this.nextIdNumber + 1;
			this.nextIdNumber = num;
			return str + num.ToString(CultureInfo.InvariantCulture) + "_" + CodeIdentifier.MakeValidInternal(name);
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x000B9F80 File Offset: 0x000B8180
		private void WritePrimitive(TypeMapping mapping, string source)
		{
			if (mapping is EnumMapping)
			{
				string text = base.ReferenceMapping(mapping);
				if (text == null)
				{
					throw new InvalidOperationException(Res.GetString("The method for enum {0} is missing.", new object[]
					{
						mapping.TypeDesc.Name
					}));
				}
				MethodBuilder methodInfo = base.EnsureMethodBuilder(this.typeBuilder, text, CodeGenerator.PrivateMethodAttributes, mapping.TypeDesc.Type, new Type[]
				{
					typeof(string)
				});
				this.ilg.Ldarg(0);
				if (source == "Reader.ReadElementString()" || source == "Reader.ReadString()")
				{
					MethodInfo method = typeof(XmlSerializationReader).GetMethod("get_Reader", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
					MethodInfo method2 = typeof(XmlReader).GetMethod((source == "Reader.ReadElementString()") ? "ReadElementString" : "ReadString", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
					this.ilg.Ldarg(0);
					this.ilg.Call(method);
					this.ilg.Call(method2);
				}
				else if (source == "Reader.Value")
				{
					MethodInfo method3 = typeof(XmlSerializationReader).GetMethod("get_Reader", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
					MethodInfo method4 = typeof(XmlReader).GetMethod("get_Value", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
					this.ilg.Ldarg(0);
					this.ilg.Call(method3);
					this.ilg.Call(method4);
				}
				else if (source == "vals[i]")
				{
					LocalBuilder local = this.ilg.GetLocal("vals");
					LocalBuilder local2 = this.ilg.GetLocal("i");
					this.ilg.LoadArrayElement(local, local2);
				}
				else
				{
					if (!(source == "false"))
					{
						throw CodeGenerator.NotSupported("Unexpected: " + source);
					}
					this.ilg.Ldc(false);
				}
				this.ilg.Call(methodInfo);
				return;
			}
			else
			{
				if (mapping.TypeDesc != base.StringTypeDesc)
				{
					if (mapping.TypeDesc.FormatterName == "String")
					{
						if (source == "vals[i]")
						{
							if (mapping.TypeDesc.CollapseWhitespace)
							{
								this.ilg.Ldarg(0);
							}
							LocalBuilder local3 = this.ilg.GetLocal("vals");
							LocalBuilder local4 = this.ilg.GetLocal("i");
							this.ilg.LoadArrayElement(local3, local4);
							if (mapping.TypeDesc.CollapseWhitespace)
							{
								MethodInfo method5 = typeof(XmlSerializationReader).GetMethod("CollapseWhitespace", CodeGenerator.InstanceBindingFlags, null, new Type[]
								{
									typeof(string)
								}, null);
								this.ilg.Call(method5);
								return;
							}
						}
						else
						{
							MethodInfo method6 = typeof(XmlSerializationReader).GetMethod("get_Reader", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
							MethodInfo method7 = typeof(XmlReader).GetMethod((source == "Reader.Value") ? "get_Value" : "ReadElementString", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
							if (mapping.TypeDesc.CollapseWhitespace)
							{
								this.ilg.Ldarg(0);
							}
							this.ilg.Ldarg(0);
							this.ilg.Call(method6);
							this.ilg.Call(method7);
							if (mapping.TypeDesc.CollapseWhitespace)
							{
								MethodInfo method8 = typeof(XmlSerializationReader).GetMethod("CollapseWhitespace", CodeGenerator.InstanceBindingFlags, null, new Type[]
								{
									typeof(string)
								}, null);
								this.ilg.Call(method8);
								return;
							}
						}
					}
					else
					{
						Type type = (source == "false") ? typeof(bool) : typeof(string);
						MethodInfo method9;
						if (mapping.TypeDesc.HasCustomFormatter)
						{
							BindingFlags bindingAttr = CodeGenerator.StaticBindingFlags;
							if ((mapping.TypeDesc.FormatterName == "ByteArrayBase64" && source == "false") || (mapping.TypeDesc.FormatterName == "ByteArrayHex" && source == "false") || mapping.TypeDesc.FormatterName == "XmlQualifiedName")
							{
								bindingAttr = CodeGenerator.InstanceBindingFlags;
								this.ilg.Ldarg(0);
							}
							method9 = typeof(XmlSerializationReader).GetMethod("To" + mapping.TypeDesc.FormatterName, bindingAttr, null, new Type[]
							{
								type
							}, null);
						}
						else
						{
							method9 = typeof(XmlConvert).GetMethod("To" + mapping.TypeDesc.FormatterName, CodeGenerator.StaticBindingFlags, null, new Type[]
							{
								type
							}, null);
						}
						if (source == "Reader.ReadElementString()" || source == "Reader.ReadString()")
						{
							MethodInfo method10 = typeof(XmlSerializationReader).GetMethod("get_Reader", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
							MethodInfo method11 = typeof(XmlReader).GetMethod((source == "Reader.ReadElementString()") ? "ReadElementString" : "ReadString", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
							this.ilg.Ldarg(0);
							this.ilg.Call(method10);
							this.ilg.Call(method11);
						}
						else if (source == "Reader.Value")
						{
							MethodInfo method12 = typeof(XmlSerializationReader).GetMethod("get_Reader", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
							MethodInfo method13 = typeof(XmlReader).GetMethod("get_Value", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
							this.ilg.Ldarg(0);
							this.ilg.Call(method12);
							this.ilg.Call(method13);
						}
						else if (source == "vals[i]")
						{
							LocalBuilder local5 = this.ilg.GetLocal("vals");
							LocalBuilder local6 = this.ilg.GetLocal("i");
							this.ilg.LoadArrayElement(local5, local6);
						}
						else
						{
							this.ilg.Ldc(false);
						}
						this.ilg.Call(method9);
					}
					return;
				}
				if (source == "Reader.ReadElementString()" || source == "Reader.ReadString()")
				{
					MethodInfo method14 = typeof(XmlSerializationReader).GetMethod("get_Reader", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
					MethodInfo method15 = typeof(XmlReader).GetMethod((source == "Reader.ReadElementString()") ? "ReadElementString" : "ReadString", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
					this.ilg.Ldarg(0);
					this.ilg.Call(method14);
					this.ilg.Call(method15);
					return;
				}
				if (source == "Reader.Value")
				{
					MethodInfo method16 = typeof(XmlSerializationReader).GetMethod("get_Reader", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
					MethodInfo method17 = typeof(XmlReader).GetMethod("get_Value", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
					this.ilg.Ldarg(0);
					this.ilg.Call(method16);
					this.ilg.Call(method17);
					return;
				}
				if (source == "vals[i]")
				{
					LocalBuilder local7 = this.ilg.GetLocal("vals");
					LocalBuilder local8 = this.ilg.GetLocal("i");
					this.ilg.LoadArrayElement(local7, local8);
					return;
				}
				throw CodeGenerator.NotSupported("Unexpected: " + source);
			}
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x000BA740 File Offset: 0x000B8940
		private string MakeUnique(EnumMapping mapping, string name)
		{
			string text = name;
			object obj = this.Enums[text];
			if (obj != null)
			{
				if (obj == mapping)
				{
					return null;
				}
				int num = 0;
				while (obj != null)
				{
					num++;
					text = name + num.ToString(CultureInfo.InvariantCulture);
					obj = this.Enums[text];
				}
			}
			this.Enums.Add(text, mapping);
			return text;
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x000BA7A0 File Offset: 0x000B89A0
		private string WriteHashtable(EnumMapping mapping, string typeName, out MethodBuilder get_TableName)
		{
			get_TableName = null;
			CodeIdentifier.CheckValidIdentifier(typeName);
			string text = this.MakeUnique(mapping, typeName + "Values");
			if (text == null)
			{
				return CodeIdentifier.GetCSharpName(typeName);
			}
			string fieldName = this.MakeUnique(mapping, "_" + text);
			text = CodeIdentifier.GetCSharpName(text);
			FieldBuilder memberInfo = this.typeBuilder.DefineField(fieldName, typeof(Hashtable), FieldAttributes.Private);
			PropertyBuilder propertyBuilder = this.typeBuilder.DefineProperty(text, PropertyAttributes.None, CallingConventions.HasThis, typeof(Hashtable), null, null, null, null, null);
			this.ilg = new CodeGenerator(this.typeBuilder);
			this.ilg.BeginMethod(typeof(Hashtable), "get_" + text, CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, MethodAttributes.Private | MethodAttributes.FamANDAssem | MethodAttributes.HideBySig | MethodAttributes.SpecialName);
			this.ilg.Ldarg(0);
			this.ilg.LoadMember(memberInfo);
			this.ilg.Load(null);
			this.ilg.If(Cmp.EqualTo);
			ConstructorInfo constructor = typeof(Hashtable).GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			LocalBuilder localBuilder = this.ilg.DeclareLocal(typeof(Hashtable), "h");
			this.ilg.New(constructor);
			this.ilg.Stloc(localBuilder);
			ConstantMapping[] constants = mapping.Constants;
			MethodInfo method = typeof(Hashtable).GetMethod("Add", CodeGenerator.InstanceBindingFlags, null, new Type[]
			{
				typeof(object),
				typeof(object)
			}, null);
			for (int i = 0; i < constants.Length; i++)
			{
				this.ilg.Ldloc(localBuilder);
				this.ilg.Ldstr(constants[i].XmlName);
				this.ilg.Ldc(Enum.ToObject(mapping.TypeDesc.Type, constants[i].Value));
				this.ilg.ConvertValue(mapping.TypeDesc.Type, typeof(long));
				this.ilg.ConvertValue(typeof(long), typeof(object));
				this.ilg.Call(method);
			}
			this.ilg.Ldarg(0);
			this.ilg.Ldloc(localBuilder);
			this.ilg.StoreMember(memberInfo);
			this.ilg.EndIf();
			this.ilg.Ldarg(0);
			this.ilg.LoadMember(memberInfo);
			get_TableName = this.ilg.EndMethod();
			propertyBuilder.SetGetMethod(get_TableName);
			return text;
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x000BAA38 File Offset: 0x000B8C38
		private void WriteEnumMethod(EnumMapping mapping)
		{
			MethodBuilder methodInfo = null;
			if (mapping.IsFlags)
			{
				this.WriteHashtable(mapping, mapping.TypeDesc.Name, out methodInfo);
			}
			string methodName = (string)base.MethodNames[mapping];
			string csharpName = mapping.TypeDesc.CSharpName;
			List<Type> list = new List<Type>();
			List<string> list2 = new List<string>();
			Type type = mapping.TypeDesc.Type;
			Type underlyingType = Enum.GetUnderlyingType(type);
			list.Add(typeof(string));
			list2.Add("s");
			this.ilg = new CodeGenerator(this.typeBuilder);
			this.ilg.BeginMethod(type, base.GetMethodBuilder(methodName), list.ToArray(), list2.ToArray(), CodeGenerator.PrivateMethodAttributes);
			ConstantMapping[] constants = mapping.Constants;
			if (mapping.IsFlags)
			{
				MethodInfo method = typeof(XmlSerializationReader).GetMethod("ToEnum", CodeGenerator.StaticBindingFlags, null, new Type[]
				{
					typeof(string),
					typeof(Hashtable),
					typeof(string)
				}, null);
				this.ilg.Ldarg("s");
				this.ilg.Ldarg(0);
				this.ilg.Call(methodInfo);
				this.ilg.Ldstr(csharpName);
				this.ilg.Call(method);
				if (underlyingType != typeof(long))
				{
					this.ilg.ConvertValue(typeof(long), underlyingType);
				}
				this.ilg.Stloc(this.ilg.ReturnLocal);
				this.ilg.Br(this.ilg.ReturnLabel);
			}
			else
			{
				List<Label> list3 = new List<Label>();
				List<object> list4 = new List<object>();
				Label label = this.ilg.DefineLabel();
				Label label2 = this.ilg.DefineLabel();
				LocalBuilder tempLocal = this.ilg.GetTempLocal(typeof(string));
				this.ilg.Ldarg("s");
				this.ilg.Stloc(tempLocal);
				this.ilg.Ldloc(tempLocal);
				this.ilg.Brfalse(label);
				Hashtable hashtable = new Hashtable();
				foreach (ConstantMapping constantMapping in constants)
				{
					CodeIdentifier.CheckValidIdentifier(constantMapping.Name);
					if (hashtable[constantMapping.XmlName] == null)
					{
						hashtable[constantMapping.XmlName] = constantMapping.XmlName;
						Label label3 = this.ilg.DefineLabel();
						this.ilg.Ldloc(tempLocal);
						this.ilg.Ldstr(constantMapping.XmlName);
						MethodInfo method2 = typeof(string).GetMethod("op_Equality", CodeGenerator.StaticBindingFlags, null, new Type[]
						{
							typeof(string),
							typeof(string)
						}, null);
						this.ilg.Call(method2);
						this.ilg.Brtrue(label3);
						list3.Add(label3);
						list4.Add(Enum.ToObject(mapping.TypeDesc.Type, constantMapping.Value));
					}
				}
				this.ilg.Br(label);
				for (int j = 0; j < list3.Count; j++)
				{
					this.ilg.MarkLabel(list3[j]);
					this.ilg.Ldc(list4[j]);
					this.ilg.Stloc(this.ilg.ReturnLocal);
					this.ilg.Br(this.ilg.ReturnLabel);
				}
				MethodInfo method3 = typeof(XmlSerializationReader).GetMethod("CreateUnknownConstantException", CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(string),
					typeof(Type)
				}, null);
				this.ilg.MarkLabel(label);
				this.ilg.Ldarg(0);
				this.ilg.Ldarg("s");
				this.ilg.Ldc(mapping.TypeDesc.Type);
				this.ilg.Call(method3);
				this.ilg.Throw();
				this.ilg.MarkLabel(label2);
			}
			this.ilg.MarkLabel(this.ilg.ReturnLabel);
			this.ilg.Ldloc(this.ilg.ReturnLocal);
			this.ilg.EndMethod();
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x000BAEBC File Offset: 0x000B90BC
		private void WriteDerivedTypes(StructMapping mapping, bool isTypedReturn, string returnTypeName)
		{
			for (StructMapping structMapping = mapping.DerivedMappings; structMapping != null; structMapping = structMapping.NextDerivedMapping)
			{
				this.ilg.InitElseIf();
				this.WriteQNameEqual("xsiType", structMapping.TypeName, structMapping.Namespace);
				this.ilg.AndIf();
				string methodName = base.ReferenceMapping(structMapping);
				List<Type> list = new List<Type>();
				this.ilg.Ldarg(0);
				if (structMapping.TypeDesc.IsNullable)
				{
					this.ilg.Ldarg("isNullable");
					list.Add(typeof(bool));
				}
				this.ilg.Ldc(false);
				list.Add(typeof(bool));
				MethodBuilder methodBuilder = base.EnsureMethodBuilder(this.typeBuilder, methodName, CodeGenerator.PrivateMethodAttributes, structMapping.TypeDesc.Type, list.ToArray());
				this.ilg.Call(methodBuilder);
				this.ilg.ConvertValue(methodBuilder.ReturnType, this.ilg.ReturnLocal.LocalType);
				this.ilg.Stloc(this.ilg.ReturnLocal);
				this.ilg.Br(this.ilg.ReturnLabel);
				this.WriteDerivedTypes(structMapping, isTypedReturn, returnTypeName);
			}
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x000BAFF8 File Offset: 0x000B91F8
		private void WriteEnumAndArrayTypes()
		{
			TypeScope[] scopes = base.Scopes;
			for (int i = 0; i < scopes.Length; i++)
			{
				foreach (object obj in scopes[i].TypeMappings)
				{
					Mapping mapping = (Mapping)obj;
					if (mapping is EnumMapping)
					{
						EnumMapping enumMapping = (EnumMapping)mapping;
						this.ilg.InitElseIf();
						this.WriteQNameEqual("xsiType", enumMapping.TypeName, enumMapping.Namespace);
						this.ilg.AndIf();
						MethodInfo method = typeof(XmlSerializationReader).GetMethod("get_Reader", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
						MethodInfo method2 = typeof(XmlReader).GetMethod("ReadStartElement", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
						this.ilg.Ldarg(0);
						this.ilg.Call(method);
						this.ilg.Call(method2);
						string methodName = base.ReferenceMapping(enumMapping);
						LocalBuilder localBuilder = this.ilg.DeclareOrGetLocal(typeof(object), "e");
						MethodBuilder methodBuilder = base.EnsureMethodBuilder(this.typeBuilder, methodName, CodeGenerator.PrivateMethodAttributes, enumMapping.TypeDesc.Type, new Type[]
						{
							typeof(string)
						});
						MethodInfo method3 = typeof(XmlSerializationReader).GetMethod("CollapseWhitespace", CodeGenerator.InstanceBindingFlags, null, new Type[]
						{
							typeof(string)
						}, null);
						MethodInfo method4 = typeof(XmlReader).GetMethod("ReadString", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
						this.ilg.Ldarg(0);
						this.ilg.Ldarg(0);
						this.ilg.Ldarg(0);
						this.ilg.Call(method);
						this.ilg.Call(method4);
						this.ilg.Call(method3);
						this.ilg.Call(methodBuilder);
						this.ilg.ConvertValue(methodBuilder.ReturnType, localBuilder.LocalType);
						this.ilg.Stloc(localBuilder);
						MethodInfo method5 = typeof(XmlSerializationReader).GetMethod("ReadEndElement", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
						this.ilg.Ldarg(0);
						this.ilg.Call(method5);
						this.ilg.Ldloc(localBuilder);
						this.ilg.Stloc(this.ilg.ReturnLocal);
						this.ilg.Br(this.ilg.ReturnLabel);
					}
					else if (mapping is ArrayMapping)
					{
						ArrayMapping arrayMapping = (ArrayMapping)mapping;
						if (arrayMapping.TypeDesc.HasDefaultConstructor)
						{
							this.ilg.InitElseIf();
							this.WriteQNameEqual("xsiType", arrayMapping.TypeName, arrayMapping.Namespace);
							this.ilg.AndIf();
							this.ilg.EnterScope();
							MemberMapping memberMapping = new MemberMapping();
							memberMapping.TypeDesc = arrayMapping.TypeDesc;
							memberMapping.Elements = arrayMapping.Elements;
							string text = "a";
							string arrayName = "z";
							XmlSerializationReaderILGen.Member member = new XmlSerializationReaderILGen.Member(this, text, arrayName, 0, memberMapping);
							TypeDesc typeDesc = arrayMapping.TypeDesc;
							LocalBuilder localBuilder2 = this.ilg.DeclareLocal(arrayMapping.TypeDesc.Type, text);
							if (arrayMapping.TypeDesc.IsValueType)
							{
								base.RaCodeGen.ILGenForCreateInstance(this.ilg, typeDesc.Type, false, false);
							}
							else
							{
								this.ilg.Load(null);
							}
							this.ilg.Stloc(localBuilder2);
							this.WriteArray(member.Source, member.ArrayName, arrayMapping, false, false, -1, 0);
							this.ilg.Ldloc(localBuilder2);
							this.ilg.Stloc(this.ilg.ReturnLocal);
							this.ilg.Br(this.ilg.ReturnLabel);
							this.ilg.ExitScope();
						}
					}
				}
			}
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x000BB430 File Offset: 0x000B9630
		private void WriteNullableMethod(NullableMapping nullableMapping)
		{
			string methodName = (string)base.MethodNames[nullableMapping];
			this.ilg = new CodeGenerator(this.typeBuilder);
			this.ilg.BeginMethod(nullableMapping.TypeDesc.Type, base.GetMethodBuilder(methodName), new Type[]
			{
				typeof(bool)
			}, new string[]
			{
				"checkType"
			}, CodeGenerator.PrivateMethodAttributes);
			LocalBuilder localBuilder = this.ilg.DeclareLocal(nullableMapping.TypeDesc.Type, "o");
			this.ilg.LoadAddress(localBuilder);
			this.ilg.InitObj(nullableMapping.TypeDesc.Type);
			MethodInfo method = typeof(XmlSerializationReader).GetMethod("ReadNull", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg.Ldarg(0);
			this.ilg.Call(method);
			this.ilg.If();
			this.ilg.Ldloc(localBuilder);
			this.ilg.Stloc(this.ilg.ReturnLocal);
			this.ilg.Br(this.ilg.ReturnLabel);
			this.ilg.EndIf();
			this.WriteElement("o", null, null, new ElementAccessor
			{
				Mapping = nullableMapping.BaseMapping,
				Any = false,
				IsNullable = nullableMapping.BaseMapping.TypeDesc.IsNullable
			}, null, null, false, false, -1, -1);
			this.ilg.Ldloc(localBuilder);
			this.ilg.Stloc(this.ilg.ReturnLocal);
			this.ilg.Br(this.ilg.ReturnLabel);
			this.ilg.MarkLabel(this.ilg.ReturnLabel);
			this.ilg.Ldloc(this.ilg.ReturnLocal);
			this.ilg.EndMethod();
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x000BB61B File Offset: 0x000B981B
		private void WriteStructMethod(StructMapping structMapping)
		{
			this.WriteLiteralStructMethod(structMapping);
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x000BB624 File Offset: 0x000B9824
		private void WriteLiteralStructMethod(StructMapping structMapping)
		{
			string methodName = (string)base.MethodNames[structMapping];
			string csharpName = structMapping.TypeDesc.CSharpName;
			this.ilg = new CodeGenerator(this.typeBuilder);
			List<Type> list = new List<Type>();
			List<string> list2 = new List<string>();
			if (structMapping.TypeDesc.IsNullable)
			{
				list.Add(typeof(bool));
				list2.Add("isNullable");
			}
			list.Add(typeof(bool));
			list2.Add("checkType");
			this.ilg.BeginMethod(structMapping.TypeDesc.Type, base.GetMethodBuilder(methodName), list.ToArray(), list2.ToArray(), CodeGenerator.PrivateMethodAttributes);
			LocalBuilder localBuilder = this.ilg.DeclareLocal(typeof(XmlQualifiedName), "xsiType");
			LocalBuilder localBuilder2 = this.ilg.DeclareLocal(typeof(bool), "isNull");
			MethodInfo method = typeof(XmlSerializationReader).GetMethod("GetXsiType", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			MethodInfo method2 = typeof(XmlSerializationReader).GetMethod("ReadNull", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			Label label = this.ilg.DefineLabel();
			Label label2 = this.ilg.DefineLabel();
			this.ilg.Ldarg("checkType");
			this.ilg.Brtrue(label);
			this.ilg.Load(null);
			this.ilg.Br_S(label2);
			this.ilg.MarkLabel(label);
			this.ilg.Ldarg(0);
			this.ilg.Call(method);
			this.ilg.MarkLabel(label2);
			this.ilg.Stloc(localBuilder);
			this.ilg.Ldc(false);
			this.ilg.Stloc(localBuilder2);
			if (structMapping.TypeDesc.IsNullable)
			{
				this.ilg.Ldarg("isNullable");
				this.ilg.If();
				this.ilg.Ldarg(0);
				this.ilg.Call(method2);
				this.ilg.Stloc(localBuilder2);
				this.ilg.EndIf();
			}
			this.ilg.Ldarg("checkType");
			this.ilg.If();
			if (structMapping.TypeDesc.IsRoot)
			{
				this.ilg.Ldloc(localBuilder2);
				this.ilg.If();
				this.ilg.Ldloc(localBuilder);
				this.ilg.Load(null);
				this.ilg.If(Cmp.NotEqualTo);
				MethodInfo method3 = typeof(XmlSerializationReader).GetMethod("ReadTypedNull", CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					localBuilder.LocalType
				}, null);
				this.ilg.Ldarg(0);
				this.ilg.Ldloc(localBuilder);
				this.ilg.Call(method3);
				this.ilg.Stloc(this.ilg.ReturnLocal);
				this.ilg.Br(this.ilg.ReturnLabel);
				this.ilg.Else();
				if (structMapping.TypeDesc.IsValueType)
				{
					throw CodeGenerator.NotSupported("Arg_NeverValueType");
				}
				this.ilg.Load(null);
				this.ilg.Stloc(this.ilg.ReturnLocal);
				this.ilg.Br(this.ilg.ReturnLabel);
				this.ilg.EndIf();
				this.ilg.EndIf();
			}
			this.ilg.Ldloc(typeof(XmlQualifiedName), "xsiType");
			this.ilg.Load(null);
			this.ilg.Ceq();
			if (!structMapping.TypeDesc.IsRoot)
			{
				label = this.ilg.DefineLabel();
				label2 = this.ilg.DefineLabel();
				this.ilg.Brtrue(label);
				this.WriteQNameEqual("xsiType", structMapping.TypeName, structMapping.Namespace);
				this.ilg.Br_S(label2);
				this.ilg.MarkLabel(label);
				this.ilg.Ldc(true);
				this.ilg.MarkLabel(label2);
			}
			this.ilg.If();
			if (structMapping.TypeDesc.IsRoot)
			{
				ConstructorInfo constructor = typeof(XmlQualifiedName).GetConstructor(CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(string),
					typeof(string)
				}, null);
				MethodInfo method4 = typeof(XmlSerializationReader).GetMethod("ReadTypedPrimitive", CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(XmlQualifiedName)
				}, null);
				this.ilg.Ldarg(0);
				this.ilg.Ldstr("anyType");
				this.ilg.Ldstr("http://www.w3.org/2001/XMLSchema");
				this.ilg.New(constructor);
				this.ilg.Call(method4);
				this.ilg.Stloc(this.ilg.ReturnLocal);
				this.ilg.Br(this.ilg.ReturnLabel);
			}
			this.WriteDerivedTypes(structMapping, !structMapping.TypeDesc.IsRoot, csharpName);
			if (structMapping.TypeDesc.IsRoot)
			{
				this.WriteEnumAndArrayTypes();
			}
			this.ilg.Else();
			if (structMapping.TypeDesc.IsRoot)
			{
				MethodInfo method5 = typeof(XmlSerializationReader).GetMethod("ReadTypedPrimitive", CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					localBuilder.LocalType
				}, null);
				this.ilg.Ldarg(0);
				this.ilg.Ldloc(localBuilder);
				this.ilg.Call(method5);
				this.ilg.Stloc(this.ilg.ReturnLocal);
				this.ilg.Br(this.ilg.ReturnLabel);
			}
			else
			{
				MethodInfo method6 = typeof(XmlSerializationReader).GetMethod("CreateUnknownTypeException", CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(XmlQualifiedName)
				}, null);
				this.ilg.Ldarg(0);
				this.ilg.Ldloc(localBuilder);
				this.ilg.Call(method6);
				this.ilg.Throw();
			}
			this.ilg.EndIf();
			this.ilg.EndIf();
			if (structMapping.TypeDesc.IsNullable)
			{
				this.ilg.Ldloc(typeof(bool), "isNull");
				this.ilg.If();
				this.ilg.Load(null);
				this.ilg.Stloc(this.ilg.ReturnLocal);
				this.ilg.Br(this.ilg.ReturnLabel);
				this.ilg.EndIf();
			}
			if (structMapping.TypeDesc.IsAbstract)
			{
				MethodInfo method7 = typeof(XmlSerializationReader).GetMethod("CreateAbstractTypeException", CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(string),
					typeof(string)
				}, null);
				this.ilg.Ldarg(0);
				this.ilg.Ldstr(structMapping.TypeName);
				this.ilg.Ldstr(structMapping.Namespace);
				this.ilg.Call(method7);
				this.ilg.Throw();
			}
			else
			{
				if (structMapping.TypeDesc.Type != null && typeof(XmlSchemaObject).IsAssignableFrom(structMapping.TypeDesc.Type))
				{
					MethodInfo method8 = typeof(XmlSerializationReader).GetMethod("set_DecodeName", CodeGenerator.InstanceBindingFlags, null, new Type[]
					{
						typeof(bool)
					}, null);
					this.ilg.Ldarg(0);
					this.ilg.Ldc(false);
					this.ilg.Call(method8);
				}
				this.WriteCreateMapping(structMapping, "o");
				LocalBuilder local = this.ilg.GetLocal("o");
				MemberMapping[] settableMembers = TypeScope.GetSettableMembers(structMapping, this.memberInfos);
				XmlSerializationReaderILGen.Member member = null;
				XmlSerializationReaderILGen.Member member2 = null;
				XmlSerializationReaderILGen.Member member3 = null;
				bool flag = structMapping.HasExplicitSequence();
				ArrayList arrayList = new ArrayList(settableMembers.Length);
				ArrayList arrayList2 = new ArrayList(settableMembers.Length);
				ArrayList arrayList3 = new ArrayList(settableMembers.Length);
				for (int i = 0; i < settableMembers.Length; i++)
				{
					MemberMapping memberMapping = settableMembers[i];
					CodeIdentifier.CheckValidIdentifier(memberMapping.Name);
					string stringForMember = base.RaCodeGen.GetStringForMember("o", memberMapping.Name, structMapping.TypeDesc);
					XmlSerializationReaderILGen.Member member4 = new XmlSerializationReaderILGen.Member(this, stringForMember, "a", i, memberMapping, this.GetChoiceIdentifierSource(memberMapping, "o", structMapping.TypeDesc));
					if (!memberMapping.IsSequence)
					{
						member4.ParamsReadSource = "paramsRead[" + i.ToString(CultureInfo.InvariantCulture) + "]";
					}
					member4.IsNullable = memberMapping.TypeDesc.IsNullable;
					if (memberMapping.CheckSpecified == SpecifiedAccessor.ReadWrite)
					{
						member4.CheckSpecifiedSource = base.RaCodeGen.GetStringForMember("o", memberMapping.Name + "Specified", structMapping.TypeDesc);
					}
					if (memberMapping.Text != null)
					{
						member = member4;
					}
					if (memberMapping.Attribute != null && memberMapping.Attribute.Any)
					{
						member3 = member4;
					}
					if (!flag)
					{
						for (int j = 0; j < memberMapping.Elements.Length; j++)
						{
							if (memberMapping.Elements[j].Any && (memberMapping.Elements[j].Name == null || memberMapping.Elements[j].Name.Length == 0))
							{
								member2 = member4;
								break;
							}
						}
					}
					else if (memberMapping.IsParticle && !memberMapping.IsSequence)
					{
						StructMapping structMapping2;
						structMapping.FindDeclaringMapping(memberMapping, out structMapping2, structMapping.TypeName);
						throw new InvalidOperationException(Res.GetString("There was an error processing type '{0}'. Type member '{1}' declared in '{2}' is missing required '{3}' property. If one class in the class hierarchy uses explicit sequencing feature ({3}), then its base class and all derived classes have to do the same.", new object[]
						{
							structMapping.TypeDesc.FullName,
							memberMapping.Name,
							structMapping2.TypeDesc.FullName,
							"Order"
						}));
					}
					if (memberMapping.Attribute == null && memberMapping.Elements.Length == 1 && memberMapping.Elements[0].Mapping is ArrayMapping)
					{
						arrayList3.Add(new XmlSerializationReaderILGen.Member(this, stringForMember, stringForMember, "a", i, memberMapping, this.GetChoiceIdentifierSource(memberMapping, "o", structMapping.TypeDesc))
						{
							CheckSpecifiedSource = member4.CheckSpecifiedSource
						});
					}
					else
					{
						arrayList3.Add(member4);
					}
					if (memberMapping.TypeDesc.IsArrayLike)
					{
						arrayList.Add(member4);
						if (memberMapping.TypeDesc.IsArrayLike && (memberMapping.Elements.Length != 1 || !(memberMapping.Elements[0].Mapping is ArrayMapping)))
						{
							member4.ParamsReadSource = null;
							if (member4 != member && member4 != member2)
							{
								arrayList2.Add(member4);
							}
						}
						else if (!memberMapping.TypeDesc.IsArray)
						{
							member4.ParamsReadSource = null;
						}
					}
				}
				if (member2 != null)
				{
					arrayList2.Add(member2);
				}
				if (member != null && member != member2)
				{
					arrayList2.Add(member);
				}
				XmlSerializationReaderILGen.Member[] members = (XmlSerializationReaderILGen.Member[])arrayList.ToArray(typeof(XmlSerializationReaderILGen.Member));
				XmlSerializationReaderILGen.Member[] members2 = (XmlSerializationReaderILGen.Member[])arrayList2.ToArray(typeof(XmlSerializationReaderILGen.Member));
				XmlSerializationReaderILGen.Member[] members3 = (XmlSerializationReaderILGen.Member[])arrayList3.ToArray(typeof(XmlSerializationReaderILGen.Member));
				this.WriteMemberBegin(members);
				this.WriteParamsRead(settableMembers.Length);
				this.WriteAttributes(members3, member3, "UnknownNode", local);
				if (member3 != null)
				{
					this.WriteMemberEnd(members);
				}
				MethodInfo method9 = typeof(XmlSerializationReader).GetMethod("get_Reader", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				MethodInfo method10 = typeof(XmlReader).GetMethod("MoveToElement", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				this.ilg.Ldarg(0);
				this.ilg.Call(method9);
				this.ilg.Call(method10);
				this.ilg.Pop();
				MethodInfo method11 = typeof(XmlReader).GetMethod("get_IsEmptyElement", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				this.ilg.Ldarg(0);
				this.ilg.Call(method9);
				this.ilg.Call(method11);
				this.ilg.If();
				MethodInfo method12 = typeof(XmlReader).GetMethod("Skip", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				this.ilg.Ldarg(0);
				this.ilg.Call(method9);
				this.ilg.Call(method12);
				this.WriteMemberEnd(members2);
				this.ilg.Ldloc(local);
				this.ilg.Stloc(this.ilg.ReturnLocal);
				this.ilg.Br(this.ilg.ReturnLabel);
				this.ilg.EndIf();
				MethodInfo method13 = typeof(XmlReader).GetMethod("ReadStartElement", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				this.ilg.Ldarg(0);
				this.ilg.Call(method9);
				this.ilg.Call(method13);
				if (this.IsSequence(members3))
				{
					this.ilg.Ldc(0);
					this.ilg.Stloc(typeof(int), "state");
				}
				int loopIndex = this.WriteWhileNotLoopStart();
				string text = "UnknownNode((object)o, " + this.ExpectedElements(members3) + ");";
				this.WriteMemberElements(members3, text, text, member2, member);
				MethodInfo method14 = typeof(XmlReader).GetMethod("MoveToContent", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				this.ilg.Ldarg(0);
				this.ilg.Call(method9);
				this.ilg.Call(method14);
				this.ilg.Pop();
				this.WriteWhileLoopEnd(loopIndex);
				this.WriteMemberEnd(members2);
				MethodInfo method15 = typeof(XmlSerializationReader).GetMethod("ReadEndElement", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				this.ilg.Ldarg(0);
				this.ilg.Call(method15);
				this.ilg.Ldloc(structMapping.TypeDesc.Type, "o");
				this.ilg.Stloc(this.ilg.ReturnLocal);
			}
			this.ilg.MarkLabel(this.ilg.ReturnLabel);
			this.ilg.Ldloc(this.ilg.ReturnLocal);
			this.ilg.EndMethod();
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x000BC4F0 File Offset: 0x000BA6F0
		private void WriteQNameEqual(string source, string name, string ns)
		{
			this.WriteID(name);
			this.WriteID(ns);
			MethodInfo method = typeof(XmlQualifiedName).GetMethod("get_Name", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			MethodInfo method2 = typeof(XmlQualifiedName).GetMethod("get_Namespace", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			Label label = this.ilg.DefineLabel();
			Label label2 = this.ilg.DefineLabel();
			LocalBuilder local = this.ilg.GetLocal(source);
			this.ilg.Ldloc(local);
			this.ilg.Call(method);
			this.ilg.Ldarg(0);
			this.ilg.LoadMember(this.idNameFields[name ?? string.Empty]);
			this.ilg.Bne(label2);
			this.ilg.Ldloc(local);
			this.ilg.Call(method2);
			this.ilg.Ldarg(0);
			this.ilg.LoadMember(this.idNameFields[ns ?? string.Empty]);
			this.ilg.Ceq();
			this.ilg.Br_S(label);
			this.ilg.MarkLabel(label2);
			this.ilg.Ldc(false);
			this.ilg.MarkLabel(label);
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x000BC646 File Offset: 0x000BA846
		private void WriteXmlNodeEqual(string source, string name, string ns)
		{
			this.WriteXmlNodeEqual(source, name, ns, true);
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x000BC654 File Offset: 0x000BA854
		private void WriteXmlNodeEqual(string source, string name, string ns, bool doAndIf)
		{
			bool flag = string.IsNullOrEmpty(name);
			if (!flag)
			{
				this.WriteID(name);
			}
			this.WriteID(ns);
			MethodInfo method = typeof(XmlSerializationReader).GetMethod("get_" + source, CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			MethodInfo method2 = typeof(XmlReader).GetMethod("get_LocalName", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			MethodInfo method3 = typeof(XmlReader).GetMethod("get_NamespaceURI", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			Label label = this.ilg.DefineLabel();
			Label label2 = this.ilg.DefineLabel();
			if (!flag)
			{
				this.ilg.Ldarg(0);
				this.ilg.Call(method);
				this.ilg.Call(method2);
				this.ilg.Ldarg(0);
				this.ilg.LoadMember(this.idNameFields[name ?? string.Empty]);
				this.ilg.Bne(label);
			}
			this.ilg.Ldarg(0);
			this.ilg.Call(method);
			this.ilg.Call(method3);
			this.ilg.Ldarg(0);
			this.ilg.LoadMember(this.idNameFields[ns ?? string.Empty]);
			this.ilg.Ceq();
			if (!flag)
			{
				this.ilg.Br_S(label2);
				this.ilg.MarkLabel(label);
				this.ilg.Ldc(false);
				this.ilg.MarkLabel(label2);
			}
			if (doAndIf)
			{
				this.ilg.AndIf();
			}
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x000BC7FC File Offset: 0x000BA9FC
		private void WriteID(string name)
		{
			if (name == null)
			{
				name = "";
			}
			if ((string)this.idNames[name] == null)
			{
				string text = this.NextIdName(name);
				this.idNames.Add(name, text);
				this.idNameFields.Add(name, this.typeBuilder.DefineField(text, typeof(string), FieldAttributes.Private));
			}
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x000BC860 File Offset: 0x000BAA60
		private void WriteAttributes(XmlSerializationReaderILGen.Member[] members, XmlSerializationReaderILGen.Member anyAttribute, string elseCall, LocalBuilder firstParam)
		{
			int num = 0;
			XmlSerializationReaderILGen.Member member = null;
			ArrayList arrayList = new ArrayList();
			MethodInfo method = typeof(XmlSerializationReader).GetMethod("get_Reader", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			MethodInfo method2 = typeof(XmlReader).GetMethod("MoveToNextAttribute", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg.WhileBegin();
			foreach (XmlSerializationReaderILGen.Member member2 in members)
			{
				if (member2.Mapping.Xmlns != null)
				{
					member = member2;
				}
				else if (!member2.Mapping.Ignore)
				{
					AttributeAccessor attribute = member2.Mapping.Attribute;
					if (attribute != null && !attribute.Any)
					{
						arrayList.Add(attribute);
						if (num++ > 0)
						{
							this.ilg.InitElseIf();
						}
						else
						{
							this.ilg.InitIf();
						}
						if (member2.ParamsReadSource != null)
						{
							this.ILGenParamsReadSource(member2.ParamsReadSource);
							this.ilg.Ldc(false);
							this.ilg.AndIf(Cmp.EqualTo);
						}
						if (attribute.IsSpecialXmlNamespace)
						{
							this.WriteXmlNodeEqual("Reader", attribute.Name, "http://www.w3.org/XML/1998/namespace");
						}
						else
						{
							this.WriteXmlNodeEqual("Reader", attribute.Name, (attribute.Form == XmlSchemaForm.Qualified) ? attribute.Namespace : "");
						}
						this.WriteAttribute(member2);
					}
				}
			}
			if (num > 0)
			{
				this.ilg.InitElseIf();
			}
			else
			{
				this.ilg.InitIf();
			}
			if (member != null)
			{
				MethodInfo method3 = typeof(XmlSerializationReader).GetMethod("IsXmlnsAttribute", CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(string)
				}, null);
				MethodInfo method4 = typeof(XmlReader).GetMethod("get_Name", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				MethodInfo method5 = typeof(XmlReader).GetMethod("get_LocalName", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				MethodInfo method6 = typeof(XmlReader).GetMethod("get_Value", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				this.ilg.Ldarg(0);
				this.ilg.Ldarg(0);
				this.ilg.Call(method);
				this.ilg.Call(method4);
				this.ilg.Call(method3);
				this.ilg.Ldc(true);
				this.ilg.AndIf(Cmp.EqualTo);
				base.ILGenLoad(member.Source);
				this.ilg.Load(null);
				this.ilg.If(Cmp.EqualTo);
				this.WriteSourceBegin(member.Source);
				ConstructorInfo constructor = member.Mapping.TypeDesc.Type.GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				this.ilg.New(constructor);
				this.WriteSourceEnd(member.Source, member.Mapping.TypeDesc.Type);
				this.ilg.EndIf();
				Label label = this.ilg.DefineLabel();
				Label label2 = this.ilg.DefineLabel();
				MethodInfo method7 = member.Mapping.TypeDesc.Type.GetMethod("Add", CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(string),
					typeof(string)
				}, null);
				MethodInfo method8 = typeof(string).GetMethod("get_Length", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				base.ILGenLoad(member.ArraySource, member.Mapping.TypeDesc.Type);
				this.ilg.Ldarg(0);
				this.ilg.Call(method);
				this.ilg.Call(method4);
				this.ilg.Call(method8);
				this.ilg.Ldc(5);
				this.ilg.Beq(label);
				this.ilg.Ldarg(0);
				this.ilg.Call(method);
				this.ilg.Call(method5);
				this.ilg.Br(label2);
				this.ilg.MarkLabel(label);
				this.ilg.Ldstr(string.Empty);
				this.ilg.MarkLabel(label2);
				this.ilg.Ldarg(0);
				this.ilg.Call(method);
				this.ilg.Call(method6);
				this.ilg.Call(method7);
				this.ilg.Else();
			}
			else
			{
				MethodInfo method9 = typeof(XmlSerializationReader).GetMethod("IsXmlnsAttribute", CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(string)
				}, null);
				MethodInfo method10 = typeof(XmlReader).GetMethod("get_Name", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				this.ilg.Ldarg(0);
				this.ilg.Ldarg(0);
				this.ilg.Call(method);
				this.ilg.Call(method10);
				this.ilg.Call(method9);
				this.ilg.Ldc(false);
				this.ilg.AndIf(Cmp.EqualTo);
			}
			if (anyAttribute != null)
			{
				LocalBuilder localBuilder = this.ilg.DeclareOrGetLocal(typeof(XmlAttribute), "attr");
				MethodInfo method11 = typeof(XmlSerializationReader).GetMethod("get_Document", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				MethodInfo method12 = typeof(XmlDocument).GetMethod("ReadNode", CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(XmlReader)
				}, null);
				this.ilg.Ldarg(0);
				this.ilg.Call(method11);
				this.ilg.Ldarg(0);
				this.ilg.Call(method);
				this.ilg.Call(method12);
				this.ilg.ConvertValue(method12.ReturnType, localBuilder.LocalType);
				this.ilg.Stloc(localBuilder);
				MethodInfo method13 = typeof(XmlSerializationReader).GetMethod("ParseWsdlArrayType", CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					localBuilder.LocalType
				}, null);
				this.ilg.Ldarg(0);
				this.ilg.Ldloc(localBuilder);
				this.ilg.Call(method13);
				this.WriteAttribute(anyAttribute);
			}
			else
			{
				List<Type> list = new List<Type>();
				this.ilg.Ldarg(0);
				list.Add(typeof(object));
				this.ilg.Ldloc(firstParam);
				this.ilg.ConvertValue(firstParam.LocalType, typeof(object));
				if (arrayList.Count > 0)
				{
					string text = "";
					for (int j = 0; j < arrayList.Count; j++)
					{
						AttributeAccessor attributeAccessor = (AttributeAccessor)arrayList[j];
						if (j > 0)
						{
							text += ", ";
						}
						text += (attributeAccessor.IsSpecialXmlNamespace ? "http://www.w3.org/XML/1998/namespace" : (((attributeAccessor.Form == XmlSchemaForm.Qualified) ? attributeAccessor.Namespace : "") + ":" + attributeAccessor.Name));
					}
					list.Add(typeof(string));
					this.ilg.Ldstr(text);
				}
				MethodInfo method14 = typeof(XmlSerializationReader).GetMethod(elseCall, CodeGenerator.InstanceBindingFlags, null, list.ToArray(), null);
				this.ilg.Call(method14);
			}
			this.ilg.EndIf();
			this.ilg.WhileBeginCondition();
			this.ilg.Ldarg(0);
			this.ilg.Call(method);
			this.ilg.Call(method2);
			this.ilg.WhileEndCondition();
			this.ilg.WhileEnd();
		}

		// Token: 0x06001E88 RID: 7816 RVA: 0x000BD044 File Offset: 0x000BB244
		private void WriteAttribute(XmlSerializationReaderILGen.Member member)
		{
			AttributeAccessor attribute = member.Mapping.Attribute;
			if (attribute.Mapping is SpecialMapping)
			{
				SpecialMapping specialMapping = (SpecialMapping)attribute.Mapping;
				if (specialMapping.TypeDesc.Kind == TypeKind.Attribute)
				{
					this.WriteSourceBegin(member.ArraySource);
					this.ilg.Ldloc("attr");
					this.WriteSourceEnd(member.ArraySource, member.Mapping.TypeDesc.IsArrayLike ? member.Mapping.TypeDesc.ArrayElementTypeDesc.Type : member.Mapping.TypeDesc.Type);
				}
				else
				{
					if (!specialMapping.TypeDesc.CanBeAttributeValue)
					{
						throw new InvalidOperationException(Res.GetString("Internal error."));
					}
					LocalBuilder local = this.ilg.GetLocal("attr");
					this.ilg.Ldloc(local);
					if (local.LocalType == typeof(XmlAttribute))
					{
						this.ilg.Load(null);
						this.ilg.Cne();
					}
					else
					{
						this.ilg.IsInst(typeof(XmlAttribute));
					}
					this.ilg.If();
					this.WriteSourceBegin(member.ArraySource);
					this.ilg.Ldloc(local);
					this.ilg.ConvertValue(local.LocalType, typeof(XmlAttribute));
					this.WriteSourceEnd(member.ArraySource, member.Mapping.TypeDesc.IsArrayLike ? member.Mapping.TypeDesc.ArrayElementTypeDesc.Type : member.Mapping.TypeDesc.Type);
					this.ilg.EndIf();
				}
			}
			else if (attribute.IsList)
			{
				LocalBuilder localBuilder = this.ilg.DeclareOrGetLocal(typeof(string), "listValues");
				LocalBuilder localBuilder2 = this.ilg.DeclareOrGetLocal(typeof(string[]), "vals");
				MethodInfo method = typeof(string).GetMethod("Split", CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(char[])
				}, null);
				MethodInfo method2 = typeof(XmlSerializationReader).GetMethod("get_Reader", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				MethodInfo method3 = typeof(XmlReader).GetMethod("get_Value", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				this.ilg.Ldarg(0);
				this.ilg.Call(method2);
				this.ilg.Call(method3);
				this.ilg.Stloc(localBuilder);
				this.ilg.Ldloc(localBuilder);
				this.ilg.Load(null);
				this.ilg.Call(method);
				this.ilg.Stloc(localBuilder2);
				LocalBuilder local2 = this.ilg.DeclareOrGetLocal(typeof(int), "i");
				this.ilg.For(local2, 0, localBuilder2);
				string arraySource = this.GetArraySource(member.Mapping.TypeDesc, member.ArrayName);
				this.WriteSourceBegin(arraySource);
				this.WritePrimitive(attribute.Mapping, "vals[i]");
				this.WriteSourceEnd(arraySource, member.Mapping.TypeDesc.ArrayElementTypeDesc.Type);
				this.ilg.EndFor();
			}
			else
			{
				this.WriteSourceBegin(member.ArraySource);
				this.WritePrimitive(attribute.Mapping, attribute.IsList ? "vals[i]" : "Reader.Value");
				this.WriteSourceEnd(member.ArraySource, member.Mapping.TypeDesc.IsArrayLike ? member.Mapping.TypeDesc.ArrayElementTypeDesc.Type : member.Mapping.TypeDesc.Type);
			}
			if (member.Mapping.CheckSpecified == SpecifiedAccessor.ReadWrite && member.CheckSpecifiedSource != null && member.CheckSpecifiedSource.Length > 0)
			{
				this.ILGenSet(member.CheckSpecifiedSource, true);
			}
			if (member.ParamsReadSource != null)
			{
				this.ILGenParamsReadSource(member.ParamsReadSource, true);
			}
		}

		// Token: 0x06001E89 RID: 7817 RVA: 0x000BD468 File Offset: 0x000BB668
		private void WriteMemberBegin(XmlSerializationReaderILGen.Member[] members)
		{
			foreach (XmlSerializationReaderILGen.Member member in members)
			{
				if (member.IsArrayLike)
				{
					string arrayName = member.ArrayName;
					string name = "c" + arrayName;
					TypeDesc typeDesc = member.Mapping.TypeDesc;
					if (member.Mapping.TypeDesc.IsArray)
					{
						this.WriteArrayLocalDecl(typeDesc.CSharpName, arrayName, "null", typeDesc);
						this.ilg.Ldc(0);
						this.ilg.Stloc(typeof(int), name);
						if (member.Mapping.ChoiceIdentifier != null)
						{
							this.WriteArrayLocalDecl(member.Mapping.ChoiceIdentifier.Mapping.TypeDesc.CSharpName + "[]", member.ChoiceArrayName, "null", member.Mapping.ChoiceIdentifier.Mapping.TypeDesc);
							this.ilg.Ldc(0);
							this.ilg.Stloc(typeof(int), "c" + member.ChoiceArrayName);
						}
					}
					else if (member.Source[member.Source.Length - 1] == '(' || member.Source[member.Source.Length - 1] == '{')
					{
						this.WriteCreateInstance(arrayName, typeDesc.CannotNew, typeDesc.Type);
						this.WriteSourceBegin(member.Source);
						this.ilg.Ldloc(this.ilg.GetLocal(arrayName));
						this.WriteSourceEnd(member.Source, typeDesc.Type);
					}
					else
					{
						if (member.IsList && !member.Mapping.ReadOnly && member.Mapping.TypeDesc.IsNullable)
						{
							base.ILGenLoad(member.Source, typeof(object));
							this.ilg.Load(null);
							this.ilg.If(Cmp.EqualTo);
							if (!member.Mapping.TypeDesc.HasDefaultConstructor)
							{
								MethodInfo method = typeof(XmlSerializationReader).GetMethod("CreateReadOnlyCollectionException", CodeGenerator.InstanceBindingFlags, null, new Type[]
								{
									typeof(string)
								}, null);
								this.ilg.Ldarg(0);
								this.ilg.Ldstr(member.Mapping.TypeDesc.CSharpName);
								this.ilg.Call(method);
								this.ilg.Throw();
							}
							else
							{
								this.WriteSourceBegin(member.Source);
								base.RaCodeGen.ILGenForCreateInstance(this.ilg, member.Mapping.TypeDesc.Type, typeDesc.CannotNew, true);
								this.WriteSourceEnd(member.Source, member.Mapping.TypeDesc.Type);
							}
							this.ilg.EndIf();
						}
						this.WriteLocalDecl(arrayName, new SourceInfo(member.Source, member.Source, member.Mapping.MemberInfo, member.Mapping.TypeDesc.Type, this.ilg));
					}
				}
			}
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x000BD794 File Offset: 0x000BB994
		private string ExpectedElements(XmlSerializationReaderILGen.Member[] members)
		{
			if (this.IsSequence(members))
			{
				return "null";
			}
			string text = string.Empty;
			bool flag = true;
			foreach (XmlSerializationReaderILGen.Member member in members)
			{
				if (member.Mapping.Xmlns == null && !member.Mapping.Ignore && !member.Mapping.IsText && !member.Mapping.IsAttribute)
				{
					foreach (ElementAccessor elementAccessor in member.Mapping.Elements)
					{
						string str = (elementAccessor.Form == XmlSchemaForm.Qualified) ? elementAccessor.Namespace : "";
						if (!elementAccessor.Any || (elementAccessor.Name != null && elementAccessor.Name.Length != 0))
						{
							if (!flag)
							{
								text += ", ";
							}
							text = text + str + ":" + elementAccessor.Name;
							flag = false;
						}
					}
				}
			}
			return ReflectionAwareILGen.GetQuotedCSharpString(null, text);
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x000BD8A0 File Offset: 0x000BBAA0
		private void WriteMemberElements(XmlSerializationReaderILGen.Member[] members, string elementElseString, string elseString, XmlSerializationReaderILGen.Member anyElement, XmlSerializationReaderILGen.Member anyText)
		{
			if (anyText != null)
			{
				this.ilg.Load(null);
				this.ilg.Stloc(typeof(string), "tmp");
			}
			MethodInfo method = typeof(XmlReader).GetMethod("get_NodeType", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			MethodInfo method2 = typeof(XmlSerializationReader).GetMethod("get_Reader", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			int intVar = 1;
			this.ilg.Ldarg(0);
			this.ilg.Call(method2);
			this.ilg.Call(method);
			this.ilg.Ldc(intVar);
			this.ilg.If(Cmp.EqualTo);
			this.WriteMemberElementsIf(members, anyElement, elementElseString);
			if (anyText != null)
			{
				this.WriteMemberText(anyText, elseString);
			}
			this.ilg.Else();
			this.ILGenElseString(elseString);
			this.ilg.EndIf();
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x000BD98C File Offset: 0x000BBB8C
		private void WriteMemberText(XmlSerializationReaderILGen.Member anyText, string elseString)
		{
			this.ilg.InitElseIf();
			Label label = this.ilg.DefineLabel();
			Label label2 = this.ilg.DefineLabel();
			MethodInfo method = typeof(XmlSerializationReader).GetMethod("get_Reader", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			MethodInfo method2 = typeof(XmlReader).GetMethod("get_NodeType", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg.Ldarg(0);
			this.ilg.Call(method);
			this.ilg.Call(method2);
			this.ilg.Ldc(XmlNodeType.Text);
			this.ilg.Ceq();
			this.ilg.Brtrue(label);
			this.ilg.Ldarg(0);
			this.ilg.Call(method);
			this.ilg.Call(method2);
			this.ilg.Ldc(XmlNodeType.CDATA);
			this.ilg.Ceq();
			this.ilg.Brtrue(label);
			this.ilg.Ldarg(0);
			this.ilg.Call(method);
			this.ilg.Call(method2);
			this.ilg.Ldc(XmlNodeType.Whitespace);
			this.ilg.Ceq();
			this.ilg.Brtrue(label);
			this.ilg.Ldarg(0);
			this.ilg.Call(method);
			this.ilg.Call(method2);
			this.ilg.Ldc(XmlNodeType.SignificantWhitespace);
			this.ilg.Ceq();
			this.ilg.Br(label2);
			this.ilg.MarkLabel(label);
			this.ilg.Ldc(true);
			this.ilg.MarkLabel(label2);
			this.ilg.AndIf();
			if (anyText != null)
			{
				this.WriteText(anyText);
			}
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x000BDB6C File Offset: 0x000BBD6C
		private void WriteText(XmlSerializationReaderILGen.Member member)
		{
			TextAccessor text = member.Mapping.Text;
			if (!(text.Mapping is SpecialMapping))
			{
				if (member.IsArrayLike)
				{
					this.WriteSourceBegin(member.ArraySource);
					if (text.Mapping.TypeDesc.CollapseWhitespace)
					{
						this.ilg.Ldarg(0);
					}
					MethodInfo method = typeof(XmlSerializationReader).GetMethod("get_Reader", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
					MethodInfo method2 = typeof(XmlReader).GetMethod("ReadString", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
					this.ilg.Ldarg(0);
					this.ilg.Call(method);
					this.ilg.Call(method2);
					if (text.Mapping.TypeDesc.CollapseWhitespace)
					{
						MethodInfo method3 = typeof(XmlSerializationReader).GetMethod("CollapseWhitespace", CodeGenerator.InstanceBindingFlags, null, new Type[]
						{
							typeof(string)
						}, null);
						this.ilg.Call(method3);
					}
				}
				else if (text.Mapping.TypeDesc == base.StringTypeDesc || text.Mapping.TypeDesc.FormatterName == "String")
				{
					LocalBuilder local = this.ilg.GetLocal("tmp");
					MethodInfo method4 = typeof(XmlSerializationReader).GetMethod("ReadString", CodeGenerator.InstanceBindingFlags, null, new Type[]
					{
						typeof(string),
						typeof(bool)
					}, null);
					this.ilg.Ldarg(0);
					this.ilg.Ldloc(local);
					this.ilg.Ldc(text.Mapping.TypeDesc.CollapseWhitespace);
					this.ilg.Call(method4);
					this.ilg.Stloc(local);
					this.WriteSourceBegin(member.ArraySource);
					this.ilg.Ldloc(local);
				}
				else
				{
					this.WriteSourceBegin(member.ArraySource);
					this.WritePrimitive(text.Mapping, "Reader.ReadString()");
				}
				this.WriteSourceEnd(member.ArraySource, text.Mapping.TypeDesc.Type);
				return;
			}
			SpecialMapping specialMapping = (SpecialMapping)text.Mapping;
			this.WriteSourceBeginTyped(member.ArraySource, specialMapping.TypeDesc);
			if (specialMapping.TypeDesc.Kind == TypeKind.Node)
			{
				MethodInfo method5 = typeof(XmlSerializationReader).GetMethod("get_Reader", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				MethodInfo method6 = typeof(XmlReader).GetMethod("ReadString", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				MethodInfo method7 = typeof(XmlSerializationReader).GetMethod("get_Document", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				MethodInfo method8 = typeof(XmlDocument).GetMethod("CreateTextNode", CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(string)
				}, null);
				this.ilg.Ldarg(0);
				this.ilg.Call(method7);
				this.ilg.Ldarg(0);
				this.ilg.Call(method5);
				this.ilg.Call(method6);
				this.ilg.Call(method8);
				this.WriteSourceEnd(member.ArraySource, specialMapping.TypeDesc.Type);
				return;
			}
			throw new InvalidOperationException(Res.GetString("Internal error."));
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x000BDEE8 File Offset: 0x000BC0E8
		private void WriteMemberElementsElse(XmlSerializationReaderILGen.Member anyElement, string elementElseString)
		{
			if (anyElement != null)
			{
				ElementAccessor[] elements = anyElement.Mapping.Elements;
				for (int i = 0; i < elements.Length; i++)
				{
					ElementAccessor elementAccessor = elements[i];
					if (elementAccessor.Any && elementAccessor.Name.Length == 0)
					{
						this.WriteElement(anyElement.ArraySource, anyElement.ArrayName, anyElement.ChoiceArraySource, elementAccessor, anyElement.Mapping.ChoiceIdentifier, (anyElement.Mapping.CheckSpecified == SpecifiedAccessor.ReadWrite) ? anyElement.CheckSpecifiedSource : null, false, false, -1, i);
						return;
					}
				}
				return;
			}
			this.ILGenElementElseString(elementElseString);
		}

		// Token: 0x06001E8F RID: 7823 RVA: 0x000BDF74 File Offset: 0x000BC174
		private bool IsSequence(XmlSerializationReaderILGen.Member[] members)
		{
			for (int i = 0; i < members.Length; i++)
			{
				if (members[i].Mapping.IsParticle && members[i].Mapping.IsSequence)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x000BDFB0 File Offset: 0x000BC1B0
		private void WriteMemberElementsIf(XmlSerializationReaderILGen.Member[] members, XmlSerializationReaderILGen.Member anyElement, string elementElseString)
		{
			int num = 0;
			bool flag = this.IsSequence(members);
			int num2 = 0;
			foreach (XmlSerializationReaderILGen.Member member in members)
			{
				if (member.Mapping.Xmlns == null && !member.Mapping.Ignore && (!flag || (!member.Mapping.IsText && !member.Mapping.IsAttribute)))
				{
					bool flag2 = true;
					ChoiceIdentifierAccessor choiceIdentifier = member.Mapping.ChoiceIdentifier;
					ElementAccessor[] elements = member.Mapping.Elements;
					for (int j = 0; j < elements.Length; j++)
					{
						ElementAccessor elementAccessor = elements[j];
						string ns = (elementAccessor.Form == XmlSchemaForm.Qualified) ? elementAccessor.Namespace : "";
						if (flag || !elementAccessor.Any || (elementAccessor.Name != null && elementAccessor.Name.Length != 0))
						{
							if (!flag2 || (!flag && num > 0))
							{
								this.ilg.InitElseIf();
							}
							else if (flag)
							{
								if (num2 > 0)
								{
									this.ilg.InitElseIf();
								}
								else
								{
									this.ilg.InitIf();
								}
								this.ilg.Ldloc("state");
								this.ilg.Ldc(num2);
								this.ilg.AndIf(Cmp.EqualTo);
								this.ilg.InitIf();
							}
							else
							{
								this.ilg.InitIf();
							}
							num++;
							flag2 = false;
							if (member.ParamsReadSource != null)
							{
								this.ILGenParamsReadSource(member.ParamsReadSource);
								this.ilg.Ldc(false);
								this.ilg.AndIf(Cmp.EqualTo);
							}
							Label label = this.ilg.DefineLabel();
							Label label2 = this.ilg.DefineLabel();
							if (member.Mapping.IsReturnValue)
							{
								MethodInfo method = typeof(XmlSerializationReader).GetMethod("get_IsReturnValue", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
								this.ilg.Ldarg(0);
								this.ilg.Call(method);
								this.ilg.Brtrue(label);
							}
							if (flag && elementAccessor.Any && elementAccessor.AnyNamespaces == null)
							{
								this.ilg.Ldc(true);
							}
							else
							{
								this.WriteXmlNodeEqual("Reader", elementAccessor.Name, ns, false);
							}
							if (member.Mapping.IsReturnValue)
							{
								this.ilg.Br_S(label2);
								this.ilg.MarkLabel(label);
								this.ilg.Ldc(true);
								this.ilg.MarkLabel(label2);
							}
							this.ilg.AndIf();
							this.WriteElement(member.ArraySource, member.ArrayName, member.ChoiceArraySource, elementAccessor, choiceIdentifier, (member.Mapping.CheckSpecified == SpecifiedAccessor.ReadWrite) ? member.CheckSpecifiedSource : null, member.IsList && member.Mapping.TypeDesc.IsNullable, member.Mapping.ReadOnly, member.FixupIndex, j);
							if (member.Mapping.IsReturnValue)
							{
								MethodInfo method2 = typeof(XmlSerializationReader).GetMethod("set_IsReturnValue", CodeGenerator.InstanceBindingFlags, null, new Type[]
								{
									typeof(bool)
								}, null);
								this.ilg.Ldarg(0);
								this.ilg.Ldc(false);
								this.ilg.Call(method2);
							}
							if (member.ParamsReadSource != null)
							{
								this.ILGenParamsReadSource(member.ParamsReadSource, true);
							}
						}
					}
					if (flag)
					{
						if (member.IsArrayLike)
						{
							this.ilg.Else();
						}
						else
						{
							this.ilg.EndIf();
						}
						num2++;
						this.ilg.Ldc(num2);
						this.ilg.Stloc(this.ilg.GetLocal("state"));
						if (member.IsArrayLike)
						{
							this.ilg.EndIf();
						}
					}
				}
			}
			if (num > 0)
			{
				this.ilg.Else();
			}
			this.WriteMemberElementsElse(anyElement, elementElseString);
			if (num > 0)
			{
				this.ilg.EndIf();
			}
		}

		// Token: 0x06001E91 RID: 7825 RVA: 0x000BE3B8 File Offset: 0x000BC5B8
		private string GetArraySource(TypeDesc typeDesc, string arrayName)
		{
			return this.GetArraySource(typeDesc, arrayName, false);
		}

		// Token: 0x06001E92 RID: 7826 RVA: 0x000BE3C4 File Offset: 0x000BC5C4
		private string GetArraySource(TypeDesc typeDesc, string arrayName, bool multiRef)
		{
			string text = "c" + arrayName;
			string text2 = "";
			if (multiRef)
			{
				text2 = "soap = (System.Object[])EnsureArrayIndex(soap, " + text + "+2, typeof(System.Object)); ";
			}
			if (typeDesc.IsArray)
			{
				string csharpName = typeDesc.ArrayElementTypeDesc.CSharpName;
				string text3 = "(" + csharpName + "[])";
				text2 = string.Concat(new string[]
				{
					text2,
					arrayName,
					" = ",
					text3,
					"EnsureArrayIndex(",
					arrayName,
					", ",
					text,
					", ",
					base.RaCodeGen.GetStringForTypeof(csharpName),
					");"
				});
				string stringForArrayMember = base.RaCodeGen.GetStringForArrayMember(arrayName, text + "++", typeDesc);
				if (multiRef)
				{
					text2 = text2 + " soap[1] = " + arrayName + ";";
					text2 = string.Concat(new string[]
					{
						text2,
						" if (ReadReference(out soap[",
						text,
						"+2])) ",
						stringForArrayMember,
						" = null; else "
					});
				}
				return text2 + stringForArrayMember;
			}
			return base.RaCodeGen.GetStringForMethod(arrayName, typeDesc.CSharpName, "Add");
		}

		// Token: 0x06001E93 RID: 7827 RVA: 0x000BE4FD File Offset: 0x000BC6FD
		private void WriteMemberEnd(XmlSerializationReaderILGen.Member[] members)
		{
			this.WriteMemberEnd(members, false);
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x000BE508 File Offset: 0x000BC708
		private void WriteMemberEnd(XmlSerializationReaderILGen.Member[] members, bool soapRefs)
		{
			foreach (XmlSerializationReaderILGen.Member member in members)
			{
				if (member.IsArrayLike)
				{
					TypeDesc typeDesc = member.Mapping.TypeDesc;
					if (typeDesc.IsArray)
					{
						this.WriteSourceBegin(member.Source);
						string text = member.ArrayName;
						string name = "c" + text;
						MethodInfo method = typeof(XmlSerializationReader).GetMethod("ShrinkArray", CodeGenerator.InstanceBindingFlags, null, new Type[]
						{
							typeof(Array),
							typeof(int),
							typeof(Type),
							typeof(bool)
						}, null);
						this.ilg.Ldarg(0);
						this.ilg.Ldloc(this.ilg.GetLocal(text));
						this.ilg.Ldloc(this.ilg.GetLocal(name));
						this.ilg.Ldc(typeDesc.ArrayElementTypeDesc.Type);
						this.ilg.Ldc(member.IsNullable);
						this.ilg.Call(method);
						this.ilg.ConvertValue(method.ReturnType, typeDesc.Type);
						this.WriteSourceEnd(member.Source, typeDesc.Type);
						if (member.Mapping.ChoiceIdentifier != null)
						{
							this.WriteSourceBegin(member.ChoiceSource);
							text = member.ChoiceArrayName;
							name = "c" + text;
							this.ilg.Ldarg(0);
							this.ilg.Ldloc(this.ilg.GetLocal(text));
							this.ilg.Ldloc(this.ilg.GetLocal(name));
							this.ilg.Ldc(member.Mapping.ChoiceIdentifier.Mapping.TypeDesc.Type);
							this.ilg.Ldc(member.IsNullable);
							this.ilg.Call(method);
							this.ilg.ConvertValue(method.ReturnType, member.Mapping.ChoiceIdentifier.Mapping.TypeDesc.Type.MakeArrayType());
							this.WriteSourceEnd(member.ChoiceSource, member.Mapping.ChoiceIdentifier.Mapping.TypeDesc.Type.MakeArrayType());
						}
					}
					else if (typeDesc.IsValueType)
					{
						LocalBuilder local = this.ilg.GetLocal(member.ArrayName);
						this.WriteSourceBegin(member.Source);
						this.ilg.Ldloc(local);
						this.WriteSourceEnd(member.Source, local.LocalType);
					}
				}
			}
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x000BE7B2 File Offset: 0x000BC9B2
		private void WriteSourceBeginTyped(string source, TypeDesc typeDesc)
		{
			this.WriteSourceBegin(source);
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x000BE7BC File Offset: 0x000BC9BC
		private void WriteSourceBegin(string source)
		{
			object obj;
			if (this.ilg.TryGetVariable(source, out obj))
			{
				if (CodeGenerator.IsNullableGenericType(this.ilg.GetVariableType(obj)))
				{
					this.ilg.LoadAddress(obj);
				}
				return;
			}
			if (source.StartsWith("o.@", StringComparison.Ordinal))
			{
				this.ilg.LdlocAddress(this.ilg.GetLocal("o"));
				return;
			}
			Match match = XmlSerializationILGen.NewRegex("(?<locA1>[^ ]+) = .+EnsureArrayIndex[(](?<locA2>[^,]+), (?<locI1>[^,]+),[^;]+;(?<locA3>[^[]+)[[](?<locI2>[^+]+)[+][+][]]").Match(source);
			if (match.Success)
			{
				LocalBuilder local = this.ilg.GetLocal(match.Groups["locA1"].Value);
				LocalBuilder local2 = this.ilg.GetLocal(match.Groups["locI1"].Value);
				Type elementType = local.LocalType.GetElementType();
				MethodInfo method = typeof(XmlSerializationReader).GetMethod("EnsureArrayIndex", CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(Array),
					typeof(int),
					typeof(Type)
				}, null);
				this.ilg.Ldarg(0);
				this.ilg.Ldloc(local);
				this.ilg.Ldloc(local2);
				this.ilg.Ldc(elementType);
				this.ilg.Call(method);
				this.ilg.Castclass(local.LocalType);
				this.ilg.Stloc(local);
				this.ilg.Ldloc(local);
				this.ilg.Ldloc(local2);
				this.ilg.Dup();
				this.ilg.Ldc(1);
				this.ilg.Add();
				this.ilg.Stloc(local2);
				if (CodeGenerator.IsNullableGenericType(elementType) || elementType.IsValueType)
				{
					this.ilg.Ldelema(elementType);
				}
				return;
			}
			if (source.EndsWith(".Add(", StringComparison.Ordinal))
			{
				int length = source.LastIndexOf(".Add(", StringComparison.Ordinal);
				LocalBuilder local3 = this.ilg.GetLocal(source.Substring(0, length));
				this.ilg.LdlocAddress(local3);
				return;
			}
			match = XmlSerializationILGen.NewRegex("(?<a>[^[]+)[[](?<ia>.+)[]]").Match(source);
			if (match.Success)
			{
				this.ilg.Load(this.ilg.GetVariable(match.Groups["a"].Value));
				this.ilg.Load(this.ilg.GetVariable(match.Groups["ia"].Value));
				return;
			}
			throw CodeGenerator.NotSupported("Unexpected: " + source);
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x000BEA59 File Offset: 0x000BCC59
		private void WriteSourceEnd(string source, Type elementType)
		{
			this.WriteSourceEnd(source, elementType, elementType);
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x000BEA64 File Offset: 0x000BCC64
		private void WriteSourceEnd(string source, Type elementType, Type stackType)
		{
			object obj;
			if (this.ilg.TryGetVariable(source, out obj))
			{
				Type variableType = this.ilg.GetVariableType(obj);
				if (CodeGenerator.IsNullableGenericType(variableType))
				{
					this.ilg.Call(variableType.GetConstructor(variableType.GetGenericArguments()));
					return;
				}
				this.ilg.ConvertValue(stackType, elementType);
				this.ilg.ConvertValue(elementType, variableType);
				this.ilg.Stloc((LocalBuilder)obj);
				return;
			}
			else
			{
				if (source.StartsWith("o.@", StringComparison.Ordinal))
				{
					MemberInfo memberInfo = this.memberInfos[source.Substring(3)];
					this.ilg.ConvertValue(stackType, (memberInfo.MemberType == MemberTypes.Field) ? ((FieldInfo)memberInfo).FieldType : ((PropertyInfo)memberInfo).PropertyType);
					this.ilg.StoreMember(memberInfo);
					return;
				}
				Match match = XmlSerializationILGen.NewRegex("(?<locA1>[^ ]+) = .+EnsureArrayIndex[(](?<locA2>[^,]+), (?<locI1>[^,]+),[^;]+;(?<locA3>[^[]+)[[](?<locI2>[^+]+)[+][+][]]").Match(source);
				if (match.Success)
				{
					object variable = this.ilg.GetVariable(match.Groups["locA1"].Value);
					Type elementType2 = this.ilg.GetVariableType(variable).GetElementType();
					this.ilg.ConvertValue(elementType, elementType2);
					if (CodeGenerator.IsNullableGenericType(elementType2) || elementType2.IsValueType)
					{
						this.ilg.Stobj(elementType2);
						return;
					}
					this.ilg.Stelem(elementType2);
					return;
				}
				else
				{
					if (source.EndsWith(".Add(", StringComparison.Ordinal))
					{
						int length = source.LastIndexOf(".Add(", StringComparison.Ordinal);
						MethodInfo method = this.ilg.GetLocal(source.Substring(0, length)).LocalType.GetMethod("Add", CodeGenerator.InstanceBindingFlags, null, new Type[]
						{
							elementType
						}, null);
						Type parameterType = method.GetParameters()[0].ParameterType;
						this.ilg.ConvertValue(stackType, parameterType);
						this.ilg.Call(method);
						if (method.ReturnType != typeof(void))
						{
							this.ilg.Pop();
						}
						return;
					}
					match = XmlSerializationILGen.NewRegex("(?<a>[^[]+)[[](?<ia>.+)[]]").Match(source);
					if (match.Success)
					{
						Type elementType3 = this.ilg.GetVariableType(this.ilg.GetVariable(match.Groups["a"].Value)).GetElementType();
						this.ilg.ConvertValue(stackType, elementType3);
						this.ilg.Stelem(elementType3);
						return;
					}
					throw CodeGenerator.NotSupported("Unexpected: " + source);
				}
			}
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x000BECDC File Offset: 0x000BCEDC
		private void WriteArray(string source, string arrayName, ArrayMapping arrayMapping, bool readOnly, bool isNullable, int fixupIndex, int elementIndex)
		{
			MethodInfo method = typeof(XmlSerializationReader).GetMethod("ReadNull", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg.Ldarg(0);
			this.ilg.Call(method);
			this.ilg.IfNot();
			MemberMapping memberMapping = new MemberMapping();
			memberMapping.Elements = arrayMapping.Elements;
			memberMapping.TypeDesc = arrayMapping.TypeDesc;
			memberMapping.ReadOnly = readOnly;
			if (source.StartsWith("o.@", StringComparison.Ordinal))
			{
				memberMapping.MemberInfo = this.memberInfos[source.Substring(3)];
			}
			XmlSerializationReaderILGen.Member member = new XmlSerializationReaderILGen.Member(this, source, arrayName, elementIndex, memberMapping, false);
			member.IsNullable = false;
			XmlSerializationReaderILGen.Member[] members = new XmlSerializationReaderILGen.Member[]
			{
				member
			};
			this.WriteMemberBegin(members);
			Label label = this.ilg.DefineLabel();
			Label label2 = this.ilg.DefineLabel();
			if (readOnly)
			{
				this.ilg.Load(this.ilg.GetVariable(member.ArrayName));
				this.ilg.Load(null);
				this.ilg.Beq(label);
			}
			MethodInfo method2 = typeof(XmlSerializationReader).GetMethod("get_Reader", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			MethodInfo method3 = typeof(XmlReader).GetMethod("get_IsEmptyElement", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg.Ldarg(0);
			this.ilg.Call(method2);
			this.ilg.Call(method3);
			if (readOnly)
			{
				this.ilg.Br_S(label2);
				this.ilg.MarkLabel(label);
				this.ilg.Ldc(true);
				this.ilg.MarkLabel(label2);
			}
			this.ilg.If();
			MethodInfo method4 = typeof(XmlReader).GetMethod("Skip", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg.Ldarg(0);
			this.ilg.Call(method2);
			this.ilg.Call(method4);
			this.ilg.Else();
			MethodInfo method5 = typeof(XmlReader).GetMethod("ReadStartElement", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg.Ldarg(0);
			this.ilg.Call(method2);
			this.ilg.Call(method5);
			int loopIndex = this.WriteWhileNotLoopStart();
			string text = "UnknownNode(null, " + this.ExpectedElements(members) + ");";
			this.WriteMemberElements(members, text, text, null, null);
			MethodInfo method6 = typeof(XmlReader).GetMethod("MoveToContent", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg.Ldarg(0);
			this.ilg.Call(method2);
			this.ilg.Call(method6);
			this.ilg.Pop();
			this.WriteWhileLoopEnd(loopIndex);
			MethodInfo method7 = typeof(XmlSerializationReader).GetMethod("ReadEndElement", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg.Ldarg(0);
			this.ilg.Call(method7);
			this.ilg.EndIf();
			this.WriteMemberEnd(members, false);
			if (isNullable)
			{
				this.ilg.Else();
				member.IsNullable = true;
				this.WriteMemberBegin(members);
				this.WriteMemberEnd(members);
			}
			this.ilg.EndIf();
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x000BF040 File Offset: 0x000BD240
		private void WriteElement(string source, string arrayName, string choiceSource, ElementAccessor element, ChoiceIdentifierAccessor choice, string checkSpecified, bool checkForNull, bool readOnly, int fixupIndex, int elementIndex)
		{
			if (checkSpecified != null && checkSpecified.Length > 0)
			{
				this.ILGenSet(checkSpecified, true);
			}
			if (element.Mapping is ArrayMapping)
			{
				this.WriteArray(source, arrayName, (ArrayMapping)element.Mapping, readOnly, element.IsNullable, fixupIndex, elementIndex);
			}
			else if (element.Mapping is NullableMapping)
			{
				string methodName = base.ReferenceMapping(element.Mapping);
				this.WriteSourceBegin(source);
				this.ilg.Ldarg(0);
				this.ilg.Ldc(true);
				MethodBuilder methodInfo = base.EnsureMethodBuilder(this.typeBuilder, methodName, CodeGenerator.PrivateMethodAttributes, element.Mapping.TypeDesc.Type, new Type[]
				{
					typeof(bool)
				});
				this.ilg.Call(methodInfo);
				this.WriteSourceEnd(source, element.Mapping.TypeDesc.Type);
			}
			else if (element.Mapping is PrimitiveMapping)
			{
				bool flag = false;
				if (element.IsNullable)
				{
					MethodInfo method = typeof(XmlSerializationReader).GetMethod("ReadNull", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
					this.ilg.Ldarg(0);
					this.ilg.Call(method);
					this.ilg.If();
					this.WriteSourceBegin(source);
					if (element.Mapping.TypeDesc.IsValueType)
					{
						throw CodeGenerator.NotSupported("No such condition.  PrimitiveMapping && IsNullable = String, XmlQualifiedName and never IsValueType");
					}
					this.ilg.Load(null);
					this.WriteSourceEnd(source, element.Mapping.TypeDesc.Type);
					this.ilg.Else();
					flag = true;
				}
				if (element.Default != null && element.Default != DBNull.Value && element.Mapping.TypeDesc.IsValueType)
				{
					MethodInfo method2 = typeof(XmlSerializationReader).GetMethod("get_Reader", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
					MethodInfo method3 = typeof(XmlReader).GetMethod("get_IsEmptyElement", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
					this.ilg.Ldarg(0);
					this.ilg.Call(method2);
					this.ilg.Call(method3);
					this.ilg.If();
					MethodInfo method4 = typeof(XmlReader).GetMethod("Skip", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
					this.ilg.Ldarg(0);
					this.ilg.Call(method2);
					this.ilg.Call(method4);
					this.ilg.Else();
					flag = true;
				}
				if (LocalAppContextSwitches.EnableTimeSpanSerialization && element.Mapping.TypeDesc.Type == typeof(TimeSpan))
				{
					MethodInfo method5 = typeof(XmlSerializationReader).GetMethod("get_Reader", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
					MethodInfo method6 = typeof(XmlReader).GetMethod("get_IsEmptyElement", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
					this.ilg.Ldarg(0);
					this.ilg.Call(method5);
					this.ilg.Call(method6);
					this.ilg.If();
					this.WriteSourceBegin(source);
					MethodInfo method7 = typeof(XmlReader).GetMethod("Skip", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
					this.ilg.Ldarg(0);
					this.ilg.Call(method5);
					this.ilg.Call(method7);
					ConstructorInfo constructor = typeof(TimeSpan).GetConstructor(CodeGenerator.InstanceBindingFlags, null, new Type[]
					{
						typeof(long)
					}, null);
					this.ilg.Ldc(default(TimeSpan).Ticks);
					this.ilg.New(constructor);
					this.WriteSourceEnd(source, element.Mapping.TypeDesc.Type);
					this.ilg.Else();
					this.WriteSourceBegin(source);
					this.WritePrimitive(element.Mapping, "Reader.ReadElementString()");
					this.WriteSourceEnd(source, element.Mapping.TypeDesc.Type);
					this.ilg.EndIf();
				}
				else
				{
					this.WriteSourceBegin(source);
					if (element.Mapping.TypeDesc == base.QnameTypeDesc)
					{
						MethodInfo method8 = typeof(XmlSerializationReader).GetMethod("ReadElementQualifiedName", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
						this.ilg.Ldarg(0);
						this.ilg.Call(method8);
					}
					else
					{
						string formatterName = element.Mapping.TypeDesc.FormatterName;
						string source2;
						if (formatterName == "ByteArrayBase64" || formatterName == "ByteArrayHex")
						{
							source2 = "false";
						}
						else
						{
							source2 = "Reader.ReadElementString()";
						}
						this.WritePrimitive(element.Mapping, source2);
					}
					this.WriteSourceEnd(source, element.Mapping.TypeDesc.Type);
				}
				if (flag)
				{
					this.ilg.EndIf();
				}
			}
			else if (element.Mapping is StructMapping)
			{
				TypeMapping mapping = element.Mapping;
				string methodName2 = base.ReferenceMapping(mapping);
				if (checkForNull)
				{
					MethodInfo method9 = typeof(XmlSerializationReader).GetMethod("get_Reader", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
					MethodInfo method10 = typeof(XmlReader).GetMethod("Skip", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
					this.ilg.Ldloc(arrayName);
					this.ilg.Load(null);
					this.ilg.If(Cmp.EqualTo);
					this.ilg.Ldarg(0);
					this.ilg.Call(method9);
					this.ilg.Call(method10);
					this.ilg.Else();
				}
				this.WriteSourceBegin(source);
				List<Type> list = new List<Type>();
				this.ilg.Ldarg(0);
				if (mapping.TypeDesc.IsNullable)
				{
					this.ilg.Load(element.IsNullable);
					list.Add(typeof(bool));
				}
				this.ilg.Ldc(true);
				list.Add(typeof(bool));
				MethodBuilder methodInfo2 = base.EnsureMethodBuilder(this.typeBuilder, methodName2, CodeGenerator.PrivateMethodAttributes, mapping.TypeDesc.Type, list.ToArray());
				this.ilg.Call(methodInfo2);
				this.WriteSourceEnd(source, mapping.TypeDesc.Type);
				if (checkForNull)
				{
					this.ilg.EndIf();
				}
			}
			else
			{
				if (!(element.Mapping is SpecialMapping))
				{
					throw new InvalidOperationException(Res.GetString("Internal error."));
				}
				SpecialMapping specialMapping = (SpecialMapping)element.Mapping;
				TypeKind kind = specialMapping.TypeDesc.Kind;
				if (kind != TypeKind.Node)
				{
					if (kind != TypeKind.Serializable)
					{
						throw new InvalidOperationException(Res.GetString("Internal error."));
					}
					SerializableMapping serializableMapping = (SerializableMapping)element.Mapping;
					if (serializableMapping.DerivedMappings != null)
					{
						MethodInfo method11 = typeof(XmlSerializationReader).GetMethod("GetXsiType", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
						Label label = this.ilg.DefineLabel();
						Label label2 = this.ilg.DefineLabel();
						LocalBuilder localBuilder = this.ilg.DeclareOrGetLocal(typeof(XmlQualifiedName), "tser");
						this.ilg.Ldarg(0);
						this.ilg.Call(method11);
						this.ilg.Stloc(localBuilder);
						this.ilg.Ldloc(localBuilder);
						this.ilg.Load(null);
						this.ilg.Ceq();
						this.ilg.Brtrue(label);
						this.WriteQNameEqual("tser", serializableMapping.XsiType.Name, serializableMapping.XsiType.Namespace);
						this.ilg.Br_S(label2);
						this.ilg.MarkLabel(label);
						this.ilg.Ldc(true);
						this.ilg.MarkLabel(label2);
						this.ilg.If();
					}
					this.WriteSourceBeginTyped(source, serializableMapping.TypeDesc);
					bool flag2 = !element.Any && XmlSerializationILGen.IsWildcard(serializableMapping);
					Type typeFromHandle = typeof(XmlSerializationReader);
					string name = "ReadSerializable";
					BindingFlags instanceBindingFlags = CodeGenerator.InstanceBindingFlags;
					Binder binder = null;
					Type[] types;
					if (!flag2)
					{
						(types = new Type[1])[0] = typeof(IXmlSerializable);
					}
					else
					{
						Type[] array = new Type[2];
						array[0] = typeof(IXmlSerializable);
						types = array;
						array[1] = typeof(bool);
					}
					MethodInfo method12 = typeFromHandle.GetMethod(name, instanceBindingFlags, binder, types, null);
					this.ilg.Ldarg(0);
					base.RaCodeGen.ILGenForCreateInstance(this.ilg, serializableMapping.TypeDesc.Type, serializableMapping.TypeDesc.CannotNew, false);
					if (serializableMapping.TypeDesc.CannotNew)
					{
						this.ilg.ConvertValue(typeof(object), typeof(IXmlSerializable));
					}
					if (flag2)
					{
						this.ilg.Ldc(true);
					}
					this.ilg.Call(method12);
					if (serializableMapping.TypeDesc != null)
					{
						this.ilg.ConvertValue(typeof(IXmlSerializable), serializableMapping.TypeDesc.Type);
					}
					this.WriteSourceEnd(source, serializableMapping.TypeDesc.Type);
					if (serializableMapping.DerivedMappings != null)
					{
						this.WriteDerivedSerializable(serializableMapping, serializableMapping, source, flag2);
						this.WriteUnknownNode("UnknownNode", "null", null, true);
					}
				}
				else
				{
					bool flag3 = specialMapping.TypeDesc.FullName == typeof(XmlDocument).FullName;
					this.WriteSourceBeginTyped(source, specialMapping.TypeDesc);
					MethodInfo method13 = typeof(XmlSerializationReader).GetMethod(flag3 ? "ReadXmlDocument" : "ReadXmlNode", CodeGenerator.InstanceBindingFlags, null, new Type[]
					{
						typeof(bool)
					}, null);
					this.ilg.Ldarg(0);
					this.ilg.Ldc(!element.Any);
					this.ilg.Call(method13);
					if (specialMapping.TypeDesc != null)
					{
						this.ilg.Castclass(specialMapping.TypeDesc.Type);
					}
					this.WriteSourceEnd(source, specialMapping.TypeDesc.Type);
				}
			}
			if (choice != null)
			{
				this.WriteSourceBegin(choiceSource);
				CodeIdentifier.CheckValidIdentifier(choice.MemberIds[elementIndex]);
				base.RaCodeGen.ILGenForEnumMember(this.ilg, choice.Mapping.TypeDesc.Type, choice.MemberIds[elementIndex]);
				this.WriteSourceEnd(choiceSource, choice.Mapping.TypeDesc.Type);
			}
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x000BFB1C File Offset: 0x000BDD1C
		private void WriteDerivedSerializable(SerializableMapping head, SerializableMapping mapping, string source, bool isWrappedAny)
		{
			if (mapping == null)
			{
				return;
			}
			for (SerializableMapping serializableMapping = mapping.DerivedMappings; serializableMapping != null; serializableMapping = serializableMapping.NextDerivedMapping)
			{
				Label label = this.ilg.DefineLabel();
				Label label2 = this.ilg.DefineLabel();
				LocalBuilder local = this.ilg.GetLocal("tser");
				this.ilg.InitElseIf();
				this.ilg.Ldloc(local);
				this.ilg.Load(null);
				this.ilg.Ceq();
				this.ilg.Brtrue(label);
				this.WriteQNameEqual("tser", serializableMapping.XsiType.Name, serializableMapping.XsiType.Namespace);
				this.ilg.Br_S(label2);
				this.ilg.MarkLabel(label);
				this.ilg.Ldc(true);
				this.ilg.MarkLabel(label2);
				this.ilg.AndIf();
				if (serializableMapping.Type != null)
				{
					if (head.Type.IsAssignableFrom(serializableMapping.Type))
					{
						this.WriteSourceBeginTyped(source, head.TypeDesc);
						Type typeFromHandle = typeof(XmlSerializationReader);
						string name = "ReadSerializable";
						BindingFlags instanceBindingFlags = CodeGenerator.InstanceBindingFlags;
						Binder binder = null;
						Type[] types;
						if (!isWrappedAny)
						{
							(types = new Type[1])[0] = typeof(IXmlSerializable);
						}
						else
						{
							Type[] array = new Type[2];
							array[0] = typeof(IXmlSerializable);
							types = array;
							array[1] = typeof(bool);
						}
						MethodInfo method = typeFromHandle.GetMethod(name, instanceBindingFlags, binder, types, null);
						this.ilg.Ldarg(0);
						base.RaCodeGen.ILGenForCreateInstance(this.ilg, serializableMapping.TypeDesc.Type, serializableMapping.TypeDesc.CannotNew, false);
						if (serializableMapping.TypeDesc.CannotNew)
						{
							this.ilg.ConvertValue(typeof(object), typeof(IXmlSerializable));
						}
						if (isWrappedAny)
						{
							this.ilg.Ldc(true);
						}
						this.ilg.Call(method);
						if (head.TypeDesc != null)
						{
							this.ilg.ConvertValue(typeof(IXmlSerializable), head.TypeDesc.Type);
						}
						this.WriteSourceEnd(source, head.TypeDesc.Type);
					}
					else
					{
						MethodInfo method2 = typeof(XmlSerializationReader).GetMethod("CreateBadDerivationException", CodeGenerator.InstanceBindingFlags, null, new Type[]
						{
							typeof(string),
							typeof(string),
							typeof(string),
							typeof(string),
							typeof(string),
							typeof(string)
						}, null);
						this.ilg.Ldarg(0);
						this.ilg.Ldstr(serializableMapping.XsiType.Name);
						this.ilg.Ldstr(serializableMapping.XsiType.Namespace);
						this.ilg.Ldstr(head.XsiType.Name);
						this.ilg.Ldstr(head.XsiType.Namespace);
						this.ilg.Ldstr(serializableMapping.Type.FullName);
						this.ilg.Ldstr(head.Type.FullName);
						this.ilg.Call(method2);
						this.ilg.Throw();
					}
				}
				else
				{
					MethodInfo method3 = typeof(XmlSerializationReader).GetMethod("CreateMissingIXmlSerializableType", CodeGenerator.InstanceBindingFlags, null, new Type[]
					{
						typeof(string),
						typeof(string),
						typeof(string)
					}, null);
					this.ilg.Ldarg(0);
					this.ilg.Ldstr(serializableMapping.XsiType.Name);
					this.ilg.Ldstr(serializableMapping.XsiType.Namespace);
					this.ilg.Ldstr(head.Type.FullName);
					this.ilg.Call(method3);
					this.ilg.Throw();
				}
				this.WriteDerivedSerializable(head, serializableMapping, source, isWrappedAny);
			}
		}

		// Token: 0x06001E9C RID: 7836 RVA: 0x000BFF24 File Offset: 0x000BE124
		private int WriteWhileNotLoopStart()
		{
			MethodInfo method = typeof(XmlSerializationReader).GetMethod("get_Reader", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			MethodInfo method2 = typeof(XmlReader).GetMethod("MoveToContent", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg.Ldarg(0);
			this.ilg.Call(method);
			this.ilg.Call(method2);
			this.ilg.Pop();
			int result = this.WriteWhileLoopStartCheck();
			this.ilg.WhileBegin();
			return result;
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x000BFFB4 File Offset: 0x000BE1B4
		private void WriteWhileLoopEnd(int loopIndex)
		{
			this.WriteWhileLoopEndCheck(loopIndex);
			this.ilg.WhileBeginCondition();
			int intVar = 0;
			int intVar2 = 15;
			MethodInfo method = typeof(XmlSerializationReader).GetMethod("get_Reader", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			MethodInfo method2 = typeof(XmlReader).GetMethod("get_NodeType", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			Label label = this.ilg.DefineLabel();
			Label label2 = this.ilg.DefineLabel();
			this.ilg.Ldarg(0);
			this.ilg.Call(method);
			this.ilg.Call(method2);
			this.ilg.Ldc(intVar2);
			this.ilg.Beq(label);
			this.ilg.Ldarg(0);
			this.ilg.Call(method);
			this.ilg.Call(method2);
			this.ilg.Ldc(intVar);
			this.ilg.Cne();
			this.ilg.Br_S(label2);
			this.ilg.MarkLabel(label);
			this.ilg.Ldc(false);
			this.ilg.MarkLabel(label2);
			this.ilg.WhileEndCondition();
			this.ilg.WhileEnd();
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x000C00F8 File Offset: 0x000BE2F8
		private int WriteWhileLoopStartCheck()
		{
			MethodInfo method = typeof(XmlSerializationReader).GetMethod("get_ReaderCount", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg.Ldc(0);
			this.ilg.Stloc(typeof(int), string.Format(CultureInfo.InvariantCulture, "whileIterations{0}", this.nextWhileLoopIndex));
			this.ilg.Ldarg(0);
			this.ilg.Call(method);
			this.ilg.Stloc(typeof(int), string.Format(CultureInfo.InvariantCulture, "readerCount{0}", this.nextWhileLoopIndex));
			int num = this.nextWhileLoopIndex;
			this.nextWhileLoopIndex = num + 1;
			return num;
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x000C01BC File Offset: 0x000BE3BC
		private void WriteWhileLoopEndCheck(int loopIndex)
		{
			Type type = Type.GetType("System.Int32&");
			MethodInfo method = typeof(XmlSerializationReader).GetMethod("CheckReaderCount", CodeGenerator.InstanceBindingFlags, null, new Type[]
			{
				type,
				type
			}, null);
			this.ilg.Ldarg(0);
			this.ilg.Ldloca(this.ilg.GetLocal(string.Format(CultureInfo.InvariantCulture, "whileIterations{0}", loopIndex)));
			this.ilg.Ldloca(this.ilg.GetLocal(string.Format(CultureInfo.InvariantCulture, "readerCount{0}", loopIndex)));
			this.ilg.Call(method);
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x000C026C File Offset: 0x000BE46C
		private void WriteParamsRead(int length)
		{
			LocalBuilder local = this.ilg.DeclareLocal(typeof(bool[]), "paramsRead");
			this.ilg.NewArray(typeof(bool), length);
			this.ilg.Stloc(local);
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x000C02BC File Offset: 0x000BE4BC
		private void WriteCreateMapping(TypeMapping mapping, string local)
		{
			string csharpName = mapping.TypeDesc.CSharpName;
			bool cannotNew = mapping.TypeDesc.CannotNew;
			LocalBuilder local2 = this.ilg.DeclareLocal(mapping.TypeDesc.Type, local);
			if (cannotNew)
			{
				this.ilg.BeginExceptionBlock();
			}
			base.RaCodeGen.ILGenForCreateInstance(this.ilg, mapping.TypeDesc.Type, mapping.TypeDesc.CannotNew, true);
			this.ilg.Stloc(local2);
			if (cannotNew)
			{
				this.ilg.Leave();
				this.WriteCatchException(typeof(MissingMethodException));
				MethodInfo method = typeof(XmlSerializationReader).GetMethod("CreateInaccessibleConstructorException", CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(string)
				}, null);
				this.ilg.Ldarg(0);
				this.ilg.Ldstr(csharpName);
				this.ilg.Call(method);
				this.ilg.Throw();
				this.WriteCatchException(typeof(SecurityException));
				MethodInfo method2 = typeof(XmlSerializationReader).GetMethod("CreateCtorHasSecurityException", CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(string)
				}, null);
				this.ilg.Ldarg(0);
				this.ilg.Ldstr(csharpName);
				this.ilg.Call(method2);
				this.ilg.Throw();
				this.ilg.EndExceptionBlock();
			}
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x000C0431 File Offset: 0x000BE631
		private void WriteCatchException(Type exceptionType)
		{
			this.ilg.BeginCatchBlock(exceptionType);
			this.ilg.Pop();
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x000C044C File Offset: 0x000BE64C
		private void WriteCatchCastException(TypeDesc typeDesc, string source, string id)
		{
			this.WriteCatchException(typeof(InvalidCastException));
			MethodInfo method = typeof(XmlSerializationReader).GetMethod("CreateInvalidCastException", CodeGenerator.InstanceBindingFlags, null, new Type[]
			{
				typeof(Type),
				typeof(object),
				typeof(string)
			}, null);
			this.ilg.Ldarg(0);
			this.ilg.Ldc(typeDesc.Type);
			if (source.StartsWith("GetTarget(ids[", StringComparison.Ordinal))
			{
				MethodInfo method2 = typeof(XmlSerializationReader).GetMethod("GetTarget", CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(string)
				}, null);
				object variable = this.ilg.GetVariable("ids");
				this.ilg.Ldarg(0);
				this.ilg.LoadArrayElement(variable, int.Parse(source.Substring(14, source.Length - 16), CultureInfo.InvariantCulture));
				this.ilg.Call(method2);
			}
			else
			{
				this.ilg.Load(this.ilg.GetVariable(source));
			}
			if (id == null)
			{
				this.ilg.Load(null);
			}
			else if (id.StartsWith("ids[", StringComparison.Ordinal))
			{
				object variable2 = this.ilg.GetVariable("ids");
				this.ilg.LoadArrayElement(variable2, int.Parse(id.Substring(4, id.Length - 5), CultureInfo.InvariantCulture));
			}
			else
			{
				object variable3 = this.ilg.GetVariable(id);
				this.ilg.Load(variable3);
				this.ilg.ConvertValue(this.ilg.GetVariableType(variable3), typeof(string));
			}
			this.ilg.Call(method);
			this.ilg.Throw();
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x000C0630 File Offset: 0x000BE830
		private void WriteArrayLocalDecl(string typeName, string variableName, string initValue, TypeDesc arrayTypeDesc)
		{
			base.RaCodeGen.WriteArrayLocalDecl(typeName, variableName, new SourceInfo(initValue, initValue, null, arrayTypeDesc.Type, this.ilg), arrayTypeDesc);
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x000C0656 File Offset: 0x000BE856
		private void WriteCreateInstance(string source, bool ctorInaccessible, Type type)
		{
			base.RaCodeGen.WriteCreateInstance(source, ctorInaccessible, type, this.ilg);
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x000C066C File Offset: 0x000BE86C
		private void WriteLocalDecl(string variableName, SourceInfo initValue)
		{
			base.RaCodeGen.WriteLocalDecl(variableName, initValue);
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x000C067C File Offset: 0x000BE87C
		private void ILGenElseString(string elseString)
		{
			MethodInfo method = typeof(XmlSerializationReader).GetMethod("UnknownNode", CodeGenerator.InstanceBindingFlags, null, new Type[]
			{
				typeof(object)
			}, null);
			MethodInfo method2 = typeof(XmlSerializationReader).GetMethod("UnknownNode", CodeGenerator.InstanceBindingFlags, null, new Type[]
			{
				typeof(object),
				typeof(string)
			}, null);
			Match match = XmlSerializationILGen.NewRegex("UnknownNode[(]null, @[\"](?<qnames>[^\"]*)[\"][)];").Match(elseString);
			if (match.Success)
			{
				this.ilg.Ldarg(0);
				this.ilg.Load(null);
				this.ilg.Ldstr(match.Groups["qnames"].Value);
				this.ilg.Call(method2);
				return;
			}
			match = XmlSerializationILGen.NewRegex("UnknownNode[(][(]object[)](?<o>[^,]+), @[\"](?<qnames>[^\"]*)[\"][)];").Match(elseString);
			if (match.Success)
			{
				this.ilg.Ldarg(0);
				LocalBuilder local = this.ilg.GetLocal(match.Groups["o"].Value);
				this.ilg.Ldloc(local);
				this.ilg.ConvertValue(local.LocalType, typeof(object));
				this.ilg.Ldstr(match.Groups["qnames"].Value);
				this.ilg.Call(method2);
				return;
			}
			match = XmlSerializationILGen.NewRegex("UnknownNode[(][(]object[)](?<o>[^,]+), null[)];").Match(elseString);
			if (match.Success)
			{
				this.ilg.Ldarg(0);
				LocalBuilder local2 = this.ilg.GetLocal(match.Groups["o"].Value);
				this.ilg.Ldloc(local2);
				this.ilg.ConvertValue(local2.LocalType, typeof(object));
				this.ilg.Load(null);
				this.ilg.Call(method2);
				return;
			}
			match = XmlSerializationILGen.NewRegex("UnknownNode[(][(]object[)](?<o>[^)]+)[)];").Match(elseString);
			if (match.Success)
			{
				this.ilg.Ldarg(0);
				LocalBuilder local3 = this.ilg.GetLocal(match.Groups["o"].Value);
				this.ilg.Ldloc(local3);
				this.ilg.ConvertValue(local3.LocalType, typeof(object));
				this.ilg.Call(method);
				return;
			}
			throw CodeGenerator.NotSupported("Unexpected: " + elseString);
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x000C0908 File Offset: 0x000BEB08
		private void ILGenParamsReadSource(string paramsReadSource)
		{
			Match match = XmlSerializationILGen.NewRegex("paramsRead\\[(?<index>[0-9]+)\\]").Match(paramsReadSource);
			if (match.Success)
			{
				this.ilg.LoadArrayElement(this.ilg.GetLocal("paramsRead"), int.Parse(match.Groups["index"].Value, CultureInfo.InvariantCulture));
				return;
			}
			throw CodeGenerator.NotSupported("Unexpected: " + paramsReadSource);
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x000C0980 File Offset: 0x000BEB80
		private void ILGenParamsReadSource(string paramsReadSource, bool value)
		{
			Match match = XmlSerializationILGen.NewRegex("paramsRead\\[(?<index>[0-9]+)\\]").Match(paramsReadSource);
			if (match.Success)
			{
				this.ilg.StoreArrayElement(this.ilg.GetLocal("paramsRead"), int.Parse(match.Groups["index"].Value, CultureInfo.InvariantCulture), value);
				return;
			}
			throw CodeGenerator.NotSupported("Unexpected: " + paramsReadSource);
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x000C09FC File Offset: 0x000BEBFC
		private void ILGenElementElseString(string elementElseString)
		{
			if (elementElseString == "throw CreateUnknownNodeException();")
			{
				MethodInfo method = typeof(XmlSerializationReader).GetMethod("CreateUnknownNodeException", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				this.ilg.Ldarg(0);
				this.ilg.Call(method);
				this.ilg.Throw();
				return;
			}
			if (elementElseString.StartsWith("UnknownNode(", StringComparison.Ordinal))
			{
				this.ILGenElseString(elementElseString);
				return;
			}
			throw CodeGenerator.NotSupported("Unexpected: " + elementElseString);
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x000C0A81 File Offset: 0x000BEC81
		private void ILGenSet(string source, object value)
		{
			this.WriteSourceBegin(source);
			this.ilg.Load(value);
			this.WriteSourceEnd(source, (value == null) ? typeof(object) : value.GetType());
		}

		// Token: 0x04001ADC RID: 6876
		private Hashtable idNames = new Hashtable();

		// Token: 0x04001ADD RID: 6877
		private Dictionary<string, FieldBuilder> idNameFields = new Dictionary<string, FieldBuilder>();

		// Token: 0x04001ADE RID: 6878
		private Hashtable enums;

		// Token: 0x04001ADF RID: 6879
		private int nextIdNumber;

		// Token: 0x04001AE0 RID: 6880
		private int nextWhileLoopIndex;

		// Token: 0x020002F6 RID: 758
		private class CreateCollectionInfo
		{
			// Token: 0x06001EAC RID: 7852 RVA: 0x000C0AB2 File Offset: 0x000BECB2
			internal CreateCollectionInfo(string name, TypeDesc td)
			{
				this.name = name;
				this.td = td;
			}

			// Token: 0x170005F9 RID: 1529
			// (get) Token: 0x06001EAD RID: 7853 RVA: 0x000C0AC8 File Offset: 0x000BECC8
			internal string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x170005FA RID: 1530
			// (get) Token: 0x06001EAE RID: 7854 RVA: 0x000C0AD0 File Offset: 0x000BECD0
			internal TypeDesc TypeDesc
			{
				get
				{
					return this.td;
				}
			}

			// Token: 0x04001AE1 RID: 6881
			private string name;

			// Token: 0x04001AE2 RID: 6882
			private TypeDesc td;
		}

		// Token: 0x020002F7 RID: 759
		private class Member
		{
			// Token: 0x06001EAF RID: 7855 RVA: 0x000C0AD8 File Offset: 0x000BECD8
			internal Member(XmlSerializationReaderILGen outerClass, string source, string arrayName, int i, MemberMapping mapping) : this(outerClass, source, null, arrayName, i, mapping, false, null)
			{
			}

			// Token: 0x06001EB0 RID: 7856 RVA: 0x000C0AF8 File Offset: 0x000BECF8
			internal Member(XmlSerializationReaderILGen outerClass, string source, string arrayName, int i, MemberMapping mapping, string choiceSource) : this(outerClass, source, null, arrayName, i, mapping, false, choiceSource)
			{
			}

			// Token: 0x06001EB1 RID: 7857 RVA: 0x000C0B18 File Offset: 0x000BED18
			internal Member(XmlSerializationReaderILGen outerClass, string source, string arraySource, string arrayName, int i, MemberMapping mapping) : this(outerClass, source, arraySource, arrayName, i, mapping, false, null)
			{
			}

			// Token: 0x06001EB2 RID: 7858 RVA: 0x000C0B38 File Offset: 0x000BED38
			internal Member(XmlSerializationReaderILGen outerClass, string source, string arraySource, string arrayName, int i, MemberMapping mapping, string choiceSource) : this(outerClass, source, arraySource, arrayName, i, mapping, false, choiceSource)
			{
			}

			// Token: 0x06001EB3 RID: 7859 RVA: 0x000C0B58 File Offset: 0x000BED58
			internal Member(XmlSerializationReaderILGen outerClass, string source, string arrayName, int i, MemberMapping mapping, bool multiRef) : this(outerClass, source, null, arrayName, i, mapping, multiRef, null)
			{
			}

			// Token: 0x06001EB4 RID: 7860 RVA: 0x000C0B78 File Offset: 0x000BED78
			internal Member(XmlSerializationReaderILGen outerClass, string source, string arraySource, string arrayName, int i, MemberMapping mapping, bool multiRef, string choiceSource)
			{
				this.source = source;
				this.arrayName = arrayName + "_" + i.ToString(CultureInfo.InvariantCulture);
				this.choiceArrayName = "choice_" + this.arrayName;
				this.choiceSource = choiceSource;
				if (mapping.TypeDesc.IsArrayLike)
				{
					if (arraySource != null)
					{
						this.arraySource = arraySource;
					}
					else
					{
						this.arraySource = outerClass.GetArraySource(mapping.TypeDesc, this.arrayName, multiRef);
					}
					this.isArray = mapping.TypeDesc.IsArray;
					this.isList = !this.isArray;
					if (mapping.ChoiceIdentifier != null)
					{
						this.choiceArraySource = outerClass.GetArraySource(mapping.TypeDesc, this.choiceArrayName, multiRef);
						string text = this.choiceArrayName;
						string text2 = "c" + text;
						string csharpName = mapping.ChoiceIdentifier.Mapping.TypeDesc.CSharpName;
						string text3 = "(" + csharpName + "[])";
						string str = string.Concat(new string[]
						{
							text,
							" = ",
							text3,
							"EnsureArrayIndex(",
							text,
							", ",
							text2,
							", ",
							outerClass.RaCodeGen.GetStringForTypeof(csharpName),
							");"
						});
						this.choiceArraySource = str + outerClass.RaCodeGen.GetStringForArrayMember(text, text2 + "++", mapping.ChoiceIdentifier.Mapping.TypeDesc);
					}
					else
					{
						this.choiceArraySource = this.choiceSource;
					}
				}
				else
				{
					this.arraySource = ((arraySource == null) ? source : arraySource);
					this.choiceArraySource = this.choiceSource;
				}
				this.mapping = mapping;
			}

			// Token: 0x170005FB RID: 1531
			// (get) Token: 0x06001EB5 RID: 7861 RVA: 0x000C0D4C File Offset: 0x000BEF4C
			internal MemberMapping Mapping
			{
				get
				{
					return this.mapping;
				}
			}

			// Token: 0x170005FC RID: 1532
			// (get) Token: 0x06001EB6 RID: 7862 RVA: 0x000C0D54 File Offset: 0x000BEF54
			internal string Source
			{
				get
				{
					return this.source;
				}
			}

			// Token: 0x170005FD RID: 1533
			// (get) Token: 0x06001EB7 RID: 7863 RVA: 0x000C0D5C File Offset: 0x000BEF5C
			internal string ArrayName
			{
				get
				{
					return this.arrayName;
				}
			}

			// Token: 0x170005FE RID: 1534
			// (get) Token: 0x06001EB8 RID: 7864 RVA: 0x000C0D64 File Offset: 0x000BEF64
			internal string ArraySource
			{
				get
				{
					return this.arraySource;
				}
			}

			// Token: 0x170005FF RID: 1535
			// (get) Token: 0x06001EB9 RID: 7865 RVA: 0x000C0D6C File Offset: 0x000BEF6C
			internal bool IsList
			{
				get
				{
					return this.isList;
				}
			}

			// Token: 0x17000600 RID: 1536
			// (get) Token: 0x06001EBA RID: 7866 RVA: 0x000C0D74 File Offset: 0x000BEF74
			internal bool IsArrayLike
			{
				get
				{
					return this.isArray || this.isList;
				}
			}

			// Token: 0x17000601 RID: 1537
			// (get) Token: 0x06001EBB RID: 7867 RVA: 0x000C0D86 File Offset: 0x000BEF86
			// (set) Token: 0x06001EBC RID: 7868 RVA: 0x000C0D8E File Offset: 0x000BEF8E
			internal bool IsNullable
			{
				get
				{
					return this.isNullable;
				}
				set
				{
					this.isNullable = value;
				}
			}

			// Token: 0x17000602 RID: 1538
			// (get) Token: 0x06001EBD RID: 7869 RVA: 0x000C0D97 File Offset: 0x000BEF97
			// (set) Token: 0x06001EBE RID: 7870 RVA: 0x000C0D9F File Offset: 0x000BEF9F
			internal bool MultiRef
			{
				get
				{
					return this.multiRef;
				}
				set
				{
					this.multiRef = value;
				}
			}

			// Token: 0x17000603 RID: 1539
			// (get) Token: 0x06001EBF RID: 7871 RVA: 0x000C0DA8 File Offset: 0x000BEFA8
			// (set) Token: 0x06001EC0 RID: 7872 RVA: 0x000C0DB0 File Offset: 0x000BEFB0
			internal int FixupIndex
			{
				get
				{
					return this.fixupIndex;
				}
				set
				{
					this.fixupIndex = value;
				}
			}

			// Token: 0x17000604 RID: 1540
			// (get) Token: 0x06001EC1 RID: 7873 RVA: 0x000C0DB9 File Offset: 0x000BEFB9
			// (set) Token: 0x06001EC2 RID: 7874 RVA: 0x000C0DC1 File Offset: 0x000BEFC1
			internal string ParamsReadSource
			{
				get
				{
					return this.paramsReadSource;
				}
				set
				{
					this.paramsReadSource = value;
				}
			}

			// Token: 0x17000605 RID: 1541
			// (get) Token: 0x06001EC3 RID: 7875 RVA: 0x000C0DCA File Offset: 0x000BEFCA
			// (set) Token: 0x06001EC4 RID: 7876 RVA: 0x000C0DD2 File Offset: 0x000BEFD2
			internal string CheckSpecifiedSource
			{
				get
				{
					return this.checkSpecifiedSource;
				}
				set
				{
					this.checkSpecifiedSource = value;
				}
			}

			// Token: 0x17000606 RID: 1542
			// (get) Token: 0x06001EC5 RID: 7877 RVA: 0x000C0DDB File Offset: 0x000BEFDB
			internal string ChoiceSource
			{
				get
				{
					return this.choiceSource;
				}
			}

			// Token: 0x17000607 RID: 1543
			// (get) Token: 0x06001EC6 RID: 7878 RVA: 0x000C0DE3 File Offset: 0x000BEFE3
			internal string ChoiceArrayName
			{
				get
				{
					return this.choiceArrayName;
				}
			}

			// Token: 0x17000608 RID: 1544
			// (get) Token: 0x06001EC7 RID: 7879 RVA: 0x000C0DEB File Offset: 0x000BEFEB
			internal string ChoiceArraySource
			{
				get
				{
					return this.choiceArraySource;
				}
			}

			// Token: 0x04001AE3 RID: 6883
			private string source;

			// Token: 0x04001AE4 RID: 6884
			private string arrayName;

			// Token: 0x04001AE5 RID: 6885
			private string arraySource;

			// Token: 0x04001AE6 RID: 6886
			private string choiceArrayName;

			// Token: 0x04001AE7 RID: 6887
			private string choiceSource;

			// Token: 0x04001AE8 RID: 6888
			private string choiceArraySource;

			// Token: 0x04001AE9 RID: 6889
			private MemberMapping mapping;

			// Token: 0x04001AEA RID: 6890
			private bool isArray;

			// Token: 0x04001AEB RID: 6891
			private bool isList;

			// Token: 0x04001AEC RID: 6892
			private bool isNullable;

			// Token: 0x04001AED RID: 6893
			private bool multiRef;

			// Token: 0x04001AEE RID: 6894
			private int fixupIndex = -1;

			// Token: 0x04001AEF RID: 6895
			private string paramsReadSource;

			// Token: 0x04001AF0 RID: 6896
			private string checkSpecifiedSource;
		}
	}
}
