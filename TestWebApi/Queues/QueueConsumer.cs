using EasyNetQ;
using EasyNetQ.Consumer;
using EasyNetQ.Topology;
using System.Text;
using System.Threading.Tasks;

namespace TestWebApi.Queues
{
    public class QueueConsumer : IConsumer
    {
        private readonly IAdvancedBus _advancedBus;
        private readonly IQueue _queue;

        public QueueConsumer(IAdvancedBus advancedBus)
        {
            _queue = _advancedBus.QueueDeclare("dropqueue", true, false, false,
                default);
            var exchange = _advancedBus.ExchangeDeclare("testexchange", ExchangeType.Topic);
            _advancedBus.Bind(exchange, _queue, "*");
        }

        /// <summary>
        /// Disposes rabbitmqclient
        /// </summary>
        public void Dispose()
        {
            _advancedBus?.Dispose();
        }

        /// <summary>
        /// Starts to consume the messages that are supposed to lead to a process start.
        /// </summary>
        public void StartConsuming()
        {
            _advancedBus.Consume(_queue, ProcessAsync);
        }

        public async Task<AckStrategy> ProcessAsync(byte[] body, MessageProperties messageProperties, MessageReceivedInfo messageReceivedInfo)
        {
            var fileIdentifier = Encoding.UTF8.GetString(body);

            await Task.Yield();
            return AckStrategies.Ack;
        }
    }
}