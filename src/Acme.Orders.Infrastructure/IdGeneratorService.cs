using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using IdGen;
using Microsoft.Extensions.Logging;
using Timer = System.Timers.Timer;
using Acme.Orders.Application.Abstracts;

namespace Acme.Orders.Infrastructure
{
    public class IdGeneratorService : IIdGenerator, IDisposable
    {
        private bool _isInitialised;
        private IdGenerator _idGenerator;
        private readonly object _lockObject = new object();
        private readonly ILogger<IdGeneratorService> _logger;

        private static readonly IdStructure _idStructure = IdStructure.Default;

        public IdGeneratorService(ILogger<IdGeneratorService> logger)
        {
            _logger = logger;
        }

        public ulong GetId()
        {
            if (_isInitialised == false)
                Initialise();

            return (ulong)_idGenerator.CreateId();
        }

        public IEnumerable<long> GetManyIds(int count)
        {
            if (_isInitialised == false)
                Initialise();

            return _idGenerator.Take(count);
        }

        private void Initialise()
        {
            lock (_lockObject)
            {
                if (_isInitialised) return;

                var epoch = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                var options = new IdGeneratorOptions(_idStructure, new DefaultTimeSource(epoch), SequenceOverflowStrategy.SpinWait);
                _idGenerator = new IdGenerator(0, options);
                _isInitialised = true;
            }
        }

        public void Dispose()
        {
        }
    }
}