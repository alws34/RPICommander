namespace RPICommander
{
    /*
     * https://stackoverflow.com/questions/72446615/c-sharp-pass-delegates-to-different-forms/72495262#72495262
     */
    public delegate void SendMessageToConsoleEventHandler(SendMessageToConsoleEventArgs e);
    public class SendMessageToConsoleEventArgs : EventArgs
    {
        public string Message { get; }
        public SendMessageToConsoleEventArgs(string message)
        {
            Message = message;
        }
    }

    public delegate void SetPlaceHolderEventHandler(SetPlaceHolderEventArgs arg);
    public class SetPlaceHolderEventArgs : EventArgs
    {
        public TextBox TB { get; set; }
        public SetPlaceHolderEventArgs(TextBox tb)
        {
            TB = tb;
        }
    }
}
