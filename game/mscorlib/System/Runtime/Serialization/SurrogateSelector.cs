using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization
{
	/// <summary>Assists formatters in selection of the serialization surrogate to delegate the serialization or deserialization process to.</summary>
	// Token: 0x02000675 RID: 1653
	[ComVisible(true)]
	public class SurrogateSelector : ISurrogateSelector
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.SurrogateSelector" /> class.</summary>
		// Token: 0x06003DBB RID: 15803 RVA: 0x000D56B5 File Offset: 0x000D38B5
		public SurrogateSelector()
		{
			this.m_surrogates = new SurrogateHashtable(32);
		}

		/// <summary>Adds a surrogate to the list of checked surrogates.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> for which the surrogate is required.</param>
		/// <param name="context">The context-specific data.</param>
		/// <param name="surrogate">The surrogate to call for this type.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> or <paramref name="surrogate" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">A surrogate already exists for this type and context.</exception>
		// Token: 0x06003DBC RID: 15804 RVA: 0x000D56CC File Offset: 0x000D38CC
		public virtual void AddSurrogate(Type type, StreamingContext context, ISerializationSurrogate surrogate)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (surrogate == null)
			{
				throw new ArgumentNullException("surrogate");
			}
			SurrogateKey key = new SurrogateKey(type, context);
			this.m_surrogates.Add(key, surrogate);
		}

		// Token: 0x06003DBD RID: 15805 RVA: 0x000D5710 File Offset: 0x000D3910
		[SecurityCritical]
		private static bool HasCycle(ISurrogateSelector selector)
		{
			ISurrogateSelector surrogateSelector = selector;
			ISurrogateSelector surrogateSelector2 = selector;
			while (surrogateSelector != null)
			{
				surrogateSelector = surrogateSelector.GetNextSelector();
				if (surrogateSelector == null)
				{
					return true;
				}
				if (surrogateSelector == surrogateSelector2)
				{
					return false;
				}
				surrogateSelector = surrogateSelector.GetNextSelector();
				surrogateSelector2 = surrogateSelector2.GetNextSelector();
				if (surrogateSelector == surrogateSelector2)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Adds the specified <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> that can handle a particular object type to the list of surrogates.</summary>
		/// <param name="selector">The surrogate selector to add.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="selector" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The selector is already on the list of selectors.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06003DBE RID: 15806 RVA: 0x000D5750 File Offset: 0x000D3950
		[SecurityCritical]
		public virtual void ChainSelector(ISurrogateSelector selector)
		{
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			if (selector == this)
			{
				throw new SerializationException(Environment.GetResourceString("Selector is already on the list of checked selectors."));
			}
			if (!SurrogateSelector.HasCycle(selector))
			{
				throw new ArgumentException(Environment.GetResourceString("Selector contained a cycle."), "selector");
			}
			ISurrogateSelector surrogateSelector = selector.GetNextSelector();
			ISurrogateSelector surrogateSelector2 = selector;
			while (surrogateSelector != null && surrogateSelector != this)
			{
				surrogateSelector2 = surrogateSelector;
				surrogateSelector = surrogateSelector.GetNextSelector();
			}
			if (surrogateSelector == this)
			{
				throw new ArgumentException(Environment.GetResourceString("Adding selector will introduce a cycle."), "selector");
			}
			surrogateSelector = selector;
			ISurrogateSelector surrogateSelector3 = selector;
			while (surrogateSelector != null)
			{
				if (surrogateSelector == surrogateSelector2)
				{
					surrogateSelector = this.GetNextSelector();
				}
				else
				{
					surrogateSelector = surrogateSelector.GetNextSelector();
				}
				if (surrogateSelector == null)
				{
					break;
				}
				if (surrogateSelector == surrogateSelector3)
				{
					throw new ArgumentException(Environment.GetResourceString("Adding selector will introduce a cycle."), "selector");
				}
				if (surrogateSelector == surrogateSelector2)
				{
					surrogateSelector = this.GetNextSelector();
				}
				else
				{
					surrogateSelector = surrogateSelector.GetNextSelector();
				}
				if (surrogateSelector3 == surrogateSelector2)
				{
					surrogateSelector3 = this.GetNextSelector();
				}
				else
				{
					surrogateSelector3 = surrogateSelector3.GetNextSelector();
				}
				if (surrogateSelector == surrogateSelector3)
				{
					throw new ArgumentException(Environment.GetResourceString("Adding selector will introduce a cycle."), "selector");
				}
			}
			ISurrogateSelector nextSelector = this.m_nextSelector;
			this.m_nextSelector = selector;
			if (nextSelector != null)
			{
				surrogateSelector2.ChainSelector(nextSelector);
			}
		}

		/// <summary>Returns the next selector on the chain of selectors.</summary>
		/// <returns>The next <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> on the chain of selectors.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06003DBF RID: 15807 RVA: 0x000D5862 File Offset: 0x000D3A62
		[SecurityCritical]
		public virtual ISurrogateSelector GetNextSelector()
		{
			return this.m_nextSelector;
		}

		/// <summary>Returns the surrogate for a particular type.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> for which the surrogate is requested.</param>
		/// <param name="context">The streaming context.</param>
		/// <param name="selector">The surrogate to use.</param>
		/// <returns>The surrogate for a particular type.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06003DC0 RID: 15808 RVA: 0x000D586C File Offset: 0x000D3A6C
		[SecurityCritical]
		public virtual ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			selector = this;
			SurrogateKey key = new SurrogateKey(type, context);
			ISerializationSurrogate serializationSurrogate = (ISerializationSurrogate)this.m_surrogates[key];
			if (serializationSurrogate != null)
			{
				return serializationSurrogate;
			}
			if (this.m_nextSelector != null)
			{
				return this.m_nextSelector.GetSurrogate(type, context, out selector);
			}
			return null;
		}

		/// <summary>Removes the surrogate associated with a given type.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> for which to remove the surrogate.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> for the current surrogate.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003DC1 RID: 15809 RVA: 0x000D58C8 File Offset: 0x000D3AC8
		public virtual void RemoveSurrogate(Type type, StreamingContext context)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			SurrogateKey key = new SurrogateKey(type, context);
			this.m_surrogates.Remove(key);
		}

		// Token: 0x040027A1 RID: 10145
		internal SurrogateHashtable m_surrogates;

		// Token: 0x040027A2 RID: 10146
		internal ISurrogateSelector m_nextSelector;
	}
}
