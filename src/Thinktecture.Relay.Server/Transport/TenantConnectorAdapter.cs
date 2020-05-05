using System;
using System.Threading.Tasks;
using Thinktecture.Relay.Transport;

namespace Thinktecture.Relay.Server.Transport
{
	/// <inheritdoc />
	public class TenantConnectorAdapter<TRequest, TResponse> : ITenantConnectorAdapter<TRequest, TResponse>
		where TRequest : IRelayClientRequest
		where TResponse : IRelayTargetResponse
	{
		private readonly IConnectorTransport<TRequest> _connectorTransport;

		/// <inheritdoc />
		public Guid TenantId { get; }

		/// <inheritdoc />
		public string ConnectionId { get; }

		/// <summary>
		/// Initializes a new instance of <see cref="TenantConnectorAdapter{TRequest,TResponse}"/>.
		/// </summary>
		/// <param name="tenantId">The unique id of the tenant.</param>
		/// <param name="connectionId">The unique id of the connection.</param>
		/// <param name="connectorTransport">An <see cref="IConnectorTransport{TRequest}"/>.</param>
		public TenantConnectorAdapter(Guid tenantId, string connectionId, IConnectorTransport<TRequest> connectorTransport)
		{
			_connectorTransport = connectorTransport;
			TenantId = tenantId;
			ConnectionId = connectionId;
		}

		/// <inheritdoc />
		public Task AcknowledgeRequestAsync(string acknowledgeId)
		{
			throw new System.NotImplementedException();
		}
	}
}
