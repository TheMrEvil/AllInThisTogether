using System;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to dynamically access a class member that does not exist or that is not declared as public. If a member in a class library has been removed or renamed, recompile any assemblies that reference that library.</summary>
	// Token: 0x020001CF RID: 463
	[Serializable]
	public class MissingMemberException : MemberAccessException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMemberException" /> class.</summary>
		// Token: 0x060013CF RID: 5071 RVA: 0x0004EAD0 File Offset: 0x0004CCD0
		public MissingMemberException() : base("Attempted to access a missing member.")
		{
			base.HResult = -2146233070;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMemberException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x060013D0 RID: 5072 RVA: 0x0004EAE8 File Offset: 0x0004CCE8
		public MissingMemberException(string message) : base(message)
		{
			base.HResult = -2146233070;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMemberException" /> class with a specified error message and a reference to the inner exception that is the root cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">An instance of <see cref="T:System.Exception" /> that is the cause of the current <see langword="Exception" />. If <paramref name="inner" /> is not a null reference (<see langword="Nothing" /> in Visual Basic), then the current <see langword="Exception" /> is raised in a catch block handling <paramref name="inner" />.</param>
		// Token: 0x060013D1 RID: 5073 RVA: 0x0004EAFC File Offset: 0x0004CCFC
		public MissingMemberException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233070;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMemberException" /> class with the specified class name and member name.</summary>
		/// <param name="className">The name of the class in which access to a nonexistent member was attempted.</param>
		/// <param name="memberName">The name of the member that cannot be accessed.</param>
		// Token: 0x060013D2 RID: 5074 RVA: 0x0004EB11 File Offset: 0x0004CD11
		public MissingMemberException(string className, string memberName)
		{
			this.ClassName = className;
			this.MemberName = memberName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMemberException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x060013D3 RID: 5075 RVA: 0x0004EB28 File Offset: 0x0004CD28
		protected MissingMemberException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ClassName = info.GetString("MMClassName");
			this.MemberName = info.GetString("MMMemberName");
			this.Signature = (byte[])info.GetValue("MMSignature", typeof(byte[]));
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the class name, the member name, the signature of the missing member, and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> object is <see langword="null" />.</exception>
		// Token: 0x060013D4 RID: 5076 RVA: 0x0004EB80 File Offset: 0x0004CD80
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("MMClassName", this.ClassName, typeof(string));
			info.AddValue("MMMemberName", this.MemberName, typeof(string));
			info.AddValue("MMSignature", this.Signature, typeof(byte[]));
		}

		/// <summary>Gets the text string showing the class name, the member name, and the signature of the missing member.</summary>
		/// <returns>The error message string.</returns>
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x0004EBE8 File Offset: 0x0004CDE8
		public override string Message
		{
			[SecuritySafeCritical]
			get
			{
				if (this.ClassName == null)
				{
					return base.Message;
				}
				return SR.Format("Member '{0}' not found.", this.ClassName + "." + this.MemberName + ((this.Signature != null) ? (" " + MissingMemberException.FormatSignature(this.Signature)) : string.Empty));
			}
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x000258DB File Offset: 0x00023ADB
		internal static string FormatSignature(byte[] signature)
		{
			return string.Empty;
		}

		/// <summary>Holds the class name of the missing member.</summary>
		// Token: 0x04001459 RID: 5209
		protected string ClassName;

		/// <summary>Holds the name of the missing member.</summary>
		// Token: 0x0400145A RID: 5210
		protected string MemberName;

		/// <summary>Holds the signature of the missing member.</summary>
		// Token: 0x0400145B RID: 5211
		protected byte[] Signature;
	}
}
