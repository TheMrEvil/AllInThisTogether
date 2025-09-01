using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Configuration;
using System.Runtime.Serialization.Formatters;
using System.Security;
using System.Security.Permissions;
using System.Xml;

namespace System.Runtime.Serialization
{
	/// <summary>Serializes and deserializes an instance of a type into XML stream or document using the supplied .NET Framework types. This class cannot be inherited.</summary>
	// Token: 0x020000EE RID: 238
	public sealed class NetDataContractSerializer : XmlObjectSerializer, IFormatter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.NetDataContractSerializer" /> class.</summary>
		// Token: 0x06000D82 RID: 3458 RVA: 0x00035D34 File Offset: 0x00033F34
		public NetDataContractSerializer() : this(new StreamingContext(StreamingContextStates.All))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.NetDataContractSerializer" /> class with the supplied streaming context data.</summary>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains context data.</param>
		// Token: 0x06000D83 RID: 3459 RVA: 0x00035D46 File Offset: 0x00033F46
		public NetDataContractSerializer(StreamingContext context) : this(context, int.MaxValue, false, FormatterAssemblyStyle.Full, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.NetDataContractSerializer" /> class with the supplied context data; in addition, specifies the maximum number of items in the object to be serialized, and parameters to specify whether extra data is ignored, the assembly loading method, and a surrogate selector.</summary>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains context data.</param>
		/// <param name="maxItemsInObjectGraph">The maximum number of items in the graph to serialize or deserialize.</param>
		/// <param name="ignoreExtensionDataObject">
		///   <see langword="true" /> to ignore the data supplied by an extension of the type; otherwise, <see langword="false" />.</param>
		/// <param name="assemblyFormat">A <see cref="T:System.Runtime.Serialization.Formatters.FormatterAssemblyStyle" /> enumeration value that specifies a method for locating and loading assemblies.</param>
		/// <param name="surrogateSelector">An implementation of the <see cref="T:System.Runtime.Serialization.ISurrogateSelector" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxItemsInObjectGraph" /> value is less than 0.</exception>
		// Token: 0x06000D84 RID: 3460 RVA: 0x00035D57 File Offset: 0x00033F57
		public NetDataContractSerializer(StreamingContext context, int maxItemsInObjectGraph, bool ignoreExtensionDataObject, FormatterAssemblyStyle assemblyFormat, ISurrogateSelector surrogateSelector)
		{
			this.Initialize(context, maxItemsInObjectGraph, ignoreExtensionDataObject, assemblyFormat, surrogateSelector);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.NetDataContractSerializer" /> class with the supplied XML root element and namespace.</summary>
		/// <param name="rootName">The name of the XML element that encloses the content to serialize or deserialize.</param>
		/// <param name="rootNamespace">The namespace of the XML element that encloses the content to serialize or deserialize.</param>
		// Token: 0x06000D85 RID: 3461 RVA: 0x00035D6C File Offset: 0x00033F6C
		public NetDataContractSerializer(string rootName, string rootNamespace) : this(rootName, rootNamespace, new StreamingContext(StreamingContextStates.All), int.MaxValue, false, FormatterAssemblyStyle.Full, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.NetDataContractSerializer" /> class with the supplied context data and root name and namespace; in addition, specifies the maximum number of items in the object to be serialized, and parameters to specify whether extra data is ignored, the assembly loading method, and a surrogate selector.</summary>
		/// <param name="rootName">The name of the XML element that encloses the content to serialize or deserialize.</param>
		/// <param name="rootNamespace">The namespace of the XML element that encloses the content to serialize or deserialize.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains context data.</param>
		/// <param name="maxItemsInObjectGraph">The maximum number of items in the graph to serialize or deserialize.</param>
		/// <param name="ignoreExtensionDataObject">
		///   <see langword="true" /> to ignore the data supplied by an extension of the type; otherwise, <see langword="false" />.</param>
		/// <param name="assemblyFormat">A <see cref="T:System.Runtime.Serialization.Formatters.FormatterAssemblyStyle" /> enumeration value that specifies a method for locating and loading assemblies.</param>
		/// <param name="surrogateSelector">An implementation of the <see cref="T:System.Runtime.Serialization.IDataContractSurrogate" /> to handle the legacy type.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxItemsInObjectGraph" /> value is less than 0.</exception>
		// Token: 0x06000D86 RID: 3462 RVA: 0x00035D88 File Offset: 0x00033F88
		public NetDataContractSerializer(string rootName, string rootNamespace, StreamingContext context, int maxItemsInObjectGraph, bool ignoreExtensionDataObject, FormatterAssemblyStyle assemblyFormat, ISurrogateSelector surrogateSelector)
		{
			XmlDictionary xmlDictionary = new XmlDictionary(2);
			this.Initialize(xmlDictionary.Add(rootName), xmlDictionary.Add(DataContract.GetNamespace(rootNamespace)), context, maxItemsInObjectGraph, ignoreExtensionDataObject, assemblyFormat, surrogateSelector);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.NetDataContractSerializer" /> class with two parameters of type <see cref="T:System.Xml.XmlDictionaryString" /> that contain the root element and namespace used to specify the content.</summary>
		/// <param name="rootName">An <see cref="T:System.Xml.XmlDictionaryString" /> that contains the name of the XML element that encloses the content to serialize or deserialize.</param>
		/// <param name="rootNamespace">An <see cref="T:System.Xml.XmlDictionaryString" /> that contains the namespace of the XML element that encloses the content to serialize or deserialize.</param>
		// Token: 0x06000D87 RID: 3463 RVA: 0x00035DC4 File Offset: 0x00033FC4
		public NetDataContractSerializer(XmlDictionaryString rootName, XmlDictionaryString rootNamespace) : this(rootName, rootNamespace, new StreamingContext(StreamingContextStates.All), int.MaxValue, false, FormatterAssemblyStyle.Full, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.NetDataContractSerializer" /> class with the supplied context data, and root name and namespace (as <see cref="T:System.Xml.XmlDictionaryString" /> parameters); in addition, specifies the maximum number of items in the object to be serialized, and parameters to specify whether extra data found is ignored, assembly loading method, and a surrogate selector.</summary>
		/// <param name="rootName">An <see cref="T:System.Xml.XmlDictionaryString" /> that contains the root element of the content.</param>
		/// <param name="rootNamespace">An <see cref="T:System.Xml.XmlDictionaryString" /> that contains the namespace of the root element.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains context data.</param>
		/// <param name="maxItemsInObjectGraph">The maximum number of items in the graph to serialize or deserialize.</param>
		/// <param name="ignoreExtensionDataObject">
		///   <see langword="true" /> to ignore the data supplied by an extension of the type; otherwise, <see langword="false" />.</param>
		/// <param name="assemblyFormat">A <see cref="T:System.Runtime.Serialization.Formatters.FormatterAssemblyStyle" /> enumeration value that specifies a method for locating and loading assemblies.</param>
		/// <param name="surrogateSelector">An implementation of the <see cref="T:System.Runtime.Serialization.IDataContractSurrogate" /> to handle the legacy type.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxItemsInObjectGraph" /> value is less than 0.</exception>
		// Token: 0x06000D88 RID: 3464 RVA: 0x00035DE0 File Offset: 0x00033FE0
		public NetDataContractSerializer(XmlDictionaryString rootName, XmlDictionaryString rootNamespace, StreamingContext context, int maxItemsInObjectGraph, bool ignoreExtensionDataObject, FormatterAssemblyStyle assemblyFormat, ISurrogateSelector surrogateSelector)
		{
			this.Initialize(rootName, rootNamespace, context, maxItemsInObjectGraph, ignoreExtensionDataObject, assemblyFormat, surrogateSelector);
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x00035DFC File Offset: 0x00033FFC
		private void Initialize(StreamingContext context, int maxItemsInObjectGraph, bool ignoreExtensionDataObject, FormatterAssemblyStyle assemblyFormat, ISurrogateSelector surrogateSelector)
		{
			this.context = context;
			if (maxItemsInObjectGraph < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("maxItemsInObjectGraph", SR.GetString("The value of this argument must be non-negative.")));
			}
			this.maxItemsInObjectGraph = maxItemsInObjectGraph;
			this.ignoreExtensionDataObject = ignoreExtensionDataObject;
			this.surrogateSelector = surrogateSelector;
			this.AssemblyFormat = assemblyFormat;
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00035E4C File Offset: 0x0003404C
		private void Initialize(XmlDictionaryString rootName, XmlDictionaryString rootNamespace, StreamingContext context, int maxItemsInObjectGraph, bool ignoreExtensionDataObject, FormatterAssemblyStyle assemblyFormat, ISurrogateSelector surrogateSelector)
		{
			this.Initialize(context, maxItemsInObjectGraph, ignoreExtensionDataObject, assemblyFormat, surrogateSelector);
			this.rootName = rootName;
			this.rootNamespace = rootNamespace;
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x00035E6C File Offset: 0x0003406C
		internal static bool UnsafeTypeForwardingEnabled
		{
			[SecuritySafeCritical]
			get
			{
				if (NetDataContractSerializer.unsafeTypeForwardingEnabled == null)
				{
					NetDataContractSerializerSection netDataContractSerializerSection;
					if (NetDataContractSerializerSection.TryUnsafeGetSection(out netDataContractSerializerSection))
					{
						NetDataContractSerializer.unsafeTypeForwardingEnabled = new bool?(netDataContractSerializerSection.EnableUnsafeTypeForwarding);
					}
					else
					{
						NetDataContractSerializer.unsafeTypeForwardingEnabled = new bool?(false);
					}
				}
				return NetDataContractSerializer.unsafeTypeForwardingEnabled.Value;
			}
		}

		/// <summary>Gets or sets the object that enables the passing of context data that is useful while serializing or deserializing.</summary>
		/// <returns>A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the context data.</returns>
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x00035EB5 File Offset: 0x000340B5
		// (set) Token: 0x06000D8D RID: 3469 RVA: 0x00035EBD File Offset: 0x000340BD
		public StreamingContext Context
		{
			get
			{
				return this.context;
			}
			set
			{
				this.context = value;
			}
		}

		/// <summary>Gets or sets an object that controls class loading.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.SerializationBinder" /> used with the current formatter.</returns>
		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x00035EC6 File Offset: 0x000340C6
		// (set) Token: 0x06000D8F RID: 3471 RVA: 0x00035ECE File Offset: 0x000340CE
		public SerializationBinder Binder
		{
			get
			{
				return this.binder;
			}
			set
			{
				this.binder = value;
			}
		}

		/// <summary>Gets or sets an object that assists the formatter when selecting a surrogate for serialization.</summary>
		/// <returns>An <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> for selecting a surrogate.</returns>
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x00035ED7 File Offset: 0x000340D7
		// (set) Token: 0x06000D91 RID: 3473 RVA: 0x00035EDF File Offset: 0x000340DF
		public ISurrogateSelector SurrogateSelector
		{
			get
			{
				return this.surrogateSelector;
			}
			set
			{
				this.surrogateSelector = value;
			}
		}

		/// <summary>Gets a value that specifies a method for locating and loading assemblies.</summary>
		/// <returns>A <see cref="T:System.Runtime.Serialization.Formatters.FormatterAssemblyStyle" /> enumeration value that specifies a method for locating and loading assemblies.</returns>
		/// <exception cref="T:System.ArgumentException">The value being set does not correspond to any of the <see cref="T:System.Runtime.Serialization.Formatters.FormatterAssemblyStyle" /> values.</exception>
		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x00035EE8 File Offset: 0x000340E8
		// (set) Token: 0x06000D93 RID: 3475 RVA: 0x00035EF0 File Offset: 0x000340F0
		public FormatterAssemblyStyle AssemblyFormat
		{
			get
			{
				return this.assemblyFormat;
			}
			set
			{
				if (value != FormatterAssemblyStyle.Full && value != FormatterAssemblyStyle.Simple)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("'{0}': invalid assembly format.", new object[]
					{
						value
					})));
				}
				this.assemblyFormat = value;
			}
		}

		/// <summary>Gets the maximum number of items allowed in the object to be serialized.</summary>
		/// <returns>The maximum number of items allowed in the object. The default is <see cref="F:System.Int32.MaxValue" />.</returns>
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x00035F24 File Offset: 0x00034124
		public int MaxItemsInObjectGraph
		{
			get
			{
				return this.maxItemsInObjectGraph;
			}
		}

		/// <summary>Gets a value that specifies whether data supplied by an extension of the object is ignored.</summary>
		/// <returns>
		///   <see langword="true" /> to ignore the data supplied by an extension of the type; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x00035F2C File Offset: 0x0003412C
		public bool IgnoreExtensionDataObject
		{
			get
			{
				return this.ignoreExtensionDataObject;
			}
		}

		/// <summary>Serializes the specified object graph using the specified writer.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> to serialize with.</param>
		/// <param name="graph">The object to serialize. All child objects of this root object are automatically serialized.</param>
		// Token: 0x06000D96 RID: 3478 RVA: 0x00035F34 File Offset: 0x00034134
		public void Serialize(Stream stream, object graph)
		{
			base.WriteObject(stream, graph);
		}

		/// <summary>Deserializes an XML document or stream into an object.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the XML to deserialize.</param>
		/// <returns>The deserialized object.</returns>
		// Token: 0x06000D97 RID: 3479 RVA: 0x00035F3E File Offset: 0x0003413E
		public object Deserialize(Stream stream)
		{
			return base.ReadObject(stream);
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00035F48 File Offset: 0x00034148
		internal override void InternalWriteObject(XmlWriterDelegator writer, object graph)
		{
			Hashtable surrogateDataContracts = null;
			DataContract dataContract = this.GetDataContract(graph, ref surrogateDataContracts);
			this.InternalWriteStartObject(writer, graph, dataContract);
			this.InternalWriteObjectContent(writer, graph, dataContract, surrogateDataContracts);
			this.InternalWriteEndObject(writer);
		}

		/// <summary>Writes the complete content (start, content, and end) of the object to the XML document or stream with the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> used to write the XML document or stream.</param>
		/// <param name="graph">The object containing the content to write.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">the type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">there is a problem with the instance being serialized.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">the maximum number of object to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		// Token: 0x06000D99 RID: 3481 RVA: 0x000306EE File Offset: 0x0002E8EE
		public override void WriteObject(XmlWriter writer, object graph)
		{
			base.WriteObjectHandleExceptions(new XmlWriterDelegator(writer), graph);
		}

		/// <summary>Writes the opening XML element using an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlDictionaryWriter" /> used to write the XML element.</param>
		/// <param name="graph">The object to serialize. All child objects of this root object are automatically serialized.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">the type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">there is a problem with the instance being serialized.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">the maximum number of object to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		// Token: 0x06000D9A RID: 3482 RVA: 0x000306FD File Offset: 0x0002E8FD
		public override void WriteStartObject(XmlWriter writer, object graph)
		{
			base.WriteStartObjectHandleExceptions(new XmlWriterDelegator(writer), graph);
		}

		/// <summary>Writes the XML content using an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> used to write the XML content.</param>
		/// <param name="graph">The object to serialize. All child objects of this root object are automatically serialized.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">the type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">there is a problem with the instance being serialized.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">the maximum number of object to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		// Token: 0x06000D9B RID: 3483 RVA: 0x0003070C File Offset: 0x0002E90C
		public override void WriteObjectContent(XmlWriter writer, object graph)
		{
			base.WriteObjectContentHandleExceptions(new XmlWriterDelegator(writer), graph);
		}

		/// <summary>Writes the closing XML element using an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> used to write the XML document or stream.</param>
		/// <exception cref="T:System.ArgumentNullException">the <paramref name="writer" /> is set to <see langword="null" />.</exception>
		// Token: 0x06000D9C RID: 3484 RVA: 0x0003071B File Offset: 0x0002E91B
		public override void WriteEndObject(XmlWriter writer)
		{
			base.WriteEndObjectHandleExceptions(new XmlWriterDelegator(writer));
		}

		/// <summary>Writes the opening XML element using an <see cref="T:System.Xml.XmlDictionaryWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlDictionaryWriter" /> used to write the XML element.</param>
		/// <param name="graph">The object to serialize. All child objects of this root object are automatically serialized.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">the type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">there is a problem with the instance being serialized.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">the maximum number of object to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		// Token: 0x06000D9D RID: 3485 RVA: 0x000306FD File Offset: 0x0002E8FD
		public override void WriteStartObject(XmlDictionaryWriter writer, object graph)
		{
			base.WriteStartObjectHandleExceptions(new XmlWriterDelegator(writer), graph);
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00035F7C File Offset: 0x0003417C
		internal override void InternalWriteStartObject(XmlWriterDelegator writer, object graph)
		{
			Hashtable hashtable = null;
			DataContract dataContract = this.GetDataContract(graph, ref hashtable);
			this.InternalWriteStartObject(writer, graph, dataContract);
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00035FA0 File Offset: 0x000341A0
		private void InternalWriteStartObject(XmlWriterDelegator writer, object graph, DataContract contract)
		{
			base.WriteRootElement(writer, contract, this.rootName, this.rootNamespace, base.CheckIfNeedsContractNsAtRoot(this.rootName, this.rootNamespace, contract));
		}

		/// <summary>Writes the XML content using an <see cref="T:System.Xml.XmlDictionaryWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlDictionaryWriter" /> used to write the XML content.</param>
		/// <param name="graph">The object to serialize. All child objects of this root object are automatically serialized.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">the type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">there is a problem with the instance being serialized.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">the maximum number of object to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		// Token: 0x06000DA0 RID: 3488 RVA: 0x0003070C File Offset: 0x0002E90C
		public override void WriteObjectContent(XmlDictionaryWriter writer, object graph)
		{
			base.WriteObjectContentHandleExceptions(new XmlWriterDelegator(writer), graph);
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00035FD4 File Offset: 0x000341D4
		internal override void InternalWriteObjectContent(XmlWriterDelegator writer, object graph)
		{
			Hashtable surrogateDataContracts = null;
			DataContract dataContract = this.GetDataContract(graph, ref surrogateDataContracts);
			this.InternalWriteObjectContent(writer, graph, dataContract, surrogateDataContracts);
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00035FF8 File Offset: 0x000341F8
		private void InternalWriteObjectContent(XmlWriterDelegator writer, object graph, DataContract contract, Hashtable surrogateDataContracts)
		{
			if (this.MaxItemsInObjectGraph == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Maximum number of items that can be serialized or deserialized in an object graph is '{0}'.", new object[]
				{
					this.MaxItemsInObjectGraph
				})));
			}
			if (base.IsRootXmlAny(this.rootName, contract))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("For type '{0}', IsAny is not supported by NetDataContractSerializer.", new object[]
				{
					contract.UnderlyingType
				})));
			}
			if (graph == null)
			{
				XmlObjectSerializer.WriteNull(writer);
				return;
			}
			Type type = graph.GetType();
			if (contract.UnderlyingType != type)
			{
				contract = this.GetDataContract(graph, ref surrogateDataContracts);
			}
			XmlObjectSerializerWriteContext xmlObjectSerializerWriteContext = null;
			if (contract.CanContainReferences)
			{
				xmlObjectSerializerWriteContext = XmlObjectSerializerWriteContext.CreateContext(this, surrogateDataContracts);
				xmlObjectSerializerWriteContext.HandleGraphAtTopLevel(writer, graph, contract);
			}
			NetDataContractSerializer.WriteClrTypeInfo(writer, contract, this.binder);
			contract.WriteXmlValue(writer, graph, xmlObjectSerializerWriteContext);
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x000360C8 File Offset: 0x000342C8
		internal static void WriteClrTypeInfo(XmlWriterDelegator writer, DataContract dataContract, SerializationBinder binder)
		{
			if (!dataContract.IsISerializable && !(dataContract is SurrogateDataContract))
			{
				TypeInformation typeInformation = null;
				Type originalUnderlyingType = dataContract.OriginalUnderlyingType;
				string text = null;
				string text2 = null;
				if (binder != null)
				{
					binder.BindToName(originalUnderlyingType, out text2, out text);
				}
				if (text == null)
				{
					typeInformation = NetDataContractSerializer.GetTypeInformation(originalUnderlyingType);
					text = typeInformation.FullTypeName;
				}
				if (text2 == null)
				{
					text2 = ((typeInformation == null) ? NetDataContractSerializer.GetTypeInformation(originalUnderlyingType).AssemblyString : typeInformation.AssemblyString);
					if (!NetDataContractSerializer.UnsafeTypeForwardingEnabled && !originalUnderlyingType.Assembly.IsFullyTrusted && !NetDataContractSerializer.IsAssemblyNameForwardingSafe(originalUnderlyingType.Assembly.FullName, text2))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Type '{0}' in assembly '{1}' cannot be forwarded from assembly '{2}'.", new object[]
						{
							DataContract.GetClrTypeFullName(originalUnderlyingType),
							originalUnderlyingType.Assembly.FullName,
							text2
						})));
					}
				}
				NetDataContractSerializer.WriteClrTypeInfo(writer, text, text2);
			}
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00036198 File Offset: 0x00034398
		internal static void WriteClrTypeInfo(XmlWriterDelegator writer, Type dataContractType, SerializationBinder binder, string defaultClrTypeName, string defaultClrAssemblyName)
		{
			string text = null;
			string text2 = null;
			if (binder != null)
			{
				binder.BindToName(dataContractType, out text2, out text);
			}
			if (text == null)
			{
				text = defaultClrTypeName;
			}
			if (text2 == null)
			{
				text2 = defaultClrAssemblyName;
			}
			NetDataContractSerializer.WriteClrTypeInfo(writer, text, text2);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x000361CC File Offset: 0x000343CC
		internal static void WriteClrTypeInfo(XmlWriterDelegator writer, Type dataContractType, SerializationBinder binder, SerializationInfo serInfo)
		{
			TypeInformation typeInformation = null;
			string text = null;
			string text2 = null;
			if (binder != null)
			{
				binder.BindToName(dataContractType, out text2, out text);
			}
			if (text == null)
			{
				if (serInfo.IsFullTypeNameSetExplicit)
				{
					text = serInfo.FullTypeName;
				}
				else
				{
					typeInformation = NetDataContractSerializer.GetTypeInformation(serInfo.ObjectType);
					text = typeInformation.FullTypeName;
				}
			}
			if (text2 == null)
			{
				if (serInfo.IsAssemblyNameSetExplicit)
				{
					text2 = serInfo.AssemblyName;
				}
				else
				{
					text2 = ((typeInformation == null) ? NetDataContractSerializer.GetTypeInformation(serInfo.ObjectType).AssemblyString : typeInformation.AssemblyString);
				}
			}
			NetDataContractSerializer.WriteClrTypeInfo(writer, text, text2);
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x0003624C File Offset: 0x0003444C
		private static void WriteClrTypeInfo(XmlWriterDelegator writer, string clrTypeName, string clrAssemblyName)
		{
			if (clrTypeName != null)
			{
				writer.WriteAttributeString("z", DictionaryGlobals.ClrTypeLocalName, DictionaryGlobals.SerializationNamespace, DataContract.GetClrTypeString(clrTypeName));
			}
			if (clrAssemblyName != null)
			{
				writer.WriteAttributeString("z", DictionaryGlobals.ClrAssemblyLocalName, DictionaryGlobals.SerializationNamespace, DataContract.GetClrTypeString(clrAssemblyName));
			}
		}

		/// <summary>Writes the closing XML element using an <see cref="T:System.Xml.XmlDictionaryWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlDictionaryWriter" /> used to write the XML document or stream.</param>
		/// <exception cref="T:System.ArgumentNullException">the <paramref name="writer" /> is set to <see langword="null" />.</exception>
		// Token: 0x06000DA7 RID: 3495 RVA: 0x0003071B File Offset: 0x0002E91B
		public override void WriteEndObject(XmlDictionaryWriter writer)
		{
			base.WriteEndObjectHandleExceptions(new XmlWriterDelegator(writer));
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0003628A File Offset: 0x0003448A
		internal override void InternalWriteEndObject(XmlWriterDelegator writer)
		{
			writer.WriteEndElement();
		}

		/// <summary>Reads the XML stream or document with an <see cref="T:System.Xml.XmlDictionaryReader" /> and returns the deserialized object.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> used to read the XML stream or document.</param>
		/// <returns>The deserialized object.</returns>
		/// <exception cref="T:System.ArgumentNullException">the <paramref name="reader" /> is set to <see langword="null" />.</exception>
		// Token: 0x06000DA9 RID: 3497 RVA: 0x00030739 File Offset: 0x0002E939
		public override object ReadObject(XmlReader reader)
		{
			return base.ReadObjectHandleExceptions(new XmlReaderDelegator(reader), true);
		}

		/// <summary>Reads the XML stream or document with an <see cref="T:System.Xml.XmlDictionaryReader" /> and returns the deserialized object; also checks whether the object data conforms to the name and namespace used to create the serializer.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> used to read the XML stream or document.</param>
		/// <param name="verifyObjectName">
		///   <see langword="true" /> to check whether the enclosing XML element name and namespace correspond to the root name and root namespace used to construct the serializer; <see langword="false" /> to skip the verification.</param>
		/// <returns>The deserialized object.</returns>
		/// <exception cref="T:System.ArgumentNullException">the <paramref name="reader" /> is set to <see langword="null" />.</exception>
		// Token: 0x06000DAA RID: 3498 RVA: 0x00030748 File Offset: 0x0002E948
		public override object ReadObject(XmlReader reader, bool verifyObjectName)
		{
			return base.ReadObjectHandleExceptions(new XmlReaderDelegator(reader), verifyObjectName);
		}

		/// <summary>Determines whether the <see cref="T:System.Xml.XmlReader" /> is positioned on an object that can be deserialized using the specified reader.</summary>
		/// <param name="reader">An <see cref="T:System.Xml.XmlReader" /> that contains the XML to read.</param>
		/// <returns>
		///   <see langword="true" /> if the reader is at the start element of the stream to read; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">the <paramref name="reader" /> is set to <see langword="null" />.</exception>
		// Token: 0x06000DAB RID: 3499 RVA: 0x00030757 File Offset: 0x0002E957
		public override bool IsStartObject(XmlReader reader)
		{
			return base.IsStartObjectHandleExceptions(new XmlReaderDelegator(reader));
		}

		/// <summary>Reads the XML stream or document with an <see cref="T:System.Xml.XmlDictionaryReader" /> and returns the deserialized object; also checks whether the object data conforms to the name and namespace used to create the serializer.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlDictionaryReader" /> used to read the XML stream or document.</param>
		/// <param name="verifyObjectName">
		///   <see langword="true" /> to check whether the enclosing XML element name and namespace correspond to the root name and root namespace used to construct the serializer; <see langword="false" /> to skip the verification.</param>
		/// <returns>The deserialized object.</returns>
		/// <exception cref="T:System.ArgumentNullException">the <paramref name="reader" /> is set to <see langword="null" />.</exception>
		// Token: 0x06000DAC RID: 3500 RVA: 0x00030748 File Offset: 0x0002E948
		public override object ReadObject(XmlDictionaryReader reader, bool verifyObjectName)
		{
			return base.ReadObjectHandleExceptions(new XmlReaderDelegator(reader), verifyObjectName);
		}

		/// <summary>Determines whether the <see cref="T:System.Xml.XmlDictionaryReader" /> is positioned on an object that can be deserialized using the specified reader.</summary>
		/// <param name="reader">An <see cref="T:System.Xml.XmlDictionaryReader" /> that contains the XML to read.</param>
		/// <returns>
		///   <see langword="true" />, if the reader is at the start element of the stream to read; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">the <paramref name="reader" /> is set to <see langword="null" />.</exception>
		// Token: 0x06000DAD RID: 3501 RVA: 0x00030757 File Offset: 0x0002E957
		public override bool IsStartObject(XmlDictionaryReader reader)
		{
			return base.IsStartObjectHandleExceptions(new XmlReaderDelegator(reader));
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00036294 File Offset: 0x00034494
		internal override object InternalReadObject(XmlReaderDelegator xmlReader, bool verifyObjectName)
		{
			if (this.MaxItemsInObjectGraph == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Maximum number of items that can be serialized or deserialized in an object graph is '{0}'.", new object[]
				{
					this.MaxItemsInObjectGraph
				})));
			}
			if (!base.IsStartElement(xmlReader))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationExceptionWithReaderDetails(SR.GetString("Expecting state '{0}' when ReadObject is called.", new object[]
				{
					XmlNodeType.Element
				}), xmlReader));
			}
			return XmlObjectSerializerReadContext.CreateContext(this).InternalDeserialize(xmlReader, null, null, null);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0003630F File Offset: 0x0003450F
		internal override bool InternalIsStartObject(XmlReaderDelegator reader)
		{
			return base.IsStartElement(reader);
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x00036318 File Offset: 0x00034518
		internal DataContract GetDataContract(object obj, ref Hashtable surrogateDataContracts)
		{
			return this.GetDataContract((obj == null) ? Globals.TypeOfObject : obj.GetType(), ref surrogateDataContracts);
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00036331 File Offset: 0x00034531
		internal DataContract GetDataContract(Type type, ref Hashtable surrogateDataContracts)
		{
			return this.GetDataContract(type.TypeHandle, type, ref surrogateDataContracts);
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x00036344 File Offset: 0x00034544
		internal DataContract GetDataContract(RuntimeTypeHandle typeHandle, Type type, ref Hashtable surrogateDataContracts)
		{
			DataContract dataContract = NetDataContractSerializer.GetDataContractFromSurrogateSelector(this.surrogateSelector, this.Context, typeHandle, type, ref surrogateDataContracts);
			if (dataContract != null)
			{
				return dataContract;
			}
			if (this.cachedDataContract == null)
			{
				dataContract = DataContract.GetDataContract(typeHandle, type, SerializationMode.SharedType);
				this.cachedDataContract = dataContract;
				return dataContract;
			}
			DataContract dataContract2 = this.cachedDataContract;
			if (dataContract2.UnderlyingType.TypeHandle.Equals(typeHandle))
			{
				return dataContract2;
			}
			return DataContract.GetDataContract(typeHandle, type, SerializationMode.SharedType);
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x000363AC File Offset: 0x000345AC
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static ISerializationSurrogate GetSurrogate(Type type, ISurrogateSelector surrogateSelector, StreamingContext context)
		{
			ISurrogateSelector surrogateSelector2;
			return surrogateSelector.GetSurrogate(type, context, out surrogateSelector2);
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x000363C4 File Offset: 0x000345C4
		internal static DataContract GetDataContractFromSurrogateSelector(ISurrogateSelector surrogateSelector, StreamingContext context, RuntimeTypeHandle typeHandle, Type type, ref Hashtable surrogateDataContracts)
		{
			if (surrogateSelector == null)
			{
				return null;
			}
			if (type == null)
			{
				type = Type.GetTypeFromHandle(typeHandle);
			}
			DataContract builtInDataContract = DataContract.GetBuiltInDataContract(type);
			if (builtInDataContract != null)
			{
				return builtInDataContract;
			}
			if (surrogateDataContracts != null)
			{
				DataContract dataContract = (DataContract)surrogateDataContracts[type];
				if (dataContract != null)
				{
					return dataContract;
				}
			}
			DataContract dataContract2 = null;
			ISerializationSurrogate surrogate = NetDataContractSerializer.GetSurrogate(type, surrogateSelector, context);
			if (surrogate != null)
			{
				dataContract2 = new SurrogateDataContract(type, surrogate);
			}
			else if (type.IsArray)
			{
				Type elementType = type.GetElementType();
				DataContract dataContract3 = NetDataContractSerializer.GetDataContractFromSurrogateSelector(surrogateSelector, context, elementType.TypeHandle, elementType, ref surrogateDataContracts);
				if (dataContract3 == null)
				{
					dataContract3 = DataContract.GetDataContract(elementType.TypeHandle, elementType, SerializationMode.SharedType);
				}
				dataContract2 = new CollectionDataContract(type, dataContract3);
			}
			if (dataContract2 != null)
			{
				if (surrogateDataContracts == null)
				{
					surrogateDataContracts = new Hashtable();
				}
				surrogateDataContracts.Add(type, dataContract2);
				return dataContract2;
			}
			return null;
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x00036484 File Offset: 0x00034684
		internal static TypeInformation GetTypeInformation(Type type)
		{
			TypeInformation typeInformation = null;
			object obj = NetDataContractSerializer.typeNameCache[type];
			if (obj == null)
			{
				bool hasTypeForwardedFrom;
				string clrAssemblyName = DataContract.GetClrAssemblyName(type, out hasTypeForwardedFrom);
				typeInformation = new TypeInformation(DataContract.GetClrTypeFullNameUsingTypeForwardedFromAttribute(type), clrAssemblyName, hasTypeForwardedFrom);
				Hashtable obj2 = NetDataContractSerializer.typeNameCache;
				lock (obj2)
				{
					NetDataContractSerializer.typeNameCache[type] = typeInformation;
					return typeInformation;
				}
			}
			typeInformation = (TypeInformation)obj;
			return typeInformation;
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x00036500 File Offset: 0x00034700
		private static bool IsAssemblyNameForwardingSafe(string originalAssemblyName, string newAssemblyName)
		{
			if (originalAssemblyName == newAssemblyName)
			{
				return true;
			}
			AssemblyName assemblyName = new AssemblyName(originalAssemblyName);
			AssemblyName assemblyName2 = new AssemblyName(newAssemblyName);
			return !string.Equals(assemblyName2.Name, "mscorlib", StringComparison.OrdinalIgnoreCase) && !string.Equals(assemblyName2.Name, "mscorlib.dll", StringComparison.OrdinalIgnoreCase) && NetDataContractSerializer.IsPublicKeyTokenForwardingSafe(assemblyName.GetPublicKeyToken(), assemblyName2.GetPublicKeyToken());
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x00036560 File Offset: 0x00034760
		private static bool IsPublicKeyTokenForwardingSafe(byte[] sourceToken, byte[] destinationToken)
		{
			if (sourceToken == null || destinationToken == null || sourceToken.Length == 0 || destinationToken.Length == 0 || sourceToken.Length != destinationToken.Length)
			{
				return false;
			}
			for (int i = 0; i < sourceToken.Length; i++)
			{
				if (sourceToken[i] != destinationToken[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0003659E File Offset: 0x0003479E
		// Note: this type is marked as 'beforefieldinit'.
		static NetDataContractSerializer()
		{
		}

		// Token: 0x04000645 RID: 1605
		private XmlDictionaryString rootName;

		// Token: 0x04000646 RID: 1606
		private XmlDictionaryString rootNamespace;

		// Token: 0x04000647 RID: 1607
		private StreamingContext context;

		// Token: 0x04000648 RID: 1608
		private SerializationBinder binder;

		// Token: 0x04000649 RID: 1609
		private ISurrogateSelector surrogateSelector;

		// Token: 0x0400064A RID: 1610
		private int maxItemsInObjectGraph;

		// Token: 0x0400064B RID: 1611
		private bool ignoreExtensionDataObject;

		// Token: 0x0400064C RID: 1612
		private FormatterAssemblyStyle assemblyFormat;

		// Token: 0x0400064D RID: 1613
		private DataContract cachedDataContract;

		// Token: 0x0400064E RID: 1614
		private static Hashtable typeNameCache = new Hashtable();

		// Token: 0x0400064F RID: 1615
		private static bool? unsafeTypeForwardingEnabled;
	}
}
