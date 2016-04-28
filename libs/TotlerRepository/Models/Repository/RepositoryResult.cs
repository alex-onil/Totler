using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TotlerRepository.Models.Repository
    {
    public class RepositoryResult
    {
        private readonly ReadOnlyCollection<string> _messages;

        public RepositoryResult(bool isSuccess, IEnumerable<string> messages)
        {
            IsSuccess = isSuccess;
            _messages = new ReadOnlyCollection<string>(new List<string>(messages ?? new string[0]));
        }

        public IReadOnlyCollection<string> ErrorMessages  => _messages;
        public bool IsSuccess { get; private set; }

        }
    }
