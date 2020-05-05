using System;
using Thinktecture.Relay.Server.Transport;
using Thinktecture.Relay.Transport;

namespace Thinktecture.Relay.Server.Factories
{
	/// <summary>
	/// An implementation of a factory to create an instance of a class implementing <see cref="ITenantConnectorAdapter{TRequest,TResponse}"/>.
	/// </summary>
	public class TenantConnectorAdapterFactory<TRequest, TResponse>
		where TRequest : IRelayClientRequest
		where TResponse : IRelayTargetResponse
	{
		/// <summary>
		/// Creates an instance of a class implementing <see cref="ITenantConnectorAdapter{TRequest,TResponse}"/>.
		/// </summary>
		/// <param name="tenantId">The unique id of the tenant.</param>
		/// <param name="connectionId">The unique id of the connection.</param>
		/// <returns>An <see cref="ITenantConnectorAdapter{TRequest,TResponse}"/>.</returns>
		public ITenantConnectorAdapter<TRequest, TResponse> Create(Guid tenantId, string connectionId)
			=> new TenantConnectorAdapter<TRequest, TResponse>(tenantId, connectionId, null); // TODO IConnectorTransport
	}
}
