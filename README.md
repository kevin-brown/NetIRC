# NetIRC

NetIRC serves as a highly flexible framework for building IRC clients using the .NET Framework.

# How is this flexible?

Most IRC frameworks don't allow you to send custom messages, and usually do not allow you to modify sent messages.  NetIRC allows you to send custom messages, receive custom messages, and detect what messages are sent, all without modifying the source code and recompling the library.

## How can I create a custom message to send?

In order to create custom messages, you just need to sub-class the `NetIRC.Messages.SendMessage` class.

```csharp
class CustomMessage : NetIRC.Messages.SendMessage
{
    public void Send(StreamWriter writer)
    {
        writer.WriteLine("CUSTOM {0}", "ARGS);
    }
}
```

The `Send` method is called when the message is passed into `Client.Send()`.  You would send this message the same way any other message would be called:

```csharp
Client client = new Client()

client.Connect(/* connection options */);
client.Send(new CustomMessage());
```

## I can change how messages are received?

NetIRC uses custom `ReceiveMessage` classes to detect and parse received messages.  These messages can be enabled and disabled at any time, including the messages that are provided with NetIRC.  Custom receive messages must sub-class the `ReceiveMessage` class.

```csharp
class AwesomeMessage : NetIRC.Messages.ReceiveMessage
{
    public static bool CheckMessage(string message, Server server)
    {
        if (message.Contains("awesome"))
        {
            return true;
        }
        
        return false;
    }
    
    public override void ProcessMessage(string message, Client client)
    {
        Console.output("An awesome message was detected");
    }
}
```

You must define two methods, `CheckMessage` and `ProcessMessage`.  `CheckMessage` must return a boolean, `true` if the message should be processed and `false` if the message should not be processed.  `ProcessMessage` is called if `CheckMessage` returns `true` and should process the actual message.  This can be anything from sending a message in a channel to changing something in the cool UI you may be creating.

### Registering and unregistering messages

Messages must be registered to the internal queue in order to process incoming messages.  The queue is not global to all clients, but is unique to the client that the message is registered through.  In order to register a message, you would call the `RegisterMessage` method of your `Client`:
```csharp
client.RegisterMessage(typeof(AwesomeMessage));
```
Internally, the default set of messages which trigger the events are registered using this method.  You can unregister messages using the `UnregisterMessage` method of your `Client`:
```csharp
client.UnregisterMessage(typeof(AwesomeMessage));
client.UnregisterMessage(typeof(NetIRC.Messages.Receive.ChatMessage));
```
This means that if you want to completely override one of the default messages, all you have to do is unregister the NetIRC version and register your own.

## Handling all sent and received messages

NetIRC provides a way of processing messages which are sent and received through `OutputWriter` classes.  By default, there are two writers which are registered, `IrcWriter` and `ConsoleWriter`.  `IrcWriter` handles the sending of the actual messages and `ConsoleWriter` outputs all received and sent messages to the console.

Output writers must sub-class `NetIRC.Output.OutputWriter` and must be registered to a `Client`.  The process of registering and unregistering writers is similar to messages.