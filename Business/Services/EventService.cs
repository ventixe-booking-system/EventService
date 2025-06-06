
using Azure.Core;
using Business.Models;
using Data.Entities;
using Data.Repositories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Business.Services;

public class EventService(IEventRepository eventRepository) : IEventService
{
    private readonly IEventRepository _eventRepository = eventRepository;

    public async Task<EventResult> CreateEventAsync(CreateEventRequest request)
    {
        try
        {
            var eventEntity = new EventEntity
            {
                Image = request.Image,
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                EventDate = request.EventDate
            };

            var result = await _eventRepository.AddAsync(eventEntity);
            return result.Success
                ? new EventResult { Success = true, StatusCode = 200 }
                : new EventResult { Success = false, StatusCode = 500, Error = result.Error };
        }
        catch (Exception ex)
        {
            return new EventResult { Success = false, Error = ex.Message };
        }
    }

    public async Task<EventResult<IEnumerable<Event>>> GetEventsAsync()
    {
        var result = await _eventRepository.GetAllAsync();
        var events = result.Result?.Select(x => new Event
        {
            Id = x.Id,
            Image = x.Image,
            Title = x.Title,
            Description = x.Description,
            Location = x.Location,
            EventDate = x.EventDate
        });

        return new EventResult<IEnumerable<Event>> { Success = true, StatusCode = 200, Result = events };
    }

    public async Task<EventResult<Event?>> GetEventAsync(string eventId)
    {
        var result = await _eventRepository.GetAsync(x => x.Id == eventId);
        if(result.Success && result.Result != null)
        {
            var currentEvent = new Event
            {
                Id = result.Result.Id,
                Image = result.Result.Image,
                Title = result.Result.Title,
                Description = result.Result.Description,
                Location = result.Result.Location,
                EventDate = result.Result.EventDate
            };

            return new EventResult<Event?> { Success = true, StatusCode = 200, Result = currentEvent };
        }

        return new EventResult<Event?> { Success = false, StatusCode = 404 ,Error = "Event not found." };
    }
}
