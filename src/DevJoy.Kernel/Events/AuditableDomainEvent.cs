namespace DevJoy.Events
{
    public record AuditableDomainEvent : IDomainEvent
    {
        public AuditableDomainEvent() { }
        public AuditableDomainEvent(string createdBy, DateTimeOffset createdAt)
        {
            if (createdBy == null) throw new ArgumentNullException(nameof(createdBy));
            if (string.IsNullOrEmpty(createdBy)) throw new ArgumentException("CreatedBy cannot be null or empty", nameof(createdBy));

            if (createdAt == DateTimeOffset.MaxValue
             || createdAt == DateTimeOffset.MinValue)
            {
                throw new ArgumentException("CreatedAt cannot be default", nameof(createdAt));
            }

            CreatedBy = createdBy;
            CreatedAt = createdAt;
        }

        public string CreatedBy { get; init; } = default!;
        public DateTimeOffset CreatedAt { get; init; }
    }
}