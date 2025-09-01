using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Security;
using Microsoft.Internal.Runtime.Serialization;

namespace System.ComponentModel.Composition.Primitives
{
	/// <summary>The exception that is thrown when an error occurs when calling methods on a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> object.</summary>
	// Token: 0x0200008D RID: 141
	[DebuggerTypeProxy(typeof(ComposablePartExceptionDebuggerProxy))]
	[DebuggerDisplay("{Message}")]
	[Serializable]
	public class ComposablePartException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" /> class.</summary>
		// Token: 0x060003BB RID: 955 RVA: 0x0000AD6B File Offset: 0x00008F6B
		public ComposablePartException() : this(null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" /> class with the specified error message.</summary>
		/// <param name="message">A message that describes the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" />, or <see langword="null" /> to set the <see cref="P:System.Exception.Message" /> property to its default value.</param>
		// Token: 0x060003BC RID: 956 RVA: 0x0000AD76 File Offset: 0x00008F76
		public ComposablePartException(string message) : this(message, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" /> class with the specified error message and the composition element that is the cause of the exception.</summary>
		/// <param name="message">A message that describes the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" />, or <see langword="null" /> to set the <see cref="P:System.Exception.Message" /> property to its default value.</param>
		/// <param name="element">The composition element that is the cause of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" />, or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.Primitives.ComposablePartException.Element" /> property to <see langword="null" />.</param>
		// Token: 0x060003BD RID: 957 RVA: 0x0000AD81 File Offset: 0x00008F81
		public ComposablePartException(string message, ICompositionElement element) : this(message, element, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" /> class with the specified error message and the exception that is the cause of this exception.</summary>
		/// <param name="message">A message that describes the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" />, or <see langword="null" /> to set the <see cref="P:System.Exception.Message" /> property to its default value.</param>
		/// <param name="innerException">The exception that is the underlying cause of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" />, or <see langword="null" /> to set the <see cref="P:System.Exception.InnerException" /> property to <see langword="null" />.</param>
		// Token: 0x060003BE RID: 958 RVA: 0x0000AD8C File Offset: 0x00008F8C
		public ComposablePartException(string message, Exception innerException) : this(message, null, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" /> class with the specified error message, and the composition element and exception that are the cause of this exception.</summary>
		/// <param name="message">A message that describes the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" />, or <see langword="null" /> to set the <see cref="P:System.Exception.Message" /> property to its default value.</param>
		/// <param name="element">The composition element that is the cause of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" />, or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.Primitives.ComposablePartException.Element" /> property to <see langword="null" />.</param>
		/// <param name="innerException">The exception that is the underlying cause of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" />, or <see langword="null" /> to set the <see cref="P:System.Exception.InnerException" /> property to <see langword="null" />.</param>
		// Token: 0x060003BF RID: 959 RVA: 0x0000AD97 File Offset: 0x00008F97
		public ComposablePartException(string message, ICompositionElement element, Exception innerException) : base(message, innerException)
		{
			this._element = element;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" /> class with the specified serialization data.</summary>
		/// <param name="info">An object that holds the serialized object data for the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" />.</param>
		/// <param name="context">An object that contains contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">
		///   <paramref name="info" /> is missing a required value.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="info" /> contains a value that cannot be cast to the correct type.</exception>
		// Token: 0x060003C0 RID: 960 RVA: 0x0000ADA8 File Offset: 0x00008FA8
		[SecuritySafeCritical]
		protected ComposablePartException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._element = info.GetValue("Element");
		}

		/// <summary>Gets the composition element that is the cause of the exception.</summary>
		/// <returns>The compositional element that is the cause of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" />. The default is <see langword="null" />.</returns>
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000ADC3 File Offset: 0x00008FC3
		public ICompositionElement Element
		{
			get
			{
				return this._element;
			}
		}

		/// <summary>Gets the serialization data for the exception.</summary>
		/// <param name="info">After calling the method, contains serialized object data about the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" />.</param>
		/// <param name="context">After calling the method, contains contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x060003C2 RID: 962 RVA: 0x0000ADCB File Offset: 0x00008FCB
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("Element", this._element.ToSerializableElement());
		}

		// Token: 0x04000177 RID: 375
		private readonly ICompositionElement _element;
	}
}
