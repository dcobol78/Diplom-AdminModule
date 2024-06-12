using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace CourseShared.Models
{
    public class AchievementsViewModel
    {
        public string CourseId { get; set; }
        public List<AchievementViewModel> Achievements { get; set; }
    }

    public class AchievementViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float? ExpThreshold { get; set; }
        public string RewardName { get; set; }
        public string RewardDescription { get; set; }
        public string RewardImageUrl { get; set; }
        public string ImageUrl { get; set; }
        public string CourseName { get; set; }

        public string CourseId { get; set; }
    }

    public class UserAchievementsViewModel
    {
        public string UserId { get; set; }
        public List<AchievementViewModel> Achievements { get; set; }
    }

    public class UserItemsViewModel
    {
        public string UserId { get; set; }
        public List<ItemViewModel> Items { get; set; }
    }

    public class ItemViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
    }

    public static class TempDataExtensions
    {
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object o;
            tempData.TryGetValue(key, out o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }
    }
}
