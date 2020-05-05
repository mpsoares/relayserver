using System;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Thinktecture.Relay.Acknowledgement;
using Thinktecture.Relay.Transport;

namespace Thinktecture.Relay.Server.Protocols.RabbitMq
{
	/// <inheritdoc cref="IServerHandler{TResponse}" />
	public class ServerHandler<TResponse> : IServerHandler<TResponse>, IDisposable
		where TResponse : class, IRelayTargetResponse
	{
		private readonly IModel _model;
		private readonly AsyncEventingBasicConsumer _responseConsumer;
		private readonly AsyncEventingBasicConsumer _acknowledgeConsumer;

		/// <inheritdoc />
		public event AsyncEventHandler<TResponse> ResponseReceived;

		/// <inheritdoc />
		public event AsyncEventHandler<IAcknowledgeRequest> AcknowledgeReceived;

		/// <summary>
		/// Initializes a new instance of <see cref="ServerHandler{TResponse}"/>.
		/// </summary>
		/// <param name="modelFactory">The <see cref="ModelFactory"/> to use.</param>
		public ServerHandler(ModelFactory modelFactory)
		{
			_model = modelFactory.Create();

			_responseConsumer = _model.ConsumeQueue($"{Constants.ResponseQueuePrefix}MISSING-ORIGIN-ID"); // TODO get origin id
			_responseConsumer.Received += async (sender, @event)
				=> await ResponseReceived.InvokeAsync(sender, JsonSerializer.Deserialize<TResponse>(@event.Body.Span));

			_acknowledgeConsumer = _model.ConsumeQueue($"{Constants.AcknowledgeQueuePrefix}MISSING-ORIGIN-ID"); // TODO get origin id
			_acknowledgeConsumer.Received += async (sender, @event)
				=> await AcknowledgeReceived.InvokeAsync(sender, JsonSerializer.Deserialize<IAcknowledgeRequest>(@event.Body.Span));
		}

		/// <inheritdoc />
		public void Dispose()
		{
			_model.CancelConsumerTags(_responseConsumer.ConsumerTags);
			_model.CancelConsumerTags(_acknowledgeConsumer.ConsumerTags);
			_model.Dispose();
		}
	}
}
