using System;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to dynamically access a field that does not exist. If a field in a class library has been removed or renamed, recompile any assemblies that reference that library.</summary>
	// Token: 0x020001CE RID: 462
	[Serializable]
	public class MissingFieldException : MissingMemberException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MissingFieldException" /> class.</summary>
		// Token: 0x060013C9 RID: 5065 RVA: 0x0004EA2E File Offset: 0x0004CC2E
		public MissingFieldException() : base("Attempted to access a non-existing field.")
		{
			base.HResult = -2146233071;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingFieldException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		// Token: 0x060013CA RID: 5066 RVA: 0x0004EA46 File Offset: 0x0004CC46
		public MissingFieldException(string message) : base(message)
		{
			base.HResult = -2146233071;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingFieldException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060013CB RID: 5067 RVA: 0x0004EA5A File Offset: 0x0004CC5A
		public MissingFieldException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233071;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingFieldException" /> class with the specified class name and field name.</summary>
		/// <param name="className">The name of the class in which access to a nonexistent field was attempted.</param>
		/// <param name="fieldName">The name of the field that cannot be accessed.</param>
		// Token: 0x060013CC RID: 5068 RVA: 0x00035DBB File Offset: 0x00033FBB
		public MissingFieldException(string className, string fieldName)
		{
			this.ClassName = className;
			this.MemberName = fieldName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingFieldException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x060013CD RID: 5069 RVA: 0x00035DD1 File Offset: 0x00033FD1
		protected MissingFieldException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Gets the text string showing the signature of the missing field, the class name, and the field name. This property is read-only.</summary>
		/// <returns>The error message string.</returns>
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060013CE RID: 5070 RVA: 0x0004EA70 File Offset: 0x0004CC70
		public override string Message
		{
			[SecuritySafeCritical]
			get
			{
				if (this.ClassName == null)
				{
					return base.Message;
				}
				return SR.Format("Field '{0}' not found.", ((this.Signature != null) ? (MissingMemberException.FormatSignature(this.Signature) + " ") : "") + this.ClassName + "." + this.MemberName);
			}
		}
	}
}
