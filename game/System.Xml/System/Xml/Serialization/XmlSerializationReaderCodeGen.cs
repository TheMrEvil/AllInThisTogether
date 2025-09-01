using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Security;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x020002F2 RID: 754
	internal class XmlSerializationReaderCodeGen : XmlSerializationCodeGen
	{
		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001E03 RID: 7683 RVA: 0x000B139F File Offset: 0x000AF59F
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

		// Token: 0x06001E04 RID: 7684 RVA: 0x000B13BA File Offset: 0x000AF5BA
		internal XmlSerializationReaderCodeGen(IndentedWriter writer, TypeScope[] scopes, string access, string className) : base(writer, scopes, access, className)
		{
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x000B13E0 File Offset: 0x000AF5E0
		internal void GenerateBegin()
		{
			base.Writer.Write(base.Access);
			base.Writer.Write(" class ");
			base.Writer.Write(base.ClassName);
			base.Writer.Write(" : ");
			base.Writer.Write(typeof(XmlSerializationReader).FullName);
			base.Writer.WriteLine(" {");
			IndentedWriter writer = base.Writer;
			int i = writer.Indent;
			writer.Indent = i + 1;
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
			TypeScope[] scopes = base.Scopes;
			for (i = 0; i < scopes.Length; i++)
			{
				foreach (object obj2 in scopes[i].TypeMappings)
				{
					TypeMapping typeMapping2 = (TypeMapping)obj2;
					if (typeMapping2.IsSoap)
					{
						if (typeMapping2 is StructMapping)
						{
							this.WriteStructMethod((StructMapping)typeMapping2);
						}
						else if (typeMapping2 is EnumMapping)
						{
							this.WriteEnumMethod((EnumMapping)typeMapping2);
						}
						else if (typeMapping2 is NullableMapping)
						{
							this.WriteNullableMethod((NullableMapping)typeMapping2);
						}
					}
				}
			}
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x000B15D0 File Offset: 0x000AF7D0
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

		// Token: 0x06001E07 RID: 7687 RVA: 0x000B1637 File Offset: 0x000AF837
		internal void GenerateEnd()
		{
			this.GenerateEnd(new string[0], new XmlMapping[0], new Type[0]);
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x000B1654 File Offset: 0x000AF854
		internal void GenerateEnd(string[] methods, XmlMapping[] xmlMappings, Type[] types)
		{
			base.GenerateReferencedMethods();
			this.GenerateInitCallbacksMethod();
			foreach (object obj in this.createMethods.Values)
			{
				XmlSerializationReaderCodeGen.CreateCollectionInfo c = (XmlSerializationReaderCodeGen.CreateCollectionInfo)obj;
				this.WriteCreateCollectionMethod(c);
			}
			base.Writer.WriteLine();
			foreach (object obj2 in this.idNames.Values)
			{
				string s = (string)obj2;
				base.Writer.Write("string ");
				base.Writer.Write(s);
				base.Writer.WriteLine(";");
			}
			base.Writer.WriteLine();
			base.Writer.WriteLine("protected override void InitIDs() {");
			IndentedWriter writer = base.Writer;
			int indent = writer.Indent;
			writer.Indent = indent + 1;
			foreach (object obj3 in this.idNames.Keys)
			{
				string text = (string)obj3;
				string s2 = (string)this.idNames[text];
				base.Writer.Write(s2);
				base.Writer.Write(" = Reader.NameTable.Add(");
				base.WriteQuotedCSharpString(text);
				base.Writer.WriteLine(");");
			}
			IndentedWriter writer2 = base.Writer;
			indent = writer2.Indent;
			writer2.Indent = indent - 1;
			base.Writer.WriteLine("}");
			IndentedWriter writer3 = base.Writer;
			indent = writer3.Indent;
			writer3.Indent = indent - 1;
			base.Writer.WriteLine("}");
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x000B184C File Offset: 0x000AFA4C
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

		// Token: 0x06001E0A RID: 7690 RVA: 0x000B18C0 File Offset: 0x000AFAC0
		private void WriteIsStartTag(string name, string ns)
		{
			base.Writer.Write("if (Reader.IsStartElement(");
			this.WriteID(name);
			base.Writer.Write(", ");
			this.WriteID(ns);
			base.Writer.WriteLine(")) {");
			IndentedWriter writer = base.Writer;
			int indent = writer.Indent;
			writer.Indent = indent + 1;
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x000B1920 File Offset: 0x000AFB20
		private void WriteUnknownNode(string func, string node, ElementAccessor e, bool anyIfs)
		{
			if (anyIfs)
			{
				base.Writer.WriteLine("else {");
				IndentedWriter writer = base.Writer;
				int indent = writer.Indent;
				writer.Indent = indent + 1;
			}
			base.Writer.Write(func);
			base.Writer.Write("(");
			base.Writer.Write(node);
			if (e != null)
			{
				base.Writer.Write(", ");
				string text = (e.Form == XmlSchemaForm.Qualified) ? e.Namespace : "";
				text += ":";
				text += e.Name;
				ReflectionAwareCodeGen.WriteQuotedCSharpString(base.Writer, text);
			}
			base.Writer.WriteLine(");");
			if (anyIfs)
			{
				IndentedWriter writer2 = base.Writer;
				int indent = writer2.Indent;
				writer2.Indent = indent - 1;
				base.Writer.WriteLine("}");
			}
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x000B1A08 File Offset: 0x000AFC08
		private void GenerateInitCallbacksMethod()
		{
			base.Writer.WriteLine();
			base.Writer.WriteLine("protected override void InitCallbacks() {");
			IndentedWriter writer = base.Writer;
			int i = writer.Indent;
			writer.Indent = i + 1;
			string text = this.NextMethodName("Array");
			bool flag = false;
			TypeScope[] scopes = base.Scopes;
			for (i = 0; i < scopes.Length; i++)
			{
				foreach (object obj in scopes[i].TypeMappings)
				{
					TypeMapping typeMapping = (TypeMapping)obj;
					if (typeMapping.IsSoap && (typeMapping is StructMapping || typeMapping is EnumMapping || typeMapping is ArrayMapping || typeMapping is NullableMapping) && !typeMapping.TypeDesc.IsRoot)
					{
						string s;
						if (typeMapping is ArrayMapping)
						{
							s = text;
							flag = true;
						}
						else
						{
							s = (string)base.MethodNames[typeMapping];
						}
						base.Writer.Write("AddReadCallback(");
						this.WriteID(typeMapping.TypeName);
						base.Writer.Write(", ");
						this.WriteID(typeMapping.Namespace);
						base.Writer.Write(", ");
						base.Writer.Write(base.RaCodeGen.GetStringForTypeof(typeMapping.TypeDesc.CSharpName, typeMapping.TypeDesc.UseReflection));
						base.Writer.Write(", new ");
						base.Writer.Write(typeof(XmlSerializationReadCallback).FullName);
						base.Writer.Write("(this.");
						base.Writer.Write(s);
						base.Writer.WriteLine("));");
					}
				}
			}
			IndentedWriter writer2 = base.Writer;
			i = writer2.Indent;
			writer2.Indent = i - 1;
			base.Writer.WriteLine("}");
			if (flag)
			{
				base.Writer.WriteLine();
				base.Writer.Write("object ");
				base.Writer.Write(text);
				base.Writer.WriteLine("() {");
				IndentedWriter writer3 = base.Writer;
				i = writer3.Indent;
				writer3.Indent = i + 1;
				base.Writer.WriteLine("// dummy array method");
				base.Writer.WriteLine("UnknownNode(null);");
				base.Writer.WriteLine("return null;");
				IndentedWriter writer4 = base.Writer;
				i = writer4.Indent;
				writer4.Indent = i - 1;
				base.Writer.WriteLine("}");
			}
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x000B1CD4 File Offset: 0x000AFED4
		private string GenerateMembersElement(XmlMembersMapping xmlMembersMapping)
		{
			if (xmlMembersMapping.Accessor.IsSoap)
			{
				return this.GenerateEncodedMembersElement(xmlMembersMapping);
			}
			return this.GenerateLiteralMembersElement(xmlMembersMapping);
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x000B1CF4 File Offset: 0x000AFEF4
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

		// Token: 0x06001E0F RID: 7695 RVA: 0x000B1D52 File Offset: 0x000AFF52
		private string GetChoiceIdentifierSource(MemberMapping mapping, string parent, TypeDesc parentTypeDesc)
		{
			if (mapping.ChoiceIdentifier == null)
			{
				return "";
			}
			CodeIdentifier.CheckValidIdentifier(mapping.ChoiceIdentifier.MemberName);
			return base.RaCodeGen.GetStringForMember(parent, mapping.ChoiceIdentifier.MemberName, parentTypeDesc);
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x000B1D8C File Offset: 0x000AFF8C
		private string GenerateLiteralMembersElement(XmlMembersMapping xmlMembersMapping)
		{
			ElementAccessor accessor = xmlMembersMapping.Accessor;
			MemberMapping[] members = ((MembersMapping)accessor.Mapping).Members;
			bool hasWrapperElement = ((MembersMapping)accessor.Mapping).HasWrapperElement;
			string text = this.NextMethodName(accessor.Name);
			base.Writer.WriteLine();
			base.Writer.Write("public object[] ");
			base.Writer.Write(text);
			base.Writer.WriteLine("() {");
			IndentedWriter writer = base.Writer;
			int indent = writer.Indent;
			writer.Indent = indent + 1;
			base.Writer.WriteLine("Reader.MoveToContent();");
			base.Writer.Write("object[] p = new object[");
			base.Writer.Write(members.Length.ToString(CultureInfo.InvariantCulture));
			base.Writer.WriteLine("];");
			this.InitializeValueTypes("p", members);
			int loopIndex = 0;
			if (hasWrapperElement)
			{
				loopIndex = this.WriteWhileNotLoopStart();
				IndentedWriter writer2 = base.Writer;
				indent = writer2.Indent;
				writer2.Indent = indent + 1;
				this.WriteIsStartTag(accessor.Name, (accessor.Form == XmlSchemaForm.Qualified) ? accessor.Namespace : "");
			}
			XmlSerializationReaderCodeGen.Member anyText = null;
			XmlSerializationReaderCodeGen.Member anyElement = null;
			XmlSerializationReaderCodeGen.Member anyAttribute = null;
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
				XmlSerializationReaderCodeGen.Member member = new XmlSerializationReaderCodeGen.Member(this, text2, arraySource, "a", i, memberMapping, choiceIdentifierSource);
				XmlSerializationReaderCodeGen.Member member2 = new XmlSerializationReaderCodeGen.Member(this, text2, null, "a", i, memberMapping, choiceIdentifierSource);
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
			XmlSerializationReaderCodeGen.Member[] array = (XmlSerializationReaderCodeGen.Member[])arrayList.ToArray(typeof(XmlSerializationReaderCodeGen.Member));
			XmlSerializationReaderCodeGen.Member[] members2 = (XmlSerializationReaderCodeGen.Member[])arrayList2.ToArray(typeof(XmlSerializationReaderCodeGen.Member));
			if (array.Length != 0 && array[0].Mapping.IsReturnValue)
			{
				base.Writer.WriteLine("IsReturnValue = true;");
			}
			this.WriteParamsRead(members.Length);
			if (arrayList3.Count > 0)
			{
				XmlSerializationReaderCodeGen.Member[] members3 = (XmlSerializationReaderCodeGen.Member[])arrayList3.ToArray(typeof(XmlSerializationReaderCodeGen.Member));
				this.WriteMemberBegin(members3);
				this.WriteAttributes(members3, anyAttribute, "UnknownNode", "(object)p");
				this.WriteMemberEnd(members3);
				base.Writer.WriteLine("Reader.MoveToElement();");
			}
			this.WriteMemberBegin(members2);
			if (hasWrapperElement)
			{
				base.Writer.WriteLine("if (Reader.IsEmptyElement) { Reader.Skip(); Reader.MoveToContent(); continue; }");
				base.Writer.WriteLine("Reader.ReadStartElement();");
			}
			if (this.IsSequence(array))
			{
				base.Writer.WriteLine("int state = 0;");
			}
			int loopIndex2 = this.WriteWhileNotLoopStart();
			IndentedWriter writer3 = base.Writer;
			indent = writer3.Indent;
			writer3.Indent = indent + 1;
			string text3 = "UnknownNode((object)p, " + this.ExpectedElements(array) + ");";
			this.WriteMemberElements(array, text3, text3, anyElement, anyText, null);
			base.Writer.WriteLine("Reader.MoveToContent();");
			this.WriteWhileLoopEnd(loopIndex2);
			this.WriteMemberEnd(members2);
			if (hasWrapperElement)
			{
				base.Writer.WriteLine("ReadEndElement();");
				IndentedWriter writer4 = base.Writer;
				indent = writer4.Indent;
				writer4.Indent = indent - 1;
				base.Writer.WriteLine("}");
				this.WriteUnknownNode("UnknownNode", "null", accessor, true);
				base.Writer.WriteLine("Reader.MoveToContent();");
				this.WriteWhileLoopEnd(loopIndex);
			}
			base.Writer.WriteLine("return p;");
			IndentedWriter writer5 = base.Writer;
			indent = writer5.Indent;
			writer5.Indent = indent - 1;
			base.Writer.WriteLine("}");
			return text;
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x000B2384 File Offset: 0x000B0584
		private void InitializeValueTypes(string arrayName, MemberMapping[] mappings)
		{
			for (int i = 0; i < mappings.Length; i++)
			{
				if (mappings[i].TypeDesc.IsValueType)
				{
					base.Writer.Write(arrayName);
					base.Writer.Write("[");
					base.Writer.Write(i.ToString(CultureInfo.InvariantCulture));
					base.Writer.Write("] = ");
					if (mappings[i].TypeDesc.IsOptionalValue && mappings[i].TypeDesc.BaseTypeDesc.UseReflection)
					{
						base.Writer.Write("null");
					}
					else
					{
						base.Writer.Write(base.RaCodeGen.GetStringForCreateInstance(mappings[i].TypeDesc.CSharpName, mappings[i].TypeDesc.UseReflection, false, false));
					}
					base.Writer.WriteLine(";");
				}
			}
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x000B2474 File Offset: 0x000B0674
		private string GenerateEncodedMembersElement(XmlMembersMapping xmlMembersMapping)
		{
			ElementAccessor accessor = xmlMembersMapping.Accessor;
			MembersMapping membersMapping = (MembersMapping)accessor.Mapping;
			MemberMapping[] members = membersMapping.Members;
			bool hasWrapperElement = membersMapping.HasWrapperElement;
			bool writeAccessors = membersMapping.WriteAccessors;
			string text = this.NextMethodName(accessor.Name);
			base.Writer.WriteLine();
			base.Writer.Write("public object[] ");
			base.Writer.Write(text);
			base.Writer.WriteLine("() {");
			IndentedWriter writer = base.Writer;
			int indent = writer.Indent;
			writer.Indent = indent + 1;
			base.Writer.WriteLine("Reader.MoveToContent();");
			base.Writer.Write("object[] p = new object[");
			base.Writer.Write(members.Length.ToString(CultureInfo.InvariantCulture));
			base.Writer.WriteLine("];");
			this.InitializeValueTypes("p", members);
			if (hasWrapperElement)
			{
				this.WriteReadNonRoots();
				if (membersMapping.ValidateRpcWrapperElement)
				{
					base.Writer.Write("if (!");
					this.WriteXmlNodeEqual("Reader", accessor.Name, (accessor.Form == XmlSchemaForm.Qualified) ? accessor.Namespace : "");
					base.Writer.WriteLine(") throw CreateUnknownNodeException();");
				}
				base.Writer.WriteLine("bool isEmptyWrapper = Reader.IsEmptyElement;");
				base.Writer.WriteLine("Reader.ReadStartElement();");
			}
			XmlSerializationReaderCodeGen.Member[] array = new XmlSerializationReaderCodeGen.Member[members.Length];
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
				XmlSerializationReaderCodeGen.Member member = new XmlSerializationReaderCodeGen.Member(this, text2, arraySource, "a", i, memberMapping);
				if (!memberMapping.IsSequence)
				{
					member.ParamsReadSource = "paramsRead[" + i.ToString(CultureInfo.InvariantCulture) + "]";
				}
				array[i] = member;
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
			}
			string fixupMethodName = "fixup_" + text;
			bool flag = this.WriteMemberFixupBegin(array, fixupMethodName, "p");
			if (array.Length != 0 && array[0].Mapping.IsReturnValue)
			{
				base.Writer.WriteLine("IsReturnValue = true;");
			}
			string text3 = (!hasWrapperElement && !writeAccessors) ? "hrefList" : null;
			if (text3 != null)
			{
				this.WriteInitCheckTypeHrefList(text3);
			}
			this.WriteParamsRead(members.Length);
			int loopIndex = this.WriteWhileNotLoopStart();
			IndentedWriter writer2 = base.Writer;
			indent = writer2.Indent;
			writer2.Indent = indent + 1;
			string elementElseString = (text3 == null) ? "UnknownNode((object)p);" : "if (Reader.GetAttribute(\"id\", null) != null) { ReadReferencedElement(); } else { UnknownNode((object)p); }";
			this.WriteMemberElements(array, elementElseString, "UnknownNode((object)p);", null, null, text3);
			base.Writer.WriteLine("Reader.MoveToContent();");
			this.WriteWhileLoopEnd(loopIndex);
			if (hasWrapperElement)
			{
				base.Writer.WriteLine("if (!isEmptyWrapper) ReadEndElement();");
			}
			if (text3 != null)
			{
				this.WriteHandleHrefList(array, text3);
			}
			base.Writer.WriteLine("ReadReferencedElements();");
			base.Writer.WriteLine("return p;");
			IndentedWriter writer3 = base.Writer;
			indent = writer3.Indent;
			writer3.Indent = indent - 1;
			base.Writer.WriteLine("}");
			if (flag)
			{
				this.WriteFixupMethod(fixupMethodName, array, "object[]", false, false, "p");
			}
			return text;
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x000B2854 File Offset: 0x000B0A54
		private void WriteCreateCollection(TypeDesc td, string source)
		{
			bool useReflection = td.UseReflection;
			string text = ((td.ArrayElementTypeDesc == null) ? "object" : td.ArrayElementTypeDesc.CSharpName) + "[]";
			bool flag = td.ArrayElementTypeDesc != null && td.ArrayElementTypeDesc.UseReflection;
			if (flag)
			{
				text = typeof(Array).FullName;
			}
			base.Writer.Write(text);
			base.Writer.Write(" ");
			base.Writer.Write("ci =");
			base.Writer.Write("(" + text + ")");
			base.Writer.Write(source);
			base.Writer.WriteLine(";");
			base.Writer.WriteLine("for (int i = 0; i < ci.Length; i++) {");
			IndentedWriter writer = base.Writer;
			int indent = writer.Indent;
			writer.Indent = indent + 1;
			base.Writer.Write(base.RaCodeGen.GetStringForMethod("c", td.CSharpName, "Add", useReflection));
			if (!flag)
			{
				base.Writer.Write("ci[i]");
			}
			else
			{
				base.Writer.Write(base.RaCodeGen.GetReflectionVariable(typeof(Array).FullName, "0") + "[ci , i]");
			}
			if (useReflection)
			{
				base.Writer.WriteLine("}");
			}
			base.Writer.WriteLine(");");
			IndentedWriter writer2 = base.Writer;
			indent = writer2.Indent;
			writer2.Indent = indent - 1;
			base.Writer.WriteLine("}");
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x000B29F8 File Offset: 0x000B0BF8
		private string GenerateTypeElement(XmlTypeMapping xmlTypeMapping)
		{
			ElementAccessor accessor = xmlTypeMapping.Accessor;
			TypeMapping mapping = accessor.Mapping;
			string text = this.NextMethodName(accessor.Name);
			base.Writer.WriteLine();
			base.Writer.Write("public object ");
			base.Writer.Write(text);
			base.Writer.WriteLine("() {");
			IndentedWriter writer = base.Writer;
			int indent = writer.Indent;
			writer.Indent = indent + 1;
			base.Writer.WriteLine("object o = null;");
			XmlSerializationReaderCodeGen.Member[] array = new XmlSerializationReaderCodeGen.Member[]
			{
				new XmlSerializationReaderCodeGen.Member(this, "o", "o", "a", 0, new MemberMapping
				{
					TypeDesc = mapping.TypeDesc,
					Elements = new ElementAccessor[]
					{
						accessor
					}
				})
			};
			base.Writer.WriteLine("Reader.MoveToContent();");
			string elseString = "UnknownNode(null, " + this.ExpectedElements(array) + ");";
			this.WriteMemberElements(array, "throw CreateUnknownNodeException();", elseString, accessor.Any ? array[0] : null, null, null);
			if (accessor.IsSoap)
			{
				base.Writer.WriteLine("Referenced(o);");
				base.Writer.WriteLine("ReadReferencedElements();");
			}
			base.Writer.WriteLine("return (object)o;");
			IndentedWriter writer2 = base.Writer;
			indent = writer2.Indent;
			writer2.Indent = indent - 1;
			base.Writer.WriteLine("}");
			return text;
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x000B2B6C File Offset: 0x000B0D6C
		private string NextMethodName(string name)
		{
			string str = "Read";
			int nextMethodNumber = base.NextMethodNumber + 1;
			base.NextMethodNumber = nextMethodNumber;
			return str + nextMethodNumber.ToString(CultureInfo.InvariantCulture) + "_" + CodeIdentifier.MakeValidInternal(name);
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x000B2BAC File Offset: 0x000B0DAC
		private string NextIdName(string name)
		{
			string str = "id";
			int num = this.nextIdNumber + 1;
			this.nextIdNumber = num;
			return str + num.ToString(CultureInfo.InvariantCulture) + "_" + CodeIdentifier.MakeValidInternal(name);
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x000B2BEC File Offset: 0x000B0DEC
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
				if (mapping.IsSoap)
				{
					base.Writer.Write("(");
					base.Writer.Write(mapping.TypeDesc.CSharpName);
					base.Writer.Write(")");
				}
				base.Writer.Write(text);
				base.Writer.Write("(");
				if (!mapping.IsSoap)
				{
					base.Writer.Write(source);
				}
				base.Writer.Write(")");
				return;
			}
			else
			{
				if (mapping.TypeDesc == base.StringTypeDesc)
				{
					base.Writer.Write(source);
					return;
				}
				if (!(mapping.TypeDesc.FormatterName == "String"))
				{
					if (!mapping.TypeDesc.HasCustomFormatter)
					{
						base.Writer.Write(typeof(XmlConvert).FullName);
						base.Writer.Write(".");
					}
					base.Writer.Write("To");
					base.Writer.Write(mapping.TypeDesc.FormatterName);
					base.Writer.Write("(");
					base.Writer.Write(source);
					base.Writer.Write(")");
					return;
				}
				if (mapping.TypeDesc.CollapseWhitespace)
				{
					base.Writer.Write("CollapseWhitespace(");
					base.Writer.Write(source);
					base.Writer.Write(")");
					return;
				}
				base.Writer.Write(source);
				return;
			}
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x000B2DB4 File Offset: 0x000B0FB4
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

		// Token: 0x06001E19 RID: 7705 RVA: 0x000B2E14 File Offset: 0x000B1014
		private string WriteHashtable(EnumMapping mapping, string typeName)
		{
			CodeIdentifier.CheckValidIdentifier(typeName);
			string text = this.MakeUnique(mapping, typeName + "Values");
			if (text == null)
			{
				return CodeIdentifier.GetCSharpName(typeName);
			}
			string s = this.MakeUnique(mapping, "_" + text);
			text = CodeIdentifier.GetCSharpName(text);
			base.Writer.WriteLine();
			base.Writer.Write(typeof(Hashtable).FullName);
			base.Writer.Write(" ");
			base.Writer.Write(s);
			base.Writer.WriteLine(";");
			base.Writer.WriteLine();
			base.Writer.Write("internal ");
			base.Writer.Write(typeof(Hashtable).FullName);
			base.Writer.Write(" ");
			base.Writer.Write(text);
			base.Writer.WriteLine(" {");
			IndentedWriter writer = base.Writer;
			int indent = writer.Indent;
			writer.Indent = indent + 1;
			base.Writer.WriteLine("get {");
			IndentedWriter writer2 = base.Writer;
			indent = writer2.Indent;
			writer2.Indent = indent + 1;
			base.Writer.Write("if ((object)");
			base.Writer.Write(s);
			base.Writer.WriteLine(" == null) {");
			IndentedWriter writer3 = base.Writer;
			indent = writer3.Indent;
			writer3.Indent = indent + 1;
			base.Writer.Write(typeof(Hashtable).FullName);
			base.Writer.Write(" h = new ");
			base.Writer.Write(typeof(Hashtable).FullName);
			base.Writer.WriteLine("();");
			ConstantMapping[] constants = mapping.Constants;
			for (int i = 0; i < constants.Length; i++)
			{
				base.Writer.Write("h.Add(");
				base.WriteQuotedCSharpString(constants[i].XmlName);
				if (!mapping.TypeDesc.UseReflection)
				{
					base.Writer.Write(", (long)");
					base.Writer.Write(mapping.TypeDesc.CSharpName);
					base.Writer.Write(".@");
					CodeIdentifier.CheckValidIdentifier(constants[i].Name);
					base.Writer.Write(constants[i].Name);
				}
				else
				{
					base.Writer.Write(", ");
					base.Writer.Write(constants[i].Value.ToString(CultureInfo.InvariantCulture) + "L");
				}
				base.Writer.WriteLine(");");
			}
			base.Writer.Write(s);
			base.Writer.WriteLine(" = h;");
			IndentedWriter writer4 = base.Writer;
			indent = writer4.Indent;
			writer4.Indent = indent - 1;
			base.Writer.WriteLine("}");
			base.Writer.Write("return ");
			base.Writer.Write(s);
			base.Writer.WriteLine(";");
			IndentedWriter writer5 = base.Writer;
			indent = writer5.Indent;
			writer5.Indent = indent - 1;
			base.Writer.WriteLine("}");
			IndentedWriter writer6 = base.Writer;
			indent = writer6.Indent;
			writer6.Indent = indent - 1;
			base.Writer.WriteLine("}");
			return text;
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x000B3188 File Offset: 0x000B1388
		private void WriteEnumMethod(EnumMapping mapping)
		{
			string s = null;
			if (mapping.IsFlags)
			{
				s = this.WriteHashtable(mapping, mapping.TypeDesc.Name);
			}
			string s2 = (string)base.MethodNames[mapping];
			base.Writer.WriteLine();
			bool useReflection = mapping.TypeDesc.UseReflection;
			string csharpName = mapping.TypeDesc.CSharpName;
			int indent;
			if (mapping.IsSoap)
			{
				base.Writer.Write("object");
				base.Writer.Write(" ");
				base.Writer.Write(s2);
				base.Writer.WriteLine("() {");
				IndentedWriter writer = base.Writer;
				indent = writer.Indent;
				writer.Indent = indent + 1;
				base.Writer.WriteLine("string s = Reader.ReadElementString();");
			}
			else
			{
				base.Writer.Write(useReflection ? "object" : csharpName);
				base.Writer.Write(" ");
				base.Writer.Write(s2);
				base.Writer.WriteLine("(string s) {");
				IndentedWriter writer2 = base.Writer;
				indent = writer2.Indent;
				writer2.Indent = indent + 1;
			}
			ConstantMapping[] constants = mapping.Constants;
			if (mapping.IsFlags)
			{
				if (useReflection)
				{
					base.Writer.Write("return ");
					base.Writer.Write(typeof(Enum).FullName);
					base.Writer.Write(".ToObject(");
					base.Writer.Write(base.RaCodeGen.GetStringForTypeof(csharpName, useReflection));
					base.Writer.Write(", ToEnum(s, ");
					base.Writer.Write(s);
					base.Writer.Write(", ");
					base.WriteQuotedCSharpString(csharpName);
					base.Writer.WriteLine("));");
				}
				else
				{
					base.Writer.Write("return (");
					base.Writer.Write(csharpName);
					base.Writer.Write(")ToEnum(s, ");
					base.Writer.Write(s);
					base.Writer.Write(", ");
					base.WriteQuotedCSharpString(csharpName);
					base.Writer.WriteLine(");");
				}
			}
			else
			{
				base.Writer.WriteLine("switch (s) {");
				IndentedWriter writer3 = base.Writer;
				indent = writer3.Indent;
				writer3.Indent = indent + 1;
				Hashtable hashtable = new Hashtable();
				foreach (ConstantMapping constantMapping in constants)
				{
					CodeIdentifier.CheckValidIdentifier(constantMapping.Name);
					if (hashtable[constantMapping.XmlName] == null)
					{
						base.Writer.Write("case ");
						base.WriteQuotedCSharpString(constantMapping.XmlName);
						base.Writer.Write(": return ");
						base.Writer.Write(base.RaCodeGen.GetStringForEnumMember(csharpName, constantMapping.Name, useReflection));
						base.Writer.WriteLine(";");
						hashtable[constantMapping.XmlName] = constantMapping.XmlName;
					}
				}
				base.Writer.Write("default: throw CreateUnknownConstantException(s, ");
				base.Writer.Write(base.RaCodeGen.GetStringForTypeof(csharpName, useReflection));
				base.Writer.WriteLine(");");
				IndentedWriter writer4 = base.Writer;
				indent = writer4.Indent;
				writer4.Indent = indent - 1;
				base.Writer.WriteLine("}");
			}
			IndentedWriter writer5 = base.Writer;
			indent = writer5.Indent;
			writer5.Indent = indent - 1;
			base.Writer.WriteLine("}");
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x000B3528 File Offset: 0x000B1728
		private void WriteDerivedTypes(StructMapping mapping, bool isTypedReturn, string returnTypeName)
		{
			for (StructMapping structMapping = mapping.DerivedMappings; structMapping != null; structMapping = structMapping.NextDerivedMapping)
			{
				base.Writer.Write("else if (");
				this.WriteQNameEqual("xsiType", structMapping.TypeName, structMapping.Namespace);
				base.Writer.WriteLine(")");
				IndentedWriter writer = base.Writer;
				int indent = writer.Indent;
				writer.Indent = indent + 1;
				string s = base.ReferenceMapping(structMapping);
				base.Writer.Write("return ");
				if (structMapping.TypeDesc.UseReflection && isTypedReturn)
				{
					base.Writer.Write("(" + returnTypeName + ")");
				}
				base.Writer.Write(s);
				base.Writer.Write("(");
				if (structMapping.TypeDesc.IsNullable)
				{
					base.Writer.Write("isNullable, ");
				}
				base.Writer.WriteLine("false);");
				IndentedWriter writer2 = base.Writer;
				indent = writer2.Indent;
				writer2.Indent = indent - 1;
				this.WriteDerivedTypes(structMapping, isTypedReturn, returnTypeName);
			}
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x000B3644 File Offset: 0x000B1844
		private void WriteEnumAndArrayTypes()
		{
			TypeScope[] scopes = base.Scopes;
			for (int i = 0; i < scopes.Length; i++)
			{
				foreach (object obj in scopes[i].TypeMappings)
				{
					Mapping mapping = (Mapping)obj;
					if (!mapping.IsSoap)
					{
						if (mapping is EnumMapping)
						{
							EnumMapping enumMapping = (EnumMapping)mapping;
							base.Writer.Write("else if (");
							this.WriteQNameEqual("xsiType", enumMapping.TypeName, enumMapping.Namespace);
							base.Writer.WriteLine(") {");
							IndentedWriter writer = base.Writer;
							int indent = writer.Indent;
							writer.Indent = indent + 1;
							base.Writer.WriteLine("Reader.ReadStartElement();");
							string s = base.ReferenceMapping(enumMapping);
							base.Writer.Write("object e = ");
							base.Writer.Write(s);
							base.Writer.WriteLine("(CollapseWhitespace(Reader.ReadString()));");
							base.Writer.WriteLine("ReadEndElement();");
							base.Writer.WriteLine("return e;");
							IndentedWriter writer2 = base.Writer;
							indent = writer2.Indent;
							writer2.Indent = indent - 1;
							base.Writer.WriteLine("}");
						}
						else if (mapping is ArrayMapping)
						{
							ArrayMapping arrayMapping = (ArrayMapping)mapping;
							if (arrayMapping.TypeDesc.HasDefaultConstructor)
							{
								base.Writer.Write("else if (");
								this.WriteQNameEqual("xsiType", arrayMapping.TypeName, arrayMapping.Namespace);
								base.Writer.WriteLine(") {");
								IndentedWriter writer3 = base.Writer;
								int indent = writer3.Indent;
								writer3.Indent = indent + 1;
								XmlSerializationReaderCodeGen.Member member = new XmlSerializationReaderCodeGen.Member(this, "a", "z", 0, new MemberMapping
								{
									TypeDesc = arrayMapping.TypeDesc,
									Elements = arrayMapping.Elements
								});
								TypeDesc typeDesc = arrayMapping.TypeDesc;
								string csharpName = arrayMapping.TypeDesc.CSharpName;
								if (typeDesc.UseReflection)
								{
									if (typeDesc.IsArray)
									{
										base.Writer.Write(typeof(Array).FullName);
									}
									else
									{
										base.Writer.Write("object");
									}
								}
								else
								{
									base.Writer.Write(csharpName);
								}
								base.Writer.Write(" a = ");
								if (arrayMapping.TypeDesc.IsValueType)
								{
									base.Writer.Write(base.RaCodeGen.GetStringForCreateInstance(csharpName, typeDesc.UseReflection, false, false));
									base.Writer.WriteLine(";");
								}
								else
								{
									base.Writer.WriteLine("null;");
								}
								this.WriteArray(member.Source, member.ArrayName, arrayMapping, false, false, -1);
								base.Writer.WriteLine("return a;");
								IndentedWriter writer4 = base.Writer;
								indent = writer4.Indent;
								writer4.Indent = indent - 1;
								base.Writer.WriteLine("}");
							}
						}
					}
				}
			}
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x000B398C File Offset: 0x000B1B8C
		private void WriteNullableMethod(NullableMapping nullableMapping)
		{
			string s = (string)base.MethodNames[nullableMapping];
			bool useReflection = nullableMapping.BaseMapping.TypeDesc.UseReflection;
			string s2 = useReflection ? "object" : nullableMapping.TypeDesc.CSharpName;
			base.Writer.WriteLine();
			base.Writer.Write(s2);
			base.Writer.Write(" ");
			base.Writer.Write(s);
			base.Writer.WriteLine("(bool checkType) {");
			IndentedWriter writer = base.Writer;
			int indent = writer.Indent;
			writer.Indent = indent + 1;
			base.Writer.Write(s2);
			base.Writer.Write(" o = ");
			if (useReflection)
			{
				base.Writer.Write("null");
			}
			else
			{
				base.Writer.Write("default(");
				base.Writer.Write(s2);
				base.Writer.Write(")");
			}
			base.Writer.WriteLine(";");
			base.Writer.WriteLine("if (ReadNull())");
			IndentedWriter writer2 = base.Writer;
			indent = writer2.Indent;
			writer2.Indent = indent + 1;
			base.Writer.WriteLine("return o;");
			IndentedWriter writer3 = base.Writer;
			indent = writer3.Indent;
			writer3.Indent = indent - 1;
			this.WriteElement("o", null, null, new ElementAccessor
			{
				Mapping = nullableMapping.BaseMapping,
				Any = false,
				IsNullable = nullableMapping.BaseMapping.TypeDesc.IsNullable
			}, null, null, false, false, -1, -1);
			base.Writer.WriteLine("return o;");
			IndentedWriter writer4 = base.Writer;
			indent = writer4.Indent;
			writer4.Indent = indent - 1;
			base.Writer.WriteLine("}");
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x000B3B57 File Offset: 0x000B1D57
		private void WriteStructMethod(StructMapping structMapping)
		{
			if (structMapping.IsSoap)
			{
				this.WriteEncodedStructMethod(structMapping);
				return;
			}
			this.WriteLiteralStructMethod(structMapping);
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x000B3B70 File Offset: 0x000B1D70
		private void WriteLiteralStructMethod(StructMapping structMapping)
		{
			string s = (string)base.MethodNames[structMapping];
			bool useReflection = structMapping.TypeDesc.UseReflection;
			string text = useReflection ? "object" : structMapping.TypeDesc.CSharpName;
			base.Writer.WriteLine();
			base.Writer.Write(text);
			base.Writer.Write(" ");
			base.Writer.Write(s);
			base.Writer.Write("(");
			if (structMapping.TypeDesc.IsNullable)
			{
				base.Writer.Write("bool isNullable, ");
			}
			base.Writer.WriteLine("bool checkType) {");
			IndentedWriter writer = base.Writer;
			int indent = writer.Indent;
			writer.Indent = indent + 1;
			base.Writer.Write(typeof(XmlQualifiedName).FullName);
			base.Writer.WriteLine(" xsiType = checkType ? GetXsiType() : null;");
			base.Writer.WriteLine("bool isNull = false;");
			if (structMapping.TypeDesc.IsNullable)
			{
				base.Writer.WriteLine("if (isNullable) isNull = ReadNull();");
			}
			base.Writer.WriteLine("if (checkType) {");
			if (structMapping.TypeDesc.IsRoot)
			{
				IndentedWriter writer2 = base.Writer;
				indent = writer2.Indent;
				writer2.Indent = indent + 1;
				base.Writer.WriteLine("if (isNull) {");
				IndentedWriter writer3 = base.Writer;
				indent = writer3.Indent;
				writer3.Indent = indent + 1;
				base.Writer.WriteLine("if (xsiType != null) return (" + text + ")ReadTypedNull(xsiType);");
				base.Writer.Write("else return ");
				if (structMapping.TypeDesc.IsValueType)
				{
					base.Writer.Write(base.RaCodeGen.GetStringForCreateInstance(structMapping.TypeDesc.CSharpName, useReflection, false, false));
					base.Writer.WriteLine(";");
				}
				else
				{
					base.Writer.WriteLine("null;");
				}
				IndentedWriter writer4 = base.Writer;
				indent = writer4.Indent;
				writer4.Indent = indent - 1;
				base.Writer.WriteLine("}");
			}
			base.Writer.Write("if (xsiType == null");
			if (!structMapping.TypeDesc.IsRoot)
			{
				base.Writer.Write(" || ");
				this.WriteQNameEqual("xsiType", structMapping.TypeName, structMapping.Namespace);
			}
			base.Writer.WriteLine(") {");
			if (structMapping.TypeDesc.IsRoot)
			{
				IndentedWriter writer5 = base.Writer;
				indent = writer5.Indent;
				writer5.Indent = indent + 1;
				base.Writer.WriteLine("return ReadTypedPrimitive(new System.Xml.XmlQualifiedName(\"anyType\", \"http://www.w3.org/2001/XMLSchema\"));");
				IndentedWriter writer6 = base.Writer;
				indent = writer6.Indent;
				writer6.Indent = indent - 1;
			}
			base.Writer.WriteLine("}");
			this.WriteDerivedTypes(structMapping, !useReflection && !structMapping.TypeDesc.IsRoot, text);
			if (structMapping.TypeDesc.IsRoot)
			{
				this.WriteEnumAndArrayTypes();
			}
			base.Writer.WriteLine("else");
			IndentedWriter writer7 = base.Writer;
			indent = writer7.Indent;
			writer7.Indent = indent + 1;
			if (structMapping.TypeDesc.IsRoot)
			{
				base.Writer.Write("return ReadTypedPrimitive((");
			}
			else
			{
				base.Writer.Write("throw CreateUnknownTypeException((");
			}
			base.Writer.Write(typeof(XmlQualifiedName).FullName);
			base.Writer.WriteLine(")xsiType);");
			IndentedWriter writer8 = base.Writer;
			indent = writer8.Indent;
			writer8.Indent = indent - 1;
			base.Writer.WriteLine("}");
			if (structMapping.TypeDesc.IsNullable)
			{
				base.Writer.WriteLine("if (isNull) return null;");
			}
			if (structMapping.TypeDesc.IsAbstract)
			{
				base.Writer.Write("throw CreateAbstractTypeException(");
				base.WriteQuotedCSharpString(structMapping.TypeName);
				base.Writer.Write(", ");
				base.WriteQuotedCSharpString(structMapping.Namespace);
				base.Writer.WriteLine(");");
			}
			else
			{
				if (structMapping.TypeDesc.Type != null && typeof(XmlSchemaObject).IsAssignableFrom(structMapping.TypeDesc.Type))
				{
					base.Writer.WriteLine("DecodeName = false;");
				}
				this.WriteCreateMapping(structMapping, "o");
				MemberMapping[] settableMembers = TypeScope.GetSettableMembers(structMapping);
				XmlSerializationReaderCodeGen.Member member = null;
				XmlSerializationReaderCodeGen.Member member2 = null;
				XmlSerializationReaderCodeGen.Member member3 = null;
				bool flag = structMapping.HasExplicitSequence();
				ArrayList arrayList = new ArrayList(settableMembers.Length);
				ArrayList arrayList2 = new ArrayList(settableMembers.Length);
				ArrayList arrayList3 = new ArrayList(settableMembers.Length);
				for (int i = 0; i < settableMembers.Length; i++)
				{
					MemberMapping memberMapping = settableMembers[i];
					CodeIdentifier.CheckValidIdentifier(memberMapping.Name);
					string stringForMember = base.RaCodeGen.GetStringForMember("o", memberMapping.Name, structMapping.TypeDesc);
					XmlSerializationReaderCodeGen.Member member4 = new XmlSerializationReaderCodeGen.Member(this, stringForMember, "a", i, memberMapping, this.GetChoiceIdentifierSource(memberMapping, "o", structMapping.TypeDesc));
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
						arrayList3.Add(new XmlSerializationReaderCodeGen.Member(this, stringForMember, stringForMember, "a", i, memberMapping, this.GetChoiceIdentifierSource(memberMapping, "o", structMapping.TypeDesc))
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
				XmlSerializationReaderCodeGen.Member[] members = (XmlSerializationReaderCodeGen.Member[])arrayList.ToArray(typeof(XmlSerializationReaderCodeGen.Member));
				XmlSerializationReaderCodeGen.Member[] members2 = (XmlSerializationReaderCodeGen.Member[])arrayList2.ToArray(typeof(XmlSerializationReaderCodeGen.Member));
				XmlSerializationReaderCodeGen.Member[] members3 = (XmlSerializationReaderCodeGen.Member[])arrayList3.ToArray(typeof(XmlSerializationReaderCodeGen.Member));
				this.WriteMemberBegin(members);
				this.WriteParamsRead(settableMembers.Length);
				this.WriteAttributes(members3, member3, "UnknownNode", "(object)o");
				if (member3 != null)
				{
					this.WriteMemberEnd(members);
				}
				base.Writer.WriteLine("Reader.MoveToElement();");
				base.Writer.WriteLine("if (Reader.IsEmptyElement) {");
				IndentedWriter writer9 = base.Writer;
				indent = writer9.Indent;
				writer9.Indent = indent + 1;
				base.Writer.WriteLine("Reader.Skip();");
				this.WriteMemberEnd(members2);
				base.Writer.WriteLine("return o;");
				IndentedWriter writer10 = base.Writer;
				indent = writer10.Indent;
				writer10.Indent = indent - 1;
				base.Writer.WriteLine("}");
				base.Writer.WriteLine("Reader.ReadStartElement();");
				if (this.IsSequence(members3))
				{
					base.Writer.WriteLine("int state = 0;");
				}
				int loopIndex = this.WriteWhileNotLoopStart();
				IndentedWriter writer11 = base.Writer;
				indent = writer11.Indent;
				writer11.Indent = indent + 1;
				string text2 = "UnknownNode((object)o, " + this.ExpectedElements(members3) + ");";
				this.WriteMemberElements(members3, text2, text2, member2, member, null);
				base.Writer.WriteLine("Reader.MoveToContent();");
				this.WriteWhileLoopEnd(loopIndex);
				this.WriteMemberEnd(members2);
				base.Writer.WriteLine("ReadEndElement();");
				base.Writer.WriteLine("return o;");
			}
			IndentedWriter writer12 = base.Writer;
			indent = writer12.Indent;
			writer12.Indent = indent - 1;
			base.Writer.WriteLine("}");
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x000B44C0 File Offset: 0x000B26C0
		private void WriteEncodedStructMethod(StructMapping structMapping)
		{
			if (structMapping.TypeDesc.IsRoot)
			{
				return;
			}
			bool useReflection = structMapping.TypeDesc.UseReflection;
			string text = (string)base.MethodNames[structMapping];
			base.Writer.WriteLine();
			base.Writer.Write("object");
			base.Writer.Write(" ");
			base.Writer.Write(text);
			base.Writer.Write("(");
			base.Writer.WriteLine(") {");
			IndentedWriter writer = base.Writer;
			int indent = writer.Indent;
			writer.Indent = indent + 1;
			XmlSerializationReaderCodeGen.Member[] array;
			bool flag;
			string fixupMethodName;
			if (structMapping.TypeDesc.IsAbstract)
			{
				base.Writer.Write("throw CreateAbstractTypeException(");
				base.WriteQuotedCSharpString(structMapping.TypeName);
				base.Writer.Write(", ");
				base.WriteQuotedCSharpString(structMapping.Namespace);
				base.Writer.WriteLine(");");
				array = new XmlSerializationReaderCodeGen.Member[0];
				flag = false;
				fixupMethodName = null;
			}
			else
			{
				this.WriteCreateMapping(structMapping, "o");
				MemberMapping[] settableMembers = TypeScope.GetSettableMembers(structMapping);
				array = new XmlSerializationReaderCodeGen.Member[settableMembers.Length];
				for (int i = 0; i < settableMembers.Length; i++)
				{
					MemberMapping memberMapping = settableMembers[i];
					CodeIdentifier.CheckValidIdentifier(memberMapping.Name);
					string stringForMember = base.RaCodeGen.GetStringForMember("o", memberMapping.Name, structMapping.TypeDesc);
					XmlSerializationReaderCodeGen.Member member = new XmlSerializationReaderCodeGen.Member(this, stringForMember, stringForMember, "a", i, memberMapping, this.GetChoiceIdentifierSource(memberMapping, "o", structMapping.TypeDesc));
					if (memberMapping.CheckSpecified == SpecifiedAccessor.ReadWrite)
					{
						member.CheckSpecifiedSource = base.RaCodeGen.GetStringForMember("o", memberMapping.Name + "Specified", structMapping.TypeDesc);
					}
					if (!memberMapping.IsSequence)
					{
						member.ParamsReadSource = "paramsRead[" + i.ToString(CultureInfo.InvariantCulture) + "]";
					}
					array[i] = member;
				}
				fixupMethodName = "fixup_" + text;
				flag = this.WriteMemberFixupBegin(array, fixupMethodName, "o");
				this.WriteParamsRead(settableMembers.Length);
				this.WriteAttributes(array, null, "UnknownNode", "(object)o");
				base.Writer.WriteLine("Reader.MoveToElement();");
				base.Writer.WriteLine("if (Reader.IsEmptyElement) { Reader.Skip(); return o; }");
				base.Writer.WriteLine("Reader.ReadStartElement();");
				int loopIndex = this.WriteWhileNotLoopStart();
				IndentedWriter writer2 = base.Writer;
				indent = writer2.Indent;
				writer2.Indent = indent + 1;
				this.WriteMemberElements(array, "UnknownNode((object)o);", "UnknownNode((object)o);", null, null, null);
				base.Writer.WriteLine("Reader.MoveToContent();");
				this.WriteWhileLoopEnd(loopIndex);
				base.Writer.WriteLine("ReadEndElement();");
				base.Writer.WriteLine("return o;");
			}
			IndentedWriter writer3 = base.Writer;
			indent = writer3.Indent;
			writer3.Indent = indent - 1;
			base.Writer.WriteLine("}");
			if (flag)
			{
				this.WriteFixupMethod(fixupMethodName, array, structMapping.TypeDesc.CSharpName, structMapping.TypeDesc.UseReflection, true, "o");
			}
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x000B47E8 File Offset: 0x000B29E8
		private void WriteFixupMethod(string fixupMethodName, XmlSerializationReaderCodeGen.Member[] members, string typeName, bool useReflection, bool typed, string source)
		{
			base.Writer.WriteLine();
			base.Writer.Write("void ");
			base.Writer.Write(fixupMethodName);
			base.Writer.WriteLine("(object objFixup) {");
			IndentedWriter writer = base.Writer;
			int indent = writer.Indent;
			writer.Indent = indent + 1;
			base.Writer.WriteLine("Fixup fixup = (Fixup)objFixup;");
			this.WriteLocalDecl(typeName, source, "fixup.Source", useReflection);
			base.Writer.WriteLine("string[] ids = fixup.Ids;");
			foreach (XmlSerializationReaderCodeGen.Member member in members)
			{
				if (member.MultiRef)
				{
					string text = member.FixupIndex.ToString(CultureInfo.InvariantCulture);
					base.Writer.Write("if (ids[");
					base.Writer.Write(text);
					base.Writer.WriteLine("] != null) {");
					IndentedWriter writer2 = base.Writer;
					indent = writer2.Indent;
					writer2.Indent = indent + 1;
					string arraySource = member.ArraySource;
					string text2 = "GetTarget(ids[" + text + "])";
					TypeDesc typeDesc = member.Mapping.TypeDesc;
					if (typeDesc.IsCollection || typeDesc.IsEnumerable)
					{
						this.WriteAddCollectionFixup(typeDesc, member.Mapping.ReadOnly, arraySource, text2);
					}
					else
					{
						if (typed)
						{
							base.Writer.WriteLine("try {");
							IndentedWriter writer3 = base.Writer;
							indent = writer3.Indent;
							writer3.Indent = indent + 1;
							this.WriteSourceBeginTyped(arraySource, member.Mapping.TypeDesc);
						}
						else
						{
							this.WriteSourceBegin(arraySource);
						}
						base.Writer.Write(text2);
						this.WriteSourceEnd(arraySource);
						base.Writer.WriteLine(";");
						if (member.Mapping.CheckSpecified == SpecifiedAccessor.ReadWrite && member.CheckSpecifiedSource != null && member.CheckSpecifiedSource.Length > 0)
						{
							base.Writer.Write(member.CheckSpecifiedSource);
							base.Writer.WriteLine(" = true;");
						}
						if (typed)
						{
							this.WriteCatchCastException(member.Mapping.TypeDesc, text2, "ids[" + text + "]");
						}
					}
					IndentedWriter writer4 = base.Writer;
					indent = writer4.Indent;
					writer4.Indent = indent - 1;
					base.Writer.WriteLine("}");
				}
			}
			IndentedWriter writer5 = base.Writer;
			indent = writer5.Indent;
			writer5.Indent = indent - 1;
			base.Writer.WriteLine("}");
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x000B4A60 File Offset: 0x000B2C60
		private void WriteAddCollectionFixup(TypeDesc typeDesc, bool readOnly, string memberSource, string targetSource)
		{
			base.Writer.WriteLine("// get array of the collection items");
			bool useReflection = typeDesc.UseReflection;
			XmlSerializationReaderCodeGen.CreateCollectionInfo createCollectionInfo = (XmlSerializationReaderCodeGen.CreateCollectionInfo)this.createMethods[typeDesc];
			int num;
			if (createCollectionInfo == null)
			{
				string str = "create";
				num = this.nextCreateMethodNumber + 1;
				this.nextCreateMethodNumber = num;
				createCollectionInfo = new XmlSerializationReaderCodeGen.CreateCollectionInfo(str + num.ToString(CultureInfo.InvariantCulture) + "_" + typeDesc.Name, typeDesc);
				this.createMethods.Add(typeDesc, createCollectionInfo);
			}
			base.Writer.Write("if ((object)(");
			base.Writer.Write(memberSource);
			base.Writer.WriteLine(") == null) {");
			IndentedWriter writer = base.Writer;
			num = writer.Indent;
			writer.Indent = num + 1;
			if (readOnly)
			{
				base.Writer.Write("throw CreateReadOnlyCollectionException(");
				base.WriteQuotedCSharpString(typeDesc.CSharpName);
				base.Writer.WriteLine(");");
			}
			else
			{
				base.Writer.Write(memberSource);
				base.Writer.Write(" = ");
				base.Writer.Write(base.RaCodeGen.GetStringForCreateInstance(typeDesc.CSharpName, typeDesc.UseReflection, typeDesc.CannotNew, true));
				base.Writer.WriteLine(";");
			}
			IndentedWriter writer2 = base.Writer;
			num = writer2.Indent;
			writer2.Indent = num - 1;
			base.Writer.WriteLine("}");
			base.Writer.Write("CollectionFixup collectionFixup = new CollectionFixup(");
			base.Writer.Write(memberSource);
			base.Writer.Write(", ");
			base.Writer.Write("new ");
			base.Writer.Write(typeof(XmlSerializationCollectionFixupCallback).FullName);
			base.Writer.Write("(this.");
			base.Writer.Write(createCollectionInfo.Name);
			base.Writer.Write("), ");
			base.Writer.Write(targetSource);
			base.Writer.WriteLine(");");
			base.Writer.WriteLine("AddFixup(collectionFixup);");
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x000B4C80 File Offset: 0x000B2E80
		private void WriteCreateCollectionMethod(XmlSerializationReaderCodeGen.CreateCollectionInfo c)
		{
			base.Writer.Write("void ");
			base.Writer.Write(c.Name);
			base.Writer.WriteLine("(object collection, object collectionItems) {");
			IndentedWriter writer = base.Writer;
			int indent = writer.Indent;
			writer.Indent = indent + 1;
			base.Writer.WriteLine("if (collectionItems == null) return;");
			base.Writer.WriteLine("if (collection == null) return;");
			TypeDesc typeDesc = c.TypeDesc;
			bool useReflection = typeDesc.UseReflection;
			string csharpName = typeDesc.CSharpName;
			this.WriteLocalDecl(csharpName, "c", "collection", useReflection);
			this.WriteCreateCollection(typeDesc, "collectionItems");
			IndentedWriter writer2 = base.Writer;
			indent = writer2.Indent;
			writer2.Indent = indent - 1;
			base.Writer.WriteLine("}");
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x000B4D4C File Offset: 0x000B2F4C
		private void WriteQNameEqual(string source, string name, string ns)
		{
			base.Writer.Write("((object) ((");
			base.Writer.Write(typeof(XmlQualifiedName).FullName);
			base.Writer.Write(")");
			base.Writer.Write(source);
			base.Writer.Write(").Name == (object)");
			this.WriteID(name);
			base.Writer.Write(" && (object) ((");
			base.Writer.Write(typeof(XmlQualifiedName).FullName);
			base.Writer.Write(")");
			base.Writer.Write(source);
			base.Writer.Write(").Namespace == (object)");
			this.WriteID(ns);
			base.Writer.Write(")");
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x000B4E24 File Offset: 0x000B3024
		private void WriteXmlNodeEqual(string source, string name, string ns)
		{
			base.Writer.Write("(");
			if (name != null && name.Length > 0)
			{
				base.Writer.Write("(object) ");
				base.Writer.Write(source);
				base.Writer.Write(".LocalName == (object)");
				this.WriteID(name);
				base.Writer.Write(" && ");
			}
			base.Writer.Write("(object) ");
			base.Writer.Write(source);
			base.Writer.Write(".NamespaceURI == (object)");
			this.WriteID(ns);
			base.Writer.Write(")");
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x000B4ED4 File Offset: 0x000B30D4
		private void WriteID(string name)
		{
			if (name == null)
			{
				name = "";
			}
			string text = (string)this.idNames[name];
			if (text == null)
			{
				text = this.NextIdName(name);
				this.idNames.Add(name, text);
			}
			base.Writer.Write(text);
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x000B4F24 File Offset: 0x000B3124
		private void WriteAttributes(XmlSerializationReaderCodeGen.Member[] members, XmlSerializationReaderCodeGen.Member anyAttribute, string elseCall, string firstParam)
		{
			int num = 0;
			XmlSerializationReaderCodeGen.Member member = null;
			ArrayList arrayList = new ArrayList();
			base.Writer.WriteLine("while (Reader.MoveToNextAttribute()) {");
			IndentedWriter writer = base.Writer;
			int indent = writer.Indent;
			writer.Indent = indent + 1;
			foreach (XmlSerializationReaderCodeGen.Member member2 in members)
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
							base.Writer.Write("else ");
						}
						base.Writer.Write("if (");
						if (member2.ParamsReadSource != null)
						{
							base.Writer.Write("!");
							base.Writer.Write(member2.ParamsReadSource);
							base.Writer.Write(" && ");
						}
						if (attribute.IsSpecialXmlNamespace)
						{
							this.WriteXmlNodeEqual("Reader", attribute.Name, "http://www.w3.org/XML/1998/namespace");
						}
						else
						{
							this.WriteXmlNodeEqual("Reader", attribute.Name, (attribute.Form == XmlSchemaForm.Qualified) ? attribute.Namespace : "");
						}
						base.Writer.WriteLine(") {");
						IndentedWriter writer2 = base.Writer;
						indent = writer2.Indent;
						writer2.Indent = indent + 1;
						this.WriteAttribute(member2);
						IndentedWriter writer3 = base.Writer;
						indent = writer3.Indent;
						writer3.Indent = indent - 1;
						base.Writer.WriteLine("}");
					}
				}
			}
			if (num > 0)
			{
				base.Writer.Write("else ");
			}
			if (member != null)
			{
				base.Writer.WriteLine("if (IsXmlnsAttribute(Reader.Name)) {");
				IndentedWriter writer4 = base.Writer;
				indent = writer4.Indent;
				writer4.Indent = indent + 1;
				base.Writer.Write("if (");
				base.Writer.Write(member.Source);
				base.Writer.Write(" == null) ");
				base.Writer.Write(member.Source);
				base.Writer.Write(" = new ");
				base.Writer.Write(member.Mapping.TypeDesc.CSharpName);
				base.Writer.WriteLine("();");
				base.Writer.Write(string.Concat(new string[]
				{
					"((",
					member.Mapping.TypeDesc.CSharpName,
					")",
					member.ArraySource,
					")"
				}));
				base.Writer.WriteLine(".Add(Reader.Name.Length == 5 ? \"\" : Reader.LocalName, Reader.Value);");
				IndentedWriter writer5 = base.Writer;
				indent = writer5.Indent;
				writer5.Indent = indent - 1;
				base.Writer.WriteLine("}");
				base.Writer.WriteLine("else {");
				IndentedWriter writer6 = base.Writer;
				indent = writer6.Indent;
				writer6.Indent = indent + 1;
			}
			else
			{
				base.Writer.WriteLine("if (!IsXmlnsAttribute(Reader.Name)) {");
				IndentedWriter writer7 = base.Writer;
				indent = writer7.Indent;
				writer7.Indent = indent + 1;
			}
			if (anyAttribute != null)
			{
				base.Writer.Write(typeof(XmlAttribute).FullName);
				base.Writer.Write(" attr = ");
				base.Writer.Write("(");
				base.Writer.Write(typeof(XmlAttribute).FullName);
				base.Writer.WriteLine(") Document.ReadNode(Reader);");
				base.Writer.WriteLine("ParseWsdlArrayType(attr);");
				this.WriteAttribute(anyAttribute);
			}
			else
			{
				base.Writer.Write(elseCall);
				base.Writer.Write("(");
				base.Writer.Write(firstParam);
				if (arrayList.Count > 0)
				{
					base.Writer.Write(", ");
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
					base.WriteQuotedCSharpString(text);
				}
				base.Writer.WriteLine(");");
			}
			IndentedWriter writer8 = base.Writer;
			indent = writer8.Indent;
			writer8.Indent = indent - 1;
			base.Writer.WriteLine("}");
			IndentedWriter writer9 = base.Writer;
			indent = writer9.Indent;
			writer9.Indent = indent - 1;
			base.Writer.WriteLine("}");
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x000B5404 File Offset: 0x000B3604
		private void WriteAttribute(XmlSerializationReaderCodeGen.Member member)
		{
			AttributeAccessor attribute = member.Mapping.Attribute;
			if (attribute.Mapping is SpecialMapping)
			{
				SpecialMapping specialMapping = (SpecialMapping)attribute.Mapping;
				if (specialMapping.TypeDesc.Kind == TypeKind.Attribute)
				{
					this.WriteSourceBegin(member.ArraySource);
					base.Writer.Write("attr");
					this.WriteSourceEnd(member.ArraySource);
					base.Writer.WriteLine(";");
				}
				else
				{
					if (!specialMapping.TypeDesc.CanBeAttributeValue)
					{
						throw new InvalidOperationException(Res.GetString("Internal error."));
					}
					base.Writer.Write("if (attr is ");
					base.Writer.Write(typeof(XmlAttribute).FullName);
					base.Writer.WriteLine(") {");
					IndentedWriter writer = base.Writer;
					int indent = writer.Indent;
					writer.Indent = indent + 1;
					this.WriteSourceBegin(member.ArraySource);
					base.Writer.Write("(");
					base.Writer.Write(typeof(XmlAttribute).FullName);
					base.Writer.Write(")attr");
					this.WriteSourceEnd(member.ArraySource);
					base.Writer.WriteLine(";");
					IndentedWriter writer2 = base.Writer;
					indent = writer2.Indent;
					writer2.Indent = indent - 1;
					base.Writer.WriteLine("}");
				}
			}
			else if (attribute.IsList)
			{
				base.Writer.WriteLine("string listValues = Reader.Value;");
				base.Writer.WriteLine("string[] vals = listValues.Split(null);");
				base.Writer.WriteLine("for (int i = 0; i < vals.Length; i++) {");
				IndentedWriter writer3 = base.Writer;
				int indent = writer3.Indent;
				writer3.Indent = indent + 1;
				string arraySource = this.GetArraySource(member.Mapping.TypeDesc, member.ArrayName);
				this.WriteSourceBegin(arraySource);
				this.WritePrimitive(attribute.Mapping, "vals[i]");
				this.WriteSourceEnd(arraySource);
				base.Writer.WriteLine(";");
				IndentedWriter writer4 = base.Writer;
				indent = writer4.Indent;
				writer4.Indent = indent - 1;
				base.Writer.WriteLine("}");
			}
			else
			{
				this.WriteSourceBegin(member.ArraySource);
				this.WritePrimitive(attribute.Mapping, attribute.IsList ? "vals[i]" : "Reader.Value");
				this.WriteSourceEnd(member.ArraySource);
				base.Writer.WriteLine(";");
			}
			if (member.Mapping.CheckSpecified == SpecifiedAccessor.ReadWrite && member.CheckSpecifiedSource != null && member.CheckSpecifiedSource.Length > 0)
			{
				base.Writer.Write(member.CheckSpecifiedSource);
				base.Writer.WriteLine(" = true;");
			}
			if (member.ParamsReadSource != null)
			{
				base.Writer.Write(member.ParamsReadSource);
				base.Writer.WriteLine(" = true;");
			}
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x000B56F4 File Offset: 0x000B38F4
		private bool WriteMemberFixupBegin(XmlSerializationReaderCodeGen.Member[] members, string fixupMethodName, string source)
		{
			int num = 0;
			foreach (XmlSerializationReaderCodeGen.Member member in members)
			{
				if (member.Mapping.Elements.Length != 0)
				{
					TypeMapping mapping = member.Mapping.Elements[0].Mapping;
					if (mapping is StructMapping || mapping is ArrayMapping || mapping is PrimitiveMapping || mapping is NullableMapping)
					{
						member.MultiRef = true;
						member.FixupIndex = num++;
					}
				}
			}
			if (num > 0)
			{
				base.Writer.Write("Fixup fixup = new Fixup(");
				base.Writer.Write(source);
				base.Writer.Write(", ");
				base.Writer.Write("new ");
				base.Writer.Write(typeof(XmlSerializationFixupCallback).FullName);
				base.Writer.Write("(this.");
				base.Writer.Write(fixupMethodName);
				base.Writer.Write("), ");
				base.Writer.Write(num.ToString(CultureInfo.InvariantCulture));
				base.Writer.WriteLine(");");
				base.Writer.WriteLine("AddFixup(fixup);");
				return true;
			}
			return false;
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x000B582C File Offset: 0x000B3A2C
		private void WriteMemberBegin(XmlSerializationReaderCodeGen.Member[] members)
		{
			foreach (XmlSerializationReaderCodeGen.Member member in members)
			{
				if (member.IsArrayLike)
				{
					string arrayName = member.ArrayName;
					string s = "c" + arrayName;
					TypeDesc typeDesc = member.Mapping.TypeDesc;
					string csharpName = typeDesc.CSharpName;
					if (member.Mapping.TypeDesc.IsArray)
					{
						this.WriteArrayLocalDecl(typeDesc.CSharpName, arrayName, "null", typeDesc);
						base.Writer.Write("int ");
						base.Writer.Write(s);
						base.Writer.WriteLine(" = 0;");
						if (member.Mapping.ChoiceIdentifier != null)
						{
							this.WriteArrayLocalDecl(member.Mapping.ChoiceIdentifier.Mapping.TypeDesc.CSharpName + "[]", member.ChoiceArrayName, "null", member.Mapping.ChoiceIdentifier.Mapping.TypeDesc);
							base.Writer.Write("int c");
							base.Writer.Write(member.ChoiceArrayName);
							base.Writer.WriteLine(" = 0;");
						}
					}
					else
					{
						bool useReflection = typeDesc.UseReflection;
						if (member.Source[member.Source.Length - 1] == '(' || member.Source[member.Source.Length - 1] == '{')
						{
							this.WriteCreateInstance(csharpName, arrayName, useReflection, typeDesc.CannotNew);
							base.Writer.Write(member.Source);
							base.Writer.Write(arrayName);
							if (member.Source[member.Source.Length - 1] == '{')
							{
								base.Writer.WriteLine("});");
							}
							else
							{
								base.Writer.WriteLine(");");
							}
						}
						else
						{
							if (member.IsList && !member.Mapping.ReadOnly && member.Mapping.TypeDesc.IsNullable)
							{
								base.Writer.Write("if ((object)(");
								base.Writer.Write(member.Source);
								base.Writer.Write(") == null) ");
								if (!member.Mapping.TypeDesc.HasDefaultConstructor)
								{
									base.Writer.Write("throw CreateReadOnlyCollectionException(");
									base.WriteQuotedCSharpString(member.Mapping.TypeDesc.CSharpName);
									base.Writer.WriteLine(");");
								}
								else
								{
									base.Writer.Write(member.Source);
									base.Writer.Write(" = ");
									base.Writer.Write(base.RaCodeGen.GetStringForCreateInstance(csharpName, useReflection, typeDesc.CannotNew, true));
									base.Writer.WriteLine(";");
								}
							}
							this.WriteLocalDecl(csharpName, arrayName, member.Source, useReflection);
						}
					}
				}
			}
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x000B5B30 File Offset: 0x000B3D30
		private string ExpectedElements(XmlSerializationReaderCodeGen.Member[] members)
		{
			if (this.IsSequence(members))
			{
				return "null";
			}
			string text = string.Empty;
			bool flag = true;
			foreach (XmlSerializationReaderCodeGen.Member member in members)
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
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			ReflectionAwareCodeGen.WriteQuotedCSharpString(new IndentedWriter(stringWriter, true), text);
			return stringWriter.ToString();
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x000B5C50 File Offset: 0x000B3E50
		private void WriteMemberElements(XmlSerializationReaderCodeGen.Member[] members, string elementElseString, string elseString, XmlSerializationReaderCodeGen.Member anyElement, XmlSerializationReaderCodeGen.Member anyText, string checkTypeHrefsSource)
		{
			bool flag = checkTypeHrefsSource != null && checkTypeHrefsSource.Length > 0;
			if (anyText != null)
			{
				base.Writer.WriteLine("string tmp = null;");
			}
			base.Writer.Write("if (Reader.NodeType == ");
			base.Writer.Write(typeof(XmlNodeType).FullName);
			base.Writer.WriteLine(".Element) {");
			IndentedWriter writer = base.Writer;
			int indent = writer.Indent;
			writer.Indent = indent + 1;
			if (flag)
			{
				this.WriteIfNotSoapRoot(elementElseString + " continue;");
				this.WriteMemberElementsCheckType(checkTypeHrefsSource);
			}
			else
			{
				this.WriteMemberElementsIf(members, anyElement, elementElseString, null);
			}
			IndentedWriter writer2 = base.Writer;
			indent = writer2.Indent;
			writer2.Indent = indent - 1;
			base.Writer.WriteLine("}");
			if (anyText != null)
			{
				this.WriteMemberText(anyText, elseString);
			}
			base.Writer.WriteLine("else {");
			IndentedWriter writer3 = base.Writer;
			indent = writer3.Indent;
			writer3.Indent = indent + 1;
			base.Writer.WriteLine(elseString);
			IndentedWriter writer4 = base.Writer;
			indent = writer4.Indent;
			writer4.Indent = indent - 1;
			base.Writer.WriteLine("}");
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x000B5D84 File Offset: 0x000B3F84
		private void WriteMemberText(XmlSerializationReaderCodeGen.Member anyText, string elseString)
		{
			base.Writer.Write("else if (Reader.NodeType == ");
			base.Writer.Write(typeof(XmlNodeType).FullName);
			base.Writer.WriteLine(".Text || ");
			base.Writer.Write("Reader.NodeType == ");
			base.Writer.Write(typeof(XmlNodeType).FullName);
			base.Writer.WriteLine(".CDATA || ");
			base.Writer.Write("Reader.NodeType == ");
			base.Writer.Write(typeof(XmlNodeType).FullName);
			base.Writer.WriteLine(".Whitespace || ");
			base.Writer.Write("Reader.NodeType == ");
			base.Writer.Write(typeof(XmlNodeType).FullName);
			base.Writer.WriteLine(".SignificantWhitespace) {");
			IndentedWriter writer = base.Writer;
			int indent = writer.Indent;
			writer.Indent = indent + 1;
			if (anyText != null)
			{
				this.WriteText(anyText);
			}
			else
			{
				base.Writer.Write(elseString);
				base.Writer.WriteLine(";");
			}
			IndentedWriter writer2 = base.Writer;
			indent = writer2.Indent;
			writer2.Indent = indent - 1;
			base.Writer.WriteLine("}");
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x000B5EDC File Offset: 0x000B40DC
		private void WriteText(XmlSerializationReaderCodeGen.Member member)
		{
			TextAccessor text = member.Mapping.Text;
			if (text.Mapping is SpecialMapping)
			{
				SpecialMapping specialMapping = (SpecialMapping)text.Mapping;
				this.WriteSourceBeginTyped(member.ArraySource, specialMapping.TypeDesc);
				if (specialMapping.TypeDesc.Kind != TypeKind.Node)
				{
					throw new InvalidOperationException(Res.GetString("Internal error."));
				}
				base.Writer.Write("Document.CreateTextNode(Reader.ReadString())");
				this.WriteSourceEnd(member.ArraySource);
			}
			else
			{
				if (member.IsArrayLike)
				{
					this.WriteSourceBegin(member.ArraySource);
					if (text.Mapping.TypeDesc.CollapseWhitespace)
					{
						base.Writer.Write("CollapseWhitespace(Reader.ReadString())");
					}
					else
					{
						base.Writer.Write("Reader.ReadString()");
					}
				}
				else if (text.Mapping.TypeDesc == base.StringTypeDesc || text.Mapping.TypeDesc.FormatterName == "String")
				{
					base.Writer.Write("tmp = ReadString(tmp, ");
					if (text.Mapping.TypeDesc.CollapseWhitespace)
					{
						base.Writer.WriteLine("true);");
					}
					else
					{
						base.Writer.WriteLine("false);");
					}
					this.WriteSourceBegin(member.ArraySource);
					base.Writer.Write("tmp");
				}
				else
				{
					this.WriteSourceBegin(member.ArraySource);
					this.WritePrimitive(text.Mapping, "Reader.ReadString()");
				}
				this.WriteSourceEnd(member.ArraySource);
			}
			base.Writer.WriteLine(";");
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x000B607C File Offset: 0x000B427C
		private void WriteMemberElementsCheckType(string checkTypeHrefsSource)
		{
			base.Writer.WriteLine("string refElemId = null;");
			base.Writer.WriteLine("object refElem = ReadReferencingElement(null, null, true, out refElemId);");
			base.Writer.WriteLine("if (refElemId != null) {");
			IndentedWriter writer = base.Writer;
			int indent = writer.Indent;
			writer.Indent = indent + 1;
			base.Writer.Write(checkTypeHrefsSource);
			base.Writer.WriteLine(".Add(refElemId);");
			base.Writer.Write(checkTypeHrefsSource);
			base.Writer.WriteLine("IsObject.Add(false);");
			IndentedWriter writer2 = base.Writer;
			indent = writer2.Indent;
			writer2.Indent = indent - 1;
			base.Writer.WriteLine("}");
			base.Writer.WriteLine("else if (refElem != null) {");
			IndentedWriter writer3 = base.Writer;
			indent = writer3.Indent;
			writer3.Indent = indent + 1;
			base.Writer.Write(checkTypeHrefsSource);
			base.Writer.WriteLine(".Add(refElem);");
			base.Writer.Write(checkTypeHrefsSource);
			base.Writer.WriteLine("IsObject.Add(true);");
			IndentedWriter writer4 = base.Writer;
			indent = writer4.Indent;
			writer4.Indent = indent - 1;
			base.Writer.WriteLine("}");
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x000B61B0 File Offset: 0x000B43B0
		private void WriteMemberElementsElse(XmlSerializationReaderCodeGen.Member anyElement, string elementElseString)
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
			base.Writer.WriteLine(elementElseString);
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x000B6240 File Offset: 0x000B4440
		private bool IsSequence(XmlSerializationReaderCodeGen.Member[] members)
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

		// Token: 0x06001E32 RID: 7730 RVA: 0x000B627C File Offset: 0x000B447C
		private void WriteMemberElementsIf(XmlSerializationReaderCodeGen.Member[] members, XmlSerializationReaderCodeGen.Member anyElement, string elementElseString, string checkTypeSource)
		{
			bool flag = checkTypeSource != null && checkTypeSource.Length > 0;
			int num = 0;
			bool flag2 = this.IsSequence(members);
			if (flag2)
			{
				base.Writer.WriteLine("switch (state) {");
			}
			int num2 = 0;
			foreach (XmlSerializationReaderCodeGen.Member member in members)
			{
				if (member.Mapping.Xmlns == null && !member.Mapping.Ignore && (!flag2 || (!member.Mapping.IsText && !member.Mapping.IsAttribute)))
				{
					bool flag3 = true;
					ChoiceIdentifierAccessor choiceIdentifier = member.Mapping.ChoiceIdentifier;
					ElementAccessor[] elements = member.Mapping.Elements;
					for (int j = 0; j < elements.Length; j++)
					{
						ElementAccessor elementAccessor = elements[j];
						string ns = (elementAccessor.Form == XmlSchemaForm.Qualified) ? elementAccessor.Namespace : "";
						if (flag2 || !elementAccessor.Any || (elementAccessor.Name != null && elementAccessor.Name.Length != 0))
						{
							int indent;
							if (!flag3 || (!flag2 && num > 0))
							{
								base.Writer.Write("else ");
							}
							else if (flag2)
							{
								base.Writer.Write("case ");
								base.Writer.Write(num2.ToString(CultureInfo.InvariantCulture));
								base.Writer.WriteLine(":");
								IndentedWriter writer = base.Writer;
								indent = writer.Indent;
								writer.Indent = indent + 1;
							}
							num++;
							flag3 = false;
							base.Writer.Write("if (");
							if (member.ParamsReadSource != null)
							{
								base.Writer.Write("!");
								base.Writer.Write(member.ParamsReadSource);
								base.Writer.Write(" && ");
							}
							if (flag)
							{
								if (elementAccessor.Mapping is NullableMapping)
								{
									TypeDesc typeDesc = ((NullableMapping)elementAccessor.Mapping).BaseMapping.TypeDesc;
									base.Writer.Write(base.RaCodeGen.GetStringForTypeof(typeDesc.CSharpName, typeDesc.UseReflection));
								}
								else
								{
									base.Writer.Write(base.RaCodeGen.GetStringForTypeof(elementAccessor.Mapping.TypeDesc.CSharpName, elementAccessor.Mapping.TypeDesc.UseReflection));
								}
								base.Writer.Write(".IsAssignableFrom(");
								base.Writer.Write(checkTypeSource);
								base.Writer.Write("Type)");
							}
							else
							{
								if (member.Mapping.IsReturnValue)
								{
									base.Writer.Write("(IsReturnValue || ");
								}
								if (flag2 && elementAccessor.Any && elementAccessor.AnyNamespaces == null)
								{
									base.Writer.Write("true");
								}
								else
								{
									this.WriteXmlNodeEqual("Reader", elementAccessor.Name, ns);
								}
								if (member.Mapping.IsReturnValue)
								{
									base.Writer.Write(")");
								}
							}
							base.Writer.WriteLine(") {");
							IndentedWriter writer2 = base.Writer;
							indent = writer2.Indent;
							writer2.Indent = indent + 1;
							if (flag)
							{
								if (elementAccessor.Mapping.TypeDesc.IsValueType || elementAccessor.Mapping is NullableMapping)
								{
									base.Writer.Write("if (");
									base.Writer.Write(checkTypeSource);
									base.Writer.WriteLine(" != null) {");
									IndentedWriter writer3 = base.Writer;
									indent = writer3.Indent;
									writer3.Indent = indent + 1;
								}
								if (elementAccessor.Mapping is NullableMapping)
								{
									this.WriteSourceBegin(member.ArraySource);
									TypeDesc typeDesc2 = ((NullableMapping)elementAccessor.Mapping).BaseMapping.TypeDesc;
									base.Writer.Write(base.RaCodeGen.GetStringForCreateInstance(elementAccessor.Mapping.TypeDesc.CSharpName, elementAccessor.Mapping.TypeDesc.UseReflection, false, true, "(" + typeDesc2.CSharpName + ")" + checkTypeSource));
								}
								else
								{
									this.WriteSourceBeginTyped(member.ArraySource, elementAccessor.Mapping.TypeDesc);
									base.Writer.Write(checkTypeSource);
								}
								this.WriteSourceEnd(member.ArraySource);
								base.Writer.WriteLine(";");
								if (elementAccessor.Mapping.TypeDesc.IsValueType)
								{
									IndentedWriter writer4 = base.Writer;
									indent = writer4.Indent;
									writer4.Indent = indent - 1;
									base.Writer.WriteLine("}");
								}
								if (member.FixupIndex >= 0)
								{
									base.Writer.Write("fixup.Ids[");
									base.Writer.Write(member.FixupIndex.ToString(CultureInfo.InvariantCulture));
									base.Writer.Write("] = ");
									base.Writer.Write(checkTypeSource);
									base.Writer.WriteLine("Id;");
								}
							}
							else
							{
								this.WriteElement(member.ArraySource, member.ArrayName, member.ChoiceArraySource, elementAccessor, choiceIdentifier, (member.Mapping.CheckSpecified == SpecifiedAccessor.ReadWrite) ? member.CheckSpecifiedSource : null, member.IsList && member.Mapping.TypeDesc.IsNullable, member.Mapping.ReadOnly, member.FixupIndex, j);
							}
							if (member.Mapping.IsReturnValue)
							{
								base.Writer.WriteLine("IsReturnValue = false;");
							}
							if (member.ParamsReadSource != null)
							{
								base.Writer.Write(member.ParamsReadSource);
								base.Writer.WriteLine(" = true;");
							}
							IndentedWriter writer5 = base.Writer;
							indent = writer5.Indent;
							writer5.Indent = indent - 1;
							base.Writer.WriteLine("}");
						}
					}
					if (flag2)
					{
						int indent;
						if (member.IsArrayLike)
						{
							base.Writer.WriteLine("else {");
							IndentedWriter writer6 = base.Writer;
							indent = writer6.Indent;
							writer6.Indent = indent + 1;
						}
						num2++;
						base.Writer.Write("state = ");
						base.Writer.Write(num2.ToString(CultureInfo.InvariantCulture));
						base.Writer.WriteLine(";");
						if (member.IsArrayLike)
						{
							IndentedWriter writer7 = base.Writer;
							indent = writer7.Indent;
							writer7.Indent = indent - 1;
							base.Writer.WriteLine("}");
						}
						base.Writer.WriteLine("break;");
						IndentedWriter writer8 = base.Writer;
						indent = writer8.Indent;
						writer8.Indent = indent - 1;
					}
				}
			}
			if (num > 0)
			{
				if (flag2)
				{
					base.Writer.WriteLine("default:");
				}
				else
				{
					base.Writer.WriteLine("else {");
				}
				IndentedWriter writer9 = base.Writer;
				int indent = writer9.Indent;
				writer9.Indent = indent + 1;
			}
			this.WriteMemberElementsElse(anyElement, elementElseString);
			if (num > 0)
			{
				if (flag2)
				{
					base.Writer.WriteLine("break;");
				}
				IndentedWriter writer10 = base.Writer;
				int indent = writer10.Indent;
				writer10.Indent = indent - 1;
				base.Writer.WriteLine("}");
			}
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x000B69BB File Offset: 0x000B4BBB
		private string GetArraySource(TypeDesc typeDesc, string arrayName)
		{
			return this.GetArraySource(typeDesc, arrayName, false);
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x000B69C8 File Offset: 0x000B4BC8
		private string GetArraySource(TypeDesc typeDesc, string arrayName, bool multiRef)
		{
			string text = "c" + arrayName;
			string text2 = "";
			if (multiRef)
			{
				text2 = "soap = (System.Object[])EnsureArrayIndex(soap, " + text + "+2, typeof(System.Object)); ";
			}
			bool useReflection = typeDesc.UseReflection;
			if (typeDesc.IsArray)
			{
				string csharpName = typeDesc.ArrayElementTypeDesc.CSharpName;
				bool useReflection2 = typeDesc.ArrayElementTypeDesc.UseReflection;
				string text3 = useReflection ? "" : ("(" + csharpName + "[])");
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
					base.RaCodeGen.GetStringForTypeof(csharpName, useReflection2),
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
			return base.RaCodeGen.GetStringForMethod(arrayName, typeDesc.CSharpName, "Add", useReflection);
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x000B6B25 File Offset: 0x000B4D25
		private void WriteMemberEnd(XmlSerializationReaderCodeGen.Member[] members)
		{
			this.WriteMemberEnd(members, false);
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x000B6B30 File Offset: 0x000B4D30
		private void WriteMemberEnd(XmlSerializationReaderCodeGen.Member[] members, bool soapRefs)
		{
			foreach (XmlSerializationReaderCodeGen.Member member in members)
			{
				if (member.IsArrayLike)
				{
					TypeDesc typeDesc = member.Mapping.TypeDesc;
					if (typeDesc.IsArray)
					{
						this.WriteSourceBegin(member.Source);
						if (soapRefs)
						{
							base.Writer.Write(" soap[1] = ");
						}
						string text = member.ArrayName;
						string s = "c" + text;
						bool useReflection = typeDesc.ArrayElementTypeDesc.UseReflection;
						string csharpName = typeDesc.ArrayElementTypeDesc.CSharpName;
						if (!useReflection)
						{
							base.Writer.Write("(" + csharpName + "[])");
						}
						base.Writer.Write("ShrinkArray(");
						base.Writer.Write(text);
						base.Writer.Write(", ");
						base.Writer.Write(s);
						base.Writer.Write(", ");
						base.Writer.Write(base.RaCodeGen.GetStringForTypeof(csharpName, useReflection));
						base.Writer.Write(", ");
						this.WriteBooleanValue(member.IsNullable);
						base.Writer.Write(")");
						this.WriteSourceEnd(member.Source);
						base.Writer.WriteLine(";");
						if (member.Mapping.ChoiceIdentifier != null)
						{
							this.WriteSourceBegin(member.ChoiceSource);
							text = member.ChoiceArrayName;
							s = "c" + text;
							bool useReflection2 = member.Mapping.ChoiceIdentifier.Mapping.TypeDesc.UseReflection;
							string csharpName2 = member.Mapping.ChoiceIdentifier.Mapping.TypeDesc.CSharpName;
							if (!useReflection2)
							{
								base.Writer.Write("(" + csharpName2 + "[])");
							}
							base.Writer.Write("ShrinkArray(");
							base.Writer.Write(text);
							base.Writer.Write(", ");
							base.Writer.Write(s);
							base.Writer.Write(", ");
							base.Writer.Write(base.RaCodeGen.GetStringForTypeof(csharpName2, useReflection2));
							base.Writer.Write(", ");
							this.WriteBooleanValue(member.IsNullable);
							base.Writer.Write(")");
							this.WriteSourceEnd(member.ChoiceSource);
							base.Writer.WriteLine(";");
						}
					}
					else if (typeDesc.IsValueType)
					{
						base.Writer.Write(member.Source);
						base.Writer.Write(" = ");
						base.Writer.Write(member.ArrayName);
						base.Writer.WriteLine(";");
					}
				}
			}
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x000B6E10 File Offset: 0x000B5010
		private void WriteSourceBeginTyped(string source, TypeDesc typeDesc)
		{
			this.WriteSourceBegin(source);
			if (typeDesc != null && !typeDesc.UseReflection)
			{
				base.Writer.Write("(");
				base.Writer.Write(typeDesc.CSharpName);
				base.Writer.Write(")");
			}
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x000B6E60 File Offset: 0x000B5060
		private void WriteSourceBegin(string source)
		{
			base.Writer.Write(source);
			if (source[source.Length - 1] != '(' && source[source.Length - 1] != '{')
			{
				base.Writer.Write(" = ");
			}
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x000B6EB0 File Offset: 0x000B50B0
		private void WriteSourceEnd(string source)
		{
			if (source[source.Length - 1] == '(')
			{
				base.Writer.Write(")");
				return;
			}
			if (source[source.Length - 1] == '{')
			{
				base.Writer.Write("})");
			}
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x000B6F04 File Offset: 0x000B5104
		private void WriteArray(string source, string arrayName, ArrayMapping arrayMapping, bool readOnly, bool isNullable, int fixupIndex)
		{
			int indent;
			if (!arrayMapping.IsSoap)
			{
				base.Writer.WriteLine("if (!ReadNull()) {");
				IndentedWriter writer = base.Writer;
				indent = writer.Indent;
				writer.Indent = indent + 1;
				XmlSerializationReaderCodeGen.Member member = new XmlSerializationReaderCodeGen.Member(this, source, arrayName, 0, new MemberMapping
				{
					Elements = arrayMapping.Elements,
					TypeDesc = arrayMapping.TypeDesc,
					ReadOnly = readOnly
				}, false);
				member.IsNullable = false;
				XmlSerializationReaderCodeGen.Member[] members = new XmlSerializationReaderCodeGen.Member[]
				{
					member
				};
				this.WriteMemberBegin(members);
				if (readOnly)
				{
					base.Writer.Write("if (((object)(");
					base.Writer.Write(member.ArrayName);
					base.Writer.Write(") == null) || ");
				}
				else
				{
					base.Writer.Write("if (");
				}
				base.Writer.WriteLine("(Reader.IsEmptyElement)) {");
				IndentedWriter writer2 = base.Writer;
				indent = writer2.Indent;
				writer2.Indent = indent + 1;
				base.Writer.WriteLine("Reader.Skip();");
				IndentedWriter writer3 = base.Writer;
				indent = writer3.Indent;
				writer3.Indent = indent - 1;
				base.Writer.WriteLine("}");
				base.Writer.WriteLine("else {");
				IndentedWriter writer4 = base.Writer;
				indent = writer4.Indent;
				writer4.Indent = indent + 1;
				base.Writer.WriteLine("Reader.ReadStartElement();");
				int loopIndex = this.WriteWhileNotLoopStart();
				IndentedWriter writer5 = base.Writer;
				indent = writer5.Indent;
				writer5.Indent = indent + 1;
				string text = "UnknownNode(null, " + this.ExpectedElements(members) + ");";
				this.WriteMemberElements(members, text, text, null, null, null);
				base.Writer.WriteLine("Reader.MoveToContent();");
				this.WriteWhileLoopEnd(loopIndex);
				IndentedWriter writer6 = base.Writer;
				indent = writer6.Indent;
				writer6.Indent = indent - 1;
				base.Writer.WriteLine("ReadEndElement();");
				base.Writer.WriteLine("}");
				this.WriteMemberEnd(members, false);
				IndentedWriter writer7 = base.Writer;
				indent = writer7.Indent;
				writer7.Indent = indent - 1;
				base.Writer.WriteLine("}");
				if (isNullable)
				{
					base.Writer.WriteLine("else {");
					IndentedWriter writer8 = base.Writer;
					indent = writer8.Indent;
					writer8.Indent = indent + 1;
					member.IsNullable = true;
					this.WriteMemberBegin(members);
					this.WriteMemberEnd(members);
					IndentedWriter writer9 = base.Writer;
					indent = writer9.Indent;
					writer9.Indent = indent - 1;
					base.Writer.WriteLine("}");
				}
				return;
			}
			base.Writer.Write("object rre = ");
			base.Writer.Write((fixupIndex >= 0) ? "ReadReferencingElement" : "ReadReferencedElement");
			base.Writer.Write("(");
			this.WriteID(arrayMapping.TypeName);
			base.Writer.Write(", ");
			this.WriteID(arrayMapping.Namespace);
			if (fixupIndex >= 0)
			{
				base.Writer.Write(", ");
				base.Writer.Write("out fixup.Ids[");
				base.Writer.Write(fixupIndex.ToString(CultureInfo.InvariantCulture));
				base.Writer.Write("]");
			}
			base.Writer.WriteLine(");");
			TypeDesc typeDesc = arrayMapping.TypeDesc;
			if (typeDesc.IsEnumerable || typeDesc.IsCollection)
			{
				base.Writer.WriteLine("if (rre != null) {");
				IndentedWriter writer10 = base.Writer;
				indent = writer10.Indent;
				writer10.Indent = indent + 1;
				this.WriteAddCollectionFixup(typeDesc, readOnly, source, "rre");
				IndentedWriter writer11 = base.Writer;
				indent = writer11.Indent;
				writer11.Indent = indent - 1;
				base.Writer.WriteLine("}");
				return;
			}
			base.Writer.WriteLine("try {");
			IndentedWriter writer12 = base.Writer;
			indent = writer12.Indent;
			writer12.Indent = indent + 1;
			this.WriteSourceBeginTyped(source, arrayMapping.TypeDesc);
			base.Writer.Write("rre");
			this.WriteSourceEnd(source);
			base.Writer.WriteLine(";");
			this.WriteCatchCastException(arrayMapping.TypeDesc, "rre", null);
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x000B7328 File Offset: 0x000B5528
		private void WriteElement(string source, string arrayName, string choiceSource, ElementAccessor element, ChoiceIdentifierAccessor choice, string checkSpecified, bool checkForNull, bool readOnly, int fixupIndex, int elementIndex)
		{
			if (checkSpecified != null && checkSpecified.Length > 0)
			{
				base.Writer.Write(checkSpecified);
				base.Writer.WriteLine(" = true;");
			}
			if (element.Mapping is ArrayMapping)
			{
				this.WriteArray(source, arrayName, (ArrayMapping)element.Mapping, readOnly, element.IsNullable, fixupIndex);
			}
			else if (element.Mapping is NullableMapping)
			{
				string s = base.ReferenceMapping(element.Mapping);
				this.WriteSourceBegin(source);
				base.Writer.Write(s);
				base.Writer.Write("(true)");
				this.WriteSourceEnd(source);
				base.Writer.WriteLine(";");
			}
			else if (!element.Mapping.IsSoap && element.Mapping is PrimitiveMapping)
			{
				int indent;
				if (element.IsNullable)
				{
					base.Writer.WriteLine("if (ReadNull()) {");
					IndentedWriter writer = base.Writer;
					indent = writer.Indent;
					writer.Indent = indent + 1;
					this.WriteSourceBegin(source);
					if (element.Mapping.TypeDesc.IsValueType)
					{
						base.Writer.Write(base.RaCodeGen.GetStringForCreateInstance(element.Mapping.TypeDesc.CSharpName, element.Mapping.TypeDesc.UseReflection, false, false));
					}
					else
					{
						base.Writer.Write("null");
					}
					this.WriteSourceEnd(source);
					base.Writer.WriteLine(";");
					IndentedWriter writer2 = base.Writer;
					indent = writer2.Indent;
					writer2.Indent = indent - 1;
					base.Writer.WriteLine("}");
					base.Writer.Write("else ");
				}
				if (element.Default != null && element.Default != DBNull.Value && element.Mapping.TypeDesc.IsValueType)
				{
					base.Writer.WriteLine("if (Reader.IsEmptyElement) {");
					IndentedWriter writer3 = base.Writer;
					indent = writer3.Indent;
					writer3.Indent = indent + 1;
					base.Writer.WriteLine("Reader.Skip();");
					IndentedWriter writer4 = base.Writer;
					indent = writer4.Indent;
					writer4.Indent = indent - 1;
					base.Writer.WriteLine("}");
					base.Writer.WriteLine("else {");
				}
				else
				{
					base.Writer.WriteLine("{");
				}
				IndentedWriter writer5 = base.Writer;
				indent = writer5.Indent;
				writer5.Indent = indent + 1;
				if (element.Mapping.TypeDesc.Type == typeof(TimeSpan) && LocalAppContextSwitches.EnableTimeSpanSerialization)
				{
					base.Writer.WriteLine("if (Reader.IsEmptyElement) {");
					IndentedWriter writer6 = base.Writer;
					indent = writer6.Indent;
					writer6.Indent = indent + 1;
					base.Writer.WriteLine("Reader.Skip();");
					this.WriteSourceBegin(source);
					base.Writer.Write("default(System.TimeSpan)");
					this.WriteSourceEnd(source);
					base.Writer.WriteLine(";");
					IndentedWriter writer7 = base.Writer;
					indent = writer7.Indent;
					writer7.Indent = indent - 1;
					base.Writer.WriteLine("}");
					base.Writer.WriteLine("else {");
					IndentedWriter writer8 = base.Writer;
					indent = writer8.Indent;
					writer8.Indent = indent + 1;
					this.WriteSourceBegin(source);
					this.WritePrimitive(element.Mapping, "Reader.ReadElementString()");
					this.WriteSourceEnd(source);
					base.Writer.WriteLine(";");
					IndentedWriter writer9 = base.Writer;
					indent = writer9.Indent;
					writer9.Indent = indent - 1;
					base.Writer.WriteLine("}");
				}
				else
				{
					this.WriteSourceBegin(source);
					if (element.Mapping.TypeDesc == base.QnameTypeDesc)
					{
						base.Writer.Write("ReadElementQualifiedName()");
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
					this.WriteSourceEnd(source);
					base.Writer.WriteLine(";");
				}
				IndentedWriter writer10 = base.Writer;
				indent = writer10.Indent;
				writer10.Indent = indent - 1;
				base.Writer.WriteLine("}");
			}
			else if (element.Mapping is StructMapping || (element.Mapping.IsSoap && element.Mapping is PrimitiveMapping))
			{
				TypeMapping mapping = element.Mapping;
				if (mapping.IsSoap)
				{
					base.Writer.Write("object rre = ");
					base.Writer.Write((fixupIndex >= 0) ? "ReadReferencingElement" : "ReadReferencedElement");
					base.Writer.Write("(");
					this.WriteID(mapping.TypeName);
					base.Writer.Write(", ");
					this.WriteID(mapping.Namespace);
					if (fixupIndex >= 0)
					{
						base.Writer.Write(", out fixup.Ids[");
						base.Writer.Write(fixupIndex.ToString(CultureInfo.InvariantCulture));
						base.Writer.Write("]");
					}
					base.Writer.Write(")");
					this.WriteSourceEnd(source);
					base.Writer.WriteLine(";");
					int indent;
					if (mapping.TypeDesc.IsValueType)
					{
						base.Writer.WriteLine("if (rre != null) {");
						IndentedWriter writer11 = base.Writer;
						indent = writer11.Indent;
						writer11.Indent = indent + 1;
					}
					base.Writer.WriteLine("try {");
					IndentedWriter writer12 = base.Writer;
					indent = writer12.Indent;
					writer12.Indent = indent + 1;
					this.WriteSourceBeginTyped(source, mapping.TypeDesc);
					base.Writer.Write("rre");
					this.WriteSourceEnd(source);
					base.Writer.WriteLine(";");
					this.WriteCatchCastException(mapping.TypeDesc, "rre", null);
					base.Writer.Write("Referenced(");
					base.Writer.Write(source);
					base.Writer.WriteLine(");");
					if (mapping.TypeDesc.IsValueType)
					{
						IndentedWriter writer13 = base.Writer;
						indent = writer13.Indent;
						writer13.Indent = indent - 1;
						base.Writer.WriteLine("}");
					}
				}
				else
				{
					string s2 = base.ReferenceMapping(mapping);
					if (checkForNull)
					{
						base.Writer.Write("if ((object)(");
						base.Writer.Write(arrayName);
						base.Writer.Write(") == null) Reader.Skip(); else ");
					}
					this.WriteSourceBegin(source);
					base.Writer.Write(s2);
					base.Writer.Write("(");
					if (mapping.TypeDesc.IsNullable)
					{
						this.WriteBooleanValue(element.IsNullable);
						base.Writer.Write(", ");
					}
					base.Writer.Write("true");
					base.Writer.Write(")");
					this.WriteSourceEnd(source);
					base.Writer.WriteLine(";");
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
						base.Writer.Write(typeof(XmlQualifiedName).FullName);
						base.Writer.WriteLine(" tser = GetXsiType();");
						base.Writer.Write("if (tser == null");
						base.Writer.Write(" || ");
						this.WriteQNameEqual("tser", serializableMapping.XsiType.Name, serializableMapping.XsiType.Namespace);
						base.Writer.WriteLine(") {");
						IndentedWriter writer14 = base.Writer;
						int indent = writer14.Indent;
						writer14.Indent = indent + 1;
					}
					this.WriteSourceBeginTyped(source, serializableMapping.TypeDesc);
					base.Writer.Write("ReadSerializable(( ");
					base.Writer.Write(typeof(IXmlSerializable).FullName);
					base.Writer.Write(")");
					base.Writer.Write(base.RaCodeGen.GetStringForCreateInstance(serializableMapping.TypeDesc.CSharpName, serializableMapping.TypeDesc.UseReflection, serializableMapping.TypeDesc.CannotNew, false));
					bool flag = !element.Any && XmlSerializationCodeGen.IsWildcard(serializableMapping);
					if (flag)
					{
						base.Writer.WriteLine(", true");
					}
					base.Writer.Write(")");
					this.WriteSourceEnd(source);
					base.Writer.WriteLine(";");
					if (serializableMapping.DerivedMappings != null)
					{
						IndentedWriter writer15 = base.Writer;
						int indent = writer15.Indent;
						writer15.Indent = indent - 1;
						base.Writer.WriteLine("}");
						this.WriteDerivedSerializable(serializableMapping, serializableMapping, source, flag);
						this.WriteUnknownNode("UnknownNode", "null", null, true);
					}
				}
				else
				{
					bool flag2 = specialMapping.TypeDesc.FullName == typeof(XmlDocument).FullName;
					this.WriteSourceBeginTyped(source, specialMapping.TypeDesc);
					base.Writer.Write(flag2 ? "ReadXmlDocument(" : "ReadXmlNode(");
					base.Writer.Write(element.Any ? "false" : "true");
					base.Writer.Write(")");
					this.WriteSourceEnd(source);
					base.Writer.WriteLine(";");
				}
			}
			if (choice != null)
			{
				string csharpName = choice.Mapping.TypeDesc.CSharpName;
				base.Writer.Write(choiceSource);
				base.Writer.Write(" = ");
				CodeIdentifier.CheckValidIdentifier(choice.MemberIds[elementIndex]);
				base.Writer.Write(base.RaCodeGen.GetStringForEnumMember(csharpName, choice.MemberIds[elementIndex], choice.Mapping.TypeDesc.UseReflection));
				base.Writer.WriteLine(";");
			}
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x000B7DC4 File Offset: 0x000B5FC4
		private void WriteDerivedSerializable(SerializableMapping head, SerializableMapping mapping, string source, bool isWrappedAny)
		{
			if (mapping == null)
			{
				return;
			}
			for (SerializableMapping serializableMapping = mapping.DerivedMappings; serializableMapping != null; serializableMapping = serializableMapping.NextDerivedMapping)
			{
				base.Writer.Write("else if (tser == null");
				base.Writer.Write(" || ");
				this.WriteQNameEqual("tser", serializableMapping.XsiType.Name, serializableMapping.XsiType.Namespace);
				base.Writer.WriteLine(") {");
				IndentedWriter writer = base.Writer;
				int indent = writer.Indent;
				writer.Indent = indent + 1;
				if (serializableMapping.Type != null)
				{
					if (head.Type.IsAssignableFrom(serializableMapping.Type))
					{
						this.WriteSourceBeginTyped(source, head.TypeDesc);
						base.Writer.Write("ReadSerializable(( ");
						base.Writer.Write(typeof(IXmlSerializable).FullName);
						base.Writer.Write(")");
						base.Writer.Write(base.RaCodeGen.GetStringForCreateInstance(serializableMapping.TypeDesc.CSharpName, serializableMapping.TypeDesc.UseReflection, serializableMapping.TypeDesc.CannotNew, false));
						if (isWrappedAny)
						{
							base.Writer.WriteLine(", true");
						}
						base.Writer.Write(")");
						this.WriteSourceEnd(source);
						base.Writer.WriteLine(";");
					}
					else
					{
						base.Writer.Write("throw CreateBadDerivationException(");
						base.WriteQuotedCSharpString(serializableMapping.XsiType.Name);
						base.Writer.Write(", ");
						base.WriteQuotedCSharpString(serializableMapping.XsiType.Namespace);
						base.Writer.Write(", ");
						base.WriteQuotedCSharpString(head.XsiType.Name);
						base.Writer.Write(", ");
						base.WriteQuotedCSharpString(head.XsiType.Namespace);
						base.Writer.Write(", ");
						base.WriteQuotedCSharpString(serializableMapping.Type.FullName);
						base.Writer.Write(", ");
						base.WriteQuotedCSharpString(head.Type.FullName);
						base.Writer.WriteLine(");");
					}
				}
				else
				{
					IndentedWriter writer2 = base.Writer;
					string str = "// missing real mapping for ";
					XmlQualifiedName xsiType = serializableMapping.XsiType;
					writer2.WriteLine(str + ((xsiType != null) ? xsiType.ToString() : null));
					base.Writer.Write("throw CreateMissingIXmlSerializableType(");
					base.WriteQuotedCSharpString(serializableMapping.XsiType.Name);
					base.Writer.Write(", ");
					base.WriteQuotedCSharpString(serializableMapping.XsiType.Namespace);
					base.Writer.Write(", ");
					base.WriteQuotedCSharpString(head.Type.FullName);
					base.Writer.WriteLine(");");
				}
				IndentedWriter writer3 = base.Writer;
				indent = writer3.Indent;
				writer3.Indent = indent - 1;
				base.Writer.WriteLine("}");
				this.WriteDerivedSerializable(head, serializableMapping, source, isWrappedAny);
			}
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x000B80E0 File Offset: 0x000B62E0
		private int WriteWhileNotLoopStart()
		{
			base.Writer.WriteLine("Reader.MoveToContent();");
			int result = this.WriteWhileLoopStartCheck();
			base.Writer.Write("while (Reader.NodeType != ");
			base.Writer.Write(typeof(XmlNodeType).FullName);
			base.Writer.Write(".EndElement && Reader.NodeType != ");
			base.Writer.Write(typeof(XmlNodeType).FullName);
			base.Writer.WriteLine(".None) {");
			return result;
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x000B8168 File Offset: 0x000B6368
		private void WriteWhileLoopEnd(int loopIndex)
		{
			this.WriteWhileLoopEndCheck(loopIndex);
			IndentedWriter writer = base.Writer;
			int indent = writer.Indent;
			writer.Indent = indent - 1;
			base.Writer.WriteLine("}");
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x000B81A4 File Offset: 0x000B63A4
		private int WriteWhileLoopStartCheck()
		{
			base.Writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "int whileIterations{0} = 0;", this.nextWhileLoopIndex));
			base.Writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "int readerCount{0} = ReaderCount;", this.nextWhileLoopIndex));
			int num = this.nextWhileLoopIndex;
			this.nextWhileLoopIndex = num + 1;
			return num;
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x000B820C File Offset: 0x000B640C
		private void WriteWhileLoopEndCheck(int loopIndex)
		{
			base.Writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "CheckReaderCount(ref whileIterations{0}, ref readerCount{1});", loopIndex, loopIndex));
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x000B8234 File Offset: 0x000B6434
		private void WriteParamsRead(int length)
		{
			base.Writer.Write("bool[] paramsRead = new bool[");
			base.Writer.Write(length.ToString(CultureInfo.InvariantCulture));
			base.Writer.WriteLine("];");
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x000B8270 File Offset: 0x000B6470
		private void WriteReadNonRoots()
		{
			base.Writer.WriteLine("Reader.MoveToContent();");
			int loopIndex = this.WriteWhileLoopStartCheck();
			base.Writer.Write("while (Reader.NodeType == ");
			base.Writer.Write(typeof(XmlNodeType).FullName);
			base.Writer.WriteLine(".Element) {");
			IndentedWriter writer = base.Writer;
			int indent = writer.Indent;
			writer.Indent = indent + 1;
			base.Writer.Write("string root = Reader.GetAttribute(\"root\", \"");
			base.Writer.Write("http://schemas.xmlsoap.org/soap/encoding/");
			base.Writer.WriteLine("\");");
			base.Writer.Write("if (root == null || ");
			base.Writer.Write(typeof(XmlConvert).FullName);
			base.Writer.WriteLine(".ToBoolean(root)) break;");
			base.Writer.WriteLine("ReadReferencedElement();");
			base.Writer.WriteLine("Reader.MoveToContent();");
			this.WriteWhileLoopEnd(loopIndex);
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x000B8374 File Offset: 0x000B6574
		private void WriteBooleanValue(bool value)
		{
			base.Writer.Write(value ? "true" : "false");
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x000B8390 File Offset: 0x000B6590
		private void WriteInitCheckTypeHrefList(string source)
		{
			base.Writer.Write(typeof(ArrayList).FullName);
			base.Writer.Write(" ");
			base.Writer.Write(source);
			base.Writer.Write(" = new ");
			base.Writer.Write(typeof(ArrayList).FullName);
			base.Writer.WriteLine("();");
			base.Writer.Write(typeof(ArrayList).FullName);
			base.Writer.Write(" ");
			base.Writer.Write(source);
			base.Writer.Write("IsObject = new ");
			base.Writer.Write(typeof(ArrayList).FullName);
			base.Writer.WriteLine("();");
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x000B8480 File Offset: 0x000B6680
		private void WriteHandleHrefList(XmlSerializationReaderCodeGen.Member[] members, string listSource)
		{
			base.Writer.WriteLine("int isObjectIndex = 0;");
			base.Writer.Write("foreach (object obj in ");
			base.Writer.Write(listSource);
			base.Writer.WriteLine(") {");
			IndentedWriter writer = base.Writer;
			int indent = writer.Indent;
			writer.Indent = indent + 1;
			base.Writer.WriteLine("bool isReferenced = true;");
			base.Writer.Write("bool isObject = (bool)");
			base.Writer.Write(listSource);
			base.Writer.WriteLine("IsObject[isObjectIndex++];");
			base.Writer.WriteLine("object refObj = isObject ? obj : GetTarget((string)obj);");
			base.Writer.WriteLine("if (refObj == null) continue;");
			base.Writer.Write(typeof(Type).FullName);
			base.Writer.WriteLine(" refObjType = refObj.GetType();");
			base.Writer.WriteLine("string refObjId = null;");
			this.WriteMemberElementsIf(members, null, "isReferenced = false;", "refObj");
			base.Writer.WriteLine("if (isObject && isReferenced) Referenced(refObj); // need to mark this obj as ref'd since we didn't do GetTarget");
			IndentedWriter writer2 = base.Writer;
			indent = writer2.Indent;
			writer2.Indent = indent - 1;
			base.Writer.WriteLine("}");
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x000B85BC File Offset: 0x000B67BC
		private void WriteIfNotSoapRoot(string source)
		{
			base.Writer.Write("if (Reader.GetAttribute(\"root\", \"");
			base.Writer.Write("http://schemas.xmlsoap.org/soap/encoding/");
			base.Writer.WriteLine("\") == \"0\") {");
			IndentedWriter writer = base.Writer;
			int indent = writer.Indent;
			writer.Indent = indent + 1;
			base.Writer.WriteLine(source);
			IndentedWriter writer2 = base.Writer;
			indent = writer2.Indent;
			writer2.Indent = indent - 1;
			base.Writer.WriteLine("}");
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x000B8640 File Offset: 0x000B6840
		private void WriteCreateMapping(TypeMapping mapping, string local)
		{
			string csharpName = mapping.TypeDesc.CSharpName;
			bool useReflection = mapping.TypeDesc.UseReflection;
			bool cannotNew = mapping.TypeDesc.CannotNew;
			base.Writer.Write(useReflection ? "object" : csharpName);
			base.Writer.Write(" ");
			base.Writer.Write(local);
			base.Writer.WriteLine(";");
			if (cannotNew)
			{
				base.Writer.WriteLine("try {");
				IndentedWriter writer = base.Writer;
				int indent = writer.Indent;
				writer.Indent = indent + 1;
			}
			base.Writer.Write(local);
			base.Writer.Write(" = ");
			base.Writer.Write(base.RaCodeGen.GetStringForCreateInstance(csharpName, useReflection, mapping.TypeDesc.CannotNew, true));
			base.Writer.WriteLine(";");
			if (cannotNew)
			{
				this.WriteCatchException(typeof(MissingMethodException));
				IndentedWriter writer2 = base.Writer;
				int indent = writer2.Indent;
				writer2.Indent = indent + 1;
				base.Writer.Write("throw CreateInaccessibleConstructorException(");
				base.WriteQuotedCSharpString(csharpName);
				base.Writer.WriteLine(");");
				this.WriteCatchException(typeof(SecurityException));
				IndentedWriter writer3 = base.Writer;
				indent = writer3.Indent;
				writer3.Indent = indent + 1;
				base.Writer.Write("throw CreateCtorHasSecurityException(");
				base.WriteQuotedCSharpString(csharpName);
				base.Writer.WriteLine(");");
				IndentedWriter writer4 = base.Writer;
				indent = writer4.Indent;
				writer4.Indent = indent - 1;
				base.Writer.WriteLine("}");
			}
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x000B87EC File Offset: 0x000B69EC
		private void WriteCatchException(Type exceptionType)
		{
			IndentedWriter writer = base.Writer;
			int indent = writer.Indent;
			writer.Indent = indent - 1;
			base.Writer.WriteLine("}");
			base.Writer.Write("catch (");
			base.Writer.Write(exceptionType.FullName);
			base.Writer.WriteLine(") {");
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x000B8850 File Offset: 0x000B6A50
		private void WriteCatchCastException(TypeDesc typeDesc, string source, string id)
		{
			this.WriteCatchException(typeof(InvalidCastException));
			IndentedWriter writer = base.Writer;
			int indent = writer.Indent;
			writer.Indent = indent + 1;
			base.Writer.Write("throw CreateInvalidCastException(");
			base.Writer.Write(base.RaCodeGen.GetStringForTypeof(typeDesc.CSharpName, typeDesc.UseReflection));
			base.Writer.Write(", ");
			base.Writer.Write(source);
			if (id == null)
			{
				base.Writer.WriteLine(", null);");
			}
			else
			{
				base.Writer.Write(", (string)");
				base.Writer.Write(id);
				base.Writer.WriteLine(");");
			}
			IndentedWriter writer2 = base.Writer;
			indent = writer2.Indent;
			writer2.Indent = indent - 1;
			base.Writer.WriteLine("}");
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x000B8936 File Offset: 0x000B6B36
		private void WriteArrayLocalDecl(string typeName, string variableName, string initValue, TypeDesc arrayTypeDesc)
		{
			base.RaCodeGen.WriteArrayLocalDecl(typeName, variableName, initValue, arrayTypeDesc);
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x000B8948 File Offset: 0x000B6B48
		private void WriteCreateInstance(string escapedName, string source, bool useReflection, bool ctorInaccessible)
		{
			base.RaCodeGen.WriteCreateInstance(escapedName, source, useReflection, ctorInaccessible);
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x000B895A File Offset: 0x000B6B5A
		private void WriteLocalDecl(string typeFullName, string variableName, string initValue, bool useReflection)
		{
			base.RaCodeGen.WriteLocalDecl(typeFullName, variableName, initValue, useReflection);
		}

		// Token: 0x04001AC6 RID: 6854
		private Hashtable idNames = new Hashtable();

		// Token: 0x04001AC7 RID: 6855
		private Hashtable enums;

		// Token: 0x04001AC8 RID: 6856
		private Hashtable createMethods = new Hashtable();

		// Token: 0x04001AC9 RID: 6857
		private int nextCreateMethodNumber;

		// Token: 0x04001ACA RID: 6858
		private int nextIdNumber;

		// Token: 0x04001ACB RID: 6859
		private int nextWhileLoopIndex;

		// Token: 0x020002F3 RID: 755
		private class CreateCollectionInfo
		{
			// Token: 0x06001E4D RID: 7757 RVA: 0x000B896C File Offset: 0x000B6B6C
			internal CreateCollectionInfo(string name, TypeDesc td)
			{
				this.name = name;
				this.td = td;
			}

			// Token: 0x170005E8 RID: 1512
			// (get) Token: 0x06001E4E RID: 7758 RVA: 0x000B8982 File Offset: 0x000B6B82
			internal string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x170005E9 RID: 1513
			// (get) Token: 0x06001E4F RID: 7759 RVA: 0x000B898A File Offset: 0x000B6B8A
			internal TypeDesc TypeDesc
			{
				get
				{
					return this.td;
				}
			}

			// Token: 0x04001ACC RID: 6860
			private string name;

			// Token: 0x04001ACD RID: 6861
			private TypeDesc td;
		}

		// Token: 0x020002F4 RID: 756
		private class Member
		{
			// Token: 0x06001E50 RID: 7760 RVA: 0x000B8994 File Offset: 0x000B6B94
			internal Member(XmlSerializationReaderCodeGen outerClass, string source, string arrayName, int i, MemberMapping mapping) : this(outerClass, source, null, arrayName, i, mapping, false, null)
			{
			}

			// Token: 0x06001E51 RID: 7761 RVA: 0x000B89B4 File Offset: 0x000B6BB4
			internal Member(XmlSerializationReaderCodeGen outerClass, string source, string arrayName, int i, MemberMapping mapping, string choiceSource) : this(outerClass, source, null, arrayName, i, mapping, false, choiceSource)
			{
			}

			// Token: 0x06001E52 RID: 7762 RVA: 0x000B89D4 File Offset: 0x000B6BD4
			internal Member(XmlSerializationReaderCodeGen outerClass, string source, string arraySource, string arrayName, int i, MemberMapping mapping) : this(outerClass, source, arraySource, arrayName, i, mapping, false, null)
			{
			}

			// Token: 0x06001E53 RID: 7763 RVA: 0x000B89F4 File Offset: 0x000B6BF4
			internal Member(XmlSerializationReaderCodeGen outerClass, string source, string arraySource, string arrayName, int i, MemberMapping mapping, string choiceSource) : this(outerClass, source, arraySource, arrayName, i, mapping, false, choiceSource)
			{
			}

			// Token: 0x06001E54 RID: 7764 RVA: 0x000B8A14 File Offset: 0x000B6C14
			internal Member(XmlSerializationReaderCodeGen outerClass, string source, string arrayName, int i, MemberMapping mapping, bool multiRef) : this(outerClass, source, null, arrayName, i, mapping, multiRef, null)
			{
			}

			// Token: 0x06001E55 RID: 7765 RVA: 0x000B8A34 File Offset: 0x000B6C34
			internal Member(XmlSerializationReaderCodeGen outerClass, string source, string arraySource, string arrayName, int i, MemberMapping mapping, bool multiRef, string choiceSource)
			{
				this.source = source;
				this.arrayName = arrayName + "_" + i.ToString(CultureInfo.InvariantCulture);
				this.choiceArrayName = "choice_" + this.arrayName;
				this.choiceSource = choiceSource;
				ElementAccessor[] elements = mapping.Elements;
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
						bool useReflection = mapping.ChoiceIdentifier.Mapping.TypeDesc.UseReflection;
						string csharpName = mapping.ChoiceIdentifier.Mapping.TypeDesc.CSharpName;
						string text3 = useReflection ? "" : ("(" + csharpName + "[])");
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
							outerClass.RaCodeGen.GetStringForTypeof(csharpName, useReflection),
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

			// Token: 0x170005EA RID: 1514
			// (get) Token: 0x06001E56 RID: 7766 RVA: 0x000B8C34 File Offset: 0x000B6E34
			internal MemberMapping Mapping
			{
				get
				{
					return this.mapping;
				}
			}

			// Token: 0x170005EB RID: 1515
			// (get) Token: 0x06001E57 RID: 7767 RVA: 0x000B8C3C File Offset: 0x000B6E3C
			internal string Source
			{
				get
				{
					return this.source;
				}
			}

			// Token: 0x170005EC RID: 1516
			// (get) Token: 0x06001E58 RID: 7768 RVA: 0x000B8C44 File Offset: 0x000B6E44
			internal string ArrayName
			{
				get
				{
					return this.arrayName;
				}
			}

			// Token: 0x170005ED RID: 1517
			// (get) Token: 0x06001E59 RID: 7769 RVA: 0x000B8C4C File Offset: 0x000B6E4C
			internal string ArraySource
			{
				get
				{
					return this.arraySource;
				}
			}

			// Token: 0x170005EE RID: 1518
			// (get) Token: 0x06001E5A RID: 7770 RVA: 0x000B8C54 File Offset: 0x000B6E54
			internal bool IsList
			{
				get
				{
					return this.isList;
				}
			}

			// Token: 0x170005EF RID: 1519
			// (get) Token: 0x06001E5B RID: 7771 RVA: 0x000B8C5C File Offset: 0x000B6E5C
			internal bool IsArrayLike
			{
				get
				{
					return this.isArray || this.isList;
				}
			}

			// Token: 0x170005F0 RID: 1520
			// (get) Token: 0x06001E5C RID: 7772 RVA: 0x000B8C6E File Offset: 0x000B6E6E
			// (set) Token: 0x06001E5D RID: 7773 RVA: 0x000B8C76 File Offset: 0x000B6E76
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

			// Token: 0x170005F1 RID: 1521
			// (get) Token: 0x06001E5E RID: 7774 RVA: 0x000B8C7F File Offset: 0x000B6E7F
			// (set) Token: 0x06001E5F RID: 7775 RVA: 0x000B8C87 File Offset: 0x000B6E87
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

			// Token: 0x170005F2 RID: 1522
			// (get) Token: 0x06001E60 RID: 7776 RVA: 0x000B8C90 File Offset: 0x000B6E90
			// (set) Token: 0x06001E61 RID: 7777 RVA: 0x000B8C98 File Offset: 0x000B6E98
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

			// Token: 0x170005F3 RID: 1523
			// (get) Token: 0x06001E62 RID: 7778 RVA: 0x000B8CA1 File Offset: 0x000B6EA1
			// (set) Token: 0x06001E63 RID: 7779 RVA: 0x000B8CA9 File Offset: 0x000B6EA9
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

			// Token: 0x170005F4 RID: 1524
			// (get) Token: 0x06001E64 RID: 7780 RVA: 0x000B8CB2 File Offset: 0x000B6EB2
			// (set) Token: 0x06001E65 RID: 7781 RVA: 0x000B8CBA File Offset: 0x000B6EBA
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

			// Token: 0x170005F5 RID: 1525
			// (get) Token: 0x06001E66 RID: 7782 RVA: 0x000B8CC3 File Offset: 0x000B6EC3
			internal string ChoiceSource
			{
				get
				{
					return this.choiceSource;
				}
			}

			// Token: 0x170005F6 RID: 1526
			// (get) Token: 0x06001E67 RID: 7783 RVA: 0x000B8CCB File Offset: 0x000B6ECB
			internal string ChoiceArrayName
			{
				get
				{
					return this.choiceArrayName;
				}
			}

			// Token: 0x170005F7 RID: 1527
			// (get) Token: 0x06001E68 RID: 7784 RVA: 0x000B8CD3 File Offset: 0x000B6ED3
			internal string ChoiceArraySource
			{
				get
				{
					return this.choiceArraySource;
				}
			}

			// Token: 0x04001ACE RID: 6862
			private string source;

			// Token: 0x04001ACF RID: 6863
			private string arrayName;

			// Token: 0x04001AD0 RID: 6864
			private string arraySource;

			// Token: 0x04001AD1 RID: 6865
			private string choiceArrayName;

			// Token: 0x04001AD2 RID: 6866
			private string choiceSource;

			// Token: 0x04001AD3 RID: 6867
			private string choiceArraySource;

			// Token: 0x04001AD4 RID: 6868
			private MemberMapping mapping;

			// Token: 0x04001AD5 RID: 6869
			private bool isArray;

			// Token: 0x04001AD6 RID: 6870
			private bool isList;

			// Token: 0x04001AD7 RID: 6871
			private bool isNullable;

			// Token: 0x04001AD8 RID: 6872
			private bool multiRef;

			// Token: 0x04001AD9 RID: 6873
			private int fixupIndex = -1;

			// Token: 0x04001ADA RID: 6874
			private string paramsReadSource;

			// Token: 0x04001ADB RID: 6875
			private string checkSpecifiedSource;
		}
	}
}
