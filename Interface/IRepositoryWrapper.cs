using EvaluationBackend.Interface;

namespace EvaluationBackend.Repository
{
    public interface IRepositoryWrapper
    {

        IUserRepository User { get; }
        ISectionRepository Section { get; }
        ISetsRepository Sets { get; }
        IOfferRepository Offer { get; }
        ISubscriptionRepository Subscription { get; }
        ISubscriptionInfoRepositroy SubscriptionInfo { get; }
        IMuscleRepository Muscle{ get; }
        IExerciseRepository Exercise{ get; }
        ICourseRepository Course{ get; }
        IDayRepository Day { get; }
        IDayExerciseRepository DayExercise { get; }
        IMessageRepository Message { get; }



    }
}