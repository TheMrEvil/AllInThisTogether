using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>Exposes the public members of the <see cref="T:System.Exception" /> class to unmanaged code.</summary>
	// Token: 0x0200076D RID: 1901
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[Guid("b36b5c63-42ef-38bc-a07e-0b34c98f164a")]
	[ComVisible(true)]
	[CLSCompliant(false)]
	public interface _Exception
	{
		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Exception.HelpLink" /> property.</summary>
		/// <returns>The Uniform Resource Name (URN) or Uniform Resource Locator (URL) to a help file.</returns>
		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x0600432E RID: 17198
		// (set) Token: 0x0600432F RID: 17199
		string HelpLink { get; set; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Exception.InnerException" /> property.</summary>
		/// <returns>An instance of <see cref="T:System.Exception" /> that describes the error that caused the current exception. The <see cref="P:System.Exception.InnerException" /> property returns the same value that was passed to the constructor, or a null reference (<see langword="Nothing" /> in Visual Basic) if the inner exception value was not supplied to the constructor. This property is read-only.</returns>
		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06004330 RID: 17200
		Exception InnerException { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Exception.Message" /> property.</summary>
		/// <returns>The error message that explains the reason for the exception, or an empty string("").</returns>
		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06004331 RID: 17201
		string Message { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Exception.Source" /> property.</summary>
		/// <returns>The name of the application or the object that causes the error.</returns>
		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06004332 RID: 17202
		// (set) Token: 0x06004333 RID: 17203
		string Source { get; set; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Exception.StackTrace" /> property.</summary>
		/// <returns>A string that describes the contents of the call stack, with the most recent method call appearing first.</returns>
		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06004334 RID: 17204
		string StackTrace { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Exception.TargetSite" /> property.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodBase" /> object that threw the current exception.</returns>
		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06004335 RID: 17205
		MethodBase TargetSite { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.Equals(System.Object)" /> method.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004336 RID: 17206
		bool Equals(object obj);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Exception.GetBaseException" /> method.</summary>
		/// <returns>The first exception thrown in a chain of exceptions. If the <see cref="P:System.Exception.InnerException" /> property of the current exception is a null reference (<see langword="Nothing" /> in Visual Basic), this property returns the current exception.</returns>
		// Token: 0x06004337 RID: 17207
		Exception GetBaseException();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.GetHashCode" /> method.</summary>
		/// <returns>The hash code for the current instance.</returns>
		// Token: 0x06004338 RID: 17208
		int GetHashCode();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Exception.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> method</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that contains contextual information about the source or destination.</param>
		// Token: 0x06004339 RID: 17209
		void GetObjectData(SerializationInfo info, StreamingContext context);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Exception.GetType" /> method.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the exact runtime type of the current instance.</returns>
		// Token: 0x0600433A RID: 17210
		Type GetType();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Exception.ToString" /> method.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Exception" /> object.</returns>
		// Token: 0x0600433B RID: 17211
		string ToString();
	}
}
