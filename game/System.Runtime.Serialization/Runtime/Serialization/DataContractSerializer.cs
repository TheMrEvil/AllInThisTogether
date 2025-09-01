using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace System.Runtime.Serialization
{
	/// <summary>Serializes and deserializes an instance of a type into an XML stream or document using a supplied data contract. This class cannot be inherited.</summary>
	// Token: 0x020000C6 RID: 198
	public sealed class DataContractSerializer : XmlObjectSerializer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> class to serialize or deserialize an object of the specified type.</summary>
		/// <param name="type">The type of the instances that are serialized or deserialized.</param>
		// Token: 0x06000B49 RID: 2889 RVA: 0x0003031E File Offset: 0x0002E51E
		public DataContractSerializer(Type type) : this(type, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> class to serialize or deserialize an object of the specified type, and a collection of known types that may be present in the object graph.</summary>
		/// <param name="type">The type of the instances that are serialized or deserialized.</param>
		/// <param name="knownTypes">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Type" /> that contains the types that may be present in the object graph.</param>
		// Token: 0x06000B4A RID: 2890 RVA: 0x00030328 File Offset: 0x0002E528
		public DataContractSerializer(Type type, IEnumerable<Type> knownTypes) : this(type, knownTypes, int.MaxValue, false, false, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> class to serialize or deserialize an object of the specified type. This method also specifies a list of known types that may be present in the object graph, the maximum number of graph items to serialize, parameters to ignore unexpected data, whether to use non-standard XML constructs to preserve object reference data in the graph, and a surrogate for custom serialization.</summary>
		/// <param name="type">The type of the instances that are serialized or deserialized.</param>
		/// <param name="knownTypes">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Type" /> that contains the known types that may be present in the object graph.</param>
		/// <param name="maxItemsInObjectGraph">The maximum number of items in the graph to serialize or deserialize. The default is the value returned by the <see cref="F:System.Int32.MaxValue" /> property.</param>
		/// <param name="ignoreExtensionDataObject">
		///   <see langword="true" /> to ignore the data supplied by an extension of the type upon serialization and deserialization; otherwise, <see langword="false" />.</param>
		/// <param name="preserveObjectReferences">
		///   <see langword="true" /> to use non-standard XML constructs to preserve object reference data; otherwise, <see langword="false" />.</param>
		/// <param name="dataContractSurrogate">An implementation of the <see cref="T:System.Runtime.Serialization.IDataContractSurrogate" /> to customize the serialization process.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of items exceeds the maximum value.</exception>
		// Token: 0x06000B4B RID: 2891 RVA: 0x0003033A File Offset: 0x0002E53A
		public DataContractSerializer(Type type, IEnumerable<Type> knownTypes, int maxItemsInObjectGraph, bool ignoreExtensionDataObject, bool preserveObjectReferences, IDataContractSurrogate dataContractSurrogate) : this(type, knownTypes, maxItemsInObjectGraph, ignoreExtensionDataObject, preserveObjectReferences, dataContractSurrogate, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> class to serialize or deserialize an object of the specified type. This method also specifies a list of known types that may be present in the object graph, the maximum number of graph items to serialize, parameters to ignore unexpected data, whether to use non-standard XML constructs to preserve object reference data in the graph, a surrogate for custom serialization, and an alternative for mapping <see langword="xsi:type" /> declarations at run time.</summary>
		/// <param name="type">The type of the instances that are serialized or deserialized.</param>
		/// <param name="knownTypes">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Type" /> that contains the known types that may be present in the object graph.</param>
		/// <param name="maxItemsInObjectGraph">The maximum number of items in the graph to serialize or deserialize. The default is the value returned by the <see cref="F:System.Int32.MaxValue" /> property.</param>
		/// <param name="ignoreExtensionDataObject">
		///   <see langword="true" /> to ignore the data supplied by an extension of the type upon serialization and deserialization; otherwise, <see langword="false" />.</param>
		/// <param name="preserveObjectReferences">
		///   <see langword="true" /> to use non-standard XML constructs to preserve object reference data; otherwise, <see langword="false" />.</param>
		/// <param name="dataContractSurrogate">An implementation of the <see cref="T:System.Runtime.Serialization.IDataContractSurrogate" /> to customize the serialization process.</param>
		/// <param name="dataContractResolver">An implementation of the <see cref="T:System.Runtime.Serialization.DataContractResolver" /> to map <see langword="xsi:type" /> declarations to data contract types.</param>
		// Token: 0x06000B4C RID: 2892 RVA: 0x0003034C File Offset: 0x0002E54C
		public DataContractSerializer(Type type, IEnumerable<Type> knownTypes, int maxItemsInObjectGraph, bool ignoreExtensionDataObject, bool preserveObjectReferences, IDataContractSurrogate dataContractSurrogate, DataContractResolver dataContractResolver)
		{
			this.Initialize(type, knownTypes, maxItemsInObjectGraph, ignoreExtensionDataObject, preserveObjectReferences, dataContractSurrogate, dataContractResolver, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> class to serialize or deserialize an object of the specified type using the supplied XML root element and namespace.</summary>
		/// <param name="type">The type of the instances that are serialized or deserialized.</param>
		/// <param name="rootName">The name of the XML element that encloses the content to serialize or deserialize.</param>
		/// <param name="rootNamespace">The namespace of the XML element that encloses the content to serialize or deserialize.</param>
		// Token: 0x06000B4D RID: 2893 RVA: 0x00030371 File Offset: 0x0002E571
		public DataContractSerializer(Type type, string rootName, string rootNamespace) : this(type, rootName, rootNamespace, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> class to serialize or deserialize an object of the specified type. This method also specifies the root XML element and namespace in two string parameters as well as a list of known types that may be present in the object graph.</summary>
		/// <param name="type">The type of the instances that are serialized or deserialized.</param>
		/// <param name="rootName">The root element name of the content.</param>
		/// <param name="rootNamespace">The namespace of the root element.</param>
		/// <param name="knownTypes">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Type" /> that contains the types that may be present in the object graph.</param>
		// Token: 0x06000B4E RID: 2894 RVA: 0x00030380 File Offset: 0x0002E580
		public DataContractSerializer(Type type, string rootName, string rootNamespace, IEnumerable<Type> knownTypes) : this(type, rootName, rootNamespace, knownTypes, int.MaxValue, false, false, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> class to serialize or deserialize an object of the specified type. This method also specifies a list of known types that may be present in the object graph, the maximum number of graph items to serialize, parameters to ignore unexpected data, whether to use non-standard XML constructs to preserve object reference data in the graph, a surrogate for custom serialization, and the XML element and namespace that contain the content.</summary>
		/// <param name="type">The type of the instances that are serialized or deserialized.</param>
		/// <param name="rootName">The XML element that encloses the content to serialize or deserialize.</param>
		/// <param name="rootNamespace">The namespace of the XML element that encloses the content to serialize or deserialize.</param>
		/// <param name="knownTypes">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Type" /> that contains the known types that may be present in the object graph.</param>
		/// <param name="maxItemsInObjectGraph">The maximum number of items in the graph to serialize or deserialize.</param>
		/// <param name="ignoreExtensionDataObject">
		///   <see langword="true" /> to ignore the data supplied by an extension of the type upon serialization and deserialization; otherwise, <see langword="false" />.</param>
		/// <param name="preserveObjectReferences">
		///   <see langword="true" /> to use non-standard XML constructs to preserve object reference data; otherwise, <see langword="false" />.</param>
		/// <param name="dataContractSurrogate">An implementation of the <see cref="T:System.Runtime.Serialization.IDataContractSurrogate" /> to customize the serialization process.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of items exceeds the maximum value.</exception>
		// Token: 0x06000B4F RID: 2895 RVA: 0x000303A0 File Offset: 0x0002E5A0
		public DataContractSerializer(Type type, string rootName, string rootNamespace, IEnumerable<Type> knownTypes, int maxItemsInObjectGraph, bool ignoreExtensionDataObject, bool preserveObjectReferences, IDataContractSurrogate dataContractSurrogate) : this(type, rootName, rootNamespace, knownTypes, maxItemsInObjectGraph, ignoreExtensionDataObject, preserveObjectReferences, dataContractSurrogate, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> class to serialize or deserialize an object of the specified type. This method also specifies a list of known types that may be present in the object graph, the maximum number of graph items to serialize, parameters to ignore unexpected data, whether to use non-standard XML constructs to preserve object reference data in the graph, a surrogate for custom serialization, the XML element and namespace that contains the content, and an alternative for mapping <see langword="xsi:type" /> declarations at run time.</summary>
		/// <param name="type">The type of the instances that are serialized or deserialized.</param>
		/// <param name="rootName">The XML element that encloses the content to serialize or deserialize.</param>
		/// <param name="rootNamespace">The namespace of the XML element that encloses the content to serialize or deserialize.</param>
		/// <param name="knownTypes">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Type" /> that contains the known types that may be present in the object graph.</param>
		/// <param name="maxItemsInObjectGraph">The maximum number of items in the graph to serialize or deserialize.</param>
		/// <param name="ignoreExtensionDataObject">
		///   <see langword="true" /> to ignore the data supplied by an extension of the type upon serialization and deserialization; otherwise, <see langword="false" />.</param>
		/// <param name="preserveObjectReferences">
		///   <see langword="true" /> to use non-standard XML constructs to preserve object reference data; otherwise, <see langword="false" />.</param>
		/// <param name="dataContractSurrogate">An implementation of the <see cref="T:System.Runtime.Serialization.IDataContractSurrogate" /> to customize the serialization process.</param>
		/// <param name="dataContractResolver">An implementation of the <see cref="T:System.Runtime.Serialization.DataContractResolver" /> to map <see langword="xsi:type" /> declarations to data contract types.</param>
		// Token: 0x06000B50 RID: 2896 RVA: 0x000303C4 File Offset: 0x0002E5C4
		public DataContractSerializer(Type type, string rootName, string rootNamespace, IEnumerable<Type> knownTypes, int maxItemsInObjectGraph, bool ignoreExtensionDataObject, bool preserveObjectReferences, IDataContractSurrogate dataContractSurrogate, DataContractResolver dataContractResolver)
		{
			XmlDictionary xmlDictionary = new XmlDictionary(2);
			this.Initialize(type, xmlDictionary.Add(rootName), xmlDictionary.Add(DataContract.GetNamespace(rootNamespace)), knownTypes, maxItemsInObjectGraph, ignoreExtensionDataObject, preserveObjectReferences, dataContractSurrogate, dataContractResolver, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> class to serialize or deserialize an object of the specified type using the XML root element and namespace specified through the parameters of type <see cref="T:System.Xml.XmlDictionaryString" />.</summary>
		/// <param name="type">The type of the instances that are serialized or deserialized.</param>
		/// <param name="rootName">An <see cref="T:System.Xml.XmlDictionaryString" /> that contains the root element name of the content.</param>
		/// <param name="rootNamespace">An <see cref="T:System.Xml.XmlDictionaryString" /> that contains the namespace of the root element.</param>
		// Token: 0x06000B51 RID: 2897 RVA: 0x00030405 File Offset: 0x0002E605
		public DataContractSerializer(Type type, XmlDictionaryString rootName, XmlDictionaryString rootNamespace) : this(type, rootName, rootNamespace, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> class to serialize or deserialize an object of the specified type. This method also specifies the root XML element and namespace in two <see cref="T:System.Xml.XmlDictionaryString" /> parameters as well as a list of known types that may be present in the object graph.</summary>
		/// <param name="type">The type of the instances that are serialized or deserialized.</param>
		/// <param name="rootName">An <see cref="T:System.Xml.XmlDictionaryString" /> that contains the root element name of the content.</param>
		/// <param name="rootNamespace">An <see cref="T:System.Xml.XmlDictionaryString" /> that contains the namespace of the root element.</param>
		/// <param name="knownTypes">A <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Type" /> that contains the known types that may be present in the object graph.</param>
		// Token: 0x06000B52 RID: 2898 RVA: 0x00030414 File Offset: 0x0002E614
		public DataContractSerializer(Type type, XmlDictionaryString rootName, XmlDictionaryString rootNamespace, IEnumerable<Type> knownTypes) : this(type, rootName, rootNamespace, knownTypes, int.MaxValue, false, false, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> class to serialize or deserialize an object of the specified type. This method also specifies a list of known types that may be present in the object graph, the maximum number of graph items to serialize, parameters to ignore unexpected data, whether to use non-standard XML constructs to preserve object reference data in the graph, a surrogate for custom serialization, and parameters of <see cref="T:System.Xml.XmlDictionaryString" /> that specify the XML element and namespace that contain the content.</summary>
		/// <param name="type">The type of the instances that are serialized or deserialized.</param>
		/// <param name="rootName">The <see cref="T:System.Xml.XmlDictionaryString" /> that specifies the XML element that encloses the content to serialize or deserialize.</param>
		/// <param name="rootNamespace">The <see cref="T:System.Xml.XmlDictionaryString" /> that specifies the XML namespace of the root.</param>
		/// <param name="knownTypes">A <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Type" /> that contains the known types that may be present in the object graph.</param>
		/// <param name="maxItemsInObjectGraph">The maximum number of items in the graph to serialize or deserialize.</param>
		/// <param name="ignoreExtensionDataObject">
		///   <see langword="true" /> to ignore the data supplied by an extension of the type upon serialization and deserialization; otherwise, <see langword="false" />.</param>
		/// <param name="preserveObjectReferences">
		///   <see langword="true" /> to use non-standard XML constructs to preserve object reference data; otherwise, <see langword="false" />.</param>
		/// <param name="dataContractSurrogate">An implementation of the <see cref="T:System.Runtime.Serialization.IDataContractSurrogate" /> to customize the serialization process.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of items exceeds the maximum value.</exception>
		// Token: 0x06000B53 RID: 2899 RVA: 0x00030438 File Offset: 0x0002E638
		public DataContractSerializer(Type type, XmlDictionaryString rootName, XmlDictionaryString rootNamespace, IEnumerable<Type> knownTypes, int maxItemsInObjectGraph, bool ignoreExtensionDataObject, bool preserveObjectReferences, IDataContractSurrogate dataContractSurrogate) : this(type, rootName, rootNamespace, knownTypes, maxItemsInObjectGraph, ignoreExtensionDataObject, preserveObjectReferences, dataContractSurrogate, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> class to serialize or deserialize an object of the specified type. This method also specifies a list of known types that may be present in the object graph, the maximum number of graph items to serialize, parameters to ignore unexpected data, whether to use non-standard XML constructs to preserve object reference data in the graph, a surrogate for custom serialization, parameters of <see cref="T:System.Xml.XmlDictionaryString" /> that specify the XML element and namespace that contains the content, and an alternative for mapping <see langword="xsi:type" /> declarations at run time.</summary>
		/// <param name="type">The type of the instances that are serialized or deserialized.</param>
		/// <param name="rootName">The XML element that encloses the content to serialize or deserialize.</param>
		/// <param name="rootNamespace">The namespace of the XML element that encloses the content to serialize or deserialize.</param>
		/// <param name="knownTypes">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Type" /> that contains the known types that may be present in the object graph.</param>
		/// <param name="maxItemsInObjectGraph">The maximum number of items in the graph to serialize or deserialize.</param>
		/// <param name="ignoreExtensionDataObject">
		///   <see langword="true" /> to ignore the data supplied by an extension of the type upon serialization and deserialization; otherwise, <see langword="false" />.</param>
		/// <param name="preserveObjectReferences">
		///   <see langword="true" /> to use non-standard XML constructs to preserve object reference data; otherwise, <see langword="false" />.</param>
		/// <param name="dataContractSurrogate">An implementation of the <see cref="T:System.Runtime.Serialization.IDataContractSurrogate" /> to customize the serialization process.</param>
		/// <param name="dataContractResolver">An implementation of the <see cref="T:System.Runtime.Serialization.DataContractResolver" /> to map <see langword="xsi:type" /> declarations to data contract types.</param>
		// Token: 0x06000B54 RID: 2900 RVA: 0x0003045C File Offset: 0x0002E65C
		public DataContractSerializer(Type type, XmlDictionaryString rootName, XmlDictionaryString rootNamespace, IEnumerable<Type> knownTypes, int maxItemsInObjectGraph, bool ignoreExtensionDataObject, bool preserveObjectReferences, IDataContractSurrogate dataContractSurrogate, DataContractResolver dataContractResolver)
		{
			this.Initialize(type, rootName, rootNamespace, knownTypes, maxItemsInObjectGraph, ignoreExtensionDataObject, preserveObjectReferences, dataContractSurrogate, dataContractResolver, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> class to serialize or deserialize an object of the specified type and settings.</summary>
		/// <param name="type">The type of the instance to serialize or deserialize.</param>
		/// <param name="settings">The serializer settings.</param>
		// Token: 0x06000B55 RID: 2901 RVA: 0x00030488 File Offset: 0x0002E688
		public DataContractSerializer(Type type, DataContractSerializerSettings settings)
		{
			if (settings == null)
			{
				settings = new DataContractSerializerSettings();
			}
			this.Initialize(type, settings.RootName, settings.RootNamespace, settings.KnownTypes, settings.MaxItemsInObjectGraph, settings.IgnoreExtensionDataObject, settings.PreserveObjectReferences, settings.DataContractSurrogate, settings.DataContractResolver, settings.SerializeReadOnlyTypes);
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x000304E4 File Offset: 0x0002E6E4
		private void Initialize(Type type, IEnumerable<Type> knownTypes, int maxItemsInObjectGraph, bool ignoreExtensionDataObject, bool preserveObjectReferences, IDataContractSurrogate dataContractSurrogate, DataContractResolver dataContractResolver, bool serializeReadOnlyTypes)
		{
			XmlObjectSerializer.CheckNull(type, "type");
			this.rootType = type;
			if (knownTypes != null)
			{
				this.knownTypeList = new List<Type>();
				foreach (Type item in knownTypes)
				{
					this.knownTypeList.Add(item);
				}
			}
			if (maxItemsInObjectGraph < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("maxItemsInObjectGraph", SR.GetString("The value of this argument must be non-negative.")));
			}
			this.maxItemsInObjectGraph = maxItemsInObjectGraph;
			this.ignoreExtensionDataObject = ignoreExtensionDataObject;
			this.preserveObjectReferences = preserveObjectReferences;
			this.dataContractSurrogate = dataContractSurrogate;
			this.dataContractResolver = dataContractResolver;
			this.serializeReadOnlyTypes = serializeReadOnlyTypes;
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x000305A0 File Offset: 0x0002E7A0
		private void Initialize(Type type, XmlDictionaryString rootName, XmlDictionaryString rootNamespace, IEnumerable<Type> knownTypes, int maxItemsInObjectGraph, bool ignoreExtensionDataObject, bool preserveObjectReferences, IDataContractSurrogate dataContractSurrogate, DataContractResolver dataContractResolver, bool serializeReadOnlyTypes)
		{
			this.Initialize(type, knownTypes, maxItemsInObjectGraph, ignoreExtensionDataObject, preserveObjectReferences, dataContractSurrogate, dataContractResolver, serializeReadOnlyTypes);
			this.rootName = rootName;
			this.rootNamespace = rootNamespace;
		}

		/// <summary>Gets a collection of types that may be present in the object graph serialized using this instance of the <see cref="T:System.Runtime.Serialization.DataContractSerializer" />.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> that contains the expected types passed in as known types to the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> constructor.</returns>
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x000305D0 File Offset: 0x0002E7D0
		public ReadOnlyCollection<Type> KnownTypes
		{
			get
			{
				if (this.knownTypeCollection == null)
				{
					if (this.knownTypeList != null)
					{
						this.knownTypeCollection = new ReadOnlyCollection<Type>(this.knownTypeList);
					}
					else
					{
						this.knownTypeCollection = new ReadOnlyCollection<Type>(Globals.EmptyTypeArray);
					}
				}
				return this.knownTypeCollection;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x0003060B File Offset: 0x0002E80B
		internal override Dictionary<XmlQualifiedName, DataContract> KnownDataContracts
		{
			get
			{
				if (this.knownDataContracts == null && this.knownTypeList != null)
				{
					this.knownDataContracts = XmlObjectSerializerContext.GetDataContractsForKnownTypes(this.knownTypeList);
				}
				return this.knownDataContracts;
			}
		}

		/// <summary>Gets the maximum number of items in an object graph to serialize or deserialize.</summary>
		/// <returns>The maximum number of items to serialize or deserialize. The default is <see cref="F:System.Int32.MaxValue" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of items exceeds the maximum value.</exception>
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x00030634 File Offset: 0x0002E834
		public int MaxItemsInObjectGraph
		{
			get
			{
				return this.maxItemsInObjectGraph;
			}
		}

		/// <summary>Gets a surrogate type that can extend the serialization or deserialization process.</summary>
		/// <returns>An implementation of the <see cref="T:System.Runtime.Serialization.IDataContractSurrogate" /> class.</returns>
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x0003063C File Offset: 0x0002E83C
		public IDataContractSurrogate DataContractSurrogate
		{
			get
			{
				return this.dataContractSurrogate;
			}
		}

		/// <summary>Gets a value that specifies whether to use non-standard XML constructs to preserve object reference data.</summary>
		/// <returns>
		///   <see langword="true" /> to keep the references; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x00030644 File Offset: 0x0002E844
		public bool PreserveObjectReferences
		{
			get
			{
				return this.preserveObjectReferences;
			}
		}

		/// <summary>Gets a value that specifies whether to ignore data supplied by an extension of the class when the class is being serialized or deserialized.</summary>
		/// <returns>
		///   <see langword="true" /> to omit the extension data; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x0003064C File Offset: 0x0002E84C
		public bool IgnoreExtensionDataObject
		{
			get
			{
				return this.ignoreExtensionDataObject;
			}
		}

		/// <summary>Gets the component used to dynamically map <see langword="xsi:type" /> declarations to known contract types.</summary>
		/// <returns>An implementation of the <see cref="T:System.Runtime.Serialization.DataContractResolver" /> class.</returns>
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x00030654 File Offset: 0x0002E854
		public DataContractResolver DataContractResolver
		{
			get
			{
				return this.dataContractResolver;
			}
		}

		/// <summary>Gets a value that specifies whether read-only types are serialized.</summary>
		/// <returns>
		///   <see langword="true" /> if read-only types are serialized; <see langword="false" /> if all types are serialized.</returns>
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x0003065C File Offset: 0x0002E85C
		public bool SerializeReadOnlyTypes
		{
			get
			{
				return this.serializeReadOnlyTypes;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x00030664 File Offset: 0x0002E864
		private DataContract RootContract
		{
			get
			{
				if (this.rootContract == null)
				{
					this.rootContract = DataContract.GetDataContract((this.dataContractSurrogate == null) ? this.rootType : DataContractSerializer.GetSurrogatedType(this.dataContractSurrogate, this.rootType));
					this.needsContractNsAtRoot = base.CheckIfNeedsContractNsAtRoot(this.rootName, this.rootNamespace, this.rootContract);
				}
				return this.rootContract;
			}
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x000306C9 File Offset: 0x0002E8C9
		internal override void InternalWriteObject(XmlWriterDelegator writer, object graph)
		{
			this.InternalWriteObject(writer, graph, null);
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x000306D4 File Offset: 0x0002E8D4
		internal override void InternalWriteObject(XmlWriterDelegator writer, object graph, DataContractResolver dataContractResolver)
		{
			this.InternalWriteStartObject(writer, graph);
			this.InternalWriteObjectContent(writer, graph, dataContractResolver);
			this.InternalWriteEndObject(writer);
		}

		/// <summary>Writes all the object data (starting XML element, content, and closing element) to an XML document or stream with an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> used to write the XML document or stream.</param>
		/// <param name="graph">The object that contains the data to write to the stream.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">The type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">There is a problem with the instance being written.</exception>
		// Token: 0x06000B63 RID: 2915 RVA: 0x000306EE File Offset: 0x0002E8EE
		public override void WriteObject(XmlWriter writer, object graph)
		{
			base.WriteObjectHandleExceptions(new XmlWriterDelegator(writer), graph);
		}

		/// <summary>Writes the opening XML element using an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> used to write the XML start element.</param>
		/// <param name="graph">The object to write.</param>
		// Token: 0x06000B64 RID: 2916 RVA: 0x000306FD File Offset: 0x0002E8FD
		public override void WriteStartObject(XmlWriter writer, object graph)
		{
			base.WriteStartObjectHandleExceptions(new XmlWriterDelegator(writer), graph);
		}

		/// <summary>Writes the XML content using an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> used to write the stream.</param>
		/// <param name="graph">The object to write to the stream.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">The type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">There is a problem with the instance being written.</exception>
		// Token: 0x06000B65 RID: 2917 RVA: 0x0003070C File Offset: 0x0002E90C
		public override void WriteObjectContent(XmlWriter writer, object graph)
		{
			base.WriteObjectContentHandleExceptions(new XmlWriterDelegator(writer), graph);
		}

		/// <summary>Writes the closing XML element using an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> used to write the stream.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">The type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">There is a problem with the instance being written.</exception>
		// Token: 0x06000B66 RID: 2918 RVA: 0x0003071B File Offset: 0x0002E91B
		public override void WriteEndObject(XmlWriter writer)
		{
			base.WriteEndObjectHandleExceptions(new XmlWriterDelegator(writer));
		}

		/// <summary>Writes the opening XML element using an <see cref="T:System.Xml.XmlDictionaryWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlDictionaryWriter" /> used to write the XML start element.</param>
		/// <param name="graph">The object to write.</param>
		// Token: 0x06000B67 RID: 2919 RVA: 0x000306FD File Offset: 0x0002E8FD
		public override void WriteStartObject(XmlDictionaryWriter writer, object graph)
		{
			base.WriteStartObjectHandleExceptions(new XmlWriterDelegator(writer), graph);
		}

		/// <summary>Writes the XML content using an <see cref="T:System.Xml.XmlDictionaryWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlDictionaryWriter" /> used to write the stream.</param>
		/// <param name="graph">The object to write to the stream.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">The type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">There is a problem with the instance being written.</exception>
		// Token: 0x06000B68 RID: 2920 RVA: 0x0003070C File Offset: 0x0002E90C
		public override void WriteObjectContent(XmlDictionaryWriter writer, object graph)
		{
			base.WriteObjectContentHandleExceptions(new XmlWriterDelegator(writer), graph);
		}

		/// <summary>Writes the closing XML element using an <see cref="T:System.Xml.XmlDictionaryWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlDictionaryWriter" /> used to write the stream.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">The type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">There is a problem with the instance being written.</exception>
		// Token: 0x06000B69 RID: 2921 RVA: 0x0003071B File Offset: 0x0002E91B
		public override void WriteEndObject(XmlDictionaryWriter writer)
		{
			base.WriteEndObjectHandleExceptions(new XmlWriterDelegator(writer));
		}

		/// <summary>Writes all the object data (starting XML element, content, and enclosing element) to an XML document or stream  using the specified XmlDictionaryWriter. The method includes a resolver for mapping <see langword="xsi:type" /> declarations at runtime.</summary>
		/// <param name="writer">An XmlDictionaryWriter used to write the content to the XML document or stream.</param>
		/// <param name="graph">The object that contains the content to write.</param>
		/// <param name="dataContractResolver">An implementation of the <see cref="T:System.Runtime.Serialization.DataContractResolver" /> used to map <see langword="xsi:type" /> declarations to known data contracts.</param>
		// Token: 0x06000B6A RID: 2922 RVA: 0x00030729 File Offset: 0x0002E929
		public void WriteObject(XmlDictionaryWriter writer, object graph, DataContractResolver dataContractResolver)
		{
			base.WriteObjectHandleExceptions(new XmlWriterDelegator(writer), graph, dataContractResolver);
		}

		/// <summary>Reads the XML stream with an <see cref="T:System.Xml.XmlReader" /> and returns the deserialized object.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> used to read the XML stream.</param>
		/// <returns>The deserialized object.</returns>
		// Token: 0x06000B6B RID: 2923 RVA: 0x00030739 File Offset: 0x0002E939
		public override object ReadObject(XmlReader reader)
		{
			return base.ReadObjectHandleExceptions(new XmlReaderDelegator(reader), true);
		}

		/// <summary>Reads the XML stream with an <see cref="T:System.Xml.XmlReader" /> and returns the deserialized object, and also specifies whether a check is made to verify the object name before reading its value.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> used to read the XML stream.</param>
		/// <param name="verifyObjectName">
		///   <see langword="true" /> to check whether the name of the object corresponds to the root name value supplied in the constructor; otherwise, <see langword="false" />.</param>
		/// <returns>The deserialized object.</returns>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <paramref name="verifyObjectName" /> parameter is set to <see langword="true" />, and the element name and namespace do not correspond to the values set in the constructor.</exception>
		// Token: 0x06000B6C RID: 2924 RVA: 0x00030748 File Offset: 0x0002E948
		public override object ReadObject(XmlReader reader, bool verifyObjectName)
		{
			return base.ReadObjectHandleExceptions(new XmlReaderDelegator(reader), verifyObjectName);
		}

		/// <summary>Determines whether the <see cref="T:System.Xml.XmlReader" /> is positioned on an object that can be deserialized.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> used to read the XML stream.</param>
		/// <returns>
		///   <see langword="true" /> if the reader is at the start element of the stream to read; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000B6D RID: 2925 RVA: 0x00030757 File Offset: 0x0002E957
		public override bool IsStartObject(XmlReader reader)
		{
			return base.IsStartObjectHandleExceptions(new XmlReaderDelegator(reader));
		}

		/// <summary>Reads the XML stream with an <see cref="T:System.Xml.XmlDictionaryReader" /> and returns the deserialized object, and also specifies whether a check is made to verify the object name before reading its value.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlDictionaryReader" /> used to read the XML stream.</param>
		/// <param name="verifyObjectName">
		///   <see langword="true" /> to check whether the name of the object corresponds to the root name value supplied in the constructor; otherwise, <see langword="false" />.</param>
		/// <returns>The deserialized object.</returns>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <paramref name="verifyObjectName" /> parameter is set to <see langword="true" />, and the element name and namespace do not correspond to the values set in the constructor.</exception>
		// Token: 0x06000B6E RID: 2926 RVA: 0x00030748 File Offset: 0x0002E948
		public override object ReadObject(XmlDictionaryReader reader, bool verifyObjectName)
		{
			return base.ReadObjectHandleExceptions(new XmlReaderDelegator(reader), verifyObjectName);
		}

		/// <summary>Determines whether the <see cref="T:System.Xml.XmlDictionaryReader" /> is positioned on an object that can be deserialized.</summary>
		/// <param name="reader">An <see cref="T:System.Xml.XmlDictionaryReader" /> used to read the XML stream.</param>
		/// <returns>
		///   <see langword="true" /> if the reader is at the start element of the stream to read; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000B6F RID: 2927 RVA: 0x00030757 File Offset: 0x0002E957
		public override bool IsStartObject(XmlDictionaryReader reader)
		{
			return base.IsStartObjectHandleExceptions(new XmlReaderDelegator(reader));
		}

		/// <summary>Reads an XML document or document stream and returns the deserialized object.  The method includes a parameter to specify whether the object name is verified is validated, and a resolver for mapping <see langword="xsi:type" /> declarations at runtime.</summary>
		/// <param name="reader">The XML reader used to read the content.</param>
		/// <param name="verifyObjectName">
		///   <see langword="true" /> to verify the object name; otherwise, <see langword="false" />.</param>
		/// <param name="dataContractResolver">An implementation of the <see cref="T:System.Runtime.Serialization.DataContractResolver" /> to map <see langword="xsi:type" /> declarations to data contract types.</param>
		/// <returns>The deserialized object.</returns>
		// Token: 0x06000B70 RID: 2928 RVA: 0x00030765 File Offset: 0x0002E965
		public object ReadObject(XmlDictionaryReader reader, bool verifyObjectName, DataContractResolver dataContractResolver)
		{
			return base.ReadObjectHandleExceptions(new XmlReaderDelegator(reader), verifyObjectName, dataContractResolver);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00030775 File Offset: 0x0002E975
		internal override void InternalWriteStartObject(XmlWriterDelegator writer, object graph)
		{
			base.WriteRootElement(writer, this.RootContract, this.rootName, this.rootNamespace, this.needsContractNsAtRoot);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00030796 File Offset: 0x0002E996
		internal override void InternalWriteObjectContent(XmlWriterDelegator writer, object graph)
		{
			this.InternalWriteObjectContent(writer, graph, null);
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x000307A4 File Offset: 0x0002E9A4
		internal void InternalWriteObjectContent(XmlWriterDelegator writer, object graph, DataContractResolver dataContractResolver)
		{
			if (this.MaxItemsInObjectGraph == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Maximum number of items that can be serialized or deserialized in an object graph is '{0}'.", new object[]
				{
					this.MaxItemsInObjectGraph
				})));
			}
			DataContract dataContract = this.RootContract;
			Type underlyingType = dataContract.UnderlyingType;
			Type type = (graph == null) ? underlyingType : graph.GetType();
			if (this.dataContractSurrogate != null)
			{
				graph = DataContractSerializer.SurrogateToDataContractType(this.dataContractSurrogate, graph, underlyingType, ref type);
			}
			if (dataContractResolver == null)
			{
				dataContractResolver = this.DataContractResolver;
			}
			if (graph == null)
			{
				if (base.IsRootXmlAny(this.rootName, dataContract))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("A null value cannot be serialized at the top level for IXmlSerializable root type '{0}' since its IsAny setting is 'true'. This type must write all its contents including the root element. Verify that the IXmlSerializable implementation is correct.", new object[]
					{
						underlyingType
					})));
				}
				XmlObjectSerializer.WriteNull(writer);
				return;
			}
			else if (underlyingType == type)
			{
				if (dataContract.CanContainReferences)
				{
					XmlObjectSerializerWriteContext xmlObjectSerializerWriteContext = XmlObjectSerializerWriteContext.CreateContext(this, dataContract, dataContractResolver);
					xmlObjectSerializerWriteContext.HandleGraphAtTopLevel(writer, graph, dataContract);
					xmlObjectSerializerWriteContext.SerializeWithoutXsiType(dataContract, writer, graph, underlyingType.TypeHandle);
					return;
				}
				dataContract.WriteXmlValue(writer, graph, null);
				return;
			}
			else
			{
				if (base.IsRootXmlAny(this.rootName, dataContract))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("An object of type '{0}' cannot be serialized at the top level for IXmlSerializable root type '{1}' since its IsAny setting is 'true'. This type must write all its contents including the root element. Verify that the IXmlSerializable implementation is correct.", new object[]
					{
						type,
						dataContract.UnderlyingType
					})));
				}
				dataContract = DataContractSerializer.GetDataContract(dataContract, underlyingType, type);
				XmlObjectSerializerWriteContext xmlObjectSerializerWriteContext2 = XmlObjectSerializerWriteContext.CreateContext(this, this.RootContract, dataContractResolver);
				if (dataContract.CanContainReferences)
				{
					xmlObjectSerializerWriteContext2.HandleGraphAtTopLevel(writer, graph, dataContract);
				}
				xmlObjectSerializerWriteContext2.OnHandleIsReference(writer, dataContract, graph);
				xmlObjectSerializerWriteContext2.SerializeWithXsiTypeAtTopLevel(dataContract, writer, graph, underlyingType.TypeHandle, type);
				return;
			}
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0003090F File Offset: 0x0002EB0F
		internal static DataContract GetDataContract(DataContract declaredTypeContract, Type declaredType, Type objectType)
		{
			if (declaredType.IsInterface && CollectionDataContract.IsCollectionInterface(declaredType))
			{
				return declaredTypeContract;
			}
			if (declaredType.IsArray)
			{
				return declaredTypeContract;
			}
			return DataContract.GetDataContract(objectType.TypeHandle, objectType, SerializationMode.SharedContract);
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0003093A File Offset: 0x0002EB3A
		internal void SetDataContractSurrogate(IDataContractSurrogate adapter)
		{
			this.dataContractSurrogate = adapter;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00030943 File Offset: 0x0002EB43
		internal override void InternalWriteEndObject(XmlWriterDelegator writer)
		{
			if (!base.IsRootXmlAny(this.rootName, this.RootContract))
			{
				writer.WriteEndElement();
			}
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0003095F File Offset: 0x0002EB5F
		internal override object InternalReadObject(XmlReaderDelegator xmlReader, bool verifyObjectName)
		{
			return this.InternalReadObject(xmlReader, verifyObjectName, null);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0003096C File Offset: 0x0002EB6C
		internal override object InternalReadObject(XmlReaderDelegator xmlReader, bool verifyObjectName, DataContractResolver dataContractResolver)
		{
			if (this.MaxItemsInObjectGraph == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Maximum number of items that can be serialized or deserialized in an object graph is '{0}'.", new object[]
				{
					this.MaxItemsInObjectGraph
				})));
			}
			if (dataContractResolver == null)
			{
				dataContractResolver = this.DataContractResolver;
			}
			if (verifyObjectName)
			{
				if (!this.InternalIsStartObject(xmlReader))
				{
					XmlDictionaryString topLevelElementName;
					XmlDictionaryString topLevelElementNamespace;
					if (this.rootName == null)
					{
						topLevelElementName = this.RootContract.TopLevelElementName;
						topLevelElementNamespace = this.RootContract.TopLevelElementNamespace;
					}
					else
					{
						topLevelElementName = this.rootName;
						topLevelElementNamespace = this.rootNamespace;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationExceptionWithReaderDetails(SR.GetString("Expecting element '{1}' from namespace '{0}'.", new object[]
					{
						topLevelElementNamespace,
						topLevelElementName
					}), xmlReader));
				}
			}
			else if (!base.IsStartElement(xmlReader))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationExceptionWithReaderDetails(SR.GetString("Expecting state '{0}' when ReadObject is called.", new object[]
				{
					XmlNodeType.Element
				}), xmlReader));
			}
			DataContract dataContract = this.RootContract;
			if (dataContract.IsPrimitive && dataContract.UnderlyingType == this.rootType)
			{
				return dataContract.ReadXmlValue(xmlReader, null);
			}
			if (base.IsRootXmlAny(this.rootName, dataContract))
			{
				return XmlObjectSerializerReadContext.ReadRootIXmlSerializable(xmlReader, dataContract as XmlDataContract, false);
			}
			return XmlObjectSerializerReadContext.CreateContext(this, dataContract, dataContractResolver).InternalDeserialize(xmlReader, this.rootType, dataContract, null, null);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00030AA0 File Offset: 0x0002ECA0
		internal override bool InternalIsStartObject(XmlReaderDelegator reader)
		{
			return base.IsRootElement(reader, this.RootContract, this.rootName, this.rootNamespace);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00030ABB File Offset: 0x0002ECBB
		internal override Type GetSerializeType(object graph)
		{
			if (graph != null)
			{
				return graph.GetType();
			}
			return this.rootType;
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x00030ACD File Offset: 0x0002ECCD
		internal override Type GetDeserializeType()
		{
			return this.rootType;
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x00030AD8 File Offset: 0x0002ECD8
		internal static object SurrogateToDataContractType(IDataContractSurrogate dataContractSurrogate, object oldObj, Type surrogatedDeclaredType, ref Type objType)
		{
			object objectToSerialize = DataContractSurrogateCaller.GetObjectToSerialize(dataContractSurrogate, oldObj, objType, surrogatedDeclaredType);
			if (objectToSerialize != oldObj)
			{
				if (objectToSerialize == null)
				{
					objType = Globals.TypeOfObject;
				}
				else
				{
					objType = objectToSerialize.GetType();
				}
			}
			return objectToSerialize;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00030B09 File Offset: 0x0002ED09
		internal static Type GetSurrogatedType(IDataContractSurrogate dataContractSurrogate, Type type)
		{
			return DataContractSurrogateCaller.GetDataContractType(dataContractSurrogate, DataContract.UnwrapNullableType(type));
		}

		// Token: 0x04000495 RID: 1173
		private Type rootType;

		// Token: 0x04000496 RID: 1174
		private DataContract rootContract;

		// Token: 0x04000497 RID: 1175
		private bool needsContractNsAtRoot;

		// Token: 0x04000498 RID: 1176
		private XmlDictionaryString rootName;

		// Token: 0x04000499 RID: 1177
		private XmlDictionaryString rootNamespace;

		// Token: 0x0400049A RID: 1178
		private int maxItemsInObjectGraph;

		// Token: 0x0400049B RID: 1179
		private bool ignoreExtensionDataObject;

		// Token: 0x0400049C RID: 1180
		private bool preserveObjectReferences;

		// Token: 0x0400049D RID: 1181
		private IDataContractSurrogate dataContractSurrogate;

		// Token: 0x0400049E RID: 1182
		private ReadOnlyCollection<Type> knownTypeCollection;

		// Token: 0x0400049F RID: 1183
		internal IList<Type> knownTypeList;

		// Token: 0x040004A0 RID: 1184
		internal Dictionary<XmlQualifiedName, DataContract> knownDataContracts;

		// Token: 0x040004A1 RID: 1185
		private DataContractResolver dataContractResolver;

		// Token: 0x040004A2 RID: 1186
		private bool serializeReadOnlyTypes;
	}
}
