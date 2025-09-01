using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x020002E9 RID: 745
	internal class XmlSerializationCodeGen
	{
		// Token: 0x06001D4F RID: 7503 RVA: 0x000AB358 File Offset: 0x000A9558
		internal XmlSerializationCodeGen(IndentedWriter writer, TypeScope[] scopes, string access, string className)
		{
			this.writer = writer;
			this.scopes = scopes;
			if (scopes.Length != 0)
			{
				this.stringTypeDesc = scopes[0].GetTypeDesc(typeof(string));
				this.qnameTypeDesc = scopes[0].GetTypeDesc(typeof(XmlQualifiedName));
			}
			this.raCodeGen = new ReflectionAwareCodeGen(writer);
			this.className = className;
			this.access = access;
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001D50 RID: 7504 RVA: 0x000AB3DE File Offset: 0x000A95DE
		internal IndentedWriter Writer
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001D51 RID: 7505 RVA: 0x000AB3E6 File Offset: 0x000A95E6
		// (set) Token: 0x06001D52 RID: 7506 RVA: 0x000AB3EE File Offset: 0x000A95EE
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

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001D53 RID: 7507 RVA: 0x000AB3F7 File Offset: 0x000A95F7
		internal ReflectionAwareCodeGen RaCodeGen
		{
			get
			{
				return this.raCodeGen;
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001D54 RID: 7508 RVA: 0x000AB3FF File Offset: 0x000A95FF
		internal TypeDesc StringTypeDesc
		{
			get
			{
				return this.stringTypeDesc;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001D55 RID: 7509 RVA: 0x000AB407 File Offset: 0x000A9607
		internal TypeDesc QnameTypeDesc
		{
			get
			{
				return this.qnameTypeDesc;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001D56 RID: 7510 RVA: 0x000AB40F File Offset: 0x000A960F
		internal string ClassName
		{
			get
			{
				return this.className;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001D57 RID: 7511 RVA: 0x000AB417 File Offset: 0x000A9617
		internal string Access
		{
			get
			{
				return this.access;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001D58 RID: 7512 RVA: 0x000AB41F File Offset: 0x000A961F
		internal TypeScope[] Scopes
		{
			get
			{
				return this.scopes;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001D59 RID: 7513 RVA: 0x000AB427 File Offset: 0x000A9627
		internal Hashtable MethodNames
		{
			get
			{
				return this.methodNames;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001D5A RID: 7514 RVA: 0x000AB42F File Offset: 0x000A962F
		internal Hashtable GeneratedMethods
		{
			get
			{
				return this.generatedMethods;
			}
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x0000B528 File Offset: 0x00009728
		internal virtual void GenerateMethod(TypeMapping mapping)
		{
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x000AB438 File Offset: 0x000A9638
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

		// Token: 0x06001D5D RID: 7517 RVA: 0x000AB470 File Offset: 0x000A9670
		internal string ReferenceMapping(TypeMapping mapping)
		{
			if (!mapping.IsSoap && this.generatedMethods[mapping] == null)
			{
				this.referencedMethods = this.EnsureArrayIndex(this.referencedMethods, this.references);
				TypeMapping[] array = this.referencedMethods;
				int num = this.references;
				this.references = num + 1;
				array[num] = mapping;
			}
			return (string)this.methodNames[mapping];
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x000AB4D8 File Offset: 0x000A96D8
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

		// Token: 0x06001D5F RID: 7519 RVA: 0x000AB50D File Offset: 0x000A970D
		internal void WriteQuotedCSharpString(string value)
		{
			this.raCodeGen.WriteQuotedCSharpString(value);
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x000AB51C File Offset: 0x000A971C
		internal void GenerateHashtableGetBegin(string privateName, string publicName)
		{
			this.writer.Write(typeof(Hashtable).FullName);
			this.writer.Write(" ");
			this.writer.Write(privateName);
			this.writer.WriteLine(" = null;");
			this.writer.Write("public override ");
			this.writer.Write(typeof(Hashtable).FullName);
			this.writer.Write(" ");
			this.writer.Write(publicName);
			this.writer.WriteLine(" {");
			IndentedWriter indentedWriter = this.writer;
			int indent = indentedWriter.Indent;
			indentedWriter.Indent = indent + 1;
			this.writer.WriteLine("get {");
			IndentedWriter indentedWriter2 = this.writer;
			indent = indentedWriter2.Indent;
			indentedWriter2.Indent = indent + 1;
			this.writer.Write("if (");
			this.writer.Write(privateName);
			this.writer.WriteLine(" == null) {");
			IndentedWriter indentedWriter3 = this.writer;
			indent = indentedWriter3.Indent;
			indentedWriter3.Indent = indent + 1;
			this.writer.Write(typeof(Hashtable).FullName);
			this.writer.Write(" _tmp = new ");
			this.writer.Write(typeof(Hashtable).FullName);
			this.writer.WriteLine("();");
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x000AB694 File Offset: 0x000A9894
		internal void GenerateHashtableGetEnd(string privateName)
		{
			this.writer.Write("if (");
			this.writer.Write(privateName);
			this.writer.Write(" == null) ");
			this.writer.Write(privateName);
			this.writer.WriteLine(" = _tmp;");
			IndentedWriter indentedWriter = this.writer;
			int indent = indentedWriter.Indent;
			indentedWriter.Indent = indent - 1;
			this.writer.WriteLine("}");
			this.writer.Write("return ");
			this.writer.Write(privateName);
			this.writer.WriteLine(";");
			IndentedWriter indentedWriter2 = this.writer;
			indent = indentedWriter2.Indent;
			indentedWriter2.Indent = indent - 1;
			this.writer.WriteLine("}");
			IndentedWriter indentedWriter3 = this.writer;
			indent = indentedWriter3.Indent;
			indentedWriter3.Indent = indent - 1;
			this.writer.WriteLine("}");
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x000AB784 File Offset: 0x000A9984
		internal void GeneratePublicMethods(string privateName, string publicName, string[] methods, XmlMapping[] xmlMappings)
		{
			this.GenerateHashtableGetBegin(privateName, publicName);
			if (methods != null && methods.Length != 0 && xmlMappings != null && xmlMappings.Length == methods.Length)
			{
				for (int i = 0; i < methods.Length; i++)
				{
					if (methods[i] != null)
					{
						this.writer.Write("_tmp[");
						this.WriteQuotedCSharpString(xmlMappings[i].Key);
						this.writer.Write("] = ");
						this.WriteQuotedCSharpString(methods[i]);
						this.writer.WriteLine(";");
					}
				}
			}
			this.GenerateHashtableGetEnd(privateName);
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x000AB810 File Offset: 0x000A9A10
		internal void GenerateSupportedTypes(Type[] types)
		{
			this.writer.Write("public override ");
			this.writer.Write(typeof(bool).FullName);
			this.writer.Write(" CanSerialize(");
			this.writer.Write(typeof(Type).FullName);
			this.writer.WriteLine(" type) {");
			IndentedWriter indentedWriter = this.writer;
			int indent = indentedWriter.Indent;
			indentedWriter.Indent = indent + 1;
			Hashtable hashtable = new Hashtable();
			foreach (Type type in types)
			{
				if (!(type == null) && (type.IsPublic || type.IsNestedPublic) && hashtable[type] == null && !DynamicAssemblies.IsTypeDynamic(type) && !type.IsGenericType && (!type.ContainsGenericParameters || !DynamicAssemblies.IsTypeDynamic(type.GetGenericArguments())))
				{
					hashtable[type] = type;
					this.writer.Write("if (type == typeof(");
					this.writer.Write(CodeIdentifier.GetCSharpName(type));
					this.writer.WriteLine(")) return true;");
				}
			}
			this.writer.WriteLine("return false;");
			IndentedWriter indentedWriter2 = this.writer;
			indent = indentedWriter2.Indent;
			indentedWriter2.Indent = indent - 1;
			this.writer.WriteLine("}");
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x000AB96C File Offset: 0x000A9B6C
		internal string GenerateBaseSerializer(string baseSerializer, string readerClass, string writerClass, CodeIdentifiers classes)
		{
			baseSerializer = CodeIdentifier.MakeValid(baseSerializer);
			baseSerializer = classes.AddUnique(baseSerializer, baseSerializer);
			this.writer.WriteLine();
			this.writer.Write("public abstract class ");
			this.writer.Write(CodeIdentifier.GetCSharpName(baseSerializer));
			this.writer.Write(" : ");
			this.writer.Write(typeof(XmlSerializer).FullName);
			this.writer.WriteLine(" {");
			IndentedWriter indentedWriter = this.writer;
			int indent = indentedWriter.Indent;
			indentedWriter.Indent = indent + 1;
			this.writer.Write("protected override ");
			this.writer.Write(typeof(XmlSerializationReader).FullName);
			this.writer.WriteLine(" CreateReader() {");
			IndentedWriter indentedWriter2 = this.writer;
			indent = indentedWriter2.Indent;
			indentedWriter2.Indent = indent + 1;
			this.writer.Write("return new ");
			this.writer.Write(readerClass);
			this.writer.WriteLine("();");
			IndentedWriter indentedWriter3 = this.writer;
			indent = indentedWriter3.Indent;
			indentedWriter3.Indent = indent - 1;
			this.writer.WriteLine("}");
			this.writer.Write("protected override ");
			this.writer.Write(typeof(XmlSerializationWriter).FullName);
			this.writer.WriteLine(" CreateWriter() {");
			IndentedWriter indentedWriter4 = this.writer;
			indent = indentedWriter4.Indent;
			indentedWriter4.Indent = indent + 1;
			this.writer.Write("return new ");
			this.writer.Write(writerClass);
			this.writer.WriteLine("();");
			IndentedWriter indentedWriter5 = this.writer;
			indent = indentedWriter5.Indent;
			indentedWriter5.Indent = indent - 1;
			this.writer.WriteLine("}");
			IndentedWriter indentedWriter6 = this.writer;
			indent = indentedWriter6.Indent;
			indentedWriter6.Indent = indent - 1;
			this.writer.WriteLine("}");
			return baseSerializer;
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x000ABB70 File Offset: 0x000A9D70
		internal string GenerateTypedSerializer(string readMethod, string writeMethod, XmlMapping mapping, CodeIdentifiers classes, string baseSerializer, string readerClass, string writerClass)
		{
			string text = CodeIdentifier.MakeValid(Accessor.UnescapeName(mapping.Accessor.Mapping.TypeDesc.Name));
			text = classes.AddUnique(text + "Serializer", mapping);
			this.writer.WriteLine();
			this.writer.Write("public sealed class ");
			this.writer.Write(CodeIdentifier.GetCSharpName(text));
			this.writer.Write(" : ");
			this.writer.Write(baseSerializer);
			this.writer.WriteLine(" {");
			IndentedWriter indentedWriter = this.writer;
			int indent = indentedWriter.Indent;
			indentedWriter.Indent = indent + 1;
			this.writer.WriteLine();
			this.writer.Write("public override ");
			this.writer.Write(typeof(bool).FullName);
			this.writer.Write(" CanDeserialize(");
			this.writer.Write(typeof(XmlReader).FullName);
			this.writer.WriteLine(" xmlReader) {");
			IndentedWriter indentedWriter2 = this.writer;
			indent = indentedWriter2.Indent;
			indentedWriter2.Indent = indent + 1;
			if (mapping.Accessor.Any)
			{
				this.writer.WriteLine("return true;");
			}
			else
			{
				this.writer.Write("return xmlReader.IsStartElement(");
				this.WriteQuotedCSharpString(mapping.Accessor.Name);
				this.writer.Write(", ");
				this.WriteQuotedCSharpString(mapping.Accessor.Namespace);
				this.writer.WriteLine(");");
			}
			IndentedWriter indentedWriter3 = this.writer;
			indent = indentedWriter3.Indent;
			indentedWriter3.Indent = indent - 1;
			this.writer.WriteLine("}");
			if (writeMethod != null)
			{
				this.writer.WriteLine();
				this.writer.Write("protected override void Serialize(object objectToSerialize, ");
				this.writer.Write(typeof(XmlSerializationWriter).FullName);
				this.writer.WriteLine(" writer) {");
				IndentedWriter indentedWriter4 = this.writer;
				indent = indentedWriter4.Indent;
				indentedWriter4.Indent = indent + 1;
				this.writer.Write("((");
				this.writer.Write(writerClass);
				this.writer.Write(")writer).");
				this.writer.Write(writeMethod);
				this.writer.Write("(");
				if (mapping is XmlMembersMapping)
				{
					this.writer.Write("(object[])");
				}
				this.writer.WriteLine("objectToSerialize);");
				IndentedWriter indentedWriter5 = this.writer;
				indent = indentedWriter5.Indent;
				indentedWriter5.Indent = indent - 1;
				this.writer.WriteLine("}");
			}
			if (readMethod != null)
			{
				this.writer.WriteLine();
				this.writer.Write("protected override object Deserialize(");
				this.writer.Write(typeof(XmlSerializationReader).FullName);
				this.writer.WriteLine(" reader) {");
				IndentedWriter indentedWriter6 = this.writer;
				indent = indentedWriter6.Indent;
				indentedWriter6.Indent = indent + 1;
				this.writer.Write("return ((");
				this.writer.Write(readerClass);
				this.writer.Write(")reader).");
				this.writer.Write(readMethod);
				this.writer.WriteLine("();");
				IndentedWriter indentedWriter7 = this.writer;
				indent = indentedWriter7.Indent;
				indentedWriter7.Indent = indent - 1;
				this.writer.WriteLine("}");
			}
			IndentedWriter indentedWriter8 = this.writer;
			indent = indentedWriter8.Indent;
			indentedWriter8.Indent = indent - 1;
			this.writer.WriteLine("}");
			return text;
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x000ABF24 File Offset: 0x000AA124
		private void GenerateTypedSerializers(Hashtable serializers)
		{
			string privateName = "typedSerializers";
			this.GenerateHashtableGetBegin(privateName, "TypedSerializers");
			foreach (object obj in serializers.Keys)
			{
				string text = (string)obj;
				this.writer.Write("_tmp.Add(");
				this.WriteQuotedCSharpString(text);
				this.writer.Write(", new ");
				this.writer.Write((string)serializers[text]);
				this.writer.WriteLine("());");
			}
			this.GenerateHashtableGetEnd("typedSerializers");
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x000ABFE4 File Offset: 0x000AA1E4
		private void GenerateGetSerializer(Hashtable serializers, XmlMapping[] xmlMappings)
		{
			this.writer.Write("public override ");
			this.writer.Write(typeof(XmlSerializer).FullName);
			this.writer.Write(" GetSerializer(");
			this.writer.Write(typeof(Type).FullName);
			this.writer.WriteLine(" type) {");
			IndentedWriter indentedWriter = this.writer;
			int indent = indentedWriter.Indent;
			indentedWriter.Indent = indent + 1;
			for (int i = 0; i < xmlMappings.Length; i++)
			{
				if (xmlMappings[i] is XmlTypeMapping)
				{
					Type type = xmlMappings[i].Accessor.Mapping.TypeDesc.Type;
					if (!(type == null) && (type.IsPublic || type.IsNestedPublic) && !DynamicAssemblies.IsTypeDynamic(type) && !type.IsGenericType && (!type.ContainsGenericParameters || !DynamicAssemblies.IsTypeDynamic(type.GetGenericArguments())))
					{
						this.writer.Write("if (type == typeof(");
						this.writer.Write(CodeIdentifier.GetCSharpName(type));
						this.writer.Write(")) return new ");
						this.writer.Write((string)serializers[xmlMappings[i].Key]);
						this.writer.WriteLine("();");
					}
				}
			}
			this.writer.WriteLine("return null;");
			IndentedWriter indentedWriter2 = this.writer;
			indent = indentedWriter2.Indent;
			indentedWriter2.Indent = indent - 1;
			this.writer.WriteLine("}");
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x000AC17C File Offset: 0x000AA37C
		internal void GenerateSerializerContract(string className, XmlMapping[] xmlMappings, Type[] types, string readerType, string[] readMethods, string writerType, string[] writerMethods, Hashtable serializers)
		{
			this.writer.WriteLine();
			this.writer.Write("public class XmlSerializerContract : global::");
			this.writer.Write(typeof(XmlSerializerImplementation).FullName);
			this.writer.WriteLine(" {");
			IndentedWriter indentedWriter = this.writer;
			int indent = indentedWriter.Indent;
			indentedWriter.Indent = indent + 1;
			this.writer.Write("public override global::");
			this.writer.Write(typeof(XmlSerializationReader).FullName);
			this.writer.Write(" Reader { get { return new ");
			this.writer.Write(readerType);
			this.writer.WriteLine("(); } }");
			this.writer.Write("public override global::");
			this.writer.Write(typeof(XmlSerializationWriter).FullName);
			this.writer.Write(" Writer { get { return new ");
			this.writer.Write(writerType);
			this.writer.WriteLine("(); } }");
			this.GeneratePublicMethods("readMethods", "ReadMethods", readMethods, xmlMappings);
			this.GeneratePublicMethods("writeMethods", "WriteMethods", writerMethods, xmlMappings);
			this.GenerateTypedSerializers(serializers);
			this.GenerateSupportedTypes(types);
			this.GenerateGetSerializer(serializers, xmlMappings);
			IndentedWriter indentedWriter2 = this.writer;
			indent = indentedWriter2.Indent;
			indentedWriter2.Indent = indent - 1;
			this.writer.WriteLine("}");
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x000AC2F4 File Offset: 0x000AA4F4
		internal static bool IsWildcard(SpecialMapping mapping)
		{
			if (mapping is SerializableMapping)
			{
				return ((SerializableMapping)mapping).IsAny;
			}
			return mapping.TypeDesc.CanBeElementValue;
		}

		// Token: 0x04001A48 RID: 6728
		private IndentedWriter writer;

		// Token: 0x04001A49 RID: 6729
		private int nextMethodNumber;

		// Token: 0x04001A4A RID: 6730
		private Hashtable methodNames = new Hashtable();

		// Token: 0x04001A4B RID: 6731
		private ReflectionAwareCodeGen raCodeGen;

		// Token: 0x04001A4C RID: 6732
		private TypeScope[] scopes;

		// Token: 0x04001A4D RID: 6733
		private TypeDesc stringTypeDesc;

		// Token: 0x04001A4E RID: 6734
		private TypeDesc qnameTypeDesc;

		// Token: 0x04001A4F RID: 6735
		private string access;

		// Token: 0x04001A50 RID: 6736
		private string className;

		// Token: 0x04001A51 RID: 6737
		private TypeMapping[] referencedMethods;

		// Token: 0x04001A52 RID: 6738
		private int references;

		// Token: 0x04001A53 RID: 6739
		private Hashtable generatedMethods = new Hashtable();
	}
}
