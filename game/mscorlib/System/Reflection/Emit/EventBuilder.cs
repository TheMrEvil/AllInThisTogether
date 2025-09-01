using System;
using System.Runtime.InteropServices;
using Unity;

namespace System.Reflection.Emit
{
	/// <summary>Defines events for a class.</summary>
	// Token: 0x02000921 RID: 2337
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_EventBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class EventBuilder : _EventBuilder
	{
		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004FEB RID: 20459 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _EventBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004FEC RID: 20460 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _EventBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004FED RID: 20461 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _EventBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
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
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004FEE RID: 20462 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _EventBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004FEF RID: 20463 RVA: 0x000FA626 File Offset: 0x000F8826
		internal EventBuilder(TypeBuilder tb, string eventName, EventAttributes eventAttrs, Type eventType)
		{
			this.name = eventName;
			this.attrs = eventAttrs;
			this.type = eventType;
			this.typeb = tb;
			this.table_idx = this.get_next_table_index(this, 20, 1);
		}

		// Token: 0x06004FF0 RID: 20464 RVA: 0x000FA65B File Offset: 0x000F885B
		internal int get_next_table_index(object obj, int table, int count)
		{
			return this.typeb.get_next_table_index(obj, table, count);
		}

		/// <summary>Adds one of the "other" methods associated with this event. "Other" methods are methods other than the "on" and "raise" methods associated with an event. This function can be called many times to add as many "other" methods.</summary>
		/// <param name="mdBuilder">A <see langword="MethodBuilder" /> object that represents the other method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mdBuilder" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type.</exception>
		// Token: 0x06004FF1 RID: 20465 RVA: 0x000FA66C File Offset: 0x000F886C
		public void AddOtherMethod(MethodBuilder mdBuilder)
		{
			if (mdBuilder == null)
			{
				throw new ArgumentNullException("mdBuilder");
			}
			this.RejectIfCreated();
			if (this.other_methods != null)
			{
				MethodBuilder[] array = new MethodBuilder[this.other_methods.Length + 1];
				this.other_methods.CopyTo(array, 0);
				this.other_methods = array;
			}
			else
			{
				this.other_methods = new MethodBuilder[1];
			}
			this.other_methods[this.other_methods.Length - 1] = mdBuilder;
		}

		/// <summary>Returns the token for this event.</summary>
		/// <returns>The <see langword="EventToken" /> for this event.</returns>
		// Token: 0x06004FF2 RID: 20466 RVA: 0x000FA6DF File Offset: 0x000F88DF
		public EventToken GetEventToken()
		{
			return new EventToken(335544320 | this.table_idx);
		}

		/// <summary>Sets the method used to subscribe to this event.</summary>
		/// <param name="mdBuilder">A <see langword="MethodBuilder" /> object that represents the method used to subscribe to this event.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mdBuilder" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type.</exception>
		// Token: 0x06004FF3 RID: 20467 RVA: 0x000FA6F2 File Offset: 0x000F88F2
		public void SetAddOnMethod(MethodBuilder mdBuilder)
		{
			if (mdBuilder == null)
			{
				throw new ArgumentNullException("mdBuilder");
			}
			this.RejectIfCreated();
			this.add_method = mdBuilder;
		}

		/// <summary>Sets the method used to raise this event.</summary>
		/// <param name="mdBuilder">A <see langword="MethodBuilder" /> object that represents the method used to raise this event.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mdBuilder" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type.</exception>
		// Token: 0x06004FF4 RID: 20468 RVA: 0x000FA715 File Offset: 0x000F8915
		public void SetRaiseMethod(MethodBuilder mdBuilder)
		{
			if (mdBuilder == null)
			{
				throw new ArgumentNullException("mdBuilder");
			}
			this.RejectIfCreated();
			this.raise_method = mdBuilder;
		}

		/// <summary>Sets the method used to unsubscribe to this event.</summary>
		/// <param name="mdBuilder">A <see langword="MethodBuilder" /> object that represents the method used to unsubscribe to this event.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mdBuilder" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type.</exception>
		// Token: 0x06004FF5 RID: 20469 RVA: 0x000FA738 File Offset: 0x000F8938
		public void SetRemoveOnMethod(MethodBuilder mdBuilder)
		{
			if (mdBuilder == null)
			{
				throw new ArgumentNullException("mdBuilder");
			}
			this.RejectIfCreated();
			this.remove_method = mdBuilder;
		}

		/// <summary>Sets a custom attribute using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class to describe the custom attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type.</exception>
		// Token: 0x06004FF6 RID: 20470 RVA: 0x000FA75C File Offset: 0x000F895C
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			this.RejectIfCreated();
			if (customBuilder.Ctor.ReflectedType.FullName == "System.Runtime.CompilerServices.SpecialNameAttribute")
			{
				this.attrs |= EventAttributes.SpecialName;
				return;
			}
			if (this.cattrs != null)
			{
				CustomAttributeBuilder[] array = new CustomAttributeBuilder[this.cattrs.Length + 1];
				this.cattrs.CopyTo(array, 0);
				array[this.cattrs.Length] = customBuilder;
				this.cattrs = array;
				return;
			}
			this.cattrs = new CustomAttributeBuilder[1];
			this.cattrs[0] = customBuilder;
		}

		/// <summary>Set a custom attribute using a specified custom attribute blob.</summary>
		/// <param name="con">The constructor for the custom attribute.</param>
		/// <param name="binaryAttribute">A byte blob representing the attributes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> or <paramref name="binaryAttribute" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type.</exception>
		// Token: 0x06004FF7 RID: 20471 RVA: 0x000FA7F9 File Offset: 0x000F89F9
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (binaryAttribute == null)
			{
				throw new ArgumentNullException("binaryAttribute");
			}
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		// Token: 0x06004FF8 RID: 20472 RVA: 0x000FA82A File Offset: 0x000F8A2A
		private void RejectIfCreated()
		{
			if (this.typeb.is_created)
			{
				throw new InvalidOperationException("Type definition of the method is complete.");
			}
		}

		// Token: 0x06004FF9 RID: 20473 RVA: 0x000173AD File Offset: 0x000155AD
		internal EventBuilder()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400314E RID: 12622
		internal string name;

		// Token: 0x0400314F RID: 12623
		private Type type;

		// Token: 0x04003150 RID: 12624
		private TypeBuilder typeb;

		// Token: 0x04003151 RID: 12625
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x04003152 RID: 12626
		internal MethodBuilder add_method;

		// Token: 0x04003153 RID: 12627
		internal MethodBuilder remove_method;

		// Token: 0x04003154 RID: 12628
		internal MethodBuilder raise_method;

		// Token: 0x04003155 RID: 12629
		internal MethodBuilder[] other_methods;

		// Token: 0x04003156 RID: 12630
		internal EventAttributes attrs;

		// Token: 0x04003157 RID: 12631
		private int table_idx;
	}
}
