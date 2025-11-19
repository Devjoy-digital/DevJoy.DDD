namespace DevJoy
{
    public interface IAuditable
    {
        /// <summary>
        /// An identifier of the individual that initiated the event, or created the entity.
        /// </summary>
        /// <remarks>This will usually be the Id of the user initiating the command.</remarks>
        string CreatedBy { get; }

        /// <summary>
        /// The date and time the event occurred expressed in UTC.
        /// </summary>
        DateTimeOffset CreatedAt { get; }
    }
}
