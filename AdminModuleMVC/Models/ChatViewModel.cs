using System;
using System.Collections.Generic;

namespace CourseShared.Models
{
    public class ChatViewModel
    {
        public string ChatId { get; set; }

        public string Name;
        public List<MessageViewModel> Messages { get; set; }

        public DateTime? LastInteraction { get; set; }
    }

    public class UserChatViewModel : ChatViewModel
    {
        public string UserName { get; set; }
    }

    public class MessageViewModel
    {
        public string AuthorName { get; set; }

        public string Content { get; set; }

        public string AuthorTitle { get; set; }

        public string AuthorAvatarUrl { get; set; }

        public DateTime? CreationTime { get; set; }
    }

    public class CourseChatViewModel : ChatViewModel
    {
        public string CourseName { get; set; }

        public string CourseId { get; set; }

        //public List<ParticipantViewModel> Participants { get; set; }
    }

    public class ParticipantViewModel
    {
        public string Name;

        public string UserId;

        public string Email;
    }

    public class IndexChatViewModel
    {
        public List<ChatViewModel> Chats { get; set; }
    }
}