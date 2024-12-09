
using AutoMapper;
using EvaluationBackend.DATA;
using EvaluationBackend.Interface;
using EvaluationBackend.Respository;

namespace EvaluationBackend.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        private IUserRepository _user;
        private ISectionRepository _section;
        private ISetsRepository _sets;
        private IOfferRepository _offer;
        private ISubscriptionRepository _subscription;
        private ISubscriptionInfoRepositroy _subscriptionInfo;
        private IMuscleRepository _muscle;
        private IExerciseRepository _exercise;
        private ICourseRepository _course;
        private IDayRepository _day;
        private IDayExerciseRepository _dayExercise;
        private IMessageRepository _message;









        public IExerciseRepository Exercise
        {
            get
            {
                if (_exercise == null)
                {
                    _exercise = new ExerciseRepository(_context, _mapper);
                }
                return _exercise;
            }
        }
        public IMessageRepository Message
        {
            get
            {
                if (_message == null)
                {
                    _message = new MessageRepository(_context, _mapper);
                }
                return _message;
            }
        }
         public IDayExerciseRepository DayExercise
        {
            get
            {
                if (_dayExercise == null)
                {
                    _dayExercise = new DayExerciseRepository(_context, _mapper);
                }
                return _dayExercise;
            }
        }
        public IDayRepository Day
        {
            get
            {
                if (_day == null)
                {
                    _day = new DayRepository(_context, _mapper);
                }
                return _day;
            }
        }
        public ICourseRepository Course
        {
            get
            {
                if (_course == null)
                {
                    _course = new CourseRepository(_context, _mapper);
                }
                return _course;
            }
        }
        public ISectionRepository Section
        {
            get
            {
                if (_section == null)
                {
                    _section = new SectionsRepository(_context, _mapper);
                }
                return _section;
            }
        }
        public IMuscleRepository Muscle
        {
            get
            {
                if (_muscle == null)
                {
                    _muscle = new MuscleRepository(_context, _mapper);
                }
                return _muscle;
            }
        }
        public ISubscriptionInfoRepositroy SubscriptionInfo
        {
            get
            {
                if (_subscriptionInfo == null)
                {
                    _subscriptionInfo = new SubscriptionInfoRepository(_context, _mapper);
                }
                return _subscriptionInfo;
            }
        }
        public IOfferRepository Offer
        {
            get
            {
                if (_offer == null)
                {
                    _offer = new OfferRepository(_context, _mapper);
                }
                return _offer;
            }
        }
        public ISubscriptionRepository Subscription
        {
            get
            {
                if (_subscription == null)
                {
                    _subscription = new SubscriptionRepository(_context, _mapper);
                }
                return _subscription;
            }
        }
        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_context, _mapper);
                }
                return _user;
            }
        }
        public ISetsRepository Sets
        {
            get
            {
                if (_sets == null)
                {
                    _sets = new SetsRepository(_context, _mapper);
                }
                return _sets;
            }
        }


        public RepositoryWrapper(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
    }
}