using System;
using System.Collections.Generic;
using Unity;

namespace System.Runtime.Serialization
{
	/// <summary>Provides data for the <see cref="E:System.Exception.SerializeObjectState" /> event.</summary>
	// Token: 0x02000669 RID: 1641
	public sealed class SafeSerializationEventArgs : EventArgs
	{
		// Token: 0x06003D54 RID: 15700 RVA: 0x000D46FE File Offset: 0x000D28FE
		internal SafeSerializationEventArgs(StreamingContext streamingContext)
		{
			this.m_serializedStates = new List<object>();
			base..ctor();
			this.m_streamingContext = streamingContext;
		}

		/// <summary>Stores the state of the exception.</summary>
		/// <param name="serializedState">A state object that is serialized with the instance.</param>
		// Token: 0x06003D55 RID: 15701 RVA: 0x000D4718 File Offset: 0x000D2918
		public void AddSerializedState(ISafeSerializationData serializedState)
		{
			if (serializedState == null)
			{
				throw new ArgumentNullException("serializedState");
			}
			if (!serializedState.GetType().IsSerializable)
			{
				throw new ArgumentException(Environment.GetResourceString("Type '{0}' in Assembly '{1}' is not marked as serializable.", new object[]
				{
					serializedState.GetType(),
					serializedState.GetType().Assembly.FullName
				}));
			}
			this.m_serializedStates.Add(serializedState);
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06003D56 RID: 15702 RVA: 0x000D477E File Offset: 0x000D297E
		internal IList<object> SerializedStates
		{
			get
			{
				return this.m_serializedStates;
			}
		}

		/// <summary>Gets or sets an object that describes the source and destination of a serialized stream.</summary>
		/// <returns>An object that describes the source and destination of a serialized stream.</returns>
		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06003D57 RID: 15703 RVA: 0x000D4786 File Offset: 0x000D2986
		public StreamingContext StreamingContext
		{
			get
			{
				return this.m_streamingContext;
			}
		}

		// Token: 0x06003D58 RID: 15704 RVA: 0x000173AD File Offset: 0x000155AD
		internal SafeSerializationEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002779 RID: 10105
		private StreamingContext m_streamingContext;

		// Token: 0x0400277A RID: 10106
		private List<object> m_serializedStates;
	}
}
