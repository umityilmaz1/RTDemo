namespace Service.Abstract
{
    public interface IRabbitmqProducerService
    {
        public void SendProductMessage<T>(T message);
    }
}



