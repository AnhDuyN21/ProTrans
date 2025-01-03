﻿namespace Application.ViewModels.NotificationDTOs
{
    public class NotificationDTO
    {
        public required int Id {  get; set; }
        public required string Title { get; set; }
        public required string Message { get; set; }
        public required string Author { get; set; }
        public required DateTime NotificationTime { get; set; }
        public bool isChecked { get; set; }

    }
}
