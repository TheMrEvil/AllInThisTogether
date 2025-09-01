using System;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>The exception that is thrown as a wrapper around the exception thrown by the class initializer. This class cannot be inherited.</summary>
	// Token: 0x020001A4 RID: 420
	[Serializable]
	public sealed class TypeInitializationException : SystemException
	{
		// Token: 0x060011E9 RID: 4585 RVA: 0x00047C80 File Offset: 0x00045E80
		private TypeInitializationException() : base("Type constructor threw an exception.")
		{
			base.HResult = -2146233036;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TypeInitializationException" /> class with the default error message, the specified type name, and a reference to the inner exception that is the root cause of this exception.</summary>
		/// <param name="fullTypeName">The fully qualified name of the type that fails to initialize.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060011EA RID: 4586 RVA: 0x00047C98 File Offset: 0x00045E98
		public TypeInitializationException(string fullTypeName, Exception innerException) : this(fullTypeName, SR.Format("The type initializer for '{0}' threw an exception.", fullTypeName), innerException)
		{
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x00047CAD File Offset: 0x00045EAD
		internal TypeInitializationException(string message) : base(message)
		{
			base.HResult = -2146233036;
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00047CC1 File Offset: 0x00045EC1
		internal TypeInitializationException(string fullTypeName, string message, Exception innerException) : base(message, innerException)
		{
			this._typeName = fullTypeName;
			base.HResult = -2146233036;
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x00047CDD File Offset: 0x00045EDD
		internal TypeInitializationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._typeName = info.GetString("TypeName");
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the type name and additional exception information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x060011EE RID: 4590 RVA: 0x00047CF8 File Offset: 0x00045EF8
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("TypeName", this.TypeName, typeof(string));
		}

		/// <summary>Gets the fully qualified name of the type that fails to initialize.</summary>
		/// <returns>The fully qualified name of the type that fails to initialize.</returns>
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x00047D1D File Offset: 0x00045F1D
		public string TypeName
		{
			get
			{
				if (this._typeName == null)
				{
					return string.Empty;
				}
				return this._typeName;
			}
		}

		// Token: 0x0400135A RID: 4954
		private string _typeName;
	}
}
