using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetService.DomainModel.Validators
{
    public class TweetValidator : AbstractValidator<Tweet>
    {
        public TweetValidator()
        {
            RuleFor(tweet => tweet.TweeterId).NotEmpty();
            RuleFor(tweet => tweet.Body).NotEmpty();
        }
    }
}
