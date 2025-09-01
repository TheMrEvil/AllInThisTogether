using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;

namespace System
{
	/// <summary>Represents one or more errors that occur during application execution.</summary>
	// Token: 0x020000F1 RID: 241
	[DebuggerDisplay("Count = {InnerExceptionCount}")]
	[Serializable]
	public class AggregateException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.AggregateException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x060006E6 RID: 1766 RVA: 0x00020A6F File Offset: 0x0001EC6F
		public AggregateException() : base("One or more errors occurred.")
		{
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(Array.Empty<Exception>());
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AggregateException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x060006E7 RID: 1767 RVA: 0x00020A8C File Offset: 0x0001EC8C
		public AggregateException(string message) : base(message)
		{
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(Array.Empty<Exception>());
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AggregateException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="innerException" /> argument is null.</exception>
		// Token: 0x060006E8 RID: 1768 RVA: 0x00020AA5 File Offset: 0x0001ECA5
		public AggregateException(string message, Exception innerException) : base(message, innerException)
		{
			if (innerException == null)
			{
				throw new ArgumentNullException("innerException");
			}
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(new Exception[]
			{
				innerException
			});
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AggregateException" /> class with references to the inner exceptions that are the cause of this exception.</summary>
		/// <param name="innerExceptions">The exceptions that are the cause of the current exception.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="innerExceptions" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="innerExceptions" /> is null.</exception>
		// Token: 0x060006E9 RID: 1769 RVA: 0x00020AD2 File Offset: 0x0001ECD2
		public AggregateException(IEnumerable<Exception> innerExceptions) : this("One or more errors occurred.", innerExceptions)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AggregateException" /> class with references to the inner exceptions that are the cause of this exception.</summary>
		/// <param name="innerExceptions">The exceptions that are the cause of the current exception.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="innerExceptions" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="innerExceptions" /> is null.</exception>
		// Token: 0x060006EA RID: 1770 RVA: 0x00020AE0 File Offset: 0x0001ECE0
		public AggregateException(params Exception[] innerExceptions) : this("One or more errors occurred.", innerExceptions)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AggregateException" /> class with a specified error message and references to the inner exceptions that are the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerExceptions">The exceptions that are the cause of the current exception.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="innerExceptions" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="innerExceptions" /> is null.</exception>
		// Token: 0x060006EB RID: 1771 RVA: 0x00020AEE File Offset: 0x0001ECEE
		public AggregateException(string message, IEnumerable<Exception> innerExceptions) : this(message, (innerExceptions as IList<Exception>) ?? ((innerExceptions == null) ? null : new List<Exception>(innerExceptions)))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AggregateException" /> class with a specified error message and references to the inner exceptions that are the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerExceptions">The exceptions that are the cause of the current exception.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="innerExceptions" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="innerExceptions" /> is null.</exception>
		// Token: 0x060006EC RID: 1772 RVA: 0x00020B0D File Offset: 0x0001ED0D
		public AggregateException(string message, params Exception[] innerExceptions) : this(message, innerExceptions)
		{
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00020B18 File Offset: 0x0001ED18
		private AggregateException(string message, IList<Exception> innerExceptions) : base(message, (innerExceptions != null && innerExceptions.Count > 0) ? innerExceptions[0] : null)
		{
			if (innerExceptions == null)
			{
				throw new ArgumentNullException("innerExceptions");
			}
			Exception[] array = new Exception[innerExceptions.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = innerExceptions[i];
				if (array[i] == null)
				{
					throw new ArgumentException("An element of innerExceptions was null.");
				}
			}
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(array);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00020B90 File Offset: 0x0001ED90
		internal AggregateException(IEnumerable<ExceptionDispatchInfo> innerExceptionInfos) : this("One or more errors occurred.", innerExceptionInfos)
		{
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00020B9E File Offset: 0x0001ED9E
		internal AggregateException(string message, IEnumerable<ExceptionDispatchInfo> innerExceptionInfos) : this(message, (innerExceptionInfos as IList<ExceptionDispatchInfo>) ?? ((innerExceptionInfos == null) ? null : new List<ExceptionDispatchInfo>(innerExceptionInfos)))
		{
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00020BC0 File Offset: 0x0001EDC0
		private AggregateException(string message, IList<ExceptionDispatchInfo> innerExceptionInfos) : base(message, (innerExceptionInfos != null && innerExceptionInfos.Count > 0 && innerExceptionInfos[0] != null) ? innerExceptionInfos[0].SourceException : null)
		{
			if (innerExceptionInfos == null)
			{
				throw new ArgumentNullException("innerExceptionInfos");
			}
			Exception[] array = new Exception[innerExceptionInfos.Count];
			for (int i = 0; i < array.Length; i++)
			{
				ExceptionDispatchInfo exceptionDispatchInfo = innerExceptionInfos[i];
				if (exceptionDispatchInfo != null)
				{
					array[i] = exceptionDispatchInfo.SourceException;
				}
				if (array[i] == null)
				{
					throw new ArgumentException("An element of innerExceptions was null.");
				}
			}
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(array);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AggregateException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> argument is null.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The exception could not be deserialized correctly.</exception>
		// Token: 0x060006F1 RID: 1777 RVA: 0x00020C50 File Offset: 0x0001EE50
		protected AggregateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			Exception[] array = info.GetValue("InnerExceptions", typeof(Exception[])) as Exception[];
			if (array == null)
			{
				throw new SerializationException("The serialization stream contains no inner exceptions.");
			}
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(array);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AggregateException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> argument is null.</exception>
		// Token: 0x060006F2 RID: 1778 RVA: 0x00020CA8 File Offset: 0x0001EEA8
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			Exception[] array = new Exception[this.m_innerExceptions.Count];
			this.m_innerExceptions.CopyTo(array, 0);
			info.AddValue("InnerExceptions", array, typeof(Exception[]));
		}

		/// <summary>Returns the <see cref="T:System.AggregateException" /> that is the root cause of this exception.</summary>
		/// <returns>The <see cref="T:System.AggregateException" /> that is the root cause of this exception.</returns>
		// Token: 0x060006F3 RID: 1779 RVA: 0x00020CF4 File Offset: 0x0001EEF4
		public override Exception GetBaseException()
		{
			Exception ex = this;
			AggregateException ex2 = this;
			while (ex2 != null && ex2.InnerExceptions.Count == 1)
			{
				ex = ex.InnerException;
				ex2 = (ex as AggregateException);
			}
			return ex;
		}

		/// <summary>Gets a read-only collection of the <see cref="T:System.Exception" /> instances that caused the current exception.</summary>
		/// <returns>A read-only collection of the <see cref="T:System.Exception" /> instances that caused the current exception.</returns>
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x00020D27 File Offset: 0x0001EF27
		public ReadOnlyCollection<Exception> InnerExceptions
		{
			get
			{
				return this.m_innerExceptions;
			}
		}

		/// <summary>Invokes a handler on each <see cref="T:System.Exception" /> contained by this <see cref="T:System.AggregateException" />.</summary>
		/// <param name="predicate">The predicate to execute for each exception. The predicate accepts as an argument the <see cref="T:System.Exception" /> to be processed and returns a Boolean to indicate whether the exception was handled.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="predicate" /> argument is null.</exception>
		/// <exception cref="T:System.AggregateException">An exception contained by this <see cref="T:System.AggregateException" /> was not handled.</exception>
		// Token: 0x060006F5 RID: 1781 RVA: 0x00020D30 File Offset: 0x0001EF30
		public void Handle(Func<Exception, bool> predicate)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			List<Exception> list = null;
			for (int i = 0; i < this.m_innerExceptions.Count; i++)
			{
				if (!predicate(this.m_innerExceptions[i]))
				{
					if (list == null)
					{
						list = new List<Exception>();
					}
					list.Add(this.m_innerExceptions[i]);
				}
			}
			if (list != null)
			{
				throw new AggregateException(this.Message, list);
			}
		}

		/// <summary>Flattens an <see cref="T:System.AggregateException" /> instances into a single, new instance.</summary>
		/// <returns>A new, flattened <see cref="T:System.AggregateException" />.</returns>
		// Token: 0x060006F6 RID: 1782 RVA: 0x00020DA4 File Offset: 0x0001EFA4
		public AggregateException Flatten()
		{
			List<Exception> list = new List<Exception>();
			List<AggregateException> list2 = new List<AggregateException>();
			list2.Add(this);
			int num = 0;
			while (list2.Count > num)
			{
				IList<Exception> innerExceptions = list2[num++].InnerExceptions;
				for (int i = 0; i < innerExceptions.Count; i++)
				{
					Exception ex = innerExceptions[i];
					if (ex != null)
					{
						AggregateException ex2 = ex as AggregateException;
						if (ex2 != null)
						{
							list2.Add(ex2);
						}
						else
						{
							list.Add(ex);
						}
					}
				}
			}
			return new AggregateException(this.Message, list);
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x00020E30 File Offset: 0x0001F030
		public override string Message
		{
			get
			{
				if (this.m_innerExceptions.Count == 0)
				{
					return base.Message;
				}
				StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
				stringBuilder.Append(base.Message);
				stringBuilder.Append(' ');
				for (int i = 0; i < this.m_innerExceptions.Count; i++)
				{
					stringBuilder.Append('(');
					stringBuilder.Append(this.m_innerExceptions[i].Message);
					stringBuilder.Append(") ");
				}
				stringBuilder.Length--;
				return StringBuilderCache.GetStringAndRelease(stringBuilder);
			}
		}

		/// <summary>Creates and returns a string representation of the current <see cref="T:System.AggregateException" />.</summary>
		/// <returns>A string representation of the current exception.</returns>
		// Token: 0x060006F8 RID: 1784 RVA: 0x00020EC8 File Offset: 0x0001F0C8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.ToString());
			for (int i = 0; i < this.m_innerExceptions.Count; i++)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append("---> ");
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "(Inner Exception #{0}) ", i);
				stringBuilder.Append(this.m_innerExceptions[i].ToString());
				stringBuilder.Append("<---");
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x00020F59 File Offset: 0x0001F159
		private int InnerExceptionCount
		{
			get
			{
				return this.InnerExceptions.Count;
			}
		}

		// Token: 0x04001047 RID: 4167
		private ReadOnlyCollection<Exception> m_innerExceptions;
	}
}
