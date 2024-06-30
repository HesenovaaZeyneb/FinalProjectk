using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evergreen_Application.Abstractions;
using Evergreen_Domain;
using Evergreen_Domain.ViewModel;
using Evergreen_Persistence.DAL;

namespace Evergreen_Persistence.Concretes
{
    public class QuestionService : IQuestionService
    {
        private readonly AppDbContext _context;

        public QuestionService(AppDbContext context)
        {
            _context = context;
        }

        public void Create(QuestionVm questionvm)
        {
            Questions questions = new Questions()
            {
                Id = questionvm.Id,
                Question = questionvm.Question,
                Answer = questionvm.Answer,
            };
            _context.Questions.Add(questions);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var question = _context.Questions.FirstOrDefault(x => x.Id == id);
            _context.Questions.Remove(question);
            _context.SaveChanges();
        }

        public QuestionVm Get(int id)
        {
           var question= _context.Questions.FirstOrDefault(x=>x.Id == id);
            QuestionVm questionVm = new QuestionVm()
            {
                Id = question.Id,
                Question = question.Question,
                Answer = question.Answer,
            };
            return questionVm;
        }

        public List<Questions> GetAll()
        {
            return (_context.Questions.ToList());
        }

        public void Update(QuestionVm newquestionvm)
        {
            var oldquestion= _context.Questions.FirstOrDefault(x=>x.Id== newquestionvm.Id);
            oldquestion.Id = newquestionvm.Id;
            oldquestion.Answer= newquestionvm.Answer;
           oldquestion.Question= newquestionvm.Question;
            _context.SaveChanges();
        }
    }
}
