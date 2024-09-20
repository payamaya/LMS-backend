﻿namespace LMS.Infrastructure.Dtos
{
    public record ActivityTypeDto
    {
        public Guid Id { get; init; }
        public string ActivityTypeName { get; init; }
        public ICollection<ActivityDto>? Activities { get; init; }
    }
}