using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	/// <summary>Permits late-bound registration of an event handler.</summary>
	// Token: 0x020002D4 RID: 724
	public class ComAwareEventInfo : EventInfo
	{
		/// <summary>Gets the attributes for this event.</summary>
		/// <returns>The read-only attributes for this event.</returns>
		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x000023CA File Offset: 0x000005CA
		[MonoTODO]
		public override EventAttributes Attributes
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the class that declares this member.</summary>
		/// <returns>The <see cref="T:System.Type" /> object for the class that declares this member.</returns>
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x0600160E RID: 5646 RVA: 0x000023CA File Offset: 0x000005CA
		[MonoTODO]
		public override Type DeclaringType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the name of the current member.</summary>
		/// <returns>The name of this member.</returns>
		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x000023CA File Offset: 0x000005CA
		[MonoTODO]
		public override string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComAwareEventInfo" /> class by using the specified type and a name of the event on the type.</summary>
		/// <param name="type">The type of object.</param>
		/// <param name="eventName">The name of an event on <paramref name="type" />.</param>
		// Token: 0x06001610 RID: 5648 RVA: 0x0004A92D File Offset: 0x00048B2D
		[MonoTODO]
		public ComAwareEventInfo(Type type, string eventName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Attaches an event handler to a COM object.</summary>
		/// <param name="target">The target object that the event delegate should bind to.</param>
		/// <param name="handler">The event delegate.</param>
		// Token: 0x06001611 RID: 5649 RVA: 0x000023CA File Offset: 0x000005CA
		[MonoTODO]
		public override void AddEventHandler(object target, Delegate handler)
		{
			throw new NotImplementedException();
		}

		/// <summary>Detaches an event handler from a COM object.</summary>
		/// <param name="target">The target object that the event delegate is bound to.</param>
		/// <param name="handler">The event delegate.</param>
		/// <exception cref="T:System.InvalidOperationException">The event does not have a public <see langword="remove" /> accessor.</exception>
		/// <exception cref="T:System.ArgumentException">The handler that was passed in cannot be used.</exception>
		/// <exception cref="T:System.Reflection.TargetException">
		///           In the .NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.The <paramref name="target" /> parameter is <see langword="null" /> and the event is not static.-or- The <see cref="T:System.Reflection.EventInfo" /> is not declared on the target.</exception>
		/// <exception cref="T:System.MethodAccessException">
		///           In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.The caller does not have access permission to the member.</exception>
		// Token: 0x06001612 RID: 5650 RVA: 0x000023CA File Offset: 0x000005CA
		[MonoTODO]
		public override void RemoveEventHandler(object target, Delegate handler)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the method that was used to add an event handler delegate to the event source.</summary>
		/// <param name="nonPublic">
		///       <see langword="true" /> to return non-public methods; otherwise, <see langword="false" />.</param>
		/// <returns>The method that was used to add an event handler delegate to the event source.</returns>
		/// <exception cref="T:System.MethodAccessException">
		///         <paramref name="nonPublic" /> is <see langword="true" /> and the method used to add an event handler delegate is non-public, but the caller does not have permission to reflect on non-public methods.</exception>
		// Token: 0x06001613 RID: 5651 RVA: 0x000023CA File Offset: 0x000005CA
		[MonoTODO]
		public override MethodInfo GetAddMethod(bool nonPublic)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, returns the method that was called when the event was raised.</summary>
		/// <param name="nonPublic">
		///       <see langword="true" /> to return non-public methods; otherwise, <see langword="false" />. </param>
		/// <returns>The object that was called when the event was raised.</returns>
		/// <exception cref="T:System.MethodAccessException">
		///         <paramref name="nonPublic" /> is <see langword="true" /> and the method used to add an event handler delegate is non-public, but the caller does not have permission to reflect on non-public methods. </exception>
		// Token: 0x06001614 RID: 5652 RVA: 0x000023CA File Offset: 0x000005CA
		[MonoTODO]
		public override MethodInfo GetRaiseMethod(bool nonPublic)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, retrieves the <see cref="T:System.Reflection.MethodInfo" /> object for removing a method of the event.</summary>
		/// <param name="nonPublic">
		///       <see langword="true" /> to return non-public methods; otherwise, <see langword="false" />. </param>
		/// <returns>The method that was used to remove an event handler delegate from the event source.</returns>
		/// <exception cref="T:System.MethodAccessException">
		///         <paramref name="nonPublic" /> is <see langword="true" /> and the method used to add an event handler delegate is non-public, but the caller does not have permission to reflect on non-public methods. </exception>
		// Token: 0x06001615 RID: 5653 RVA: 0x000023CA File Offset: 0x000005CA
		[MonoTODO]
		public override MethodInfo GetRemoveMethod(bool nonPublic)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, gets an array that contains all the custom attributes of the specified type that are applied to this member.</summary>
		/// <param name="attributeType">The attribute type to search for. Only attributes that are assignable to this type can be returned.</param>
		/// <param name="inherit">
		///       <see langword="true" /> to search this member's inheritance chain to find the attributes; otherwise, <see langword="false" />.</param>
		/// <returns>An array that contains all the custom attributes of the specified type, or an array that has no elements if no attributes were defined.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="attributeType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This member belongs to a type that is loaded into the reflection-only context. See How to: Load Assemblies into the Reflection-Only Context</exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded.</exception>
		// Token: 0x06001616 RID: 5654 RVA: 0x000023CA File Offset: 0x000005CA
		[MonoTODO]
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, gets an array that contains all the custom attributes that are applied to this member.</summary>
		/// <param name="inherit">
		///       <see langword="true" /> to search this member's inheritance chain to find the attributes; otherwise, <see langword="false" />.</param>
		/// <returns>An array that contains all the custom attributes, or an array that has no elements if no attributes were defined.</returns>
		/// <exception cref="T:System.InvalidOperationException">This member belongs to a type that is loaded into the reflection-only context. See How to: Load Assemblies into the Reflection-Only Context.</exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded.</exception>
		// Token: 0x06001617 RID: 5655 RVA: 0x000023CA File Offset: 0x000005CA
		[MonoTODO]
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates whether one or more instances of the specified attribute are applied to this member.</summary>
		/// <param name="attributeType">The attribute type to search for.</param>
		/// <param name="inherit">
		///       <see langword="true" /> to search this member's inheritance chain to find the attributes; otherwise, <see langword="false" />.</param>
		/// <returns>
		///     <see langword="true" /> if the specified attribute has been applied to this member; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001618 RID: 5656 RVA: 0x000023CA File Offset: 0x000005CA
		[MonoTODO]
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the class object that was used to initialize this instance.</summary>
		/// <returns>The <see cref="T:System.Type" /> object that was used to initialize the current object.</returns>
		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001619 RID: 5657 RVA: 0x000023CA File Offset: 0x000005CA
		[MonoTODO]
		public override Type ReflectedType
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
