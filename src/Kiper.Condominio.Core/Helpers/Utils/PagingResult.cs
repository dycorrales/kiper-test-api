using System.Collections.Generic;

namespace Kiper.Condominio.Core.Helpers.Utils
{
    public class PagingResult<T>
    {
        public IEnumerable<T> Elements { get; set; }
        public int ElementsCount { get; set; }

        public PagingResult()
        {
            Elements = new List<T>();
            ElementsCount = 0;
        }
    }
}
