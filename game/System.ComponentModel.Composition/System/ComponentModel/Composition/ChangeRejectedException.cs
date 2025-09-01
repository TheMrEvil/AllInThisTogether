using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Internal;

namespace System.ComponentModel.Composition
{
	/// <summary>An exception that indicates whether a part has been rejected during composition.</summary>
	// Token: 0x02000023 RID: 35
	[Serializable]
	public class ChangeRejectedException : CompositionException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ChangeRejectedException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x0600012C RID: 300 RVA: 0x00004380 File Offset: 0x00002580
		public ChangeRejectedException() : this(null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ChangeRejectedException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x0600012D RID: 301 RVA: 0x0000438A File Offset: 0x0000258A
		public ChangeRejectedException(string message) : this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ChangeRejectedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x0600012E RID: 302 RVA: 0x00004394 File Offset: 0x00002594
		public ChangeRejectedException(string message, Exception innerException) : base(message, innerException, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ChangeRejectedException" /> class with a list of composition errors.</summary>
		/// <param name="errors">A collection of errors that occurred during composition.</param>
		// Token: 0x0600012F RID: 303 RVA: 0x0000439F File Offset: 0x0000259F
		public ChangeRejectedException(IEnumerable<CompositionError> errors) : base(null, null, errors)
		{
		}

		/// <summary>Gets or sets the message associated with the component rejection.</summary>
		/// <returns>The message associated with the component rejection.</returns>
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000043AA File Offset: 0x000025AA
		public override string Message
		{
			get
			{
				return string.Format(CultureInfo.CurrentCulture, Strings.CompositionException_ChangesRejected, base.Message);
			}
		}
	}
}
