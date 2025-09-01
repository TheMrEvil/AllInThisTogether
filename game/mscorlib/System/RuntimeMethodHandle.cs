using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;

namespace System
{
	/// <summary>
	///   <see cref="T:System.RuntimeMethodHandle" /> is a handle to the internal metadata representation of a method.</summary>
	// Token: 0x0200024B RID: 587
	[ComVisible(true)]
	[Serializable]
	public struct RuntimeMethodHandle : ISerializable
	{
		// Token: 0x06001AFA RID: 6906 RVA: 0x00064706 File Offset: 0x00062906
		internal RuntimeMethodHandle(IntPtr v)
		{
			this.value = v;
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x00064710 File Offset: 0x00062910
		private RuntimeMethodHandle(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			RuntimeMethodInfo runtimeMethodInfo = (RuntimeMethodInfo)info.GetValue("MethodObj", typeof(RuntimeMethodInfo));
			this.value = runtimeMethodInfo.MethodHandle.Value;
			if (this.value == IntPtr.Zero)
			{
				throw new SerializationException("Insufficient state.");
			}
		}

		/// <summary>Gets the value of this instance.</summary>
		/// <returns>A <see cref="T:System.RuntimeMethodHandle" /> that is the internal metadata representation of a method.</returns>
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001AFC RID: 6908 RVA: 0x00064777 File Offset: 0x00062977
		public IntPtr Value
		{
			get
			{
				return this.value;
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data necessary to deserialize the field represented by this instance.</summary>
		/// <param name="info">The object to populate with serialization information.</param>
		/// <param name="context">(Reserved) The place to store and retrieve serialized data.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">
		///   <see cref="P:System.RuntimeMethodHandle.Value" /> is invalid.</exception>
		// Token: 0x06001AFD RID: 6909 RVA: 0x00064780 File Offset: 0x00062980
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
			info.AddValue("MethodObj", (RuntimeMethodInfo)MethodBase.GetMethodFromHandle(this), typeof(RuntimeMethodInfo));
		}

		// Token: 0x06001AFE RID: 6910
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetFunctionPointer(IntPtr m);

		/// <summary>Obtains a pointer to the method represented by this instance.</summary>
		/// <returns>A pointer to the method represented by this instance.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the necessary permission to perform this operation.</exception>
		// Token: 0x06001AFF RID: 6911 RVA: 0x000647DD File Offset: 0x000629DD
		public IntPtr GetFunctionPointer()
		{
			return RuntimeMethodHandle.GetFunctionPointer(this.value);
		}

		/// <summary>Indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">A <see cref="T:System.Object" /> to compare to this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.RuntimeMethodHandle" /> and equal to the value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001B00 RID: 6912 RVA: 0x000647EC File Offset: 0x000629EC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.value == ((RuntimeMethodHandle)obj).Value;
		}

		/// <summary>Indicates whether this instance is equal to a specified <see cref="T:System.RuntimeMethodHandle" />.</summary>
		/// <param name="handle">A <see cref="T:System.RuntimeMethodHandle" /> to compare to this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="handle" /> is equal to the value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001B01 RID: 6913 RVA: 0x00064834 File Offset: 0x00062A34
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public bool Equals(RuntimeMethodHandle handle)
		{
			return this.value == handle.Value;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001B02 RID: 6914 RVA: 0x00064848 File Offset: 0x00062A48
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		/// <summary>Indicates whether two instances of <see cref="T:System.RuntimeMethodHandle" /> are equal.</summary>
		/// <param name="left">A <see cref="T:System.RuntimeMethodHandle" /> to compare to <paramref name="right" />.</param>
		/// <param name="right">A <see cref="T:System.RuntimeMethodHandle" /> to compare to <paramref name="left" />.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="left" /> is equal to the value of <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001B03 RID: 6915 RVA: 0x00064855 File Offset: 0x00062A55
		public static bool operator ==(RuntimeMethodHandle left, RuntimeMethodHandle right)
		{
			return left.Equals(right);
		}

		/// <summary>Indicates whether two instances of <see cref="T:System.RuntimeMethodHandle" /> are not equal.</summary>
		/// <param name="left">A <see cref="T:System.RuntimeMethodHandle" /> to compare to <paramref name="right" />.</param>
		/// <param name="right">A <see cref="T:System.RuntimeMethodHandle" /> to compare to <paramref name="left" />.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="left" /> is unequal to the value of <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001B04 RID: 6916 RVA: 0x0006485F File Offset: 0x00062A5F
		public static bool operator !=(RuntimeMethodHandle left, RuntimeMethodHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x0006486C File Offset: 0x00062A6C
		internal static string ConstructInstantiation(RuntimeMethodInfo method, TypeNameFormatFlags format)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Type[] genericArguments = method.GetGenericArguments();
			stringBuilder.Append("[");
			for (int i = 0; i < genericArguments.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(genericArguments[i].Name);
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x000648D1 File Offset: 0x00062AD1
		internal bool IsNullHandle()
		{
			return this.value == IntPtr.Zero;
		}

		// Token: 0x04001779 RID: 6009
		private IntPtr value;
	}
}
