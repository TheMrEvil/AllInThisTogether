using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace System.Net
{
	// Token: 0x02000578 RID: 1400
	internal sealed class TrackingValidationObjectDictionary : StringDictionary
	{
		// Token: 0x06002D44 RID: 11588 RVA: 0x0009B217 File Offset: 0x00099417
		internal TrackingValidationObjectDictionary(Dictionary<string, TrackingValidationObjectDictionary.ValidateAndParseValue> validators)
		{
			this.IsChanged = false;
			this._validators = validators;
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x0009B230 File Offset: 0x00099430
		private void PersistValue(string key, string value, bool addValue)
		{
			key = key.ToLowerInvariant();
			if (!string.IsNullOrEmpty(value))
			{
				TrackingValidationObjectDictionary.ValidateAndParseValue validateAndParseValue;
				if (this._validators != null && this._validators.TryGetValue(key, out validateAndParseValue))
				{
					object obj = validateAndParseValue(value);
					if (this._internalObjects == null)
					{
						this._internalObjects = new Dictionary<string, object>();
					}
					if (addValue)
					{
						this._internalObjects.Add(key, obj);
						base.Add(key, obj.ToString());
					}
					else
					{
						this._internalObjects[key] = obj;
						base[key] = obj.ToString();
					}
				}
				else if (addValue)
				{
					base.Add(key, value);
				}
				else
				{
					base[key] = value;
				}
				this.IsChanged = true;
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06002D46 RID: 11590 RVA: 0x0009B2DA File Offset: 0x000994DA
		// (set) Token: 0x06002D47 RID: 11591 RVA: 0x0009B2E2 File Offset: 0x000994E2
		internal bool IsChanged
		{
			[CompilerGenerated]
			get
			{
				return this.<IsChanged>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsChanged>k__BackingField = value;
			}
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x0009B2EC File Offset: 0x000994EC
		internal object InternalGet(string key)
		{
			object result;
			if (this._internalObjects != null && this._internalObjects.TryGetValue(key, out result))
			{
				return result;
			}
			return base[key];
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x0009B31A File Offset: 0x0009951A
		internal void InternalSet(string key, object value)
		{
			if (this._internalObjects == null)
			{
				this._internalObjects = new Dictionary<string, object>();
			}
			this._internalObjects[key] = value;
			base[key] = value.ToString();
			this.IsChanged = true;
		}

		// Token: 0x17000919 RID: 2329
		public override string this[string key]
		{
			get
			{
				return base[key];
			}
			set
			{
				this.PersistValue(key, value, false);
			}
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x0009B35B File Offset: 0x0009955B
		public override void Add(string key, string value)
		{
			this.PersistValue(key, value, true);
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x0009B366 File Offset: 0x00099566
		public override void Clear()
		{
			if (this._internalObjects != null)
			{
				this._internalObjects.Clear();
			}
			base.Clear();
			this.IsChanged = true;
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x0009B388 File Offset: 0x00099588
		public override void Remove(string key)
		{
			if (this._internalObjects != null)
			{
				this._internalObjects.Remove(key);
			}
			base.Remove(key);
			this.IsChanged = true;
		}

		// Token: 0x0400189A RID: 6298
		private readonly Dictionary<string, TrackingValidationObjectDictionary.ValidateAndParseValue> _validators;

		// Token: 0x0400189B RID: 6299
		private Dictionary<string, object> _internalObjects;

		// Token: 0x0400189C RID: 6300
		[CompilerGenerated]
		private bool <IsChanged>k__BackingField;

		// Token: 0x02000579 RID: 1401
		// (Invoke) Token: 0x06002D50 RID: 11600
		internal delegate object ValidateAndParseValue(object valueToValidate);
	}
}
