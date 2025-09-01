using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Diagnostics;
using System.Runtime.Serialization.Diagnostics;
using System.Security;
using System.Text;
using System.Xml;

namespace System.Runtime.Serialization
{
	/// <summary>Provides the base class used to serialize objects as XML streams or documents. This class is abstract.</summary>
	// Token: 0x02000145 RID: 325
	public abstract class XmlObjectSerializer
	{
		/// <summary>Writes the start of the object's data as an opening XML element using the specified <see cref="T:System.Xml.XmlDictionaryWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlDictionaryWriter" /> used to write the XML document.</param>
		/// <param name="graph">The object to serialize.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">the type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">there is a problem with the instance being serialized.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">the maximum number of objects to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		// Token: 0x06000FC7 RID: 4039
		public abstract void WriteStartObject(XmlDictionaryWriter writer, object graph);

		/// <summary>Writes only the content of the object to the XML document or stream using the specified <see cref="T:System.Xml.XmlDictionaryWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlDictionaryWriter" /> used to write the XML document or stream.</param>
		/// <param name="graph">The object that contains the content to write.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">the type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">there is a problem with the instance being serialized.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">the maximum number of objects to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		// Token: 0x06000FC8 RID: 4040
		public abstract void WriteObjectContent(XmlDictionaryWriter writer, object graph);

		/// <summary>Writes the end of the object data as a closing XML element to the XML document or stream with an <see cref="T:System.Xml.XmlDictionaryWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlDictionaryWriter" /> used to write the XML document or stream.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">the type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">there is a problem with the instance being serialized.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">the maximum number of objects to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		// Token: 0x06000FC9 RID: 4041
		public abstract void WriteEndObject(XmlDictionaryWriter writer);

		/// <summary>Writes the complete content (start, content, and end) of the object to the XML document or stream with the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> used to write the XML document or stream.</param>
		/// <param name="graph">The object that contains the data to write to the stream.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">the type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">there is a problem with the instance being serialized.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">the maximum number of objects to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		// Token: 0x06000FCA RID: 4042 RVA: 0x0003EC48 File Offset: 0x0003CE48
		public virtual void WriteObject(Stream stream, object graph)
		{
			XmlObjectSerializer.CheckNull(stream, "stream");
			XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateTextWriter(stream, Encoding.UTF8, false);
			this.WriteObject(xmlDictionaryWriter, graph);
			xmlDictionaryWriter.Flush();
		}

		/// <summary>Writes the complete content (start, content, and end) of the object to the XML document or stream with the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> used to write the XML document or stream.</param>
		/// <param name="graph">The object that contains the content to write.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">the type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">there is a problem with the instance being serialized.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">the maximum number of objects to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		// Token: 0x06000FCB RID: 4043 RVA: 0x0003EC7B File Offset: 0x0003CE7B
		public virtual void WriteObject(XmlWriter writer, object graph)
		{
			XmlObjectSerializer.CheckNull(writer, "writer");
			this.WriteObject(XmlDictionaryWriter.CreateDictionaryWriter(writer), graph);
		}

		/// <summary>Writes the start of the object's data as an opening XML element using the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> used to write the XML document.</param>
		/// <param name="graph">The object to serialize.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">the type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">there is a problem with the instance being serialized.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">the maximum number of objects to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		// Token: 0x06000FCC RID: 4044 RVA: 0x0003EC95 File Offset: 0x0003CE95
		public virtual void WriteStartObject(XmlWriter writer, object graph)
		{
			XmlObjectSerializer.CheckNull(writer, "writer");
			this.WriteStartObject(XmlDictionaryWriter.CreateDictionaryWriter(writer), graph);
		}

		/// <summary>Writes only the content of the object to the XML document or stream with the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> used to write the XML document or stream.</param>
		/// <param name="graph">The object that contains the content to write.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">the type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">there is a problem with the instance being serialized.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">the maximum number of objects to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		// Token: 0x06000FCD RID: 4045 RVA: 0x0003ECAF File Offset: 0x0003CEAF
		public virtual void WriteObjectContent(XmlWriter writer, object graph)
		{
			XmlObjectSerializer.CheckNull(writer, "writer");
			this.WriteObjectContent(XmlDictionaryWriter.CreateDictionaryWriter(writer), graph);
		}

		/// <summary>Writes the end of the object data as a closing XML element to the XML document or stream with an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> used to write the XML document or stream.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">the type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">there is a problem with the instance being serialized.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">the maximum number of objects to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		// Token: 0x06000FCE RID: 4046 RVA: 0x0003ECC9 File Offset: 0x0003CEC9
		public virtual void WriteEndObject(XmlWriter writer)
		{
			XmlObjectSerializer.CheckNull(writer, "writer");
			this.WriteEndObject(XmlDictionaryWriter.CreateDictionaryWriter(writer));
		}

		/// <summary>Writes the complete content (start, content, and end) of the object to the XML document or stream with the specified <see cref="T:System.Xml.XmlDictionaryWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlDictionaryWriter" /> used to write the content to the XML document or stream.</param>
		/// <param name="graph">The object that contains the content to write.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">the type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">there is a problem with the instance being serialized.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">the maximum number of objects to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		// Token: 0x06000FCF RID: 4047 RVA: 0x000306EE File Offset: 0x0002E8EE
		public virtual void WriteObject(XmlDictionaryWriter writer, object graph)
		{
			this.WriteObjectHandleExceptions(new XmlWriterDelegator(writer), graph);
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0003ECE2 File Offset: 0x0003CEE2
		internal void WriteObjectHandleExceptions(XmlWriterDelegator writer, object graph)
		{
			this.WriteObjectHandleExceptions(writer, graph, null);
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0003ECF0 File Offset: 0x0003CEF0
		internal void WriteObjectHandleExceptions(XmlWriterDelegator writer, object graph, DataContractResolver dataContractResolver)
		{
			try
			{
				XmlObjectSerializer.CheckNull(writer, "writer");
				if (DiagnosticUtility.ShouldTraceInformation)
				{
					TraceUtility.Trace(TraceEventType.Information, 196609, SR.GetString("WriteObject begins"), new StringTraceRecord("Type", XmlObjectSerializer.GetTypeInfo(this.GetSerializeType(graph))));
					this.InternalWriteObject(writer, graph, dataContractResolver);
					TraceUtility.Trace(TraceEventType.Information, 196610, SR.GetString("WriteObject ends"), new StringTraceRecord("Type", XmlObjectSerializer.GetTypeInfo(this.GetSerializeType(graph))));
				}
				else
				{
					this.InternalWriteObject(writer, graph, dataContractResolver);
				}
			}
			catch (XmlException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(XmlObjectSerializer.GetTypeInfoError("There was an error serializing the object {0}. {1}", this.GetSerializeType(graph), innerException), innerException));
			}
			catch (FormatException innerException2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(XmlObjectSerializer.GetTypeInfoError("There was an error serializing the object {0}. {1}", this.GetSerializeType(graph), innerException2), innerException2));
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x0001BF43 File Offset: 0x0001A143
		internal virtual Dictionary<XmlQualifiedName, DataContract> KnownDataContracts
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0003EDD8 File Offset: 0x0003CFD8
		internal virtual void InternalWriteObject(XmlWriterDelegator writer, object graph)
		{
			this.WriteStartObject(writer.Writer, graph);
			this.WriteObjectContent(writer.Writer, graph);
			this.WriteEndObject(writer.Writer);
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x0003EE00 File Offset: 0x0003D000
		internal virtual void InternalWriteObject(XmlWriterDelegator writer, object graph, DataContractResolver dataContractResolver)
		{
			this.InternalWriteObject(writer, graph);
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x00003141 File Offset: 0x00001341
		internal virtual void InternalWriteStartObject(XmlWriterDelegator writer, object graph)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x00003141 File Offset: 0x00001341
		internal virtual void InternalWriteObjectContent(XmlWriterDelegator writer, object graph)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x00003141 File Offset: 0x00001341
		internal virtual void InternalWriteEndObject(XmlWriterDelegator writer)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0003EE0C File Offset: 0x0003D00C
		internal void WriteStartObjectHandleExceptions(XmlWriterDelegator writer, object graph)
		{
			try
			{
				XmlObjectSerializer.CheckNull(writer, "writer");
				this.InternalWriteStartObject(writer, graph);
			}
			catch (XmlException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(XmlObjectSerializer.GetTypeInfoError("There was an error writing start element of object {0}. {1}", this.GetSerializeType(graph), innerException), innerException));
			}
			catch (FormatException innerException2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(XmlObjectSerializer.GetTypeInfoError("There was an error writing start element of object {0}. {1}", this.GetSerializeType(graph), innerException2), innerException2));
			}
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0003EE88 File Offset: 0x0003D088
		internal void WriteObjectContentHandleExceptions(XmlWriterDelegator writer, object graph)
		{
			try
			{
				XmlObjectSerializer.CheckNull(writer, "writer");
				if (DiagnosticUtility.ShouldTraceInformation)
				{
					TraceUtility.Trace(TraceEventType.Information, 196611, SR.GetString("WriteObjectContent begins"), new StringTraceRecord("Type", XmlObjectSerializer.GetTypeInfo(this.GetSerializeType(graph))));
					if (writer.WriteState != WriteState.Element)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("WriteState '{0}' not valid. Caller must write start element before serializing in contentOnly mode.", new object[]
						{
							writer.WriteState
						})));
					}
					this.InternalWriteObjectContent(writer, graph);
					TraceUtility.Trace(TraceEventType.Information, 196612, SR.GetString("WriteObjectContent ends"), new StringTraceRecord("Type", XmlObjectSerializer.GetTypeInfo(this.GetSerializeType(graph))));
				}
				else
				{
					if (writer.WriteState != WriteState.Element)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("WriteState '{0}' not valid. Caller must write start element before serializing in contentOnly mode.", new object[]
						{
							writer.WriteState
						})));
					}
					this.InternalWriteObjectContent(writer, graph);
				}
			}
			catch (XmlException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(XmlObjectSerializer.GetTypeInfoError("There was an error serializing the object {0}. {1}", this.GetSerializeType(graph), innerException), innerException));
			}
			catch (FormatException innerException2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(XmlObjectSerializer.GetTypeInfoError("There was an error serializing the object {0}. {1}", this.GetSerializeType(graph), innerException2), innerException2));
			}
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0003EFD4 File Offset: 0x0003D1D4
		internal void WriteEndObjectHandleExceptions(XmlWriterDelegator writer)
		{
			try
			{
				XmlObjectSerializer.CheckNull(writer, "writer");
				this.InternalWriteEndObject(writer);
			}
			catch (XmlException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(XmlObjectSerializer.GetTypeInfoError("There was an error writing end element of object {0}. {1}", null, innerException), innerException));
			}
			catch (FormatException innerException2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(XmlObjectSerializer.GetTypeInfoError("There was an error writing end element of object {0}. {1}", null, innerException2), innerException2));
			}
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0003F044 File Offset: 0x0003D244
		internal void WriteRootElement(XmlWriterDelegator writer, DataContract contract, XmlDictionaryString name, XmlDictionaryString ns, bool needsContractNsAtRoot)
		{
			if (name != null)
			{
				contract.WriteRootElement(writer, name, ns);
				if (needsContractNsAtRoot)
				{
					writer.WriteNamespaceDecl(contract.Namespace);
				}
				return;
			}
			if (!contract.HasRoot)
			{
				return;
			}
			contract.WriteRootElement(writer, contract.TopLevelElementName, contract.TopLevelElementNamespace);
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0003F080 File Offset: 0x0003D280
		internal bool CheckIfNeedsContractNsAtRoot(XmlDictionaryString name, XmlDictionaryString ns, DataContract contract)
		{
			if (name == null)
			{
				return false;
			}
			if (contract.IsBuiltInDataContract || !contract.CanContainReferences || contract.IsISerializable)
			{
				return false;
			}
			string @string = XmlDictionaryString.GetString(contract.Namespace);
			return !string.IsNullOrEmpty(@string) && !(@string == XmlDictionaryString.GetString(ns));
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x0003F0D1 File Offset: 0x0003D2D1
		internal static void WriteNull(XmlWriterDelegator writer)
		{
			writer.WriteAttributeBool("i", DictionaryGlobals.XsiNilLocalName, DictionaryGlobals.SchemaInstanceNamespace, true);
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x0003F0EC File Offset: 0x0003D2EC
		internal static bool IsContractDeclared(DataContract contract, DataContract declaredContract)
		{
			return (contract.Name == declaredContract.Name && contract.Namespace == declaredContract.Namespace) || (contract.Name.Value == declaredContract.Name.Value && contract.Namespace.Value == declaredContract.Namespace.Value);
		}

		/// <summary>Reads the XML stream or document with a <see cref="T:System.IO.Stream" /> and returns the deserialized object.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> used to read the XML stream or document.</param>
		/// <returns>The deserialized object.</returns>
		// Token: 0x06000FDF RID: 4063 RVA: 0x0003F151 File Offset: 0x0003D351
		public virtual object ReadObject(Stream stream)
		{
			XmlObjectSerializer.CheckNull(stream, "stream");
			return this.ReadObject(XmlDictionaryReader.CreateTextReader(stream, XmlDictionaryReaderQuotas.Max));
		}

		/// <summary>Reads the XML document or stream with an <see cref="T:System.Xml.XmlReader" /> and returns the deserialized object.</summary>
		/// <param name="reader">An <see cref="T:System.Xml.XmlReader" /> used to read the XML stream or document.</param>
		/// <returns>The deserialized object.</returns>
		// Token: 0x06000FE0 RID: 4064 RVA: 0x0003F16F File Offset: 0x0003D36F
		public virtual object ReadObject(XmlReader reader)
		{
			XmlObjectSerializer.CheckNull(reader, "reader");
			return this.ReadObject(XmlDictionaryReader.CreateDictionaryReader(reader));
		}

		/// <summary>Reads the XML document or stream with an <see cref="T:System.Xml.XmlDictionaryReader" /> and returns the deserialized object.</summary>
		/// <param name="reader">An <see cref="T:System.Xml.XmlDictionaryReader" /> used to read the XML document.</param>
		/// <returns>The deserialized object.</returns>
		// Token: 0x06000FE1 RID: 4065 RVA: 0x00030739 File Offset: 0x0002E939
		public virtual object ReadObject(XmlDictionaryReader reader)
		{
			return this.ReadObjectHandleExceptions(new XmlReaderDelegator(reader), true);
		}

		/// <summary>Reads the XML document or stream with an <see cref="T:System.Xml.XmlReader" /> and returns the deserialized object; it also enables you to specify whether the serializer can read the data before attempting to read it.</summary>
		/// <param name="reader">An <see cref="T:System.Xml.XmlReader" /> used to read the XML document or stream.</param>
		/// <param name="verifyObjectName">
		///   <see langword="true" /> to check whether the enclosing XML element name and namespace correspond to the root name and root namespace; <see langword="false" /> to skip the verification.</param>
		/// <returns>The deserialized object.</returns>
		// Token: 0x06000FE2 RID: 4066 RVA: 0x0003F188 File Offset: 0x0003D388
		public virtual object ReadObject(XmlReader reader, bool verifyObjectName)
		{
			XmlObjectSerializer.CheckNull(reader, "reader");
			return this.ReadObject(XmlDictionaryReader.CreateDictionaryReader(reader), verifyObjectName);
		}

		/// <summary>Reads the XML stream or document with an <see cref="T:System.Xml.XmlDictionaryReader" /> and returns the deserialized object; it also enables you to specify whether the serializer can read the data before attempting to read it.</summary>
		/// <param name="reader">An <see cref="T:System.Xml.XmlDictionaryReader" /> used to read the XML document.</param>
		/// <param name="verifyObjectName">
		///   <see langword="true" /> to check whether the enclosing XML element name and namespace correspond to the root name and root namespace; otherwise, <see langword="false" /> to skip the verification.</param>
		/// <returns>The deserialized object.</returns>
		// Token: 0x06000FE3 RID: 4067
		public abstract object ReadObject(XmlDictionaryReader reader, bool verifyObjectName);

		/// <summary>Gets a value that specifies whether the <see cref="T:System.Xml.XmlReader" /> is positioned over an XML element that can be read.</summary>
		/// <param name="reader">An <see cref="T:System.Xml.XmlReader" /> used to read the XML stream or document.</param>
		/// <returns>
		///   <see langword="true" /> if the reader is positioned over the starting element; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000FE4 RID: 4068 RVA: 0x0003F1A2 File Offset: 0x0003D3A2
		public virtual bool IsStartObject(XmlReader reader)
		{
			XmlObjectSerializer.CheckNull(reader, "reader");
			return this.IsStartObject(XmlDictionaryReader.CreateDictionaryReader(reader));
		}

		/// <summary>Gets a value that specifies whether the <see cref="T:System.Xml.XmlDictionaryReader" /> is positioned over an XML element that can be read.</summary>
		/// <param name="reader">An <see cref="T:System.Xml.XmlDictionaryReader" /> used to read the XML stream or document.</param>
		/// <returns>
		///   <see langword="true" /> if the reader can read the data; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000FE5 RID: 4069
		public abstract bool IsStartObject(XmlDictionaryReader reader);

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0003F1BB File Offset: 0x0003D3BB
		internal virtual object InternalReadObject(XmlReaderDelegator reader, bool verifyObjectName)
		{
			return this.ReadObject(reader.UnderlyingReader, verifyObjectName);
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0003F1CA File Offset: 0x0003D3CA
		internal virtual object InternalReadObject(XmlReaderDelegator reader, bool verifyObjectName, DataContractResolver dataContractResolver)
		{
			return this.InternalReadObject(reader, verifyObjectName);
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x00003141 File Offset: 0x00001341
		internal virtual bool InternalIsStartObject(XmlReaderDelegator reader)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0003F1D4 File Offset: 0x0003D3D4
		internal object ReadObjectHandleExceptions(XmlReaderDelegator reader, bool verifyObjectName)
		{
			return this.ReadObjectHandleExceptions(reader, verifyObjectName, null);
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x0003F1E0 File Offset: 0x0003D3E0
		internal object ReadObjectHandleExceptions(XmlReaderDelegator reader, bool verifyObjectName, DataContractResolver dataContractResolver)
		{
			object result;
			try
			{
				XmlObjectSerializer.CheckNull(reader, "reader");
				if (DiagnosticUtility.ShouldTraceInformation)
				{
					TraceUtility.Trace(TraceEventType.Information, 196613, SR.GetString("ReadObject begins"), new StringTraceRecord("Type", XmlObjectSerializer.GetTypeInfo(this.GetDeserializeType())));
					object obj = this.InternalReadObject(reader, verifyObjectName, dataContractResolver);
					TraceUtility.Trace(TraceEventType.Information, 196614, SR.GetString("ReadObject ends"), new StringTraceRecord("Type", XmlObjectSerializer.GetTypeInfo(this.GetDeserializeType())));
					result = obj;
				}
				else
				{
					result = this.InternalReadObject(reader, verifyObjectName, dataContractResolver);
				}
			}
			catch (XmlException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(XmlObjectSerializer.GetTypeInfoError("There was an error deserializing the object {0}. {1}", this.GetDeserializeType(), innerException), innerException));
			}
			catch (FormatException innerException2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(XmlObjectSerializer.GetTypeInfoError("There was an error deserializing the object {0}. {1}", this.GetDeserializeType(), innerException2), innerException2));
			}
			return result;
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0003F2C4 File Offset: 0x0003D4C4
		internal bool IsStartObjectHandleExceptions(XmlReaderDelegator reader)
		{
			bool result;
			try
			{
				XmlObjectSerializer.CheckNull(reader, "reader");
				result = this.InternalIsStartObject(reader);
			}
			catch (XmlException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(XmlObjectSerializer.GetTypeInfoError("There was an error checking start element of object {0}. {1}", this.GetDeserializeType(), innerException), innerException));
			}
			catch (FormatException innerException2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(XmlObjectSerializer.GetTypeInfoError("There was an error checking start element of object {0}. {1}", this.GetDeserializeType(), innerException2), innerException2));
			}
			return result;
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x0003F340 File Offset: 0x0003D540
		internal bool IsRootXmlAny(XmlDictionaryString rootName, DataContract contract)
		{
			return rootName == null && !contract.HasRoot;
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0003F350 File Offset: 0x0003D550
		internal bool IsStartElement(XmlReaderDelegator reader)
		{
			return reader.MoveToElement() || reader.IsStartElement();
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0003F364 File Offset: 0x0003D564
		internal bool IsRootElement(XmlReaderDelegator reader, DataContract contract, XmlDictionaryString name, XmlDictionaryString ns)
		{
			reader.MoveToElement();
			if (name != null)
			{
				return reader.IsStartElement(name, ns);
			}
			if (!contract.HasRoot)
			{
				return reader.IsStartElement();
			}
			if (reader.IsStartElement(contract.TopLevelElementName, contract.TopLevelElementNamespace))
			{
				return true;
			}
			ClassDataContract classDataContract = contract as ClassDataContract;
			if (classDataContract != null)
			{
				classDataContract = classDataContract.BaseContract;
			}
			while (classDataContract != null)
			{
				if (reader.IsStartElement(classDataContract.TopLevelElementName, classDataContract.TopLevelElementNamespace))
				{
					return true;
				}
				classDataContract = classDataContract.BaseContract;
			}
			if (classDataContract == null)
			{
				DataContract primitiveDataContract = PrimitiveDataContract.GetPrimitiveDataContract(Globals.TypeOfObject);
				if (reader.IsStartElement(primitiveDataContract.TopLevelElementName, primitiveDataContract.TopLevelElementNamespace))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0003F402 File Offset: 0x0003D602
		internal static void CheckNull(object obj, string name)
		{
			if (obj == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException(name));
			}
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0003F414 File Offset: 0x0003D614
		internal static string TryAddLineInfo(XmlReaderDelegator reader, string errorMessage)
		{
			if (reader.HasLineInfo())
			{
				return string.Format(CultureInfo.InvariantCulture, "{0} {1}", SR.GetString("Error in line {0} position {1}.", new object[]
				{
					reader.LineNumber,
					reader.LinePosition
				}), errorMessage);
			}
			return errorMessage;
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0003F468 File Offset: 0x0003D668
		internal static Exception CreateSerializationExceptionWithReaderDetails(string errorMessage, XmlReaderDelegator reader)
		{
			return XmlObjectSerializer.CreateSerializationException(XmlObjectSerializer.TryAddLineInfo(reader, SR.GetString("{0}. Encountered '{1}'  with name '{2}', namespace '{3}'.", new object[]
			{
				errorMessage,
				reader.NodeType,
				reader.LocalName,
				reader.NamespaceURI
			})));
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0003F4B4 File Offset: 0x0003D6B4
		internal static SerializationException CreateSerializationException(string errorMessage)
		{
			return XmlObjectSerializer.CreateSerializationException(errorMessage, null);
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0003F4BD File Offset: 0x0003D6BD
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static SerializationException CreateSerializationException(string errorMessage, Exception innerException)
		{
			return new SerializationException(errorMessage, innerException);
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0003F4C6 File Offset: 0x0003D6C6
		private static string GetTypeInfo(Type type)
		{
			if (!(type == null))
			{
				return DataContract.GetClrTypeFullName(type);
			}
			return string.Empty;
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0003F4E0 File Offset: 0x0003D6E0
		private static string GetTypeInfoError(string errorMessage, Type type, Exception innerException)
		{
			string text = (type == null) ? string.Empty : SR.GetString("of type {0}", new object[]
			{
				DataContract.GetClrTypeFullName(type)
			});
			string text2 = (innerException == null) ? string.Empty : innerException.Message;
			return SR.GetString(errorMessage, new object[]
			{
				text,
				text2
			});
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0003F53C File Offset: 0x0003D73C
		internal virtual Type GetSerializeType(object graph)
		{
			if (graph != null)
			{
				return graph.GetType();
			}
			return null;
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0001BF43 File Offset: 0x0001A143
		internal virtual Type GetDeserializeType()
		{
			return null;
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x0003F549 File Offset: 0x0003D749
		internal static IFormatterConverter FormatterConverter
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlObjectSerializer.formatterConverter == null)
				{
					XmlObjectSerializer.formatterConverter = new FormatterConverter();
				}
				return XmlObjectSerializer.formatterConverter;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.XmlObjectSerializer" /> class.</summary>
		// Token: 0x06000FF9 RID: 4089 RVA: 0x0000222F File Offset: 0x0000042F
		protected XmlObjectSerializer()
		{
		}

		// Token: 0x040006F2 RID: 1778
		[SecurityCritical]
		private static IFormatterConverter formatterConverter;
	}
}
