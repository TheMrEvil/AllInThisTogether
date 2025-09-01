using System;

namespace System.Threading.Tasks
{
	/// <summary>Provides a set of static (Shared in Visual Basic) methods for working with specific kinds of <see cref="T:System.Threading.Tasks.Task" /> instances.</summary>
	// Token: 0x02000366 RID: 870
	public static class TaskExtensions
	{
		/// <summary>Creates a proxy <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation of a <see cref="M:System.Threading.Tasks.TaskScheduler.TryExecuteTaskInline(System.Threading.Tasks.Task,System.Boolean)" />.</summary>
		/// <param name="task">The <see langword="Task&lt;Task&gt;" /> (C#) or <see langword="Task (Of Task)" /> (Visual Basic) to unwrap.</param>
		/// <returns>A Task that represents the asynchronous operation of the provided <see langword="System.Threading.Tasks.Task(Of Task)" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown if the <paramref name="task" /> argument is null.</exception>
		// Token: 0x06001AA5 RID: 6821 RVA: 0x00059FF0 File Offset: 0x000581F0
		public static Task Unwrap(this Task<Task> task)
		{
			if (task == null)
			{
				throw new ArgumentNullException("task");
			}
			Task result;
			if (task.IsCompletedSuccessfully)
			{
				if ((result = task.Result) == null)
				{
					return Task.FromCanceled(new CancellationToken(true));
				}
			}
			else
			{
				result = Task.CreateUnwrapPromise<VoidTaskResult>(task, false);
			}
			return result;
		}

		/// <summary>Creates a proxy <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation of a <see langword="Task&lt;Task&lt;T&gt;&gt;" /> (C#) or <see langword="Task (Of Task(Of T))" /> (Visual Basic).</summary>
		/// <param name="task">The <see langword="Task&lt;Task&lt;T&gt;&gt;" /> (C#) or <see langword="Task (Of Task(Of T))" /> (Visual Basic) to unwrap.</param>
		/// <typeparam name="TResult">The type of the task's result.</typeparam>
		/// <returns>A <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation of the provided <see langword="Task&lt;Task&lt;T&gt;&gt;" /> (C#) or <see langword="Task (Of Task(Of T))" /> (Visual Basic).</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown if the <paramref name="task" /> argument is null.</exception>
		// Token: 0x06001AA6 RID: 6822 RVA: 0x0005A025 File Offset: 0x00058225
		public static Task<TResult> Unwrap<TResult>(this Task<Task<TResult>> task)
		{
			if (task == null)
			{
				throw new ArgumentNullException("task");
			}
			Task<TResult> result;
			if (task.IsCompletedSuccessfully)
			{
				if ((result = task.Result) == null)
				{
					return Task.FromCanceled<TResult>(new CancellationToken(true));
				}
			}
			else
			{
				result = Task.CreateUnwrapPromise<TResult>(task, false);
			}
			return result;
		}
	}
}
