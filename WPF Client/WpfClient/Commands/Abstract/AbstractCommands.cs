namespace WpfClient.Commands.Abstract
    {
    abstract class AbstractCommands
        {
        protected CommandContext CurrentContext { get; }

        protected AbstractCommands(CommandContext currentContext)
            {
            CurrentContext = currentContext;
            }
        }
    }
