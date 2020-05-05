using System;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Thinktecture.Relay.Acknowledgement;
using Thinktecture.Relay.Transport;

namespace Thinktecture.Relay.Server.Protocols.RabbitMq
{
	/// <inheritdoc cref="ITenantHandler{TRequest,TResponse}" />
	public class TenantHandler<TRequest, TResponse> : ITenantHandler<TRequest, TResponse>, IDisposable
		where TRequest : IRelayClientRequest
		where TResponse : class, IRelayTargetResponse
	{
		private readonly IModel _model;
		private readonly AsyncEventingBasicConsumer _consumer;

		/// <summary>
		/// Initializes a new instance of <see cref="TenantHandler{TRequest,TResponse}"/>.
		/// </summary>
		/// <param name="tenantId">The unique id of the tenant.</param>
		/// <param name="modelFactory">The <see cref="ModelFactory"/> to use.</param>
		/// <param name="serverHandler">An <see cref="IServerHandler{TResponse}"/>.</param>
		public TenantHandler(Guid tenantId, ModelFactory modelFactory, IServerHandler<TResponse> serverHandler)
		{
			_model = modelFactory.Create();

			// TODO acknowledge mode
			_consumer = _model.ConsumeQueue($"{Constants.RequestQueuePrefix}{tenantId}", autoAck: false, durable: true);
			_consumer.Received += OnRequestReceived;

			serverHandler.AcknowledgeReceived += (sender, request) =>
			{
				if (ulong.TryParse(request.AcknowledgeId, out var deliveryTag))
				{
					_model.BasicAck(deliveryTag, false);
				}

				return Task.CompletedTask;
			};
		}

		private Task OnRequestReceived(object sender, BasicDeliverEventArgs @event)
		{
			var request = JsonSerializer.Deserialize<TRequest>(@event.Body.Span);

			if (request.AcknowledgeMode != AcknowledgeMode.Disabled)
			{
				request.AcknowledgeOriginId = Guid.Empty; // TODO get origin id
				request.AcknowledgeId = @event.DeliveryTag.ToString();
			}

			// TODO what now? find a connection and submit
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public void Dispose()
		{
			_model.CancelConsumerTags(_consumer.ConsumerTags);
			_model.Dispose();
		}
	}
}
