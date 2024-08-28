using common.Api;
using CommonLib;
using CommonLib.Models;
using ConferenceApi.Entity;
using MassTransit;


namespace ConferenceApi.Client;

public class UserRegisterService : IConsumer<UserMessageDto>
{
    private readonly IRepository<UserConf> _repository;
    public UserRegisterService(IRepository<UserConf> repository)
    {
        _repository = repository;
    }
    public async Task Consume(ConsumeContext<UserMessageDto> context)
    {
        var msg = context.Message;
        Console.WriteLine($"User {msg.Id} is registered.");

        // Map UserMessageDto to User entity
        var user = new UserConf
        {
            Id = msg.Id,
            Email = msg.Email,
            Secter = msg.Secter,
            Document = msg.Document,
            Departmnet = msg.Departmnet,

        };

        // Store the user in MongoDB via the repository
        await _repository.CreateAsync(user);
    }


}
