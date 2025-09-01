using System;
using System.Collections.Generic;

namespace System.Runtime.Serialization
{
	/// <summary>Manages serialization processes at run time. This class cannot be inherited.</summary>
	// Token: 0x02000659 RID: 1625
	public sealed class SerializationObjectManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.SerializationObjectManager" /> class.</summary>
		/// <param name="context">An instance of the <see cref="T:System.Runtime.Serialization.StreamingContext" /> class that contains information about the current serialization operation.</param>
		// Token: 0x06003CB4 RID: 15540 RVA: 0x000D1D08 File Offset: 0x000CFF08
		public SerializationObjectManager(StreamingContext context)
		{
			this._context = context;
			this._objectSeenTable = new Dictionary<object, object>();
		}

		/// <summary>Registers the object upon which events will be raised.</summary>
		/// <param name="obj">The object to register.</param>
		// Token: 0x06003CB5 RID: 15541 RVA: 0x000D1D24 File Offset: 0x000CFF24
		public void RegisterObject(object obj)
		{
			SerializationEvents serializationEventsForType = SerializationEventsCache.GetSerializationEventsForType(obj.GetType());
			if (serializationEventsForType.HasOnSerializingEvents && this._objectSeenTable.TryAdd(obj, true))
			{
				serializationEventsForType.InvokeOnSerializing(obj, this._context);
				this.AddOnSerialized(obj);
			}
		}

		/// <summary>Invokes the OnSerializing callback event if the type of the object has one; and registers the object for raising the OnSerialized event if the type of the object has one.</summary>
		// Token: 0x06003CB6 RID: 15542 RVA: 0x000D1D6D File Offset: 0x000CFF6D
		public void RaiseOnSerializedEvent()
		{
			SerializationEventHandler onSerializedHandler = this._onSerializedHandler;
			if (onSerializedHandler == null)
			{
				return;
			}
			onSerializedHandler(this._context);
		}

		// Token: 0x06003CB7 RID: 15543 RVA: 0x000D1D88 File Offset: 0x000CFF88
		private void AddOnSerialized(object obj)
		{
			SerializationEvents serializationEventsForType = SerializationEventsCache.GetSerializationEventsForType(obj.GetType());
			this._onSerializedHandler = serializationEventsForType.AddOnSerialized(obj, this._onSerializedHandler);
		}

		// Token: 0x0400272B RID: 10027
		private readonly Dictionary<object, object> _objectSeenTable;

		// Token: 0x0400272C RID: 10028
		private readonly StreamingContext _context;

		// Token: 0x0400272D RID: 10029
		private SerializationEventHandler _onSerializedHandler;
	}
}
