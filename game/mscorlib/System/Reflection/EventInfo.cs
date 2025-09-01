using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Mono;
using Unity;

namespace System.Reflection
{
	/// <summary>Discovers the attributes of an event and provides access to event metadata.</summary>
	// Token: 0x02000899 RID: 2201
	[Serializable]
	public abstract class EventInfo : MemberInfo, _EventInfo
	{
		/// <summary>Initializes a new instance of the <see langword="EventInfo" /> class.</summary>
		// Token: 0x0600489A RID: 18586 RVA: 0x00047204 File Offset: 0x00045404
		protected EventInfo()
		{
		}

		/// <summary>Gets a <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is an event.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is an event.</returns>
		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x0600489B RID: 18587 RVA: 0x00015831 File Offset: 0x00013A31
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Event;
			}
		}

		/// <summary>Gets the attributes for this event.</summary>
		/// <returns>The read-only attributes for this event.</returns>
		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x0600489C RID: 18588
		public abstract EventAttributes Attributes { get; }

		/// <summary>Gets a value indicating whether the <see langword="EventInfo" /> has a name with a special meaning.</summary>
		/// <returns>
		///   <see langword="true" /> if this event has a special name; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x0600489D RID: 18589 RVA: 0x000EE24E File Offset: 0x000EC44E
		public bool IsSpecialName
		{
			get
			{
				return (this.Attributes & EventAttributes.SpecialName) > EventAttributes.None;
			}
		}

		/// <summary>Returns the public methods that have been associated with an event in metadata using the <see langword=".other" /> directive.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.EventInfo" /> objects representing the public methods that have been associated with the event in metadata by using the <see langword=".other" /> directive. If there are no such public methods, an empty array is returned.</returns>
		// Token: 0x0600489E RID: 18590 RVA: 0x000EE25F File Offset: 0x000EC45F
		public MethodInfo[] GetOtherMethods()
		{
			return this.GetOtherMethods(false);
		}

		/// <summary>Returns the methods that have been associated with the event in metadata using the <see langword=".other" /> directive, specifying whether to include non-public methods.</summary>
		/// <param name="nonPublic">
		///   <see langword="true" /> to include non-public methods; otherwise, <see langword="false" />.</param>
		/// <returns>An array of <see cref="T:System.Reflection.EventInfo" /> objects representing methods that have been associated with an event in metadata by using the <see langword=".other" /> directive. If there are no methods matching the specification, an empty array is returned.</returns>
		/// <exception cref="T:System.NotImplementedException">This method is not implemented.</exception>
		// Token: 0x0600489F RID: 18591 RVA: 0x00047226 File Offset: 0x00045426
		public virtual MethodInfo[] GetOtherMethods(bool nonPublic)
		{
			throw NotImplemented.ByDesign;
		}

		/// <summary>Gets the <see cref="T:System.Reflection.MethodInfo" /> object for the <see cref="M:System.Reflection.EventInfo.AddEventHandler(System.Object,System.Delegate)" /> method of the event, including non-public methods.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodInfo" /> object for the <see cref="M:System.Reflection.EventInfo.AddEventHandler(System.Object,System.Delegate)" /> method.</returns>
		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x060048A0 RID: 18592 RVA: 0x000EE268 File Offset: 0x000EC468
		public virtual MethodInfo AddMethod
		{
			get
			{
				return this.GetAddMethod(true);
			}
		}

		/// <summary>Gets the <see langword="MethodInfo" /> object for removing a method of the event, including non-public methods.</summary>
		/// <returns>The <see langword="MethodInfo" /> object for removing a method of the event.</returns>
		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x060048A1 RID: 18593 RVA: 0x000EE271 File Offset: 0x000EC471
		public virtual MethodInfo RemoveMethod
		{
			get
			{
				return this.GetRemoveMethod(true);
			}
		}

		/// <summary>Gets the method that is called when the event is raised, including non-public methods.</summary>
		/// <returns>The method that is called when the event is raised.</returns>
		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x060048A2 RID: 18594 RVA: 0x000EE27A File Offset: 0x000EC47A
		public virtual MethodInfo RaiseMethod
		{
			get
			{
				return this.GetRaiseMethod(true);
			}
		}

		/// <summary>Returns the method used to add an event handler delegate to the event source.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the method used to add an event handler delegate to the event source.</returns>
		// Token: 0x060048A3 RID: 18595 RVA: 0x000EE283 File Offset: 0x000EC483
		public MethodInfo GetAddMethod()
		{
			return this.GetAddMethod(false);
		}

		/// <summary>Returns the method used to remove an event handler delegate from the event source.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the method used to remove an event handler delegate from the event source.</returns>
		// Token: 0x060048A4 RID: 18596 RVA: 0x000EE28C File Offset: 0x000EC48C
		public MethodInfo GetRemoveMethod()
		{
			return this.GetRemoveMethod(false);
		}

		/// <summary>Returns the method that is called when the event is raised.</summary>
		/// <returns>The method that is called when the event is raised.</returns>
		// Token: 0x060048A5 RID: 18597 RVA: 0x000EE295 File Offset: 0x000EC495
		public MethodInfo GetRaiseMethod()
		{
			return this.GetRaiseMethod(false);
		}

		/// <summary>When overridden in a derived class, retrieves the <see langword="MethodInfo" /> object for the <see cref="M:System.Reflection.EventInfo.AddEventHandler(System.Object,System.Delegate)" /> method of the event, specifying whether to return non-public methods.</summary>
		/// <param name="nonPublic">
		///   <see langword="true" /> if non-public methods can be returned; otherwise, <see langword="false" />.</param>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the method used to add an event handler delegate to the event source.</returns>
		/// <exception cref="T:System.MethodAccessException">
		///   <paramref name="nonPublic" /> is <see langword="true" />, the method used to add an event handler delegate is non-public, and the caller does not have permission to reflect on non-public methods.</exception>
		// Token: 0x060048A6 RID: 18598
		public abstract MethodInfo GetAddMethod(bool nonPublic);

		/// <summary>When overridden in a derived class, retrieves the <see langword="MethodInfo" /> object for removing a method of the event, specifying whether to return non-public methods.</summary>
		/// <param name="nonPublic">
		///   <see langword="true" /> if non-public methods can be returned; otherwise, <see langword="false" />.</param>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the method used to remove an event handler delegate from the event source.</returns>
		/// <exception cref="T:System.MethodAccessException">
		///   <paramref name="nonPublic" /> is <see langword="true" />, the method used to add an event handler delegate is non-public, and the caller does not have permission to reflect on non-public methods.</exception>
		// Token: 0x060048A7 RID: 18599
		public abstract MethodInfo GetRemoveMethod(bool nonPublic);

		/// <summary>When overridden in a derived class, returns the method that is called when the event is raised, specifying whether to return non-public methods.</summary>
		/// <param name="nonPublic">
		///   <see langword="true" /> if non-public methods can be returned; otherwise, <see langword="false" />.</param>
		/// <returns>A <see langword="MethodInfo" /> object that was called when the event was raised.</returns>
		/// <exception cref="T:System.MethodAccessException">
		///   <paramref name="nonPublic" /> is <see langword="true" />, the method used to add an event handler delegate is non-public, and the caller does not have permission to reflect on non-public methods.</exception>
		// Token: 0x060048A8 RID: 18600
		public abstract MethodInfo GetRaiseMethod(bool nonPublic);

		/// <summary>Gets a value indicating whether the event is multicast.</summary>
		/// <returns>
		///   <see langword="true" /> if the delegate is an instance of a multicast delegate; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x060048A9 RID: 18601 RVA: 0x000EE2A0 File Offset: 0x000EC4A0
		public virtual bool IsMulticast
		{
			get
			{
				Type eventHandlerType = this.EventHandlerType;
				return typeof(MulticastDelegate).IsAssignableFrom(eventHandlerType);
			}
		}

		/// <summary>Gets the <see langword="Type" /> object of the underlying event-handler delegate associated with this event.</summary>
		/// <returns>A read-only <see langword="Type" /> object representing the delegate event handler.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x060048AA RID: 18602 RVA: 0x000EE2C4 File Offset: 0x000EC4C4
		public virtual Type EventHandlerType
		{
			get
			{
				ParameterInfo[] parametersInternal = this.GetAddMethod(true).GetParametersInternal();
				Type typeFromHandle = typeof(Delegate);
				for (int i = 0; i < parametersInternal.Length; i++)
				{
					Type parameterType = parametersInternal[i].ParameterType;
					if (parameterType.IsSubclassOf(typeFromHandle))
					{
						return parameterType;
					}
				}
				return null;
			}
		}

		/// <summary>Removes an event handler from an event source.</summary>
		/// <param name="target">The event source.</param>
		/// <param name="handler">The delegate to be disassociated from the events raised by target.</param>
		/// <exception cref="T:System.InvalidOperationException">The event does not have a public <see langword="remove" /> accessor.</exception>
		/// <exception cref="T:System.ArgumentException">The handler that was passed in cannot be used.</exception>
		/// <exception cref="T:System.Reflection.TargetException">In .NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.  
		///
		///
		///
		///
		///  The <paramref name="target" /> parameter is <see langword="null" /> and the event is not static.  
		///  -or-  
		///  The <see cref="T:System.Reflection.EventInfo" /> is not declared on the target.</exception>
		/// <exception cref="T:System.MethodAccessException">In .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.  
		///
		///
		///
		///
		///  The caller does not have access permission to the member.</exception>
		// Token: 0x060048AB RID: 18603 RVA: 0x000EE30C File Offset: 0x000EC50C
		[DebuggerStepThrough]
		[DebuggerHidden]
		public virtual void RemoveEventHandler(object target, Delegate handler)
		{
			MethodInfo removeMethod = this.GetRemoveMethod(false);
			if (removeMethod == null)
			{
				throw new InvalidOperationException("Cannot remove the event handler since no public remove method exists for the event.");
			}
			if (removeMethod.GetParametersNoCopy()[0].ParameterType == typeof(EventRegistrationToken))
			{
				throw new InvalidOperationException("Adding or removing event handlers dynamically is not supported on WinRT events.");
			}
			removeMethod.Invoke(target, new object[]
			{
				handler
			});
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An object to compare with this instance, or <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> equals the type and value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x060048AC RID: 18604 RVA: 0x000EE36E File Offset: 0x000EC56E
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060048AD RID: 18605 RVA: 0x000EE377 File Offset: 0x000EC577
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.EventInfo" /> objects are equal.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060048AE RID: 18606 RVA: 0x00064560 File Offset: 0x00062760
		public static bool operator ==(EventInfo left, EventInfo right)
		{
			return left == right || (left != null && right != null && left.Equals(right));
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.EventInfo" /> objects are not equal.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060048AF RID: 18607 RVA: 0x000EE37F File Offset: 0x000EC57F
		public static bool operator !=(EventInfo left, EventInfo right)
		{
			return !(left == right);
		}

		/// <summary>Adds an event handler to an event source.</summary>
		/// <param name="target">The event source.</param>
		/// <param name="handler">Encapsulates a method or methods to be invoked when the event is raised by the target.</param>
		/// <exception cref="T:System.InvalidOperationException">The event does not have a public <see langword="add" /> accessor.</exception>
		/// <exception cref="T:System.ArgumentException">The handler that was passed in cannot be used.</exception>
		/// <exception cref="T:System.MethodAccessException">In .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.  
		///
		///
		///
		///
		///  The caller does not have access permission to the member.</exception>
		/// <exception cref="T:System.Reflection.TargetException">In .NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.  
		///
		///
		///
		///
		///  The <paramref name="target" /> parameter is <see langword="null" /> and the event is not static.  
		///  -or-  
		///  The <see cref="T:System.Reflection.EventInfo" /> is not declared on the target.</exception>
		// Token: 0x060048B0 RID: 18608 RVA: 0x000EE38C File Offset: 0x000EC58C
		[DebuggerStepThrough]
		[DebuggerHidden]
		public virtual void AddEventHandler(object target, Delegate handler)
		{
			if (this.cached_add_event == null)
			{
				MethodInfo addMethod = this.GetAddMethod();
				if (addMethod == null)
				{
					throw new InvalidOperationException("Cannot add the event handler since no public add method exists for the event.");
				}
				if (addMethod.DeclaringType.IsValueType)
				{
					if (target == null && !addMethod.IsStatic)
					{
						throw new TargetException("Cannot add a handler to a non static event with a null target");
					}
					addMethod.Invoke(target, new object[]
					{
						handler
					});
					return;
				}
				else
				{
					this.cached_add_event = EventInfo.CreateAddEventDelegate(addMethod);
				}
			}
			this.cached_add_event(target, handler);
		}

		// Token: 0x060048B1 RID: 18609 RVA: 0x000EE40C File Offset: 0x000EC60C
		private static void AddEventFrame<T, D>(EventInfo.AddEvent<T, D> addEvent, object obj, object dele)
		{
			if (obj == null)
			{
				throw new TargetException("Cannot add a handler to a non static event with a null target");
			}
			if (!(obj is T))
			{
				throw new TargetException("Object doesn't match target");
			}
			if (!(dele is D))
			{
				throw new ArgumentException(string.Format("Object of type {0} cannot be converted to type {1}.", dele.GetType(), typeof(D)));
			}
			addEvent((T)((object)obj), (D)((object)dele));
		}

		// Token: 0x060048B2 RID: 18610 RVA: 0x000EE474 File Offset: 0x000EC674
		private static void StaticAddEventAdapterFrame<D>(EventInfo.StaticAddEvent<D> addEvent, object obj, object dele)
		{
			addEvent((D)((object)dele));
		}

		// Token: 0x060048B3 RID: 18611 RVA: 0x000EE484 File Offset: 0x000EC684
		private static EventInfo.AddEventAdapter CreateAddEventDelegate(MethodInfo method)
		{
			Type[] typeArguments;
			Type typeFromHandle;
			string name;
			if (method.IsStatic)
			{
				typeArguments = new Type[]
				{
					method.GetParametersInternal()[0].ParameterType
				};
				typeFromHandle = typeof(EventInfo.StaticAddEvent<>);
				name = "StaticAddEventAdapterFrame";
			}
			else
			{
				typeArguments = new Type[]
				{
					method.DeclaringType,
					method.GetParametersInternal()[0].ParameterType
				};
				typeFromHandle = typeof(EventInfo.AddEvent<, >);
				name = "AddEventFrame";
			}
			object firstArgument = Delegate.CreateDelegate(typeFromHandle.MakeGenericType(typeArguments), method);
			MethodInfo methodInfo = typeof(EventInfo).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic);
			methodInfo = methodInfo.MakeGenericMethod(typeArguments);
			return (EventInfo.AddEventAdapter)Delegate.CreateDelegate(typeof(EventInfo.AddEventAdapter), firstArgument, methodInfo, true);
		}

		// Token: 0x060048B4 RID: 18612
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern EventInfo internal_from_handle_type(IntPtr event_handle, IntPtr type_handle);

		// Token: 0x060048B5 RID: 18613 RVA: 0x000EE538 File Offset: 0x000EC738
		internal static EventInfo GetEventFromHandle(RuntimeEventHandle handle, RuntimeTypeHandle reflectedType)
		{
			if (handle.Value == IntPtr.Zero)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			EventInfo eventInfo = EventInfo.internal_from_handle_type(handle.Value, reflectedType.Value);
			if (eventInfo == null)
			{
				throw new ArgumentException("The event handle and the type handle are incompatible.");
			}
			return eventInfo;
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060048B6 RID: 18614 RVA: 0x000173AD File Offset: 0x000155AD
		void _EventInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns a T:System.Type object representing the <see cref="T:System.Reflection.EventInfo" /> type.</summary>
		/// <returns>A T:System.Type object representing the <see cref="T:System.Reflection.EventInfo" /> type.</returns>
		// Token: 0x060048B7 RID: 18615 RVA: 0x00052955 File Offset: 0x00050B55
		Type _EventInfo.GetType()
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060048B8 RID: 18616 RVA: 0x000173AD File Offset: 0x000155AD
		void _EventInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060048B9 RID: 18617 RVA: 0x000173AD File Offset: 0x000155AD
		void _EventInfo.GetTypeInfoCount(out uint pcTInfo)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060048BA RID: 18618 RVA: 0x000173AD File Offset: 0x000155AD
		void _EventInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002E8B RID: 11915
		private EventInfo.AddEventAdapter cached_add_event;

		// Token: 0x0200089A RID: 2202
		// (Invoke) Token: 0x060048BC RID: 18620
		private delegate void AddEventAdapter(object _this, Delegate dele);

		// Token: 0x0200089B RID: 2203
		// (Invoke) Token: 0x060048C0 RID: 18624
		private delegate void AddEvent<T, D>(T _this, D dele);

		// Token: 0x0200089C RID: 2204
		// (Invoke) Token: 0x060048C4 RID: 18628
		private delegate void StaticAddEvent<D>(D dele);
	}
}
