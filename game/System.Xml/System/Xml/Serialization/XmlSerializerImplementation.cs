using System;
using System.Collections;

namespace System.Xml.Serialization
{
	/// <summary>Defines the reader, writer, and methods for pre-generated, typed serializers.</summary>
	// Token: 0x02000301 RID: 769
	public abstract class XmlSerializerImplementation
	{
		/// <summary>Gets the XML reader object that is used by the serializer.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlSerializationReader" /> that is used to read an XML document or data stream.</returns>
		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001FE2 RID: 8162 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public virtual XmlSerializationReader Reader
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets the XML writer object for the serializer.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlSerializationWriter" /> that is used to write to an XML data stream or document.</returns>
		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001FE3 RID: 8163 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public virtual XmlSerializationWriter Writer
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets the collection of methods that is used to read a data stream.</summary>
		/// <returns>A <see cref="T:System.Collections.Hashtable" /> that contains the methods.</returns>
		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001FE4 RID: 8164 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public virtual Hashtable ReadMethods
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Get the collection of methods that is used to write to a data stream.</summary>
		/// <returns>A <see cref="T:System.Collections.Hashtable" /> that contains the methods.</returns>
		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001FE5 RID: 8165 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public virtual Hashtable WriteMethods
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets the collection of typed serializers that is found in the assembly.</summary>
		/// <returns>A <see cref="T:System.Collections.Hashtable" /> that contains the typed serializers.</returns>
		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001FE6 RID: 8166 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public virtual Hashtable TypedSerializers
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets a value that determines whether a type can be serialized.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> to be serialized.</param>
		/// <returns>
		///     <see langword="true" /> if the type can be serialized; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001FE7 RID: 8167 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public virtual bool CanSerialize(Type type)
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns a serializer for the specified type.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> to be serialized.</param>
		/// <returns>An instance of a type derived from the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class. </returns>
		// Token: 0x06001FE8 RID: 8168 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public virtual XmlSerializer GetSerializer(Type type)
		{
			throw new NotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializerImplementation" /> class. </summary>
		// Token: 0x06001FE9 RID: 8169 RVA: 0x0000216B File Offset: 0x0000036B
		protected XmlSerializerImplementation()
		{
		}
	}
}
