using System;
using System.Runtime.InteropServices;
using Unity;

namespace System.Reflection.Emit
{
	/// <summary>Creates or associates parameter information.</summary>
	// Token: 0x0200093E RID: 2366
	[ComDefaultInterface(typeof(_ParameterBuilder))]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public class ParameterBuilder : _ParameterBuilder
	{
		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060051FE RID: 20990 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _ParameterBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060051FF RID: 20991 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _ParameterBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06005200 RID: 20992 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _ParameterBuilder.GetTypeInfoCount(out uint pcTInfo)
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
		// Token: 0x06005201 RID: 20993 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _ParameterBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005202 RID: 20994 RVA: 0x001021C8 File Offset: 0x001003C8
		internal ParameterBuilder(MethodBase mb, int pos, ParameterAttributes attributes, string strParamName)
		{
			this.name = strParamName;
			this.position = pos;
			this.attrs = attributes;
			this.methodb = mb;
			if (mb is DynamicMethod)
			{
				this.table_idx = 0;
				return;
			}
			this.table_idx = mb.get_next_table_index(this, 8, 1);
		}

		/// <summary>Retrieves the attributes for this parameter.</summary>
		/// <returns>Read-only. Retrieves the attributes for this parameter.</returns>
		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x06005203 RID: 20995 RVA: 0x00102217 File Offset: 0x00100417
		public virtual int Attributes
		{
			get
			{
				return (int)this.attrs;
			}
		}

		/// <summary>Retrieves whether this is an input parameter.</summary>
		/// <returns>Read-only. Retrieves whether this is an input parameter.</returns>
		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x06005204 RID: 20996 RVA: 0x0010221F File Offset: 0x0010041F
		public bool IsIn
		{
			get
			{
				return (this.attrs & ParameterAttributes.In) > ParameterAttributes.None;
			}
		}

		/// <summary>Retrieves whether this parameter is an output parameter.</summary>
		/// <returns>Read-only. Retrieves whether this parameter is an output parameter.</returns>
		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x06005205 RID: 20997 RVA: 0x0010222C File Offset: 0x0010042C
		public bool IsOut
		{
			get
			{
				return (this.attrs & ParameterAttributes.Out) > ParameterAttributes.None;
			}
		}

		/// <summary>Retrieves whether this parameter is optional.</summary>
		/// <returns>Read-only. Specifies whether this parameter is optional.</returns>
		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x06005206 RID: 20998 RVA: 0x00102239 File Offset: 0x00100439
		public bool IsOptional
		{
			get
			{
				return (this.attrs & ParameterAttributes.Optional) > ParameterAttributes.None;
			}
		}

		/// <summary>Retrieves the name of this parameter.</summary>
		/// <returns>Read-only. Retrieves the name of this parameter.</returns>
		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x06005207 RID: 20999 RVA: 0x00102247 File Offset: 0x00100447
		public virtual string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Retrieves the signature position for this parameter.</summary>
		/// <returns>Read-only. Retrieves the signature position for this parameter.</returns>
		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x06005208 RID: 21000 RVA: 0x0010224F File Offset: 0x0010044F
		public virtual int Position
		{
			get
			{
				return this.position;
			}
		}

		/// <summary>Retrieves the token for this parameter.</summary>
		/// <returns>The token for this parameter.</returns>
		// Token: 0x06005209 RID: 21001 RVA: 0x00102257 File Offset: 0x00100457
		public virtual ParameterToken GetToken()
		{
			return new ParameterToken(8 | this.table_idx);
		}

		/// <summary>Sets the default value of the parameter.</summary>
		/// <param name="defaultValue">The default value of this parameter.</param>
		/// <exception cref="T:System.ArgumentException">The parameter is not one of the supported types.  
		///  -or-  
		///  The type of <paramref name="defaultValue" /> does not match the type of the parameter.  
		///  -or-  
		///  The parameter is of type <see cref="T:System.Object" /> or other reference type, <paramref name="defaultValue" /> is not <see langword="null" />, and the value cannot be assigned to the reference type.</exception>
		// Token: 0x0600520A RID: 21002 RVA: 0x00102266 File Offset: 0x00100466
		public virtual void SetConstant(object defaultValue)
		{
			if (this.position > 0)
			{
				TypeBuilder.SetConstantValue(this.methodb.GetParameterType(this.position - 1), defaultValue, ref defaultValue);
			}
			this.def_value = defaultValue;
			this.attrs |= ParameterAttributes.HasDefault;
		}

		/// <summary>Set a custom attribute using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class to define the custom attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> is <see langword="null" />.</exception>
		// Token: 0x0600520B RID: 21003 RVA: 0x001022A8 File Offset: 0x001004A8
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			string fullName = customBuilder.Ctor.ReflectedType.FullName;
			if (fullName == "System.Runtime.InteropServices.InAttribute")
			{
				this.attrs |= ParameterAttributes.In;
				return;
			}
			if (fullName == "System.Runtime.InteropServices.OutAttribute")
			{
				this.attrs |= ParameterAttributes.Out;
				return;
			}
			if (fullName == "System.Runtime.InteropServices.OptionalAttribute")
			{
				this.attrs |= ParameterAttributes.Optional;
				return;
			}
			if (fullName == "System.Runtime.InteropServices.MarshalAsAttribute")
			{
				this.attrs |= ParameterAttributes.HasFieldMarshal;
				this.marshal_info = CustomAttributeBuilder.get_umarshal(customBuilder, false);
				return;
			}
			if (fullName == "System.Runtime.InteropServices.DefaultParameterValueAttribute")
			{
				CustomAttributeBuilder.CustomAttributeInfo customAttributeInfo = CustomAttributeBuilder.decode_cattr(customBuilder);
				this.SetConstant(customAttributeInfo.ctorArgs[0]);
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
		// Token: 0x0600520C RID: 21004 RVA: 0x001023B8 File Offset: 0x001005B8
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		/// <summary>Specifies the marshaling for this parameter.</summary>
		/// <param name="unmanagedMarshal">The marshaling information for this parameter.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="unmanagedMarshal" /> is <see langword="null" />.</exception>
		// Token: 0x0600520D RID: 21005 RVA: 0x001023C7 File Offset: 0x001005C7
		[Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead.")]
		public virtual void SetMarshal(UnmanagedMarshal unmanagedMarshal)
		{
			this.marshal_info = unmanagedMarshal;
			this.attrs |= ParameterAttributes.HasFieldMarshal;
		}

		// Token: 0x0600520E RID: 21006 RVA: 0x000173AD File Offset: 0x000155AD
		internal ParameterBuilder()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040032EE RID: 13038
		private MethodBase methodb;

		// Token: 0x040032EF RID: 13039
		private string name;

		// Token: 0x040032F0 RID: 13040
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x040032F1 RID: 13041
		private UnmanagedMarshal marshal_info;

		// Token: 0x040032F2 RID: 13042
		private ParameterAttributes attrs;

		// Token: 0x040032F3 RID: 13043
		private int position;

		// Token: 0x040032F4 RID: 13044
		private int table_idx;

		// Token: 0x040032F5 RID: 13045
		private object def_value;
	}
}
