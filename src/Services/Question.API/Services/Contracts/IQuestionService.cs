﻿using Question.API.Models;

namespace Question.API.Services.Contracts
{
    public interface IQuestionService
    {
        Task<List<QuestionDetail>> GetQuestions();

        Task<IEnumerable<QuestionDetail>> GetQuestions(int? limit, int? offset, string? filter);
    }
}