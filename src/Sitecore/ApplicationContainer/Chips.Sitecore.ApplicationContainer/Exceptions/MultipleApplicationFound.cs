using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Chips.Sitecore.ApplicationContainer.Exceptions
{
    [Serializable]
    public class MultipleApplicationFound : Exception
    {
        public MultipleApplicationFound()
        {
        }

        public MultipleApplicationFound(string message)
            : base(message)
        {
        }

        public MultipleApplicationFound(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected MultipleApplicationFound(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }

        public MultipleApplicationFound(IEnumerable<Type> applications)
            : this(string.Join(", ", applications))
        {
        }
    }
}
