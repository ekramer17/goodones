using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoodOnes.DAL.Abstract;
using GoodOnes.Entities;
using GoodOnes.Models.Shared;

namespace GoodOnes.Services
{
    public class QuestionsService
    {
        IQuestionRepository repo;

        public QuestionsService(IQuestionRepository repository)
        {
            repo = repository;
        }

        public IEnumerable<Question> GetQuestions()
        {
            return repo.Get();
        }
    }
}