using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Internal;
using Microsoft.Internal.Collections;
using Unity;

namespace System.ComponentModel.Composition
{
	/// <summary>Represents the exception that is thrown when one or more errors occur during composition in a <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object.</summary>
	// Token: 0x02000028 RID: 40
	[DebuggerDisplay("{Message}")]
	[DebuggerTypeProxy(typeof(CompositionExceptionDebuggerProxy))]
	[Serializable]
	public class CompositionException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.CompositionException" /> class.</summary>
		// Token: 0x06000147 RID: 327 RVA: 0x000044DD File Offset: 0x000026DD
		public CompositionException() : this(null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.CompositionException" /> class with the specified error message.</summary>
		/// <param name="message">A message that describes the <see cref="T:System.ComponentModel.Composition.CompositionException" /> or <see langword="null" /> to set the <see cref="P:System.Exception.Message" /> property to its default value.</param>
		// Token: 0x06000148 RID: 328 RVA: 0x000044E8 File Offset: 0x000026E8
		public CompositionException(string message) : this(message, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.CompositionException" /> class with the specified error message and the exception that is the cause of this exception.</summary>
		/// <param name="message">A message that describes the <see cref="T:System.ComponentModel.Composition.CompositionException" /> or <see langword="null" /> to set the <see cref="P:System.Exception.Message" /> property to its default value.</param>
		/// <param name="innerException">The exception that is the underlying cause of the <see cref="T:System.ComponentModel.Composition.CompositionException" /> or <see langword="null" /> to set the <see cref="P:System.Exception.InnerException" /> property to <see langword="null" />.</param>
		// Token: 0x06000149 RID: 329 RVA: 0x00004394 File Offset: 0x00002594
		public CompositionException(string message, Exception innerException) : this(message, innerException, null)
		{
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000044F3 File Offset: 0x000026F3
		internal CompositionException(CompositionError error) : this(new CompositionError[]
		{
			error
		})
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.CompositionException" /> class with the specified collection of composition errors.</summary>
		/// <param name="errors">A collection of <see cref="T:System.ComponentModel.Composition.CompositionError" /> objects that represent problems during composition.</param>
		// Token: 0x0600014B RID: 331 RVA: 0x0000439F File Offset: 0x0000259F
		public CompositionException(IEnumerable<CompositionError> errors) : this(null, null, errors)
		{
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004508 File Offset: 0x00002708
		internal CompositionException(string message, Exception innerException, IEnumerable<CompositionError> errors) : base(message, innerException)
		{
			Requires.NullOrNotNullElements<CompositionError>(errors, "errors");
			base.SerializeObjectState += delegate(object exception, SafeSerializationEventArgs eventArgs)
			{
				CompositionException.CompositionExceptionData compositionExceptionData = default(CompositionException.CompositionExceptionData);
				if (this._errors != null)
				{
					compositionExceptionData._errors = (from error in this._errors
					select new CompositionError(error.Id, error.Description, error.Element.ToSerializableElement(), error.Exception)).ToArray<CompositionError>();
				}
				else
				{
					compositionExceptionData._errors = new CompositionError[0];
				}
				eventArgs.AddSerializedState(compositionExceptionData);
			};
			this._errors = new ReadOnlyCollection<CompositionError>((errors == null) ? new CompositionError[0] : errors.ToArray<CompositionError>());
		}

		/// <summary>Gets or sets a collection of <see cref="T:System.ComponentModel.Composition.CompositionError" /> objects that describe the errors associated with the <see cref="T:System.ComponentModel.Composition.CompositionException" />.</summary>
		/// <returns>A collection of <see cref="T:System.ComponentModel.Composition.CompositionError" /> objects that describe the errors associated with the <see cref="T:System.ComponentModel.Composition.CompositionException" />.</returns>
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00004556 File Offset: 0x00002756
		public ReadOnlyCollection<CompositionError> Errors
		{
			get
			{
				return this._errors;
			}
		}

		/// <summary>Gets a message that describes the exception.</summary>
		/// <returns>A message that describes the <see cref="T:System.ComponentModel.Composition.CompositionException" />.</returns>
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600014E RID: 334 RVA: 0x0000455E File Offset: 0x0000275E
		public override string Message
		{
			get
			{
				if (this.Errors.Count == 0)
				{
					return base.Message;
				}
				return this.BuildDefaultMessage();
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000457C File Offset: 0x0000277C
		private string BuildDefaultMessage()
		{
			IEnumerable<IEnumerable<CompositionError>> enumerable = CompositionException.CalculatePaths(this);
			StringBuilder stringBuilder = new StringBuilder();
			CompositionException.WriteHeader(stringBuilder, this.Errors.Count, enumerable.Count<IEnumerable<CompositionError>>());
			CompositionException.WritePaths(stringBuilder, enumerable);
			return stringBuilder.ToString();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000045B8 File Offset: 0x000027B8
		private static void WriteHeader(StringBuilder writer, int errorsCount, int pathCount)
		{
			if (errorsCount > 1 && pathCount > 1)
			{
				writer.AppendFormat(CultureInfo.CurrentCulture, Strings.CompositionException_MultipleErrorsWithMultiplePaths, pathCount);
			}
			else if (errorsCount == 1 && pathCount > 1)
			{
				writer.AppendFormat(CultureInfo.CurrentCulture, Strings.CompositionException_SingleErrorWithMultiplePaths, pathCount);
			}
			else
			{
				Assumes.IsTrue(errorsCount == 1);
				Assumes.IsTrue(pathCount == 1);
				writer.AppendFormat(CultureInfo.CurrentCulture, Strings.CompositionException_SingleErrorWithSinglePath, pathCount);
			}
			writer.Append(' ');
			writer.AppendLine(Strings.CompositionException_ReviewErrorProperty);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00004648 File Offset: 0x00002848
		private static void WritePaths(StringBuilder writer, IEnumerable<IEnumerable<CompositionError>> paths)
		{
			int num = 0;
			foreach (IEnumerable<CompositionError> path in paths)
			{
				num++;
				CompositionException.WritePath(writer, path, num);
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00004698 File Offset: 0x00002898
		private static void WritePath(StringBuilder writer, IEnumerable<CompositionError> path, int ordinal)
		{
			writer.AppendLine();
			writer.Append(ordinal.ToString(CultureInfo.CurrentCulture));
			writer.Append(Strings.CompositionException_PathsCountSeparator);
			writer.Append(' ');
			CompositionException.WriteError(writer, path.First<CompositionError>());
			foreach (CompositionError error in path.Skip(1))
			{
				writer.AppendLine();
				writer.Append(Strings.CompositionException_ErrorPrefix);
				writer.Append(' ');
				CompositionException.WriteError(writer, error);
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00004740 File Offset: 0x00002940
		private static void WriteError(StringBuilder writer, CompositionError error)
		{
			writer.AppendLine(error.Description);
			if (error.Element != null)
			{
				CompositionException.WriteElementGraph(writer, error.Element);
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00004764 File Offset: 0x00002964
		private static void WriteElementGraph(StringBuilder writer, ICompositionElement element)
		{
			writer.AppendFormat(CultureInfo.CurrentCulture, Strings.CompositionException_ElementPrefix, element.DisplayName);
			while ((element = element.Origin) != null)
			{
				writer.AppendFormat(CultureInfo.CurrentCulture, Strings.CompositionException_OriginFormat, Strings.CompositionException_OriginSeparator, element.DisplayName);
			}
			writer.AppendLine();
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000047B8 File Offset: 0x000029B8
		private static IEnumerable<IEnumerable<CompositionError>> CalculatePaths(CompositionException exception)
		{
			List<IEnumerable<CompositionError>> paths = new List<IEnumerable<CompositionError>>();
			CompositionException.VisitCompositionException(exception, new CompositionException.VisitContext
			{
				Path = new Stack<CompositionError>(),
				LeafVisitor = delegate(Stack<CompositionError> path)
				{
					paths.Add(path.Copy<CompositionError>());
				}
			});
			return paths;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000480C File Offset: 0x00002A0C
		private static void VisitCompositionException(CompositionException exception, CompositionException.VisitContext context)
		{
			foreach (CompositionError error in exception.Errors)
			{
				CompositionException.VisitError(error, context);
			}
			if (exception.InnerException != null)
			{
				CompositionException.VisitException(exception.InnerException, context);
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000486C File Offset: 0x00002A6C
		private static void VisitError(CompositionError error, CompositionException.VisitContext context)
		{
			context.Path.Push(error);
			if (error.Exception == null)
			{
				context.LeafVisitor(context.Path);
			}
			else
			{
				CompositionException.VisitException(error.Exception, context);
			}
			context.Path.Pop();
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000048B8 File Offset: 0x00002AB8
		private static void VisitException(Exception exception, CompositionException.VisitContext context)
		{
			CompositionException ex = exception as CompositionException;
			if (ex != null)
			{
				CompositionException.VisitCompositionException(ex, context);
				return;
			}
			CompositionException.VisitError(new CompositionError(exception.Message, exception.InnerException), context);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000048F0 File Offset: 0x00002AF0
		[CompilerGenerated]
		private void <.ctor>b__8_0(object exception, SafeSerializationEventArgs eventArgs)
		{
			CompositionException.CompositionExceptionData compositionExceptionData = default(CompositionException.CompositionExceptionData);
			if (this._errors != null)
			{
				compositionExceptionData._errors = (from error in this._errors
				select new CompositionError(error.Id, error.Description, error.Element.ToSerializableElement(), error.Exception)).ToArray<CompositionError>();
			}
			else
			{
				compositionExceptionData._errors = new CompositionError[0];
			}
			eventArgs.AddSerializedState(compositionExceptionData);
		}

		/// <summary>Gets a collection that contains the initial sources of this exception.</summary>
		/// <returns>A collection that contains the initial sources of this exception.</returns>
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600015A RID: 346 RVA: 0x0000495E File Offset: 0x00002B5E
		public ReadOnlyCollection<Exception> RootCauses
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		// Token: 0x04000087 RID: 135
		private const string ErrorsKey = "Errors";

		// Token: 0x04000088 RID: 136
		private ReadOnlyCollection<CompositionError> _errors;

		// Token: 0x02000029 RID: 41
		[Serializable]
		private struct CompositionExceptionData : ISafeSerializationData
		{
			// Token: 0x0600015B RID: 347 RVA: 0x00004966 File Offset: 0x00002B66
			void ISafeSerializationData.CompleteDeserialization(object obj)
			{
				(obj as CompositionException)._errors = new ReadOnlyCollection<CompositionError>(this._errors);
			}

			// Token: 0x04000089 RID: 137
			public CompositionError[] _errors;
		}

		// Token: 0x0200002A RID: 42
		private struct VisitContext
		{
			// Token: 0x0400008A RID: 138
			public Stack<CompositionError> Path;

			// Token: 0x0400008B RID: 139
			public Action<Stack<CompositionError>> LeafVisitor;
		}

		// Token: 0x0200002B RID: 43
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600015C RID: 348 RVA: 0x0000497E File Offset: 0x00002B7E
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600015D RID: 349 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c()
			{
			}

			// Token: 0x0600015E RID: 350 RVA: 0x0000498A File Offset: 0x00002B8A
			internal CompositionError <.ctor>b__8_1(CompositionError error)
			{
				return new CompositionError(error.Id, error.Description, error.Element.ToSerializableElement(), error.Exception);
			}

			// Token: 0x0400008C RID: 140
			public static readonly CompositionException.<>c <>9 = new CompositionException.<>c();

			// Token: 0x0400008D RID: 141
			public static Func<CompositionError, CompositionError> <>9__8_1;
		}

		// Token: 0x0200002C RID: 44
		[CompilerGenerated]
		private sealed class <>c__DisplayClass19_0
		{
			// Token: 0x0600015F RID: 351 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass19_0()
			{
			}

			// Token: 0x06000160 RID: 352 RVA: 0x000049AE File Offset: 0x00002BAE
			internal void <CalculatePaths>b__0(Stack<CompositionError> path)
			{
				this.paths.Add(path.Copy<CompositionError>());
			}

			// Token: 0x0400008E RID: 142
			public List<IEnumerable<CompositionError>> paths;
		}
	}
}
