using System;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to dynamically access a method that does not exist.</summary>
	// Token: 0x0200015C RID: 348
	[Serializable]
	public class MissingMethodException : MissingMemberException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMethodException" /> class.</summary>
		// Token: 0x06000DC8 RID: 3528 RVA: 0x00035D7A File Offset: 0x00033F7A
		public MissingMethodException() : base("Attempted to access a missing method.")
		{
			base.HResult = -2146233069;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMethodException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		// Token: 0x06000DC9 RID: 3529 RVA: 0x00035D92 File Offset: 0x00033F92
		public MissingMethodException(string message) : base(message)
		{
			base.HResult = -2146233069;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMethodException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000DCA RID: 3530 RVA: 0x00035DA6 File Offset: 0x00033FA6
		public MissingMethodException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233069;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMethodException" /> class with the specified class name and method name.</summary>
		/// <param name="className">The name of the class in which access to a nonexistent method was attempted.</param>
		/// <param name="methodName">The name of the method that cannot be accessed.</param>
		// Token: 0x06000DCB RID: 3531 RVA: 0x00035DBB File Offset: 0x00033FBB
		public MissingMethodException(string className, string methodName)
		{
			this.ClassName = className;
			this.MemberName = methodName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMethodException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000DCC RID: 3532 RVA: 0x00035DD1 File Offset: 0x00033FD1
		protected MissingMethodException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Gets the text string showing the class name, the method name, and the signature of the missing method. This property is read-only.</summary>
		/// <returns>The error message string.</returns>
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x00035DDC File Offset: 0x00033FDC
		public override string Message
		{
			[SecuritySafeCritical]
			get
			{
				if (this.ClassName != null)
				{
					return SR.Format("Method '{0}' not found.", this.ClassName + "." + this.MemberName + ((this.Signature != null) ? (" " + MissingMemberException.FormatSignature(this.Signature)) : string.Empty));
				}
				return base.Message;
			}
		}
	}
}
