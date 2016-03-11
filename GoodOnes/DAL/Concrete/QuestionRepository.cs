using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoodOnes.DAL;
using GoodOnes.Entities;
using GoodOnes.DAL.Abstract;
using GoodOnes.Models.Shared;

namespace GoodOnes.DAL.Concrete
{
    public class QuestionRepository : IQuestionRepository
    {
        private GOContext db;

        public QuestionRepository(GOContext dbcontext)
        {
            db = dbcontext;
        }

        public IEnumerable<Question> Get()
        {
            return db.Questions.Where(q => q.Live).OrderBy(q => q.Sequence);
        }
    }
}