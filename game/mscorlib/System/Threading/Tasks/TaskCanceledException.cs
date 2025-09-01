using System;
using System.Runtime.Serialization;

namespace System.Threading.Tasks
{
	/// <summary>Represents an exception used to communicate task cancellation.</summary>
	// Token: 0x02000309 RID: 777
	[Serializable]
	public class TaskCanceledException : OperationCanceledException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskCanceledException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x06002167 RID: 8551 RVA: 0x00078461 File Offset: 0x00076661
		public TaskCanceledException() : base("A task was canceled.")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskCanceledException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x06002168 RID: 8552 RVA: 0x0007846E File Offset: 0x0007666E
		public TaskCanceledException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskCanceledException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06002169 RID: 8553 RVA: 0x00078477 File Offset: 0x00076677
		public TaskCanceledException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x00078481 File Offset: 0x00076681
		public TaskCanceledException(string message, Exception innerException, CancellationToken token) : base(message, innerException, token)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskCanceledException" /> class with a reference to the <see cref="T:System.Threading.Tasks.Task" /> that has been canceled.</summary>
		/// <param name="task">A task that has been canceled.</param>
		// Token: 0x0600216B RID: 8555 RVA: 0x0007848C File Offset: 0x0007668C
		public TaskCanceledException(Task task) : base("A task was canceled.", (task != null) ? task.CancellationToken : default(CancellationToken))
		{
			this._canceledTask = task;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskCanceledException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x0600216C RID: 8556 RVA: 0x000784BF File Offset: 0x000766BF
		protected TaskCanceledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Gets the task associated with this exception.</summary>
		/// <returns>A reference to the <see cref="T:System.Threading.Tasks.Task" /> that is associated with this exception.</returns>
		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x0600216D RID: 8557 RVA: 0x000784C9 File Offset: 0x000766C9
		public Task Task
		{
			get
			{
				return this._canceledTask;
			}
		}

		// Token: 0x04001BC7 RID: 7111
		[NonSerialized]
		private readonly Task _canceledTask;
	}
}
