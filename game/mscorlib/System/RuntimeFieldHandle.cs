using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>Represents a field using an internal metadata token.</summary>
	// Token: 0x0200024A RID: 586
	[ComVisible(true)]
	[Serializable]
	public struct RuntimeFieldHandle : ISerializable
	{
		// Token: 0x06001AEC RID: 6892 RVA: 0x0006458C File Offset: 0x0006278C
		internal RuntimeFieldHandle(IntPtr v)
		{
			this.value = v;
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x00064598 File Offset: 0x00062798
		private RuntimeFieldHandle(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			RuntimeFieldInfo runtimeFieldInfo = (RuntimeFieldInfo)info.GetValue("FieldObj", typeof(RuntimeFieldInfo));
			this.value = runtimeFieldInfo.FieldHandle.Value;
			if (this.value == IntPtr.Zero)
			{
				throw new SerializationException("Insufficient state.");
			}
		}

		/// <summary>Gets a handle to the field represented by the current instance.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> that contains the handle to the field represented by the current instance.</returns>
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06001AEE RID: 6894 RVA: 0x000645FF File Offset: 0x000627FF
		public IntPtr Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x00064607 File Offset: 0x00062807
		internal bool IsNullHandle()
		{
			return this.value == IntPtr.Zero;
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data necessary to deserialize the field represented by the current instance.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with serialization information.</param>
		/// <param name="context">(Reserved) The place to store and retrieve serialized data.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="P:System.RuntimeFieldHandle.Value" /> property of the current instance is not a valid handle.</exception>
		// Token: 0x06001AF0 RID: 6896 RVA: 0x0006461C File Offset: 0x0006281C
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (this.value == IntPtr.Zero)
			{
				throw new SerializationException("Object fields may not be properly initialized");
			}
			info.AddValue("FieldObj", (RuntimeFieldInfo)FieldInfo.GetFieldFromHandle(this), typeof(RuntimeFieldInfo));
		}

		/// <summary>Indicates whether the current instance is equal to the specified object.</summary>
		/// <param name="obj">The object to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.RuntimeFieldHandle" /> and equal to the value of the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001AF1 RID: 6897 RVA: 0x0006467C File Offset: 0x0006287C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.value == ((RuntimeFieldHandle)obj).Value;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.RuntimeFieldHandle" />.</summary>
		/// <param name="handle">The <see cref="T:System.RuntimeFieldHandle" /> to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="handle" /> is equal to the value of the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001AF2 RID: 6898 RVA: 0x000646C4 File Offset: 0x000628C4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public bool Equals(RuntimeFieldHandle handle)
		{
			return this.value == handle.Value;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
		// Token: 0x06001AF3 RID: 6899 RVA: 0x000646D8 File Offset: 0x000628D8
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		/// <summary>Indicates whether two <see cref="T:System.RuntimeFieldHandle" /> structures are equal.</summary>
		/// <param name="left">The <see cref="T:System.RuntimeFieldHandle" /> to compare to <paramref name="right" />.</param>
		/// <param name="right">The <see cref="T:System.RuntimeFieldHandle" /> to compare to <paramref name="left" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001AF4 RID: 6900 RVA: 0x000646E5 File Offset: 0x000628E5
		public static bool operator ==(RuntimeFieldHandle left, RuntimeFieldHandle right)
		{
			return left.Equals(right);
		}

		/// <summary>Indicates whether two <see cref="T:System.RuntimeFieldHandle" /> structures are not equal.</summary>
		/// <param name="left">The <see cref="T:System.RuntimeFieldHandle" /> to compare to <paramref name="right" />.</param>
		/// <param name="right">The <see cref="T:System.RuntimeFieldHandle" /> to compare to <paramref name="left" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001AF5 RID: 6901 RVA: 0x000646EF File Offset: 0x000628EF
		public static bool operator !=(RuntimeFieldHandle left, RuntimeFieldHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06001AF6 RID: 6902
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetValueInternal(FieldInfo fi, object obj, object value);

		// Token: 0x06001AF7 RID: 6903 RVA: 0x000646FC File Offset: 0x000628FC
		internal static void SetValue(RuntimeFieldInfo field, object obj, object value, RuntimeType fieldType, FieldAttributes fieldAttr, RuntimeType declaringType, ref bool domainInitialized)
		{
			RuntimeFieldHandle.SetValueInternal(field, obj, value);
		}

		// Token: 0x06001AF8 RID: 6904
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern object GetValueDirect(RuntimeFieldInfo field, RuntimeType fieldType, void* pTypedRef, RuntimeType contextType);

		// Token: 0x06001AF9 RID: 6905
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void SetValueDirect(RuntimeFieldInfo field, RuntimeType fieldType, void* pTypedRef, object value, RuntimeType contextType);

		// Token: 0x04001778 RID: 6008
		private IntPtr value;
	}
}
