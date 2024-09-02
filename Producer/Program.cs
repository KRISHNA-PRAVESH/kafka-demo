// See https://aka.ms/new-console-template for more 


using Confluent.Kafka;
using Producer;

class Program
{
    public static void Main(string[] args)
    {
        var config = KafkaProducerConfig.GetConfig();

        using var producer = new ProducerBuilder<Null, string>(config).Build();
        string topic = "my-topic"; //pub/sub topic which will be subscribed by consumers
        string message = args[0];
        var deliveryReport = producer.ProduceAsync(topic, new Message<Null, string> { Value = message }).Result;
        Console.WriteLine($"Produced message to {deliveryReport.Topic} partition {deliveryReport.Partition} @ offset {deliveryReport.Offset}");
    }
}