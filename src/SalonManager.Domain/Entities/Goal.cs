using SalonManager.Domain.Enums;

namespace SalonManager.Domain.Entities
{
    public class Goal : BaseEntity
    {
        public string? Title { get; set; }

        public string? CounterLabel { get; set; }
        public int CounterValue { get; set; }

        public string? Labels { get; set; } // string[]

        public string? LabelCurrentValues { get; set; }
        public string? CurrentValues { get; set; } // string[]

        public string? LabelFutureValues { get; set; }
        public string? FutureValues { get; set; } // string[]

        public EGoalType GoalType { get; set; }
        public string? GraphicType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public record InputGoalModel
    (
        string? Title, string? CounterLabel,
        int CounterValue, string? Labels,
        string? LabelCurrentValues, string? CurrentValues,
        string? LabelFutureValues, string? FutureValues,
        EGoalType GoalType, string GraphicType, DateTime StartDate, DateTime EndDate
    );
    public record EditGoalModel
    (
            string? Labels,
            string? LabelCurrentValues, string? CurrentValues,
            string? LabelFutureValues, string? FutureValues,
            EGoalType GoalType, string GraphicType, DateTime StartDate, DateTime EndDate
    );
}
