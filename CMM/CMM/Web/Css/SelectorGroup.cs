namespace CMM.Web.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;


    public class SelectorGroup : List<Selector>
    {
        public override string ToString()
        {
            return string.Join(", ", this.Select(o => o.ToString()));
            
        }
    }
}

