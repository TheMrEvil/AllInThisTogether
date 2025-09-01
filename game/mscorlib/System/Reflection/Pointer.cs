using System;
using System.Runtime.Serialization;
using Unity;

namespace System.Reflection
{
	/// <summary>Provides a wrapper class for pointers.</summary>
	// Token: 0x020008B8 RID: 2232
	[CLSCompliant(false)]
	public sealed class Pointer : ISerializable
	{
		// Token: 0x060049DE RID: 18910 RVA: 0x000EF401 File Offset: 0x000ED601
		private unsafe Pointer(void* ptr, Type ptrType)
		{
			this._ptr = ptr;
			this._ptrType = ptrType;
		}

		/// <summary>Boxes the supplied unmanaged memory pointer and the type associated with that pointer into a managed <see cref="T:System.Reflection.Pointer" /> wrapper object. The value and the type are saved so they can be accessed from the native code during an invocation.</summary>
		/// <param name="ptr">The supplied unmanaged memory pointer.</param>
		/// <param name="type">The type associated with the <paramref name="ptr" /> parameter.</param>
		/// <returns>A pointer object.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is not a pointer.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		// Token: 0x060049DF RID: 18911 RVA: 0x000EF418 File Offset: 0x000ED618
		public unsafe static object Box(void* ptr, Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!type.IsPointer)
			{
				throw new ArgumentException("Type must be a Pointer.", "ptr");
			}
			if (!type.IsRuntimeImplemented())
			{
				throw new ArgumentException("Type must be a type provided by the runtime.", "ptr");
			}
			return new Pointer(ptr, type);
		}

		/// <summary>Returns the stored pointer.</summary>
		/// <param name="ptr">The stored pointer.</param>
		/// <returns>This method returns void.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is not a pointer.</exception>
		// Token: 0x060049E0 RID: 18912 RVA: 0x000EF470 File Offset: 0x000ED670
		public unsafe static void* Unbox(object ptr)
		{
			if (!(ptr is Pointer))
			{
				throw new ArgumentException("Type must be a Pointer.", "ptr");
			}
			return ((Pointer)ptr)._ptr;
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the file name, fusion log, and additional exception information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x060049E1 RID: 18913 RVA: 0x0001B98B File Offset: 0x00019B8B
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060049E2 RID: 18914 RVA: 0x000EF495 File Offset: 0x000ED695
		internal Type GetPointerType()
		{
			return this._ptrType;
		}

		// Token: 0x060049E3 RID: 18915 RVA: 0x000EF49D File Offset: 0x000ED69D
		internal IntPtr GetPointerValue()
		{
			return (IntPtr)this._ptr;
		}

		// Token: 0x060049E4 RID: 18916 RVA: 0x000173AD File Offset: 0x000155AD
		internal Pointer()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002F0D RID: 12045
		private unsafe readonly void* _ptr;

		// Token: 0x04002F0E RID: 12046
		private readonly Type _ptrType;
	}
}
