namespace Service.Abstract
{
    public interface IRabbitmqService
    {
        public void ProduceMessage<T>(T message);
    }
}



