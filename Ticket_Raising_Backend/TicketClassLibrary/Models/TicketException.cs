namespace TicketClassLibrary.Exceptions
{
    public class TicketException : Exception
    {
        public int ErrorCode { get; set; }

        public TicketException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
