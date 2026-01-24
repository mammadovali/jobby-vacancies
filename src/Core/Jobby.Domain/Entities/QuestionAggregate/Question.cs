using Jobby.Domain.Entities.ApplicantAggregate;
using Jobby.Domain.Entities.Common;
using Jobby.Domain.Entities.Identity;
using Jobby.Domain.Entities.VacancyAggragate;

namespace Jobby.Domain.Entities.QuestionAggregate
{
    public class Question : Editable<User>
    {

        public int VacancyId { get; private set; }
        public Vacancy Vacancy { get; private set; }

        public string Text { get; private set; }
        public int Order { get; private set; }
        public IReadOnlyCollection<QuestionOption> Options => _options;
        private readonly List<QuestionOption> _options = new();

        public IReadOnlyCollection<ApplicantAnswer> ApplicantAnswers => _applicantAnswers;
        private readonly List<ApplicantAnswer> _applicantAnswers = new();

        protected Question() { }

        public Question(int vacancyId, string text, int order, int createdById)
        {
            VacancyId = vacancyId;
            Text = text;
            Order = order;
            SetAuditDetails(createdById);
        }

        public void Update(string text, int order, int updatedById)
        {
            Text = text;
            Order = order;
            SetEditFields(updatedById);
        }

        public bool HasAnswers()
        {
            return _applicantAnswers.Any();
        }

        public void AddOption(QuestionOption option)
        {
            _options.Add(option);
        }

        public void RemoveOption(QuestionOption option)
        {
            _options.Remove(option);
        }

        public bool IsValid()
        {
            return _options.Count >= 4
                && _options.Count(o => o.IsCorrect) == 1;
        }

        public void Reorder(int newOrder, int updatedById)
        {
            Order = newOrder;
            SetEditFields(updatedById);
        }
    }

}
