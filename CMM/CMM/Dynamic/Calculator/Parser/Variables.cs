namespace CMM.Dynamic.Calculator.Parser
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class Variables : IEnumerable<Variable>, IEnumerable
    {
        private SortedList<string, Variable> items = new SortedList<string, Variable>();

        public Variable Add(string Name)
        {
            Name = Name.Trim();
            string name = Name.ToLower();
            if (this.VariableExists(name))
            {
                return this[name];
            }
            Variable variable = new Variable(Name);
            this.items.Add(name, variable);
            return variable;
        }

        public void Clear()
        {
            this.items.Clear();
        }

        public IEnumerator<Variable> GetEnumerator()
        {
            return new VariablesEnumerator(this.items);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new VariablesEnumerator(this.items);
        }

        public bool VariableExists(string Name)
        {
            Name = Name.Trim();
            string key = Name.ToLower();
            return this.items.ContainsKey(key);
        }

        public int Count
        {
            get
            {
                return this.items.Count;
            }
        }

        public Variable this[int index]
        {
            get
            {
                return this.items[this.items.Keys[index]];
            }
        }

        public Variable this[string Name]
        {
            get
            {
                Name = Name.Trim().ToLower();
                return this.items[Name];
            }
        }
    }
}

