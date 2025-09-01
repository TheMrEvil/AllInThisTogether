using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Provides the default implementations of the <see cref="T:System.Runtime.Remoting.Contexts.IContextAttribute" /> and <see cref="T:System.Runtime.Remoting.Contexts.IContextProperty" /> interfaces.</summary>
	// Token: 0x0200058F RID: 1423
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(true)]
	[Serializable]
	public class ContextAttribute : Attribute, IContextAttribute, IContextProperty
	{
		/// <summary>Creates an instance of the <see cref="T:System.Runtime.Remoting.Contexts.ContextAttribute" /> class with the specified name.</summary>
		/// <param name="name">The name of the context attribute.</param>
		// Token: 0x060037B7 RID: 14263 RVA: 0x000C8A24 File Offset: 0x000C6C24
		public ContextAttribute(string name)
		{
			this.AttributeName = name;
		}

		/// <summary>Gets the name of the context attribute.</summary>
		/// <returns>The name of the context attribute.</returns>
		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x060037B8 RID: 14264 RVA: 0x000C8A33 File Offset: 0x000C6C33
		public virtual string Name
		{
			[SecurityCritical]
			get
			{
				return this.AttributeName;
			}
		}

		/// <summary>Returns a Boolean value indicating whether this instance is equal to the specified object.</summary>
		/// <param name="o">The object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="o" /> is not <see langword="null" /> and if the object names are equivalent; otherwise, <see langword="false" />.</returns>
		// Token: 0x060037B9 RID: 14265 RVA: 0x000C8A3B File Offset: 0x000C6C3B
		public override bool Equals(object o)
		{
			return o != null && o is ContextAttribute && !(((ContextAttribute)o).AttributeName != this.AttributeName);
		}

		/// <summary>Called when the context is frozen.</summary>
		/// <param name="newContext">The context to freeze.</param>
		// Token: 0x060037BA RID: 14266 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[SecurityCritical]
		public virtual void Freeze(Context newContext)
		{
		}

		/// <summary>Returns the hashcode for this instance of <see cref="T:System.Runtime.Remoting.Contexts.ContextAttribute" />.</summary>
		/// <returns>The hashcode for this instance of <see cref="T:System.Runtime.Remoting.Contexts.ContextAttribute" />.</returns>
		// Token: 0x060037BB RID: 14267 RVA: 0x000C8A67 File Offset: 0x000C6C67
		public override int GetHashCode()
		{
			if (this.AttributeName == null)
			{
				return 0;
			}
			return this.AttributeName.GetHashCode();
		}

		/// <summary>Adds the current context property to the given message.</summary>
		/// <param name="ctorMsg">The <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> to which to add the context property.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="ctorMsg" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060037BC RID: 14268 RVA: 0x000C8A7E File Offset: 0x000C6C7E
		[SecurityCritical]
		public virtual void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
			if (ctorMsg == null)
			{
				throw new ArgumentNullException("ctorMsg");
			}
			ctorMsg.ContextProperties.Add(this);
		}

		/// <summary>Returns a Boolean value indicating whether the context parameter meets the context attribute's requirements.</summary>
		/// <param name="ctx">The context in which to check.</param>
		/// <param name="ctorMsg">The <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> to which to add the context property.</param>
		/// <returns>
		///   <see langword="true" /> if the passed in context is okay; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">Either <paramref name="ctx" /> or <paramref name="ctorMsg" /> is <see langword="null" />.</exception>
		// Token: 0x060037BD RID: 14269 RVA: 0x000C8A9C File Offset: 0x000C6C9C
		[SecurityCritical]
		public virtual bool IsContextOK(Context ctx, IConstructionCallMessage ctorMsg)
		{
			if (ctorMsg == null)
			{
				throw new ArgumentNullException("ctorMsg");
			}
			if (ctx == null)
			{
				throw new ArgumentNullException("ctx");
			}
			if (!ctorMsg.ActivationType.IsContextful)
			{
				return true;
			}
			IContextProperty property = ctx.GetProperty(this.AttributeName);
			return property != null && this == property;
		}

		/// <summary>Returns a Boolean value indicating whether the context property is compatible with the new context.</summary>
		/// <param name="newCtx">The new context in which the property has been created.</param>
		/// <returns>
		///   <see langword="true" /> if the context property is okay with the new context; otherwise, <see langword="false" />.</returns>
		// Token: 0x060037BE RID: 14270 RVA: 0x000040F7 File Offset: 0x000022F7
		[SecurityCritical]
		public virtual bool IsNewContextOK(Context newCtx)
		{
			return true;
		}

		/// <summary>Indicates the name of the context attribute.</summary>
		// Token: 0x040025B1 RID: 9649
		protected string AttributeName;
	}
}
