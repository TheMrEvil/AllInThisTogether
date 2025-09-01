using System;
using System.Collections;
using System.IO;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides the base class for serializing a set of components or serializable objects into a serialization store.</summary>
	// Token: 0x02000480 RID: 1152
	public abstract class ComponentSerializationService
	{
		/// <summary>Creates a new <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" />.</summary>
		/// <returns>A new created serialization store.</returns>
		// Token: 0x060024F4 RID: 9460
		public abstract SerializationStore CreateStore();

		/// <summary>Loads a <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> from a stream.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> from which the store will be loaded.</param>
		/// <returns>A new <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="stream" /> does not contain data saved by a previous call to <see cref="M:System.ComponentModel.Design.Serialization.SerializationStore.Save(System.IO.Stream)" />.</exception>
		// Token: 0x060024F5 RID: 9461
		public abstract SerializationStore LoadStore(Stream stream);

		/// <summary>Serializes the given object to the given <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" />.</summary>
		/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to which the state of <paramref name="value" /> will be written.</param>
		/// <param name="value">The object to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="store" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="store" /> is closed, or <paramref name="store" /> is not a supported type of serialization store. Use a store returned by <see cref="M:System.ComponentModel.Design.Serialization.CodeDomComponentSerializationService.CreateStore" />.</exception>
		// Token: 0x060024F6 RID: 9462
		public abstract void Serialize(SerializationStore store, object value);

		/// <summary>Serializes the given object, accounting for default property values.</summary>
		/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to which the state of <paramref name="value" /> will be serialized.</param>
		/// <param name="value">The object to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="store" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="store" /> is closed, or <paramref name="store" /> is not a supported type of serialization store. Use a store returned by <see cref="M:System.ComponentModel.Design.Serialization.CodeDomComponentSerializationService.CreateStore" />.</exception>
		// Token: 0x060024F7 RID: 9463
		public abstract void SerializeAbsolute(SerializationStore store, object value);

		/// <summary>Serializes the given member on the given object.</summary>
		/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to which the state of <paramref name="member" /> will be serialized.</param>
		/// <param name="owningObject">The object to which <paramref name="member" /> is attached.</param>
		/// <param name="member">A <see cref="T:System.ComponentModel.MemberDescriptor" /> specifying the member to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="store" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="store" /> is closed, or <paramref name="store" /> is not a supported type of serialization store. Use a store returned by <see cref="M:System.ComponentModel.Design.Serialization.CodeDomComponentSerializationService.CreateStore" />.</exception>
		// Token: 0x060024F8 RID: 9464
		public abstract void SerializeMember(SerializationStore store, object owningObject, MemberDescriptor member);

		/// <summary>Serializes the given member on the given object, accounting for the default property value.</summary>
		/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to which the state of <paramref name="member" /> will be serialized.</param>
		/// <param name="owningObject">The object to which <paramref name="member" /> is attached.</param>
		/// <param name="member">The member to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="store" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="store" /> is closed, or <paramref name="store" /> is not a supported type of serialization store. Use a store returned by <see cref="M:System.ComponentModel.Design.Serialization.CodeDomComponentSerializationService.CreateStore" />.</exception>
		// Token: 0x060024F9 RID: 9465
		public abstract void SerializeMemberAbsolute(SerializationStore store, object owningObject, MemberDescriptor member);

		/// <summary>Deserializes the given store to produce a collection of objects.</summary>
		/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to deserialize.</param>
		/// <returns>A collection of objects created according to the stored state.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="store" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="store" /> does not contain data in a format the serialization container can process.</exception>
		// Token: 0x060024FA RID: 9466
		public abstract ICollection Deserialize(SerializationStore store);

		/// <summary>Deserializes the given store and populates the given <see cref="T:System.ComponentModel.IContainer" /> with deserialized <see cref="T:System.ComponentModel.IComponent" /> objects.</summary>
		/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to deserialize.</param>
		/// <param name="container">The <see cref="T:System.ComponentModel.IContainer" /> to which <see cref="T:System.ComponentModel.IComponent" /> objects will be added.</param>
		/// <returns>A collection of objects created according to the stored state.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="store" /> or <paramref name="container" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="store" /> does not contain data in a format the serialization container can process.</exception>
		// Token: 0x060024FB RID: 9467
		public abstract ICollection Deserialize(SerializationStore store, IContainer container);

		/// <summary>Deserializes the given <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to the given container, optionally applying default property values.</summary>
		/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to deserialize.</param>
		/// <param name="container">The container to which <see cref="T:System.ComponentModel.IComponent" /> objects will be added.</param>
		/// <param name="validateRecycledTypes">
		///   <see langword="true" /> to guarantee that the deserialization will only work if applied to an object of the same type.</param>
		/// <param name="applyDefaults">
		///   <see langword="true" /> to indicate that the default property values should be applied.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="store" /> or <paramref name="container" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="store" /> does not contain data in a format the serialization container can process.</exception>
		// Token: 0x060024FC RID: 9468
		public abstract void DeserializeTo(SerializationStore store, IContainer container, bool validateRecycledTypes, bool applyDefaults);

		/// <summary>Deserializes the given <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to the given container.</summary>
		/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to deserialize.</param>
		/// <param name="container">The container to which <see cref="T:System.ComponentModel.IComponent" /> objects will be added.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="store" /> or <paramref name="container" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="store" /> does not contain data in a format the serialization container can process.</exception>
		// Token: 0x060024FD RID: 9469 RVA: 0x000829E6 File Offset: 0x00080BE6
		public void DeserializeTo(SerializationStore store, IContainer container)
		{
			this.DeserializeTo(store, container, true, true);
		}

		/// <summary>Deserializes the given <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to the given container, optionally validating recycled types.</summary>
		/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to deserialize.</param>
		/// <param name="container">The container to which <see cref="T:System.ComponentModel.IComponent" /> objects will be added.</param>
		/// <param name="validateRecycledTypes">
		///   <see langword="true" /> to guarantee that the deserialization will only work if applied to an object of the same type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="store" /> or <paramref name="container" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="store" /> does not contain data in a format the serialization container can process.</exception>
		// Token: 0x060024FE RID: 9470 RVA: 0x000829F2 File Offset: 0x00080BF2
		public void DeserializeTo(SerializationStore store, IContainer container, bool validateRecycledTypes)
		{
			this.DeserializeTo(store, container, validateRecycledTypes, true);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.ComponentSerializationService" /> class.</summary>
		// Token: 0x060024FF RID: 9471 RVA: 0x0000219B File Offset: 0x0000039B
		protected ComponentSerializationService()
		{
		}
	}
}
