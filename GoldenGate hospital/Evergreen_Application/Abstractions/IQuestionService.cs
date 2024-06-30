using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evergreen_Domain.ViewModel;
using Evergreen_Domain;

namespace Evergreen_Application.Abstractions
{
    public interface IQuestionService
    {
        List<Questions> GetAll();
        void Create(QuestionVm questionvm);
        void Update(QuestionVm newquestionvm);
        void Delete(int id);
        QuestionVm Get(int id);
    }
}
