using System;
using Tringle.Banking.Entities.Interfaces;

namespace Tringle.Banking.Entities.Concrete
{
    public class ResultLog : IObject
    {
        public bool IsError { get; set; }
        public string ReferenceNumber { get; set; } = Guid.NewGuid().ToString();
    }
}
