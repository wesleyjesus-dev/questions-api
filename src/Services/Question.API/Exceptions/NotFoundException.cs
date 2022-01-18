﻿namespace Question.API.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message = "Not Found")
            : base(message)
        {
           
        }
    }
}
