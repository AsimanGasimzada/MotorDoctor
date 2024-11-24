using Microsoft.EntityFrameworkCore;
using MotorDoctor.Business.Exceptions;
using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.Core.Entities;
using MotorDoctor.DataAccess.Localizers;
using MotorDoctor.DataAccess.Repositories.Abstractions;

namespace MotorDoctor.Business.Services.Implementations;

internal class SubscriberService : ISubscriberService
{
    private readonly ISubscriberRepository _repository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly ErrorLocalizer _errorLocalizer;

    public SubscriberService(ISubscriberRepository repository, IMapper mapper, IEmailService emailService, ErrorLocalizer errorLocalizer)
    {
        _repository = repository;
        _mapper = mapper;
        _emailService = emailService;
        _errorLocalizer = errorLocalizer;
    }

    public async Task<bool> CreateAsync(SubscriberCreateDto dto, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;

        var isExist = await _repository.IsExistAsync(x => x.Email.ToUpper() == dto.Email.ToUpper());

        if (isExist)
        {
            ModelState.AddModelError("Email", "Bu email artıq mövcuddur");
            return false;
        }

        var subscriber = _mapper.Map<Subscriber>(dto);

        await _repository.CreateAsync(subscriber);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task DeleteAsync(int id)
    {
        var subscriber = await _repository.GetAsync(id);

        if (subscriber is null)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

        _repository.Delete(subscriber);
        await _repository.SaveChangesAsync();
    }

    public async Task<List<SubscriberGetDto>> GetAllAsync()
    {
        var subscribes = await _repository.GetAll().ToListAsync();

        var dtos = _mapper.Map<List<SubscriberGetDto>>(subscribes);

        return dtos;
    }

    public async Task<SubscriberGetDto> GetAsync(int id)
    {
        var subscriber = await _repository.GetAsync(id);

        if (subscriber is null)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

        var dto = _mapper.Map<SubscriberGetDto>(subscriber);

        return dto;
    }

    public async Task<SubscriberUpdateDto> GetUpdatedDtoAsync(int id)
    {
        var subscriber = await _repository.GetAsync(id);

        if (subscriber is null)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

        var dto = _mapper.Map<SubscriberUpdateDto>(subscriber);

        return dto;
    }

    public async Task<bool> SendEmailToSubscribres(SubscriberEmailDto dto, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;

        var subscribers = await _repository.GetAll().ToListAsync();


        foreach (var subscriber in subscribers)
        {
            var emailDto = new EmailSendDto()
            {
                Body = dto.Body,
                Subject = dto.Subject,
                Attachments = dto.Attachments ?? new(),
                ToEmail = subscriber.Email
            };

            await _emailService.SendEmailAsync(emailDto);
        }

        return true;
    }

    public async Task<bool> UpdateAsync(SubscriberUpdateDto dto, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;

        var existSubscriber = await _repository.GetAsync(x => x.Id == dto.Id);

        if (existSubscriber is null)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

        var isExist = await _repository.IsExistAsync(x => x.Email == dto.Email.ToUpper() && x.Id != dto.Id);

        if (isExist)
        {
            ModelState.AddModelError("Email", "Bu email artıq mövcuddur");
            return false;
        }

        existSubscriber = _mapper.Map(dto, existSubscriber);

        _repository.Update(existSubscriber);
        await _repository.SaveChangesAsync();

        return true;
    }
}
