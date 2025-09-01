using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a simple implementation of the <see cref="T:System.ComponentModel.Design.IServiceContainer" /> interface. This class cannot be inherited.</summary>
	// Token: 0x02000472 RID: 1138
	public class ServiceContainer : IServiceContainer, IServiceProvider, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ServiceContainer" /> class.</summary>
		// Token: 0x0600249E RID: 9374 RVA: 0x0000219B File Offset: 0x0000039B
		public ServiceContainer()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ServiceContainer" /> class using the specified parent service provider.</summary>
		/// <param name="parentProvider">A parent service provider.</param>
		// Token: 0x0600249F RID: 9375 RVA: 0x00081AF9 File Offset: 0x0007FCF9
		public ServiceContainer(IServiceProvider parentProvider)
		{
			this._parentProvider = parentProvider;
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x060024A0 RID: 9376 RVA: 0x00081B08 File Offset: 0x0007FD08
		private IServiceContainer Container
		{
			get
			{
				IServiceContainer result = null;
				if (this._parentProvider != null)
				{
					result = (IServiceContainer)this._parentProvider.GetService(typeof(IServiceContainer));
				}
				return result;
			}
		}

		/// <summary>Gets the default services implemented directly by <see cref="T:System.ComponentModel.Design.ServiceContainer" />.</summary>
		/// <returns>The default services.</returns>
		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x060024A1 RID: 9377 RVA: 0x00081B3B File Offset: 0x0007FD3B
		protected virtual Type[] DefaultServices
		{
			get
			{
				return ServiceContainer.s_defaultServices;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x060024A2 RID: 9378 RVA: 0x00081B44 File Offset: 0x0007FD44
		private ServiceContainer.ServiceCollection<object> Services
		{
			get
			{
				ServiceContainer.ServiceCollection<object> result;
				if ((result = this._services) == null)
				{
					result = (this._services = new ServiceContainer.ServiceCollection<object>());
				}
				return result;
			}
		}

		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add.</param>
		/// <param name="serviceInstance">An instance of the service to add. This object must implement or inherit from the type indicated by the <paramref name="serviceType" /> parameter.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> or <paramref name="serviceInstance" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">A service of type <paramref name="serviceType" /> already exists in the container.</exception>
		// Token: 0x060024A3 RID: 9379 RVA: 0x00081B69 File Offset: 0x0007FD69
		public void AddService(Type serviceType, object serviceInstance)
		{
			this.AddService(serviceType, serviceInstance, false);
		}

		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add.</param>
		/// <param name="serviceInstance">An instance of the service type to add. This object must implement or inherit from the type indicated by the <paramref name="serviceType" /> parameter.</param>
		/// <param name="promote">
		///   <see langword="true" /> if this service should be added to any parent service containers; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> or <paramref name="serviceInstance" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">A service of type <paramref name="serviceType" /> already exists in the container.</exception>
		// Token: 0x060024A4 RID: 9380 RVA: 0x00081B74 File Offset: 0x0007FD74
		public virtual void AddService(Type serviceType, object serviceInstance, bool promote)
		{
			if (promote)
			{
				IServiceContainer container = this.Container;
				if (container != null)
				{
					container.AddService(serviceType, serviceInstance, promote);
					return;
				}
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			if (serviceInstance == null)
			{
				throw new ArgumentNullException("serviceInstance");
			}
			if (!(serviceInstance is ServiceCreatorCallback) && !serviceInstance.GetType().IsCOMObject && !serviceType.IsInstanceOfType(serviceInstance))
			{
				throw new ArgumentException(SR.Format("The service instance must derive from or implement {0}.", serviceType.FullName));
			}
			if (this.Services.ContainsKey(serviceType))
			{
				throw new ArgumentException(SR.Format("The service {0} already exists in the service container.", serviceType.FullName), "serviceType");
			}
			this.Services[serviceType] = serviceInstance;
		}

		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add.</param>
		/// <param name="callback">A callback object that can create the service. This allows a service to be declared as available, but delays creation of the object until the service is requested.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> or <paramref name="callback" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">A service of type <paramref name="serviceType" /> already exists in the container.</exception>
		// Token: 0x060024A5 RID: 9381 RVA: 0x00081C24 File Offset: 0x0007FE24
		public void AddService(Type serviceType, ServiceCreatorCallback callback)
		{
			this.AddService(serviceType, callback, false);
		}

		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add.</param>
		/// <param name="callback">A callback object that can create the service. This allows a service to be declared as available, but delays creation of the object until the service is requested.</param>
		/// <param name="promote">
		///   <see langword="true" /> if this service should be added to any parent service containers; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> or <paramref name="callback" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">A service of type <paramref name="serviceType" /> already exists in the container.</exception>
		// Token: 0x060024A6 RID: 9382 RVA: 0x00081C30 File Offset: 0x0007FE30
		public virtual void AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
		{
			if (promote)
			{
				IServiceContainer container = this.Container;
				if (container != null)
				{
					container.AddService(serviceType, callback, promote);
					return;
				}
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			if (this.Services.ContainsKey(serviceType))
			{
				throw new ArgumentException(SR.Format("The service {0} already exists in the service container.", serviceType.FullName), "serviceType");
			}
			this.Services[serviceType] = callback;
		}

		/// <summary>Disposes this service container.</summary>
		// Token: 0x060024A7 RID: 9383 RVA: 0x00081CAC File Offset: 0x0007FEAC
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Disposes this service container.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> if the <see cref="T:System.ComponentModel.Design.ServiceContainer" /> is in the process of being disposed of; otherwise, <see langword="false" />.</param>
		// Token: 0x060024A8 RID: 9384 RVA: 0x00081CB8 File Offset: 0x0007FEB8
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				ServiceContainer.ServiceCollection<object> services = this._services;
				this._services = null;
				if (services != null)
				{
					foreach (object obj in services.Values)
					{
						if (obj is IDisposable)
						{
							((IDisposable)obj).Dispose();
						}
					}
				}
			}
		}

		/// <summary>Gets the requested service.</summary>
		/// <param name="serviceType">The type of service to retrieve.</param>
		/// <returns>An instance of the service if it could be found, or <see langword="null" /> if it could not be found.</returns>
		// Token: 0x060024A9 RID: 9385 RVA: 0x00081D2C File Offset: 0x0007FF2C
		public virtual object GetService(Type serviceType)
		{
			object obj = null;
			Type[] defaultServices = this.DefaultServices;
			for (int i = 0; i < defaultServices.Length; i++)
			{
				if (serviceType.IsEquivalentTo(defaultServices[i]))
				{
					obj = this;
					break;
				}
			}
			if (obj == null)
			{
				this.Services.TryGetValue(serviceType, out obj);
			}
			if (obj is ServiceCreatorCallback)
			{
				obj = ((ServiceCreatorCallback)obj)(this, serviceType);
				if (obj != null && !obj.GetType().IsCOMObject && !serviceType.IsInstanceOfType(obj))
				{
					obj = null;
				}
				this.Services[serviceType] = obj;
			}
			if (obj == null && this._parentProvider != null)
			{
				obj = this._parentProvider.GetService(serviceType);
			}
			return obj;
		}

		/// <summary>Removes the specified service type from the service container.</summary>
		/// <param name="serviceType">The type of service to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> is <see langword="null" />.</exception>
		// Token: 0x060024AA RID: 9386 RVA: 0x00081DC8 File Offset: 0x0007FFC8
		public void RemoveService(Type serviceType)
		{
			this.RemoveService(serviceType, false);
		}

		/// <summary>Removes the specified service type from the service container.</summary>
		/// <param name="serviceType">The type of service to remove.</param>
		/// <param name="promote">
		///   <see langword="true" /> if this service should be removed from any parent service containers; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> is <see langword="null" />.</exception>
		// Token: 0x060024AB RID: 9387 RVA: 0x00081DD4 File Offset: 0x0007FFD4
		public virtual void RemoveService(Type serviceType, bool promote)
		{
			if (promote)
			{
				IServiceContainer container = this.Container;
				if (container != null)
				{
					container.RemoveService(serviceType, promote);
					return;
				}
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			this.Services.Remove(serviceType);
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x00081E18 File Offset: 0x00080018
		// Note: this type is marked as 'beforefieldinit'.
		static ServiceContainer()
		{
		}

		// Token: 0x040010E6 RID: 4326
		private ServiceContainer.ServiceCollection<object> _services;

		// Token: 0x040010E7 RID: 4327
		private IServiceProvider _parentProvider;

		// Token: 0x040010E8 RID: 4328
		private static Type[] s_defaultServices = new Type[]
		{
			typeof(IServiceContainer),
			typeof(ServiceContainer)
		};

		// Token: 0x040010E9 RID: 4329
		private static TraceSwitch s_TRACESERVICE = new TraceSwitch("TRACESERVICE", "ServiceProvider: Trace service provider requests.");

		// Token: 0x02000473 RID: 1139
		private sealed class ServiceCollection<T> : Dictionary<Type, T>
		{
			// Token: 0x060024AD RID: 9389 RVA: 0x00081E53 File Offset: 0x00080053
			public ServiceCollection() : base(ServiceContainer.ServiceCollection<T>.s_serviceTypeComparer)
			{
			}

			// Token: 0x060024AE RID: 9390 RVA: 0x00081E60 File Offset: 0x00080060
			// Note: this type is marked as 'beforefieldinit'.
			static ServiceCollection()
			{
			}

			// Token: 0x040010EA RID: 4330
			private static ServiceContainer.ServiceCollection<T>.EmbeddedTypeAwareTypeComparer s_serviceTypeComparer = new ServiceContainer.ServiceCollection<T>.EmbeddedTypeAwareTypeComparer();

			// Token: 0x02000474 RID: 1140
			private sealed class EmbeddedTypeAwareTypeComparer : IEqualityComparer<Type>
			{
				// Token: 0x060024AF RID: 9391 RVA: 0x00081E6C File Offset: 0x0008006C
				public bool Equals(Type x, Type y)
				{
					return x.IsEquivalentTo(y);
				}

				// Token: 0x060024B0 RID: 9392 RVA: 0x00081E75 File Offset: 0x00080075
				public int GetHashCode(Type obj)
				{
					return obj.FullName.GetHashCode();
				}

				// Token: 0x060024B1 RID: 9393 RVA: 0x0000219B File Offset: 0x0000039B
				public EmbeddedTypeAwareTypeComparer()
				{
				}
			}
		}
	}
}
