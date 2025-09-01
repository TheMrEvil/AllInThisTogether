using System;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>The exception that is thrown when an operation is performed on a disposed object.</summary>
	// Token: 0x02000168 RID: 360
	[Serializable]
	public class ObjectDisposedException : InvalidOperationException
	{
		// Token: 0x06000E58 RID: 3672 RVA: 0x0003AC51 File Offset: 0x00038E51
		private ObjectDisposedException() : this(null, "Cannot access a disposed object.")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ObjectDisposedException" /> class with a string containing the name of the disposed object.</summary>
		/// <param name="objectName">A string containing the name of the disposed object.</param>
		// Token: 0x06000E59 RID: 3673 RVA: 0x0003AC5F File Offset: 0x00038E5F
		public ObjectDisposedException(string objectName) : this(objectName, "Cannot access a disposed object.")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ObjectDisposedException" /> class with the specified object name and message.</summary>
		/// <param name="objectName">The name of the disposed object.</param>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06000E5A RID: 3674 RVA: 0x0003AC6D File Offset: 0x00038E6D
		public ObjectDisposedException(string objectName, string message) : base(message)
		{
			base.HResult = -2146232798;
			this._objectName = objectName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ObjectDisposedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If <paramref name="innerException" /> is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000E5B RID: 3675 RVA: 0x0003AC88 File Offset: 0x00038E88
		public ObjectDisposedException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232798;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ObjectDisposedException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x06000E5C RID: 3676 RVA: 0x0003AC9D File Offset: 0x00038E9D
		protected ObjectDisposedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._objectName = info.GetString("ObjectName");
		}

		/// <summary>Retrieves the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the parameter name and additional exception information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x06000E5D RID: 3677 RVA: 0x0003ACB8 File Offset: 0x00038EB8
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ObjectName", this.ObjectName, typeof(string));
		}

		/// <summary>Gets the message that describes the error.</summary>
		/// <returns>A string that describes the error.</returns>
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x0003ACE0 File Offset: 0x00038EE0
		public override string Message
		{
			get
			{
				string objectName = this.ObjectName;
				if (objectName == null || objectName.Length == 0)
				{
					return base.Message;
				}
				string str = SR.Format("Object name: '{0}'.", objectName);
				return base.Message + Environment.NewLine + str;
			}
		}

		/// <summary>Gets the name of the disposed object.</summary>
		/// <returns>A string containing the name of the disposed object.</returns>
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x0003AD23 File Offset: 0x00038F23
		public string ObjectName
		{
			get
			{
				if (this._objectName == null)
				{
					return string.Empty;
				}
				return this._objectName;
			}
		}

		// Token: 0x040012A4 RID: 4772
		private string _objectName;
	}
}
