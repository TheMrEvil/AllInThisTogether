using System;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.Globalization;

namespace System.ComponentModel.Composition
{
	/// <summary>Represents an error that occurred during composition.</summary>
	// Token: 0x02000025 RID: 37
	[DebuggerTypeProxy(typeof(CompositionErrorDebuggerProxy))]
	[Serializable]
	public class CompositionError
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.CompositionError" /> class with the specified error message.</summary>
		/// <param name="message">A message that describes the <see cref="T:System.ComponentModel.Composition.CompositionError" /> or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.CompositionError.Description" /> property to an empty string ("").</param>
		// Token: 0x06000135 RID: 309 RVA: 0x000043DF File Offset: 0x000025DF
		public CompositionError(string message) : this(CompositionErrorId.Unknown, message, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.CompositionError" /> class with the specified error message and the composition element that is the cause of the composition error.</summary>
		/// <param name="message">A message that describes the <see cref="T:System.ComponentModel.Composition.CompositionError" /> or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.CompositionError.Description" /> property to an empty string ("").</param>
		/// <param name="element">The composition element that is the cause of the <see cref="T:System.ComponentModel.Composition.CompositionError" /> or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.CompositionError.Element" /> property to <see langword="null" />.</param>
		// Token: 0x06000136 RID: 310 RVA: 0x000043EB File Offset: 0x000025EB
		public CompositionError(string message, ICompositionElement element) : this(CompositionErrorId.Unknown, message, element, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.CompositionError" /> class with the specified error message and the exception that is the cause of the composition error.</summary>
		/// <param name="message">A message that describes the <see cref="T:System.ComponentModel.Composition.CompositionError" /> or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.CompositionError.Description" /> property to an empty string ("").</param>
		/// <param name="exception">The <see cref="P:System.ComponentModel.Composition.CompositionError.Exception" /> that is the underlying cause of the <see cref="T:System.ComponentModel.Composition.CompositionError" /> or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.CompositionError.Exception" /> property to <see langword="null" />.</param>
		// Token: 0x06000137 RID: 311 RVA: 0x000043F7 File Offset: 0x000025F7
		public CompositionError(string message, Exception exception) : this(CompositionErrorId.Unknown, message, null, exception)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.CompositionError" /> class with the specified error message, and the composition element and exception that are the cause of the composition error.</summary>
		/// <param name="message">A message that describes the <see cref="T:System.ComponentModel.Composition.CompositionError" /> or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.CompositionError.Description" /> property to an empty string ("").</param>
		/// <param name="element">The composition element that is the cause of the <see cref="T:System.ComponentModel.Composition.CompositionError" /> or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.CompositionError.Element" /> property to <see langword="null" />.</param>
		/// <param name="exception">The <see cref="P:System.ComponentModel.Composition.CompositionError.Exception" /> that is the underlying cause of the <see cref="T:System.ComponentModel.Composition.CompositionError" /> or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.CompositionError.Exception" /> property to <see langword="null" />.</param>
		// Token: 0x06000138 RID: 312 RVA: 0x00004403 File Offset: 0x00002603
		public CompositionError(string message, ICompositionElement element, Exception exception) : this(CompositionErrorId.Unknown, message, element, exception)
		{
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000440F File Offset: 0x0000260F
		internal CompositionError(CompositionErrorId id, string description, ICompositionElement element, Exception exception)
		{
			this._id = id;
			this._description = (description ?? string.Empty);
			this._element = element;
			this._exception = exception;
		}

		/// <summary>Gets the composition element that is the cause of the error.</summary>
		/// <returns>The composition element that is the cause of the <see cref="T:System.ComponentModel.Composition.CompositionError" />. The default is <see langword="null" />.</returns>
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600013A RID: 314 RVA: 0x0000443D File Offset: 0x0000263D
		public ICompositionElement Element
		{
			get
			{
				return this._element;
			}
		}

		/// <summary>Gets a description of the composition error.</summary>
		/// <returns>A message that describes the <see cref="T:System.ComponentModel.Composition.CompositionError" />.</returns>
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00004445 File Offset: 0x00002645
		public string Description
		{
			get
			{
				return this._description;
			}
		}

		/// <summary>Gets the exception that is the underlying cause of the composition error.</summary>
		/// <returns>The exception that is the underlying cause of the <see cref="T:System.ComponentModel.Composition.CompositionError" />. The default is <see langword="null" />.</returns>
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000444D File Offset: 0x0000264D
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00004455 File Offset: 0x00002655
		internal CompositionErrorId Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000445D File Offset: 0x0000265D
		internal Exception InnerException
		{
			get
			{
				return this.Exception;
			}
		}

		/// <summary>Returns a string representation of the composition error.</summary>
		/// <returns>A string that contains the <see cref="P:System.ComponentModel.Composition.CompositionError.Description" /> property.</returns>
		// Token: 0x0600013F RID: 319 RVA: 0x00004465 File Offset: 0x00002665
		public override string ToString()
		{
			return this.Description;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000446D File Offset: 0x0000266D
		internal static CompositionError Create(CompositionErrorId id, string format, params object[] parameters)
		{
			return CompositionError.Create(id, null, null, format, parameters);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004479 File Offset: 0x00002679
		internal static CompositionError Create(CompositionErrorId id, ICompositionElement element, string format, params object[] parameters)
		{
			return CompositionError.Create(id, element, null, format, parameters);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004485 File Offset: 0x00002685
		internal static CompositionError Create(CompositionErrorId id, ICompositionElement element, Exception exception, string format, params object[] parameters)
		{
			return new CompositionError(id, string.Format(CultureInfo.CurrentCulture, format, parameters), element, exception);
		}

		// Token: 0x0400006C RID: 108
		private readonly CompositionErrorId _id;

		// Token: 0x0400006D RID: 109
		private readonly string _description;

		// Token: 0x0400006E RID: 110
		private readonly Exception _exception;

		// Token: 0x0400006F RID: 111
		private readonly ICompositionElement _element;
	}
}
