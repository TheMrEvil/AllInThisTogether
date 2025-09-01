using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Globalization
{
	/// <summary>The exception that is thrown when a method attempts to construct a culture that is not available.</summary>
	// Token: 0x02000957 RID: 2391
	[Serializable]
	public class CultureNotFoundException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with its message string set to a system-supplied message.</summary>
		// Token: 0x06005499 RID: 21657 RVA: 0x0011AB3E File Offset: 0x00118D3E
		public CultureNotFoundException() : base(CultureNotFoundException.DefaultMessage)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with the specified error message.</summary>
		/// <param name="message">The error message to display with this exception.</param>
		// Token: 0x0600549A RID: 21658 RVA: 0x0011AB4B File Offset: 0x00118D4B
		public CultureNotFoundException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with a specified error message and the name of the parameter that is the cause this exception.</summary>
		/// <param name="paramName">The name of the parameter that is the cause of the current exception.</param>
		/// <param name="message">The error message to display with this exception.</param>
		// Token: 0x0600549B RID: 21659 RVA: 0x0011AB54 File Offset: 0x00118D54
		public CultureNotFoundException(string paramName, string message) : base(message, paramName)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message to display with this exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x0600549C RID: 21660 RVA: 0x0011AB5E File Offset: 0x00118D5E
		public CultureNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with a specified error message, the invalid Culture Name, and the name of the parameter that is the cause this exception.</summary>
		/// <param name="paramName">The name of the parameter that is the cause the current exception.</param>
		/// <param name="invalidCultureName">The Culture Name that cannot be found.</param>
		/// <param name="message">The error message to display with this exception.</param>
		// Token: 0x0600549D RID: 21661 RVA: 0x0011AB68 File Offset: 0x00118D68
		public CultureNotFoundException(string paramName, string invalidCultureName, string message) : base(message, paramName)
		{
			this._invalidCultureName = invalidCultureName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with a specified error message, the invalid Culture Name, and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message to display with this exception.</param>
		/// <param name="invalidCultureName">The Culture Name that cannot be found.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x0600549E RID: 21662 RVA: 0x0011AB79 File Offset: 0x00118D79
		public CultureNotFoundException(string message, string invalidCultureName, Exception innerException) : base(message, innerException)
		{
			this._invalidCultureName = invalidCultureName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with a specified error message, the invalid Culture ID, and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message to display with this exception.</param>
		/// <param name="invalidCultureId">The Culture ID that cannot be found.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x0600549F RID: 21663 RVA: 0x0011AB8A File Offset: 0x00118D8A
		public CultureNotFoundException(string message, int invalidCultureId, Exception innerException) : base(message, innerException)
		{
			this._invalidCultureId = new int?(invalidCultureId);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with a specified error message, the invalid Culture ID, and the name of the parameter that is the cause this exception.</summary>
		/// <param name="paramName">The name of the parameter that is the cause the current exception.</param>
		/// <param name="invalidCultureId">The Culture ID that cannot be found.</param>
		/// <param name="message">The error message to display with this exception.</param>
		// Token: 0x060054A0 RID: 21664 RVA: 0x0011ABA0 File Offset: 0x00118DA0
		public CultureNotFoundException(string paramName, int invalidCultureId, string message) : base(message, paramName)
		{
			this._invalidCultureId = new int?(invalidCultureId);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class using the specified serialization data and context.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x060054A1 RID: 21665 RVA: 0x0011ABB8 File Offset: 0x00118DB8
		protected CultureNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._invalidCultureId = (int?)info.GetValue("InvalidCultureId", typeof(int?));
			this._invalidCultureName = (string)info.GetValue("InvalidCultureName", typeof(string));
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the parameter name and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x060054A2 RID: 21666 RVA: 0x0011AC10 File Offset: 0x00118E10
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("InvalidCultureId", this._invalidCultureId, typeof(int?));
			info.AddValue("InvalidCultureName", this._invalidCultureName, typeof(string));
		}

		/// <summary>Gets the culture identifier that cannot be found.</summary>
		/// <returns>The invalid culture identifier.</returns>
		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x060054A3 RID: 21667 RVA: 0x0011AC60 File Offset: 0x00118E60
		public virtual int? InvalidCultureId
		{
			get
			{
				return this._invalidCultureId;
			}
		}

		/// <summary>Gets the culture name that cannot be found.</summary>
		/// <returns>The invalid culture name.</returns>
		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x060054A4 RID: 21668 RVA: 0x0011AC68 File Offset: 0x00118E68
		public virtual string InvalidCultureName
		{
			get
			{
				return this._invalidCultureName;
			}
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x060054A5 RID: 21669 RVA: 0x0011AC70 File Offset: 0x00118E70
		private static string DefaultMessage
		{
			get
			{
				return "Culture is not supported.";
			}
		}

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x060054A6 RID: 21670 RVA: 0x0011AC78 File Offset: 0x00118E78
		private string FormatedInvalidCultureId
		{
			get
			{
				if (this.InvalidCultureId == null)
				{
					return this.InvalidCultureName;
				}
				return string.Format(CultureInfo.InvariantCulture, "{0} (0x{0:x4})", this.InvalidCultureId.Value);
			}
		}

		/// <summary>Gets the error message that explains the reason for the exception.</summary>
		/// <returns>A text string describing the details of the exception.</returns>
		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x060054A7 RID: 21671 RVA: 0x0011ACC0 File Offset: 0x00118EC0
		public override string Message
		{
			get
			{
				string message = base.Message;
				if (this._invalidCultureId == null && this._invalidCultureName == null)
				{
					return message;
				}
				string text = SR.Format("{0} is an invalid culture identifier.", this.FormatedInvalidCultureId);
				if (message == null)
				{
					return text;
				}
				return message + Environment.NewLine + text;
			}
		}

		// Token: 0x040033D2 RID: 13266
		private string _invalidCultureName;

		// Token: 0x040033D3 RID: 13267
		private int? _invalidCultureId;
	}
}
