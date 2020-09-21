namespace SGR.Communicator.Models
{
    using System.Collections.Generic;

    public class QueryModel<T>
    {
        public IEnumerable<T> Data { get; set; }

        public PagingParameters Paging { get; set; }

        public int Total { get; set; }        
    }
}

