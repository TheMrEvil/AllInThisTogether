using System;
using System.Collections;

namespace System.ComponentModel
{
	/// <summary>Provides supplemental metadata to the <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
	// Token: 0x020003F3 RID: 1011
	public abstract class TypeDescriptionProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> class.</summary>
		// Token: 0x06002102 RID: 8450 RVA: 0x0000219B File Offset: 0x0000039B
		protected TypeDescriptionProvider()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> class using a parent type description provider.</summary>
		/// <param name="parent">The parent type description provider.</param>
		// Token: 0x06002103 RID: 8451 RVA: 0x00071C33 File Offset: 0x0006FE33
		protected TypeDescriptionProvider(TypeDescriptionProvider parent)
		{
			this._parent = parent;
		}

		/// <summary>Creates an object that can substitute for another data type.</summary>
		/// <param name="provider">An optional service provider.</param>
		/// <param name="objectType">The type of object to create. This parameter is never <see langword="null" />.</param>
		/// <param name="argTypes">An optional array of types that represent the parameter types to be passed to the object's constructor. This array can be <see langword="null" /> or of zero length.</param>
		/// <param name="args">An optional array of parameter values to pass to the object's constructor.</param>
		/// <returns>The substitute <see cref="T:System.Object" />.</returns>
		// Token: 0x06002104 RID: 8452 RVA: 0x00071C42 File Offset: 0x0006FE42
		public virtual object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
		{
			if (this._parent != null)
			{
				return this._parent.CreateInstance(provider, objectType, argTypes, args);
			}
			if (objectType == null)
			{
				throw new ArgumentNullException("objectType");
			}
			return Activator.CreateInstance(objectType, args);
		}

		/// <summary>Gets a per-object cache, accessed as an <see cref="T:System.Collections.IDictionary" /> of key/value pairs.</summary>
		/// <param name="instance">The object for which to get the cache.</param>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> if the provided object supports caching; otherwise, <see langword="null" />.</returns>
		// Token: 0x06002105 RID: 8453 RVA: 0x00071C79 File Offset: 0x0006FE79
		public virtual IDictionary GetCache(object instance)
		{
			TypeDescriptionProvider parent = this._parent;
			if (parent == null)
			{
				return null;
			}
			return parent.GetCache(instance);
		}

		/// <summary>Gets an extended custom type descriptor for the given object.</summary>
		/// <param name="instance">The object for which to get the extended type descriptor.</param>
		/// <returns>An <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> that can provide extended metadata for the object.</returns>
		// Token: 0x06002106 RID: 8454 RVA: 0x00071C90 File Offset: 0x0006FE90
		public virtual ICustomTypeDescriptor GetExtendedTypeDescriptor(object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetExtendedTypeDescriptor(instance);
			}
			TypeDescriptionProvider.EmptyCustomTypeDescriptor result;
			if ((result = this._emptyDescriptor) == null)
			{
				result = (this._emptyDescriptor = new TypeDescriptionProvider.EmptyCustomTypeDescriptor());
			}
			return result;
		}

		/// <summary>Gets the extender providers for the specified object.</summary>
		/// <param name="instance">The object to get extender providers for.</param>
		/// <returns>An array of extender providers for <paramref name="instance" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is <see langword="null" />.</exception>
		// Token: 0x06002107 RID: 8455 RVA: 0x00071CCA File Offset: 0x0006FECA
		protected internal virtual IExtenderProvider[] GetExtenderProviders(object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetExtenderProviders(instance);
			}
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return Array.Empty<IExtenderProvider>();
		}

		/// <summary>Gets the name of the specified component, or <see langword="null" /> if the component has no name.</summary>
		/// <param name="component">The specified component.</param>
		/// <returns>The name of the specified component.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is <see langword="null" />.</exception>
		// Token: 0x06002108 RID: 8456 RVA: 0x00071CF4 File Offset: 0x0006FEF4
		public virtual string GetFullComponentName(object component)
		{
			if (this._parent != null)
			{
				return this._parent.GetFullComponentName(component);
			}
			return this.GetTypeDescriptor(component).GetComponentName();
		}

		/// <summary>Performs normal reflection against a type.</summary>
		/// <param name="objectType">The type of object for which to retrieve the <see cref="T:System.Reflection.IReflect" />.</param>
		/// <returns>The type of reflection for this <paramref name="objectType" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="objectType" /> is <see langword="null" />.</exception>
		// Token: 0x06002109 RID: 8457 RVA: 0x00071D17 File Offset: 0x0006FF17
		public Type GetReflectionType(Type objectType)
		{
			return this.GetReflectionType(objectType, null);
		}

		/// <summary>Performs normal reflection against the given object.</summary>
		/// <param name="instance">An instance of the type (should not be <see langword="null" />).</param>
		/// <returns>The type of reflection for this <paramref name="instance" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is <see langword="null" />.</exception>
		// Token: 0x0600210A RID: 8458 RVA: 0x00071D21 File Offset: 0x0006FF21
		public Type GetReflectionType(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return this.GetReflectionType(instance.GetType(), instance);
		}

		/// <summary>Performs normal reflection against the given object with the given type.</summary>
		/// <param name="objectType">The type of object for which to retrieve the <see cref="T:System.Reflection.IReflect" />.</param>
		/// <param name="instance">An instance of the type. Can be <see langword="null" />.</param>
		/// <returns>The type of reflection for this <paramref name="objectType" />.</returns>
		// Token: 0x0600210B RID: 8459 RVA: 0x00071D3E File Offset: 0x0006FF3E
		public virtual Type GetReflectionType(Type objectType, object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetReflectionType(objectType, instance);
			}
			return objectType;
		}

		/// <summary>Converts a reflection type into a runtime type.</summary>
		/// <param name="reflectionType">The type to convert to its runtime equivalent.</param>
		/// <returns>A <see cref="T:System.Type" /> that represents the runtime equivalent of <paramref name="reflectionType" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reflectionType" /> is <see langword="null" />.</exception>
		// Token: 0x0600210C RID: 8460 RVA: 0x00071D58 File Offset: 0x0006FF58
		public virtual Type GetRuntimeType(Type reflectionType)
		{
			if (this._parent != null)
			{
				return this._parent.GetRuntimeType(reflectionType);
			}
			if (reflectionType == null)
			{
				throw new ArgumentNullException("reflectionType");
			}
			if (reflectionType.GetType().Assembly == typeof(object).Assembly)
			{
				return reflectionType;
			}
			return reflectionType.UnderlyingSystemType;
		}

		/// <summary>Gets a custom type descriptor for the given type.</summary>
		/// <param name="objectType">The type of object for which to retrieve the type descriptor.</param>
		/// <returns>An <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> that can provide metadata for the type.</returns>
		// Token: 0x0600210D RID: 8461 RVA: 0x00071DB7 File Offset: 0x0006FFB7
		public ICustomTypeDescriptor GetTypeDescriptor(Type objectType)
		{
			return this.GetTypeDescriptor(objectType, null);
		}

		/// <summary>Gets a custom type descriptor for the given object.</summary>
		/// <param name="instance">An instance of the type. Can be <see langword="null" /> if no instance was passed to the <see cref="T:System.ComponentModel.TypeDescriptor" />.</param>
		/// <returns>An <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> that can provide metadata for the type.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is <see langword="null" />.</exception>
		// Token: 0x0600210E RID: 8462 RVA: 0x00071DC1 File Offset: 0x0006FFC1
		public ICustomTypeDescriptor GetTypeDescriptor(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return this.GetTypeDescriptor(instance.GetType(), instance);
		}

		/// <summary>Gets a custom type descriptor for the given type and object.</summary>
		/// <param name="objectType">The type of object for which to retrieve the type descriptor.</param>
		/// <param name="instance">An instance of the type. Can be <see langword="null" /> if no instance was passed to the <see cref="T:System.ComponentModel.TypeDescriptor" />.</param>
		/// <returns>An <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> that can provide metadata for the type.</returns>
		// Token: 0x0600210F RID: 8463 RVA: 0x00071DE0 File Offset: 0x0006FFE0
		public virtual ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetTypeDescriptor(objectType, instance);
			}
			TypeDescriptionProvider.EmptyCustomTypeDescriptor result;
			if ((result = this._emptyDescriptor) == null)
			{
				result = (this._emptyDescriptor = new TypeDescriptionProvider.EmptyCustomTypeDescriptor());
			}
			return result;
		}

		/// <summary>Gets a value that indicates whether the specified type is compatible with the type description and its chain of type description providers.</summary>
		/// <param name="type">The type to test for compatibility.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="type" /> is compatible with the type description and its chain of type description providers; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		// Token: 0x06002110 RID: 8464 RVA: 0x00071E1B File Offset: 0x0007001B
		public virtual bool IsSupportedType(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return this._parent == null || this._parent.IsSupportedType(type);
		}

		// Token: 0x04000FF3 RID: 4083
		private readonly TypeDescriptionProvider _parent;

		// Token: 0x04000FF4 RID: 4084
		private TypeDescriptionProvider.EmptyCustomTypeDescriptor _emptyDescriptor;

		// Token: 0x020003F4 RID: 1012
		private sealed class EmptyCustomTypeDescriptor : CustomTypeDescriptor
		{
			// Token: 0x06002111 RID: 8465 RVA: 0x00071E47 File Offset: 0x00070047
			public EmptyCustomTypeDescriptor()
			{
			}
		}
	}
}
