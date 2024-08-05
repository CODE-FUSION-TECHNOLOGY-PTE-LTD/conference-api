using common.Api;
using CommonLib.Models;
using MassTransit;

namespace ConferenceApi.Client;

public class UserRegisterService : IConsumer<User>
{
    private readonly IRepository<User> _repository;
    public UserRegisterService(IRepository<User> repository)
    {
        _repository = repository;
    }
    public Task Consume(ConsumeContext<User> context)
    {
        var msg = context.Message;
        Console.WriteLine($"User {msg.Id} is registered.");
        _repository.CreateAsync(msg);
        return Task.CompletedTask;
    }

}
