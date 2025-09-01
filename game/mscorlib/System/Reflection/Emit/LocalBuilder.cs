using System;
using System.Runtime.InteropServices;
using Unity;

namespace System.Reflection.Emit
{
	/// <summary>Represents a local variable within a method or constructor.</summary>
	// Token: 0x02000933 RID: 2355
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_LocalBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class LocalBuilder : LocalVariableInfo, _LocalBuilder
	{
		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060050F2 RID: 20722 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _LocalBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060050F3 RID: 20723 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _LocalBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060050F4 RID: 20724 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _LocalBuilder.GetTypeInfoCount(out uint pcTInfo)
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
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060050F5 RID: 20725 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _LocalBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060050F6 RID: 20726 RVA: 0x000FD7C8 File Offset: 0x000FB9C8
		internal LocalBuilder(Type t, ILGenerator ilgen)
		{
			this.type = t;
			this.ilgen = ilgen;
		}

		/// <summary>Sets the name and lexical scope of this local variable.</summary>
		/// <param name="name">The name of the local variable.</param>
		/// <param name="startOffset">The beginning offset of the lexical scope of the local variable.</param>
		/// <param name="endOffset">The ending offset of the lexical scope of the local variable.</param>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created with <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  There is no symbolic writer defined for the containing module.</exception>
		/// <exception cref="T:System.NotSupportedException">This local is defined in a dynamic method, rather than in a method of a dynamic type.</exception>
		// Token: 0x060050F7 RID: 20727 RVA: 0x000FD7DE File Offset: 0x000FB9DE
		public void SetLocalSymInfo(string name, int startOffset, int endOffset)
		{
			this.name = name;
			this.startOffset = startOffset;
			this.endOffset = endOffset;
		}

		/// <summary>Sets the name of this local variable.</summary>
		/// <param name="name">The name of the local variable.</param>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created with <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  There is no symbolic writer defined for the containing module.</exception>
		/// <exception cref="T:System.NotSupportedException">This local is defined in a dynamic method, rather than in a method of a dynamic type.</exception>
		// Token: 0x060050F8 RID: 20728 RVA: 0x000FD7F5 File Offset: 0x000FB9F5
		public void SetLocalSymInfo(string name)
		{
			this.SetLocalSymInfo(name, 0, 0);
		}

		/// <summary>Gets the type of the local variable.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the local variable.</returns>
		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x060050F9 RID: 20729 RVA: 0x000F30FD File Offset: 0x000F12FD
		public override Type LocalType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Gets a value indicating whether the object referred to by the local variable is pinned in memory.</summary>
		/// <returns>
		///   <see langword="true" /> if the object referred to by the local variable is pinned in memory; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x060050FA RID: 20730 RVA: 0x000F30ED File Offset: 0x000F12ED
		public override bool IsPinned
		{
			get
			{
				return this.is_pinned;
			}
		}

		/// <summary>Gets the zero-based index of the local variable within the method body.</summary>
		/// <returns>An integer value that represents the order of declaration of the local variable within the method body.</returns>
		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x060050FB RID: 20731 RVA: 0x000F30F5 File Offset: 0x000F12F5
		public override int LocalIndex
		{
			get
			{
				return (int)this.position;
			}
		}

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x060050FC RID: 20732 RVA: 0x000FD800 File Offset: 0x000FBA00
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x060050FD RID: 20733 RVA: 0x000FD808 File Offset: 0x000FBA08
		internal int StartOffset
		{
			get
			{
				return this.startOffset;
			}
		}

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x060050FE RID: 20734 RVA: 0x000FD810 File Offset: 0x000FBA10
		internal int EndOffset
		{
			get
			{
				return this.endOffset;
			}
		}

		// Token: 0x060050FF RID: 20735 RVA: 0x000173AD File Offset: 0x000155AD
		internal LocalBuilder()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040031B3 RID: 12723
		private string name;

		// Token: 0x040031B4 RID: 12724
		internal ILGenerator ilgen;

		// Token: 0x040031B5 RID: 12725
		private int startOffset;

		// Token: 0x040031B6 RID: 12726
		private int endOffset;
	}
}
