using System;
using System.Collections.Generic;
using GoodOnes.Models.Shared;
using GoodOnes.Entities;

namespace GoodOnes.DAL.Abstract
{
    public interface IQuestionRepository
    {
        IEnumerable<Question> Get();
    }
}